using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using MVC_API.Models.CustomerConnectHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers.CustomerConnect
{
    public class PickingController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;

        //Items and Batch based on one picklist - 2 Dataset and send as single JSON - INPUT - PicklistID, UserID
        [HttpPost]
        public string jsonsStr(string json, string defaultValue, string conditionValue1, string conditionValue2, string jsonval)
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

        public DataTable PickingTotal(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectTotalPicking", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable PickingOngoing(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "",Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectPickingOngoing", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable PickingNotStarted(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectPickingNotStarted", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable PickingCompleted(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectPickingCompleted", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public string SelectPickingHeader([FromForm] PickingIn inputParams)
        {
            dm.TraceService("SelectPickingHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PostItemPickingCus> itemData = JsonConvert.DeserializeObject<List<PostItemPickingCus>>(inputParams.JSONStringCus);
                List<PostItemPickingCusOutlet> itemDataOutlet = JsonConvert.DeserializeObject<List<PostItemPickingCusOutlet>>(inputParams.JSONStringOutlet);
                List<PostItemPickingRot> itemDatas = JsonConvert.DeserializeObject<List<PostItemPickingRot>>(inputParams.JSONStringRot);
                List<PostItemPickingOrder> itemDatass = JsonConvert.DeserializeObject<List<PostItemPickingOrder>>(inputParams.JSONStringOrder);
                List<PostItemPickingProducts> itemDataZ = JsonConvert.DeserializeObject<List<PostItemPickingProducts>>(inputParams.JSONStringProducts);
               // List<PostItemPickingStatus> itemDataStatus = JsonConvert.DeserializeObject<List<PostItemPickingStatus>>(inputParams.JSONStringStatus);

                string cusCondition = jsonsStr(inputParams.JSONStringCus, " and D.csh_ID in (D.csh_ID)", " and D.csh_ID in (", ")", "cus_HeaderID");
                string outletCondition = jsonsStr(inputParams.JSONStringOutlet, " and C.cus_ID in (C.cus_ID)", " and C.cus_ID in (", ")", "ID");
                string rotCondition = jsonsStr(inputParams.JSONStringRot, " ", " and isnull(ord_assigned_rot_ID,ord_default_rot_ID)  in (", ")", "rot_ID");
                string orderCondition = jsonsStr(inputParams.JSONStringOrder, " and ord_ID in (ord_ID)", " and ord_ID in (", ")", "ordNo");
                string prdCondition = jsonsStr(inputParams.JSONStringProducts, " and D.csh_ID in (D.csh_ID)", " and D.csh_ID in (", ")", "prd_ID");
              //  string StatusCondition = jsonsStr(inputParams.JSONStringStatus, " and pih_Status in (pih_Status)", " and pih_Status in (", ")", "Status");

                string userID = inputParams.userID == null ? "0" : inputParams.userID; 
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string Mode = inputParams.Mode == null ? "0" : inputParams.Mode;

                string dateCondition = " (cast(ord_ExpectedDelDate as date) between cast('" + FromDate + "' as date) and cast('" + ToDate + "' as date))";

                string mainCondition = "";
                mainCondition += dateCondition;
                mainCondition += cusCondition;
                mainCondition += outletCondition;
                mainCondition += rotCondition;
                mainCondition += orderCondition;
             //   mainCondition += StatusCondition;

                DataTable dtPicking = new DataTable();

                if (Mode == "TotalPicking") 
                {
                     dtPicking = PickingTotal(userID, mainCondition);
                }
                else if (Mode == "PickingOngoing")
                {
                     dtPicking = PickingOngoing(userID, mainCondition);
                }
                else if (Mode == "PickingNotStarted")
                {
                     dtPicking = PickingNotStarted(userID, mainCondition);
                }
                else if (Mode == "PickingCompleted")
                {
                     dtPicking = PickingCompleted(userID, mainCondition);
                }

               

                if (dtPicking.Rows.Count > 0)
                {
                    List<PickingOut> listItems = new List<PickingOut>();
                    foreach (DataRow dr in dtPicking.Rows)
                    {

                        listItems.Add(new PickingOut
                        {
                            PickListID = dr["pih_Number"].ToString(),
                            PickingStatus = dr["Status"].ToString(),
                            ExpectedDelDate = dr["ord_ExpectedDelDate"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            cus_HeaderID = dr["csh_ID"].ToString(),
                            cus_HeaderCode = dr["csh_Code"].ToString(),
                            cus_HeaderName = dr["csh_Name"].ToString(),
                            rot_ID = dr["rot_ID"].ToString(),
                            rot_Code = dr["rot_Code"].ToString(),
                            rot_Name = dr["rot_Name"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            ord_LPONumber = dr["ord_LPONumber"].ToString(),
                            ID = dr["ID"].ToString(),
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
                dm.TraceService(" SelectPickingHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectPickingHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectPickingDetail([FromForm] PickingDetailIn inputParams)
        {
            dm.TraceService("SelectPickingDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string PickListID = inputParams.ID == null ? "0" : inputParams.ID;

                DataTable dtPicking = dm.loadList("SelPickingDetail", "sp_CustomerConnect", PickListID.ToString());

                if (dtPicking.Rows.Count > 0)
                {
                    List<PickingDetailOut> listItems = new List<PickingDetailOut>();
                    foreach (DataRow dr in dtPicking.Rows)
                    {

                        listItems.Add(new PickingDetailOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            BaseUOM = dr["BaseUOM"].ToString(),
                            Qty = dr["Qty"].ToString(),
                            OrdQty = dr["OrdQty"].ToString(),
                            OrdUOM = dr["OrdUOM"].ToString(),
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
                dm.TraceService(" SelectPickingDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectPickingDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectPickingCustomers([FromForm] PickingCustomersIn inputParams)
        {
            dm.TraceService("SelectPickingCustomers STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string Mode = inputParams.Mode == null ? "0" : inputParams.Mode;

                if(Mode == "PickingNotStarted")
                {
                    Mode = " and pih_Status in ('N')";
                }
                else if (Mode == "PickingOngoing")
                {
                    Mode = " and pih_Status in ('O','P','PR')";
                }
                else if (Mode == "PickingCompleted")
                {
                    Mode = " and pih_Status in ('PC')";
                }
                else
                {
                    Mode = " and pih_Status in ('N','O','P','PR','PC')";
                }


                string[] arr = { FromDate.ToString(), ToDate.ToString() , Mode.ToString() };
                DataTable dtPicking = dm.loadList("SelPickingCustomers", "sp_CustomerConnect", usrID.ToString(),arr);

                if (dtPicking.Rows.Count > 0)
                {
                    List<PickingCustomersOut> listItems = new List<PickingCustomersOut>();
                    foreach (DataRow dr in dtPicking.Rows)
                    {

                        listItems.Add(new PickingCustomersOut
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
                dm.TraceService(" SelectPickingCustomers Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectPickingCustomers ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}