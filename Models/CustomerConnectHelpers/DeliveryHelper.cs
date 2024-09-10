using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models.CustomerConnectHelpers
{
    public class DeliveryHelper
    {
    }
    public class DelievryIn
    {
        public string userID { get; set; }
        public string JSONStringCus { get; set; }
        public string JSONStringOutlet { get; set; }
        public string JSONStringRot { get; set; }
        public string JSONStringOrder { get; set; }
        public string JSONStringProducts { get; set; }
     //   public string JSONStringStatus { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Mode { get; set; }
    }
    public class DelievryOut
    {
        public string DispatchID { get; set; }
        public string Status { get; set; }
        public string ExpectedDelDate { get; set; }
        public string cus_ID { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
        public string cus_HeaderID { get; set; }
        public string cus_HeaderCode { get; set; }
        public string cus_HeaderName { get; set; }
        public string rot_ID { get; set; }
        public string rot_Code { get; set; }
        public string rot_Name { get; set; }
        public string IsPartiallyDelivered { get; set; }
        public string SubTotal { get; set; }
        public string VAT { get; set; }
        public string GrandTotal { get; set; }
        public string dln_DeliveryNumber { get; set; }
        public string ord_ERP_OrderNo { get; set; }
        public string ord_LPONumber { get; set; }
        public string ID { get; set; }
        public string ContactNumber { get; set; }
        
    }
    public class DelievryDetailIn
    {
        public string ID { get; set; }

    }
    public class DelievryDetailOut
    {
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string BaseUOM { get; set; }
        public string Qty { get; set; }
        public string Status { get; set; }
        public string LineTotal { get; set; }
        public string DelUOM { get; set; }
        public string DelQty { get; set; }

    }
    public class DelDetailOut
    {
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string BaseUOM { get; set; }
        public string Qty { get; set; }
        public string AdjUOM { get; set; }
        public string AdjQty { get; set; }
        public string FinalUOM { get; set; }
        public string FinalQty { get; set; }
        public string Status { get; set; }
        public string LineTotal { get; set; }
        public string Reason { get; set; }
    }
    public class PostItemDelCus
    {
        public string cus_HeaderID { get; set; }

    }
    public class PostItemDelCusOutlet
    {
        public string ID { get; set; }

    }
    public class PostItemDelRot
    {
        public string rot_ID { get; set; }

    }
    public class PostItemDelOrder
    {
        public string ordNo { get; set; }
    }
    public class PostItemDelProducts
    {
        public string prd_ID { get; set; }
    }
    public class PostItemDelStatus
    {
        public string Status { get; set; }
    }
    public class DeliveryCustomersIn
    {
        public string usrID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Mode { get; set; }
    }
    public class DeliveryCustomersOut
    {
        public string cus_HeaderID { get; set; }
        public string cus_HeaderCode { get; set; }
        public string cus_HeaderName { get; set; }
    }
    public class DelStampedCopyIn
    {
        public string dln_ID { get; set; }     
    }
    public class DelStampedCopyOut
    {
        public string StampedCopy { get; set; }
       
    }
}

