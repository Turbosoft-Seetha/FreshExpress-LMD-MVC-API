using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.DynamicData;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers
{
    public class VanToVanController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        [HttpPost]
       
        public string VanToVanTransferINHeader([FromForm] VanToVanTransIn inputParams)
        {
            dm.TraceService("VanToVanTransferINHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotId == null ? "0" : inputParams.rotId;
                
                DataTable dtTrnsIn = dm.loadList("SelVantoVanTransferInHeader", "sp_VanToVan", RotID.ToString());
               
                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<VanParams> listItems = new List<VanParams>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new VanParams
                        {
                            TransID = dr["vvh_TransID"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            Status = dr["Status"].ToString(),
                            Rot = dr["FromRot"].ToString(),
                            HeaderID= dr["vvh_ID"].ToString(),
                            RotID = dr["FromRotID"].ToString(),

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
                dm.TraceService(" VanToVanTransferINHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("VanToVanTransferINHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string InsVanToVanTransferOut([FromForm] TransferOut inputParams)
        {
            dm.TraceService("InsVanToVanTransferOut STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            try { 
            List<DispatchIDs> itemData = JsonConvert.DeserializeObject<List<DispatchIDs>>(inputParams.Dispatches);

            string UdpID = inputParams.UdpID == null ? "0" : inputParams.UdpID;
            string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;
            string FromRot = inputParams.FromRot == null ? "0" : inputParams.FromRot;
            string ToRot = inputParams.ToRot == null ? "0" : inputParams.ToRot;

            string CreatedDate = inputParams.CreatedDate == null ? "0" : inputParams.CreatedDate;
            string userID = inputParams.userID == null ? "0" : inputParams.userID;
           // string Dispatche=inputParams.Dispatches == null ?"0":inputParams.Dispatches.ToString();
            //[ { "dspID: 1" , "asd" : 12 , "sad" [{asda : as} , {asda : as} ]} , { "dspID: 1" , "asd" : 12} ]

            string InputXml = "";
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (DispatchIDs id in itemData)
                        {
                            string[] arr = { id.dsp_ID.ToString() };
                            string[] arrName = { "dsp_ID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw.ToString();
                }



                string[] ar = { TransID.ToString(), FromRot.ToString(), ToRot.ToString(), CreatedDate.ToString(), userID.ToString(), InputXml.ToString() };
            DataTable dtVanToVan = dm.loadList("InsVanToVanTransferOut", "sp_VanToVan", UdpID.ToString(), ar);
            
                if (dtVanToVan.Rows.Count > 0)
                {
                    List<TransferOutOut> listTransout = new List<TransferOutOut>();
                    foreach (DataRow dr in dtVanToVan.Rows)
                    {
                        listTransout.Add(new TransferOutOut
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),
                            Descr = dr["Descr"].ToString()


                        });
                    }
                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listTransout
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
                dm.TraceService(" InsVanToVanTransferOut Exception - " + ex.Message.ToString());
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsVanToVanTransferOut ENDED - " + DateTime.Now.ToString());
            dm.TraceService("==================");
            return JSONString;
        }
        public string VanToVanTransferInUpdateStatus([FromForm] TransInUpdateStatus inputParams)
        {
            dm.TraceService("VanToVanTransferInUpdateStatus STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                List<DispatchIDs> itemData = JsonConvert.DeserializeObject<List<DispatchIDs>>(inputParams.Dispatch);
                string HeaderID = inputParams.HeaderID == null ? "0" : inputParams.HeaderID;
                string Status = inputParams.Status == null ? "0" : inputParams.Status;
                
                string DteTime = inputParams.DateTime == null ? "0" : inputParams.DateTime;
                string InputXml = "";
                if (Status == "RC")
                {
                    using (var sw = new StringWriter())
                    {
                        using (var writer = XmlWriter.Create(sw))
                        {

                            writer.WriteStartDocument(true);
                            writer.WriteStartElement("r");
                            int c = 0;
                            foreach (DispatchIDs id in itemData)
                            {
                                string[] arr = { id.dsp_ID.ToString() };
                                string[] arrName = { "dsp_ID" };
                                dm.createNode(arr, arrName, writer);
                            }

                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            writer.Close();
                        }
                        InputXml = sw.ToString();
                    }
                }
                string[] ar = {HeaderID.ToString(),Status.ToString(),DteTime.ToString()};
                DataTable dtTrnsIn = dm.loadList("UpdateVanToVanTransferInStatus", "sp_VanToVan",InputXml .ToString(),ar);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<TransferOutOut> listItems = new List<TransferOutOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new TransferOutOut
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),
                            Descr = dr["Descr"].ToString()



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
                dm.TraceService(" VanToVanTransferInUpdateStatus Exception - " + ex.Message.ToString());
            }
            dm.TraceService("VanToVanTransferInUpdateStatus ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string GetTransferInItemBatch([FromForm] PostTransferInData inputParams)
        {
            dm.TraceService("GetTransferInItemBatch STARTED " + DateTime.Now);
            dm.TraceService("============================");
            try
            {
                string HeaderID = inputParams.VanHeaderId == null ? "0" : inputParams.VanHeaderId;


                string[] arr = { };
                DataSet dtItemBatch = dm.loadListDS("SelTransferInItemBatch", "sp_VanToVan", HeaderID.ToString(),arr);
                DataTable TrnData = dtItemBatch.Tables[0];
                DataTable itemData = dtItemBatch.Tables[1];
                DataTable batchData = dtItemBatch.Tables[2];
                if (TrnData.Rows.Count > 0)
                {
                    List<VanListInfo> listPicking = new List<VanListInfo>();
                    foreach (DataRow drp in TrnData.Rows)
                    {
                        List<GetTransferInItemHeader> listItems = new List<GetTransferInItemHeader>();
                        foreach (DataRow dr in itemData.Rows)
                        {
                            List<GetTransferInBatchSerial> listBatchSerial = new List<GetTransferInBatchSerial>();
                            foreach (DataRow drDetails in batchData.Rows)
                            {
                                if ( dr["dsd_ID"].ToString() == drDetails["dbs_dsd_ID"].ToString())
                                {
                                    listBatchSerial.Add(new GetTransferInBatchSerial
                                    {
                                        Number = drDetails["dbs_Number"].ToString(),
                                        ExpiryDate = drDetails["dbs_ExpiryDate"].ToString(),
                                        BaseUOM = drDetails["dbs_BaseUOM"].ToString(),
                                        OrderedQty = drDetails["dbs_OrderedQty"].ToString(),
                                        LoadInQty = drDetails["dbs_LoadInQty"].ToString(),
                                        AdjustedQty = drDetails["dbs_AdjustedQty"].ToString(),
                                        ItemCode = drDetails["prd_Code"].ToString(),
                                        BatSerialId = drDetails["dbs_ID"].ToString(),
                                        DetailId = drDetails["dbs_dsd_ID"].ToString(),
                                        LineNo = drDetails["dsd_LineNo"].ToString(),
                                    });
                                }
                            }
                            if (drp["dsp_ID"].ToString() == dr["dsp_ID"].ToString())
                            {
                                listItems.Add(new GetTransferInItemHeader
                                {
                                    Id = Int32.Parse(dr["prd_ID"].ToString()),
                                    Name = dr["prd_Name"].ToString(),
                                    Code = dr["prd_Code"].ToString(),
                                    Spec = dr["prd_Spec"].ToString(),
                                    WarehouseId = dr["war_ID"].ToString(),
                                    Warehouse = dr["war_Code"].ToString(),
                                    RackId = dr["rak_ID"].ToString(),
                                    Rack = dr["rak_Code"].ToString(),
                                    BasketId = dr["bas_ID"].ToString(),
                                    Basket = dr["bas_Code"].ToString(),
                                    CategoryId = dr["prd_cat_ID"].ToString(),
                                    SubcategoryId = dr["prd_sub_ID"].ToString(),
                                    LocationId = dr["plm_ID"].ToString(),
                                    LocationName = dr["plm_Name"].ToString(),
                                    HigherUOM = dr["dsd_HigherUOM"].ToString(),
                                    LowerUOM = dr["dsd_LowerUOM"].ToString(),
                                    HigherQty = dr["dsd_HigherQty"].ToString(),
                                    LowerQty = dr["dsd_LowerQty"].ToString(),
                                    EnableExcess = dr["prd_EnableExcess"].ToString(),
                                    WeighingItem = dr["prd_WeighingItem"].ToString(),
                                    BatchSerial = listBatchSerial,
                                    PromoType = dr["dsd_TransType"].ToString(),
                                    LineNo = dr["dsd_LineNo"].ToString(),
                                    prd_Desc = dr["prd_Desc"].ToString(),
                                    prd_LongDesc = dr["prd_LongDesc"].ToString(),
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
                                });
                            }
                        }
                        listPicking.Add(new VanListInfo
                        {
                            DespatchId = Int32.Parse(drp["dsp_ID"].ToString()),
                            VanHeaderID = drp["vvd_vvh_ID"].ToString(),
                            OrderId = drp["ord_ID"].ToString(),
                            PickingLocation = drp["PickLocation"].ToString(),
                            CusHeaderCode = drp["csh_Code"].ToString(),
                            CusHeaderName = drp["csh_Name"].ToString(),
                            CustomerCode = drp["cus_Code"].ToString(),
                            CustomerName = drp["cus_Name"].ToString(),
                            Date = drp["Date"].ToString(),
                            Time = drp["Time"].ToString(),
                            dsp_DispatchID = drp["dsp_DispatchID"].ToString(),
                            DespatchItems = listItems,                           
                            IsPickupOrder = drp["IsPickupOrder"].ToString(),                          
                            ord_dep_ID = drp["ord_dep_ID"].ToString(),
                            ord_CashPaidStatus = drp["ord_CashPaidStatus"].ToString(),
                            ord_PayMode = drp["ord_PayMode"].ToString(),
                            ord_LPONumber = drp["ord_LPONumber"].ToString(),
                            TotalAmount = drp["TotalAmount"].ToString(),
                            dsp_LOStatus = drp["dsp_LOStatus"].ToString(),
                            cus_ArabicName = drp["cus_ArabicName"].ToString(),
                            dsp_ArabicDeliveryType = drp["dsp_ArabicDeliveryType"].ToString(),
                            DeliveyType = drp["DeliveyType"].ToString(),
                            dsp_DeliveryType = drp["dsp_DeliveryType"].ToString(),
                            Status = drp["Status"].ToString(),
                            NeedDownload = drp["NeedDownload"].ToString(),
                            CustomerID = drp["cus_ID"].ToString(),
                            CusHeaderID = drp["csh_ID"].ToString(),
                            PickLocationID = drp["PickLocationID"].ToString(),
                            dln_DeliveryNumber = drp["dln_DeliveryNumber"].ToString(),
                            dln_ID = drp["dln_ID"].ToString(),
                            Type = drp["Type"].ToString(),
                            cus_Type = drp["cus_Type"].ToString(),
                        });
                    }

                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listPicking
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
                dm.TraceService("NoDataSQL - " + ex.Message.ToString());
            }

            dm.TraceService("GetTransferInItemBatch ENDED " + DateTime.Now);
            dm.TraceService("==========================");

            return JSONString;
        }
        public string VanToVanTransferOutUpdateStatus([FromForm] TransOutUpdateStatus inputParams)
        {
            dm.TraceService("VanToVanTransferOutUpdateStatus STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;
                string Status = inputParams.Status == null ? "0" : inputParams.Status;
            
                string DteTime = inputParams.DateTime == null ? "0" : inputParams.DateTime;
                
               
                string[] ar = {  Status.ToString(), DteTime.ToString() };
                DataTable dtTrnsIn = dm.loadList("UpdateVanToVanTransferOutStatus", "sp_VanToVan",TransID.ToString(), ar);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<TransferOutOut> listItems = new List<TransferOutOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new TransferOutOut
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),
                            Descr = dr["Descr"].ToString()



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
                dm.TraceService(" VanToVanTransferOutUpdateStatus Exception - " + ex.Message.ToString());
            }
            dm.TraceService("VanToVanTransferOutUpdateStatus ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string VanToVanTransferCheckStatus([FromForm] TransCheckStatus inputParams)
        {
            dm.TraceService("VanToVanTransferCheckStatus STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;
               

                
                DataTable dtTrnsIn = dm.loadList("VanToVanTransferCheckStatus", "sp_VanToVan", TransID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<TransCheckStatusOut> listItems = new List<TransCheckStatusOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new TransCheckStatusOut
                        {
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
                dm.TraceService(" VanToVanTransferCheckStatus Exception - " + ex.Message.ToString());
            }
            dm.TraceService("VanToVanTransferCheckStatus ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetTransferInConfirmedItemBatch([FromForm] TransCheckStatus inputParams)
        {
            dm.TraceService("GetTransferInConfirmedItemBatch STARTED " + DateTime.Now);
            dm.TraceService("============================");
            try
            {
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;


                string[] arr = { };
                DataSet dtItemBatch = dm.loadListDS("SelTransferInConfirmedItemBatch", "sp_VanToVan", TransID.ToString(), arr);
                DataTable TrnData = dtItemBatch.Tables[0];
                DataTable itemData = dtItemBatch.Tables[1];
                DataTable batchData = dtItemBatch.Tables[2];
                if (TrnData.Rows.Count > 0)
                {
                    List<VanListInfo> listPicking = new List<VanListInfo>();
                    foreach (DataRow drp in TrnData.Rows)
                    {
                        List<GetTransferInItemHeader> listItems = new List<GetTransferInItemHeader>();
                        foreach (DataRow dr in itemData.Rows)
                        {
                            List<GetTransferInBatchSerial> listBatchSerial = new List<GetTransferInBatchSerial>();
                            foreach (DataRow drDetails in batchData.Rows)
                            {
                                if (dr["dsd_ID"].ToString() == drDetails["dbs_dsd_ID"].ToString())
                                {
                                    listBatchSerial.Add(new GetTransferInBatchSerial
                                    {
                                        Number = drDetails["dbs_Number"].ToString(),
                                        ExpiryDate = drDetails["dbs_ExpiryDate"].ToString(),
                                        BaseUOM = drDetails["dbs_BaseUOM"].ToString(),
                                        OrderedQty = drDetails["dbs_OrderedQty"].ToString(),
                                        LoadInQty = drDetails["dbs_LoadInQty"].ToString(),
                                        AdjustedQty = drDetails["dbs_AdjustedQty"].ToString(),
                                        ItemCode = drDetails["prd_Code"].ToString(),
                                        BatSerialId = drDetails["dbs_ID"].ToString(),
                                        DetailId = drDetails["dbs_dsd_ID"].ToString(),
                                        LineNo = drDetails["dsd_LineNo"].ToString(),
                                    });
                                }
                            }
                            if (drp["dsp_ID"].ToString() == dr["dsp_ID"].ToString())
                            {
                                listItems.Add(new GetTransferInItemHeader
                                {
                                    Id = Int32.Parse(dr["prd_ID"].ToString()),
                                    Name = dr["prd_Name"].ToString(),
                                    Code = dr["prd_Code"].ToString(),
                                    Spec = dr["prd_Spec"].ToString(),
                                    WarehouseId = dr["war_ID"].ToString(),
                                    Warehouse = dr["war_Code"].ToString(),
                                    RackId = dr["rak_ID"].ToString(),
                                    Rack = dr["rak_Code"].ToString(),
                                    BasketId = dr["bas_ID"].ToString(),
                                    Basket = dr["bas_Code"].ToString(),
                                    CategoryId = dr["prd_cat_ID"].ToString(),
                                    SubcategoryId = dr["prd_sub_ID"].ToString(),
                                    LocationId = dr["plm_ID"].ToString(),
                                    LocationName = dr["plm_Name"].ToString(),
                                    HigherUOM = dr["dsd_HigherUOM"].ToString(),
                                    LowerUOM = dr["dsd_LowerUOM"].ToString(),
                                    HigherQty = dr["dsd_HigherQty"].ToString(),
                                    LowerQty = dr["dsd_LowerQty"].ToString(),
                                    EnableExcess = dr["prd_EnableExcess"].ToString(),
                                    WeighingItem = dr["prd_WeighingItem"].ToString(),
                                    BatchSerial = listBatchSerial,
                                    PromoType = dr["dsd_TransType"].ToString(),
                                    LineNo = dr["dsd_LineNo"].ToString(),
                                    prd_Desc = dr["prd_Desc"].ToString(),
                                    prd_LongDesc = dr["prd_LongDesc"].ToString(),                                    
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
                                });
                            }
                        }
                        listPicking.Add(new VanListInfo
                        {
                            DespatchId = Int32.Parse(drp["dsp_ID"].ToString()),
                            VanHeaderID = drp["vvd_vvh_ID"].ToString(),
                            OrderId = drp["ord_ID"].ToString(),
                            PickingLocation = drp["PickLocation"].ToString(),
                            CusHeaderCode = drp["csh_Code"].ToString(),
                            CusHeaderName = drp["csh_Name"].ToString(),
                            CustomerCode = drp["cus_Code"].ToString(),
                            CustomerName = drp["cus_Name"].ToString(),
                            Date = drp["Date"].ToString(),
                            Time = drp["Time"].ToString(),
                            dsp_DispatchID = drp["dsp_DispatchID"].ToString(),
                            DespatchItems = listItems,                           
                            IsPickupOrder = drp["IsPickupOrder"].ToString(),                           
                            ord_dep_ID = drp["ord_dep_ID"].ToString(),
                            ord_CashPaidStatus = drp["ord_CashPaidStatus"].ToString(),
                            ord_PayMode = drp["ord_PayMode"].ToString(),
                            ord_LPONumber = drp["ord_LPONumber"].ToString(),
                            TotalAmount = drp["TotalAmount"].ToString(),
                            dsp_LOStatus = drp["dsp_LOStatus"].ToString(),
                            cus_ArabicName = drp["cus_ArabicName"].ToString(),
                            dsp_ArabicDeliveryType = drp["dsp_ArabicDeliveryType"].ToString(),
                            DeliveyType = drp["DeliveyType"].ToString(),
                            dsp_DeliveryType = drp["dsp_DeliveryType"].ToString(),
                            Status = drp["Status"].ToString(),
                            NeedDownload = drp["NeedDownload"].ToString(),
                            CustomerID = drp["cus_ID"].ToString(),
                            CusHeaderID = drp["csh_ID"].ToString(),
                            PickLocationID = drp["PickLocationID"].ToString(),
                            dln_DeliveryNumber = drp["dln_DeliveryNumber"].ToString(),
                            dln_ID = drp["dln_ID"].ToString(),
                            Type = drp["Type"].ToString(),
                            cus_Type = drp["cus_Type"].ToString(),
                        });
                    }

                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listPicking
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
                dm.TraceService("NoDataSQL - " + ex.Message.ToString());
            }

            dm.TraceService("GetTransferInConfirmedItemBatch ENDED " + DateTime.Now);
            dm.TraceService("==========================");

            return JSONString;
        }
        public string InsVanToVanTransferInCompletion([FromForm] TransferInCompletion inputParams)
        {
            dm.TraceService("InsVanToVanTransferInCompletion STARTED -" + DateTime.Now);
            dm.TraceService("====================");
            List<DispatchIDs> itemData = JsonConvert.DeserializeObject<List<DispatchIDs>>(inputParams.Dispatches);
            string HeaderID = inputParams.VanHeaderID == null ? "0" : inputParams.VanHeaderID;
            string FromRotID = inputParams.FromRotID == null ? "0" : inputParams.FromRotID;
            string ToRotID = inputParams.ToRotID == null ? "0" : inputParams.ToRotID;

            // string Dispatche=inputParams.Dispatches == null ?"0":inputParams.Dispatches.ToString();
            //[ { "dspID: 1" , "asd" : 12 , "sad" [{asda : as} , {asda : as} ]} , { "dspID: 1" , "asd" : 12} ]

            string InputXml = "";
            using (var sw = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sw))
                {

                    writer.WriteStartDocument(true);
                    writer.WriteStartElement("r");
                    int c = 0;
                    foreach (DispatchIDs id in itemData)
                    {
                        string[] arr = { id.dsp_ID.ToString() };
                        string[] arrName = { "dsp_ID" };
                        dm.createNode(arr, arrName, writer);
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                    writer.Close();
                }
                InputXml = sw.ToString();
            }


            string[] ar = { FromRotID.ToString(), ToRotID.ToString(), InputXml.ToString() };

            DataTable dtVanToVan = dm.loadList("InsVanToVanTransferInCompletion", "sp_VanToVan", HeaderID.ToString(), ar);
            try
            {
                if (dtVanToVan.Rows.Count > 0)
                {
                    List<TransferInCompletionOut> listTransout = new List<TransferInCompletionOut>();
                    foreach (DataRow dr in dtVanToVan.Rows)
                    {
                        listTransout.Add(new TransferInCompletionOut
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),
                            Descr = dr["Descr"].ToString()

                        });
                    }
                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listTransout
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
                dm.TraceService(" InsVanToVanTransferInCompletion Exception - " + ex.Message.ToString());
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsVanToVanTransferInCompletion ENDED - " + DateTime.Now);
            dm.TraceService("==================");
            return JSONString;
        }
    }
}