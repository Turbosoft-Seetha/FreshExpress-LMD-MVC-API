using Microsoft.AspNetCore.Mvc;
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

namespace MVC_API.Controllers
{
    public class AccountsReceivableController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;

        [HttpPost]
        public string PostAR([FromForm] ARHelper inputParams)
        {
            dm.TraceService("PostAR STARTED ");
            dm.TraceService("===============");
            try
            {
                List<PostARHeader> arHeader = JsonConvert.DeserializeObject<List<PostARHeader>>(inputParams.ARHeaders);
                List<PostARDetail> arDetail = JsonConvert.DeserializeObject<List<PostARDetail>>(inputParams.ARDetails);
                dm.TraceService("ARHeaders in paras : -  " + inputParams.ARHeaders);
                dm.TraceService("ARDetails in paras : -" + inputParams.ARDetails);
                try
                {
                    DataSet dsAR = new DataSet();

                    DataTable dtARHeader = new DataTable();
                    dtARHeader.Columns.Add("udpID", typeof(string));
                    dtARHeader.Columns.Add("cusID", typeof(string));
                    dtARHeader.Columns.Add("cseID", typeof(string));
                    dtARHeader.Columns.Add("CollectedAmount", typeof(string));
                    dtARHeader.Columns.Add("BalanceAmount", typeof(string));
                    dtARHeader.Columns.Add("Remarks", typeof(string));
                    dtARHeader.Columns.Add("AppOrderID", typeof(string));
                    dtARHeader.Columns.Add("Signature", typeof(string));
                    dtARHeader.Columns.Add("PayMode", typeof(string));
                    dtARHeader.Columns.Add("ReceiptImage", typeof(string));
                    dtARHeader.Columns.Add("GeoCode", typeof(string));
                    dtARHeader.Columns.Add("GeoCodeName", typeof(string));
                    dtARHeader.Columns.Add("SyncedDateTime", typeof(string));
                    dtARHeader.Columns.Add("CreationMode", typeof(string));
                    dtARHeader.Columns.Add("CreatedBy", typeof(string));
                    
                    DataTable dtARDetail = new DataTable();
                    dtARDetail.Columns.Add("invID", typeof(string));
                    dtARDetail.Columns.Add("Amount", typeof(string));
                    dtARDetail.Columns.Add("PrevBalance", typeof(string));
                    dtARDetail.Columns.Add("CurrentBalance", typeof(string));
                    
                    foreach (var header in arHeader)
                    {
                        dtARHeader.Rows.Add(header.udpID, header.cusID, header.cseID, header.CollectedAmount, header.BalanceAmount, header.Remarks,
                            header.AppOrderID, header.Signature, header.PayMode, header.ReceiptImage, header.GeoCode, header.GeoCodeName, header.SyncedDateTime,
                            header.CreationMode, header.CreatedBy);
                    }

                    foreach (var item in arDetail)
                    {
                        dtARDetail.Rows.Add(item.invID, item.Amount, item.PrevBalance, item.CurrentBalance);
                    }

                    dsAR.Tables.Add(dtARHeader);
                    dsAR.Tables.Add(dtARDetail);
                    
                    try
                    {
                        string[] keys = { };
                        string[] values = { };
                        string[] arr = { "@ARHeader", "@ARDetail" };
                        DataSet Value = dm.bulkUpdate(dsAR, arr, keys, values, "sp_Generate_AR");
                        foreach (DataTable table in Value.Tables)
                        {
                            string Mode = table.Rows[0]["res"].ToString();
                            string Status = table.Rows[0]["status"].ToString();
                            dm.TraceService("Response from Sql Procedure : Mode=" + Mode + " and Status=" + Status);
                            List<GetARInsertStatus> listStatus = new List<GetARInsertStatus>();
                            listStatus.Add(new GetARInsertStatus
                            {
                                Res = Mode,
                                Title = Status,
                            });
                            string JSONString = JsonConvert.SerializeObject(new
                            {
                                result = listStatus
                            });
                            return JSONString;
                        }
                    }
                    catch (Exception ex)
                    {
                        dm.TraceService(ex.Message.ToString());
                        JSONString = "NoDataSQL - " + ex.Message.ToString();
                    }
                }
                catch (Exception ex)
                {
                    dm.TraceService(ex.Message.ToString());
                    JSONString = "NoDataSQL - " + ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                dm.TraceService(ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }

            dm.TraceService("PostAR ENDED ");
            dm.TraceService("=============");

            return JSONString;
        }

        public string PostOutStandingInvoices([FromForm] PostOutStandingdInvData inputParams)
        {
            dm.TraceService("PostOutStandingInvoices STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string Inv_ID = inputParams.Inv_ID == null ? "0" : inputParams.Inv_ID;

                DataTable dtitem = dm.loadList("SelOutStandingInvoices", "sp_OutStandingInvoices", Inv_ID.ToString());

                if (dtitem.Rows.Count > 0)
                {
                    List<PostOutStandingdInvDataOut> listItems = new List<PostOutStandingdInvDataOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new PostOutStandingdInvDataOut
                        {

                            Inv_ID = dr["oid_inv_ID"].ToString(),
                            InvoiceID = dr["InvoiceID"].ToString(),                   
                            cus_ID = dr["oid_cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                          
                            Date = dr["Date"].ToString(),
                            Time = dr["Time"].ToString(),

                            InvoiceAmount = dr["InvoiceAmount"].ToString(),
                            AmountPaid = dr["AmountPaid"].ToString(),
                            InvoiceBalance = dr["InvoiceBalance"].ToString(),
                            PDC_Amount = dr["PDC_Amount"].ToString(),
                           

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
                dm.TraceService(" SelectLoadInCompleted Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectLoadInCompleted ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}