using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.PlatformAbstractions;
using MultipartDataMediaFormatter.Infrastructure;
using MVC_API.Models;
using MVC_API.Models.CustomerConnectHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers
{
    public class HomeController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;

        //Items and Batch based on one picklist - 2 Dataset and send as single JSON - INPUT - PicklistID, UserID
        [HttpPost]

        public string AppLogin([FromForm] LoginIn inputParams)
        {
            dm.TraceService("AppLogin STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string Username = inputParams.Username == null ? "0" : inputParams.Username;
                string Password = inputParams.Password == null ? "0" : inputParams.Password;


                if (Membership.ValidateUser(inputParams.Username, inputParams.Password))
                {
                    string[] arr = { Password.ToString() };

                    DataTable dtLogin = dm.loadList("AppLogin", "sp_CustomerConnect", Username.ToString(), arr);

                    if (dtLogin.Rows.Count > 0)
                    {
                        List<LoginOut> listItems = new List<LoginOut>();
                        foreach (DataRow dr in dtLogin.Rows)
                        {

                            listItems.Add(new LoginOut
                            {
                                FirstName = dr["FirstName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                Email = dr["Email"].ToString(),
                                ContacInfo = dr["ContacInfo"].ToString(),
                                Active = dr["Active"].ToString(),
                                usrID = dr["usrID"].ToString(),
                                UserName = dr["UserName"].ToString(),
                                Mobile = dr["Mobile"].ToString(),
                                NewUser = dr["NewUser"].ToString(),
                                cus_ID = dr["cus_ID"].ToString(),
                                spm_ID = dr["spm_ID"].ToString(),
                                spm_Code = dr["spm_Code"].ToString(),
                                spm_Name = dr["spm_Name"].ToString(),
                                spm_Email = dr["spm_Email"].ToString(),
                                spm_Phone = dr["spm_Phone"].ToString(),
                                Title = dr["Title"].ToString(),
                                Descr = dr["Descr"].ToString(),
                                VersionDate = dr["VersionDate"].ToString(),
                            });
                        }

                        string JSONString = JsonConvert.SerializeObject(new
                        {
                            result = listItems
                        });

                        return JSONString;
                    }
                    else
                    {
                        JSONString = "NoDataRes";
                    }
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" AppLogin Exception - " + ex.Message.ToString());
            }
            dm.TraceService("AppLogin ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string ActivateAccount([FromForm] changepass inputParams)
        {
            dm.TraceService("ActivateAccount STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            MembershipUser user;
            try
            {
                user = Membership.GetUser(inputParams.uName);

                Boolean isSucess = user.ChangePassword(inputParams.cPass, inputParams.nPass);
                if (isSucess)
                {
                    string[] arr = { "" };
                    string svd = dm.SaveData("sp_CustomerConnect", "UpdNewUserStatus", inputParams.userID.ToString(), arr);

                    List<changePassout> listItems = new List<changePassout>();

                    listItems.Add(new changePassout
                    {
                        Mode = "1",
                        Status = "Password updated successfully"

                    });
                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    dm.TraceService("ActivateAccount Success -" + DateTime.Now);
                    return JSONString;
                }
                else
                {
                    List<changePassout> listItems = new List<changePassout>();

                    listItems.Add(new changePassout
                    {
                        Mode = "0",
                        Status = "Your old password is wrong, please try again"
                    });
                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    dm.TraceService("ActivateAccount Failure -" + DateTime.Now);
                    return JSONString;
                }


            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL";
            }

            dm.TraceService("ActivateAccount ENDED - " + DateTime.Now);
            dm.TraceService("==================");

            return JSONString;


        }

        public string jsonsStr(string json , string defaultValue, string conditionValue1, string conditionValue2, string jsonval)
        {
            try
            {
                // Parse the JSON data as an array
                JArray jsonArray = JArray.Parse(json);

                // Extract the "cus_ID" values and convert them to a comma-separated string
                string csvString = string.Join(", ", jsonArray.Select(j => j[jsonval].ToString()));

                if (!string.IsNullOrEmpty(csvString))
                {
                    return conditionValue1 + csvString + conditionValue2;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception ex)
            {
                return defaultValue;
            }

        }

        public string PickingTotal(string usrID , string MainCondition)
        {
            string Mode = "P";
          //  string MainCondition = mainConditions(Mode);

            string status = " and pih_Status in ('N','O' ,'PC','P','PR','UP')";

            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelPickingCounts", "sp_OrderDashboards", MainCondition, arr);
            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string PickingCompleted(string usrID, string MainCondition)
        {
            string Mode = "PC";
           // string MainCondition = mainConditions(Mode);

            string status = " and pih_Status in ('PC')";

            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelPickingCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string PickingOngoing(string usrID, string MainCondition)
        {
            string Mode = "PO";
           // string MainCondition = mainConditions(Mode);

            string status = " and pih_Status in ('O','P','PR' )";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelPickingCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string PickingNotStarted(string usrID, string MainCondition)
        {
            string Mode = "PN";
           // string MainCondition = mainConditions(Mode);
            string status = " and pih_Status in ('N' )";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelPickingCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }

        public string DispatchCompleted(string usrID, string MainCondition)
        {
            string Mode = "D";
           // string MainCondition = mainConditions(Mode);
            string status = " and A.Status in ('DD','LD','DR','ND','R') ";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelDispatchCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string DispatchPending(string usrID, string MainCondition)
        {
            string Mode = "D";
           // string MainCondition = mainConditions(Mode);
            string status = " and A.Status in ('D') ";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelDispatchCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string DispatchCancelled(string usrID, string MainCondition)
        {
            string Mode = "D";
           // string MainCondition = mainConditions(Mode);
            string status = " and A.Status in ('CN') ";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelDispatchCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string TotalDispatch(string usrID, string MainCondition)
        {
            string Mode = "D";
           // string MainCondition = mainConditions(Mode);
            string status = "";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelDispatchCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }

        public string LoadInCompleted(string usrID, string MainCondition)
        {
            string Mode = "L";
           // string MainCondition = mainConditions(Mode);
            string status = " and A.Status in ('LD','DR','ND')";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelLoadInCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string LoadInTotal(string usrID, string MainCondition)
        {
            string Mode = "L";
           // string MainCondition = mainConditions(Mode);
            string status = " and A.Status in ('LD','DR','ND','DD','R')";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelLoadInCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string LoadInPending(string usrID, string MainCondition)
        {
            string Mode = "L";
           // string MainCondition = mainConditions(Mode);
            string status = " and A.Status in ('DD') ";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelLoadInCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string LoadInCancelled(string usrID, string MainCondition)
        {
            string Mode = "L";
           // string MainCondition = mainConditions(Mode);
            string status = " and A.Status in ('R') ";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelLoadInCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string LoadInReschedule(string usrID, string MainCondition)
        {
            string Mode = "L";
           // string MainCondition = mainConditions(Mode);
            string status = " and A.Status in ('RS','CF') ";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelLoadInCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }

        public string TotalDelivery(string usrID, string MainCondition)
        {
            string Mode = "DL";
           // string MainCondition = mainConditions(Mode);
            string status = " and ISNULL(A.Status, 'NUL') in ('DR','LD','ND','NUL') ";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelDeliveryCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string DeliveryFD(string usrID, string MainCondition)
        {
            string Mode = "DL";
           // string MainCondition = mainConditions(Mode);
            string status = " and ISNULL(A.Status, 'NUL') in ('DR' ) and isnull(dln_IsPartiallyDelivered,'N') = 'N' ";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelDeliveryCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string DeliveryPD(string usrID, string MainCondition)
        {
            string Mode = "DL";
           // string MainCondition = mainConditions(Mode);
            string status = " and ISNULL(A.Status, 'NUL') in ('DR' ) and isnull(dln_IsPartiallyDelivered,'N') = 'Y' ";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelDeliveryCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;

        }
        public string DeliveryND(string usrID, string MainCondition)
        {
            string Mode = "NDL";
           // string MainCondition = mainConditions(Mode);
            string status = " and ISNULL(A.Status, 'NUL') in ('ND') ";
            string Uid = usrID.ToString();
            string[] arr = { status,Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelDeliveryCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;

        }
        public string DeliveryNP(string usrID, string MainCondition)
        {
            string Mode = "NDL";
           // string MainCondition = mainConditions(Mode);
            string status = " and ISNULL(A.Status, 'NUL') in ('LD') ";
            string Uid = usrID.ToString();
            string[] arr = { status, Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelDeliveryCounts", "sp_OrderDashboards", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }

        public string TotalPickOrder(string usrID, string MainCondition)
        {
            string count;
            string[] arr = { "", usrID };
            //string Mode = "OR";
            // string MainCondition = mainConditions(Mode);
           
                DataTable dtTotalPickingOrders = new DataTable();
                dtTotalPickingOrders = dm.loadList("SelectTotalPickingOrderCount", "sp_OrderDashboards", MainCondition, arr);
                 count = dtTotalPickingOrders.Rows[0]["numbers"].ToString();
                  return count;
        }
        public string TotalPickCompleteOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtPickingCompletedOrder = new DataTable();
            dtPickingCompletedOrder = dm.loadList("SelectTotalPickingOrderCount", "sp_OrderDashboards", MainCondition, arr);
           string count = dtPickingCompletedOrder.Rows[0]["numbers"].ToString();

            return count;
        }
        public string TotalPickOngoingOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtPickingOngoingOrder = new DataTable();
                dtPickingOngoingOrder = dm.loadList("SelectPickingOngoingOrderCount", "sp_OrderDashboards", MainCondition, arr);
                 string count = dtPickingOngoingOrder.Rows[0]["numbers"].ToString();
            return count;
        }
        public string TotalPickNSOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtPickingNSOrder = new DataTable();
                dtPickingNSOrder = dm.loadList("SelectPickingNotStartedOrderCount", "sp_OrderDashboards", MainCondition, arr);
                string count = dtPickingNSOrder.Rows[0]["numbers"].ToString();
            return count;
        }
          
        public string TotalDispatchOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            //string Mode = "D";
            //string MainCondition = mainConditions(Mode);
           
                DataTable dtTotalDispatchOrder = new DataTable();
                dtTotalDispatchOrder = dm.loadList("SelectTotalDispatchOrderCount", "sp_OrderDashboards", MainCondition, arr);
                string count = dtTotalDispatchOrder.Rows[0]["numbers"].ToString();
                return count;

        }
        public string TotalDisComOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtDispatchCompletedOrder = new DataTable();
            dtDispatchCompletedOrder = dm.loadList("SelectDispatchCompletedOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtDispatchCompletedOrder.Rows[0]["numbers"].ToString();
            return count;
        }
        public string TotalDisCanOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtDispatchCancelledOrder = new DataTable();
            dtDispatchCancelledOrder = dm.loadList("SelectDispatchCancelledOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtDispatchCancelledOrder.Rows[0]["numbers"].ToString();
            return count;
        }
        public string TotalDisResOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtDispatchRescheduleOrder = new DataTable();
            dtDispatchRescheduleOrder = dm.loadList("SelectDispatchRescheduleOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtDispatchRescheduleOrder.Rows[0]["numbers"].ToString();
            return count;
        }

        public string TotalLoadInOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtTotalLoadInOrders = new DataTable();
            dtTotalLoadInOrders = dm.loadList("SelectTotalLoadInOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtTotalLoadInOrders.Rows[0]["numbers"].ToString();
            return count;
        }
        public string TotalComLoadInOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtLoadInCompletedOrder = new DataTable();
            dtLoadInCompletedOrder = dm.loadList("SelectLoadInCompletedOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtLoadInCompletedOrder.Rows[0]["numbers"].ToString();
            return count;
        }
        public string TotalPenLoadInOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtLoadInPendingOrder = new DataTable();
            dtLoadInPendingOrder = dm.loadList("SelectLoadInPendingOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtLoadInPendingOrder.Rows[0]["numbers"].ToString();
            return count;
        }
        public string TotalRejLoadInOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtLoadInRejecetedOrder = new DataTable();
            dtLoadInRejecetedOrder = dm.loadList("SelectLoadInRejectedOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtLoadInRejecetedOrder.Rows[0]["numbers"].ToString();
            return count;
        }
        public string TotalDeliveryOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtFullyDeliveryOrders = new DataTable();
            dtFullyDeliveryOrders = dm.loadList("SelectFullyDeliveryOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtFullyDeliveryOrders.Rows[0]["numbers"].ToString();
            return count;
        }
        public string TotalPartialDeliveryOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtPartialDeliveryOrder = new DataTable();
            dtPartialDeliveryOrder = dm.loadList("SelectPartiallyDeliveryOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtPartialDeliveryOrder.Rows[0]["numbers"].ToString();
            return count;
        }
        public string TotalNDDeliveryOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtNotDeliveredOrder = new DataTable();
            dtNotDeliveredOrder = dm.loadList("SelectNotDeliveryOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtNotDeliveredOrder.Rows[0]["numbers"].ToString();
            return count;
        }
        public string TotalNPDeliveryOrder(string usrID, string MainCondition)
        {
            string[] arr = { "", usrID };
            DataTable dtNotProcessedOrder = new DataTable();
            dtNotProcessedOrder = dm.loadList("SelectNotProcessedDeliveryOrderCount", "sp_OrderDashboards", MainCondition, arr);
            string count = dtNotProcessedOrder.Rows[0]["numbers"].ToString();
            return count;
        }

        public string CarryForwardOrder(string usrID, string MainCondition)
        {          
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelCFDispatchCounts", "sp_CustomerConnect", MainCondition, arr);

            string count = dtOrder.Rows[0]["count"].ToString();
            return count;
        }
        public string SelectDashboardCounts([FromForm] HomeCountIn inputParams)
        {
            dm.TraceService("SelectDashboardCounts STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {

                List<PostItemDatasCus> itemData = JsonConvert.DeserializeObject<List<PostItemDatasCus>>(inputParams.JSONStringCus);
                List<PostItemDatasCusOutlet> itemDataOutlet = JsonConvert.DeserializeObject<List<PostItemDatasCusOutlet>>(inputParams.JSONStringOutlet);
                List<PostItemDatasRot> itemDatas = JsonConvert.DeserializeObject<List<PostItemDatasRot>>(inputParams.JSONStringRot);
                List<PostItemDatasOrder> itemDatass = JsonConvert.DeserializeObject<List<PostItemDatasOrder>>(inputParams.JSONStringOrder);
                List<PostItemDatasProducts> itemDataZ = JsonConvert.DeserializeObject<List<PostItemDatasProducts>>(inputParams.JSONStringProducts);

               
              string cusCondition =   jsonsStr(inputParams.JSONStringCus , " and D.csh_ID in (D.csh_ID)", " and D.csh_ID in ("  , ")" , "cus_HeaderID");
              string outletCondition = jsonsStr(inputParams.JSONStringOutlet, " and C.cus_ID in (C.cus_ID)", " and C.cus_ID in (", ")", "ID");
              string orderCondition = jsonsStr(inputParams.JSONStringOrder, " and ord_ID in (ord_ID)", " and ord_ID in (", ")", "ordNo");
              string prdCondition = jsonsStr(inputParams.JSONStringProducts, " and D.csh_ID in (D.csh_ID)", " and D.csh_ID in (", ")", "prd_ID");

                string userID = inputParams.userID == null ? "0" : inputParams.userID; 
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;

                string PickrotCondition = jsonsStr(inputParams.JSONStringRot, " ", " and isnull(ord_assigned_rot_ID,ord_default_rot_ID)  in (", ")", "rot_ID");
                string DesprotCondition = jsonsStr(inputParams.JSONStringRot, " ", " and dsp_rot_ID  in (", ")", "rot_ID");
                string DelrotCondition = jsonsStr(inputParams.JSONStringRot, " ", " and dsp_rot_ID  in (", ")", "rot_ID");

                string pickdateCondition = " (cast(ord_ExpectedDelDate as date) between cast('" + FromDate + "' as date) and cast('" + ToDate + "' as date))";
                string despdateCondition = " (cast(A.CreatedDate as date) between cast('" + FromDate + "' as date) and cast('" + ToDate + "' as date))";
                string deldateCondition = " (cast(E.CreatedDate as date) between cast('" + FromDate + "' as date) and cast('" + ToDate + "' as date))";
              //  string delND_NPdateCondition = " (cast(E.CreatedDate as date) between cast('" + FromDate + "' as date) and cast('" + ToDate + "' as date))";
                string TotalOrderDateCondition = " (cast(H.CreatedDate as date) between cast('" + FromDate + "' as date) and cast('" + ToDate + "' as date))";


                string mainCondition = "";            
                mainCondition += cusCondition;
                mainCondition += outletCondition;             
                mainCondition += orderCondition;

                string pickmainCondition = "";
                string despmainCondition = "";
                string delmainCondition = "";
                //string deliverymainCondition = "";
                string TotalOrderCondition = "";

                //Picking Condition
                pickmainCondition += pickdateCondition;
                pickmainCondition += PickrotCondition;
                pickmainCondition += mainCondition;

                //Despatch and LoadIn condition
                despmainCondition += despdateCondition;
                despmainCondition += DesprotCondition;
                despmainCondition += mainCondition;

                //Delivery condition
                delmainCondition += deldateCondition;
                delmainCondition += DelrotCondition;
                delmainCondition += mainCondition;

                //Delivery NP & ND condition
                //deliverymainCondition += delND_NPdateCondition;
                //deliverymainCondition += DelrotCondition;
                //deliverymainCondition += mainCondition;

                //Total Despatch & LoadIn
                TotalOrderCondition += TotalOrderDateCondition;
                TotalOrderCondition += DesprotCondition;
                TotalOrderCondition += mainCondition;

                string pickTotalCount =  PickingTotal(userID , pickmainCondition);
                string PickingNotStartedCount = PickingNotStarted(userID, pickmainCondition);
                string PickingOngoingCount = PickingOngoing(userID, pickmainCondition);
                string PickingCompletedCount = PickingCompleted(userID, pickmainCondition);

                string DispatchTotalCount = TotalDispatch(userID, despmainCondition);
                string DispatchPendingCount = DispatchPending(userID, despmainCondition);
                string DispatchCompletedCount = DispatchCompleted(userID, despmainCondition);
                string DispatchCancelledCount = DispatchCancelled(userID, despmainCondition);

                string LoadInTotalCount = LoadInTotal(userID, despmainCondition);
                string LoadInPendingCount = LoadInPending(userID, despmainCondition);
                string LoadInCompletedCount = LoadInCompleted(userID, despmainCondition);

                string LoadInCancelledCount = LoadInCancelled(userID, despmainCondition);
                string LoadInRescheduleCount = LoadInReschedule(userID, despmainCondition);

                string TotalDel = TotalDelivery(userID, delmainCondition);
                string FDCount = DeliveryFD(userID, delmainCondition);
                string PDCount = DeliveryPD(userID, delmainCondition);
                string NDCount = DeliveryND(userID, delmainCondition);
                string NPCount = DeliveryNP(userID, delmainCondition);

                string TotalPickingOrders = TotalPickOrder(userID, pickmainCondition);
                string TotalPickingOngoingOrders = TotalPickOngoingOrder(userID, pickmainCondition);
                string TotalPickingNotStartedOrders = TotalPickNSOrder(userID, pickmainCondition);
                string TotalPickingCompletedOrders = TotalPickCompleteOrder(userID, pickmainCondition);

                string TotalDesOrders = TotalDispatchOrder(userID, TotalOrderCondition);
                string TotalDesCompletedOrders = TotalDisComOrder(userID, TotalOrderCondition);
                string TotalDesCancelledOrders = TotalDisCanOrder(userID, TotalOrderCondition);
                string TotalDesRescheduleOrders = TotalDisResOrder(userID, TotalOrderCondition);

                string TotalLoadInOrders = TotalLoadInOrder(userID, TotalOrderCondition);
                string TotalComLoadInOrders = TotalComLoadInOrder(userID, TotalOrderCondition);
                string TotalPenLoadInOrders = TotalPenLoadInOrder(userID, TotalOrderCondition);
                string TotalRejLoadInOrders = TotalRejLoadInOrder(userID, TotalOrderCondition);

                string TotalFullDeliveryOrders = TotalDeliveryOrder(userID, delmainCondition);
                string TotalPartialDeliveryOrders = TotalPartialDeliveryOrder(userID, delmainCondition);
                string TotalNDDeliveryOrders = TotalNDDeliveryOrder(userID, delmainCondition);
                string TotalNPDeliveryOrders = TotalNPDeliveryOrder(userID, delmainCondition);

                string CarryForwardOrders = CarryForwardOrder(userID, mainCondition);

                //if (dtCount.Rows.Count > 0)
                //{
                List<Counts> listItems = new List<Counts>();


                listItems.Add(new Counts
                {
                    PickingTotal = pickTotalCount.ToString(),
                    PickingNotStarted = PickingNotStartedCount.ToString(),
                    PickingOngoing = PickingOngoingCount.ToString(),
                    PickingCompleted = PickingCompletedCount.ToString(),

                    DispatchTotal = DispatchTotalCount.ToString(),
                    DispatchPending = DispatchPendingCount.ToString(),
                    DispatchCompleted = DispatchCompletedCount.ToString(),
                    DispatchCancelled = DispatchCancelledCount.ToString(),

                    LoadInTotal = LoadInTotalCount.ToString(),
                    LoadInPending = LoadInPendingCount.ToString(),
                    LoadInCompleted = LoadInCompletedCount.ToString(),
                    LoadInCancelled = LoadInCancelledCount.ToString(),
                    LoadInReschedule = LoadInRescheduleCount.ToString(),

                    TotalDelivery = TotalDel.ToString(),
                    FD = FDCount.ToString(),
                    PD = PDCount.ToString(),
                    ND = NDCount.ToString(),
                    NP = NPCount.ToString(),

                    TotalPickingOrders = TotalPickingOrders.ToString(),
                    TotalPickingOngoingOrders = TotalPickingOngoingOrders.ToString(),
                    TotalPickingNotStartedOrders = TotalPickingNotStartedOrders.ToString(),
                    TotalPickingCompletedOrders = TotalPickingCompletedOrders.ToString(),

                    TotalDesOrders = TotalDesOrders.ToString(),
                    TotalDesCompletedOrders = TotalDesCompletedOrders.ToString(),
                    TotalDesCancelledOrders = TotalDesCancelledOrders.ToString(),
                    TotalDesRescheduleOrders = TotalDesRescheduleOrders.ToString(),

                    TotalLoadInOrders = TotalLoadInOrders.ToString(),
                    TotalComLoadInOrders = TotalComLoadInOrders.ToString(),
                    TotalPenLoadInOrders = TotalPenLoadInOrders.ToString(),
                    TotalRejLoadInOrders = TotalRejLoadInOrders.ToString(),

                    TotalFullDeliveryOrders = TotalFullDeliveryOrders.ToString(),
                    TotalPartialDeliveryOrders = TotalPartialDeliveryOrders.ToString(),
                    TotalNDDeliveryOrders = TotalNDDeliveryOrders.ToString(),
                    TotalNPDeliveryOrders = TotalNPDeliveryOrders.ToString(),

                    CarryForwardOrders = CarryForwardOrders.ToString(),


                }) ;
                  

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                //}
                //else
                //{
                //    JSONString = "NoDataRes";
                //}
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelectDashboardCounts Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectDashboardCounts ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
      
        public string SelOrderTrack([FromForm] OrderTrackIn inputParams)
        {
            dm.TraceService("SelOrderTrack STARTED -" + DateTime.Now);
            dm.TraceService("====================");
            
            try
            {
                string DispatchID = inputParams.DispatchID == null ? "0" : inputParams.DispatchID;

                DataTable dtCount = dm.loadList("SelOrderTrack", "sp_CustomerConnect", DispatchID.ToString());

                if (dtCount.Rows.Count > 0)
                {
                    List<OrderTrackOut> listItems = new List<OrderTrackOut>();
                    foreach (DataRow dr in dtCount.Rows)
                    {

                        listItems.Add(new OrderTrackOut
                        {
                            DispatchID = dr["dsp_DispatchID"].ToString(),
                            Track = dr["olc_Remarks"].ToString(),
                            ord_ID = dr["olc_ord_ID"].ToString(),
                            Mode = dr["olc_Mode"].ToString(),
                            Date = dr["Date"].ToString(),
                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelOrderTrack Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelOrderTrack ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelReasons()
        {
            dm.TraceService("SelReasons STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
               

                DataTable dtrsn = dm.loadList("SelReason", "sp_CustomerConnect");

                if (dtrsn.Rows.Count > 0)
                {
                    List<ReasonOut> listItems = new List<ReasonOut>();
                    foreach (DataRow dr in dtrsn.Rows)
                    {

                        listItems.Add(new ReasonOut
                        {
                            rsn_ID = dr["rsn_ID"].ToString(),
                            rsn_Name = dr["rsn_Name"].ToString(),
                            rsn_ArabicName = dr["rsn_ArabicName"].ToString(),
                            rsn_Type = dr["rsn_Type"].ToString(),

                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelReasons Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelReasons ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectCustomersForDashboard([FromForm] CusDashboardIN inputParams)
        {
            dm.TraceService("SelectCustomersForDashboard STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string userID = inputParams.userID == null ? "0" : inputParams.userID;
             
                DataTable dtTrnsIn = dm.loadList("SelCustomersForDashboard", "sp_CustomerConnect", userID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<CusDashboardOUT> listItems = new List<CusDashboardOUT>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new CusDashboardOUT
                        {

                            cus_HeaderID = dr["csh_ID"].ToString(),
                            cus_HeaderCode = dr["csh_Code"].ToString(),
                            cus_HeaderName = dr["csh_Name"].ToString(),

                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelectCustomersForDashboard Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectCustomersForDashboard ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectOutlets([FromForm] OutletIn inputParams)
        {
            dm.TraceService("SelectOutlets STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string csh_ID = inputParams.csh_ID == null ? "0" : inputParams.csh_ID;

                string[] arr = { csh_ID.ToString() };
                DataTable dtitem = dm.loadList("SelOutlets", "sp_CustomerConnect", usrID.ToString(),arr);

                if (dtitem.Rows.Count > 0)
                {
                    List<OutletOut> listItems = new List<OutletOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new OutletOut
                        {
                            ID = dr["ID"].ToString(),
                            Name = dr["Name"].ToString(),
                     

                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelectOutlets Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectOutlets ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectRoute([FromForm] RouteIn inputParams)
        {
            dm.TraceService("SelectRoute STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string Cus_ID = inputParams.Cus_ID == null ? "0" : inputParams.Cus_ID;

                string[] arr = { Cus_ID.ToString() };
                DataTable dtitem = dm.loadList("SelRoute", "sp_CustomerConnect", usrID.ToString(),arr);

                if (dtitem.Rows.Count > 0)
                {
                    List<RouteOut> listItems = new List<RouteOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new RouteOut
                        {
                            rot_ID = dr["rot_ID"].ToString(),
                            rot_Name = dr["rot_Name"].ToString(),


                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelectRoute Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectRoute ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectOrders([FromForm] OrderIn inputParams)
        {
            dm.TraceService("SelectOrders STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string Cus_ID = inputParams.Cus_ID == null ? "0" : inputParams.Cus_ID;
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;

                string pickdateCondition = "";
                pickdateCondition = " and (cast(ord_ExpectedDelDate as date) between cast('" + FromDate + "' as date) and cast('" + ToDate + "' as date))";

                string[] arr = { Cus_ID.ToString() ,  pickdateCondition.ToString() };
                DataTable dtitem = dm.loadList("SelOrders", "sp_CustomerConnect", usrID.ToString(),arr);

                if (dtitem.Rows.Count > 0)
                {
                    List<OrderOut> listItems = new List<OrderOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new OrderOut
                        {
                            ord_ID = dr["ord_ID"].ToString(),
                            OrderID = dr["OrderID"].ToString(),


                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelectOrders Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectOrders ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectProducts([FromForm] ProductsIn inputParams)
        {
            dm.TraceService("SelectProducts STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string ord_ID = inputParams.ord_ID == null ? "0" : inputParams.ord_ID;

                string[] arr = { ord_ID.ToString() };
                DataTable dtitem = dm.loadList("SelProducts", "sp_CustomerConnect", usrID.ToString(),arr);

                if (dtitem.Rows.Count > 0)
                {
                    List<ProductsOut> listItems = new List<ProductsOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new ProductsOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),


                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelectProducts Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectProducts ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectStatus([FromForm] StatusIn inputParams)
        {
            dm.TraceService("SelectStatus STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PostItemStatus> itemDataStatus = JsonConvert.DeserializeObject<List<PostItemStatus>>(inputParams.JSONStringStatus);

                string InputXmlStatus = "";
                using (var sw2 = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw2))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostItemStatus id in itemDataStatus)
                        {
                            string[] arr = { id.Status.ToString() };
                            string[] arrName = { "Status" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXmlStatus = sw2.ToString();
                }
                DataTable dtitem = dm.loadList("SelStatus", "sp_CustomerConnect", InputXmlStatus.ToString());

                if (dtitem.Rows.Count > 0)
                {
                    List<StatusOut> listItems = new List<StatusOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new StatusOut
                        {
                            sts_Type = dr["sts_Type"].ToString(),
                            sts_Name = dr["sts_Name"].ToString(),


                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelectStatus Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectStatus ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectStatusForDespatch([FromForm] StatusIn inputParams)
        {
            dm.TraceService("SelectStatusForDespatch STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PostItemStatus> itemDataStatus = JsonConvert.DeserializeObject<List<PostItemStatus>>(inputParams.JSONStringStatus);

                string InputXmlStatus = "";
                using (var sw2 = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw2))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostItemStatus id in itemDataStatus)
                        {
                            string[] arr = { id.Status.ToString() };
                            string[] arrName = { "Status" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXmlStatus = sw2.ToString();
                }
                DataTable dtitem = dm.loadList("SelStatusForDespatch", "sp_CustomerConnect", InputXmlStatus.ToString());

                if (dtitem.Rows.Count > 0)
                {
                    List<StatusOut> listItems = new List<StatusOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new StatusOut
                        {
                            sts_Type = dr["sts_Type"].ToString(),
                            sts_Name = dr["sts_Name"].ToString(),


                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelectStatusForDespatch Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectStatusForDespatch ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelVersionDetails([FromForm] VersionDetailIn inputParams)
        {
            dm.TraceService("SelVersionDetails STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string Type = inputParams.Type == null ? "0" : inputParams.Type;
               
               
                DataTable dtitem = dm.loadList("SelVersionDetail", "sp_CustomerConnect", Type.ToString());

                if (dtitem.Rows.Count > 0)
                {
                    List<VersionDetailOut> listItems = new List<VersionDetailOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new VersionDetailOut
                        {
                            ver_code = dr["ver_code"].ToString(),
                            ver_name = dr["ver_name"].ToString(),
                            url = dr["redir_url"].ToString(),
                            msg = dr["msg"].ToString(),


                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelVersionDetails Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelVersionDetails ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelFooterLogo()
        {
            dm.TraceService("SelFooterLogo STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {


                DataTable dt = dm.loadList("SelLogoFooterPath", "sp_CustomerConnect");
                string url = ConfigurationManager.AppSettings.Get("CustomerConnectUrl");

                if (dt.Rows.Count > 0)
                {
                    List<FooterLogo> listItems = new List<FooterLogo>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        listItems.Add(new FooterLogo
                        {
                            footerImage = url + dr["footerImage"].ToString(),
                            platform = dr["platform"].ToString(),

                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelFooterLogo Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelFooterLogo ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string UpdateCusConnectUserAppVersion([FromForm] CCVersionIn inputParams)
        {
            dm.TraceService("UpdateUserAppVersion STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;
                string Version = inputParams.Version == null ? "0" : inputParams.Version;


                string[] ar = { Version.ToString() };
                DataTable dt = dm.loadList("UpdateUserAppVersion", "sp_CustomerConnect", UserID.ToString(), ar);

                if (dt.Rows.Count > 0)
                {
                    List<CCVersionOut> listItems = new List<CCVersionOut>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        listItems.Add(new CCVersionOut
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),
                            Descr = dr["Descr"].ToString()

                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" UpdateUserAppVersion Exception - " + ex.Message.ToString());
            }
            dm.TraceService("UpdateUserAppVersion ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}