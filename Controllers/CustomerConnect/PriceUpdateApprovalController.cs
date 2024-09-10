using Microsoft.AspNetCore.Mvc;
using MVC_API.Models.CustomerConnectHelpers;
using MVC_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using System.IO;
using System.Xml;

namespace MVC_API.Controllers.CustomerConnect
{
    public class PriceUpdateApprovalController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;

        [HttpPost]
        public string SelPriceUpdateHeader([FromForm] PriceUpdateIn inputParams)
        {
            dm.TraceService(" SelPriceUpdateHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {

                List<PostItemsCus> itemData = JsonConvert.DeserializeObject<List<PostItemsCus>>(inputParams.JSONStringCus);
                List<PostItemCusOutlet> itemDataOutlet = JsonConvert.DeserializeObject<List<PostItemCusOutlet>>(inputParams.JSONStringOutlet);
                List<PostItemRot> itemDatas = JsonConvert.DeserializeObject<List<PostItemRot>>(inputParams.JSONStringRot);
                List<PostItemOrder> itemDatass = JsonConvert.DeserializeObject<List<PostItemOrder>>(inputParams.JSONStringOrder);
                List<PostItemProducts> itemDataZ = JsonConvert.DeserializeObject<List<PostItemProducts>>(inputParams.JSONStringProducts);

                string smp_ID = inputParams.smp_ID == null ? "0" : inputParams.smp_ID;


                string InputXmlCus = "";
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostItemsCus id in itemData)
                        {
                            string[] arr = { id.cus_HeaderID.ToString() };
                            string[] arrName = { "cus_HeaderID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXmlCus = sw.ToString();
                }

                string InputXmlCusOutlet = "";
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostItemCusOutlet id in itemDataOutlet)
                        {
                            string[] arr = { id.ID.ToString() };
                            string[] arrName = { "ID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXmlCusOutlet = sw.ToString();
                }


                string InputXmlRot = "";
                using (var sw1 = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw1))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostItemRot id in itemDatas)
                        {
                            string[] arr = { id.rot_ID.ToString() };
                            string[] arrName = { "rot_ID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXmlRot = sw1.ToString();
                }

                string InputXmlOrder = "";
                using (var sw2 = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw2))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostItemOrder id in itemDatass)
                        {
                            string[] arr = { id.ordNo.ToString() };
                            string[] arrName = { "ordNo" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXmlOrder = sw2.ToString();
                }

                string InputXmlProducts = "";
                using (var sw2 = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw2))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostItemProducts id in itemDataZ)
                        {
                            string[] arr = { id.prd_ID.ToString() };
                            string[] arrName = { "prd_ID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXmlProducts = sw2.ToString();
                }

                string[] ar = { InputXmlCus.ToString(), InputXmlCusOutlet.ToString(), InputXmlRot.ToString(), InputXmlOrder.ToString(), InputXmlProducts.ToString() };

                DataTable dtPriceUpdate = dm.loadList("SelPriceUpdateHeader", "sp_CustomerConnectFilter", smp_ID.ToString(),ar);

                if (dtPriceUpdate.Rows.Count > 0)
                {
                    List<PriceUpdateOut> listItems = new List<PriceUpdateOut>();
                    foreach (DataRow dr in dtPriceUpdate.Rows)
                    {

                        listItems.Add(new PriceUpdateOut
                        {
                            DispatchID = dr["dsp_DispatchID"].ToString(),
                            DeliveryNumber = dr["dln_DeliveryNumber"].ToString(),
                            OrderID = dr["OrderID"].ToString(),
                            DispatchedOn = dr["DispatchedOn"].ToString(),
                            ExpectedDelDate = dr["ExpectedDelDate"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            cus_HeaderID = dr["csh_ID"].ToString(),
                            cus_HeaderCode = dr["csh_Code"].ToString(),
                            cus_HeaderName = dr["csh_Name"].ToString(),
                            PriceUpdateID = dr["pqh_ID"].ToString(),
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
                dm.TraceService("  SelPriceUpdateHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService(" SelPriceUpdateHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelPriceUpdateDetail([FromForm] PriceUpdateDetailIn inputParams)
        {
            dm.TraceService(" SelPriceUpdateHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string PriceUpdateID = inputParams.PriceUpdateID == null ? "0" : inputParams.PriceUpdateID;
                string DispatchID = inputParams.DispatchID == null ? "0" : inputParams.DispatchID;

                string[] arr = { DispatchID.ToString() };
                DataTable dtPriceUpdate = dm.loadList("SelPriceUpdateDetail", "sp_CustomerConnect", PriceUpdateID.ToString() , arr);

                if (dtPriceUpdate.Rows.Count > 0)
                {
                    List<PriceUpdateDetailOut> listItems = new List<PriceUpdateDetailOut>();
                    foreach (DataRow dr in dtPriceUpdate.Rows)
                    {

                        listItems.Add(new PriceUpdateDetailOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            SellingPrice = dr["pqd_SellingPrice"].ToString(),
                            RequestedPrice = dr["pqd_RequestedPrice"].ToString(),
                            FinalPrice = dr["pqd_FinalPrice"].ToString(),
                            StdPrice = dr["odd_HigherPrice"].ToString(),
                            Mode = dr["Mode"].ToString(),
                            rsn_ID = dr["rsn_ID"].ToString(),
                            rsn_Name = dr["rsn_Name"].ToString(),
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
                dm.TraceService("  SelPriceUpdateHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService(" SelPriceUpdateHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string InsPriceUpdateApproval([FromForm] PriceUpdateApproveIn inputParams)
        {
            dm.TraceService(" InsPriceUpdateApproval STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PostItemData> itemData = JsonConvert.DeserializeObject<List<PostItemData>>(inputParams.JSONString);

                string userID = inputParams.userID == null ? "0" : inputParams.userID;

                string InputXml = "";
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostItemData id in itemData)
                        {
                            string[] arr = { id.PriceUpdateID.ToString(), id.prd_ID.ToString(), id.FinalPrice.ToString(), id.rsnID.ToString() };
                            string[] arrName = { "PriceUpdateID", "prd_ID" , "FinalPrice" , "rsnID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw.ToString();
                }

                string[] ar = {  InputXml.ToString()};
                DataTable dtPriceUpdate = dm.loadList("InsPriceUpdateApproval", "sp_CustomerConnect", userID.ToString() , ar);

                if (dtPriceUpdate.Rows.Count > 0)
                {
                    List<PriceUpdateApproveOut> listItems = new List<PriceUpdateApproveOut>();
                    foreach (DataRow dr in dtPriceUpdate.Rows)
                    {

                        listItems.Add(new PriceUpdateApproveOut
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
                dm.TraceService("  InsPriceUpdateApproval Exception - " + ex.Message.ToString());
            }
            dm.TraceService(" InsPriceUpdateApproval ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectPriceUpdateCustomers([FromForm] PriceUpdateCustomersIn inputParams)
        {
            dm.TraceService("SelectPriceUpdateCustomers STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;

                DataTable dtPicking = dm.loadList("SelPriceUpdateCustomers", "sp_CustomerConnect", usrID.ToString());

                if (dtPicking.Rows.Count > 0)
                {
                    List<PriceUpdateCustomersOut> listItems = new List<PriceUpdateCustomersOut>();
                    foreach (DataRow dr in dtPicking.Rows)
                    {

                        listItems.Add(new PriceUpdateCustomersOut
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
                dm.TraceService(" SelectPriceUpdateCustomers Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectPriceUpdateCustomers ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}