using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models.CustomerConnectHelpers
{
    public class NotificationHelper
    {
    }
    public class NotificationIn
    {
        public string UserID { get; set; }

    }
    public class NotificationOut
    {
        public string rnt_ID { get; set; }
        public string rnt_Header { get; set; }
        public string rnt_Desc { get; set; }
        public string rnt_ReadFlag { get; set; }
        public string rnt_ReplyMessage { get; set; }
        public string rnt_ReplyUserID { get; set; }
        public string rnt_ReplyTime { get; set; }
        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string rnt_ArabicHeader { get; set; }
        public string rnt_ArabicDesc { get; set; }
        public string rnt_usr_ID { get; set; }
        public string rnt_IsDetail { get; set; }
        public string TotalItemCount { get; set; }
        public string PartialPickedCount { get; set; }
        public string NotPickedCount { get; set; }

    }
    public class InsNotificationIn
    {
        public string rnt_ReplyMessage { get; set; }
        public string rnt_ReplyUserID { get; set; }
        public string rnt_ID { get; set; }

    }
    public class InsNotificationOut
    {
        public string Title { get; set; }
        public string Res { get; set; }
        public string Descr { get; set; }

    }
    public class UpdateNotificationIn
    {
        public string rnt_ID { get; set; }

    }
    public class UpdateNotificationOut
    {
        public string Title { get; set; }
        public string Res { get; set; }
        public string Descr { get; set; }

    }
    public class NotiDetailIn
    {
        public string rnt_ID { get; set; }

    }
    public class NotiDetailOut
    {
       
        public string rnd_ID { get; set; }
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string rnd_ord_ID { get; set; }
        public string ord_ERP_OrderNo { get; set; }
        public string CreatedDate { get; set; }
        public string UOM { get; set; }
        public string OrdQty { get; set; }
        public string AdjQty { get; set; }
        public string PickedQty { get; set; }
        public string rsn_ID { get; set; }
        public string rsn_Name { get; set; }
    }
    public class NotificationCountIn
    {
        public string UserID { get; set; }

    }
    public class NotificationCountOut
    {
        public string SentCount { get; set; }
        public string ReadCount { get; set; }
        
    }

    public class UpdateUserTokenIn
    {
        public string UserID { get; set; }
        public string Token { get; set; }

    }
    public class UpdateUserTokenOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }

    }
}