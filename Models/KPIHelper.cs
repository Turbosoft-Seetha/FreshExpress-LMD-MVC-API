using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class KPIHelper
    {
    }
    public class KPICountIn
    {
        public string RotID { get; set; }

    }
    public class KPICountOut
    {
        public string TotalDeliveryCount { get; set; }
        public string FDCount { get; set; }
        public string PDCount { get; set; }
        public string NDCount { get; set; }
        public string NPCount { get; set; }
        public string LONDCount { get; set; }
        public string LONPCount { get; set; }
    }
    
    public class KPIDeliveryHeaderIn
    {
        public string rot_ID { get; set; }
        public string userID { get; set; }


    }
    public class KPIDeliveryHeaderOut
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
        public string Status { get; set; }
    }

    public class KPIDeliveryDetailIn
    {
        public string dln_ID { get; set; }

    }
    public class KPIDeliveryDetailOut
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
        public List<GetKPIDelItemBatchSerial> BatchSerial { get; set; }

    }

    public class GetKPIDelItemBatchSerial
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
    
    public class KPIInvCountIn
    {
        public string udpID { get; set; }

    }
    public class KPIInvCountOut
    {
        public string TotalAmount { get; set; }
        public string HCCount { get; set; }
        public string HCAmount { get; set; }
        public string POSCount { get; set; }
        public string POSAmount { get; set; }
        public string OPCount { get; set; }
        public string OPAmount { get; set; }
        public string GCCount { get; set; }
        public string GCAmount { get; set; }
        public string TCCount { get; set; }
        public string TCAmount { get; set; }
        public string APCount { get; set; }
        public string APAmount { get; set; }
    }

    public class KPIInvoiceHeaderIn
    {
        public string udp_ID { get; set; }
        public string userID { get; set; }

    }
    public class KPIPostInvoiceHeaderOut
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

    public class KPIInvoiceDetailIn
    {
        public string InvID { get; set; }


    }
    public class KPIPostInvoiceDetailOut
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
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
    }

    public class KPIReturnCountIn
    {
        public string RotID { get; set; }

    }
    public class KPIReturnCountOut
    {
        public string WithInvoiceCount { get; set; }
        public string WithOutInvoiceCount { get; set; }
      
    }

    public class KPIReturnHeaderIn
    {
        public string RotID { get; set; }
        public string userID { get; set; }
        
    }
    public class KPIReturnHeaderOut
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

    public class KPIReturnDetailIn
    {
        public string rrh_ID { get; set; }
     
    }
    public class KPIReturnDetailOut
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
    }

    public class KPIStyrofoamInvIn
    {
        public string udpID { get; set; }
        public string UserId { get; set; }
    }
    public class KPIStyrofoamInvOut
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
        public List<KPIStyrofoamInvDetailOut> StyrofoamDetail { get; set; }
    }
    public class KPIStyrofoamInvDetailOut
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