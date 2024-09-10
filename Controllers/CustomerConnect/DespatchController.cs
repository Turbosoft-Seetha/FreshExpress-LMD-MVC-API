using iTextSharp.text.pdf.qrcode;
using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using MVC_API.Models.CustomerConnectHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers.CustomerConnect
{
    public class DespatchController : Controller
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

        public DataTable DispatchTotal(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectDispatchTotal", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable DispatchCancelled(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectDispatchCancelled", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable DispatchReschedule(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectLoadInReschedule", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable DispatchCompleted(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectDispatchCompleted", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
     
        public string SelectDespatchHeader([FromForm] DispatchIn inputParams)
        {
            dm.TraceService("SelectDespatchHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PostItemDespatchCus> itemData = JsonConvert.DeserializeObject<List<PostItemDespatchCus>>(inputParams.JSONStringCus);
                List<PostItemDespatchCusOutlet> itemDataOutlet = JsonConvert.DeserializeObject<List<PostItemDespatchCusOutlet>>(inputParams.JSONStringOutlet);
                List<PostItemDespatchRot> itemDatas = JsonConvert.DeserializeObject<List<PostItemDespatchRot>>(inputParams.JSONStringRot);
                List<PostItemDespatchOrder> itemDatass = JsonConvert.DeserializeObject<List<PostItemDespatchOrder>>(inputParams.JSONStringOrder);
                List<PostItemDespatchProducts> itemDataZ = JsonConvert.DeserializeObject<List<PostItemDespatchProducts>>(inputParams.JSONStringProducts);
                //  List<PostItemDespatchStatus> itemDataStatus = JsonConvert.DeserializeObject<List<PostItemDespatchStatus>>(inputParams.JSONStringStatus);

                string cusCondition = jsonsStr(inputParams.JSONStringCus, " and D.csh_ID in (D.csh_ID)", " and D.csh_ID in (", ")", "cus_HeaderID");
                string outletCondition = jsonsStr(inputParams.JSONStringOutlet, " and C.cus_ID in (C.cus_ID)", " and C.cus_ID in (", ")", "ID");
                string rotCondition = jsonsStr(inputParams.JSONStringRot, " ", " and dsp_rot_ID  in (", ")", "rot_ID");
                string orderCondition = jsonsStr(inputParams.JSONStringOrder, " and ord_ID in (ord_ID)", " and ord_ID in (", ")", "ordNo");
                string prdCondition = jsonsStr(inputParams.JSONStringProducts, " and D.csh_ID in (D.csh_ID)", " and D.csh_ID in (", ")", "prd_ID");
                //  string StatusCondition = jsonsStr(inputParams.JSONStringStatus, " and pih_Status in (pih_Status)", " and pih_Status in (", ")", "Status");

                string userID = inputParams.userID == null ? "0" : inputParams.userID;
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string Mode = inputParams.Mode == null ? "0" : inputParams.Mode;

                string dateCondition = " (cast(dsp_ExpDeliveryDate as date) between cast('" + FromDate + "' as date) and cast('" + ToDate + "' as date))";

                string mainCondition = "";
                mainCondition += dateCondition;
                mainCondition += cusCondition;
                mainCondition += outletCondition;
                mainCondition += rotCondition;
                mainCondition += orderCondition;
                //   mainCondition += StatusCondition;

                DataTable dtDispatch = new DataTable();

                if (Mode == "DispatchTotal")
                {
                    dtDispatch = DispatchTotal(userID, mainCondition);
                }
                else if (Mode == "DispatchCancelled")
                {
                    dtDispatch = DispatchCancelled(userID, mainCondition);
                }
                else if (Mode == "DispatchReschedule")
                {
                    dtDispatch = DispatchReschedule(userID, mainCondition);
                }
                else if (Mode == "DispatchCompleted")
                {
                    dtDispatch = DispatchCompleted(userID, mainCondition);
                }

                
                if (dtDispatch.Rows.Count > 0)
                {
                    List<DispatchOut> listItems = new List<DispatchOut>();
                    foreach (DataRow dr in dtDispatch.Rows)
                    {

                        listItems.Add(new DispatchOut
                        {
                            DispatchID = dr["dsp_DispatchID"].ToString(),
                            Status = dr["sts_Name"].ToString(),
                            ExpectedDelDate = dr["dsp_ExpDeliveryDate"].ToString(),
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
                dm.TraceService(" SelectDespatchHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectDespatchHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectDespatchDetail([FromForm] DispatchDetailIn inputParams)
        {
            dm.TraceService("SelectDespatchDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string DispatchID = inputParams.ID == null ? "0" : inputParams.ID;

                DataTable dtDispatch = dm.loadList("SelDespatchDetail", "sp_CustomerConnect", DispatchID.ToString());

                if (dtDispatch.Rows.Count > 0)
                {
                    List<DispatchDetailOut> listItems = new List<DispatchDetailOut>();
                    foreach (DataRow dr in dtDispatch.Rows)
                    {

                        listItems.Add(new DispatchDetailOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            BaseUOM = dr["BaseUOM"].ToString(),
                            Qty = dr["Qty"].ToString(),


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
                dm.TraceService(" SelectDespatchDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectDespatchDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectDespatchCustomers([FromForm] DespatchCustomersIn inputParams)
        {
            dm.TraceService("SelectDespatchCustomers STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string Mode = inputParams.Mode == null ? "0" : inputParams.Mode;

                if (Mode == "DispatchCancelled")
                {
                    Mode = " and  A.Status in ('CN')";
                }
                else if (Mode == "DispatchReschedule")
                {
                    Mode = " and  A.Status in ('RS')";
                }
                else if (Mode == "DispatchCompleted")
                {
                    Mode = " and  A.Status in ('DD','LD','DR','ND')";
                }
                else
                {
                    Mode = "";
                }

                string[] arr = { FromDate.ToString(), ToDate.ToString(), Mode.ToString() };
                DataTable dtPicking = dm.loadList("SelDespatchCustomers", "sp_CustomerConnect", usrID.ToString(),arr);

                if (dtPicking.Rows.Count > 0)
                {
                    List<DespatchCustomersOut> listItems = new List<DespatchCustomersOut>();
                    foreach (DataRow dr in dtPicking.Rows)
                    {

                        listItems.Add(new DespatchCustomersOut
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
                dm.TraceService(" SelectDespatchCustomers Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectDespatchCustomers ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelCarryForwardDespatches([FromForm] CFDispatchIn inputParams)
        {
            dm.TraceService("SelCarryForwardDespatches STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PostItemDespatchCus> itemData = JsonConvert.DeserializeObject<List<PostItemDespatchCus>>(inputParams.JSONStringCus);
                List<PostItemDespatchCusOutlet> itemDataOutlet = JsonConvert.DeserializeObject<List<PostItemDespatchCusOutlet>>(inputParams.JSONStringOutlet);
                List<PostItemDespatchRot> itemDatas = JsonConvert.DeserializeObject<List<PostItemDespatchRot>>(inputParams.JSONStringRot);
                List<PostItemDespatchOrder> itemDatass = JsonConvert.DeserializeObject<List<PostItemDespatchOrder>>(inputParams.JSONStringOrder);
                List<PostItemDespatchProducts> itemDataZ = JsonConvert.DeserializeObject<List<PostItemDespatchProducts>>(inputParams.JSONStringProducts);

                string cusCondition = jsonsStr(inputParams.JSONStringCus, " and D.csh_ID in (D.csh_ID)", " and D.csh_ID in (", ")", "cus_HeaderID");
                string outletCondition = jsonsStr(inputParams.JSONStringOutlet, " and C.cus_ID in (C.cus_ID)", " and C.cus_ID in (", ")", "ID");
                string rotCondition = jsonsStr(inputParams.JSONStringRot, " ", " and dsp_rot_ID  in (", ")", "rot_ID");
                string orderCondition = jsonsStr(inputParams.JSONStringOrder, " and ord_ID in (ord_ID)", " and ord_ID in (", ")", "ordNo");
                string prdCondition = jsonsStr(inputParams.JSONStringProducts, " and D.csh_ID in (D.csh_ID)", " and D.csh_ID in (", ")", "prd_ID");

                string userID = inputParams.userID == null ? "0" : inputParams.userID;
               
                string mainCondition = "";
                mainCondition += cusCondition;
                mainCondition += outletCondition;
                mainCondition += rotCondition;
                mainCondition += orderCondition;

                string[] arr = { "", userID };
                DataTable dtDispatch = dm.loadList("SelCFNotDeliveredOrders", "sp_CustomerConnect", mainCondition, arr);

                if (dtDispatch.Rows.Count > 0)
                {
                    List<CFDispatchOut> listItems = new List<CFDispatchOut>();
                    foreach (DataRow dr in dtDispatch.Rows)
                    {

                        listItems.Add(new CFDispatchOut
                        {
                            dsp_ID = dr["dsp_ID"].ToString(),
                            dsp_DispatchID = dr["dsp_DispatchID"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            ord_ExpectedDelDate = dr["ord_ExpectedDelDate"].ToString(),
                            dsp_ExpDeliveryDate = dr["dsp_ExpDeliveryDate"].ToString(),                                                                               
                            csh_Code = dr["csh_Code"].ToString(),
                            csh_Name = dr["csh_Name"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            rot_Code = dr["rot_Code"].ToString(),
                            rot_Name = dr["rot_Name"].ToString(),
                            DispatchedBy = dr["DispatchedBy"].ToString(),
                            dsp_DispatchType = dr["dsp_DispatchType"].ToString(),
                            TotalAmount = dr["TotalAmount"].ToString(),
                            ApprovedBy = dr["ApprovedBy"].ToString(),
                            Status = dr["Status"].ToString(),
                            ord_LPONumber = dr["ord_LPONumber"].ToString(),

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
                dm.TraceService(" SelCarryForwardDespatches Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelCarryForwardDespatches ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

    }
}