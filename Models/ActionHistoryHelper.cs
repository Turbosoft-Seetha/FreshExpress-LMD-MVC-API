using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class ActionHistoryHelper
    {
       
    }

    public class DeliveryHeaderIn
    {
        public string rot_ID { get; set; }
        public string userID { get; set; }


    }
    public class PostDeliveryHeaderOut
    {
        public string dln_ID { get; set; }
        public string dln_DeliveryNumber { get; set; }
        public string OrderId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string dsp_DispatchID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CusHeaderID { get; set; }
        public string CusHeaderCode { get; set; }
        public string CusHeaderName { get; set; }
        public string IsPartiallyDelivered { get; set; }
        public string SubTotal { get; set; }
        public string VAT { get; set; }
        public string TotalAmount { get; set; }
        public string ERP_OrderNo { get; set; }

    }
    public class DeliveryDetailIn
    {
        public string dln_ID { get; set; }

    }
    public class PostDeliveryDetailOut
    {
        public string prd_ID { get; set; }
        public string prd_Name { get; set; }
        public string prd_Code { get; set; }
        public string Spec { get; set; }
        public string prd_Desc { get; set; }
        public string prd_LongDesc { get; set; }
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
        public string WeighingItem { get; set; }
        public string SysHUOM { get; set; }
        public string SysLUOM { get; set; }
        public string SysHQty { get; set; }
        public string SysLQty { get; set; }
        public string AdjHUOM { get; set; }
        public string AdjLUOM { get; set; }
        public string AdjHQty { get; set; }
        public string AdjLQty { get; set; }
        public string LiHUOM { get; set; }
        public string LiLUOM { get; set; }
        public string LiHQty { get; set; }
        public string LiLQty { get; set; }
        public string Type { get; set; }
        public string LineNo { get; set; }
        public string Price { get; set; }
        public string LineTotal { get; set; }
        public List<GetDelItemBatchSerial> BatchSerial { get; set; }

    }

    public class GetDelItemBatchSerial
    {
        public string ItemCode { get; set; }
        public string Number { get; set; }
        public string ExpiryDate { get; set; }
        public string BaseUOM { get; set; }
        public string OrderedQty { get; set; }
        public string LoadInQty { get; set; }
        public string AdjustedQty { get; set; }
        public string BatSerialId { get; set; }
        public string DetailId { get; set; }
        public string LineNo { get; set; }
        public string ItemId { get; set; }
    }
    public class PostHelperRotIn
    {
        public string rotId { get; set; }

    }
    public class PostHelperHeader
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string MainRot { get; set; }
        public string HeaderID { get; set; }

    }
    public class PostHelperRotDetailIn
    {
        public string HeaderId { get; set; }

    }
    public class PostHelperDetail
    {
        public string HeaderID { get; set; }
        public string DespatchId { get; set; }
        public string PickingLocation { get; set; }
        public string CusHeaderCode { get; set; }
        public string CusHeaderName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string dsp_DispatchID { get; set; }
        public string CustomerID { get; set; }
        public string CusHeaderID { get; set; }
        public string PickLocationID { get; set; }
        public string Status { get; set; }
        public string IsPartiallyDelivered { get; set; }
        public string dln_ID { get; set; }
        public string dln_DeliveryNumber { get; set; }
    }

    public class PostMainHeaderOut
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string HelperRot { get; set; }
        public string HeaderID { get; set; }

    }
    public class DeliveryCustomerHeaderIn
    {
        public string rot_ID { get; set; }
        public string cus_ID { get; set; }
        
        public string userID { get; set; }


    }
    public class PostCustomerDeliveryHeaderOut
    {
        public string dln_ID { get; set; }
        public string dln_DeliveryNumber { get; set; }
        public string OrderId { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string dsp_DispatchID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CusHeaderID { get; set; }
        public string CusHeaderCode { get; set; }
        public string CusHeaderName { get; set; }
        public string IsPartiallyDelivered { get; set; }
        public string SubTotal { get; set; }
        public string VAT { get; set; }
        public string TotalAmount { get; set; }
        public string ERP_OrderNo { get; set; }

    }
    public class MainRouteHead
    {
        public string rotId { get; set; }


    }
    public class MainRoutOut
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string Rot { get; set; }
        public string HeaderID { get; set; }
       
    }
    public class InvoiceHeaderIn
    {
        public string udp_ID { get; set; }
        public string userID { get; set; }
      
    }
    
    public class PostInvoiceHeaderOut
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
        public string cus_ID { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
    }
    public class InvoiceDetailIn
    {
        public string InvID { get; set; }
      

    }
    public class PostInvoiceDetailOut
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

    public class ReturnHeaderIn
    {
        public string RotID { get; set; }
        public string userID { get; set; }

    }
    public class ReturnHeaderOut
    {
        public string rrh_ID { get; set; }
        public string rrh_RequestNumber { get; set; }
        public string inv_InvoiceID { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string csh_ID { get; set; }
        public string csh_Code { get; set; }
        public string csh_Name { get; set; }
        public string cus_ID { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }

    }
    public class CusReturnHeaderIn
    {
        public string RotID { get; set; }
        public string cusID { get; set; }       
        public string userID { get; set; }

    }
    public class ReturnDetailIn
    {
        public string rrh_ID { get; set; }

    }
    public class ReturnDetailOut
    {
        public string rrd_ID { get; set; }
        public string rrd_HUOM { get; set; }
        public string rrd_HQty { get; set; }
        public string rrd_LUOM { get; set; }
        public string rrd_LQty { get; set; }
        public string ReasonCode { get; set; }
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string prd_Spec { get; set; }
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
        public string Return_HQty { get; set; }
        public string Return_HUOM { get; set; }
        public string Return_LQty { get; set; }
        public string Return_LUOM { get; set; }
        public List<GetReturnItemBatchSerial> BatchSerial { get; set; }

    }
    public class GetReturnItemBatchSerial
    {
        public string RtnBatSerialId { get; set; }
        public string RtnDetailId { get; set; }
        public string ItemCode { get; set; }
        public string Number { get; set; }
        public string ExpiryDate { get; set; }
        public string BaseUOM { get; set; }
        public string RtnQty { get; set; }

    }

    public class PostReturnHelperRotIn
    {
        public string rotId { get; set; }
    }
    public class PostReturnHelperHeader
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string MainRot { get; set; }
        public string HeaderID { get; set; }

    }
    public class PostReturnHelperRotDetailIn
    {
        public string HeaderId { get; set; }

    }
    public class PostReturnHelperDetail
    {
        public string HeaderID { get; set; }
        public string ReturnRequestId { get; set; }
        public string rrh_RequestNumber { get; set; }
        public string CusHeaderCode { get; set; }
        public string CusHeaderName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerID { get; set; }
        public string CusHeaderID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string inv_InvoiceID { get; set; }
        public string Status { get; set; }
    }
    public class PostReturnMainHeaderOut
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string HelperRot { get; set; }
        public string HeaderID { get; set; }

    }
    public class ReturnMainRouteHead
    {
        public string rotId { get; set; }


    }
    public class ReturnMainRoutOut
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string Rot { get; set; }
        public string HeaderID { get; set; }

    }
    public class PostStyrofoamInvIn
    {
        public string udpID { get; set; }
        public string cusID { get; set; }
        public string UserId { get; set; }      
    }   
    public class PostStyrofoamInvOut
    {
        public string psh_ID { get; set; }
        public string psh_Number { get; set; }
        public string psh_inv_ID { get; set; }
        public string inv_InvoiceID { get; set; }
        public string psh_udp_ID { get; set; }
        public string rot_ID { get; set; }
        public string rot_Code { get; set; }
        public string rot_Name { get; set; }
        public string CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string OutletID { get; set; }
        public string OutletCode { get; set; }
        public string OutletName { get; set; }
        public string CreatedDate { get; set; }
        public List<PostStyrofoamInvDetailOut> StyrofoamDetail { get; set; }
    }
    public class PostStyrofoamInvDetailOut
    {
        public string psd_ID { get; set; }
        public string psd_psh_ID { get; set; }
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string psd_Qty { get; set; }
    }
}