using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class ARHelper
    {
        public string ARHeaders { get; set; }
        public string ARDetails { get; set; }
    }
    public class PostARHeader
    {
        public string udpID { get; set; }
        public string cusID { get; set; }
        public string cseID { get; set; }
        public string CollectedAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string Remarks { get; set; }
        public string AppOrderID { get; set; }
        public string Signature { get; set; }
        public string PayMode { get; set; }
        public string ReceiptImage { get; set; }
        public string GeoCode { get; set; }
        public string GeoCodeName { get; set; }
        public string SyncedDateTime { get; set; }
        public string CreationMode { get; set; }
        public string CreatedBy { get; set; }
    }
    public class PostARDetail
    {
        public string invID { get; set; }
        public string Amount { get; set; }
        public string PrevBalance { get; set; }
        public string CurrentBalance { get; set; }
    }
    public class GetARInsertStatus
    {
        public string Res { get; set; }
        public string Title { get; set; }
    }

    public class PostOutStandingdInvData
    {
        public string Inv_ID { get; set; }
       
    }
    public class PostOutStandingdInvDataOut
    {
        public string InvoiceID { get; set; }
        public string Inv_ID { get; set; }
        public string cus_ID { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string InvoiceAmount { get; set; }
        public string AmountPaid { get; set; }
        public string InvoiceBalance { get; set; }
        public string PDC_Amount { get; set; }
    }
}