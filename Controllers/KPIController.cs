using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers
{
    public class KPIController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;

         [HttpPost]

        // KPI Delivery
        public string SelKPIDeliveryCount([FromForm] KPICountIn inputParams)
        {
            dm.TraceService("SelKPIDeliveryCount STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.RotID == null ? "0" : inputParams.RotID;

                DataTable dtCount = dm.loadList("SelKPIDeliveryCount", "sp_KPIHistory", RotID.ToString());

                if (dtCount.Rows.Count > 0)
                {
                    List<KPICountOut> listItems = new List<KPICountOut>();
                    foreach (DataRow dr in dtCount.Rows)
                    {

                        listItems.Add(new KPICountOut
                        {
                            TotalDeliveryCount = dr["TotalDeliveryCount"].ToString(),
                            FDCount = dr["FDCount"].ToString(),
                            PDCount = dr["PDCount"].ToString(),
                            NDCount = dr["NDCount"].ToString(),
                            NPCount = dr["NPCount"].ToString(),
                            LONDCount = dr["LONDCount"].ToString(),
                            LONPCount = dr["LONPCount"].ToString(),
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
                dm.TraceService(" SelKPIDeliveryCount Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelKPIDeliveryCount ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetKPIDeliveryHeader([FromForm] KPIDeliveryHeaderIn inputParams)
        {
            dm.TraceService("GetKPIDeliveryHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rot_ID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;

                string[] arr = { userID.ToString() };

                DataTable dtTrnsIn = dm.loadList("SelKPIDeliveryHeader", "sp_KPIHistory", rot_ID.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<KPIDeliveryHeaderOut> listItems = new List<KPIDeliveryHeaderOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new KPIDeliveryHeaderOut
                        {

                            dln_ID = dr["dln_ID"].ToString(),
                            dln_DeliveryNumber = dr["dln_DeliveryNumber"].ToString(),
                            OrderId = dr["ord_ID"].ToString(),
                            Date = dr["Date"].ToString(),
                            Time = dr["Time"].ToString(),
                            dsp_DispatchID = dr["dsp_DispatchID"].ToString(),
                            CustomerID = dr["cus_ID"].ToString(),
                            CustomerCode = dr["cus_Code"].ToString(),
                            CustomerName = dr["cus_Name"].ToString(),
                            CusHeaderID = dr["csh_ID"].ToString(),
                            CusHeaderCode = dr["csh_Code"].ToString(),
                            CusHeaderName = dr["csh_Name"].ToString(),
                            IsPartiallyDelivered = dr["dln_IsPartiallyDelivered"].ToString(),
                            SubTotal = dr["SubTotal"].ToString(),
                            VAT = dr["VAT"].ToString(),
                            TotalAmount = dr["TotalAmount"].ToString(),
                            ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            Status = dr["Status"].ToString(),
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
                dm.TraceService(" GetKPIDeliveryHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetKPIDeliveryHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetKPIDeliveryDetail([FromForm] KPIDeliveryDetailIn inputParams)
        {
            dm.TraceService("GetKPIDeliveryDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string dln_ID = inputParams.dln_ID == null ? "0" : inputParams.dln_ID;

                string[] arr = { };
                DataSet dtTrnsIn = dm.loadListDS("SelKPIDeliveryDetail", "sp_KPIHistory", dln_ID.ToString(), arr);
                DataTable itemData = dtTrnsIn.Tables[0];
                DataTable batchData = dtTrnsIn.Tables[1];
                if (itemData.Rows.Count > 0)
                {
                    List<KPIDeliveryDetailOut> listItems = new List<KPIDeliveryDetailOut>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<GetKPIDelItemBatchSerial> listBatchSerial = new List<GetKPIDelItemBatchSerial>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["dld_ID"].ToString() == drDetails["dns_dld_ID"].ToString())
                            {
                                listBatchSerial.Add(new GetKPIDelItemBatchSerial
                                {
                                    Number = drDetails["dns_Number"].ToString(),
                                    ExpiryDate = drDetails["dns_ExpiryDate"].ToString(),
                                    BaseUOM = drDetails["dns_BaseUOM"].ToString(),
                                    OrderedQty = drDetails["dns_OrderedQty"].ToString(),
                                    LoadInQty = drDetails["dns_LoadInQty"].ToString(),
                                    AdjustedQty = drDetails["dns_AdjustedQty"].ToString(),
                                    ItemCode = drDetails["prd_Code"].ToString(),
                                    BatSerialId = drDetails["dns_ID"].ToString(),
                                    DetailId = drDetails["dns_dld_ID"].ToString(),
                                    LineNo = drDetails["dln_LineNo"].ToString(),
                                    ItemId = drDetails["prd_ID"].ToString(),
                                });
                            }
                        }

                        listItems.Add(new KPIDeliveryDetailOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            Spec = dr["prd_Spec"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            prd_LongDesc = dr["prd_LongDesc"].ToString(),
                            CategoryId = dr["prd_cat_ID"].ToString(),
                            SubcategoryId = dr["prd_sub_ID"].ToString(),
                            WeighingItem = dr["prd_WeighingItem"].ToString(),
                            SysHUOM = dr["dld_HigherUOM"].ToString(),
                            SysLUOM = dr["dld_LowerUOM"].ToString(),
                            SysHQty = dr["dld_HigherQty"].ToString(),
                            SysLQty = dr["dld_LowerQty"].ToString(),
                            AdjHUOM = dr["dld_AdjHigherUOM"].ToString(),
                            AdjLUOM = dr["dld_AdjLowerUOM"].ToString(),
                            AdjHQty = dr["dld_AdjHigherQty"].ToString(),
                            AdjLQty = dr["dld_AdjLowerQty"].ToString(),
                            LiHUOM = dr["dld_FinalHigherUOM"].ToString(),
                            LiLUOM = dr["dld_FinalLowerUOM"].ToString(),
                            LiHQty = dr["dld_FinalHigherQty"].ToString(),
                            LiLQty = dr["dld_FinalLowerQty"].ToString(),
                            Type = dr["dld_TransType"].ToString(),
                            LineNo = dr["dln_LineNo"].ToString(),
                            Price = dr["Price"].ToString(),
                            LineTotal = dr["LineTotal"].ToString(),
                            BatchSerial = listBatchSerial,
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
                dm.TraceService(" GetKPIDeliveryDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetKPIDeliveryDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
      

        // KPI Invoice
        public string SelKPIInvoiceCount([FromForm] KPIInvCountIn inputParams)
        {
            dm.TraceService("SelKPIInvoiceCount STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string udpID = inputParams.udpID == null ? "0" : inputParams.udpID;

                DataTable dtCount = dm.loadList("SelKPIInvoiceCount", "sp_KPIHistory", udpID.ToString());

                if (dtCount.Rows.Count > 0)
                {
                    List<KPIInvCountOut> listItems = new List<KPIInvCountOut>();
                    foreach (DataRow dr in dtCount.Rows)
                    {

                        listItems.Add(new KPIInvCountOut
                        {
                            TotalAmount = dr["TotalAmount"].ToString(),
                            HCCount = dr["HCCount"].ToString(),
                            HCAmount = dr["HCAmount"].ToString(),
                            POSCount = dr["POSCount"].ToString(),
                            POSAmount = dr["POSAmount"].ToString(),
                            OPCount = dr["OPCount"].ToString(),
                            OPAmount = dr["OPAmount"].ToString(),
                            GCCount = dr["GCCount"].ToString(),
                            GCAmount = dr["GCAmount"].ToString(),
                            TCCount = dr["TCCount"].ToString(),
                            TCAmount = dr["TCAmount"].ToString(),
                            APCount = dr["APCount"].ToString(),
                            APAmount = dr["APAmount"].ToString(),
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
                dm.TraceService(" SelKPIInvoiceCount Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelKPIInvoiceCount ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetKPIInvoiceHeader([FromForm] KPIInvoiceHeaderIn inputParams)
        {
            dm.TraceService("GetKPIInvoiceHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string udp_ID = inputParams.udp_ID == null ? "0" : inputParams.udp_ID;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;

                string[] arr = { userID.ToString() };

                DataTable dtTrnsIn = dm.loadList("SelKPIInvoiceHeader", "sp_KPIHistory", udp_ID.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<KPIPostInvoiceHeaderOut> listItems = new List<KPIPostInvoiceHeaderOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new KPIPostInvoiceHeaderOut
                        {

                            InvID = dr["InvID"].ToString(),
                            InvoiceNumber = dr["InvoiceNumber"].ToString(),
                            InvoiceDate = dr["InvoiceDate"].ToString(),
                            InvoiceTime = dr["InvoiceTime"].ToString(),
                            CshCode = dr["csh_Code"].ToString(),
                            CshName = dr["csh_Name"].ToString(),
                            Address1 = dr["csh_Address_1"].ToString(),
                            Address2 = dr["csh_Address_2"].ToString(),
                            Address3 = dr["csh_Address_3"].ToString(),
                            CshPhone = dr["csh_Phone"].ToString(),
                            CshTRN = dr["csh_TRN"].ToString(),
                            CshPatID = dr["csh_pat_ID"].ToString(),
                            CshCurrency = dr["csh_Currency"].ToString(),
                            SubTotalWODiscount = dr["inv_SubTotal_WO_Discount"].ToString(),
                            Discount = dr["inv_Discount"].ToString(),
                            SubTotal = dr["inv_SubTotal"].ToString(),
                            VAT = dr["inv_VAT"].ToString(),
                            TotalAmount = dr["inv_TotalAmount"].ToString(),
                            DepartmentID = dr["inv_dep_ID"].ToString(),
                            PayType = dr["inv_PayType"].ToString(),
                            PayMode = dr["inv_PayMode"].ToString(),
                            cus_ID = dr["inv_cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
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
                dm.TraceService(" GetKPIInvoiceHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetKPIInvoiceHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetKPIInvoiceDetail([FromForm] KPIInvoiceDetailIn inputParams)
        {
            dm.TraceService("MainRouteDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string InvID = inputParams.InvID == null ? "0" : inputParams.InvID;

                DataTable dtTrnsIn = dm.loadList("SelKPIInvoiceDetail", "sp_KPIHistory", InvID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<KPIPostInvoiceDetailOut> listItems = new List<KPIPostInvoiceDetailOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new KPIPostInvoiceDetailOut
                        {
                            InvID = dr["ind_inv_ID"].ToString(),
                            IndID = dr["ind_ID"].ToString(),
                            DlnID = dr["ind_dln_ID"].ToString(),
                            PrdID = dr["prd_ID"].ToString(),
                            PrdCode = dr["prd_Code"].ToString(),
                            PrdName = dr["prd_Name"].ToString(),
                            IndHigherUOM = dr["ind_HigherUOM"].ToString(),
                            IndHigherQty = dr["ind_HigherQty"].ToString(),
                            IndLowerUOM = dr["ind_LowerUOM"].ToString(),
                            IndLowerQty = dr["ind_LowerQty"].ToString(),
                            IndHigherPrice = dr["ind_HigherPrice"].ToString(),
                            IndLowerPrice = dr["ind_LowerPrice"].ToString(),
                            IndLineNo = dr["ind_LineNo"].ToString(),
                            IndTransType = dr["ind_TransType"].ToString(),
                            IndLineTotal = dr["ind_LineTotal"].ToString(),
                            IndPrice = dr["ind_Price"].ToString(),
                            IndDiscount = dr["ind_Discount"].ToString(),
                            IndDiscountPrec = dr["ind_DiscountPrec"].ToString(),
                            IndGrandTotal = dr["ind_GrandTotal"].ToString(),
                            IndLineVAT = dr["ind_LineVAT"].ToString(),
                            IndPieceDiscount = dr["ind_PieceDiscount"].ToString(),
                            IndSubTotalWODiscount = dr["ind_SubTotal_WO_Discount"].ToString(),
                            IndTotalQty = dr["ind_TotalQty"].ToString(),
                            IndVAT = dr["ind_VAT"].ToString(),
                            IndVATPerc = dr["ind_VATPerc"].ToString(),
                            PrdDesc = dr["prd_Desc"].ToString(),
                            PrdSpec = dr["prd_Spec"].ToString(),
                            CategoryId = dr["prd_cat_ID"].ToString(),
                            SubcategoryId = dr["prd_sub_ID"].ToString(),
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
                dm.TraceService(" MainRouteDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("MainRouteDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        // KPI Return
        public string SelKPIReturnCount([FromForm] KPIReturnCountIn inputParams)
        {
            dm.TraceService("SelKPIReturnCount STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.RotID == null ? "0" : inputParams.RotID;

                DataTable dtCount = dm.loadList("SelKPIReturnCount", "sp_KPIHistory", RotID.ToString());

                if (dtCount.Rows.Count > 0)
                {
                    List<KPIReturnCountOut> listItems = new List<KPIReturnCountOut>();
                    foreach (DataRow dr in dtCount.Rows)
                    {

                        listItems.Add(new KPIReturnCountOut
                        {

                            WithInvoiceCount = dr["WithInvoiceCount"].ToString(),
                            WithOutInvoiceCount = dr["WithOutInvoiceCount"].ToString(),
                         
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
                dm.TraceService(" SelKPIReturnCount Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelKPIReturnCount ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetKPIReturnHeader([FromForm] KPIReturnHeaderIn inputParams)
        {
            dm.TraceService("GetKPIReturnHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.RotID == null ? "0" : inputParams.RotID;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;

                string[] arr = { userID.ToString() };

                DataTable dtTrnsIn = dm.loadList("SelKPIReturnHeader", "sp_KPIHistory", RotID.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<KPIReturnHeaderOut> listItems = new List<KPIReturnHeaderOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new KPIReturnHeaderOut
                        {                        
                            rrh_ID = dr["rrh_ID"].ToString(),
                            rrh_RequestNumber = dr["rrh_RequestNumber"].ToString(),
                            inv_InvoiceID = dr["inv_InvoiceID"].ToString(),
                            Date = dr["Date"].ToString(),
                            Type = dr["Type"].ToString(),
                            Status = dr["Status"].ToString(),
                            csh_ID = dr["csh_ID"].ToString(),
                            csh_Code = dr["csh_Code"].ToString(),
                            csh_Name = dr["csh_Name"].ToString(),                          
                            cus_ID = dr["cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
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
                dm.TraceService(" GetKPIReturnHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetKPIReturnHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetKPIReturnDetail([FromForm] KPIReturnDetailIn inputParams)
        {
            dm.TraceService("GetKPIReturnDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rrh_ID = inputParams.rrh_ID == null ? "0" : inputParams.rrh_ID;

                DataTable dtTrnsIn = dm.loadList("SelKPIReturnDetail", "sp_KPIHistory", rrh_ID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<KPIReturnDetailOut> listItems = new List<KPIReturnDetailOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new KPIReturnDetailOut
                        {
                            rrd_ID = dr["rrd_ID"].ToString(),
                            rrd_HUOM = dr["rrd_HUOM"].ToString(),
                            rrd_HQty = dr["rrd_HQty"].ToString(),
                            rrd_LUOM = dr["rrd_LUOM"].ToString(),
                            rrd_LQty = dr["rrd_LQty"].ToString(),
                            ReasonCode = dr["ReasonCode"].ToString(),
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            prd_Spec = dr["prd_Spec"].ToString(),
                            CategoryId = dr["prd_cat_ID"].ToString(),
                            SubcategoryId = dr["prd_sub_ID"].ToString(),
                            Return_HQty = dr["rtd_HigherQty"].ToString(),
                            Return_HUOM = dr["rtd_HigherUOM"].ToString(),
                            Return_LQty = dr["rtd_LowerQty"].ToString(),
                            Return_LUOM = dr["rtd_LowerUOM"].ToString(),
                           
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
                dm.TraceService(" GetKPIReturnDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetKPIReturnDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        //KPI Styrofoam Invoices
        public string SelKPIStyrofoamInvoices([FromForm] KPIStyrofoamInvIn inputParams)
        {
            dm.TraceService("SelKPIStyrofoamInvoices STARTED");
            dm.TraceService("====================");

            try
            {
                string udpID = inputParams.udpID == null ? "0" : inputParams.udpID;
                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

                string[] arr = { userID.ToString() };
                DataSet dt = dm.loadListDS("SelKPIStyrofoamInvoices", "sp_KPIHistory", udpID.ToString() , arr);
                DataTable InvData = dt.Tables[0];
                DataTable ItemData = dt.Tables[1];

                if (InvData.Rows.Count > 0)
                {
                    List<KPIStyrofoamInvOut> listItems = new List<KPIStyrofoamInvOut>();
                    foreach (DataRow dr in InvData.Rows)
                    {
                        List<KPIStyrofoamInvDetailOut> listStyrofoamInvDetail = new List<KPIStyrofoamInvDetailOut>();
                        foreach (DataRow drDetails in ItemData.Rows)
                        {
                            if (dr["psh_ID"].ToString() == drDetails["psd_psh_ID"].ToString())
                            {
                                listStyrofoamInvDetail.Add(new KPIStyrofoamInvDetailOut
                                { 
                                    psd_ID = drDetails["psd_ID"].ToString(),
                                    psd_psh_ID = drDetails["psd_psh_ID"].ToString(),
                                    prd_ID = drDetails["prd_ID"].ToString(),
                                    prd_Code = drDetails["prd_Code"].ToString(),
                                    prd_Name = drDetails["prd_Name"].ToString(),
                                    prd_Desc = drDetails["prd_Desc"].ToString(),
                                    psd_Qty = drDetails["psd_Qty"].ToString(),
                                
                                });
                            }
                        }

                        listItems.Add(new KPIStyrofoamInvOut
                        {
                            psh_ID = dr["psh_ID"].ToString(),
                            psh_Number = dr["psh_Number"].ToString(),
                            psh_inv_ID = dr["psh_inv_ID"].ToString(),
                            inv_InvoiceID = dr["inv_InvoiceID"].ToString(),
                            psh_udp_ID = dr["psh_udp_ID"].ToString(),
                            rot_ID = dr["rot_ID"].ToString(),
                            rot_Code = dr["rot_Code"].ToString(),
                            rot_Name = dr["rot_Name"].ToString(),
                            CustomerID = dr["csh_ID"].ToString(),
                            CustomerCode = dr["csh_Code"].ToString(),
                            CustomerName = dr["csh_Name"].ToString(),
                            OutletID = dr["cus_ID"].ToString(),
                            OutletCode = dr["cus_Code"].ToString(),
                            OutletName = dr["cus_Name"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            StyrofoamDetail = listStyrofoamInvDetail,
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
                dm.TraceService("SelKPIStyrofoamInvoices Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelKPIStyrofoamInvoices ENDED");
            dm.TraceService("==================");


            return JSONString;
        }

    }
}