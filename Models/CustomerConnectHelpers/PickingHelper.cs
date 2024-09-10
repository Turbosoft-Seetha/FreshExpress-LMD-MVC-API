using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models.CustomerConnectHelpers
{
    public class PickingHelper
    {
    }
    public class PickingIn
    {
        public string userID { get; set; }
        public string JSONStringCus { get; set; }
        public string JSONStringOutlet { get; set; }
        public string JSONStringRot { get; set; }
        public string JSONStringOrder { get; set; }
        public string JSONStringProducts { get; set; }
       // public string JSONStringStatus { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Mode { get; set; }

    }
    public class PickingOut
    {
        public string PickListID { get; set; }
        public string PickingStatus { get; set; }
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
        public string ord_ERP_OrderNo { get; set; }
        public string ord_LPONumber { get; set; }
        public string ID { get; set; }
    }

    public class PickingDetailIn
    {
        public string ID { get; set; }

    }
    public class PickingDetailOut
    {
       
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string BaseUOM { get; set; }
        public string Qty { get; set; }
        public string OrdQty { get; set; }
        public string OrdUOM { get; set; }

    }
    public class PostItemPickingCus
    {
        public string cus_HeaderID { get; set; }

    }
    public class PostItemPickingCusOutlet
    {
        public string ID { get; set; }

    }
    public class PostItemPickingRot
    {
        public string rot_ID { get; set; }

    }
    public class PostItemPickingOrder
    {
        public string ordNo { get; set; }
    }
    public class PostItemPickingProducts
    {
        public string prd_ID { get; set; }
    }
    public class PostItemPickingStatus
    {
        public string Status { get; set; }
    }
    public class PickingCustomersIn
    {
        public string usrID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Mode { get; set; }
    }
    public class PickingCustomersOut
    {
        public string cus_HeaderID { get; set; }
        public string cus_HeaderCode { get; set; }
        public string cus_HeaderName { get; set; }
    }
}