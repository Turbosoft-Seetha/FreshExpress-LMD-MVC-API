using iTextSharp.text.pdf;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using MVC_API.Models.CustomerConnectHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers
{
    public class MasterController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        [HttpPost]

        public string SelectCustomerDetails([FromForm] CustomerMasterIn inputParams)
        {
            dm.TraceService("SelectCustomers STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PostItemCus> itemData = JsonConvert.DeserializeObject<List<PostItemCus>>(inputParams.JSONString);

                string InputXml = "";
                using (var sw2 = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw2))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostItemCus id in itemData)
                        {
                            string[] arr = { id.CusID.ToString() };
                            string[] arrName = { "CusID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw2.ToString();
                }

                string RotID = inputParams.RotID == null ? "0" : inputParams.RotID;
            

                //string[] ar = { RotID.ToString()  };
                DataTable dtCustomer = dm.loadList("SelCustomerDetails", "sp_SalesApp", InputXml.ToString());

                if (dtCustomer.Rows.Count > 0)
                {
                    List<CustomerMasterOut> listItems = new List<CustomerMasterOut>();
                    foreach (DataRow dr in dtCustomer.Rows)
                    {

                        listItems.Add(new CustomerMasterOut
                        {

                            cus_ID = dr["cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            cus_ShortName = dr["cus_ShortName"].ToString(),
                            cus_ContactPerson = dr["cus_ContactPerson"].ToString(),
                            cus_ContactEmail = dr["cus_ContactEmail"].ToString(),
                            cus_ContactNo = dr["cus_ContactNo"].ToString(),
                            cus_Whatsapp = dr["cus_Whatsapp"].ToString(),
                            cus_Address = dr["cus_Address"].ToString(),
                            cus_GeoLoc = dr["cus_GeoLoc"].ToString(),
                            cus_Type = dr["cus_Type"].ToString(),
                            cus_TotalCreditLimit = dr["cus_TotalCreditLimit"].ToString(),
                            cus_UsedCreditLimit = dr["cus_UsedCreditLimit"].ToString(),
                            cus_AvailableCreditLimit = dr["cus_AvailableCreditLimit"].ToString(),
                            cus_CreditDays = dr["cus_CreditDays"].ToString(),
                            IsLoadIn = dr["IsLoadIn"].ToString(),
                            cus_IsVatEnabled = dr["cus_IsVatEnabled"].ToString(),
                            RouteCustomer = dr["RouteCustomer"].ToString(),
                            cus_IsInvVisibile = dr["cus_IsInvVisibile"].ToString(),
                            cus_col_rot_ID = dr["rot_IsOrder"].ToString(),
                            cus_del_rot_ID = dr["rot_IsDelivery"].ToString(),
                            cus_ar_rot_ID = dr["rot_IsAR"].ToString(),
                            cus_ret_rot_ID = dr["rot_IsPrevReturn"].ToString(),
                            cus_IsSignature = dr["cus_IsSignature"].ToString(),
                            cus_IsRemarks = dr["cus_IsRemarks"].ToString(),
                            cus_Area = dr["cus_are_ID"].ToString(),
                            cus_StoreType = dr["cus_cls_ID"].ToString(),
                            rcs_RecaptureGeo = dr["cus_RecaptureGeo"].ToString(),
                            rcs_SelectType = dr["cus_SelectionType"].ToString(),
                            cus_FencingRadius = dr["cus_FencingRadius"].ToString(),
                            rcs_OnCallFeatures = dr["rot_onCallFeature"].ToString(),
                            cus_IsOrderPromo = dr["cus_IsOrderPromo"].ToString(),
                            cus_hold = dr["cus_hold"].ToString(),
                            rot_IsAdvPayment = dr["rot_IsAdvPayment"].ToString(),
                            cus_NoOfInvoices = dr["cus_NoOfInvoices"].ToString(),
                            cus_EnforceCustSelection = dr["cus_EnforceCustSelection"].ToString(),
                            cus_EnforceVoid = dr["cus_EnforceVoid"].ToString(),
                            cus_ArabicName = dr["cus_ArabicName"].ToString(),
                            CashPayEnable = dr["cus_IsCashPayEnable"].ToString(),
                            EnableSuggestedOrd = dr["EnableSuggestedOrd"].ToString(),
                            csr_PaymentModes = dr["csr_PaymentModes"].ToString(),
                            CustCategoryType = dr["csr_CustCategoryType"].ToString(),
                            rot_IsAR = dr["rot_IsAR"].ToString(),
                            rot_IsOrder = dr["rot_IsOrder"].ToString(),
                            rot_IsDelivery = dr["rot_IsDelivery"].ToString(),
                            rot_IsPrevReturn = dr["rot_IsPrevReturn"].ToString(),
                            csr_IsFGExemption = dr["csr_IsFGExemption"].ToString(),
                            InvoiceMode = dr["InvoiceMode"].ToString(),
                            csh_OutletType = dr["csh_OutletType"].ToString(),
                            cus_DocType = dr["cus_DocType"].ToString(),
                            cus_DocumentFormat = dr["cus_DocumentFormat"].ToString(),
                            csh_ID = dr["csh_ID"].ToString(),
                            EnableDeliverySignature = dr["cus_EnableDeliverySignature"].ToString(),
                            csh_Name = dr["csh_Name"].ToString(),
                            csh_Code = dr["csh_Code"].ToString(),
                            csh_VirtualType = dr["csh_VirtualType"].ToString(),
                            csh_InvocieLocation = dr["csh_InvocieLocation"].ToString(),
                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelectCustomers Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectCustomers ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectRouteItemUOM([FromForm] RouteItemMasterIn inputParams)
        {
            dm.TraceService("SelectRouteItemUOM STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string ItemID = inputParams.ItemID == null ? "0" : inputParams.ItemID;
                string RotID = inputParams.RotID == null ? "0" : inputParams.RotID;
               

                string[] arr = { RotID.ToString() };
                DataTable dtItem = dm.loadList("SelectRouteItemUOM", "sp_App_UOM", ItemID.ToString(), arr);

                if (dtItem.Rows.Count > 0)
                {
                    List<RouteItemMasterOut> listItems = new List<RouteItemMasterOut>();
                    foreach (DataRow dr in dtItem.Rows)
                    {

                        listItems.Add(new RouteItemMasterOut
                        {
                           
                            prd_ID = dr["prd_ID"].ToString(),
                            uom_ID = dr["prd_uom_ID"].ToString(),
                            pru_Price = dr["pru_Price"].ToString(),
                            pru_IsDefault = dr["pru_IsDefault"].ToString(),
                            pru_UPC = dr["pru_UPC"].ToString(),
                            pru_ReturnPrice = dr["pru_ReturnPrice"].ToString(),
                          
                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelectRouteItemUOM Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectRouteItemUOM ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectPackagingItems([FromForm] PackageItemsIn inputParams)
        {
            dm.TraceService("SelectPackagingItems STARTED");
            dm.TraceService("====================");

            try
            {
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;

                DataTable dtItem = dm.loadList("SelPackagingItems", "sp_PackageItems");

                if (dtItem.Rows.Count > 0)
                {
                    List<PackageItemsOut> listItems = new List<PackageItemsOut>();
                    foreach (DataRow dr in dtItem.Rows)
                    {

                        listItems.Add(new PackageItemsOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            prd_brd_ID = dr["prd_brd_ID"].ToString(),
                            prd_cat_ID = dr["prd_cat_ID"].ToString(),
                            prd_sub_ID = dr["prd_sub_ID"].ToString(),
                            prd_plm_ID = dr["prd_plm_ID"].ToString(),
                            prd_NameArabic = dr["prd_NameArabic"].ToString(),
                            prd_DescArabic = dr["prd_DescArabic"].ToString(),
                            prd_LongDesc = dr["prd_LongDesc"].ToString(),
                            prd_ArabicLongDesc = dr["prd_ArabicLongDesc"].ToString(),
                            prd_Image = dr["prd_Image"].ToString(),
                            prd_Spec = dr["prd_Spec"].ToString(),
                            prd_rak_ID = dr["prd_rak_ID"].ToString(),
                            prd_war_ID = dr["prd_war_ID"].ToString(),
                            prd_bas_ID = dr["prd_bas_ID"].ToString(),
                            prd_Barcode = dr["prd_Barcode"].ToString(),
                            prd_ExcessThreshold = dr["prd_ExcessThreshold"].ToString(),
                            prd_WeighingItem = dr["prd_WeighingItem"].ToString(),
                            prd_SortOrder = dr["prd_SortOrder"].ToString(),
                            prd_ReturnDays = dr["prd_ReturnDays"].ToString(),
                            prd_EnableOrderHold = dr["prd_EnableOrderHold"].ToString(),
                            prd_EnableReturnHold = dr["prd_EnableReturnHold"].ToString(),
                            prd_EnableDeliveryHold = dr["prd_EnableDeliveryHold"].ToString(),
                            prd_sct_ID = dr["prd_sct_ID"].ToString(),
                            prd_sco_ID = dr["prd_sco_ID"].ToString(),
                            prd_dep_ID = dr["prd_dep_ID"].ToString(),
                            prd_BaseUOM = dr["prd_BaseUOM"].ToString(),
                            prd_IsProductionItem = dr["prd_IsProductionItem"].ToString(),
                            prd_IsBarcodeOverItemCode = dr["prd_IsBarcodeOverItemCode"].ToString(),
                            prd_IsPackaging = dr["prd_IsPackaging"].ToString(),

                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService("SelectPackagingItems Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectPackagingItems ENDED");
            dm.TraceService("==================");


            return JSONString;
        }

    }
}