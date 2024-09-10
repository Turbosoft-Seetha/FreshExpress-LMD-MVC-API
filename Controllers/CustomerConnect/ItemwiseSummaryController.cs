using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using MVC_API.Models.CustomerConnectHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers.CustomerConnect
{
    public class ItemwiseSummaryController : Controller
    {

        DataModel dm = new DataModel();
        string JSONString = string.Empty;

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
        public string SelItemWiseSummary([FromForm] ItemwiseSummaryIn inputParams)
        {
            dm.TraceService("SelItemWiseSummary STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {              
                string itemID = inputParams.itemID == null ? "0" : inputParams.itemID;
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string cusID = inputParams.cusID == null ? "0" : inputParams.cusID;

                string cusCondition = "";
                if (cusID == "0")
                {
                    cusCondition = "";
                }
                else
                {
                    cusCondition = " and D.csh_ID in (" + cusID + ") ";
                }

                string[] arr = { usrID.ToString() , cusCondition.ToString() };
                DataTable dtItemSumm = dm.loadList("SelItemWiseSummary", "sp_CustomerConnect", itemID.ToString(),arr);

                if (dtItemSumm.Rows.Count > 0)
                {
                    List<ItemwiseSummaryOut> listItems = new List<ItemwiseSummaryOut>();
                    foreach (DataRow dr in dtItemSumm.Rows)
                    {

                        listItems.Add(new ItemwiseSummaryOut
                        {
                            DispatchID = dr["dsp_DispatchID"].ToString(),
                            Status = dr["Status"].ToString(),
                            ExpectedDelDate = dr["ExpectedDelDate"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            cus_HeaderID = dr["csh_ID"].ToString(),
                            cus_HeaderCode = dr["csh_Code"].ToString(),
                            cus_HeaderName = dr["csh_Name"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            ord_LPONumber = dr["ord_LPONumber"].ToString(),
                            dsp_ID = dr["dsp_ID"].ToString(),

                            rot_ID = dr["rot_ID"].ToString(),
                            rot_Code = dr["rot_Code"].ToString(),
                            rot_Name = dr["rot_Name"].ToString(),
                            IsPartiallyDelivered = dr["dln_IsPartiallyDelivered"].ToString(),
                            SubTotal = dr["ord_SubTotal"].ToString(),
                            VAT = dr["ord_VAT"].ToString(),
                            GrandTotal = dr["ord_GrandTotal"].ToString(),
                            dln_DeliveryNumber = dr["dln_DeliveryNumber"].ToString(),
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
                dm.TraceService(" SelItemWiseSummary Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelItemWiseSummary ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelItems([FromForm] selItemIn inputParams)
        {
            dm.TraceService("SelItems STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
              
                DataTable dtItem = dm.loadList("SelItemsForItemSummary", "sp_CustomerConnect", usrID.ToString());

                if (dtItem.Rows.Count > 0)
                {
                    List<selItemOut> listItems = new List<selItemOut>();
                    foreach (DataRow dr in dtItem.Rows)
                    {

                        listItems.Add(new selItemOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code= dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),


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
                dm.TraceService(" SelItems Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelItems ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelCustomer([FromForm] selCustomerIn inputParams)
        {
            dm.TraceService("SelCustomer STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string ItemID = inputParams.ItemID == null ? "0" : inputParams.ItemID;
                string[] ar = { ItemID.ToString() };
                DataTable dtcus = dm.loadList("SelCusForItemSummary", "sp_CustomerConnect", usrID.ToString(),ar);

                if (dtcus.Rows.Count > 0)
                {
                    List<selCusOut> listItems = new List<selCusOut>();
                    foreach (DataRow dr in dtcus.Rows)
                    {

                        listItems.Add(new selCusOut
                        {
                            cus_ID = dr["csh_ID"].ToString(),
                            cus_Code = dr["csh_Code"].ToString(),
                            cus_Name = dr["csh_Name"].ToString()



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
                dm.TraceService(" SelCustomer Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelCustomer ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelItemWiseSummaryDetail([FromForm] ItemwiseSummaryDetailIn inputParams)
        {
            dm.TraceService("SelItemWiseSummaryDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string dsp_ID = inputParams.dsp_ID == null ? "0" : inputParams.dsp_ID;                
              
                DataTable dtItemSumm = dm.loadList("SelItemWiseSummaryDetail", "sp_CustomerConnect", dsp_ID.ToString());

                if (dtItemSumm.Rows.Count > 0)
                {
                    List<ItemwiseSummaryDetailOut> listItems = new List<ItemwiseSummaryDetailOut>();
                    foreach (DataRow dr in dtItemSumm.Rows)
                    {

                        listItems.Add(new ItemwiseSummaryDetailOut
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
                dm.TraceService(" SelItemWiseSummaryDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelItemWiseSummaryDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}