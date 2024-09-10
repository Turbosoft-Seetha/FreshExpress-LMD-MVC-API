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
    public class ActionHistoryController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        [HttpPost]

        //Delivery
        public string GetActionHistoryDeliveryHeader([FromForm] DeliveryHeaderIn inputParams)
        {
            dm.TraceService("GetActionHistoryDeliveryHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rot_ID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;

                string[] arr = { userID.ToString() };

                DataTable dtTrnsIn = dm.loadList("SelActionHistoryDeliveryHeader", "sp_ActionHistory", rot_ID.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostDeliveryHeaderOut> listItems = new List<PostDeliveryHeaderOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostDeliveryHeaderOut
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
                dm.TraceService(" GetActionHistoryDeliveryHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetActionHistoryDeliveryHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetActionHistoryDeliveryDetail([FromForm] DeliveryDetailIn inputParams)
        {
            dm.TraceService("MainRouteDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string dln_ID = inputParams.dln_ID == null ? "0" : inputParams.dln_ID;

                string[] arr = { };
                DataSet dtTrnsIn = dm.loadListDS("SelActionHistoryDeliveryDetail", "sp_ActionHistory", dln_ID.ToString(), arr);
                DataTable itemData = dtTrnsIn.Tables[0];
                DataTable batchData = dtTrnsIn.Tables[1];
                if (itemData.Rows.Count > 0)
                {
                    List<PostDeliveryDetailOut> listItems = new List<PostDeliveryDetailOut>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<GetDelItemBatchSerial> listBatchSerial = new List<GetDelItemBatchSerial>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["dld_ID"].ToString() == drDetails["dns_dld_ID"].ToString())
                            {
                                listBatchSerial.Add(new GetDelItemBatchSerial
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
                      
                            listItems.Add(new PostDeliveryDetailOut
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
                dm.TraceService(" MainRouteDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("MainRouteDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetActionHistoryCustomerDeliveryHeader([FromForm] DeliveryCustomerHeaderIn inputParams)
        {
            dm.TraceService("GetActionHistoryCustomerDeliveryHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rot_ID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
                string cus_ID = inputParams.cus_ID == null ? "0" : inputParams.cus_ID;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;

                string[] arr = { cus_ID.ToString(), userID.ToString() };

                DataTable dtTrnsIn = dm.loadList("SelActionHistoryCustomerDeliveryHeader", "sp_ActionHistory", rot_ID.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostCustomerDeliveryHeaderOut> listItems = new List<PostCustomerDeliveryHeaderOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostCustomerDeliveryHeaderOut
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
                dm.TraceService(" GetActionHistoryCustomerDeliveryHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetActionHistoryCustomerDeliveryHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
 
        //Delivery Helper
        public string ActionHistoryHelperRouteHeader([FromForm] PostHelperRotIn inputParams)
        {
            dm.TraceService("HelperRouteHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotId == null ? "0" : inputParams.rotId;

                DataTable dtTrnsIn = dm.loadList("SelHelperHeader", "sp_ActionHistory", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostHelperHeader> listItems = new List<PostHelperHeader>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostHelperHeader
                        {
                            TransID = dr["trm_TransID"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            Status = dr["Status"].ToString(),
                            MainRot = dr["MainRot"].ToString(),
                            HeaderID = dr["trm_ID"].ToString(),


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
                dm.TraceService(" HelperRouteHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("HelperRouteHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string ActionHistoryHelperRouteDetail([FromForm] PostHelperRotDetailIn inputParams)
        {
            dm.TraceService("ActionHistoryHelperRouteDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string HeaderID = inputParams.HeaderId == null ? "0" : inputParams.HeaderId;

                DataTable dtTrnsIn = dm.loadList("SelHelperDetail", "sp_ActionHistory", HeaderID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostHelperDetail> listItems = new List<PostHelperDetail>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostHelperDetail
                        {
                            HeaderID = dr["trd_trm_ID"].ToString(),
                            DespatchId = dr["dsp_ID"].ToString(),
                            PickingLocation = dr["PickLocation"].ToString(),
                            CusHeaderCode = dr["csh_Code"].ToString(),
                            CusHeaderName = dr["csh_Name"].ToString(),
                            CustomerCode = dr["cus_Code"].ToString(),
                            CustomerName = dr["cus_Name"].ToString(),
                            Date = dr["Date"].ToString(),
                            Time = dr["Time"].ToString(),
                            dsp_DispatchID = dr["dsp_DispatchID"].ToString(),
                            CustomerID = dr["cus_ID"].ToString(),
                            CusHeaderID = dr["csh_ID"].ToString(),
                            PickLocationID = dr["PickLocationID"].ToString(),
                            Status = dr["Status"].ToString(),
                            IsPartiallyDelivered = dr["dln_IsPartiallyDelivered"].ToString(),
                            dln_ID = dr["dln_ID"].ToString(),
                            dln_DeliveryNumber = dr["dln_DeliveryNumber"].ToString(),
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
                dm.TraceService(" ActionHistoryHelperRouteDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("ActionHistoryHelperRouteDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string ActionHistoryMainRouteHeader([FromForm] PostHelperRotIn inputParams)
        {
            dm.TraceService("HelperRouteHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotId == null ? "0" : inputParams.rotId;

                DataTable dtTrnsIn = dm.loadList("SelMainHeader", "sp_ActionHistory", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostMainHeaderOut> listItems = new List<PostMainHeaderOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostMainHeaderOut
                        {
                            TransID = dr["trm_TransID"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            Status = dr["Status"].ToString(),
                            HelperRot = dr["HelperRot"].ToString(),
                            HeaderID = dr["trm_ID"].ToString(),


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
                dm.TraceService(" HelperRouteHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("HelperRouteHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string MainRouteHeader([FromForm] MainRouteHead inputParams)
        {
            dm.TraceService("HelperRouteHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotId == null ? "0" : inputParams.rotId;

                DataTable dtTrnsIn = dm.loadList("SelTransferOutHeader", "sp_ActionHistory", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<MainRoutOut> listItems = new List<MainRoutOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new MainRoutOut
                        {
                            TransID = dr["thh_TransID"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            Status = dr["Status"].ToString(),
                            Rot = dr["HelperRot"].ToString(),
                            HeaderID = dr["thh_ID"].ToString(),


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
                dm.TraceService(" HelperRouteHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("HelperRouteHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        //Invoice
        public string GetActionHistoryInvoiceHeader([FromForm] InvoiceHeaderIn inputParams)
        {
            dm.TraceService("GetActionHistoryInvoiceHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string udp_ID = inputParams.udp_ID == null ? "0" : inputParams.udp_ID;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;

                string[] arr = { userID.ToString() };

                DataTable dtTrnsIn = dm.loadList("SelActionHistoryInvoiceHeader", "sp_ActionHistory", udp_ID.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostInvoiceHeaderOut> listItems = new List<PostInvoiceHeaderOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostInvoiceHeaderOut
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
                dm.TraceService(" GetActionHistoryInvoiceHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetActionHistoryInvoiceHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetActionHistoryInvoiceDetail([FromForm] InvoiceDetailIn inputParams)
        {
            dm.TraceService("MainRouteDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string InvID = inputParams.InvID == null ? "0" : inputParams.InvID;

                DataTable dtTrnsIn = dm.loadList("SelActionHistoryInvoiceDetail", "sp_ActionHistory", InvID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostInvoiceDetailOut> listItems = new List<PostInvoiceDetailOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostInvoiceDetailOut
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

        //Return
        public string GetActionHistoryReturnHeader([FromForm] ReturnHeaderIn inputParams)
        {
            dm.TraceService("GetActionHistoryReturnHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.RotID == null ? "0" : inputParams.RotID;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;

                string[] arr = { userID.ToString() };

                DataTable dtTrnsIn = dm.loadList("SelActionHistoryReturnHeader", "sp_ActionHistory", RotID.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<ReturnHeaderOut> listItems = new List<ReturnHeaderOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new ReturnHeaderOut
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
                dm.TraceService(" GetActionHistoryReturnHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetActionHistoryReturnHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetActionHistoryCusReturnHeader([FromForm] CusReturnHeaderIn inputParams)
        {
            dm.TraceService("GetActionHistoryCusReturnHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.RotID == null ? "0" : inputParams.RotID;
                string cusID = inputParams.cusID == null ? "0" : inputParams.cusID;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;

                string[] arr = { cusID.ToString() , userID.ToString() };

                DataTable dtTrnsIn = dm.loadList("SelActionHistoryCusReturnHeader", "sp_ActionHistory", RotID.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<ReturnHeaderOut> listItems = new List<ReturnHeaderOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new ReturnHeaderOut
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
                dm.TraceService(" GetActionHistoryCusReturnHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetActionHistoryCusReturnHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetActionHistoryReturnDetail([FromForm] ReturnDetailIn inputParams)
        {
            dm.TraceService("GetActionHistoryReturnDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rrh_ID = inputParams.rrh_ID == null ? "0" : inputParams.rrh_ID;

              
                string[] arr = { };
                DataSet dtTrnsIn = dm.loadListDS("SelActionHistoryReturnDetail", "sp_ActionHistory", rrh_ID.ToString(), arr);
                DataTable itemData = dtTrnsIn.Tables[0];
                DataTable batchData = dtTrnsIn.Tables[1];


                if (itemData.Rows.Count > 0)
                {
                    List<ReturnDetailOut> listItems = new List<ReturnDetailOut>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<GetReturnItemBatchSerial> listBatchSerial = new List<GetReturnItemBatchSerial>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["prd_Code"].ToString() == drDetails["prd_Code"].ToString() && dr["rtd_ID"].ToString() == drDetails["rbs_rtd_ID"].ToString())
                            {
                                listBatchSerial.Add(new GetReturnItemBatchSerial
                                {
                                    RtnBatSerialId = drDetails["rbs_ID"].ToString(),
                                    RtnDetailId = drDetails["rbs_rtd_ID"].ToString(),
                                    Number = drDetails["rbs_Number"].ToString(),
                                    ExpiryDate = drDetails["rbs_ExpiryDate"].ToString(),
                                    BaseUOM = drDetails["rbs_BaseUOM"].ToString(),
                                    RtnQty = drDetails["rbs_ReturnedQty"].ToString(),
                                    ItemCode = drDetails["prd_Code"].ToString()


                                });
                            }
                        }

                        listItems.Add(new ReturnDetailOut
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
                dm.TraceService(" GetActionHistoryReturnDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetActionHistoryReturnDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        //Return Helper
        public string ActionHistoryReturnHelperRouteHeader([FromForm] PostReturnHelperRotIn inputParams)
        {
            dm.TraceService("ActionHistoryReturnHelperRouteHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotId == null ? "0" : inputParams.rotId;

                DataTable dtTrnsIn = dm.loadList("SelReturnHelperHeader", "sp_ActionHistory", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostReturnHelperHeader> listItems = new List<PostReturnHelperHeader>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostReturnHelperHeader
                        {
                            TransID = dr["tmr_TransID"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            Status = dr["Status"].ToString(),
                            MainRot = dr["MainRot"].ToString(),
                            HeaderID = dr["tmr_ID"].ToString(),


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
                dm.TraceService(" ActionHistoryReturnHelperRouteHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("ActionHistoryReturnHelperRouteHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string ActionHistoryReturnHelperRouteDetail([FromForm] PostReturnHelperRotDetailIn inputParams)
        {
            dm.TraceService("ActionHistoryReturnHelperRouteDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string HeaderID = inputParams.HeaderId == null ? "0" : inputParams.HeaderId;

                DataTable dtTrnsIn = dm.loadList("SelReturnHelperDetail", "sp_ActionHistory", HeaderID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostReturnHelperDetail> listItems = new List<PostReturnHelperDetail>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostReturnHelperDetail
                        {
                            HeaderID = dr["tmd_tmr_ID"].ToString(),
                            ReturnRequestId = dr["rrh_ID"].ToString(),
                            rrh_RequestNumber = dr["rrh_RequestNumber"].ToString(),
                            CusHeaderCode = dr["csh_Code"].ToString(),
                            CusHeaderName = dr["csh_Name"].ToString(),
                            CustomerCode = dr["cus_Code"].ToString(),
                            CustomerName = dr["cus_Name"].ToString(),
                            CustomerID = dr["cus_ID"].ToString(),
                            CusHeaderID = dr["csh_ID"].ToString(),
                            Date = dr["Date"].ToString(),
                            Time = dr["Time"].ToString(),
                            inv_InvoiceID = dr["inv_InvoiceID"].ToString(),
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
                dm.TraceService(" ActionHistoryReturnHelperRouteDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("ActionHistoryReturnHelperRouteDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string ActionHistoryReturnMainRouteHeader([FromForm] PostReturnHelperRotIn inputParams)
        {
            dm.TraceService("ActionHistoryReturnMainRouteHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotId == null ? "0" : inputParams.rotId;

                DataTable dtTrnsIn = dm.loadList("SelReturnMainHeader", "sp_ActionHistory", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostReturnMainHeaderOut> listItems = new List<PostReturnMainHeaderOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostReturnMainHeaderOut
                        {
                            TransID = dr["tmr_TransID"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            Status = dr["Status"].ToString(),
                            HelperRot = dr["HelperRot"].ToString(),
                            HeaderID = dr["tmr_ID"].ToString(),


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
                dm.TraceService(" ActionHistoryReturnMainRouteHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("ActionHistoryReturnMainRouteHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string ReturnMainRouteHeader([FromForm] ReturnMainRouteHead inputParams)
        {
            dm.TraceService("ReturnMainRouteHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotId == null ? "0" : inputParams.rotId;

                DataTable dtTrnsIn = dm.loadList("SelReturnTransferOutHeader", "sp_ActionHistory", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<ReturnMainRoutOut> listItems = new List<ReturnMainRoutOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new ReturnMainRoutOut
                        {
                            TransID = dr["thr_TransID"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            Status = dr["Status"].ToString(),
                            Rot = dr["HelperRot"].ToString(),
                            HeaderID = dr["thr_ID"].ToString(),


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
                dm.TraceService(" ReturnMainRouteHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("ReturnMainRouteHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        
        //Styrofoam Invoices
        public string SelActionHistoryStyrofoamInvoices([FromForm] PostStyrofoamInvIn inputParams)
        {
            dm.TraceService("SelActionHistoryStyrofoamInvoices STARTED");
            dm.TraceService("====================");

            try
            {
                string udpID = inputParams.udpID == null ? "0" : inputParams.udpID;
                string cusID = inputParams.cusID == null ? "0" : inputParams.cusID;
                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

                string[] arr = { cusID.ToString() , userID.ToString() };
                DataSet dt = dm.loadListDS("SelCusStyrofoamInvoices", "sp_ActionHistory", udpID.ToString(), arr);
                DataTable InvData = dt.Tables[0];
                DataTable ItemData = dt.Tables[1];

                if (InvData.Rows.Count > 0)
                {
                    List<PostStyrofoamInvOut> listItems = new List<PostStyrofoamInvOut>();
                    foreach (DataRow dr in InvData.Rows)
                    {
                        List<PostStyrofoamInvDetailOut> listStyrofoamInvDetail = new List<PostStyrofoamInvDetailOut>();
                        foreach (DataRow drDetails in ItemData.Rows)
                        {
                            if (dr["psh_ID"].ToString() == drDetails["psd_psh_ID"].ToString())
                            {
                                listStyrofoamInvDetail.Add(new PostStyrofoamInvDetailOut
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

                        listItems.Add(new PostStyrofoamInvOut
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
                dm.TraceService("SelActionHistoryStyrofoamInvoices Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelActionHistoryStyrofoamInvoices ENDED");
            dm.TraceService("==================");


            return JSONString;
        }
    }
}