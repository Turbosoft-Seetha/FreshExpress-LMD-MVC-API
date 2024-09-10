using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class MasterHelper
    {
    }
    public class CustomerMasterIn
    {
        public string RotID { get; set; }
        public string JSONString { get; set; }

    }
    public class PostItemCus
    {
        public string CusID { get; set; }

    }
    public class CustomerMasterOut
    {

        public string cus_ID { get; set; }
        public string cus_Code { get; set; }
        public string cus_Name { get; set; }
        public string cus_ShortName { get; set; }
        public string cus_ContactPerson { get; set; }
        public string cus_ContactEmail { get; set; }
        public string cus_ContactNo { get; set; }
        public string cus_Whatsapp { get; set; }
        public string cus_Address { get; set; }
        public string cus_GeoLoc { get; set; }
        public string cus_Type { get; set; }
        public string cus_TotalCreditLimit { get; set; }
        public string cus_UsedCreditLimit { get; set; }
        public string cus_AvailableCreditLimit { get; set; }
        public string cus_CreditDays { get; set; }
        public string IsLoadIn { get; set; }
        public string cus_IsVatEnabled { get; set; }
        public string RouteCustomer { get; set; }
        public string cus_IsInvVisibile { get; set; }
        public string cus_col_rot_ID { get; set; }
        public string cus_del_rot_ID { get; set; }
        public string cus_ar_rot_ID { get; set; }
        public string cus_ret_rot_ID { get; set; }
        public string cus_IsSignature { get; set; }
        public string cus_IsRemarks { get; set; }
        public string cus_Area { get; set; }
        public string cus_StoreType { get; set; }
        public string rcs_RecaptureGeo { get; set; }
        public string rcs_SelectType { get; set; }
        public string cus_FencingRadius { get; set; }
        public string rcs_OnCallFeatures { get; set; }
        public string cus_IsOrderPromo { get; set; }
        public string cus_hold { get; set; }
        public string rot_IsAdvPayment { get; set; }
        public string cus_NoOfInvoices { get; set; }
        public string cus_EnforceCustSelection { get; set; }
        public string cus_EnforceVoid { get; set; }
        public string cus_ArabicName { get; set; }
        public string CashPayEnable { get; set; }
        public string EnableSuggestedOrd { get; set; }
        public string csr_PaymentModes { get; set; }
        public string CustCategoryType { get; set; }
        public string rot_IsAR { get; set; }
        public string rot_IsOrder { get; set; }
        public string rot_IsDelivery { get; set; }
        public string rot_IsPrevReturn { get; set; }
        public string csr_IsFGExemption { get; set; }
        public string InvoiceMode { get; set; }
        public string csh_OutletType { get; set; }
        public string cus_DocType { get; set; }
        public string cus_DocumentFormat { get; set; }
        public string csh_ID { get; set; }
        public string EnableDeliverySignature { get; set; }
        public string csh_Name { get; set; }
        public string csh_Code { get; set; }
        public string csh_VirtualType { get; set; }
        public string csh_InvocieLocation { get; set; }

    }
    public class RouteItemMasterIn
    {
        public string ItemID { get; set; }
        public string RotID { get; set; }


    }
    public class RouteItemMasterOut
    {
        public string prd_ID { get; set; }
        public string uom_ID { get; set; }
        public string pru_Price { get; set; }
        public string pru_IsDefault { get; set; }
        public string pru_UPC { get; set; }
        public string pru_ReturnPrice { get; set; }
    }
    public class PackageItemsIn
    {
        public string UserID { get; set; }
    }

    public class PackageItemsOut
    {
        public string prd_ID { get; set; }
        public string prd_Code { get; set; }
        public string prd_Name { get; set; }
        public string prd_Desc { get; set; }
        public string prd_brd_ID { get; set; }
        public string prd_cat_ID { get; set; }
        public string prd_sub_ID { get; set; }
        public string prd_plm_ID { get; set; }
        public string prd_NameArabic { get; set; }
        public string prd_DescArabic { get; set; }
        public string prd_LongDesc { get; set; }
        public string prd_ArabicLongDesc { get; set; }
        public string prd_Image { get; set; }
        public string prd_Spec { get; set; }
        public string prd_rak_ID { get; set; }
        public string prd_war_ID { get; set; }
        public string prd_bas_ID { get; set; }
        public string prd_Barcode { get; set; }
        public string prd_ExcessThreshold { get; set; }
        public string prd_WeighingItem { get; set; }
        public string prd_SortOrder { get; set; }
        public string prd_ReturnDays { get; set; }
        public string prd_EnableOrderHold { get; set; }
        public string prd_EnableReturnHold { get; set; }
        public string prd_EnableDeliveryHold { get; set; }
        public string prd_sco_ID { get; set; }
        public string prd_sct_ID { get; set; }
        public string prd_dep_ID { get; set; }
        public string prd_BaseUOM { get; set; }
        public string prd_IsProductionItem { get; set; }
        public string prd_IsBarcodeOverItemCode { get; set; }
        public string prd_IsPackaging { get; set; }
    }


}