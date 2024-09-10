using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class DelHelper
    {

    }

    public class PostDispatchData
    {
        public string DispatchId { get; set; }
        public string UserId { get; set; }
    }

    public class GetDispatchItemHeader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Spec { get; set; }
        public string Warehouse { get; set; }
        public string Rack { get; set; }
        public string Basket { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
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
        public string PromoType { get; set; }
        public string WeighingItem { get; set; }
        public string LineNo { get; set; }
        public List<GetDispatchBatchSerial> BatchSerial { get; set; }
        public string prd_Desc { get; set; }
        public string prd_LongDesc { get; set; }
        public string prd_cat_id { get; set; }
        public string prd_sub_ID { get; set; }
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

        public string Reason { get; set; }
    }

    public class GetDispatchBatchSerial
    {
        public string DispBatSerialId { get; set; }
        public string DisDetailId { get; set; }
        public string ItemCode { get; set; }
        public string Number { get; set; }
        public string ExpiryDate { get; set; }
        public string BaseUOM { get; set; }
        public string OrderedQty { get; set; }
        public string AdjustedQty { get; set; }
        public string LoadInQty { get; set; }
        public string LineNo { get; set; }
    }

    public class PostRejectData
    {
        public string DispatchId { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string ReasonId { get; set; }
    }

    public class GetInsertStatus
    {
        public string Mode { get; set; }
        public string Status { get; set; }
        public string DeliveryNumber { get; set; }
        public string dln_ID { get; set; }

    }

    public class PostParkData
    {
        public string DispatchId { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string JSONString { get; set; }
        public string BatchData { get; set; }
    }

    public class PostDispatchItemData
    {
        public string ItemId { get; set; }
        public string AdjustedHigherUOM { get; set; }
        public string AdjustedHigherQty { get; set; }
        public string AdjustedLowerUOM { get; set; }
        public string AdjustedLowerQty { get; set; }
        public string LoadInHigherUOM { get; set; }
        public string LoadInHigherQty { get; set; }
        public string LoadInLowerUOM { get; set; }
        public string LoadInLowerQty { get; set; }
        public string LineNumber { get; set; }
        public string Reason { get; set; }
        public List<PostDispatchBatchSerial> ItemBatchInfo { get; set; }

    }

    public class PostCompleteData
    {
        public string DispatchId { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
        public string Remark { get; set; }
        public string JSONString { get; set; }
        public string BatchData { get; set; }
        public Bitmap Signature { get; set; }
    }

    public class PostDispatchBatchSerial
    {
        public string DispBatSerialId { get; set; }
        public string DisDetailId { get; set; }
        public string Number { get; set; }
        public string AdjustedQty { get; set; }
        public string LoadInQty { get; set; }
    }

    //Delivery

    public class PostDeliveryData
    {
        public string DispatchId { get; set; }
        public string UserId { get; set; }
        public string FinalAmount { get; set; }
        public string Status { get; set; }
        public string JSONString { get; set; }
    }

    public class PostDeliveryItemData
    {
        public string ItemId { get; set; }
        public string HigherUOM { get; set; }
        public string HigherQty { get; set; }
        public string LowerUOM { get; set; }
        public string LowerQty { get; set; }
        public string ReasonId { get; set; }
        public string LineNo { get; set; }
    }

    public class GetDeliveryInsertStatus
    {
        public string Mode { get; set; }
        public string Status { get; set; }
    }

    public class PostDeliveryApprovalData
    {
        public string UserId { get; set; }
    }

    public class GetDeliveryApprovalHeader
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string DepartmentId { get; set; }
        public string LPONumber { get; set; }
        public string Status { get; set; }
        public string ApprovalStatus { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string FinalAmount { get; set; }
        public List<GetDeliveryApprovalDetail> ApprovalDetail { get; set; }
    }

    public class GetDeliveryApprovalDetail
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string ItemSpec { get; set; }
        public string ItemPrice { get; set; }
        public string HigherUOM { get; set; }
        public string HigherQty { get; set; }
        public string LowerUOM { get; set; }
        public string LowerQty { get; set; }
        public string UserReasonId { get; set; }
        public string ApproverReasonId { get; set; }
        public string LineNo { get; set; }
    }

    public class PostApprovalStatusData
    {
        public string DispatchId { get; set; }
        public string UserId { get; set; }

    }
    public class PostApprovalHeaderStatusData
    {
        public string DispatchId { get; set; }
        public string UserId { get; set; }
    }

    public class GetDeliveryApprovalStatus
    {
        public string ApprovalReason { get; set; }
        public string ApprovalStatus { get; set; }

        public string Products { get; set; }
        public string IsLossInTransit { get; set; }
        public string ReasonCode { get; set; }
        public string LineNo { get; set; }

    }
    public class GetDeliveryApprovalHeaderStatus
    {
        public string ApprovalReason { get; set; }
        public string ApprovalStatus { get; set; }
    }

    public class DeliveryVerificationIn
    {
        public string DispatchId { get; set; }
        public string PickerName { get; set; }
        public string PickerMobileNumber { get; set; }
        public string UserId { get; set; }
        public string CreatedDate { get; set; }
        public string InvPrintName { get; set; }

    }
    public class DeliveryVerificationOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Desr { get; set; }

    }
    public class DeliveryVerifyOTPIn
    {
        public string OTP { get; set; }
        public string DispatchId { get; set; }
        public string PickerMobileNumber { get; set; }


    }
    public class DeliveryVerifyOTPOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Desr { get; set; }

    }
    public class DeliveryPaymentStatusIn
    {
        public string ordNumber { get; set; }


    }
    public class DeliveryPaymentStatusOut
    {
        public string PaymentStatus { get; set; }


    }
    public class OutletsIn
    {
        public string rot_ID { get; set; }
        public string Cus_IDs { get; set; }
    }
    public class OutletsOut
    {

        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }


    }
    public class GetLoadInHeader
    {

        public string dsp_DispatchID { get; set; }
        public string OrderID { get; set; }
        public string dsp_ID { get; set; }
        public string cus_ID { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
        public string cus_ShortName { get; set; }
        public string ArabicCusName { get; set; }
        public string ArabicDeliveryType { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public string NeedDownload { get; set; }
        public string dsp_DeliveryType { get; set; }
        public string DeliveyType { get; set; }
        public string ord_PayMode { get; set; }
        public string dsp_LOStatus { get; set; }
        public string TotalAmount { get; set; }
        public string PickLocation { get; set; }
        public string ord_LPONumber { get; set; }
        public string IsPickupOrder { get; set; }
        public string ord_dep_ID { get; set; }
        public string ord_CashPaidStatus { get; set; }
        public string Customer { get; set; }
        public string Outlet { get; set; }
        public string OutletID { get; set; }
        public string PickLocationID { get; set; }
        public string VanToVan { get; set; }
        public List<GetDispatchItemHeader> LoadInDetail { get; set; }
        public string dln_DeliveryNumber { get; set; }
        public string dln_ID { get; set; }
        public string dsp_IsPartialLoad { get; set; }
        public string Type { get; set; }
        public string cus_Type { get; set; }
        public string IsHelper { get; set; }
        public string OrderReleasedBy { get; set; }
        public string csh_InvocieLocation { get; set; }

    }
    public class PostLoadInData
    {
        public string rot_ID { get; set; }

    }


    public class PostNotDispatchData
    {
        public string OrdId { get; set; }
        public string UserId { get; set; }
    }

    public class GetNotDispatchItemHeader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Spec { get; set; }
        public string Warehouse { get; set; }
        public string Rack { get; set; }
        public string Basket { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
        public string SysHUOM { get; set; }
        public string SysLUOM { get; set; }
        public string SysHQty { get; set; }
        public string SysLQty { get; set; }

        public string PromoType { get; set; }
        public string WeighingItem { get; set; }
        public string LineNo { get; set; }
        public List<GetNotDispatchBatchSerial> BatchSerial { get; set; }
        public string prd_Desc { get; set; }
        public string prd_LongDesc { get; set; }
        public string prd_cat_id { get; set; }
        public string prd_sub_ID { get; set; }
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

    public class GetNotDispatchBatchSerial
    {
        public string OrdBatSerialId { get; set; }
        public string OrdDetailId { get; set; }
        public string ItemCode { get; set; }
        public string Number { get; set; }
        public string ExpiryDate { get; set; }
        public string BaseUOM { get; set; }
        public string OrderedQty { get; set; }

        public string LineNo { get; set; }
    }
    public class PicklocationIn
    {
        public string rot_ID { get; set; }

    }

    public class PicklocationOut
    {
        public string PickLocID { get; set; }
        public string PickLocCode { get; set; }
        public string PickLocName { get; set; }

    }
    public class PrintIn
    {
        public string rot_ID { get; set; }
        public string JsonString { get; set; }
        public string user_ID { get; set; }
        public string udp_ID { get; set; }
    }

    public class PrintOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }

    }
    public class PrintLoadIn
    {

        public string dsp_ID { get; set; }

    }
    public class DeliveryOTP
    {
        public string MobNumber { get; set; }
        public string Message { get; set; }


    }
    public class DeliveryItemCountIn
    {
        public string dln_ID { get; set; }

    }
    public class DeliveryItemCountOut
    {
        public string DeliveryItemCount { get; set; }
        public string IsPickupOrder { get; set; }

    }
    public class DeliveryItemsIn
    {
        public string dln_ID { get; set; }
        public string UserId { get; set; }

    }
    public class DeliveryItemsOut
    {
        public int prd_ID { get; set; }
        public string prd_Name { get; set; }
        public string prd_Code { get; set; }
        public string prd_Spec { get; set; }
        public string Warehouse { get; set; }
        public string Rack { get; set; }
        public string Basket { get; set; }
        public string LocationId { get; set; }
        public string LocationName { get; set; }
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
        public string PromoType { get; set; }
        public string WeighingItem { get; set; }
        public string LineNo { get; set; }
        public string prd_Desc { get; set; }
        public string prd_LongDesc { get; set; }
        public string prd_cat_id { get; set; }
        public string prd_sub_ID { get; set; }
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
        public string Reason { get; set; }
        public List<DeliveryItemBatchSerial> BatchSerial { get; set; }
    }
    public class DeliveryItemBatchSerial
    {
     public string DelBatSerialId { get; set; }
    public string DelDetailId { get; set; }
    public string ItemCode { get; set; }
    public string Number { get; set; }
    public string ExpiryDate { get; set; }
    public string BaseUOM { get; set; }
    public string OrderedQty { get; set; }
    public string AdjustedQty { get; set; }
    public string LoadInQty { get; set; }
    public string LineNo { get; set; }
}

    public class DeliverySPIn
    {
        public string dln_ID { get; set; }

    }
    public class DeliverySPOut
    {
        public string SalesPersonCode { get; set; }
        public string SalesPersonName { get; set; }
        public string SalesPersonPhone { get; set; }
        public string OrderReleasedBy { get; set; }

    }
}