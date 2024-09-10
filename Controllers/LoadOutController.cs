using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers
{
    public class LoadOutController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        [HttpPost]

        public string GetLoadOutCount([FromForm] LoadOutInpara inputParams)
        {
            dm.TraceService("GetLoadOutCount STARTED ");
            dm.TraceService("====================");
            try
            {

                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

                string rotID = inputParams.rotID == null ? "0" : inputParams.rotID;
                string udpID = inputParams.udpID == null ? "0" : inputParams.udpID;
                string[] ar = { rotID, udpID };

                DataTable dtLoadOut = dm.loadList("SelLoadOutCounts", "sp_LoadOut", userID.ToString(), ar);

                if (dtLoadOut.Rows.Count > 0)
                {
                    List<LoadOutOutpara> listItems = new List<LoadOutOutpara>();
                    foreach (DataRow dr in dtLoadOut.Rows)
                    {

                        listItems.Add(new LoadOutOutpara
                        {
                            NDCount = dr["NDCount"].ToString(),
                            NPCount = dr["NPCount"].ToString(),
                            PDCount = dr["PDCount"].ToString(),
                            FDCount = dr["FDCount"].ToString(),
                            RtnCount = dr["RtnCount"].ToString(),


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
                dm.TraceService(" GetLoadOutCount Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetLoadOutCount ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetLoadOutFD([FromForm] LoadOutFDInpara inputParams)
        {
            dm.TraceService("GetLoadOutFD STARTED ");
            dm.TraceService("====================");
            try
            {

                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

                string rotID = inputParams.rotID == null ? "0" : inputParams.rotID;
                string[] ar = { rotID };

                DataTable dtLoadOut = dm.loadList("SelLOFD", "sp_LoadOut", userID.ToString(), ar);

                if (dtLoadOut.Rows.Count > 0)
                {
                    List<LoadOutFDOutpara> listItems = new List<LoadOutFDOutpara>();
                    foreach (DataRow dr in dtLoadOut.Rows)
                    {

                        listItems.Add(new LoadOutFDOutpara
                        {
                            DispatchNumber = dr["dsp_DispatchID"].ToString(),
                            Date = dr["Date"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            dln_ID = dr["dln_ID"].ToString(),
                            ord_ID = dr["ord_ID"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),


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
                dm.TraceService(" GetLoadOutFD Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetLoadOutFD ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetLoadOutPD([FromForm] LoadOutPDInpara inputParams)
        {
            dm.TraceService("GetLoadOutPD STARTED ");
            dm.TraceService("====================");
            try
            {
                dm.TraceService("UserId -  " + inputParams.UserId);
                dm.TraceService("rotID -  " + inputParams.rotID);

                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

                string rotID = inputParams.rotID == null ? "0" : inputParams.rotID;
                string[] ar = { rotID };

                DataTable dtLoadOut = dm.loadList("SelLOPD", "sp_LoadOut", userID.ToString(), ar);

                if (dtLoadOut.Rows.Count > 0)
                {
                    List<LoadOutPDOutpara> listItems = new List<LoadOutPDOutpara>();
                    foreach (DataRow dr in dtLoadOut.Rows)
                    {

                        listItems.Add(new LoadOutPDOutpara
                        {
                            DispatchNumber = dr["dsp_DispatchID"].ToString(),
                            Date = dr["Date"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            dln_ID = dr["dln_ID"].ToString(),
                            ord_ID = dr["ord_ID"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),
                            ReadyToLoadOut = dr["dsp_ReadyToLoadOut"].ToString(),
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
                dm.TraceService(" GetLoadOutPD Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetLoadOutPD ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetLoadOutND([FromForm] LoadOutNDInpara inputParams)
        {
            dm.TraceService("GetLoadOutND STARTED ");
            dm.TraceService("====================");
            try
            {

                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

                string rotID = inputParams.rotID == null ? "0" : inputParams.rotID;
                string[] ar = { rotID };

                DataTable dtLoadOut = dm.loadList("SelLOND", "sp_LoadOut", userID.ToString(), ar);

                if (dtLoadOut.Rows.Count > 0)
                {
                    List<LoadOutNDOutpara> listItems = new List<LoadOutNDOutpara>();
                    foreach (DataRow dr in dtLoadOut.Rows)
                    {

                        listItems.Add(new LoadOutNDOutpara
                        {
                            DispatchNumber = dr["dsp_DispatchID"].ToString(),
                            Date = dr["Date"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            dsp_ID = dr["dsp_ID"].ToString(),
                            ord_ID = dr["ord_ID"].ToString(),
                            ReadyToLoadOut = dr["dsp_ReadyToLoadOut"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),
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
                dm.TraceService(" GetLoadOutND Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetLoadOutND ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetLoadOutNP([FromForm] LoadOutNPInpara inputParams)
        {
            dm.TraceService("GetLoadOutNP STARTED ");
            dm.TraceService("====================");
            try
            {

                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

                string rotID = inputParams.rotID == null ? "0" : inputParams.rotID;
                string[] ar = { rotID };

                DataTable dtLoadOut = dm.loadList("SelLONP", "sp_LoadOut", userID.ToString(), ar);

                if (dtLoadOut.Rows.Count > 0)
                {
                    List<LoadOutNPOutpara> listItems = new List<LoadOutNPOutpara>();
                    foreach (DataRow dr in dtLoadOut.Rows)
                    {

                        listItems.Add(new LoadOutNPOutpara
                        {
                            DispatchNumber = dr["dsp_DispatchID"].ToString(),
                            Date = dr["Date"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            dsp_ID = dr["dsp_ID"].ToString(),
                            ord_ID = dr["ord_ID"].ToString(),
                            ReadyToLoadOut = dr["dsp_ReadyToLoadOut"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),

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
                dm.TraceService(" GetLoadOutNP Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetLoadOutNP ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetLoadOutRTN([FromForm] LoadOutRInpara inputParams)
        {
            dm.TraceService("GetLoadOutRTN STARTED ");
            dm.TraceService("====================");
            try
            {

                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

                string udpID = inputParams.udpID == null ? "0" : inputParams.udpID;
                string[] ar = { udpID };

                DataTable dtLoadOut = dm.loadList("SelLOR", "sp_LoadOut", userID.ToString(), ar);

                if (dtLoadOut.Rows.Count > 0)
                {
                    List<LoadOutROutpara> listItems = new List<LoadOutROutpara>();
                    foreach (DataRow dr in dtLoadOut.Rows)
                    {

                        listItems.Add(new LoadOutROutpara
                        {
                            rtn_Number = dr["rtn_Number"].ToString(),
                            Date = dr["Date"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            rtn_ID = dr["rtn_ID"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),
                            RequestNumber = dr["rrh_RequestNumber"].ToString(),
                            ReadyToLoadOut = dr["rrh_ReadyToLoadOut"].ToString(),

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
                dm.TraceService(" GetLoadOutRTN Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetLoadOutRTN ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string GetLoadOutItemBatch([FromForm] PostLoadoutData inputParams)
        {
            dm.TraceService("GetItemBatch STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string dispatchID = inputParams.DispatchId == null ? "0" : inputParams.DispatchId;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

            string[] arr = { userID.ToString() };
            DataSet dtItemBatch = dm.loadListDS("SelLoadOutBatchSerial", "sp_LoadOut", dispatchID.ToString(), arr);
            DataTable itemData = dtItemBatch.Tables[0];
            DataTable batchData = dtItemBatch.Tables[1];
            //Items In Picklist 

            //Batch/Serial Number in Picklist against each item

            try
            {
                if (itemData.Rows.Count > 0)
                {
                    List<GetLoadOutItemHeader> listItems = new List<GetLoadOutItemHeader>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<GetLoadOutBatchSerial> listBatchSerial = new List<GetLoadOutBatchSerial>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["prd_Code"].ToString() == drDetails["prd_Code"].ToString() && dr["dsd_ID"].ToString() == drDetails["dbs_dsd_ID"].ToString())
                            {
                                listBatchSerial.Add(new GetLoadOutBatchSerial
                                {
                                    DispBatSerialId = drDetails["dbs_ID"].ToString(),
                                    DisDetailId = drDetails["dbs_dsd_ID"].ToString(),
                                    Number = drDetails["dbs_Number"].ToString(),
                                    ExpiryDate = drDetails["dbs_ExpiryDate"].ToString(),
                                    BaseUOM = drDetails["dbs_BaseUOM"].ToString(),
                                    OrderedQty = drDetails["dbs_OrderedQty"].ToString(),
                                    AdjustedQty = drDetails["dbs_AdjustedQty"].ToString(),
                                    LoadInQty = drDetails["dbs_LoadInQty"].ToString(),
                                    ItemCode = drDetails["prd_Code"].ToString(),
                                    LineNo = drDetails["dsd_LineNo"].ToString(),
                                });
                            }
                        }

                        listItems.Add(new GetLoadOutItemHeader
                        {
                            Id = Int32.Parse(dr["prd_ID"].ToString()),
                            Name = dr["prd_Name"].ToString(),
                            Code = dr["prd_Code"].ToString(),
                            Spec = dr["prd_Spec"].ToString(),
                            Warehouse = dr["war_Code"].ToString(),
                            Rack = dr["rak_Code"].ToString(),
                            Basket = dr["bas_Code"].ToString(),
                            LocationId = dr["plm_ID"].ToString(),
                            LocationName = dr["plm_Name"].ToString(),
                            SysHUOM = dr["dsd_HigherUOM"].ToString(),
                            SysLUOM = dr["dsd_LowerUOM"].ToString(),
                            SysHQty = dr["dsd_HigherQty"].ToString(),
                            SysLQty = dr["dsd_LowerQty"].ToString(),
                            AdjHUOM = dr["dsd_AdjustedHigherUOM"].ToString(),
                            AdjLUOM = dr["dsd_AdjustedLowerUOM"].ToString(),
                            AdjHQty = dr["dsd_AdjustedHigherQty"].ToString(),
                            AdjLQty = dr["dsd_AdjustedLowerQty"].ToString(),
                            LiHUOM = dr["dsd_FinalHigherUOM"].ToString(),
                            LiLUOM = dr["dsd_FinalLowerUOM"].ToString(),
                            LiHQty = dr["dsd_FinalHigherQty"].ToString(),
                            LiLQty = dr["dsd_FinalLowerQty"].ToString(),
                            PromoType = dr["dsd_TransType"].ToString(),
                            WeighingItem = dr["prd_WeighingItem"].ToString(),
                            LineNo = dr["dsd_LineNo"].ToString(),
                            BatchSerial = listBatchSerial,
                            prd_Desc = dr["prd_Desc"].ToString(),
                            prd_LongDesc = dr["prd_LongDesc"].ToString(),
                            prd_cat_id = dr["prd_cat_id"].ToString(),
                            prd_sub_ID = dr["prd_sub_ID"].ToString(),
                            prd_brd_ID = dr["prd_brd_ID"].ToString(),
                            prd_EnableOrderHold = dr["prd_EnableOrderHold"].ToString(),
                            prd_EnableReturnHold = dr["prd_EnableReturnHold"].ToString(),
                            prd_EnableDeliveryHold = dr["prd_EnableDeliveryHold"].ToString(),
                            prd_NameArabic = dr["prd_NameArabic"].ToString(),
                            prd_DescArabic = dr["prd_DescArabic"].ToString(),
                            prd_Image = dr["prd_Image"].ToString(),
                            prd_SortOrder = dr["prd_SortOrder"].ToString(),
                            brd_Code = dr["brd_Code"].ToString(),
                            brd_Name = dr["brd_Name"].ToString(),
                            prd_BaseUOM = dr["prd_BaseUOM"].ToString(),
                            VATPercent = dr["odd_VATPercent"].ToString(),
                            Price = dr["odd_Price"].ToString(),
                            Discount = dr["odd_Discount"].ToString(),
                            DiscountPercentage = dr["odd_DiscountPercentage"].ToString(),
                            UserReason = dr["UserReason"].ToString(),
                            UserReasonId = dr["UserReasonId"].ToString(),
                        });
                    }

                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    dm.TraceService("NoDataRes");
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                dm.TraceService(ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }

            return JSONString;
        }
        public string GetDeliveryItemBatch([FromForm] DeliveryItemBatchInpara inputParams)
        {
            dm.TraceService("GetItemBatch STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string dlnID = inputParams.dln_Id == null ? "0" : inputParams.dln_Id;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

            string[] arr = { userID.ToString() };
            DataSet dtItemBatch = dm.loadListDS("SelDeliveryBatchSerial", "sp_LoadOut", dlnID.ToString(), arr);
            DataTable itemData = dtItemBatch.Tables[0];
            DataTable batchData = dtItemBatch.Tables[1];
            //Items In Picklist 

            //Batch/Serial Number in Picklist against each item

            try
            {
                if (itemData.Rows.Count > 0)
                {
                    List<GetDeliveryItemHeader> listItems = new List<GetDeliveryItemHeader>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<GetDeliveryBatchSerial> listBatchSerial = new List<GetDeliveryBatchSerial>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["prd_Code"].ToString() == drDetails["prd_Code"].ToString() && dr["dld_ID"].ToString() == drDetails["dns_dld_ID"].ToString())
                            {
                                listBatchSerial.Add(new GetDeliveryBatchSerial
                                {
                                    DelBatSerialId = drDetails["dns_ID"].ToString(),
                                    DelDetailId = drDetails["dns_dld_ID"].ToString(),
                                    Number = drDetails["dns_Number"].ToString(),
                                    ExpiryDate = drDetails["dns_ExpiryDate"].ToString(),
                                    BaseUOM = drDetails["dns_BaseUOM"].ToString(),
                                    OrderedQty = drDetails["dns_OrderedQty"].ToString(),
                                    AdjustedQty = drDetails["dns_AdjustedQty"].ToString(),
                                    LoadInQty = drDetails["dns_LoadInQty"].ToString(),
                                    ItemCode = drDetails["prd_Code"].ToString(),
                                    LineNo= drDetails["dln_LineNo"].ToString(),

                                });
                            }
                        }

                        listItems.Add(new GetDeliveryItemHeader
                        {
                            Id = Int32.Parse(dr["prd_ID"].ToString()),
                            Name = dr["prd_Name"].ToString(),
                            Code = dr["prd_Code"].ToString(),
                            Spec = dr["prd_Spec"].ToString(),
                            Warehouse = dr["war_Code"].ToString(),
                            Rack = dr["rak_Code"].ToString(),
                            Basket = dr["bas_Code"].ToString(),
                            LocationId = dr["plm_ID"].ToString(),
                            LocationName = dr["plm_Name"].ToString(),
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
                            PromoType = dr["dld_TransType"].ToString(),
                            WeighingItem = dr["prd_WeighingItem"].ToString(),
                            LineNo = dr["dln_LineNo"].ToString(),
                            BatchSerial = listBatchSerial,
                            prd_Desc = dr["prd_Desc"].ToString(),
                            prd_LongDesc = dr["prd_LongDesc"].ToString(),
                            prd_cat_id = dr["prd_cat_id"].ToString(),
                            prd_sub_ID = dr["prd_sub_ID"].ToString(),
                            prd_brd_ID = dr["prd_brd_ID"].ToString(),
                            prd_EnableOrderHold = dr["prd_EnableOrderHold"].ToString(),
                            prd_EnableReturnHold = dr["prd_EnableReturnHold"].ToString(),
                            prd_EnableDeliveryHold = dr["prd_EnableDeliveryHold"].ToString(),
                            prd_NameArabic = dr["prd_NameArabic"].ToString(),
                            prd_DescArabic = dr["prd_DescArabic"].ToString(),
                            prd_Image = dr["prd_Image"].ToString(),
                            prd_SortOrder = dr["prd_SortOrder"].ToString(),
                            brd_Code = dr["brd_Code"].ToString(),
                            brd_Name = dr["brd_Name"].ToString(),
                            prd_BaseUOM = dr["prd_BaseUOM"].ToString(),
                            VATPercent = dr["odd_VATPercent"].ToString(),
                            Price = dr["odd_Price"].ToString(),
                            UserReason = dr["UserReason"].ToString(),
                            ApprovalReason = dr["ApprovalReason"].ToString(),
                            UserReasonId = dr["UserReasonId"].ToString(),
                            ApprovalReasonId = dr["ApprovalReasonId"].ToString(),
                        });
                    }

                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    dm.TraceService("NoDataRes");
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                dm.TraceService(ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }

            return JSONString;
        }

        public string GetReturnItemBatch([FromForm] ReturnItemBatchInpara inputParams)
        {
            dm.TraceService("GetReturnItemBatch STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string dlnID = inputParams.rtn_Id == null ? "0" : inputParams.rtn_Id;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

            string[] arr = { userID.ToString() };
            DataSet dtItemBatch = dm.loadListDS("SelReturnBatchdDetail", "sp_LoadOut", dlnID.ToString(), arr);
            DataTable itemData = dtItemBatch.Tables[0];
            DataTable batchData = dtItemBatch.Tables[1];
            //Items In Picklist 

            //Batch/Serial Number in Picklist against each item

            try
            {
                if (itemData.Rows.Count > 0)
                {
                    List<GetReturnItemHeader> listItems = new List<GetReturnItemHeader>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<GetRtnBatchSerial> listBatchSerial = new List<GetRtnBatchSerial>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["prd_Code"].ToString() == drDetails["prd_Code"].ToString() && dr["rtd_ID"].ToString() == drDetails["rbs_rtd_ID"].ToString())
                            {
                                listBatchSerial.Add(new GetRtnBatchSerial
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

                        listItems.Add(new GetReturnItemHeader
                        {
                            Id = Int32.Parse(dr["prd_ID"].ToString()),
                            Name = dr["prd_Name"].ToString(),
                            Code = dr["prd_Code"].ToString(),
                            Spec = dr["prd_Spec"].ToString(),
                            Warehouse = dr["war_Code"].ToString(),
                            Rack = dr["rak_Code"].ToString(),
                            Basket = dr["bas_Code"].ToString(),
                            LocationId = dr["plm_ID"].ToString(),
                            LocationName = dr["plm_Name"].ToString(),
                            SysHUOM = dr["rtd_HigherUOM"].ToString(),
                            SysLUOM = dr["rtd_LowerUOM"].ToString(),
                            SysHQty = dr["rtd_HigherQty"].ToString(),
                            SysLQty = dr["rtd_LowerQty"].ToString(),
                            PromoType = dr["rtd_TransType"].ToString(),
                            WeighingItem = dr["prd_WeighingItem"].ToString(),

                            BatchSerial = listBatchSerial,
                            prd_Desc = dr["prd_Desc"].ToString(),
                            prd_LongDesc = dr["prd_LongDesc"].ToString(),
                            prd_cat_id = dr["prd_cat_id"].ToString(),
                            prd_sub_ID = dr["prd_sub_ID"].ToString(),
                            prd_brd_ID = dr["prd_brd_ID"].ToString(),
                            prd_EnableOrderHold = dr["prd_EnableOrderHold"].ToString(),
                            prd_EnableReturnHold = dr["prd_EnableReturnHold"].ToString(),
                            prd_EnableDeliveryHold = dr["prd_EnableDeliveryHold"].ToString(),
                            prd_NameArabic = dr["prd_NameArabic"].ToString(),
                            prd_DescArabic = dr["prd_DescArabic"].ToString(),
                            prd_Image = dr["prd_Image"].ToString(),
                            prd_SortOrder = dr["prd_SortOrder"].ToString(),
                            brd_Code = dr["brd_Code"].ToString(),
                            brd_Name = dr["brd_Name"].ToString(),
                            prd_BaseUOM = dr["prd_BaseUOM"].ToString(),
                            ApprovalReason =  dr["ApprovalReason"].ToString(),
                            ApprovalReasonID = dr["rad_ApprovalReason_ID"].ToString(),
                            UserReason = dr["UserReason"].ToString(),
                            UserReasonID = dr["rad_UserReason_ID"].ToString(),
                           
                        });
                    }

                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });

                    return JSONString;
                }
                else
                {
                    dm.TraceService("NoDataRes");
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                dm.TraceService(ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }
            dm.TraceService("GetReturnItemBatch ENDED - " + DateTime.Now);
            dm.TraceService("==================");

            return JSONString;
        }

        public string InsLoadOutCarryForward([FromForm] LoadOutCFinpara inputParams)
        {
            dm.TraceService("InsLoadOutCarryForward STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            try
            {


                string Date = inputParams.date == null ? "0" : inputParams.date;
                string dspID = inputParams.dsp_ID == null ? "0" : inputParams.dsp_ID;
                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
                string ApprovedBy = inputParams.ApprovedBy == null ? "0" : inputParams.ApprovedBy;

                string[] arr = { Date.ToString(), userID.ToString() , ApprovedBy.ToString()};
                DataTable dtLoadOut = dm.loadList("InsLoadOutCarryForward", "sp_LoadOut", dspID.ToString(), arr);

                if (dtLoadOut.Rows.Count > 0)
                {
                    List<LoadOutCFoutpara> listLoadOut = new List<LoadOutCFoutpara>();
                    foreach (DataRow dr in dtLoadOut.Rows)
                    {
                        listLoadOut.Add(new LoadOutCFoutpara
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),
                            Descr = dr["Descr"].ToString()


                        });
                    }
                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listLoadOut
                    });

                    return JSONString;
                }
                else
                {
                    JSONString = "NoDataRes";
                    dm.TraceService("NoDataRes");
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" InsLoadOutCarryForward Exception - " + ex.Message.ToString());
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsLoadOutCarryForward ENDED - " + DateTime.Now.ToString());
            dm.TraceService("==================");
            return JSONString;
        }
    }
   
}