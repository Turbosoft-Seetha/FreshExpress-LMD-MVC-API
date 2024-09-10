using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models.CustomerConnectHelpers
{
    public class PriceCheck
    {
    }

    public class selPriceCheckCustomerIn
    {
        public string SalesPersonID { get; set; }
       


    }
    public class selPriceCheckCustomerOut
    {
        public string cus_ID { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }



    }
    public class selPriceCheckIn
    {
        public string ProductCode { get; set; }

        public string CustomerCode { get; set; }


    }
    public class selPriceCheckOut
    { 
        public string UOM { get; set; }
        public string Price { get; set; }
    }

    public class PriceCheckProductsIn
    {
        public string Product { get; set; }
    }
    public class PriceCheckProductsOut
    {
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
    }
}