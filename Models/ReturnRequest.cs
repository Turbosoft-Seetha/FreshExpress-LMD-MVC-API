using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class ReturnRequest
    {
    }
    public class ReturnRequestIn
    {
        
        public string UserId { get; set; }
        public string rot_ID { get; set; }
    }
    public class GetRtnRequestHeader
    {
        
        public string inv_ID { get; set; }
        public string RequestNumber { get; set; }
        public string date { get; set; }
       
        public List<GetRtnRequestDetail> RequestDetail { get; set; }
        public string cus_ID { get; set; }
        public string Request_ID { get; set; }

        public string ID { get; set; }
    }
    public class GetRtnRequestDetail
    {
       
        public string prd_ID { get; set; }
        public string HUOM { get; set; }
        public string HQty { get; set; }
        public string LUOM { get; set; }
        public string LQty { get; set; }
        public string Weighing { get; set; }
        public string spec { get; set; }

        public List<GetReturnBatchSerial> batchserial { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string prd_code { get; set; }
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
        public string prd_BaseUOM { get; set; }
        public string VATPercent { get; set; }
        public string LineNo { get; set; }
        public string rrh_inv_ID { get; set; }
        public string ID { get; set; }
    }
    public class GetReturnBatchSerial
    {
        public string ind_inv_ID { get; set; }   
        public string Number { get; set; }
        public string ExpiryDate { get; set; }
        public string BaseUOM { get; set; }
        public string EligibleQty { get; set; }    
        public string prd_ID { get; set; }
        public string Price { get; set; }
        public string ID { get; set; }
        public string BatchSerialId { get; set; }
        
    }
    public class ReturnIn
    {
        public string cseID { get; set; }
        public string cusID { get; set; }
        public string udpID { get; set; }
        public string type { get; set; }
        
        public string date{ get; set; }
        public string usrID { get; set; }

        public string ItemID { get; set; }

        public string BatchData { get; set; }
        public string Request_ID { get; set; }
        
    }
    public class ItemIDs
    {
        public string reason { get; set; }
        public string invoiceID { get; set; }
        public string prdID { get; set; }
        public string HigherUOM { get; set; }

        public string HigherQty { get; set; }
        public string LowerUOM { get; set; }

        public string LowerQty { get; set; }

    }
    public class BatchSerial
    {
        public string prdID { get; set; }
        public string ExpiryDate { get; set; }
        public string UOM { get; set; }
        public string ReturnQty { get; set; }

        public string Mode { get; set; }

        public string BatSerialNo { get; set; }
    }
    public class ReturnOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }

    }
    
    public class PostReturnItemData
    {
        public string ItemId { get; set; }
        public string HigherUOM { get; set; }
        public string HigherQty { get; set; }
        public string LowerUOM { get; set; }
        public string LowerQty { get; set; }
        public string ReasonId { get; set; }
    }
    public class PostReturnData
    {
        public string ReturnID { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public string JSONString { get; set; }
        public string udpID { get; set; }
        public string rotID { get; set; }
        
    }
    public class GetReturnInsertStatus
    {
        public string Mode { get; set; }
        public string Status { get; set; }
    }
    public class PostReturnApprovalStatusData
    {
        public string ReturnID { get; set; }
        public string UserId { get; set; }

    }
    public class GetReturnApprovalStatus
    {
        public string ApprovalReason { get; set; }
        public string ApprovalStatus { get; set; }

        public string Products { get; set; }

    }
    public class PostReturnApprovalHeaderStatusData
    {
        public string ReturnID { get; set; }
        public string UserId { get; set; }
    }
    public class GetReturnApprovalHeaderStatus
    {
        public string ApprovalReason { get; set; }
        public string ApprovalStatus { get; set; }
    }
}