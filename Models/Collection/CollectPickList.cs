using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models.Collection
{
    public class CollectPickList
    {
    }

    public class GetPicklocIn
    {
        public string rotID { get; set; }
        public string usrID { get; set; }

    }
    public class GetPicklocOut
    {
        public string plm_ID { get; set; }
        public string plm_Code { get; set; }
        public string Status{ get; set; }
        public string plm_Name { get; set; }

    }
    public class GetPicklistIn
    {
        public string plm_ID { get; set; }
        public string rotID { get; set; }
        public string usrID { get; set; }

    }
    public class GetPicklistOut
    {
        public string PickListID { get; set; }
        public string ord_ID { get; set; }

        public string PickLocation { get; set; }
        public string CusHeaderCode { get; set; }
        public string CusHeaderName { get; set; }
        public string cus_Code { get; set; }

        public string cus_Name { get; set; }
        public string ExpectedDelDate { get; set; }
        public string PickListNumber { get; set; }
       
        public string pih_Status { get; set; }
      
        public string PickerContactNo { get; set; }
        public string Remarks { get; set; }
        public string PickingInstruction { get; set; }
        public string ord_ERP_OrderNo { get; set; }
        public string rot_ID { get; set; }
        public string rot_Code { get; set; }
        public string rot_Name { get; set; }
        public string cus_ID { get; set; }
        public string pickCount { get; set; }
        public string CollectedStatus { get; set; }


    }
    public class pickingDetailsIn
    {
        public string PickingId { get; set; }
        public string UserId { get; set; }
    }

    public class GetPickingItemDetails
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

        public List<GetPickingBatchData> BatchSerial { get; set; }
    }

    public class GetPickingBatchData
    {
        public string ItemCode { get; set; }
        public string Number { get; set; }
        public string ExpiryDate { get; set; }
        public string BaseUOM { get; set; }
        public string OrderedQty { get; set; }
        public string LoadInQty { get; set; }
        public string AdjustedQty { get; set; }
        public string LineNo { get; set; }
        
        public string DispBatSerialId { get; set; }

        public string DisDetailId { get; set; }

        

    }
    public class pickingCollectIn
    {
        public string PicklistId { get; set; }
       
        public string UserId { get; set; }
        public string JSONString { get; set; }
        public string BatchData { get; set; }
    }
    public class PostPickingCollectionItemData
    {
        public string ItemId { get; set; }
        
        public string CollectionQty { get; set; }
        public string LineNumber { get; set; }
        




    }
    public class PostPickingCollectionBatchSerial
    {
        public string BatSerialId { get; set; }
        public string DetailId { get; set; }
        public string CollectionQty { get; set; }
        
        
    }

    public class GetCollectionStatus
    {
        public string Mode { get; set; }
        public string Status { get; set; }
       

    }
}