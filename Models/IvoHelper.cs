using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class IvoHelper
    {

    }
    public class PostODIHeader
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
        public string XMLPackageData { get; set; }
    }
    public class PostODIDetail
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
    public class PostODIBatchSerial
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
    public class PostODIPackage
    {
        public string itmID { get; set; }
        public string Qty { get; set; }
    }
    public class PostTransactionalAttachment
    {
        public string AttachmentPath { get; set; }
        public string AttachmentType { get; set; } 
    }

    public class GetInvoiceDetail
    {
        public string InvID { get; set; }
        public string IndID { get; set; }
        public string DlnID { get; set; }
        public string PrdID { get; set; }
        public string PrdCode { get; set; }
        public string PrdName { get; set; }
        public string IndHigherUOM { get; set; }
        public string IndHigherQty { get; set; }
        public string IndLowerUOM { get; set; }
        public string IndLowerQty { get; set; }
        public string IndHigherPrice { get; set; }
        public string IndLowerPrice { get; set; }
        public string IndLineNo { get; set; }
        public string IndTransType { get; set; }
        public string IndLineTotal { get; set; }
        public string IndPrice { get; set; }
        public string IndDiscount { get; set; }
        public string IndDiscountPrec { get; set; }
        public string IndGrandTotal { get; set; }
        public string IndLineVAT { get; set; }
        public string IndPieceDiscount { get; set; }
        public string IndSubTotalWODiscount { get; set; }
        public string IndTotalQty { get; set; }
        public string IndVAT { get; set; }
        public string IndVATPerc { get; set; }
        public string PrdDesc { get; set; }
        public string PrdSpec { get; set; }
    }
    public class GetApplicableDeliveryNotes
    {
        public string DlnID { get; set; }
        public string InvID { get; set; }
    }
    public class GetInvoiceHeader
    {
        public string InvID { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; } 
        public string InvoiceTime { get; set; }
        public string CshCode { get; set; }
        public string CshName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string CshPhone { get; set; }
        public string CshTRN { get; set; }
        public string CshPatID { get; set; }
        public string CshCurrency { get; set; }
        public string SubTotalWODiscount { get; set; }
        public string Discount { get; set; }
        public string SubTotal { get; set; }
        public string VAT { get; set; }
        public string TotalAmount { get; set; } 
        public string DepartmentID { get; set; }
        public string PayType { get; set; }
        public string PayMode { get; set; }
        public List<GetApplicableDeliveryNotes> ApplicableDeliveryNotes { get; set; }
        public List<GetInvoiceDetail> InvoiceDetail { get; set; }
    }
    public class PostMDIHeader
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
    public class PostMDIDetail
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
    public class PostMDIBatchSerial
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
    public class PostMDIPackage
    {
        public string itmID { get; set; }
        public string Qty { get; set; }
        public string InvoiceID { get; set; }
        public string udp_ID { get; set; }
        public string rot_ID { get; set; }
        public string CustomerHeaderID { get; set; }
        public string CustomerID { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
    public class PostMDIInputData
    {
        public string InvoiceHeader { get; set; }
        public string InvoiceDetail { get; set; }
        public string InvoiceBatchDetail { get; set; }
        public string InvoicePackageDetail { get; set; }
    }
    public class PostAttachments
    {
        public string Mode { get; set; }
        public string InvoiceID { get; set; }
        public string UserID { get; set; }
        public string AttachType { get; set; }
    }

    public class PostDNImg
    {
        public string dln_ID { get; set; }
        public string AttachType { get; set; }

    }
    public class PostVoid
    {
        public string VoidDeliveryNote { get; set; }
    }
    public class PostVoidDetail
    {
        public string dlnID { get; set; }
        public string VoidMode { get; set; }
        public string VoidUser { get; set; }
        public string VoidDate { get; set; }
        public string VoidTime { get; set; }
        public string VoidPlatform { get; set; }
        public string VoidStatus { get; set; }
    }
    public class GetDeliNoteInsertStatus
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
    }
    public class GetInsertAttachmentStatus
    {
        public string Mode { get; set; }
        public string Status { get; set; }
    }

    public class DeliveryCSEIDIn
    {
        public string JsonString { get; set; }
        public string UserID { get; set; }
        public string CseID { get; set; }

    }
    public class DeliveryCSEIDOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }

    }
    public class DelCseIds
    {
        public string dln_ID { get; set; }
        
    }
    public class GetDeliveryInpara
    {
        public string rot_ID { get; set; }
        public string userID { get; set; }

    }
    public class GetDeliveryOutpara
    {
        public string dln_ID { get; set; }
        public string dln_DeliveryNumber { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CusHeaderCode { get; set; }
        public string CusHeaderName { get; set; }
        public string Type { get; set; }
        public string Department { get; set; }
        public string TransType { get; set; }
        public string CustomerID { get; set; }
        public string CusHeaderID { get; set; }
    }
    public class PostStampedBackndIn
    {
        public string dln_ID { get; set; }
       

    }
    public class GetInvoiceInfoInpara
    {
        public string inv_ID { get; set; }

    }
    public class GetInvoiceOutpara
    {
        public string inv_InvoiceID { get; set; }
        public string inv_ID { get; set; }
        public string csh_Code { get; set; }
        public string csh_Name { get; set; }
        public string csh_Address_1 { get; set; }
        public string csh_Address_2 { get; set; }
        public string csh_Address_3 { get; set; }
        public string csh_Phone { get; set; }
        public string csh_TRN { get; set; }
        public string pat_Description { get; set; }
        public string csh_Currency { get; set; }
        public string OutletName { get; set; }
        public string inv_SubTotal_WO_Discount { get; set; }
        public string inv_Discount { get; set; }
        public string inv_SubTotal { get; set; }
        public string inv_VAT { get; set; }
        public string inv_TotalAmount { get; set; }
        public string createdOn { get; set; }
        public string BatchInvoice { get; set; }
        public string usr_Name { get; set; }
        public string csh_CostCentre { get; set; }
        public string csh_BillToCustomer { get; set; }
        public string csh_FAX { get; set; }
       
    }
    public class InvDetailOut
    {
        public string Barcode { get; set; }
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string uom { get; set; }
        public string ind_HigherQty { get; set; }
        public string UnitPrice { get; set; }
        public string ind_VATPerc { get; set; }
        public string ind_VAT { get; set; }
        public string ind_GrandTotal { get; set; }
        public string Discount { get; set; }
        public string ind_dln_ID { get; set; }
       
    }
    public class DeliveryInfoOut
    {
        public string dln_DeliveryNumber { get; set; }
        public string dln_ID { get; set; }
       
    }
    public class BatchInfoOut
    {      
        public string Batch { get; set; }
        public string dns_dln_ID { get; set; }
        public string dld_prd_ID { get; set; }

    }
    public class TDInfoOut
    {
        public string prd_Code { get; set; }
        public string uom { get; set; }
        public string ind_dln_ID { get; set; }
        public string prd_ID { get; set; }
        public string TDInfo { get; set; }
    }
    public class InvoicePrintOut
    {
        public List<GetInvoiceOutpara> InvoiceHeader { get; set; }
        public List<InvDetailOut> InvoiceDetail{ get; set; }
        public List<DeliveryInfoOut> DeliveryInfo { get; set; }
        public List<BatchInfoOut> BatchInfo { get; set; }
        public List<TDInfoOut> TDInfo { get; set; }
    }

}