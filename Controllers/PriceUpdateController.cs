using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
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

namespace MVC_API.Controllers
{

    public class PriceUpdateController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        public string PriceUpdateRequest([FromForm] PriceUpdateRequestIn inputParams)
        {
            dm.TraceService("PriceUpdateRequest STARTED " + DateTime.Now.ToString());
            dm.TraceService("============================================");
            try
            {
                List<PostDeliveryHeader> XMLHeaderData = JsonConvert.DeserializeObject<List<PostDeliveryHeader>>(inputParams.InvoiceHeader);
                List<PostDeliveryDetail> jsonValue = JsonConvert.DeserializeObject<List<PostDeliveryDetail>>(inputParams.InvoiceDetail);
                List<PostDeliveryBatchSerial> XMLBatchData = JsonConvert.DeserializeObject<List<PostDeliveryBatchSerial>>(inputParams.InvoiceBatchDetail);
                List<PriceRequestItemData> ItemDetail = JsonConvert.DeserializeObject<List<PriceRequestItemData>>(inputParams.ItemDetail);

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

                DataTable dtPriceUpdateReq = new DataTable();
                dtPriceUpdateReq.Columns.Add("DeliveryHeaderID", typeof(string));
                dtPriceUpdateReq.Columns.Add("ItemID", typeof(string));
                dtPriceUpdateReq.Columns.Add("SellingPrice", typeof(string));
                dtPriceUpdateReq.Columns.Add("RequestedPrice", typeof(string));
                dtPriceUpdateReq.Columns.Add("LineNumber", typeof(string));
                dtPriceUpdateReq.Columns.Add("Reason", typeof(string));

                foreach (var header in XMLHeaderData)
                {
                    dtInvoiceHeader.Rows.Add(header.dsp_ID, header.CreatedBy, header.CreatedDate, header.GeoCode, header.GeoCodeName, header.remarks,
                        header.dln_cse_ID, header.CreationMode, header.dln_udp_ID, header.dln_Signature, header.inv_SubTotal, header.inv_VAT, header.inv_TotalAmount,
                        header.inv_ReceivedAmount, header.inv_TotalPaidAmount, header.inv_PayType, header.dln_IsPartiallyDelivered, header.FreeGoodAmount,
                        header.HeaderDiscount, header.inv_SubTotal_WO_Discount, header.AppDeliveryID, header.rot_ID, header.paymode, header.Receiptimg,
                        header.inv_InvoiceID, header.dln_ID, header.Type, header.CustomerID, header.CustomeHeaderID, header.PriceUpdateRequested, 
                        header.IsCashCollected, header.HelperRoute);
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

                foreach (var price in ItemDetail)
                {
                    dtPriceUpdateReq.Rows.Add(price.DeliveryHeaderID, price.ItemId, price.SellingPrice, price.RequestedPrice, price.LineNumber , price.Reason);
                }

                dsInvoiceDetail.Tables.Add(dtInvoiceHeader);
                dsInvoiceDetail.Tables.Add(dtInvoiceDetail);
                dsInvoiceDetail.Tables.Add(dtInvoiceBatchSetial);
                dsInvoiceDetail.Tables.Add(dtPriceUpdateReq);

                try
                {
                    string[] keys = { };
                    string[] values = { };
                    string[] arr = { "@InvoiceHeader", "@InvoiceDetail", "@InvoiceBatchDetail", "@PriceListDetail" };
                    DataSet Value = dm.bulkUpdate(dsInvoiceDetail, arr, keys, values, "sp_PriceUpdateRequest");
                    DataTable dtPriceUpdate = Value.Tables[0];
                    if (dtPriceUpdate.Rows.Count > 0)
                    {
                        List<PriceRequestOut> listTransout = new List<PriceRequestOut>();
                        foreach (DataRow dr in dtPriceUpdate.Rows)
                        {
                            listTransout.Add(new PriceRequestOut
                            {
                                Res = dr["Res"].ToString(),
                                Title = dr["Title"].ToString(),
                                Descr = dr["Descr"].ToString()


                            });
                        }
                        JSONString = JsonConvert.SerializeObject(new
                        {
                            result = listTransout
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
            dm.TraceService("PriceUpdateRequest ENDED " + DateTime.Now.ToString());
            dm.TraceService("========================================");
            return JSONString;
        }

    }
}