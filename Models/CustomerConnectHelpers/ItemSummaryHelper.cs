using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models.CustomerConnectHelpers
{
    public class ItemSummaryHelper
    {
    }
    public class ItemwiseSummaryIn
    {
        public string itemID { get; set; }
        public string usrID { get; set; }
        public string cusID { get; set; }
    }
    public class ItemwiseSummaryOut
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
        public string ord_ERP_OrderNo { get; set; }
        public string ord_LPONumber { get; set; }
        public string dsp_ID { get; set; }
        public string rot_ID { get; set; }
        public string rot_Code { get; set; }
        public string rot_Name { get; set; }
        public string IsPartiallyDelivered { get; set; }
        public string SubTotal { get; set; }
        public string VAT { get; set; }
        public string GrandTotal { get; set; }
        public string dln_DeliveryNumber { get; set; }
        public string ID { get; set; }
        public string ContactNumber { get; set; }

    }
    public class selItemIn
    {
        public string usrID { get; set; }

       
    }
    public class selItemOut
    {
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }

    }
    public class selCustomerIn
    {
        public string usrID { get; set; }
        public string ItemID { get; set; }


    }
    public class selCustomerOut
    {
        public string cus_ID { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }

    }
    public class ItemwiseSummaryDetailIn
    {
        public string dsp_ID { get; set; }
    }
    public class ItemwiseSummaryDetailOut
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
    }
}