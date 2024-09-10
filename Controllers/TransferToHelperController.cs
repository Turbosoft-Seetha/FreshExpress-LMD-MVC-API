using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using MVC_API.Models.CustomerConnectHelpers;
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
    public class TransferToHelperController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        [HttpPost]
      
        public string SelectHelper([FromForm] HelperIN inputParams)
        {
            dm.TraceService("SelectHelper STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotId == null ? "0" : inputParams.rotId;

                DataTable dtTrnsIn = dm.loadList("SelHelper", "sp_TransferToHelper", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<TransferToOut> listItems = new List<TransferToOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new TransferToOut
                        {
                            rot_ID = dr["rot_ID"].ToString(),
                            rot_Code = dr["rot_Code"].ToString(),
                            rot_Name = dr["rot_Name"].ToString(),
                            IsHelperRoute = dr["IsHelperRoute"].ToString(),
                            ParentRoute = dr["ParentRoute"].ToString(),


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
                dm.TraceService(" SelectHelper Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectHelper ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string HelperRouteHeader([FromForm] HelperRouteHead inputParams)
        {
            dm.TraceService("HelperRouteHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotId == null ? "0" : inputParams.rotId;

                DataTable dtTrnsIn = dm.loadList("SelTransferToHelperHeader", "Sp_TransferToHelper", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<HelperRouteOut> listItems = new List<HelperRouteOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new HelperRouteOut
                        {
                            TransID = dr["thh_TransID"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            Status = dr["Status"].ToString(),
                            Rot = dr["MainRot"].ToString(),
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
        public string HelperRouteUpdateStatus([FromForm] HelperRotUpdateStatus inputParams)
        {
            dm.TraceService("HelperRouteUpdateStatus STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                List<DispatchID> itemData = JsonConvert.DeserializeObject<List<DispatchID>>(inputParams.Dispatch);
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
                            foreach (DispatchID id in itemData)
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
                string[] ar = { HeaderID.ToString(), Status.ToString(), DteTime.ToString() };
                DataTable dtTrnsIn = dm.loadList("UpdateHelperStatus", "Sp_TransferToHelper", InputXml.ToString(), ar);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<TransferHelperOut> listItems = new List<TransferHelperOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new TransferHelperOut
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
                dm.TraceService(" HelperRouteUpdateStatus Exception - " + ex.Message.ToString());
            }
            dm.TraceService("HelperRouteUpdateStatus ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string InsTransferToHelper([FromForm] HelperOut inputParams)
        {
            dm.TraceService("InsTransferToHelper STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            try
            {
                List<DispatchIds> itemData = JsonConvert.DeserializeObject<List<DispatchIds>>(inputParams.Dispatches);

                string UdpID = inputParams.UdpID == null ? "0" : inputParams.UdpID;
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;
                string MainRot = inputParams.MainRot == null ? "0" : inputParams.MainRot;
                string HelperRot = inputParams.HelperRot == null ? "0" : inputParams.HelperRot;

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
                        foreach (DispatchIds id in itemData)
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



                string[] ar = { TransID.ToString(), MainRot.ToString(), HelperRot.ToString(), CreatedDate.ToString(), userID.ToString(), InputXml.ToString() };
                DataTable dtHelper = dm.loadList("InsTransferToHelper", "sp_TransferToHelper", UdpID.ToString(), ar);

                if (dtHelper.Rows.Count > 0)
                {
                    List<TransferHelperOut> listTransout = new List<TransferHelperOut>();
                    foreach (DataRow dr in dtHelper.Rows)
                    {
                        listTransout.Add(new TransferHelperOut
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
                dm.TraceService(" InsTransferToHelper Exception - " + ex.Message.ToString());
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsTransferToHelper ENDED - " + DateTime.Now.ToString());
            dm.TraceService("==================");
            return JSONString;
        }

        public string GetTransferHelperItemBatch([FromForm] PostTransferHelperData inputParams)
        {
            dm.TraceService("GetTransferHelperItemBatch STARTED " + DateTime.Now);
            dm.TraceService("============================");
            try
            {
                string HeaderID = inputParams.HelperHeaderId == null ? "0" : inputParams.HelperHeaderId;


                string[] arr = { };
                DataSet dtItemBatch = dm.loadListDS("SelTransferHelperItemBatch", "sp_TransferToHelper", HeaderID.ToString(), arr);
                DataTable TrnData = dtItemBatch.Tables[0];
                DataTable itemData = dtItemBatch.Tables[1];
                DataTable batchData = dtItemBatch.Tables[2];
                if (TrnData.Rows.Count > 0)
                {
                    List<TransferHelperListInfo> listPicking = new List<TransferHelperListInfo>();
                    foreach (DataRow drp in TrnData.Rows)
                    {
                        List<GetTransferHelperItemHeader> listItems = new List<GetTransferHelperItemHeader>();
                        foreach (DataRow dr in itemData.Rows)
                        {
                            List<GetTransferHelperBatchSerial> listBatchSerial = new List<GetTransferHelperBatchSerial>();
                            foreach (DataRow drDetails in batchData.Rows)
                            {
                                if (dr["dsd_ID"].ToString() == drDetails["dbs_dsd_ID"].ToString())
                                {
                                    listBatchSerial.Add(new GetTransferHelperBatchSerial
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
                                listItems.Add(new GetTransferHelperItemHeader
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
                        listPicking.Add(new TransferHelperListInfo
                        {
                            DespatchId = Int32.Parse(drp["dsp_ID"].ToString()),
                            HelperHeaderId = drp["thd_thh_ID"].ToString(),
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

            dm.TraceService("GetTransferHelperItemBatch ENDED " + DateTime.Now);
            dm.TraceService("==========================");

            return JSONString;
        }

        public string TransferMainRouteUpdateStatus([FromForm] TransMainRotUpdateStatus inputParams)
        {
            dm.TraceService("TransferMainRouteUpdateStatus STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;
                string Status = inputParams.Status == null ? "0" : inputParams.Status;

                string DteTime = inputParams.DateTime == null ? "0" : inputParams.DateTime;


                string[] ar = { Status.ToString(), DteTime.ToString() };
                DataTable dtTrnsIn = dm.loadList("UpdateMainRouteStatus", "sp_TransferToHelper", TransID.ToString(), ar);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<TransferHelperOut> listItems = new List<TransferHelperOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new TransferHelperOut
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
                dm.TraceService(" TransferMainRouteUpdateStatus Exception - " + ex.Message.ToString());
            }
            dm.TraceService("TransferMainRouteUpdateStatus ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string TransferToHelperCheckStatus([FromForm] TransHelperCheckStatus inputParams)
        {
            dm.TraceService("TransferToHelperCheckStatus STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                 string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;

                dm.TraceService("TransID-" + TransID);


                DataTable dtTrnsIn = dm.loadList("TransferToHelperCheckStatus", "sp_TransferToHelper", TransID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    dm.TraceService("Count -" + dtTrnsIn.Rows.Count);

                    List<TransHelperCheckStatusOut> listItems = new List<TransHelperCheckStatusOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new TransHelperCheckStatusOut
                        {
                            Status = dr["Status"].ToString(),

                            
                    });
                    }
                 
                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });
                    dm.TraceService("JSONString -" + JSONString);
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
                dm.TraceService(" TransferToHelperCheckStatus Exception - " + ex.Message.ToString());
            }
            dm.TraceService("TransferToHelperCheckStatus ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetTransferHelperConfirmedItemBatch([FromForm] TransHelperCheckStatus inputParams)
        {
            dm.TraceService("GetTransferHelperConfirmedItemBatch STARTED " + DateTime.Now);
            dm.TraceService("============================");
            try
            {
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;


                string[] arr = { };
                DataSet dtItemBatch = dm.loadListDS("SelTransferHelperConfirmedItemBatch", "sp_TransferToHelper", TransID.ToString(), arr);
                DataTable TrnData = dtItemBatch.Tables[0];
                DataTable itemData = dtItemBatch.Tables[1];
                DataTable batchData = dtItemBatch.Tables[2];
                if (TrnData.Rows.Count > 0)
                {
                    List<TransferHelperListInfo> listPicking = new List<TransferHelperListInfo>();
                    foreach (DataRow drp in TrnData.Rows)
                    {
                        List<GetTransferHelperItemHeader> listItems = new List<GetTransferHelperItemHeader>();
                        foreach (DataRow dr in itemData.Rows)
                        {
                            List<GetTransferHelperBatchSerial> listBatchSerial = new List<GetTransferHelperBatchSerial>();
                            foreach (DataRow drDetails in batchData.Rows)
                            {
                                if (dr["dsd_ID"].ToString() == drDetails["dbs_dsd_ID"].ToString())
                                {
                                    listBatchSerial.Add(new GetTransferHelperBatchSerial
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
                                listItems.Add(new GetTransferHelperItemHeader
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
                        listPicking.Add(new TransferHelperListInfo
                        {
                            DespatchId = Int32.Parse(drp["dsp_ID"].ToString()),
                            HelperHeaderId = drp["thd_thh_ID"].ToString(),
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

            dm.TraceService("GetTransferHelperConfirmedItemBatch ENDED " + DateTime.Now);
            dm.TraceService("==========================");

            return JSONString;
        }

        // Sync to Main Route Starts
        public string InsTransferFromHelperToMainRoute([FromForm] MainHelperOut inputParams)
        {
            dm.TraceService("InsTransferFromHelperToMainRoute STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            try
            {
                List<DispatchIds> itemData = JsonConvert.DeserializeObject<List<DispatchIds>>(inputParams.Dispatches);

                string UdpID = inputParams.UdpID == null ? "0" : inputParams.UdpID;
                string MainRot = inputParams.MainRot == null ? "0" : inputParams.MainRot;
                string HelperRot = inputParams.HelperRot == null ? "0" : inputParams.HelperRot;
                string CreatedDate = inputParams.CreatedDate == null ? "0" : inputParams.CreatedDate;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;

                string InputXml = "";
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (DispatchIds id in itemData)
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



                string[] ar = { MainRot.ToString(), HelperRot.ToString(), CreatedDate.ToString(), userID.ToString(), InputXml.ToString() ,TransID.ToString()};
                DataTable dtHelper = dm.loadList("InsTransferFromHelperToMain", "sp_TransferToHelper", UdpID.ToString(), ar);

                if (dtHelper.Rows.Count > 0)
                {
                    List<TransferHelperOut> listTransout = new List<TransferHelperOut>();
                    foreach (DataRow dr in dtHelper.Rows)
                    {
                        listTransout.Add(new TransferHelperOut
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
                dm.TraceService(" InsTransferFromHelperToMainRoute Exception - " + ex.Message.ToString());
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsTransferFromHelperToMainRoute ENDED - " + DateTime.Now.ToString());
            dm.TraceService("==================");
            return JSONString;
        }

        public string MainRouteHeader([FromForm] PostTransferMainRot inputParams)
        {
            dm.TraceService("MainRouteHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.MainrotId == null ? "0" : inputParams.MainrotId;

                DataTable dtTrnsIn = dm.loadList("SelTransferToMainHeader", "Sp_TransferToHelper", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostMainHeader> listItems = new List<PostMainHeader>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostMainHeader
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
                dm.TraceService(" MainRouteHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("MainRouteHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string MainRouteDetail([FromForm] PostTransferMainRotDetail inputParams)
        {
            dm.TraceService("MainRouteDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string HeaderID = inputParams.HeaderId == null ? "0" : inputParams.HeaderId;

                DataTable dtTrnsIn = dm.loadList("SelTransferToMainDetail", "Sp_TransferToHelper", HeaderID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostMainDetail> listItems = new List<PostMainDetail>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostMainDetail
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
                dm.TraceService(" MainRouteDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("MainRouteDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string InsConfirmationFromMainRoute([FromForm] PostTransferMainRotIn inputParams)
        {
            dm.TraceService("InsConfirmationFromMainRoute STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            try
            {

                string HeaderID = inputParams.HeaderId == null ? "0" : inputParams.HeaderId;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;
                string ModifiedDate = inputParams.ModifiedDate == null ? "0" : inputParams.ModifiedDate;

                string[] arr = { userID.ToString() , ModifiedDate.ToString() };
                DataTable dtHelper = dm.loadList("InsConfirmationFromMainRot", "sp_TransferToHelper", HeaderID.ToString(),arr);

                if (dtHelper.Rows.Count > 0)
                {
                    List<TransferHelperOut> listTransout = new List<TransferHelperOut>();
                    foreach (DataRow dr in dtHelper.Rows)
                    {
                        listTransout.Add(new TransferHelperOut
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
                dm.TraceService(" InsConfirmationFromMainRoute Exception - " + ex.Message.ToString());
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsConfirmationFromMainRoute ENDED - " + DateTime.Now.ToString());
            dm.TraceService("==================");
            return JSONString;
        }

        // Sync to Main Route Ends

        //Return Helper Starts
        public string InsTransferToHelperReturn([FromForm] HelperReturnOut inputParams)
        {
            dm.TraceService("InsTransferToHelper STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            try
            {
                List<Returns> itemData = JsonConvert.DeserializeObject<List<Returns>>(inputParams.ReturnIds);

                string UdpID = inputParams.UdpID == null ? "0" : inputParams.UdpID;
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;
                string MainRot = inputParams.MainRot == null ? "0" : inputParams.MainRot;
                string HelperRot = inputParams.HelperRot == null ? "0" : inputParams.HelperRot;

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
                        foreach (Returns id in itemData)
                        {
                            string[] arr = { id.rrh_ID.ToString() };
                            string[] arrName = { "rrh_ID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw.ToString();
                }



                string[] ar = { TransID.ToString(), MainRot.ToString(), HelperRot.ToString(), CreatedDate.ToString(), userID.ToString(), InputXml.ToString() };
                DataTable dtHelper = dm.loadList("InsTransferToHelperReturn", "sp_TransferToHelper", UdpID.ToString(), ar);

                if (dtHelper.Rows.Count > 0)
                {
                    List<TransferHelperOut> listTransout = new List<TransferHelperOut>();
                    foreach (DataRow dr in dtHelper.Rows)
                    {
                        listTransout.Add(new TransferHelperOut
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
                dm.TraceService(" InsTransferToHelper Exception - " + ex.Message.ToString());
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsTransferToHelper ENDED - " + DateTime.Now.ToString());
            dm.TraceService("==================");
            return JSONString;
        }
        public string HelperRouteReturnHeader([FromForm] HelperRouteReturnHead inputParams)
        {
            dm.TraceService("HelperRouteHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotId == null ? "0" : inputParams.rotId;

                DataTable dtTrnsIn = dm.loadList("SelTransferToHelperReturnHeader", "Sp_TransferToHelper", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<HelperRouteReturnOut> listItems = new List<HelperRouteReturnOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new HelperRouteReturnOut
                        {
                            TransID = dr["thr_TransID"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            Status = dr["Status"].ToString(),
                            Rot = dr["MainRot"].ToString(),
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
                dm.TraceService(" HelperRouteHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("HelperRouteHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string HelperRouteReturnUpdateStatus([FromForm] HelperRotReturnUpdateStatus inputParams)
        {
            dm.TraceService("HelperRouteUpdateStatus STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                List<ReturnID> itemData = JsonConvert.DeserializeObject<List<ReturnID>>(inputParams.ReturnIds);
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
                            foreach (ReturnID id in itemData)
                            {
                                string[] arr = { id.rrh_ID.ToString() };
                                string[] arrName = { "rrh_ID" };
                                dm.createNode(arr, arrName, writer);
                            }

                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            writer.Close();
                        }
                        InputXml = sw.ToString();
                    }
                }
                string[] ar = { HeaderID.ToString(), Status.ToString(), DteTime.ToString() };
                DataTable dtTrnsIn = dm.loadList("UpdateHelperReturnStatus", "Sp_TransferToHelper", InputXml.ToString(), ar);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<TransferHelperOut> listItems = new List<TransferHelperOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new TransferHelperOut
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
                dm.TraceService(" HelperRouteUpdateStatus Exception - " + ex.Message.ToString());
            }
            dm.TraceService("HelperRouteUpdateStatus ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string TransferMainRouteReturnUpdateStatus([FromForm] TransMainRotUpdatereturnStatus inputParams)
        {
            dm.TraceService("TransferMainRouteUpdateStatus STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;
                string Status = inputParams.Status == null ? "0" : inputParams.Status;

                string DteTime = inputParams.DateTime == null ? "0" : inputParams.DateTime;


                string[] ar = { Status.ToString(), DteTime.ToString() };
                DataTable dtTrnsIn = dm.loadList("UpdateMainRouteReturnStatus", "sp_TransferToHelper", TransID.ToString(), ar);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<TransferHelperOut> listItems = new List<TransferHelperOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new TransferHelperOut
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
                dm.TraceService(" TransferMainRouteUpdateStatus Exception - " + ex.Message.ToString());
            }
            dm.TraceService("TransferMainRouteUpdateStatus ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string TransferToHelperReturnCheckStatus([FromForm] TransHelperCheckStatus inputParams)
        {
            dm.TraceService("TransferToHelperCheckStatus STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;

                dm.TraceService("TransID-" + TransID);


                DataTable dtTrnsIn = dm.loadList("TransferToHelperReturnCheckStatus", "sp_TransferToHelper", TransID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    dm.TraceService("Count -" + dtTrnsIn.Rows.Count);

                    List<TransHelperCheckStatusOut> listItems = new List<TransHelperCheckStatusOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new TransHelperCheckStatusOut
                        {
                            Status = dr["Status"].ToString(),


                        });
                    }

                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listItems
                    });
                    dm.TraceService("JSONString -" + JSONString);
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
                dm.TraceService(" TransferToHelperCheckStatus Exception - " + ex.Message.ToString());
            }
            dm.TraceService("TransferToHelperCheckStatus ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string GetTransferHelperReturnItemBatch([FromForm] PostTransferHelperReturnData inputParams)
        {
            dm.TraceService("GetTransferHelperItemBatch STARTED " + DateTime.Now);
            dm.TraceService("============================");
            try
            {
                string HeaderID = inputParams.HelperHeaderId == null ? "0" : inputParams.HelperHeaderId;


                string[] arr = { };
                DataSet dtItemBatch = dm.loadListDS("SelTransferHelperReturnItemBatch", "sp_TransferToHelper", HeaderID.ToString(), arr);
                DataTable TrnData = dtItemBatch.Tables[0];
                DataTable itemData = dtItemBatch.Tables[1];
                DataTable batchData = dtItemBatch.Tables[2];
                if (TrnData.Rows.Count > 0)
                {
                    List<TransferHelperReturnListInfo> listPicking = new List<TransferHelperReturnListInfo>();
                    foreach (DataRow drp in TrnData.Rows)
                    {
                        List<GetTransferHelperReturnItemHeader> listItems = new List<GetTransferHelperReturnItemHeader>();
                        foreach (DataRow dr in itemData.Rows)
                        {
                            List<GetTransferHelperReturnBatchSerial> listBatchSerial = new List<GetTransferHelperReturnBatchSerial>();
                            foreach (DataRow drDetails in batchData.Rows)
                            {
                                if (dr["rrd_prd_ID"].ToString() == drDetails["prd_ID"].ToString())
                                {
                                    listBatchSerial.Add(new GetTransferHelperReturnBatchSerial
                                    {
                                    
                                        ind_inv_ID = drDetails["inv_InvoiceID"].ToString(),
                                        Number = drDetails["dns_Number"].ToString(),
                                        ExpiryDate = drDetails["ExpiryDate"].ToString(),
                                        BaseUOM = drDetails["BaseUOM"].ToString(),
                                        EligibleQty = drDetails["EligibleQty"].ToString(),
                                        prd_ID = drDetails["prd_ID"].ToString(),                                    
                                        ID = drDetails["ind_inv_ID"].ToString(),
                                        BatchSerialId = drDetails["dns_ID"].ToString(),
                                    });
                                }
                            }
                            if (drp["rrh_ID"].ToString() == dr["rrd_rrh_ID"].ToString())
                            {
                                listItems.Add(new GetTransferHelperReturnItemHeader
                                {
                                    Id = Int32.Parse(dr["prd_ID"].ToString()),
                                    Name = dr["prd_Name"].ToString(),
                                    Code = dr["prd_Code"].ToString(),
                                    Spec = dr["prd_Spec"].ToString(),                                 
                                    CategoryId = dr["prd_cat_ID"].ToString(),
                                    SubcategoryId = dr["prd_sub_ID"].ToString(),
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
                                    EnableExcess = dr["prd_EnableExcess"].ToString(),
                                    WeighingItem = dr["prd_WeighingItem"].ToString(),
                                    prd_BaseUOM = dr["prd_BaseUOM"].ToString(),
                                    HigherUOM = dr["rrd_HUOM"].ToString(),
                                    LowerUOM = dr["rrd_LUOM"].ToString(),
                                    HigherQty = dr["rrd_HQty"].ToString(),
                                    LowerQty = dr["rrd_LQty"].ToString(),
                                    BatchSerial = listBatchSerial,
                                });
                            }
                        }
                        listPicking.Add(new TransferHelperReturnListInfo
                        {
                            ReturnRequestId = Int32.Parse(drp["rrh_ID"].ToString()),
                            HelperHeaderId = drp["trd_thr_ID"].ToString(),
                            rrh_RequestNumber = drp["rrh_RequestNumber"].ToString(),
                            Date = drp["Date"].ToString(),
                            Time = drp["Time"].ToString(),
                            CusHeaderCode = drp["csh_Code"].ToString(),
                            CusHeaderName = drp["csh_Name"].ToString(),
                            CustomerCode = drp["cus_Code"].ToString(),
                            CustomerName = drp["cus_Name"].ToString(),
                            cus_ArabicName = drp["cus_ArabicName"].ToString(),
                            CustomerID = drp["cus_ID"].ToString(),
                            CusHeaderID = drp["csh_ID"].ToString(),
                            cus_Type = drp["cus_Type"].ToString(),                           
                            Type = drp["Type"].ToString(),                           
                            ReturnItems = listItems,
                            inv_InvoiceID = drp["inv_InvoiceID"].ToString(),
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

            dm.TraceService("GetTransferHelperItemBatch ENDED " + DateTime.Now);
            dm.TraceService("==========================");

            return JSONString;
        }

        public string GetTransferHelperReturnConfirmedItemBatch([FromForm] PostTransferHelperReturnData inputParams)
        {
            dm.TraceService("GetTransferHelperItemBatch STARTED " + DateTime.Now);
            dm.TraceService("============================");
            try
            {
                string HeaderID = inputParams.HelperHeaderId == null ? "0" : inputParams.HelperHeaderId;


                string[] arr = { };
                DataSet dtItemBatch = dm.loadListDS("SelTransferHelperReturnConfirmedItemBatch", "sp_TransferToHelper", HeaderID.ToString(), arr);
                DataTable TrnData = dtItemBatch.Tables[0];
                DataTable itemData = dtItemBatch.Tables[1];
                DataTable batchData = dtItemBatch.Tables[2];
                if (TrnData.Rows.Count > 0)
                {
                    List<TransferHelperReturnListInfo> listPicking = new List<TransferHelperReturnListInfo>();
                    foreach (DataRow drp in TrnData.Rows)
                    {
                        List<GetTransferHelperReturnItemHeader> listItems = new List<GetTransferHelperReturnItemHeader>();
                        foreach (DataRow dr in itemData.Rows)
                        {
                            List<GetTransferHelperReturnBatchSerial> listBatchSerial = new List<GetTransferHelperReturnBatchSerial>();
                            foreach (DataRow drDetails in batchData.Rows)
                            {
                                if (dr["rrd_prd_ID"].ToString() == drDetails["prd_ID"].ToString())
                                {
                                    listBatchSerial.Add(new GetTransferHelperReturnBatchSerial
                                    {

                                        ind_inv_ID = drDetails["inv_InvoiceID"].ToString(),
                                        Number = drDetails["dns_Number"].ToString(),
                                        ExpiryDate = drDetails["ExpiryDate"].ToString(),
                                        BaseUOM = drDetails["BaseUOM"].ToString(),
                                        EligibleQty = drDetails["EligibleQty"].ToString(),
                                        prd_ID = drDetails["prd_ID"].ToString(),
                                        ID = drDetails["ind_inv_ID"].ToString(),
                                        BatchSerialId = drDetails["dns_ID"].ToString(),
                                    });
                                }
                            }
                            if (drp["rrh_ID"].ToString() == dr["rrd_ID"].ToString())
                            {
                                listItems.Add(new GetTransferHelperReturnItemHeader
                                {
                                    Id = Int32.Parse(dr["prd_ID"].ToString()),
                                    Name = dr["prd_Name"].ToString(),
                                    Code = dr["prd_Code"].ToString(),
                                    Spec = dr["prd_Spec"].ToString(),
                                    CategoryId = dr["prd_cat_ID"].ToString(),
                                    SubcategoryId = dr["prd_sub_ID"].ToString(),
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
                                    EnableExcess = dr["prd_EnableExcess"].ToString(),
                                    WeighingItem = dr["prd_WeighingItem"].ToString(),
                                    prd_BaseUOM = dr["prd_BaseUOM"].ToString(),
                                    HigherUOM = dr["rrd_HUOM"].ToString(),
                                    LowerUOM = dr["rrd_LUOM"].ToString(),
                                    HigherQty = dr["rrd_HQty"].ToString(),
                                    LowerQty = dr["rrd_LQty"].ToString(),
                                    BatchSerial = listBatchSerial,
                                });
                            }
                        }
                        listPicking.Add(new TransferHelperReturnListInfo
                        {
                            ReturnRequestId = Int32.Parse(drp["rrh_ID"].ToString()),
                            HelperHeaderId = drp["trd_thr_ID"].ToString(),
                            rrh_RequestNumber = drp["rrh_RequestNumber"].ToString(),
                            Date = drp["Date"].ToString(),
                            Time = drp["Time"].ToString(),
                            CusHeaderCode = drp["csh_Code"].ToString(),
                            CusHeaderName = drp["csh_Name"].ToString(),
                            CustomerCode = drp["cus_Code"].ToString(),
                            CustomerName = drp["cus_Name"].ToString(),
                            cus_ArabicName = drp["cus_ArabicName"].ToString(),
                            CustomerID = drp["cus_ID"].ToString(),
                            CusHeaderID = drp["csh_ID"].ToString(),
                            cus_Type = drp["cus_Type"].ToString(),
                            Type = drp["Type"].ToString(),
                            ReturnItems = listItems,
                            inv_InvoiceID = drp["inv_InvoiceID"].ToString(),
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

            dm.TraceService("GetTransferHelperItemBatch ENDED " + DateTime.Now);
            dm.TraceService("==========================");

            return JSONString;
        }

        //Return Sync to Main Route Starts

        public string InsTransferFromHelperReturnToMainRoute([FromForm] MainHelperReturnOut inputParams)
        {
            dm.TraceService("InsTransferFromHelperToMainRoute STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            try
            {
                List<Returns> itemData = JsonConvert.DeserializeObject<List<Returns>>(inputParams.ReturnIds);

                string UdpID = inputParams.UdpID == null ? "0" : inputParams.UdpID;
                string MainRot = inputParams.MainRot == null ? "0" : inputParams.MainRot;
                string HelperRot = inputParams.HelperRot == null ? "0" : inputParams.HelperRot;
                string CreatedDate = inputParams.CreatedDate == null ? "0" : inputParams.CreatedDate;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;
                string TransID = inputParams.TransID == null ? "0" : inputParams.TransID;


                string InputXml = "";
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (Returns id in itemData)
                        {
                            string[] arr = { id.rrh_ID.ToString() };
                            string[] arrName = { "rrh_ID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw.ToString();
                }



                string[] ar = { MainRot.ToString(), HelperRot.ToString(), CreatedDate.ToString(), userID.ToString(), InputXml.ToString(),TransID.ToString() };
                DataTable dtHelper = dm.loadList("InsTransferFromHelperReturnToMain", "sp_TransferToHelper", UdpID.ToString(), ar);

                if (dtHelper.Rows.Count > 0)
                {
                    List<TransferHelperOut> listTransout = new List<TransferHelperOut>();
                    foreach (DataRow dr in dtHelper.Rows)
                    {
                        listTransout.Add(new TransferHelperOut
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
                dm.TraceService(" InsTransferFromHelperToMainRoute Exception - " + ex.Message.ToString());
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsTransferFromHelperToMainRoute ENDED - " + DateTime.Now.ToString());
            dm.TraceService("==================");
            return JSONString;
        }

        public string MainRouteReturnHeader([FromForm] PostTransferMainRotReturn inputParams)
        {
            dm.TraceService("MainRouteHeader STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.MainrotId == null ? "0" : inputParams.MainrotId;

                DataTable dtTrnsIn = dm.loadList("SelTransferToMainReturnHeader", "Sp_TransferToHelper", RotID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostMainHeaderReturn> listItems = new List<PostMainHeaderReturn>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostMainHeaderReturn
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
                dm.TraceService(" MainRouteHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("MainRouteHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string MainRouteReturnDetail([FromForm] PostTransferMainRotReturnDetail inputParams)
        {
            dm.TraceService("MainRouteDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string HeaderID = inputParams.HeaderId == null ? "0" : inputParams.HeaderId;

                DataTable dtTrnsIn = dm.loadList("SelTransferToMainReturnDetail", "Sp_TransferToHelper", HeaderID.ToString());

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<PostMainReturnDetail> listItems = new List<PostMainReturnDetail>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new PostMainReturnDetail
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
                dm.TraceService(" MainRouteDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("MainRouteDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string InsConfirmationFromMainRouteReturn([FromForm] PostTransferMainRotReturnIn inputParams)
        {
            dm.TraceService("InsConfirmationFromMainRoute STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            try
            {

                string HeaderID = inputParams.HeaderId == null ? "0" : inputParams.HeaderId;
                string userID = inputParams.userID == null ? "0" : inputParams.userID;
                string ModifiedDate = inputParams.ModifiedDate == null ? "0" : inputParams.ModifiedDate;

                string[] arr = { userID.ToString(), ModifiedDate.ToString() };
                DataTable dtHelper = dm.loadList("InsConfirmationFromMainRotReturn", "sp_TransferToHelper", HeaderID.ToString(), arr);

                if (dtHelper.Rows.Count > 0)
                {
                    List<TransferHelperOut> listTransout = new List<TransferHelperOut>();
                    foreach (DataRow dr in dtHelper.Rows)
                    {
                        listTransout.Add(new TransferHelperOut
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
                dm.TraceService(" InsConfirmationFromMainRoute Exception - " + ex.Message.ToString());
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsConfirmationFromMainRoute ENDED - " + DateTime.Now.ToString());
            dm.TraceService("==================");
            return JSONString;
        }

        //Return Sync to Main Route Ends
        //Return Helper Ends
    }
}