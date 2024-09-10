using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class LoadOutHelper
    {
    }
    public class LoadOutInpara
    {
        public string UserId { get; set; }
        public string rotID { get; set; }
        public string udpID { get; set; }

    }
    public class LoadOutOutpara
    {
        public string NDCount { get; set; }
        public string NPCount { get; set; }
        public string PDCount { get; set; }
        public string FDCount { get; set; }
        public string RtnCount { get; set; }

    }
    public class LoadOutFDInpara
    {
        public string UserId { get; set; }
        public string rotID { get; set; }
    }
    public class LoadOutFDOutpara
    {
        public string DispatchNumber { get; set; }
        public string Date { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
        public string dln_ID { get; set; }
        public string ord_ID { get; set; }
        public string cus_ID { get; set; }
    }
    public class LoadOutPDInpara
    {
        public string UserId { get; set; }
        public string rotID { get; set; }
    }
    public class LoadOutPDOutpara
    {
        public string DispatchNumber { get; set; }
        public string Date { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
        public string dln_ID { get; set; }
        public string ord_ID { get; set; }
        public string cus_ID { get; set; }
        public string ReadyToLoadOut { get; set; }
    }
    public class LoadOutNDInpara
    {
        public string UserId { get; set; }
        public string rotID { get; set; }
    }
    public class LoadOutNDOutpara
    {
        public string DispatchNumber { get; set; }
        public string Date { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
        public string dsp_ID { get; set; }
        public string ord_ID { get; set; }

        public string ReadyToLoadOut { get; set; }
        public string cus_ID { get; set; }
    }
    public class LoadOutNPInpara
    {
        public string UserId { get; set; }
        public string rotID { get; set; }
    }
    public class LoadOutNPOutpara
    {
        public string DispatchNumber { get; set; }
        public string Date { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
        public string dsp_ID { get; set; }
        public string ord_ID { get; set; }
        public string ReadyToLoadOut { get; set; }
        public string cus_ID { get; set; }
    }
    public class LoadOutRInpara
    {
        public string UserId { get; set; }
        public string udpID { get; set; }
    }
    public class LoadOutROutpara
    {
        public string rtn_Number { get; set; }
        public string Date { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }

        public string rtn_ID { get; set; }
        public string cus_ID { get; set; }
        public string RequestNumber { get; set; }
        public string ReadyToLoadOut { get; set; }

    }
    public class DeliveryItemBatchInpara
    {
        public string dln_Id { get; set; }
        public string UserId { get; set; }
    }
    public class GetDeliveryItemHeader
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
        public List<GetDeliveryBatchSerial> BatchSerial { get; set; }
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
        public string UserReason { get; set; }
        public string ApprovalReason { get; set; }
        public string UserReasonId { get; set; }
        public string ApprovalReasonId { get; set; }
    }
    public class GetDeliveryBatchSerial
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
    public class ReturnItemBatchInpara
    {
        public string rtn_Id { get; set; }
        public string UserId { get; set; }
    }
    public class GetReturnItemHeader
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

        public List<GetRtnBatchSerial> BatchSerial { get; set; }
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
        public string ApprovalReason { get; set; }
        public string ApprovalReasonID { get; set; }
        public string UserReason { get; set; }
        public string UserReasonID { get; set; }
    }
    public class GetRtnBatchSerial
    {
        public string RtnBatSerialId { get; set; }
        public string RtnDetailId { get; set; }
        public string ItemCode { get; set; }
        public string Number { get; set; }
        public string ExpiryDate { get; set; }
        public string BaseUOM { get; set; }
        public string RtnQty { get; set; }

    }
    public class LoadOutCFinpara
    {
        public string dsp_ID { get; set; }
        public string date { get; set; }
        public string UserId { get; set; }
        public string ApprovedBy { get; set; }

    }
    public class LoadOutCFoutpara
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }


    }
    public class PostLoadoutData
    {
        public string DispatchId { get; set; }
        public string UserId { get; set; }
    }

    public class GetLoadOutItemHeader
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
        public List<GetLoadOutBatchSerial> BatchSerial { get; set; }
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
        public string UserReason { get; set; }
        public string UserReasonId { get; set; }
    }

    public class GetLoadOutBatchSerial
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

}