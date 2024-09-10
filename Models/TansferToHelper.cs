using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class TansferToHelper
    {
    }
    public class HelperIN
    {
        public string rotId { get; set; }

    }
    public class TransferToOut
    {   
        public string rot_ID { get; set; }
        public string rot_Code { get; set; }
        public string rot_Name { get; set; }
        public string IsHelperRoute { get; set; }
        public string ParentRoute { get; set; }

    }
    public class HelperRouteHead
    {
        public string rotId { get; set; }


    }
    public class HelperRotUpdateStatus
    {
        public string Dispatch { get; set; }
        public string HeaderID { get; set; }
        public string Status { get; set; }

        public string DateTime { get; set; }


    }
    public class DispatchID
    {
        public string dsp_ID { get; set; }
    }
    public class HelperOut
    {
        public string UdpID { get; set; }
        public string TransID { get; set; }
        public string MainRot { get; set; }
        public string HelperRot { get; set; }

        public string CreatedDate { get; set; }
        public string userID { get; set; }
        public string Dispatches { get; set; }
    }

    public class DispatchIds
    {
        public string dsp_ID { get; set; }
    }
    public class TransferHelperOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }

    }


    public class PostTransferHelperData
    {

        public string HelperHeaderId { get; set; }
    }
    public class TransferHelperListInfo
    {
        public int DespatchId { get; set; }
        public string HelperHeaderId { get; set; }

        public string OrderId { get; set; }
        public string PickingLocation { get; set; }
        public string CusHeaderCode { get; set; }
        public string CusHeaderName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string dsp_DispatchID { get; set; }

        public List<GetTransferHelperItemHeader> DespatchItems { get; set; }
       
        public string IsPickupOrder { get; set; }
       
        public string ord_dep_ID { get; set; }
        public string ord_CashPaidStatus { get; set; }
        public string ord_PayMode { get; set; }
        public string ord_LPONumber { get; set; }
        public string TotalAmount { get; set; }
        public string dsp_LOStatus { get; set; }
        public string cus_ArabicName { get; set; }
        public string dsp_ArabicDeliveryType { get; set; }
        public string DeliveyType { get; set; }
        public string dsp_DeliveryType { get; set; }
        public string Status { get; set; }
        public string NeedDownload { get; set; }
        public string CustomerID { get; set; }
        public string CusHeaderID { get; set; }
        public string PickLocationID { get; set; }
        public string dln_DeliveryNumber { get; set; }
        public string dln_ID { get; set; }
        public string Type { get; set; }
        public string cus_Type { get; set; }
    }
    public class GetTransferHelperItemHeader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Spec { get; set; }
        public string WarehouseId { get; set; }
        public string Warehouse { get; set; }
        public string RackId { get; set; }
        public string Rack { get; set; }
        public string BasketId { get; set; }
        public string Basket { get; set; }
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }

        public string HigherUOM { get; set; }
        public string LowerUOM { get; set; }
        public string HigherQty { get; set; }
        public string LowerQty { get; set; }

        public string EnableExcess { get; set; }
        public string WeighingItem { get; set; }
        public List<GetTransferHelperBatchSerial> BatchSerial { get; set; }
        public string PromoType { get; set; }
        public string LineNo { get; set; }
        public string prd_Desc { get; set; }
        public string prd_LongDesc { get; set; }
        
        public string prd_brd_ID { get; set; }
        public string prd_EnableOrderHold { get; set; }
        public string prd_EnableReturnHold { get; set; }
        public string prd_EnableDeliveryHold { get; set; }
        public string prd_NameArabic { get; set; }
        public string prd_DescArabic { get; set; }
        public string prd_Image { get; set; }
        public string prd_SortOrder { get; set; }
        public string brd_Code { get; set; }
        public string brd_Name { get; set; }
        public string prd_BaseUOM { get; set; }
        public string VATPercent { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string DiscountPercentage { get; set; }

    }

    public class GetTransferHelperBatchSerial
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

    }
    public class TransMainRotUpdateStatus
    {
       
        public string TransID { get; set; }
        public string Status { get; set; }

        public string DateTime { get; set; }


    }
    public class TransHelperCheckStatus
    {

        public string TransID { get; set; }
    }
    public class TransHelperCheckStatusOut
    {

        public string Status { get; set; }
    }
    public class MainHelperOut
    {
        public string UdpID { get; set; }
        public string MainRot { get; set; }
        public string HelperRot { get; set; }
        public string CreatedDate { get; set; }
        public string userID { get; set; }
        public string Dispatches { get; set; }
        public string TransID { get; set; }
    }
    public class PostTransferMainRot
    {
        public string MainrotId { get; set; }
        
    }
    public class PostMainHeader
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string HelperRot { get; set; }
        public string HeaderID { get; set; }
      
    }
    public class PostTransferMainRotDetail
    {
        public string HeaderId { get; set; }

    }
    public class PostMainDetail
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
    public class PostTransferMainRotIn
    {
        public string HeaderId { get; set; }
        public string userID { get; set; }
        public string ModifiedDate { get; set; }
        
    }
    public class HelperRouteOut
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string Rot { get; set; }
        public string HeaderID { get; set; }
      
    }
    public class HelperRouteReturnHead
    {
        public string rotId { get; set; }


    }
    public class HelperRouteReturnOut
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string Rot { get; set; }
        public string HeaderID { get; set; }

    }
    public class HelperReturnOut
    {
        public string UdpID { get; set; }
        public string TransID { get; set; }
        public string MainRot { get; set; }
        public string HelperRot { get; set; }

        public string CreatedDate { get; set; }
        public string userID { get; set; }
        public string ReturnIds{ get; set; }
    }
    public class Returns
    {
        public string rrh_ID { get; set; }
    }
    public class HelperRotReturnUpdateStatus
    {
        public string ReturnIds { get; set; }
        public string HeaderID { get; set; }
        public string Status { get; set; }

        public string DateTime { get; set; }


    }
    public class ReturnID
    {
        public string rrh_ID { get; set; }
    }
    public class TransMainRotUpdatereturnStatus
    {

        public string TransID { get; set; }
        public string Status { get; set; }

        public string DateTime { get; set; }


    }
    public class PostTransferHelperReturnData
    {
        public string HelperHeaderId { get; set; }
    }
    public class GetTransferHelperReturnItemHeader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Spec { get; set; }
     
        public string CategoryId { get; set; }
        public string SubcategoryId { get; set; }
        public string prd_Desc { get; set; }
        public string prd_LongDesc { get; set; }
        public string prd_brd_ID { get; set; }
        public string prd_EnableOrderHold { get; set; }
        public string prd_EnableReturnHold { get; set; }
        public string prd_EnableDeliveryHold { get; set; }
        public string prd_NameArabic { get; set; }
        public string prd_DescArabic { get; set; }
        public string prd_Image { get; set; }
        public string prd_SortOrder { get; set; }
        public string EnableExcess { get; set; }
        public string WeighingItem { get; set; }
        public string prd_BaseUOM { get; set; }
        public string HigherUOM { get; set; }
        public string LowerUOM { get; set; }
        public string HigherQty { get; set; }
        public string LowerQty { get; set; }

        public List<GetTransferHelperReturnBatchSerial> BatchSerial { get; set; }
      
    }
    public class GetTransferHelperReturnBatchSerial
    {
        public string ind_inv_ID { get; set; }
        public string Number { get; set; }
        public string ExpiryDate { get; set; }
        public string BaseUOM { get; set; }
        public string EligibleQty { get; set; }
        public string prd_ID { get; set; }
        public string ID { get; set; }
        public string BatchSerialId { get; set; }

    }
    public class TransferHelperReturnListInfo
    {
        public int ReturnRequestId { get; set; }
        public string HelperHeaderId { get; set; }
        public string rrh_RequestNumber { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string CusHeaderCode { get; set; }
        public string CusHeaderName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string cus_ArabicName { get; set; }
        public string CustomerID { get; set; }
        public string CusHeaderID { get; set; }
        public string cus_Type { get; set; }
        public string Type { get; set; }
        public List<GetTransferHelperReturnItemHeader> ReturnItems { get; set; }
        public string inv_InvoiceID { get; set; }
    }
    public class MainHelperReturnOut
    {
        public string UdpID { get; set; }
        public string MainRot { get; set; }
        public string HelperRot { get; set; }
        public string CreatedDate { get; set; }
        public string userID { get; set; }
        public string ReturnIds { get; set; }
        public string TransID { get; set; }
    }
    public class PostTransferMainRotReturn
    {
        public string MainrotId { get; set; }

    }
    public class PostMainHeaderReturn
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string HelperRot { get; set; }
        public string HeaderID { get; set; }

    }
    public class PostTransferMainRotReturnDetail
    {
        public string HeaderId { get; set; }

    }
    public class PostMainReturnDetail
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
    public class PostTransferMainRotReturnIn
    {
        public string HeaderId { get; set; }
        public string userID { get; set; }
        public string ModifiedDate { get; set; }

    }
}