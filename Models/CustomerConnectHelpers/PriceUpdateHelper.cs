using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models.CustomerConnectHelpers
{
    public class PriceUpdateHelper
    {
    }
    public class PriceUpdateIn
    {
        public string smp_ID { get; set; }
        public string JSONStringCus { get; set; }
        public string JSONStringOutlet { get; set; }
        public string JSONStringRot { get; set; }
        public string JSONStringOrder { get; set; }
        public string JSONStringProducts { get; set; }
    }
    public class PriceUpdateOut
    {
        public string DispatchID { get; set; }
        public string DeliveryNumber { get; set; }
        public string OrderID { get; set; }
        public string DispatchedOn { get; set; }
        public string ExpectedDelDate { get; set; }
        public string cus_ID { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
        public string cus_HeaderID { get; set; }
        public string cus_HeaderCode { get; set; }
        public string cus_HeaderName { get; set; }

        public string PriceUpdateID { get; set; }
    }
    public class PriceUpdateDetailIn
    {
        public string PriceUpdateID { get; set; }
        public string DispatchID { get; set; }

    }
    
    public class PriceUpdateDetailOut
    {
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string SellingPrice { get; set; }
        public string RequestedPrice { get; set; }
        public string FinalPrice { get; set; }
        public string StdPrice { get; set; }
        public string Mode { get; set; }
        public string rsn_ID { get; set; }
        public string rsn_Name { get; set; }

    }
    public class PriceUpdateApproveIn
    {
       
        public string userID { get; set; }
        public string JSONString { get; set; }
    }
    public class PostItemData
    {
        public string PriceUpdateID { get; set; }
        public string prd_ID { get; set; }
        public string FinalPrice { get; set; }
        public string rsnID { get; set; }
    }
    public class PriceUpdateApproveOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }

    }
    public class PostItemsCus
    {
        public string cus_HeaderID { get; set; }

    }
    public class PostItemCusOutlet
    {
        public string ID { get; set; }

    }
    public class PostItemRot
    {
        public string rot_ID { get; set; }

    }
    public class PostItemOrder
    {
        public string ordNo { get; set; }
    }
    public class PostItemProducts
    {
        public string prd_ID { get; set; }
    }
    public class PriceUpdateCustomersIn
    {
        public string usrID { get; set; }
    }
    public class PriceUpdateCustomersOut
    {
        public string cus_HeaderID { get; set; }
        public string cus_HeaderCode { get; set; }
        public string cus_HeaderName { get; set; }
    }
}