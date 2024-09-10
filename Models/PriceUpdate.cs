using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class PriceUpdate
    {
    }
    public class PriceUpdateRequestIn
    {
        public string InvoiceHeader { get; set; }
        public string InvoiceDetail { get; set; }
        public string InvoiceBatchDetail { get; set; }
        public string ItemDetail { get; set; }
}
public class PriceRequestItemData
    {
        public string DeliveryHeaderID { get; set; }
        public string ItemId { get; set; }
        public string SellingPrice { get; set; }
        public string RequestedPrice { get; set; }
        public string LineNumber { get; set; }
        public string Reason { get; set; }
    }
    public class PriceRequestOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
    }
    public class PostDeliveryHeader
    {
        public string dsp_ID { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string GeoCode { get; set; }
        public string GeoCodeName { get; set; }
        public string remarks { get; set; }
        public string jsonValue { get; set; }
        public string dln_cse_ID { get; set; }
        public string CreationMode { get; set; }
        public string dln_udp_ID { get; set; }
        public string dln_Signature { get; set; }
        public string inv_SubTotal { get; set; }
        public string inv_VAT { get; set; }
        public string inv_TotalAmount { get; set; }
        public string inv_ReceivedAmount { get; set; }
        public string inv_TotalPaidAmount { get; set; }
        public string inv_PayType { get; set; }
        public string dln_IsPartiallyDelivered { get; set; }
        public string FreeGoodAmount { get; set; }
        public string HeaderDiscount { get; set; }
        public string XMLdataQlftn { get; set; }
        public string XMLdataAssgn { get; set; }
        public string inv_SubTotal_WO_Discount { get; set; }
        public string AppDeliveryID { get; set; }
        public string rot_ID { get; set; }
        public string paymode { get; set; }
        public string Receiptimg { get; set; }
        public string inv_InvoiceID { get; set; }
        public string dln_ID { get; set; }
        public string XMLBatchData { get; set; }
        public string Type { get; set; }
        public string XMLTransactionalAttachment { get; set; }
        public string CustomerID { get; set; }
        public string CustomeHeaderID { get; set; }
        public string PriceUpdateRequested { get; set; }
        public string IsCashCollected { get; set; }
        public string HelperRoute { get; set; }
    }
    public class PostDeliveryDetail
    {
        public string dlnID { get; set; }
        public string itmID { get; set; }
        public string LIHUom { get; set; }
        public string LILUom { get; set; }
        public string LIHQty { get; set; }
        public string LILQty { get; set; }
        public string FHQty { get; set; }
        public string FHUom { get; set; }
        public string FLQty { get; set; }
        public string FLUom { get; set; }
        public string AdjHQty { get; set; }
        public string AdjHUom { get; set; }
        public string AdjLQty { get; set; }
        public string AdjLUom { get; set; }
        public string ind_Price { get; set; }
        public string dld_TransType { get; set; }
        public string dld_ReturnHigherPrice { get; set; }
        public string dld_ReturnLowerPrice { get; set; }
        public string PriceUpdateRequested { get; set; }
        public string VAT { get; set; }
        public string InvoicePrice { get; set; }
        public string PieceDiscount { get; set; }
        public string GrandTotal { get; set; }
        public string Discount { get; set; }
        public string SubTotalWODiscount { get; set; }
        public string InvoiceLineNo { get; set; }
        public string DiscountPerc { get; set; }
        public string HigherPrice { get; set; }
        public string LowerPrice { get; set; }
        public string LossInTransit { get; set; }
        public string ReasonCode { get; set; }
        public string VATPerc { get; set; }
    }
    public class PostDeliveryBatchSerial
    {
        public string dlnID { get; set; }
        public string dldID { get; set; }
        public string dnsNumber { get; set; }
        public string ExpDate { get; set; }
        public string baseUOM { get; set; }
        public string OrdQty { get; set; }
        public string AdjQty { get; set; }
        public string LIQty { get; set; }
        public string Status { get; set; }
        public string itmID { get; set; }
        public string LossInTransit { get; set; }
    }
    
}