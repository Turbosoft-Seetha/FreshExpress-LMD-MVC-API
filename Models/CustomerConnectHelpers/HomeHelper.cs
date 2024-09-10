using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class HomeHelper
    {
    }
    public class HomeCountIn
    {
        public string userID { get; set; }
        public string JSONStringCus { get; set; }
        public string JSONStringOutlet { get; set; }
        public string JSONStringRot { get; set; }
        public string JSONStringOrder { get; set; }
        public string JSONStringProducts { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
    public class Counts
    {
        public string PickingTotal { get; set; }
        public string PickingNotStarted { get; set; }
        public string PickingOngoing { get; set; }
        public string PickingCompleted { get; set; }

        public string DispatchTotal { get; set; }
        public string DispatchPending { get; set; }
        public string DispatchCompleted { get; set; }
        public string DispatchCancelled { get; set; }

        public string LoadInTotal { get; set; }
        public string LoadInPending { get; set; }
        public string LoadInCompleted { get; set; }
        public string LoadInCancelled { get; set; }
        public string LoadInReschedule { get; set; }

        public string TotalDelivery { get; set; }
        public string FD { get; set; }
        public string PD { get; set; }
        public string ND { get; set; }
        public string NP { get; set; }

        public string TotalPickingOrders { get; set; }
        public string TotalPickingOngoingOrders { get; set; }
        public string TotalPickingNotStartedOrders { get; set; }
        public string TotalPickingCompletedOrders { get; set; }

        public string TotalDesOrders { get; set; }
        public string TotalDesCompletedOrders { get; set; }
        public string TotalDesCancelledOrders { get; set; }
        public string TotalDesRescheduleOrders { get; set; }

        public string TotalLoadInOrders { get; set; }
        public string TotalComLoadInOrders { get; set; }
        public string TotalPenLoadInOrders { get; set; }
        public string TotalRejLoadInOrders { get; set; }

        public string TotalFullDeliveryOrders { get; set; }
        public string TotalPartialDeliveryOrders { get; set; }
        public string TotalNDDeliveryOrders { get; set; }
        public string TotalNPDeliveryOrders { get; set; }

        public string CarryForwardOrders { get; set; }

    }

    public class LoginIn
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
    public class LoginOut
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContacInfo { get; set; }
        public string Active { get; set; }
        public string usrID { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string NewUser { get; set; }
        public string cus_ID { get; set; }
        public string spm_ID { get; set; }
        public string spm_Code { get; set; }
        public string spm_Name { get; set; }
        public string spm_Email { get; set; }
        public string spm_Phone { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
        public string VersionDate { get; set; }
    }

    public class changepass
    {
        public string cPass { get; set; }
        public string nPass { get; set; }
        public string uName { get; set; }

        public string userID { get; set; }
    }
    public class changePassout
    {
        public string Mode { get; set; }
        public string Status { get; set; }

    }
    public class OrderTrackIn
    {
        public string DispatchID { get; set; }

    }
    public class OrderTrackOut
    {
        public string DispatchID { get; set; }
        public string Track { get; set; }
        public string ord_ID { get; set; }
        public string Mode { get; set; }
        public string Date { get; set; }
    }
    public class ReasonOut
    {
        public string rsn_ID { get; set; }
        public string rsn_Name { get; set; }
        public string rsn_ArabicName { get; set; }
        public string rsn_Type { get; set; }
    }
    public class OutletIn
    {
        public string usrID { get; set; }
        public string csh_ID { get; set; }
    }
    public class OutletOut
    {

        public string ID { get; set; }
        public string Name { get; set; }
       

    }
    public class RouteIn
    {
        public string usrID { get; set; }
        public string Cus_ID { get; set; }
    }
    public class RouteOut
    {

        public string rot_ID { get; set; }
        public string rot_Name { get; set; }


    }
    public class OrderIn
    {
        public string usrID { get; set; }
        public string Cus_ID { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
    public class OrderOut
    {

        public string ord_ID { get; set; }
        public string OrderID { get; set; }


    }
    public class ProductsIn
    {
        public string usrID { get; set; }
        public string ord_ID { get; set; }
    }
    public class ProductsOut
    {

        public string prd_ID { get; set; }
        public string prd_Name { get; set; }


    }
    public class StatusOut
    {

        public string sts_Type { get; set; }
        public string sts_Name { get; set; }


    }
    public class PostItemDatasCus
    {
        public string cus_HeaderID { get; set; }
       
    }
    public class PostItemDatasCusOutlet
    {
        public string ID { get; set; }

    }
    public class PostItemDatasRot
    {
        public string rot_ID { get; set; }
      
    }
    public class PostItemDatasOrder
    {
        public string ordNo { get; set; }
    }
    public class PostItemDatasProducts
    {
        public string prd_ID { get; set; }
    }
    public class StatusIn
    {
        public string JSONStringStatus { get; set; }
    }
    public class PostItemStatus
    {
        public string Status { get; set; }

    }
    public class VersionDetailIn
    {
        public string Type { get; set; }
       
    }
    public class VersionDetailOut
    {

        public string ver_code { get; set; }
        public string ver_name { get; set; }
        public string url { get; set; }
        public string msg { get; set; }

    }
    public class FooterLogo
    {
        public string footerImage { get; set; }
        public string platform { get; set; }
        
    }
    public class CCVersionIn
    {
        public string UserID { get; set; }
        public string Version { get; set; }
    }
    public class CCVersionOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
    }
    public class CusDashboardIN
    {
        public string userID { get; set; }
      
    }
    public class CusDashboardOUT
    {
        public string cus_HeaderID { get; set; }
        public string cus_HeaderCode { get; set; }
        public string cus_HeaderName { get; set; }
    }
}