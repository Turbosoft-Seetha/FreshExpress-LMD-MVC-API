using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class InvHelper
    {

    }

    public class PostPickingData
    {
        public string PickingId { get; set; }
        public string UserId { get; set; }
    }
   

    public class PickListInfo
    {
        public int PickingId { get; set; }
        public string OrderId { get; set; }
        public string PickingLocation { get; set; }
        public string CusHeaderCode { get; set; }
        public string CusHeaderName { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string ExpectedDeliveryDate { get; set; }
        public string PickListNumber { get; set; }
        public string Picker { get; set; }
        public string PickListStatus { get; set; }
        public string Mode { get; set; }
        public string PickerContactNumber { get; set; }
        public string Remarks { get; set; }
        public List<GetPickingItemHeader> PickingItems { get; set; }
    }

    public class GetPickingItemHeader
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
        public string OHUOM { get; set; }
        public string OLUOM { get; set; }
        public string OHQty { get; set; }
        public string OLQty { get; set; }
        public string HigherUOM { get; set; }
        public string LowerUOM { get; set; }
        public string HigherQty { get; set; }
        public string LowerQty { get; set; }
        public string PromoType { get; set; }
        public string ReasonId { get; set; }
        public string EnableExcess { get; set; }
        public string WeighingItem { get; set; }
        public string Desc { get; set; }
        public string LineNo { get; set; }
        public string pid_ID { get; set; }
        public string odd_PromoType { get; set; }

        public List<GetPickingBatchSerial> BatchSerial { get; set; }
    }

    public class GetPickingBatchSerial
    {
        public string ItemCode { get; set; }
        public string Number { get; set; }
        public string ExpiryDate { get; set; }
        public string BaseUOM { get; set; }
        public string OrderedQty { get; set; }
        public string PickedQty { get; set; }
        public string AdjustedQty { get; set; }
        public string ReasonId { get; set; }
        public string UserId { get; set; }
        public string Mode { get; set; }
        public string salesman { get; set; }
        public string EligibleQty { get; set; }
        public string BatchID { get; set; }

        public string pid_ID { get; set; }

		public string ReservationNo { get; set; }

	}

    public class PostOrderData
    {
        public string ItemId { get; set; }
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public string ExpDelDate { get; set; }

		public string ItemLineNo { get; set; }
	}

    public class GetNewBatch
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string ExpiryDate { get; set; }
        public string AvailableQty { get; set; }
        public string SalesPerson { get; set; }
    }

    public class PostParkAndParkRelease
    {
        public string PickingId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public string ItemData { get; set; }
        public string BatchData { get; set; }
    }

    public class PostPickingItemData
    {
        public string ProductId { get; set; }
        public string ProductHUOM { get; set; }
        public string ProductLUOM { get; set; }
        public string ProductHQty { get; set; }
        public string ProductLQty { get; set; }
        public string ReasonId { get; set; }
        public string UserId { get; set; }
        public string ModifiedOn { get; set; }
        public string LineNumber{ get; set; }

    }

    public class PostPickingBatchData
    {
        public string ProductId { get; set; }
		public int itemLineNo { get; set; }

		public string itemCode { get; set; }
		public string Number { get; set; }
        public string PickedQty { get; set; }
        public string AdjustedQty { get; set; }
        public string ReasonId { get; set; }
        public string BatchMode { get; set; }
        public string ExpiryDate { get; set; }
        public string UserId { get; set; }
        public string ModifiedOn { get; set; }
        public int BatchID { get; set; }
        public string pid_ID { get; set; }
        public string SalesPerson { get; set; }
        public string EligibleQty { get; set; }
        public string BaseUOM { get; set; }
		public int ReservationLineNo { get; set; }
		public string Status { get; set; }

        public string ExtraQty { get; set; }
	}


    public class ErrorBatches
    {
        public string Status { get; set; }
        public string EligibleQty { get; set; }

        public string Number { get; set; }
        public int BatchID { get; set; }
        public int itemLineNo { get; set; }
        public string Desc { get; set; }

    }

    public class PostPickNewBatches
	{
		public string OrderNo { get; set; }
		public string itemNo { get; set; }
		public string lineNo { get; set; }
		public string BatchNo { get; set; }
		public string ReservationNo { get; set; }
		public string Qty { get; set; }
		public string EligibleQty { get; set; }
		public string Status { get; set; }


	}
    public class PostCompletePicking
    {
        public string ERP_OrderNo { get; set; }
        public string PickingId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }

        public string IntegrationMode { get; set; }
        public string ItemData { get; set; }
        public string BatchData { get; set; }
        public string NewBatches { get; set; }
    }
  
    public class OrderPickingHeaderIn
    {
      
        public string UserId { get; set; }
        public string JsonString { get; set; }

    }
    public class OrderPickingFilters
    {

        public string cus_ID { get; set; }
        public string rot_ID { get; set; }

        public string ordNo { get; set; }
        public string time { get; set; }


    }
    public class OrderPickingHeaderOut
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
        public string Picker { get; set; }

        public string pih_Status { get; set; }
        public string Mode { get; set; }
        public string PickerContactNo { get; set; }
        public string Remarks { get; set; }

        public string TimeRange { get; set; }
        public string PickingInstruction { get; set; }
        public string ord_ERP_OrderNo { get; set; }
        public string rot_ID { get; set; }
        public string rot_Code { get; set; }

        public string rot_Name { get; set; }
        public string cus_ID { get; set; }


    }
    public class selCusIn
    {
        public string UserID { get; set; }
        public string rot_ID { get; set; }


    }
    public class selCusOut
    {
        public string cus_ID { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
        public string cus_HeaderID { get; set; }
        public string cus_HeaderCode { get; set; }
        public string cus_HeaderName { get; set; }


    }
    public class selRotIn
    {
        public string UserID { get; set; }
        
    }
    public class selRotOut
    {
        public string rot_ID { get; set; }
        public string rot_Code { get; set; }
        public string rot_Name { get; set; }



    }
    public class SelfPickingIn
    {

        public string UserId { get; set; }
        public string JsonString { get; set; }

    }
    public class SelfpickingIds
    {

        public string pih_ID { get; set; }
      


    }

    public class SelfPickingOut
    {

        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }



    }
    public class UndoIn
    {
        public string PickingId { get; set; }
       
    }
    public class UndoOut
    {
        public string prd_ID { get; set; }
        public string LineNo { get; set; }
    }
    public class WeighInfo
    {
        public string UserID { get; set; }
        public string IP { get; set; }
        public int PortNo { get; set; }
    }
    public class ItemwiseSummaryOrdersIn
    {
        public string prdID { get; set; }
        public string UserID { get; set; }
        public string JsonString { get; set; }
        
    }
    public class ItemwiseSummaryOrdersOut
    {
        public string pih_ID { get; set; }
        public string ord_ERP_OrderNo { get; set; }
        public string pih_Number { get; set; }
        public string prd_ID { get; set; }
        public string ord_Huom { get; set; }
        public string ord_Hqty { get; set; }
        public string pid_HigherQty { get; set; }
        public string pid_HigherUOM { get; set; }
        public string ord_ExpectedDelDate { get; set; }
        public string prd_WeighingItem { get; set; }
        public string ord_ID { get; set; }
        public string cus_Name { get; set; }
        public string cus_Code { get; set; }
        public string csh_Name { get; set; }
        public string csh_Code { get; set; }
        public string pih_Remarks { get; set; }
        public string usr_Name { get; set; }
        public string usr_ContactNo { get; set; }
        public string ModifiedDate { get; set; }
        public string pih_Status { get; set; }
        public string pid_LineNo { get; set; }
        public string plm_Name { get; set; }

    }
    public class PickingIds
    {
        public string pih_ID { get; set; }
    }
    public class PickingId
    {
        public string pih_ID { get; set; }
    }
    public class ItemwiseSummarysIn
    {
        public string JsonString { get; set; }
        public string UserID { get; set; }
       
    }
    public class ItemwiseSummarysOut
    {
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string PickingInstruction { get; set; }
        public string BaseUOM { get; set; }
        public string Qty { get; set; }
       
    }
    public class selRotInfoOut
    {
        public string rot_ID { get; set; }
        public string rot_Code { get; set; }
        public string rot_Name { get; set; }
        public string User_Name { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string PicklistCount { get; set; }

    }
    public class VersionIn
    {
        public string UserID { get; set; }
        public string Version { get; set; }
    }
    public class VersionOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
    }


}