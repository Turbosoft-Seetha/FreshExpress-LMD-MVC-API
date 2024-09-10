using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models.CustomerConnectHelpers
{
    public class CustomerHelper
    {
    }
    public class CustomerIN
    {
        public string usrID { get; set; }
        public string customer { get; set; }
    }
    public class CustomerOut
    {
       
        public string cus_HeaderID { get; set; }
        public string cus_HeaderCode { get; set; }
        public string cus_HeaderName { get; set; }
       
    }
    public class CustomerActionIn
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CusID { get; set; }
        

    }
    public class CustomerActionOut
    {
        public string Del_Count { get; set; }
        public string PD_Count { get; set; }
        public string FD_Count { get; set; }
        public string Return_Count { get; set; }
        public string AR_Count { get; set; }
        public string PriceApproval_Count { get; set; }
        public string Invoice_Count { get; set; }

    }
    public class CustomerActionARIn
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CusID { get; set; }

    }
    public class CustomerActionAROut
    {
        public string arh_ID { get; set; }
        public string arh_ARNumber { get; set; }
        public string Date { get; set; }
        public string CollectedAmount { get; set; }
      
    }
    public class CustomerActionDelOut
    {
        public string dln_ID { get; set; }
        public string dln_DeliveryNumber { get; set; }
        public string Date { get; set; }
        public string ord_ERP_OrderNo { get; set; }
        public string ord_LPONumber { get; set; }
        public string Status { get; set; }

    }
    public class CustomerActionReturnOut
    {
        public string rtn_ID { get; set; }
        public string rtn_Number { get; set; }
        public string Date { get; set; }
      
    }
    public class CustomerActionInvOut
    {
        public string inv_ID { get; set; }
        public string inv_InvoiceID { get; set; }
        public string Date { get; set; }
        public string inv_TotalAmount { get; set; }
      
    }
    public class CustomerActionPriceApprovalOut
    {
        public string pqh_ID { get; set; }
        public string dsp_DispatchID { get; set; }
        public string Date { get; set; }
        public string ord_ERP_OrderNo { get; set; }
        public string ord_LPONumber { get; set; }
    }
    public class DelInvIn
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CusID { get; set; }


    }
    public class DeliveryInvOut
    {
        public string inv_ID { get; set; }
        public string dln_ID { get; set; }
        public string dln_DeliveryNumber { get; set; }


    }
}