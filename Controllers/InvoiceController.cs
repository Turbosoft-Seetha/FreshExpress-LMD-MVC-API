using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;


namespace MVC_API.Controllers
{
    public class InvoiceController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;

        [HttpPost]
        public string PostODI([FromForm] PostODIHeader inputParams)
        {
            dm.TraceService("PostODI STARTED ");
            dm.TraceService("===================");
            try
            {
                List<PostODIDetail> jsonValue = JsonConvert.DeserializeObject<List<PostODIDetail>>(inputParams.jsonValue);
                List<PostODIBatchSerial> XMLBatchData = JsonConvert.DeserializeObject<List<PostODIBatchSerial>>(inputParams.XMLBatchData);

                dm.TraceService("PostODI InvoiceDetail -  " + inputParams.jsonValue);
                dm.TraceService("PostODI InvoiceBatchDetail -  " + inputParams.XMLBatchData);
               

                try
                {
                    string dsp_ID = inputParams.dsp_ID == null ? "0" : inputParams.dsp_ID;
                    string CreatedBy = inputParams.CreatedBy == null ? "0" : inputParams.CreatedBy;
                    string CreatedDate = inputParams.CreatedDate == null ? "0" : inputParams.CreatedDate;
                    string GeoCode = inputParams.GeoCode == null ? "0" : inputParams.GeoCode;
                    string GeoCodeName = inputParams.GeoCodeName == null ? "0" : inputParams.GeoCodeName;
                    string remarks = inputParams.remarks == null ? "0" : inputParams.remarks;
                    string dln_cse_ID = inputParams.dln_cse_ID == null ? "0" : inputParams.dln_cse_ID;
                    string CreationMode = inputParams.CreationMode == null ? "0" : inputParams.CreationMode;
                    string dln_udp_ID = inputParams.dln_udp_ID == null ? "0" : inputParams.dln_udp_ID;
                    string dln_Signature = inputParams.dln_Signature == null ? "0" : inputParams.dln_Signature;
                    string inv_SubTotal = inputParams.inv_SubTotal == null ? "0" : inputParams.inv_SubTotal;
                    string inv_VAT = inputParams.inv_VAT == null ? "0" : inputParams.inv_VAT;
                    string inv_TotalAmount = inputParams.inv_TotalAmount == null ? "0" : inputParams.inv_TotalAmount;
                    string inv_ReceivedAmount = inputParams.inv_ReceivedAmount == null ? "0" : inputParams.inv_ReceivedAmount;
                    string inv_TotalPaidAmount = inputParams.inv_TotalPaidAmount == null ? "0" : inputParams.inv_TotalPaidAmount;
                    string inv_PayType = inputParams.inv_PayType == null ? "0" : inputParams.inv_PayType;
                    string dln_IsPartiallyDelivered = inputParams.dln_IsPartiallyDelivered == null ? "0" : inputParams.dln_IsPartiallyDelivered;
                    string FreeGoodAmount = inputParams.FreeGoodAmount == null ? "0" : inputParams.FreeGoodAmount;
                    string HeaderDiscount = inputParams.HeaderDiscount == null ? "0" : inputParams.HeaderDiscount;
                    string inv_SubTotal_WO_Discount = inputParams.inv_SubTotal_WO_Discount == null ? "0" : inputParams.inv_SubTotal_WO_Discount;
                    string AppDeliveryID = inputParams.AppDeliveryID == null ? "0" : inputParams.AppDeliveryID;
                    string rot_ID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
                    string paymode = inputParams.paymode == null ? "0" : inputParams.paymode;
                    string Receiptimg = inputParams.Receiptimg == null ? "0" : inputParams.Receiptimg;
                    string inv_InvoiceID = inputParams.inv_InvoiceID == null ? "0" : inputParams.inv_InvoiceID;
                    string dln_ID = inputParams.dln_ID == null ? "0" : inputParams.dln_ID;
                    string Type = inputParams.Type == null ? "0" : inputParams.Type;
                    string CustomerID = inputParams.CustomerID == null ? "0" : inputParams.CustomerID;
                    string CustomerHeaderID = inputParams.CustomeHeaderID == null ? "0" : inputParams.CustomeHeaderID;
                    string PriceUpdateRequested = inputParams.PriceUpdateRequested == null ? "0" : inputParams.PriceUpdateRequested;
                    string IsCashCollected = inputParams.IsCashCollected == null ? "0" : inputParams.IsCashCollected;
                    string HelperRoute = inputParams.HelperRoute == null ? "0" : inputParams.HelperRoute;

                    DataSet dsInvoiceDetail = new DataSet();

                    DataTable dtInvoiceDetail = new DataTable();
                    dtInvoiceDetail.Columns.Add("dlnID", typeof(string));
                    dtInvoiceDetail.Columns.Add("itmID", typeof(string));
                    dtInvoiceDetail.Columns.Add("LIHUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("LILUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("LIHQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("LILQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("FHQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("FHUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("FLQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("FLUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjHQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjHUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjLQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjLUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("ind_Price", typeof(string));
                    dtInvoiceDetail.Columns.Add("dld_TransType", typeof(string));
                    dtInvoiceDetail.Columns.Add("dld_ReturnHigherPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("dld_ReturnLowerPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("PriceUpdateRequested", typeof(string));
                    dtInvoiceDetail.Columns.Add("VAT", typeof(string));
                    dtInvoiceDetail.Columns.Add("InvoicePrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("PieceDiscount", typeof(string));
                    dtInvoiceDetail.Columns.Add("GrandTotal", typeof(string));
                    dtInvoiceDetail.Columns.Add("Discount", typeof(string));
                    dtInvoiceDetail.Columns.Add("SubTotalWODiscount", typeof(string));
                    dtInvoiceDetail.Columns.Add("InvoiceLineNo", typeof(string));
                    dtInvoiceDetail.Columns.Add("DiscountPerc", typeof(string));
                    dtInvoiceDetail.Columns.Add("HigherPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("LowerPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("LossInTransit", typeof(string));
                    dtInvoiceDetail.Columns.Add("ReasonCode", typeof(string));
                    dtInvoiceDetail.Columns.Add("VATPerc", typeof(string));
                    
                    DataTable dtInvoiceBatchSetial = new DataTable();
                    dtInvoiceBatchSetial.Columns.Add("DeliveryHeaderID", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("DeliveryDetailID", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("Number", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("ExpiryDate", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("BaseUOM", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("OrderedQty", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("AdjQty", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("LoadInQty", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("DeliveryStatus", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("itmID", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("LossInTransit", typeof(string));

                    foreach (var item in jsonValue)
                    {
                        dtInvoiceDetail.Rows.Add(item.dlnID, item.itmID, item.LIHUom, item.LILUom, item.LIHQty, item.LILQty, item.FHQty, item.FHUom, item.FLQty, item.FLUom, item.AdjHQty, 
                            item.AdjHUom, item.AdjLQty, item.AdjLUom, item.ind_Price, item.dld_TransType, item.dld_ReturnHigherPrice, item.dld_ReturnLowerPrice, item.PriceUpdateRequested, 
                            item.VAT, item.InvoicePrice, item.PieceDiscount, item.GrandTotal, item.Discount, item.SubTotalWODiscount, item.InvoiceLineNo, item.DiscountPerc, item.HigherPrice, 
                            item.LowerPrice, item.LossInTransit, item.ReasonCode, item.VATPerc);
                    }

                    foreach (var batch in XMLBatchData)
                    {
                        dtInvoiceBatchSetial.Rows.Add(batch.dlnID, batch.dldID, batch.dnsNumber, batch.ExpDate, batch.baseUOM, batch.OrdQty, batch.AdjQty, batch.LIQty, batch.Status, batch.itmID, batch.LossInTransit);
                    }

                    dsInvoiceDetail.Tables.Add(dtInvoiceDetail);
                    dsInvoiceDetail.Tables.Add(dtInvoiceBatchSetial);


                    try
                    {
                        string[] keys = { "@DispatchID", "@CreatedBy", "@CreatedDate", "@GeoCode", "@GeoCodeName", "@Remarks", "@CusStartExit", "@CreationMode",
                            "@UserDailProcess", "@Signature", "@SubTotal", "@VAT", "@TotalAmount", "@ReceivedAmount", "@TotalPaidAmount", "@PayType", "@IsPartiallyDelivered",
                            "@FreeGoodAmount", "@HeaderDiscount", "@SubTotalWithOutDiscount", "@AppDeliveryID", "@RouteID", "@PayMode", "@ReceiptImage", "@InvoiceID", "@DeliveryNoteID", "@Type", "@CustomerID", "@CustomerHeaderID",
                            "@PriceUpdateRequested", "@IsCashCollected", "@HelperRoute"};
                        string[] values = { dsp_ID.ToString(), CreatedBy.ToString(), CreatedDate.ToString(), GeoCode.ToString(), GeoCodeName.ToString(), remarks.ToString(), dln_cse_ID.ToString(), CreationMode.ToString(),
                        dln_udp_ID.ToString(), dln_Signature.ToString(), inv_SubTotal.ToString(), inv_VAT.ToString(), inv_TotalAmount.ToString(), inv_ReceivedAmount.ToString(), inv_TotalPaidAmount.ToString(), inv_PayType.ToString(), dln_IsPartiallyDelivered.ToString(),
                        FreeGoodAmount.ToString(), HeaderDiscount.ToString(), inv_SubTotal_WO_Discount.ToString(), AppDeliveryID.ToString(), rot_ID.ToString(), paymode.ToString(), Receiptimg.ToString(), inv_InvoiceID.ToString(), dln_ID.ToString(), Type.ToString(), CustomerID.ToString(), CustomerHeaderID.ToString(),
                        PriceUpdateRequested.ToString(), IsCashCollected.ToString(), HelperRoute.ToString()};
                        string[] arr = { "@InvoiceDetail", "@InvoiceBatchDetail" };
                        DataSet Value = dm.bulkUpdate(dsInvoiceDetail, arr, keys, values, "sp_GenerateInvoice_ODI");
                        DataTable invoiceHeader = Value.Tables[0];
                        DataTable appliDeliNotes = Value.Tables[1];
                        DataTable invoiceDetail = Value.Tables[2];
                        if (invoiceHeader.Rows.Count > 0)
                        {
                            //Package Items Starts
                            try
                            {
                                List<PostODIPackage> XMLPackageData = JsonConvert.DeserializeObject<List<PostODIPackage>>(inputParams.XMLPackageData);
                                dm.TraceService("PostODI InvoicePackageDetail -  " + inputParams.XMLPackageData);

                                if (inputParams.XMLPackageData != null)
                                {
                                    if (XMLPackageData.Count > 0)
                                    {
                                        dm.TraceService("Insertion to Package Sales - ODI Starts.");

                                        string DetailXml = "";

                                        using (var sw = new StringWriter())
                                        {
                                            using (var writer = XmlWriter.Create(sw))
                                            {

                                                writer.WriteStartDocument(true);
                                                writer.WriteStartElement("r");
                                                int c = 0;
                                                foreach (PostODIPackage id in XMLPackageData)
                                                {
                                                    string[] array = { id.itmID.ToString(), id.Qty.ToString() };
                                                    string[] arrName = { "itmID", "Qty" };
                                                    dm.createNode(array, arrName, writer);
                                                }

                                                writer.WriteEndElement();
                                                writer.WriteEndDocument();
                                                writer.Close();
                                            }
                                            DetailXml = sw.ToString();
                                        }

                                        string InvID = invoiceHeader.Rows[0]["InvID"].ToString();

                                        dm.TraceService("Insertion to Package Sales - ODI , Inv_ID : " + InvID);

                                        string[] ar = { dln_udp_ID.ToString(), rot_ID.ToString(), CustomerHeaderID.ToString(), CustomerID.ToString(), CreatedBy.ToString(), CreatedDate.ToString(), DetailXml.ToString() };
                                        DataTable dt = dm.loadList("InsPackageInvoiceODI", "sp_PackageItems", InvID.ToString(), ar);
                                        if (dt.Rows.Count > 0)
                                        {
                                            string Res = dt.Rows[0]["Res"].ToString();
                                            dm.TraceService("Insertion to Package Sales - ODI  Success, Res : " + Res);
                                        }
                                        else
                                        {
                                            string Res = dt.Rows[0]["Res"].ToString();
                                            dm.TraceService("Insertion to Package Sales - ODI  Failure, Res : " + Res);
                                        }
                                    }


                                }
                            }
                            catch(Exception ex) 
                            {
                                dm.TraceService("Error : " + ex.Message);
                            }
                            
                            
                            //Package Items Ends 


                            List<GetInvoiceHeader> listInvoiceHeader = new List<GetInvoiceHeader>();
                            foreach (DataRow dr in invoiceHeader.Rows)
                            {
                                List<GetApplicableDeliveryNotes> listApplicableDeliveryNotes = new List<GetApplicableDeliveryNotes>();
                                foreach (DataRow drDetails in appliDeliNotes.Rows)
                                {
                                    if (dr["InvID"].ToString() == drDetails["ind_inv_ID"].ToString())
                                    {
                                        listApplicableDeliveryNotes.Add(new GetApplicableDeliveryNotes
                                        {
                                            DlnID = drDetails["ind_dln_ID"].ToString(),
                                            InvID = drDetails["ind_inv_ID"].ToString(),
                                        });
                                    }
                                }
                                List<GetInvoiceDetail> listInvoiceDetail = new List<GetInvoiceDetail>();
                                foreach (DataRow drInvoiceDetails in invoiceDetail.Rows)
                                {
                                    if (dr["InvID"].ToString() == drInvoiceDetails["ind_inv_ID"].ToString())
                                    {
                                        listInvoiceDetail.Add(new GetInvoiceDetail
                                        {
                                            InvID = drInvoiceDetails["ind_inv_ID"].ToString(),
                                            IndID = drInvoiceDetails["ind_ID"].ToString(),
                                            DlnID = drInvoiceDetails["ind_dln_ID"].ToString(),
                                            PrdID = drInvoiceDetails["prd_ID"].ToString(),
                                            PrdCode = drInvoiceDetails["prd_Code"].ToString(),
                                            PrdName = drInvoiceDetails["prd_Name"].ToString(),
                                            IndHigherUOM = drInvoiceDetails["ind_HigherUOM"].ToString(),
                                            IndHigherQty = drInvoiceDetails["ind_HigherQty"].ToString(),
                                            IndLowerUOM = drInvoiceDetails["ind_LowerUOM"].ToString(),
                                            IndLowerQty = drInvoiceDetails["ind_LowerQty"].ToString(),
                                            IndHigherPrice = drInvoiceDetails["ind_HigherPrice"].ToString(),
                                            IndLowerPrice = drInvoiceDetails["ind_LowerPrice"].ToString(),
                                            IndLineNo = drInvoiceDetails["ind_LineNo"].ToString(),
                                            IndTransType = drInvoiceDetails["ind_TransType"].ToString(),
                                            IndLineTotal = drInvoiceDetails["ind_LineTotal"].ToString(),
                                            IndPrice = drInvoiceDetails["ind_Price"].ToString(),
                                            IndDiscount = drInvoiceDetails["ind_Discount"].ToString(),
                                            IndDiscountPrec = drInvoiceDetails["ind_DiscountPrec"].ToString(),
                                            IndGrandTotal = drInvoiceDetails["ind_GrandTotal"].ToString(),
                                            IndLineVAT = drInvoiceDetails["ind_LineVAT"].ToString(),
                                            IndPieceDiscount = drInvoiceDetails["ind_PieceDiscount"].ToString(),
                                            IndSubTotalWODiscount = drInvoiceDetails["ind_SubTotal_WO_Discount"].ToString(),
                                            IndTotalQty = drInvoiceDetails["ind_TotalQty"].ToString(),
                                            IndVAT = drInvoiceDetails["ind_VAT"].ToString(),
                                            IndVATPerc = drInvoiceDetails["ind_VATPerc"].ToString(),
                                            PrdDesc = drInvoiceDetails["prd_Desc"].ToString(),
                                            PrdSpec = drInvoiceDetails["prd_Spec"].ToString(),
                                        });
                                    }
                                }
                                listInvoiceHeader.Add(new GetInvoiceHeader
                                {
                                    InvID = dr["InvID"].ToString(),
                                    InvoiceNumber = dr["InvoiceNumber"].ToString(),
                                    InvoiceDate = dr["InvoiceDate"].ToString(),
                                    InvoiceTime = dr["InvoiceTime"].ToString(),
                                    CshCode = dr["csh_Code"].ToString(),
                                    CshName = dr["csh_Name"].ToString(),
                                    Address1 = dr["csh_Address_1"].ToString(),
                                    Address2 = dr["csh_Address_2"].ToString(),
                                    Address3 = dr["csh_Address_3"].ToString(),
                                    CshPhone = dr["csh_Phone"].ToString(),
                                    CshTRN = dr["csh_TRN"].ToString(),
                                    CshPatID = dr["csh_pat_ID"].ToString(),
                                    CshCurrency = dr["csh_Currency"].ToString(),
                                    SubTotalWODiscount = dr["inv_SubTotal_WO_Discount"].ToString(),
                                    Discount = dr["inv_Discount"].ToString(),
                                    SubTotal = dr["inv_SubTotal"].ToString(),
                                    VAT = dr["inv_VAT"].ToString(),
                                    TotalAmount = dr["inv_TotalAmount"].ToString(),
                                    DepartmentID = dr["inv_dep_ID"].ToString(),
                                    PayType = dr["inv_PayType"].ToString(),
                                    PayMode = dr["inv_PayMode"].ToString(),
                                    ApplicableDeliveryNotes = listApplicableDeliveryNotes,
                                    InvoiceDetail = listInvoiceDetail
                                });
                                string JSONString = JsonConvert.SerializeObject(new
                                {
                                    result = listInvoiceHeader
                                });
                                return JSONString;
                            }
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

            dm.TraceService("PostODI ENDED ");
            dm.TraceService("=================");

            return JSONString;
        }

        public string PostMDI([FromForm] PostMDIInputData inputParams)
        {
            dm.TraceService("PostMDI STARTED ");
            dm.TraceService("===================");
            try
            {
                List<PostMDIHeader> XMLHeaderData = JsonConvert.DeserializeObject<List<PostMDIHeader>>(inputParams.InvoiceHeader);
                List<PostMDIDetail> jsonValue = JsonConvert.DeserializeObject<List<PostMDIDetail>>(inputParams.InvoiceDetail);
                List<PostMDIBatchSerial> XMLBatchData = JsonConvert.DeserializeObject<List<PostMDIBatchSerial>>(inputParams.InvoiceBatchDetail);
               

                dm.TraceService("PostMDI InvoiceHeader -  " + inputParams.InvoiceHeader);
                dm.TraceService("PostMDI InvoiceDetail -  " + inputParams.InvoiceDetail);
                dm.TraceService("PostMDI InvoiceBatchDetail -  " + inputParams.InvoiceBatchDetail);
                

                try
                {
                    DataSet dsInvoiceDetail = new DataSet();

                    DataTable dtInvoiceHeader = new DataTable();
                    dtInvoiceHeader.Columns.Add("dsp_ID", typeof(string));
                    dtInvoiceHeader.Columns.Add("CreatedBy", typeof(string));
                    dtInvoiceHeader.Columns.Add("CreatedDate", typeof(string));
                    dtInvoiceHeader.Columns.Add("GeoCode", typeof(string));
                    dtInvoiceHeader.Columns.Add("GeoCodeName", typeof(string));
                    dtInvoiceHeader.Columns.Add("remarks", typeof(string));
                    dtInvoiceHeader.Columns.Add("dln_cse_ID", typeof(string));
                    dtInvoiceHeader.Columns.Add("CreationMode", typeof(string));
                    dtInvoiceHeader.Columns.Add("dln_udp_ID", typeof(string));
                    dtInvoiceHeader.Columns.Add("dln_Signature", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_SubTotal", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_VAT", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_TotalAmount", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_ReceivedAmount", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_TotalPaidAmount", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_PayType", typeof(string));
                    dtInvoiceHeader.Columns.Add("dln_IsPartiallyDelivered", typeof(string));
                    dtInvoiceHeader.Columns.Add("FreeGoodAmount", typeof(string));
                    dtInvoiceHeader.Columns.Add("HeaderDiscount", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_SubTotal_WO_Discount", typeof(string));
                    dtInvoiceHeader.Columns.Add("AppDeliveryID", typeof(string));
                    dtInvoiceHeader.Columns.Add("rot_ID", typeof(string));
                    dtInvoiceHeader.Columns.Add("paymode", typeof(string));
                    dtInvoiceHeader.Columns.Add("Receiptimg", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_InvoiceID", typeof(string));
                    dtInvoiceHeader.Columns.Add("dln_ID", typeof(string));
                    dtInvoiceHeader.Columns.Add("Type", typeof(string));
                    dtInvoiceHeader.Columns.Add("CustomerID", typeof(string));
                    dtInvoiceHeader.Columns.Add("CustomerHeaderID", typeof(string));
                    dtInvoiceHeader.Columns.Add("PriceUpdateRequested", typeof(string));
                    dtInvoiceHeader.Columns.Add("IsCashCollected", typeof(string));
                    dtInvoiceHeader.Columns.Add("HelperRoute", typeof(string));

                    DataTable dtInvoiceDetail = new DataTable();
                    dtInvoiceDetail.Columns.Add("dlnID", typeof(string));
                    dtInvoiceDetail.Columns.Add("itmID", typeof(string));
                    dtInvoiceDetail.Columns.Add("LIHUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("LILUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("LIHQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("LILQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("FHQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("FHUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("FLQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("FLUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjHQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjHUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjLQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjLUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("ind_Price", typeof(string));
                    dtInvoiceDetail.Columns.Add("dld_TransType", typeof(string));
                    dtInvoiceDetail.Columns.Add("dld_ReturnHigherPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("dld_ReturnLowerPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("PriceUpdateRequested", typeof(string));
                    dtInvoiceDetail.Columns.Add("VAT", typeof(string));
                    dtInvoiceDetail.Columns.Add("InvoicePrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("PieceDiscount", typeof(string));
                    dtInvoiceDetail.Columns.Add("GrandTotal", typeof(string));
                    dtInvoiceDetail.Columns.Add("Discount", typeof(string));
                    dtInvoiceDetail.Columns.Add("SubTotalWODiscount", typeof(string));
                    dtInvoiceDetail.Columns.Add("InvoiceLineNo", typeof(string));
                    dtInvoiceDetail.Columns.Add("DiscountPerc", typeof(string));
                    dtInvoiceDetail.Columns.Add("HigherPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("LowerPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("LossInTransit", typeof(string));
                    dtInvoiceDetail.Columns.Add("ReasonCode", typeof(string));
                    dtInvoiceDetail.Columns.Add("VATPerc", typeof(string));
                    
                    DataTable dtInvoiceBatchSetial = new DataTable();
                    dtInvoiceBatchSetial.Columns.Add("DeliveryHeaderID", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("DeliveryDetailID", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("Number", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("ExpiryDate", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("BaseUOM", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("OrderedQty", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("AdjQty", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("LoadInQty", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("DeliveryStatus", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("itmID", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("LossInTransit", typeof(string));

                    foreach (var header in XMLHeaderData)
                    {
                        dtInvoiceHeader.Rows.Add(header.dsp_ID, header.CreatedBy, header.CreatedDate, header.GeoCode, header.GeoCodeName, header.remarks,
                            header.dln_cse_ID, header.CreationMode, header.dln_udp_ID, header.dln_Signature, header.inv_SubTotal, header.inv_VAT, header.inv_TotalAmount,
                            header.inv_ReceivedAmount, header.inv_TotalPaidAmount, header.inv_PayType, header.dln_IsPartiallyDelivered, header.FreeGoodAmount,
                            header.HeaderDiscount, header.inv_SubTotal_WO_Discount, header.AppDeliveryID, header.rot_ID, header.paymode, header.Receiptimg,
                            header.inv_InvoiceID, header.dln_ID, header.Type, header.CustomerID, header.CustomeHeaderID, header.PriceUpdateRequested ,header.IsCashCollected, header.HelperRoute);
                    }

                    foreach (var item in jsonValue)
                    {
                        dtInvoiceDetail.Rows.Add(item.dlnID, item.itmID, item.LIHUom, item.LILUom, item.LIHQty, item.LILQty, item.FHQty, item.FHUom, item.FLQty, item.FLUom,
                            item.AdjHQty, item.AdjHUom, item.AdjLQty, item.AdjLUom, item.ind_Price, item.dld_TransType, item.dld_ReturnHigherPrice, item.dld_ReturnLowerPrice,
                            item.PriceUpdateRequested, item.VAT, item.InvoicePrice, item.PieceDiscount, item.GrandTotal, item.Discount, item.SubTotalWODiscount, item.InvoiceLineNo,
                            item.DiscountPerc, item.HigherPrice, item.LowerPrice, item.LossInTransit, item.ReasonCode, item.VATPerc);
                    }

                    foreach (var batch in XMLBatchData)
                    {
                        dtInvoiceBatchSetial.Rows.Add(batch.dlnID, batch.dldID, batch.dnsNumber, batch.ExpDate, batch.baseUOM, batch.OrdQty, batch.AdjQty, batch.LIQty, batch.Status,
                            batch.itmID, batch.LossInTransit);
                    }


                    dsInvoiceDetail.Tables.Add(dtInvoiceHeader);
                    dsInvoiceDetail.Tables.Add(dtInvoiceDetail);
                    dsInvoiceDetail.Tables.Add(dtInvoiceBatchSetial);

                    try
                    {
                        string[] keys = { };
                        string[] values = { };
                        string[] arr = { "@InvoiceHeader", "@InvoiceDetail", "@InvoiceBatchDetail"};
                        DataSet Value = dm.bulkUpdate(dsInvoiceDetail, arr, keys, values, "sp_GenerateInvoice_MDI");
                        dm.TraceService("1");
                        dm.TraceService("Table count : " + Value.Tables.Count.ToString());
                        if (Value.Tables.Count < 2)
                        {
                            dm.TraceService("2");

                            JSONString = "NoDataSQL - " + Value.Tables[0].Rows[0]["Descr"].ToString() ;
                            dm.TraceService(JSONString);
                        }
                        else
                        {
                            dm.TraceService("3");
                            DataTable invoiceHeader = Value.Tables[0];
                            DataTable appliDeliNotes = Value.Tables[1];
                            DataTable invoiceDetail = Value.Tables[2];
                            if (invoiceHeader.Rows.Count > 0)
                            {
                                dm.TraceService("5");
                                List<GetInvoiceHeader> listInvoiceHeader = new List<GetInvoiceHeader>();
                                foreach (DataRow dr in invoiceHeader.Rows)
                                {
                                    try
                                    {
                                        string res = dr["InvID"].ToString();

                                        //Package Items Starts
                                        try
                                        {
                                            List<PostMDIPackage> XMLPackageData = JsonConvert.DeserializeObject<List<PostMDIPackage>>(inputParams.InvoicePackageDetail);
                                            dm.TraceService("PostMDI InvoicePackageDetail -  " + inputParams.InvoicePackageDetail);

                                            if (inputParams.InvoicePackageDetail != null)
                                            {
                                                if (XMLPackageData.Count > 0)
                                                {
                                                    dm.TraceService("Insertion to Package Sales - MDI Starts , Inv_ID : " + res);

                                                    string DetailXml = "";

                                                    using (var sw = new StringWriter())
                                                    {
                                                        using (var writer = XmlWriter.Create(sw))
                                                        {

                                                            writer.WriteStartDocument(true);
                                                            writer.WriteStartElement("r");
                                                            int c = 0;
                                                            foreach (PostMDIPackage id in XMLPackageData)
                                                            {

                                                                string[] aarPck = { id.itmID.ToString(), id.Qty.ToString(), id.udp_ID.ToString(), id.rot_ID.ToString(), id.CustomerHeaderID.ToString(), id.CustomerID.ToString(), id.CreatedBy.ToString(), id.CreatedDate.ToString() };
                                                                string[] arrName = { "itmID", "Qty", "udp_ID", "rot_ID", "CustomerHeaderID", "CustomerID", "CreatedBy", "CreatedDate" };
                                                                dm.createNode(aarPck, arrName, writer);
                                                            }

                                                            writer.WriteEndElement();
                                                            writer.WriteEndDocument();
                                                            writer.Close();
                                                        }
                                                        DetailXml = sw.ToString();
                                                    }

                                                    string[] ar = { DetailXml.ToString() };
                                                    DataTable dt = dm.loadList("InsPackageInvoiceMDI", "sp_PackageItems", res.ToString(), ar);
                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        string Res = dt.Rows[0]["Res"].ToString();
                                                        dm.TraceService("Insertion to Package Sales - MDI Success, Res : " + Res);
                                                    }
                                                    else
                                                    {
                                                        string Res = dt.Rows[0]["Res"].ToString();
                                                        dm.TraceService("Insertion to Package Sales - MDI Failure, Res : " + Res);
                                                    }

                                                    dm.TraceService("Insertion to Package Sales - MDI Ends.");
                                                }
                                                   
                                            }
                                        }
                                        catch(Exception ex)
                                        {
                                            dm.TraceService("Error : " + ex.Message.ToString());
                                            
                                        }
                                        
                                       
                                        //Package Items Ends 



                                        List<GetApplicableDeliveryNotes> listApplicableDeliveryNotes = new List<GetApplicableDeliveryNotes>();
                                        foreach (DataRow drDetails in appliDeliNotes.Rows)
                                        {
                                            if (dr["InvID"].ToString() == drDetails["ind_inv_ID"].ToString())
                                            {
                                                listApplicableDeliveryNotes.Add(new GetApplicableDeliveryNotes
                                                {
                                                    DlnID = drDetails["ind_dln_ID"].ToString(),
                                                    InvID = drDetails["ind_inv_ID"].ToString(),
                                                });
                                            }
                                        }
                                        List<GetInvoiceDetail> listInvoiceDetail = new List<GetInvoiceDetail>();
                                        foreach (DataRow drInvoiceDetails in invoiceDetail.Rows)
                                        {
                                            if (dr["InvID"].ToString() == drInvoiceDetails["ind_inv_ID"].ToString())
                                            {
                                                listInvoiceDetail.Add(new GetInvoiceDetail
                                                {
                                                    InvID = drInvoiceDetails["ind_inv_ID"].ToString(),
                                                    IndID = drInvoiceDetails["ind_ID"].ToString(),
                                                    DlnID = drInvoiceDetails["ind_dln_ID"].ToString(),
                                                    PrdID = drInvoiceDetails["prd_ID"].ToString(),
                                                    PrdCode = drInvoiceDetails["prd_Code"].ToString(),
                                                    PrdName = drInvoiceDetails["prd_Name"].ToString(),
                                                    IndHigherUOM = drInvoiceDetails["ind_HigherUOM"].ToString(),
                                                    IndHigherQty = drInvoiceDetails["ind_HigherQty"].ToString(),
                                                    IndLowerUOM = drInvoiceDetails["ind_LowerUOM"].ToString(),
                                                    IndLowerQty = drInvoiceDetails["ind_LowerQty"].ToString(),
                                                    IndHigherPrice = drInvoiceDetails["ind_HigherPrice"].ToString(),
                                                    IndLowerPrice = drInvoiceDetails["ind_LowerPrice"].ToString(),
                                                    IndLineNo = drInvoiceDetails["ind_LineNo"].ToString(),
                                                    IndTransType = drInvoiceDetails["ind_TransType"].ToString(),
                                                    IndLineTotal = drInvoiceDetails["ind_LineTotal"].ToString(),
                                                    IndPrice = drInvoiceDetails["ind_Price"].ToString(),
                                                    IndDiscount = drInvoiceDetails["ind_Discount"].ToString(),
                                                    IndDiscountPrec = drInvoiceDetails["ind_DiscountPrec"].ToString(),
                                                    IndGrandTotal = drInvoiceDetails["ind_GrandTotal"].ToString(),
                                                    IndLineVAT = drInvoiceDetails["ind_LineVAT"].ToString(),
                                                    IndPieceDiscount = drInvoiceDetails["ind_PieceDiscount"].ToString(),
                                                    IndSubTotalWODiscount = drInvoiceDetails["ind_SubTotal_WO_Discount"].ToString(),
                                                    IndTotalQty = drInvoiceDetails["ind_TotalQty"].ToString(),
                                                    IndVAT = drInvoiceDetails["ind_VAT"].ToString(),
                                                    IndVATPerc = drInvoiceDetails["ind_VATPerc"].ToString(),
                                                    PrdDesc = drInvoiceDetails["prd_Desc"].ToString(),
                                                    PrdSpec = drInvoiceDetails["prd_Spec"].ToString(),
                                                });
                                            }
                                        }
                                        listInvoiceHeader.Add(new GetInvoiceHeader
                                        {
                                            InvID = dr["InvID"].ToString(),
                                            InvoiceNumber = dr["InvoiceNumber"].ToString(),
                                            InvoiceDate = dr["InvoiceDate"].ToString(),
                                            InvoiceTime = dr["InvoiceTime"].ToString(),
                                            CshCode = dr["csh_Code"].ToString(),
                                            CshName = dr["csh_Name"].ToString(),
                                            Address1 = dr["csh_Address_1"].ToString(),
                                            Address2 = dr["csh_Address_2"].ToString(),
                                            Address3 = dr["csh_Address_3"].ToString(),
                                            CshPhone = dr["csh_Phone"].ToString(),
                                            CshTRN = dr["csh_TRN"].ToString(),
                                            CshPatID = dr["csh_pat_ID"].ToString(),
                                            CshCurrency = dr["csh_Currency"].ToString(),
                                            SubTotalWODiscount = dr["inv_SubTotal_WO_Discount"].ToString(),
                                            Discount = dr["inv_Discount"].ToString(),
                                            SubTotal = dr["inv_SubTotal"].ToString(),
                                            VAT = dr["inv_VAT"].ToString(),
                                            TotalAmount = dr["inv_TotalAmount"].ToString(),
                                            DepartmentID = dr["inv_dep_ID"].ToString(),
                                            PayType = dr["inv_PayType"].ToString(),
                                            PayMode = dr["inv_PayMode"].ToString(),
                                            ApplicableDeliveryNotes = listApplicableDeliveryNotes,
                                            InvoiceDetail = listInvoiceDetail
                                        });
                                        string JSONString = JsonConvert.SerializeObject(new
                                        {
                                            result = listInvoiceHeader
                                        });
                                        dm.TraceService("JSONString : " + JSONString);
                                        return JSONString;
                                    }
                                    catch (Exception ex)
                                    {
                                        dm.TraceService(ex.Message.ToString());
                                        JSONString = "NoDataParameters - " + dr["Descr"].ToString();
                                        return JSONString;
                                    }


                                }
                            }
                            dm.TraceService("4");
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
                dm.TraceService("Exception 1");
                dm.TraceService(ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }

            dm.TraceService("PostMDI ENDED ");
            dm.TraceService("=================");

            return JSONString;
        }

        public string PostDeliveryNoteCustomer([FromForm] PostMDIInputData inputParams)
        {
            dm.TraceService("PostDeliveryNoteCustomer STARTED ");
            dm.TraceService("=================================");
            try
            {
                List<PostMDIHeader> XMLHeaderData = JsonConvert.DeserializeObject<List<PostMDIHeader>>(inputParams.InvoiceHeader);
                List<PostMDIDetail> jsonValue = JsonConvert.DeserializeObject<List<PostMDIDetail>>(inputParams.InvoiceDetail);
                List<PostMDIBatchSerial> XMLBatchData = JsonConvert.DeserializeObject<List<PostMDIBatchSerial>>(inputParams.InvoiceBatchDetail);

                try
                {
                    DataSet dsInvoiceDetail = new DataSet();

                    DataTable dtInvoiceHeader = new DataTable();
                    dtInvoiceHeader.Columns.Add("dsp_ID", typeof(string));
                    dtInvoiceHeader.Columns.Add("CreatedBy", typeof(string));
                    dtInvoiceHeader.Columns.Add("CreatedDate", typeof(string));
                    dtInvoiceHeader.Columns.Add("GeoCode", typeof(string));
                    dtInvoiceHeader.Columns.Add("GeoCodeName", typeof(string));
                    dtInvoiceHeader.Columns.Add("remarks", typeof(string));
                    dtInvoiceHeader.Columns.Add("dln_cse_ID", typeof(string));
                    dtInvoiceHeader.Columns.Add("CreationMode", typeof(string));
                    dtInvoiceHeader.Columns.Add("dln_udp_ID", typeof(string));
                    dtInvoiceHeader.Columns.Add("dln_Signature", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_SubTotal", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_VAT", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_TotalAmount", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_ReceivedAmount", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_TotalPaidAmount", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_PayType", typeof(string));
                    dtInvoiceHeader.Columns.Add("dln_IsPartiallyDelivered", typeof(string));
                    dtInvoiceHeader.Columns.Add("FreeGoodAmount", typeof(string));
                    dtInvoiceHeader.Columns.Add("HeaderDiscount", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_SubTotal_WO_Discount", typeof(string));
                    dtInvoiceHeader.Columns.Add("AppDeliveryID", typeof(string));
                    dtInvoiceHeader.Columns.Add("rot_ID", typeof(string));
                    dtInvoiceHeader.Columns.Add("paymode", typeof(string));
                    dtInvoiceHeader.Columns.Add("Receiptimg", typeof(string));
                    dtInvoiceHeader.Columns.Add("inv_InvoiceID", typeof(string));
                    dtInvoiceHeader.Columns.Add("dln_ID", typeof(string));
                    dtInvoiceHeader.Columns.Add("Type", typeof(string));
                    dtInvoiceHeader.Columns.Add("CustomerID", typeof(string));
                    dtInvoiceHeader.Columns.Add("CustomerHeaderID", typeof(string));
                    dtInvoiceHeader.Columns.Add("PriceUpdateRequested", typeof(string));
                    dtInvoiceHeader.Columns.Add("IsCashCollected", typeof(string));
                    dtInvoiceHeader.Columns.Add("HelperRoute", typeof(string));

                    DataTable dtInvoiceDetail = new DataTable();
                    dtInvoiceDetail.Columns.Add("dlnID", typeof(string));
                    dtInvoiceDetail.Columns.Add("itmID", typeof(string));
                    dtInvoiceDetail.Columns.Add("LIHUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("LILUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("LIHQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("LILQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("FHQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("FHUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("FLQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("FLUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjHQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjHUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjLQty", typeof(string));
                    dtInvoiceDetail.Columns.Add("AdjLUom", typeof(string));
                    dtInvoiceDetail.Columns.Add("ind_Price", typeof(string));
                    dtInvoiceDetail.Columns.Add("dld_TransType", typeof(string));
                    dtInvoiceDetail.Columns.Add("dld_ReturnHigherPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("dld_ReturnLowerPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("PriceUpdateRequested", typeof(string));
                    dtInvoiceDetail.Columns.Add("VAT", typeof(string));
                    dtInvoiceDetail.Columns.Add("InvoicePrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("PieceDiscount", typeof(string));
                    dtInvoiceDetail.Columns.Add("GrandTotal", typeof(string));
                    dtInvoiceDetail.Columns.Add("Discount", typeof(string));
                    dtInvoiceDetail.Columns.Add("SubTotalWODiscount", typeof(string));
                    dtInvoiceDetail.Columns.Add("InvoiceLineNo", typeof(string));
                    dtInvoiceDetail.Columns.Add("DiscountPerc", typeof(string));
                    dtInvoiceDetail.Columns.Add("HigherPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("LowerPrice", typeof(string));
                    dtInvoiceDetail.Columns.Add("LossInTransit", typeof(string));
                    dtInvoiceDetail.Columns.Add("ReasonCode", typeof(string));
                    dtInvoiceDetail.Columns.Add("VATPerc", typeof(string));
                    
                    DataTable dtInvoiceBatchSetial = new DataTable();
                    dtInvoiceBatchSetial.Columns.Add("DeliveryHeaderID", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("DeliveryDetailID", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("Number", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("ExpiryDate", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("BaseUOM", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("OrderedQty", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("AdjQty", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("LoadInQty", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("DeliveryStatus", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("itmID", typeof(string));
                    dtInvoiceBatchSetial.Columns.Add("LossInTransit", typeof(string));

                    foreach (var header in XMLHeaderData)
                    {
                        dtInvoiceHeader.Rows.Add(header.dsp_ID, header.CreatedBy, header.CreatedDate, header.GeoCode, header.GeoCodeName, header.remarks,
                            header.dln_cse_ID, header.CreationMode, header.dln_udp_ID, header.dln_Signature, header.inv_SubTotal, header.inv_VAT, header.inv_TotalAmount,
                            header.inv_ReceivedAmount, header.inv_TotalPaidAmount, header.inv_PayType, header.dln_IsPartiallyDelivered, header.FreeGoodAmount,
                            header.HeaderDiscount, header.inv_SubTotal_WO_Discount, header.AppDeliveryID, header.rot_ID, header.paymode, header.Receiptimg,
                            header.inv_InvoiceID, header.dln_ID, header.Type, header.CustomerID, header.CustomeHeaderID, header.PriceUpdateRequested, header.IsCashCollected, header.HelperRoute);
                    }

                    foreach (var item in jsonValue)
                    {
                        dtInvoiceDetail.Rows.Add(item.dlnID, item.itmID, item.LIHUom, item.LILUom, item.LIHQty, item.LILQty, item.FHQty, item.FHUom, item.FLQty, item.FLUom,
                            item.AdjHQty, item.AdjHUom, item.AdjLQty, item.AdjLUom, item.ind_Price, item.dld_TransType, item.dld_ReturnHigherPrice, item.dld_ReturnLowerPrice,
                            item.PriceUpdateRequested, item.VAT, item.InvoicePrice, item.PieceDiscount, item.GrandTotal, item.Discount, item.SubTotalWODiscount, item.InvoiceLineNo,
                            item.DiscountPerc, item.HigherPrice, item.LowerPrice, item.LossInTransit, item.ReasonCode, item.VATPerc);
                    }

                    foreach (var batch in XMLBatchData)
                    {
                        dtInvoiceBatchSetial.Rows.Add(batch.dlnID, batch.dldID, batch.dnsNumber, batch.ExpDate, batch.baseUOM, batch.OrdQty, batch.AdjQty, batch.LIQty, batch.Status,
                            batch.itmID, batch.LossInTransit);
                    }

                    dsInvoiceDetail.Tables.Add(dtInvoiceHeader);
                    dsInvoiceDetail.Tables.Add(dtInvoiceDetail);
                    dsInvoiceDetail.Tables.Add(dtInvoiceBatchSetial);

                    try
                    {
                        string[] keys = { };
                        string[] values = { };
                        string[] arr = { "@InvoiceHeader", "@InvoiceDetail", "@InvoiceBatchDetail" };
                        DataSet Value = dm.bulkUpdate(dsInvoiceDetail, arr, keys, values, "sp_Generate_DeleveryNote");
                        foreach (DataTable table in Value.Tables)
                        {
                            string Mode = table.Rows[0]["res"].ToString();
                            string Status = table.Rows[0]["status"].ToString();
                            dm.TraceService("Response from Sql Procedure : Mode=" + Mode + " and Status=" + Status);
                            List<GetDeliNoteInsertStatus> listStatus = new List<GetDeliNoteInsertStatus>();
                            listStatus.Add(new GetDeliNoteInsertStatus
                            {
                                Res = Mode,
                                Title = Status,
                                Descr = ""
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

            dm.TraceService("PostDeliveryNoteCustomer ENDED ");
            dm.TraceService("==============================");

            return JSONString;
        }

        public string PostVoidDeliveryNote([FromForm] PostVoid inputParams)
        {
            dm.TraceService("PostVoidDeliveryNote STARTED ");
            dm.TraceService("==============================");
            try
            {
                List<PostVoidDetail> DeliveryVoid = JsonConvert.DeserializeObject<List<PostVoidDetail>>(inputParams.VoidDeliveryNote);

                try
                {
                    DataSet dsDeliveryVoid = new DataSet();

                    DataTable dtDeliveryVoid = new DataTable();
                    dtDeliveryVoid.Columns.Add("DeliveryHeaderID", typeof(string));
                    dtDeliveryVoid.Columns.Add("VoidMode", typeof(string));
                    dtDeliveryVoid.Columns.Add("VoidUser", typeof(string));
                    dtDeliveryVoid.Columns.Add("VoidDate", typeof(string));
                    dtDeliveryVoid.Columns.Add("VoidTime", typeof(string));
                    dtDeliveryVoid.Columns.Add("VoidPlatform", typeof(string));
                    dtDeliveryVoid.Columns.Add("VoidStatus", typeof(string));

                    foreach (var header in DeliveryVoid)
                    {
                        dtDeliveryVoid.Rows.Add(header.dlnID, header.VoidMode, header.VoidUser, header.VoidDate, header.VoidTime, header.VoidPlatform, header.VoidStatus);
                    }

                    dsDeliveryVoid.Tables.Add(dtDeliveryVoid);

                    try
                    {
                        string[] keys = { };
                        string[] values = { };
                        string[] arr = { "@VoidDeliveryNote" };
                        DataSet Value = dm.bulkUpdate(dsDeliveryVoid, arr, keys, values, "sp_Generate_DeleveryNote_Void");
                        foreach (DataTable table in Value.Tables)
                        {
                            string Mode = table.Rows[0]["res"].ToString();
                            string Status = table.Rows[0]["status"].ToString();
                            dm.TraceService("Response from Sql Procedure : Mode=" + Mode + " and Status=" + Status);
                            List<GetInsertStatus> listStatus = new List<GetInsertStatus>();
                            listStatus.Add(new GetInsertStatus
                            {
                                Mode = Mode,
                                Status = Status
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

            dm.TraceService("PostVoidDeliveryNote ENDED ");
            dm.TraceService("==========================");

            return JSONString;
        }

        public string PostTransactionalImage([FromForm] PostAttachments inputParams)
        {
            dm.TraceService("PostTransactionalImage STARTED ");
            dm.TraceService("==============================");
            try
            {
                string mode = inputParams.Mode == null ? "0" : inputParams.Mode;
                string invoiceID = inputParams.InvoiceID == null ? "0" : inputParams.InvoiceID;
                string userID = inputParams.UserID == null ? "0" : inputParams.UserID;
                string attachType = inputParams.AttachType == null ? "0" : inputParams.AttachType;
                dm.TraceService("Value for Mode" + mode.ToString());
                dm.TraceService("Value for Transaction" + invoiceID.ToString());
                dm.TraceService("Value for User" + userID.ToString());
                dm.TraceService("Value for Attachament Type" + attachType.ToString());
                try
                {
                    var HttpReq = HttpContext.Request;
                    try
                    {
                        HttpPostedFileBase[] imageFiles = new HttpPostedFileBase[HttpReq.Files.Count];
                        dm.TraceService("Image Received in Httpreq" + imageFiles.Length.ToString());
                        var folderName = DateTime.Now.ToString("ddMMyyyy");
                        string newServerBasePath = ConfigurationManager.AppSettings["NewServerBasePath"];
                        var physicalPath = Server.MapPath("~/TransactionalDocuments/" + attachType + "/Images"  );
                        dm.TraceService("Physical Path Generated" + physicalPath.ToString());
                        if (!Directory.Exists(physicalPath))
                        {
                            Directory.CreateDirectory(physicalPath);
                            dm.TraceService("Directory Created");
                        }
                        string image = "";
                        var imagePath = physicalPath + "/" + invoiceID;
                        if (!Directory.Exists(imagePath))
                        {
                            Directory.CreateDirectory(imagePath);
                            dm.TraceService("Directory for Image Path Created");
                        }
                        
                        for (int y = 0; y < HttpReq.Files.Count; y++)
                        {
                            
                            dm.TraceService("Loop Started" + y.ToString());
                            imageFiles[y] = HttpReq.Files[y];
                            image = imagePath + "/" + imageFiles[y].FileName + (DateTime.Now.ToString("HHmmss") + ".jpg");
                            imageFiles[y].SaveAs(image);
                            dm.TraceService("ImageFile" + imageFiles[y].FileName.ToString());
                            dm.TraceService("Loop Ended" + y.ToString());
                        }


						List<GetInsertAttachmentStatus> listStatus = new List<GetInsertAttachmentStatus>();
						listStatus.Add(new GetInsertAttachmentStatus
						{
							Mode = "1",
							Status = "Success"
						});
						string JSONString = JsonConvert.SerializeObject(new
						{
							result = listStatus
						});
						return JSONString;
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

            dm.TraceService("PostTransactionalImage ENDED ");
            dm.TraceService("==========================");

            return JSONString;
        }


        public void GenerateDN_StampedPDF()
        {
            dm.TraceService("GenerateDN_StampedPDF STARTED ");
            dm.TraceService("==============================");

            var ImagesPath = Server.MapPath("~/TransactionalDocuments/" + "Stamped" + "/Images");

			var FinalPath = Server.MapPath("~/TransactionalDocuments/" + "Stamped" + "/PDF");

            dm.TraceService("ImagesPath :-  " + ImagesPath);
            dm.TraceService("FinalPath :-  " + FinalPath);

            string[] allFiles = Directory.GetDirectories(ImagesPath, "*.*", SearchOption.AllDirectories);

            foreach(string dir in allFiles)
            {
                dm.TraceService("FIlename" + dir);
                string lastDirName = Path.GetFileName(dir);
                string finalFileName = lastDirName + ".pdf";
				string res = dm.CombineImagesToPdf(dir, FinalPath + "/" + finalFileName);
				dm.TraceService("Value for Res" + res.ToString());
				if (res.Equals("1"))
				{
				
				
					try
					{
						DataSet dsTransAttachment = new DataSet();

						DataTable dtTransAttachment = new DataTable();
						dtTransAttachment.Columns.Add("AttachmentPath", typeof(string));
						dtTransAttachment.Columns.Add("AttachmentType", typeof(string));

						dtTransAttachment.Rows.Add("/TransactionalDocuments/Stamped/PDF/" + finalFileName, "STAMPED");
						dsTransAttachment.Tables.Add(dtTransAttachment);

						try
						{
							string[] keys = { "@Mode", "@TransactionID", "@UserID" };
							string[] values = { "DN", lastDirName, ""};
							string[] arr = { "@TransAttachment" };
							DataSet Value = dm.bulkUpdate(dsTransAttachment, arr, keys, values, "sp_TransactionalAttachment");

                            //try
                            //{
                            //    if (Directory.Exists(dir))
                            //    {
                            //        Directory.Delete(dir, true);
                            //        dm.TraceService("Image Directory Deleted");
                            //    }
                            //    else
                            //    {
                            //        dm.TraceService("The folder does not exist.");
                            //    }
                            //}
                            //catch (Exception ex)
                            //{
                            //    Console.WriteLine($"Error while deleting folder: {ex.Message}");
                            //}
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
				else
				{

					JSONString = "NoDataImage - " + "Image Upload failed";

				}
			}

            dm.TraceService("GenerateDN_StampedPDF ENDED ");
            dm.TraceService("==========================");
        }

        public string PostStampedCopyBackend([FromForm] PostStampedBackndIn inputParams)
        {
            dm.TraceService("PostStampedCopyBackend STARTED ");
            dm.TraceService("==============================");
            try
            {
                //string dln_ID = inputParams.dln_ID == null ? "0" : inputParams.dln_ID;
                string dln_ID = HttpContext.Request.Form["dln_ID"];
                dm.TraceService("Value for dln_ID -" + dln_ID.ToString());
               
                try
                {
                    var HttpReq = HttpContext.Request;
                    try
                    {
                        HttpPostedFileBase[] PDFFiles = new HttpPostedFileBase[HttpReq.Files.Count];
                        dm.TraceService("pdf Received in Httpreq -" + PDFFiles.Length.ToString());
                        var folderName = DateTime.Now.ToString("ddMMyyyy");
                       
                        var physicalPath = Server.MapPath("~/TransactionalDocuments/Stamped/PDF");
                        dm.TraceService("Physical Path Generated -" + physicalPath.ToString());
                        if (!Directory.Exists(physicalPath))
                        {
                            Directory.CreateDirectory(physicalPath);
                            dm.TraceService("Directory Created");
                        }
                        string pdf = "";
                        var pdfPath = physicalPath;
                           
                        string pdfFile = "";
                        for (int y = 0; y < HttpReq.Files.Count; y++)
                        {

                            dm.TraceService("Loop Started -" + y.ToString());
                            PDFFiles[y] = HttpReq.Files[y];
                            string pdfattch =  PDFFiles[y].FileName ;
                            pdf = pdfPath + "/" +  PDFFiles[y].FileName ;
                            PDFFiles[y].SaveAs(pdf);
                            pdfFile = "/TransactionalDocuments/Stamped/PDF/" + pdfattch;
                           
                            dm.TraceService("pdfFile -" + PDFFiles[y].FileName.ToString());
                            dm.TraceService("Loop Ended -" + y.ToString());
                        }
                       
                        string[] ar = { pdfFile.ToString() };
                        DataTable dtDN = dm.loadList("UpdateStampedCopy", "sp_Transaction_Uom", dln_ID.ToString(), ar);

                        if (dtDN.Rows.Count > 0)
                        {
                            List<GetDeliNoteInsertStatus> listDn = new List<GetDeliNoteInsertStatus>();
                            foreach (DataRow dr in dtDN.Rows)
                            {
                                listDn.Add(new GetDeliNoteInsertStatus
                                {
                                    Res = dr["Res"].ToString(),
                                    Title = dr["Title"].ToString(),
                                    Descr = dr["Descr"].ToString()


                                });
                            }
                            JSONString = JsonConvert.SerializeObject(new
                            {
                                result = listDn
                            });

                            return JSONString;
                        }
                        else
                        {
                            JSONString = "NoDataRes";
                            dm.TraceService("NoDataRes");
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

            dm.TraceService("PostStampedCopyBackend ENDED ");
            dm.TraceService("==========================");

            return JSONString;
        }

        public string UpdateDeliveryCSEID([FromForm] DeliveryCSEIDIn inputParams)
        {
            dm.TraceService("UpdateDeliveryCSEID STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            dm.TraceService("CSEID  -"+ inputParams.CseID + DateTime.Now.ToString());
            try
            {
                List<DelCseIds> cseData = JsonConvert.DeserializeObject<List<DelCseIds>>(inputParams.JsonString);
                
                string User = inputParams.UserID == null ? "0" : inputParams.UserID;

                string CseID = inputParams.CseID == null ? "0" : inputParams.CseID;
                string InputXml = "";
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (DelCseIds id in cseData)
                        {
                            string[] arr = { id.dln_ID.ToString() };
                            string[] arrName = { "dln_ID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw.ToString();
                }



                string[] ar = { InputXml.ToString(), User.ToString() };
                DataTable dtdelIn = dm.loadList("UpdateDeliveryCseID", "sp_App_UOM", CseID.ToString(), ar);

                if (dtdelIn.Rows.Count > 0)
                {
                    List<DeliveryCSEIDOut> listItems = new List<DeliveryCSEIDOut>();
                    foreach (DataRow dr in dtdelIn.Rows)
                    {

                        listItems.Add(new DeliveryCSEIDOut
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
                dm.TraceService(" UpdateDeliveryCSEID Exception - " + ex.Message.ToString());
            }
            dm.TraceService("UpdateDeliveryCSEID ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string GetDeliveryHeader([FromForm] GetDeliveryInpara inputParams)
        {
            dm.TraceService("GetDeliveryHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rot_ID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;

                string[] arr = { userID.ToString() };

                DataTable dtTrnsIn = dm.loadList("SelDeliveryHeaderForKPI", "Sp_App_UOM", rot_ID.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<GetDeliveryOutpara> listItems = new List<GetDeliveryOutpara>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new GetDeliveryOutpara
                        {

                            dln_ID = dr["dln_ID"].ToString(),
                            dln_DeliveryNumber = dr["dln_DeliveryNumber"].ToString(),
                          
                            Date = dr["Date"].ToString(),
                            Time = dr["Time"].ToString(),
                            CustomerID = dr["cus_ID"].ToString(),
                            CustomerCode = dr["cus_Code"].ToString(),
                            CustomerName = dr["cus_Name"].ToString(),
                            CusHeaderID = dr["csh_ID"].ToString(),
                            CusHeaderCode = dr["csh_Code"].ToString(),
                            CusHeaderName = dr["csh_Name"].ToString(),
                            Type = dr["Type"].ToString(),
                            Department = dr["dep_ShortName"].ToString(),
                            TransType = dr["dld_TransType"].ToString(),

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
                dm.TraceService(" GetDeliveryHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetDeliveryHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string PostDNReceiptImg([FromForm] PostDNImg inputParams)
        {
            dm.TraceService("PostDNReceiptImg STARTED ");
            dm.TraceService("==============================");
            try
            {
                string dln_ID = inputParams.dln_ID == null ? "0" : inputParams.dln_ID;
                string attachType = inputParams.AttachType == null ? "0" : inputParams.AttachType;
                dm.TraceService("Value for dln_ID" + dln_ID.ToString());

                try
                {
                    var HttpReq = HttpContext.Request;
                    try
                    {
                        HttpPostedFileBase[] imageFiles = new HttpPostedFileBase[HttpReq.Files.Count];
                        dm.TraceService("Image Received in Httpreq" + imageFiles.Length.ToString());
                        var folderName = DateTime.Now.ToString("ddMMyyyy");
                        string newServerBasePath = ConfigurationManager.AppSettings["NewServerBasePath"];
                        var physicalPath = Server.MapPath("~/DNDocument/" + attachType);
                        dm.TraceService("Physical Path Generated" + physicalPath.ToString());
                        if (!Directory.Exists(physicalPath))
                        {
                            Directory.CreateDirectory(physicalPath);
                            dm.TraceService("Directory Created");
                        }
                        string image = "";
                        var imagePath = physicalPath + "/" + dln_ID;
                        if (!Directory.Exists(imagePath))
                        {
                            Directory.CreateDirectory(imagePath);
                            dm.TraceService("Directory for Image Path Created");
                        }
                        string ReceiptImages = "";
                        for (int y = 0; y < HttpReq.Files.Count; y++)
                        {

                            dm.TraceService("Loop Started" + y.ToString());
                            imageFiles[y] = HttpReq.Files[y];
                            string REcimage = (DateTime.Now.ToString("HHmmss") + imageFiles[y].FileName);
                            image = imagePath + "/" + (DateTime.Now.ToString("HHmmss") + imageFiles[y].FileName);
                            imageFiles[y].SaveAs(image);
                            if (y == 0)
                            {
                                ReceiptImages = "/DNDocument/DNReceipt/" + dln_ID + "/" + REcimage;

                            }
                            else
                            {
                                ReceiptImages += "," + "/DNDocument/DNReceipt/" + dln_ID + "/" + REcimage;
                            }
                            dm.TraceService("ImageFile" + imageFiles[y].FileName.ToString());
                            dm.TraceService("Loop Ended" + y.ToString());
                        }

                        string[] ar = { ReceiptImages.ToString() };
                        DataTable dtDN = dm.loadList("InsDliveryReceiptImage", "sp_Dispatch", dln_ID.ToString(), ar);

                        if (dtDN.Rows.Count > 0)
                        {
                            List<GetDeliNoteInsertStatus> listDn = new List<GetDeliNoteInsertStatus>();
                            foreach (DataRow dr in dtDN.Rows)
                            {
                                listDn.Add(new GetDeliNoteInsertStatus
                                {
                                    Res = dr["Res"].ToString(),
                                    Title = dr["Title"].ToString(),
                                    Descr = dr["Descr"].ToString()


                                });
                            }
                            JSONString = JsonConvert.SerializeObject(new
                            {
                                result = listDn
                            });

                            return JSONString;
                        }
                        else
                        {
                            JSONString = "NoDataRes";
                            dm.TraceService("NoDataRes");
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

            dm.TraceService("PostDNReceiptImg ENDED ");
            dm.TraceService("==========================");

            return JSONString;
        }

        public string GetInvoiceInfo([FromForm] GetInvoiceInfoInpara inputParams)
        {
            dm.TraceService("GetInvoiceInfo STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string inv_ID = inputParams.inv_ID == null ? "0" : inputParams.inv_ID;

                string[] arr = { };

                DataSet dtTrnsIn = dm.loadListDS("SelInvoiceInfo", "sp_DocumentGeneration", inv_ID.ToString(),arr);
                DataTable InvHeader = dtTrnsIn.Tables[0];
                DataTable InvDetail = dtTrnsIn.Tables[1];
                DataTable DeliveryInfo = dtTrnsIn.Tables[2];
                DataTable BatchInfo = dtTrnsIn.Tables[3];
                DataTable TDInfo = dtTrnsIn.Tables[4];

                if (InvHeader.Rows.Count > 0)
                {
                    List<GetInvoiceOutpara> listHeader = new List<GetInvoiceOutpara>();
                    foreach (DataRow dr in InvHeader.Rows)
                    {

                        listHeader.Add(new GetInvoiceOutpara
                        {
                            inv_InvoiceID = dr["inv_InvoiceID"].ToString(),
                            inv_ID = dr["inv_ID"].ToString(),
                            csh_Code = dr["csh_Code"].ToString(),
                            csh_Name = dr["csh_Name"].ToString(),
                            csh_Address_1 = dr["csh_Address_1"].ToString(),
                            csh_Address_2 = dr["csh_Address_2"].ToString(),
                            csh_Address_3 = dr["csh_Address_3"].ToString(),
                            csh_Phone = dr["csh_Phone"].ToString(),
                            csh_TRN = dr["csh_TRN"].ToString(),
                            pat_Description = dr["pat_Description"].ToString(),
                            csh_Currency = dr["csh_Currency"].ToString(),
                            OutletName = dr["OutletName"].ToString(),
                            inv_SubTotal_WO_Discount = dr["inv_SubTotal_WO_Discount"].ToString(),
                            inv_Discount = dr["inv_Discount"].ToString(),
                            inv_SubTotal = dr["inv_SubTotal"].ToString(),
                            inv_VAT = dr["inv_VAT"].ToString(),
                            inv_TotalAmount = dr["inv_TotalAmount"].ToString(),
                            createdOn = dr["createdOn"].ToString(),
                            BatchInvoice = dr["BatchInvoice"].ToString(),
                            usr_Name = dr["usr_Name"].ToString(),
                            csh_CostCentre = dr["csh_CostCentre"].ToString(),
                            csh_BillToCustomer = dr["csh_BillToCustomer"].ToString(),
                            csh_FAX = dr["csh_FAX"].ToString(),

                        });
                    }

                    List<InvDetailOut> listDetail = new List<InvDetailOut>();
                    foreach (DataRow drDetail in InvDetail.Rows)
                    {

                        listDetail.Add(new InvDetailOut
                        {
                            Barcode = drDetail["Barcode"].ToString(),
                            prd_Code = drDetail["prd_Code"].ToString(),
                            prd_Name = drDetail["prd_Name"].ToString(),
                            prd_Desc = drDetail["prd_Desc"].ToString(),
                            uom = drDetail["uom"].ToString(),
                            ind_HigherQty = drDetail["ind_HigherQty"].ToString(),
                            UnitPrice = drDetail["UnitPrice"].ToString(),
                            ind_VATPerc = drDetail["ind_VATPerc"].ToString(),
                            ind_VAT = drDetail["ind_VAT"].ToString(),
                            ind_GrandTotal = drDetail["ind_GrandTotal"].ToString(),
                            Discount = drDetail["Discount"].ToString(),
                            ind_dln_ID = drDetail["ind_dln_ID"].ToString(),
                            prd_ID = drDetail["prd_ID"].ToString(),
                           
                        });
                    }

                    List<DeliveryInfoOut> listDeliveryInfo = new List<DeliveryInfoOut>();
                    foreach (DataRow drDel in DeliveryInfo.Rows)
                    {

                        listDeliveryInfo.Add(new DeliveryInfoOut
                        {
                            dln_DeliveryNumber = drDel["dln_DeliveryNumber"].ToString(),
                            dln_ID = drDel["dln_ID"].ToString(),
                           
                        });
                    }

                    List<BatchInfoOut> listBatchInfo = new List<BatchInfoOut>();
                    foreach (DataRow drBatch in BatchInfo.Rows)
                    {

                        listBatchInfo.Add(new BatchInfoOut
                        {
                            Batch = drBatch["Batch"].ToString(),
                            dns_dln_ID = drBatch["dns_dln_ID"].ToString(),
                            dld_prd_ID = drBatch["dld_prd_ID"].ToString(),
                        });
                    }

                    List<TDInfoOut> listTDInfoOut = new List<TDInfoOut>();
                    foreach (DataRow drTD in TDInfo.Rows)
                    {

                        listTDInfoOut.Add(new TDInfoOut
                        {
                            prd_Code = drTD["prd_Code"].ToString(),
                            uom = drTD["uom"].ToString(),
                            ind_dln_ID = drTD["ind_dln_ID"].ToString(),
                            prd_ID = drTD["prd_ID"].ToString(),
                            TDInfo = drTD["TDInfo"].ToString(),
                        });
                    }

                    List<InvoicePrintOut> listInvoicePrint = new List<InvoicePrintOut>();
                    listInvoicePrint.Add(new InvoicePrintOut
                    {
                        InvoiceHeader = listHeader,
                        InvoiceDetail = listDetail,
                        DeliveryInfo = listDeliveryInfo,
                        BatchInfo = listBatchInfo,
                        TDInfo = listTDInfoOut,
                    });

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listInvoicePrint
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
                dm.TraceService("GetInvoiceInfo Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetInvoiceInfo ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}