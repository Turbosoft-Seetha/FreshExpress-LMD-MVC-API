using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using MVC_API.Models.CustomerConnectHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
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
    public class DelController : Controller
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

        public DataTable DeliveryTotal(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectDeliveryTotal", "sp_CustomerConnect", MainCondition, arr);
            return dtOrder;
        }
        public DataTable DeliveryFD(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectDeliveryFD", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable DeliveryPD(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectDeliveryPD", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable DeliveryND(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectDeliveryND", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable DeliveryNP(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectDeliveryNP", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public string SelectDeliveryHeader([FromForm] DelievryIn inputParams)
        {
            dm.TraceService("SelectDeliveryHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PostItemDelCus> itemData = JsonConvert.DeserializeObject<List<PostItemDelCus>>(inputParams.JSONStringCus);
                List<PostItemDelCusOutlet> itemDataOutlet = JsonConvert.DeserializeObject<List<PostItemDelCusOutlet>>(inputParams.JSONStringOutlet);
                List<PostItemDelRot> itemDatas = JsonConvert.DeserializeObject<List<PostItemDelRot>>(inputParams.JSONStringRot);
                List<PostItemDelOrder> itemDatass = JsonConvert.DeserializeObject<List<PostItemDelOrder>>(inputParams.JSONStringOrder);
                List<PostItemDelProducts> itemDataZ = JsonConvert.DeserializeObject<List<PostItemDelProducts>>(inputParams.JSONStringProducts);
                //List<PostItemDelStatus> itemDataStatus = JsonConvert.DeserializeObject<List<PostItemDelStatus>>(inputParams.JSONStringStatus);

                string cusCondition = jsonsStr(inputParams.JSONStringCus, " and D.csh_ID in (D.csh_ID)", " and D.csh_ID in (", ")", "cus_HeaderID");
                string outletCondition = jsonsStr(inputParams.JSONStringOutlet, " and C.cus_ID in (C.cus_ID)", " and C.cus_ID in (", ")", "ID");
                string rotCondition = jsonsStr(inputParams.JSONStringRot, " ", " and dsp_rot_ID in (", ")", "rot_ID");
                string orderCondition = jsonsStr(inputParams.JSONStringOrder, " and ord_ID in (ord_ID)", " and ord_ID in (", ")", "ordNo");
                string prdCondition = jsonsStr(inputParams.JSONStringProducts, " and D.csh_ID in (D.csh_ID)", " and D.csh_ID in (", ")", "prd_ID");
                //  string StatusCondition = jsonsStr(inputParams.JSONStringStatus, " and pih_Status in (pih_Status)", " and pih_Status in (", ")", "Status");

                string userID = inputParams.userID == null ? "0" : inputParams.userID;
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string Mode = inputParams.Mode == null ? "0" : inputParams.Mode;

                string dateCondition = " (cast(E.CreatedDate as date) between cast('" + FromDate + "' as date) and cast('" + ToDate + "' as date))";

                string mainCondition = "";
                mainCondition += dateCondition;
                mainCondition += cusCondition;
                mainCondition += outletCondition;
                mainCondition += rotCondition;
                mainCondition += orderCondition;
                //   mainCondition += StatusCondition;

                DataTable dtDelievry = new DataTable();

                if (Mode == "FD")
                {
                    dtDelievry = DeliveryFD(userID, mainCondition);
                }
                else if (Mode == "PD")
                {
                    dtDelievry = DeliveryPD(userID, mainCondition);
                }
                else if (Mode == "ND")
                {
                    dtDelievry = DeliveryND(userID, mainCondition);
                }
                else if (Mode == "NP")
                {
                    
                    dtDelievry = DeliveryNP(userID, mainCondition);
                }
                else if (Mode == "TotalDelivery")
                {
                    dtDelievry = DeliveryTotal(userID, mainCondition);
                }

                if (dtDelievry.Rows.Count > 0)
                {
                    List<DelievryOut> listItems = new List<DelievryOut>();
                    foreach (DataRow dr in dtDelievry.Rows)
                    {

                        listItems.Add(new DelievryOut
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
                            IsPartiallyDelivered = dr["dln_IsPartiallyDelivered"].ToString(),
                            SubTotal = dr["ord_SubTotal"].ToString(),
                            VAT = dr["ord_VAT"].ToString(),
                            GrandTotal = dr["ord_GrandTotal"].ToString(),
                            dln_DeliveryNumber = dr["dln_DeliveryNumber"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            ord_LPONumber = dr["ord_LPONumber"].ToString(),
                            ID = dr["ID"].ToString(),
                            ContactNumber = dr["ContactNumber"].ToString(),
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
                dm.TraceService(" SelectDeliveryHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectDeliveryHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectDeliveryDetail([FromForm] DelievryDetailIn inputParams)
        {
            dm.TraceService("SelectDeliveryDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string DispatchID = inputParams.ID == null ? "0" : inputParams.ID;

                DataTable dtDelievry = dm.loadList("SelND_FullDeliveryDetail", "sp_CustomerConnect", DispatchID.ToString());

                if (dtDelievry.Rows.Count > 0)
                {
                    List<DelievryDetailOut> listItems = new List<DelievryDetailOut>();
                    foreach (DataRow dr in dtDelievry.Rows)
                    {

                        listItems.Add(new DelievryDetailOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            BaseUOM = dr["BaseUOM"].ToString(),
                            Qty = dr["Qty"].ToString(),
                            Status = dr["Status"].ToString(),
                            LineTotal = dr["odd_HigherPrice"].ToString(),
                            DelUOM = dr["DelUOM"].ToString(),
                            DelQty = dr["DelQty"].ToString(),

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
                dm.TraceService(" SelectDeliveryDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectDeliveryDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectNPDeliveryDetail([FromForm] DelievryDetailIn inputParams)
        {
            dm.TraceService("SelectNPDeliveryDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string DispatchID = inputParams.ID == null ? "0" : inputParams.ID;

                DataTable dtDelievry = dm.loadList("SelNPDeliveryDetail", "sp_CustomerConnect", DispatchID.ToString());

                if (dtDelievry.Rows.Count > 0)
                {
                    List<DelDetailOut> listItems = new List<DelDetailOut>();
                    foreach (DataRow dr in dtDelievry.Rows)
                    {

                        listItems.Add(new DelDetailOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            BaseUOM = dr["BaseUOM"].ToString(),
                            Qty = dr["Qty"].ToString(),
                            AdjUOM = dr["AdjUOM"].ToString(),
                            AdjQty = dr["AdjQty"].ToString(),
                            FinalUOM = dr["FinalUOM"].ToString(),
                            FinalQty = dr["FinalQty"].ToString(),
                            Status = dr["Status"].ToString(),
                            LineTotal = dr["odd_HigherPrice"].ToString(),
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
                dm.TraceService("SelectNPDeliveryDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectNPDeliveryDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectPartialDeliveryDetail([FromForm] DelievryDetailIn inputParams)
        {
            dm.TraceService("SelectPartialDeliveryDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string DispatchID = inputParams.ID == null ? "0" : inputParams.ID;

                DataTable dtDelievry = dm.loadList("SelPartialDeliveryDetail", "sp_CustomerConnect", DispatchID.ToString());

                if (dtDelievry.Rows.Count > 0)
                {
                    List<DelDetailOut> listItems = new List<DelDetailOut>();
                    foreach (DataRow dr in dtDelievry.Rows)
                    {

                        listItems.Add(new DelDetailOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            BaseUOM = dr["BaseUOM"].ToString(),
                            Qty = dr["Qty"].ToString(),
                            AdjUOM = dr["AdjUOM"].ToString(),
                            AdjQty = dr["AdjQty"].ToString(),
                            FinalUOM = dr["FinalUOM"].ToString(),
                            FinalQty = dr["FinalQty"].ToString(),
                            Status = dr["Status"].ToString(),
                            LineTotal = dr["odd_HigherPrice"].ToString(),
                            Reason = dr["Reason"].ToString(),
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
                dm.TraceService("SelectPartialDeliveryDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectPartialDeliveryDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectDeliveryCustomers([FromForm] DeliveryCustomersIn inputParams)
        {
            dm.TraceService("SelectDeliveryCustomers STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {

                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string Mode = inputParams.Mode == null ? "0" : inputParams.Mode;

                if (Mode == "FD")
                {
                    Mode = " and  A.Status in ('DR') and isnull(dln_IsPartiallyDelivered ,'N') = 'N' ";
                }
                else if (Mode == "PD")
                {
                    Mode = " and  A.Status in ('DR') and isnull(dln_IsPartiallyDelivered ,'N') = 'Y' ";
                }
                else if (Mode == "ND")
                {
                    Mode = " and  A.Status in ('ND') ";
                }
                else if (Mode == "NP")
                {
                    Mode = " and ISNULL(E.Status, 'NUL') in ('NUL') ";
                }
                else if (Mode == "TotalDelivery")
                {
                    Mode = "";
                }

                string[] arr = { FromDate.ToString(), ToDate.ToString(), Mode.ToString() };

                DataTable dtPicking = dm.loadList("SelDeliveryCustomers", "sp_CustomerConnect", usrID.ToString(),arr);

                if (dtPicking.Rows.Count > 0)
                {
                    List<DeliveryCustomersOut> listItems = new List<DeliveryCustomersOut>();
                    foreach (DataRow dr in dtPicking.Rows)
                    {

                        listItems.Add(new DeliveryCustomersOut
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
                dm.TraceService(" SelectDeliveryCustomers Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectDeliveryCustomers ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectStampedCopy([FromForm] DelStampedCopyIn inputParams)
        {
            dm.TraceService("SelectStampedCopy STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {

                string dln_ID = inputParams.dln_ID == null ? "0" : inputParams.dln_ID;
               
                DataTable dt = dm.loadList("SelStampedCopy", "sp_CustomerConnect", dln_ID.ToString());

                string url = ConfigurationManager.AppSettings.Get("Stamped_URL");

                if (dt.Rows.Count > 0)
                {
                    List<DelStampedCopyOut> listItems = new List<DelStampedCopyOut>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        listItems.Add(new DelStampedCopyOut
                        {
                            StampedCopy = url + dr["Stamped"].ToString(),
                           
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
                dm.TraceService(" SelectStampedCopy Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectStampedCopy ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}