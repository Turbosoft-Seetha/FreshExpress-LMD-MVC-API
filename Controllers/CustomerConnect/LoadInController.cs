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
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers.CustomerConnect
{
    public class LoadInController : Controller
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

        public DataTable LoadInCompleted(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectLoadInCompleted", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable LoadInTotal(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectLoadInTotal", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable LoadInPending(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectLoadInPending", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public DataTable LoadInCancelled(string usrID, string MainCondition)
        {
            string Uid = usrID.ToString();
            string[] arr = { "", Uid };
            DataTable dtOrder = new DataTable();
            dtOrder = dm.loadList("SelectLoadInCancelled", "sp_OrderDashboards", MainCondition, arr);
            return dtOrder;
        }
        public string SelectLoadInHeader([FromForm] LoadInIn inputParams)
        {
            dm.TraceService("SelectLoadInHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PostItemLoadInCus> itemData = JsonConvert.DeserializeObject<List<PostItemLoadInCus>>(inputParams.JSONStringCus);
                List<PostItemLoadInCusOutlet> itemDataOutlet = JsonConvert.DeserializeObject<List<PostItemLoadInCusOutlet>>(inputParams.JSONStringOutlet);
                List<PostItemLoadInRot> itemDatas = JsonConvert.DeserializeObject<List<PostItemLoadInRot>>(inputParams.JSONStringRot);
                List<PostItemLoadInOrder> itemDatass = JsonConvert.DeserializeObject<List<PostItemLoadInOrder>>(inputParams.JSONStringOrder);
                List<PostItemLoadInProducts> itemDataZ = JsonConvert.DeserializeObject<List<PostItemLoadInProducts>>(inputParams.JSONStringProducts);
                // List<PostItemLoadInStatus> itemDataStatus = JsonConvert.DeserializeObject<List<PostItemLoadInStatus>>(inputParams.JSONStringStatus);

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

                string dateCondition = " (cast(A.CreatedDate as date) between cast('" + FromDate + "' as date) and cast('" + ToDate + "' as date))";

                string mainCondition = "";
                mainCondition += dateCondition;
                mainCondition += cusCondition;
                mainCondition += outletCondition;
                mainCondition += rotCondition;
                mainCondition += orderCondition;
                //   mainCondition += StatusCondition;

                DataTable dtLoadIn = new DataTable();

                if (Mode == "LoadInCompleted")
                {
                    dtLoadIn = LoadInCompleted(userID, mainCondition);
                }
                else if (Mode == "LoadInTotal")
                {
                    dtLoadIn = LoadInTotal(userID, mainCondition);
                }
                else if (Mode == "LoadInPending")
                {
                    dtLoadIn = LoadInPending(userID, mainCondition);
                }
                else if (Mode == "LoadInCancelled")
                {
                    dtLoadIn = LoadInCancelled(userID, mainCondition);
                }



                if (dtLoadIn.Rows.Count > 0)
                {
                    List<LoadInOut> listItems = new List<LoadInOut>();
                    foreach (DataRow dr in dtLoadIn.Rows)
                    {

                        listItems.Add(new LoadInOut
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
                dm.TraceService(" SelectLoadInHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectLoadInHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectLoadInDetail([FromForm] LoadInDetailIn inputParams)
        {
            dm.TraceService("SelectLoadInDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string DispatchID = inputParams.ID == null ? "0" : inputParams.ID;

                DataTable dtLoadIn = dm.loadList("SelLoadInDetail", "sp_CustomerConnect", DispatchID.ToString());

                if (dtLoadIn.Rows.Count > 0)
                {
                    List<LoadInDetailOut> listItems = new List<LoadInDetailOut>();
                    foreach (DataRow dr in dtLoadIn.Rows)
                    {

                        listItems.Add(new LoadInDetailOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            BaseUOM = dr["BaseUOM"].ToString(),
                            Qty = dr["Qty"].ToString(),
                            LoadInQty = dr["LoadInQty"].ToString(),
                            LoadInUOM = dr["LoadInUOM"].ToString(),
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
                dm.TraceService(" SelectLoadInDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectLoadInDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectLoadInCustomers([FromForm] LoadInCustomersIn inputParams)
        {
            dm.TraceService("SelectLoadInCustomers STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string Mode = inputParams.Mode == null ? "0" : inputParams.Mode;

                if (Mode == "LoadInCompleted")
                {
                    Mode = " and  A.Status in ('LD','DR','ND')";
                }
                else if (Mode == "LoadInPending")
                {
                    Mode = " and  A.Status in ('DD')";
                }
                else if (Mode == "LoadInCancelled")
                {
                    Mode = " and  A.Status in ('R')";
                }
                else
                {
                    Mode = " and  A.Status in ('LD','DR','ND','DD','R')";
                }

                string[] arr = { FromDate.ToString(), ToDate.ToString(), Mode.ToString() };

                DataTable dtPicking = dm.loadList("SelLoadInCustomers", "sp_CustomerConnect", usrID.ToString(), arr);

                if (dtPicking.Rows.Count > 0)
                {
                    List<LoadInCustomersOut> listItems = new List<LoadInCustomersOut>();
                    foreach (DataRow dr in dtPicking.Rows)
                    {

                        listItems.Add(new LoadInCustomersOut
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
                dm.TraceService(" SelectLoadInCustomers Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectLoadInCustomers ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}