using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class VanHelper
    {
        
    }
    public class VanToVanTransIn
    {
        public string rotId { get; set; }

    }
    public class VanParams
    {
        public string TransID { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string Rot { get; set; }
        public string HeaderID { get; set; }
        public string RotID { get; set; }
    }
    public class TransferOut
    {
        public string UdpID { get; set; }
        public string TransID { get; set; }
        public string FromRot { get; set; }
        public string ToRot { get; set; }

        public string CreatedDate { get; set; }
        public string userID { get; set; }
        public string Dispatches { get; set; }
    }

    public class DispatchIDs
    {
        public string dsp_ID { get; set; }
    }
    public class TransferOutOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }

    }
    public class TransInUpdateStatus
    {
        public string Dispatch { get; set; }
        public string HeaderID { get; set; }
        public string Status { get; set; }
       
        public string DateTime { get; set; }


    }
    public class TransOutUpdateStatus
    {
        public string Dispatch { get; set; }
        public string TransID { get; set; }
        public string Status { get; set; }
      
        public string DateTime { get; set; }


    }

    public class PostTransferInData
    {
        
        public string VanHeaderId { get; set; }
    }
    public class VanListInfo
    {
        public int DespatchId { get; set; }
        public string VanHeaderID { get; set; }

        public string OrderId { get; set; }
        public string PickingLocation { get; set; }
        public string CusHeaderCode { get; set; }
        public string CusHeaderName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string dsp_DispatchID { get; set; }

        public List<GetTransferInItemHeader> DespatchItems { get; set; }
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
    public class GetTransferInItemHeader
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
        public List<GetTransferInBatchSerial> BatchSerial { get; set; }
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

    public class GetTransferInBatchSerial
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
    public class TransCheckStatus
    {

        public string TransID{ get; set; }
    }
    public class TransCheckStatusOut
    {

        public string Status { get; set; }
    }
    
    public class TransferInCompletion
    {
        public string VanHeaderID { get; set; }
        public string FromRotID { get; set; }
        public string ToRotID { get; set; }
        public string Dispatches { get; set; }
    }
    public class TransferInCompletionOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }

    }

}