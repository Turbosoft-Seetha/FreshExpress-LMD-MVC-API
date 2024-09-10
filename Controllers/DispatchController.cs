using Microsoft.AspNetCore.Mvc;
using MVC_API.FE_Nav_Service;
using MVC_API.Models;
using MVC_API.Models.CustomerConnectHelpers;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers
{
    public class DispatchController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        [HttpPost]
        public string GetItemBatch([FromForm] PostDispatchData inputParams)
        {
            dm.TraceService("GetItemBatch STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string dispatchID = inputParams.DispatchId == null ? "0" : inputParams.DispatchId;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

            string[] arr = { userID.ToString() };
            DataSet dtItemBatch = dm.loadListDS("SelDispatchBatchSerial", "sp_Dispatch", dispatchID.ToString(), arr);
            DataTable itemData = dtItemBatch.Tables[0];
            DataTable batchData = dtItemBatch.Tables[1];
            //Items In Picklist 

            //Batch/Serial Number in Picklist against each item
            
            try
            {
                if (itemData.Rows.Count > 0)
                {
                    List<GetDispatchItemHeader> listItems = new List<GetDispatchItemHeader>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<GetDispatchBatchSerial> listBatchSerial = new List<GetDispatchBatchSerial>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["prd_Code"].ToString() == drDetails["prd_Code"].ToString() && dr["dsd_ID"].ToString() == drDetails["dbs_dsd_ID"].ToString())
                            {
                                listBatchSerial.Add(new GetDispatchBatchSerial
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

                        listItems.Add(new GetDispatchItemHeader
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
                            VATPercent =  dr["odd_VATPercent"].ToString(),
                            Price = dr["odd_Price"].ToString(),
                            Discount = dr["odd_Discount"].ToString(),
                            DiscountPercentage = dr["odd_DiscountPercentage"].ToString(),
                            Reason = dr["dsd_rsn_ID"].ToString()
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

        public string PostRejectDispatch([FromForm] PostRejectData inputParams)
        {
            dm.TraceService("PostRejectDispatch STARTED " + DateTime.Now.ToString());
            dm.TraceService("============================================");
            string dispatchID = inputParams.DispatchId == null ? "0" : inputParams.DispatchId;
            string status = inputParams.Status == null ? "R" : inputParams.Status;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
            string reasonId = inputParams.ReasonId == null ? "0" : inputParams.ReasonId;
            try
            {
                string[] arr = { status.ToString(), userID.ToString(), reasonId.ToString() };
                string Value = dm.SaveData("sp_Dispatch", "RejectDispatch", dispatchID.ToString(), arr);
                int Output = Int32.Parse(Value);
                List<GetInsertStatus> listStatus = new List<GetInsertStatus>();
                if (Output > 0)
                {
                    
                    listStatus.Add(new GetInsertStatus
                    {
                        Mode = "1",
                        Status = "Dispatch rejected successfully"
                    });
                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listStatus
                    });
                    return JSONString;
                }
                else
                {
                    listStatus.Add(new GetInsertStatus
                    {
                        Mode = "0",
                        Status = "Dispatch rejection failed"
                    });
                    string JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listStatus
                    });
                    return JSONString;
                }
            }
            catch (Exception ex)
            {
                dm.TraceService(ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }
            dm.TraceService("PostRejectDispatch ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            return JSONString;
        }

        public string PostParkDispatch([FromForm] PostParkData inputParams)
        {
            dm.TraceService("PostParkDispatch STARTED " + DateTime.Now.ToString());
            dm.TraceService("============================================");
            try
            {
                List<PostDispatchItemData> itemData = JsonConvert.DeserializeObject<List<PostDispatchItemData>>(inputParams.JSONString);
                List<PostDispatchBatchSerial> batchData = JsonConvert.DeserializeObject<List<PostDispatchBatchSerial>>(inputParams.BatchData);
                try
                {
                    string dispatchID = inputParams.DispatchId == null ? "0" : inputParams.DispatchId;
                    string status = inputParams.Status == null ? "P" : inputParams.Status;
                    string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
                    string InputXml = "";
                    string InputBatchSerialXML = "";
                    using (var sw = new StringWriter())
                    {
                        using (var writer = XmlWriter.Create(sw))
                        {

                            writer.WriteStartDocument(true);
                            writer.WriteStartElement("r");
                            int c = 0;
                            foreach (PostDispatchItemData id in itemData)
                            {
                                string[] arr = { id.ItemId.ToString(), id.AdjustedHigherUOM.ToString(), id.AdjustedHigherQty.ToString(), id.AdjustedLowerUOM.ToString(), id.AdjustedLowerQty.ToString(), id.LoadInHigherUOM.ToString(), id.LoadInHigherQty.ToString(), id.LoadInLowerUOM.ToString(), id.LoadInLowerQty.ToString(),id.LineNumber };
                                string[] arrName = { "ItemId", "AdjustedHigherUOM", "AdjustedHigherQty", "AdjustedLowerUOM", "AdjustedLowerQty", "LoadInHigherUOM", "LoadInHigherQty", "LoadInLowerUOM", "LoadInLowerQty" ,"dsd_LineNo"};
                                dm.createNode(arr, arrName, writer);
                            }

                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            writer.Close();
                        }
                        InputXml = sw.ToString();
                    }
                    using (var sws = new StringWriter())
                    {
                        using (var writer = XmlWriter.Create(sws))
                        {

                            writer.WriteStartDocument(true);
                            writer.WriteStartElement("r");
                            int c = 0;
                            foreach (PostDispatchBatchSerial id in batchData)
                            {
                                string[] arr = { id.DispBatSerialId.ToString(), id.DisDetailId.ToString(), id.Number.ToString(), id.AdjustedQty.ToString(), id.LoadInQty.ToString() };
                                string[] arrName = { "DispBatSerialId", "DisDetailId", "Number", "AdjustedQty", "LoadInQty" };
                                dm.createNode(arr, arrName, writer);
                            }

                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            writer.Close();
                        }
                        InputBatchSerialXML = sws.ToString();
                    }

                    try
                    {
                        string[] arr = { status.ToString(), userID.ToString(), InputXml.ToString(), InputBatchSerialXML.ToString() };
                        string Value = dm.SaveData("sp_Dispatch", "ParkDispatch", dispatchID.ToString(), arr);
                        int Output = Int32.Parse(Value);
                        List<GetInsertStatus> listStatus = new List<GetInsertStatus>();
                        if (Output > 0)
                        {

                            listStatus.Add(new GetInsertStatus
                            {
                                Mode = "1",
                                Status = "Dispatch parked successfully"
                            });
                            string JSONString = JsonConvert.SerializeObject(new
                            {
                                result = listStatus
                            });
                            return JSONString;
                        }
                        else
                        {
                            listStatus.Add(new GetInsertStatus
                            {
                                Mode = "0",
                                Status = "Dispatch park failed"
                            });
                            string JSONString = JsonConvert.SerializeObject(new
                            {
                                result = listStatus
                            });
                            return JSONString;
                        }
                    }
                    catch (Exception ex)
                    {
                        dm.TraceService(ex.Message.ToString());
                        JSONString = "NoDataSQL - " + ex.Message.ToString();
                    }

                }
                catch (Exception ex)
                {
                    dm.TraceService(ex.Message.ToString());
                    JSONString = "NoDataSQL - " + ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                dm.TraceService(ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }
            dm.TraceService("PostParkDispatch ENDED " + DateTime.Now.ToString());
            dm.TraceService("========================================");
            return JSONString;
        }
        public string GetNewBatch([FromForm] PostOrderData inputParams)
        {
            dm.TraceService("GetNewBatch STARTED ");
            dm.TraceService("====================");
            string itemID = inputParams.ItemId == null ? "0" : inputParams.ItemId;
            string orderID = inputParams.OrderId == null ? "0" : inputParams.OrderId;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
            string ExpDate = inputParams.ExpDelDate == null ? "0" : inputParams.ExpDelDate;

            DateTime ExpDelDate;
            string format = "ddMMyyyy";
            try { ExpDelDate = DateTime.ParseExact(ExpDate, format, CultureInfo.InvariantCulture); } catch { ExpDelDate = DateTime.Now; }


            int ItemLineNo = Int32.Parse(inputParams.ItemLineNo == null ? "0" : inputParams.ItemLineNo);


            MasterMgmntDigits gm = dm.Creds();
			Root1 root = new Root1();

            try
            {
                dm.TraceService("GetBatchDetails FE API Call started At - " + DateTime.Now.ToString());
                gm.GetBatchList(itemID, ExpDelDate, ref root);
                dm.TraceService("GetBatchDetails FE API Call Completed At - " + DateTime.Now.ToString());

                BatchDetails[] bd = root.BatchDetails;
                List<GetNewBatch> listNewBatch = new List<GetNewBatch>();
                foreach (BatchDetails bd_info in bd)
                {
                    if (bd_info.BatchCode != "")
                    {
                        //Decimal outNumber;
                        //decimal.TryParse(bd_info.AvailQty, NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out outNumber);
                        string date = "";
                        try
                        {
                            date = DateTime.Parse(bd_info.ExpirationDate).ToString("dd-MMM-yyyy");

                        }
                        catch (Exception ex)
                        {
                            date = bd_info.ExpirationDate;

                        }
                        listNewBatch.Add(new GetNewBatch
                        {

                            Id = 0,
                            Number = bd_info.BatchCode,
                            ExpiryDate = date,
                            AvailableQty = bd_info.AvailQty.Replace(",", ""),
                            SalesPerson = "FREE ON HAND"
                        });
                    }
                }

                if (listNewBatch.Count > 0)
                {
                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listNewBatch
                    });
                }
                else
                {
                    return "NoData";
                }


                return JSONString;
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("GetNewBatch ENDED ");
            dm.TraceService("==================");
            return JSONString;
        }
        public string PostCompleteDispatch([FromForm] PostCompleteData inputParams)
        {
            dm.TraceService("PostCompleteDispatch STARTED " + DateTime.Now.ToString());
            dm.TraceService("=============================");
            string imagePath = "";
			try
            {
                dm.TraceService("PostCompleteDispatch-JSONString inparas - " + inputParams.JSONString + DateTime.Now.ToString());
                dm.TraceService("PostCompleteDispatch-BatchData inparas - " + inputParams.BatchData + DateTime.Now.ToString());
                List<PostDispatchItemData> itemData = JsonConvert.DeserializeObject<List<PostDispatchItemData>>(inputParams.JSONString);
                List<PostDispatchBatchSerial> batchData = JsonConvert.DeserializeObject<List<PostDispatchBatchSerial>>(inputParams.BatchData);
                try
                {
                    var HttpReq = HttpContext.Request;
                    string UploadPath = ConfigurationManager.AppSettings.Get("SignaturePath").ToString();
                    var x = HttpReq.Files.AllKeys;

                    foreach (var file in x)
                    {
                        if (file.Equals("signature"))
                        {
                            var y = HttpReq.Files["signature"];
                            var physicalPath = Path.Combine(Server.MapPath(UploadPath), y.FileName);
                            imagePath = UploadPath + y.FileName;
                            // The files are not actually saved in this demo
                            y.SaveAs(physicalPath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    dm.TraceService("Dispatch_Signature_Upload-" + ex.Message.ToString());
                }

                string dispatchID = inputParams.DispatchId == null ? "0" : inputParams.DispatchId;
                string status = inputParams.Status == null ? "C" : inputParams.Status;
                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
                string remark = inputParams.Remark == null ? "0" : inputParams.Remark;
               
                string InputXml = "";
                string InputBatchSerialXML = "";
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {
                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostDispatchItemData id in itemData)
                        {
                            string[] arr = { id.ItemId.ToString(), id.AdjustedHigherUOM.ToString(), id.AdjustedHigherQty.ToString(), id.AdjustedLowerUOM.ToString(), id.AdjustedLowerQty.ToString(), id.LoadInHigherUOM.ToString(), id.LoadInHigherQty.ToString(), id.LoadInLowerUOM.ToString(), id.LoadInLowerQty.ToString() ,id.LineNumber,id.Reason};
                            string[] arrName = { "ItemId", "AdjustedHigherUOM", "AdjustedHigherQty", "AdjustedLowerUOM", "AdjustedLowerQty", "LoadInHigherUOM", "LoadInHigherQty", "LoadInLowerUOM", "LoadInLowerQty","dsd_LineNo","Reason" };
                            dm.createNode(arr, arrName, writer);

                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw.ToString();
                }
                using (var sws = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sws))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PostDispatchBatchSerial id in batchData)
                        {
                            string[] arr = { id.DispBatSerialId.ToString(), id.DisDetailId.ToString(), id.Number.ToString(), id.AdjustedQty.ToString(), id.LoadInQty.ToString() };
                            string[] arrName = { "DispBatSerialId", "DisDetailId", "Number", "AdjustedQty", "LoadInQty" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputBatchSerialXML = sws.ToString();
                }

                try
                {
                    string[] arr = { status.ToString(), imagePath.ToString(), remark.ToString(), userID.ToString(), InputXml.ToString(), InputBatchSerialXML.ToString() };
                    DataTable dtValue = dm.loadList( "CompleteDispatch","sp_Dispatch", dispatchID.ToString(), arr);
                   // int Output = Int32.Parse(Value);
                    List<GetInsertStatus> listStatus = new List<GetInsertStatus>();
                    if (dtValue.Rows.Count > 0)
                    {
                       
                        foreach (DataRow dr in dtValue.Rows)
                        {
                            listStatus.Add(new GetInsertStatus
                            {
                                Mode = "1",
                                Status = "Dispatch completed successfully",
                                DeliveryNumber = dr["dln_DeliveryNumber"].ToString(),
                                dln_ID = dr["dln_ID"].ToString(),
                            });
                        }
                        string JSONString = JsonConvert.SerializeObject(new
                        {
                            result = listStatus
                        });
                        return JSONString;
                    }
                    else
                    {
                       
                        foreach (DataRow dr in dtValue.Rows)
                        {
                            listStatus.Add(new GetInsertStatus
                            {
                                Mode = "0",
                                Status = "Dispatch failed",
                                DeliveryNumber = "0",
                                dln_ID = "0",
                            });
                        }
                        string JSONString = JsonConvert.SerializeObject(new
                        {
                            result = listStatus
                        });
                        return JSONString;
                    }
                }
                catch (Exception ex)
                {
                    dm.TraceService(ex.Message.ToString());
                    JSONString = "NoDataSQL - " + ex.Message.ToString();
                }
            }
            catch(Exception ex)
            {
                dm.TraceService(ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }
            dm.TraceService("PostCompleteDispatch ENDED " + DateTime.Now.ToString());
            dm.TraceService("===============================================");
            return JSONString;
        }
        public string SelectOutlets([FromForm] OutletsIn inputParams)
        {
            dm.TraceService("SelectOutlets STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rot_ID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
                string Cus_IDs = inputParams.Cus_IDs == null ? "0" : inputParams.Cus_IDs;

                string[] arr = { Cus_IDs.ToString() };
                DataTable dtitem = dm.loadList("SelOutlets", "sp_App_UOM", rot_ID.ToString(), arr);

                if (dtitem.Rows.Count > 0)
                {
                    List<OutletsOut> listItems = new List<OutletsOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new OutletsOut
                        {
                            ID = dr["csh_ID"].ToString(),
                            Code = dr["csh_Code"].ToString(),
                            Name = dr["csh_Name"].ToString(),


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
                dm.TraceService(" SelectOutlets Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectOutlets ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string GetLoadInData([FromForm] PostLoadInData inputParams)
        {
            dm.TraceService("GetLoadInData STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            try
            {
                string rot_ID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
                dm.TraceService("rot_ID -   " + rot_ID + " - " + DateTime.Now.ToString());
                string[] arr = {  };
                DataSet dtreturn = dm.loadListDS("SelLoadInData", "sp_Dispatch", rot_ID.ToString(), arr);
                dm.TraceService("SelLoadInData - Query success " + " - " + DateTime.Now.ToString());
                DataTable HeaderData = dtreturn.Tables[0];
                dm.TraceService("HeaderData" + DateTime.Now.ToString());

                DataTable DetailData = dtreturn.Tables[1];
                dm.TraceService("DetailData" + DateTime.Now.ToString());

                DataTable batchData = dtreturn.Tables[2];
                dm.TraceService("batchData" + DateTime.Now.ToString());

           
                dm.TraceService("try " + DateTime.Now.ToString());
                if (HeaderData.Rows.Count > 0)
                {
                    dm.TraceService("Header condition success " + DateTime.Now.ToString());
                    List<GetLoadInHeader> listHeader = new List<GetLoadInHeader>();
                    foreach (DataRow dr in HeaderData.Rows)
                    {
                        List<GetDispatchItemHeader> listDetail = new List<GetDispatchItemHeader>();
                        foreach (DataRow drDetails in DetailData.Rows)
                        {
                            List<GetDispatchBatchSerial> listBatchSerial = new List<GetDispatchBatchSerial>();
                            foreach (DataRow drbatch in batchData.Rows)
                            {
                                string s = drbatch["prd_ID"].ToString();
                                string M = drDetails["dsd_prd_ID"].ToString();
                                
                                    if (drDetails["dsd_ID"].ToString() == drbatch["dbs_dsd_ID"].ToString())
                                    {
                                        listBatchSerial.Add(new GetDispatchBatchSerial
                                        {
                                            DispBatSerialId = drbatch["dbs_ID"].ToString(),
                                            DisDetailId = drbatch["dbs_dsd_ID"].ToString(),
                                            Number = drbatch["dbs_Number"].ToString(),
                                            ExpiryDate = drbatch["dbs_ExpiryDate"].ToString(),
                                            BaseUOM = drbatch["dbs_BaseUOM"].ToString(),
                                            OrderedQty = drbatch["dbs_OrderedQty"].ToString(),
                                            AdjustedQty = drbatch["dbs_AdjustedQty"].ToString(),
                                            LoadInQty = drbatch["dbs_LoadInQty"].ToString(),
                                            ItemCode = drbatch["prd_Code"].ToString(),
                                            LineNo = drbatch["dsd_LineNo"].ToString(),
                                        });
                                    }
                                
                            }

                            if (drDetails["dsd_dsp_ID"].ToString() == dr["dsp_ID"].ToString())
                            {


                                listDetail.Add(new GetDispatchItemHeader
                                {

                                    Id = Int32.Parse(drDetails["prd_ID"].ToString()),
                                    Name = drDetails["prd_Name"].ToString(),
                                    Code = drDetails["prd_Code"].ToString(),
                                    Spec = drDetails["prd_Spec"].ToString(),
                                    Warehouse = drDetails["war_Code"].ToString(),
                                    Rack = drDetails["rak_Code"].ToString(),
                                    Basket = drDetails["bas_Code"].ToString(),
                                    LocationId = drDetails["plm_ID"].ToString(),
                                    LocationName = drDetails["plm_Name"].ToString(),
                                    SysHUOM = drDetails["dsd_HigherUOM"].ToString(),
                                    SysLUOM = drDetails["dsd_LowerUOM"].ToString(),
                                    SysHQty = drDetails["dsd_HigherQty"].ToString(),
                                    SysLQty = drDetails["dsd_LowerQty"].ToString(),
                                    AdjHUOM = drDetails["dsd_AdjustedHigherUOM"].ToString(),
                                    AdjLUOM = drDetails["dsd_AdjustedLowerUOM"].ToString(),
                                    AdjHQty = drDetails["dsd_AdjustedHigherQty"].ToString(),
                                    AdjLQty = drDetails["dsd_AdjustedLowerQty"].ToString(),
                                    LiHUOM = drDetails["dsd_FinalHigherUOM"].ToString(),
                                    LiLUOM = drDetails["dsd_FinalLowerUOM"].ToString(),
                                    LiHQty = drDetails["dsd_FinalHigherQty"].ToString(),
                                    LiLQty = drDetails["dsd_FinalLowerQty"].ToString(),
                                    PromoType = drDetails["dsd_TransType"].ToString(),
                                    WeighingItem = drDetails["prd_WeighingItem"].ToString(),
                                    LineNo = drDetails["dsd_LineNo"].ToString(),
                                    BatchSerial = listBatchSerial,
                                    prd_Desc = drDetails["prd_Desc"].ToString(),
                                    prd_LongDesc = drDetails["prd_LongDesc"].ToString(),
                                    prd_cat_id = drDetails["prd_cat_id"].ToString(),
                                    prd_sub_ID = drDetails["prd_sub_ID"].ToString(),
                                    prd_brd_ID = drDetails["prd_brd_ID"].ToString(),
                                    prd_EnableOrderHold = drDetails["prd_EnableOrderHold"].ToString(),
                                    prd_EnableReturnHold = drDetails["prd_EnableReturnHold"].ToString(),
                                    prd_EnableDeliveryHold = drDetails["prd_EnableDeliveryHold"].ToString(),
                                    prd_NameArabic = drDetails["prd_NameArabic"].ToString(),
                                    prd_DescArabic = drDetails["prd_DescArabic"].ToString(),
                                    prd_Image = drDetails["prd_Image"].ToString(),
                                    prd_SortOrder = drDetails["prd_SortOrder"].ToString(),
                                    brd_Code = drDetails["brd_Code"].ToString(),
                                    brd_Name = drDetails["brd_Name"].ToString(),
                                    prd_BaseUOM = drDetails["prd_BaseUOM"].ToString(),
                                    VATPercent = drDetails["odd_VATPercent"].ToString(),
                                    Price = drDetails["odd_Price"].ToString(),
                                    Discount = drDetails["odd_Discount"].ToString(),
                                    DiscountPercentage = drDetails["odd_DiscountPercentage"].ToString(),
                                   
                                });
                            }
                        }

                        listHeader.Add(new GetLoadInHeader
                        {
                            dsp_DispatchID = dr["dsp_DispatchID"].ToString(),
                            OrderID = dr["OrderID"].ToString(),
                            dsp_ID = dr["dsp_ID"].ToString(),                           
                            cus_ID = dr["cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            cus_ShortName = dr["cus_ShortName"].ToString(),
                            ArabicCusName = dr["cus_ArabicName"].ToString(),
                            ArabicDeliveryType = dr["dsp_ArabicDeliveryType"].ToString(),
                            Date = dr["Date"].ToString(),
                            Time = dr["Time"].ToString(),
                             Status = dr["Status"].ToString(),
                            NeedDownload = dr["NeedDownload"].ToString(),
                             dsp_DeliveryType = dr["dsp_DeliveryType"].ToString(),
                              DeliveyType = dr["DeliveyType"].ToString(),
                            ord_PayMode = dr["ord_PayMode"].ToString(),
                            dsp_LOStatus = dr["dsp_LOStatus"].ToString(),
                            TotalAmount = dr["TotalAmount"].ToString(),
                            PickLocation = dr["PickLocation"].ToString(),
                            ord_LPONumber = dr["ord_LPONumber"].ToString(),
                            IsPickupOrder = dr["IsPickupOrder"].ToString(),
                            ord_dep_ID = dr["ord_dep_ID"].ToString(),
                            ord_CashPaidStatus = dr["ord_CashPaidStatus"].ToString(),
                            Customer = dr["Customer"].ToString(),
                            Outlet = dr["Outlet"].ToString(),
                              OutletID = dr["OutletID"].ToString(),
                               PickLocationID = dr["PickLocationID"].ToString(),
                               VanToVan = dr["VanToVan"].ToString(),
                               LoadInDetail = listDetail,
                            dln_DeliveryNumber =dr["dln_DeliveryNumber"].ToString(),
                            dln_ID = dr["dln_ID"].ToString(),
                            dsp_IsPartialLoad = dr["dsp_IsPartialLoad"].ToString(),
                            Type = dr["Type"].ToString(),
                            cus_Type = dr["cus_Type"].ToString(),
                            IsHelper= dr["IsHelper"].ToString(),
                            OrderReleasedBy = dr["ord_ReleasedBy"].ToString(),
                            csh_InvocieLocation = dr["csh_InvocieLocation"].ToString(),
                        });
                    }

                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listHeader
                    });

                    return JSONString;
                }
                else
                {
                    dm.TraceService("header condition failed " + DateTime.Now.ToString());
                    dm.TraceService("NoDataRes");
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                dm.TraceService("catch " + DateTime.Now.ToString());
                dm.TraceService(ex.Message.ToString());
                JSONString = "GetLoadInData - " + ex.Message.ToString();
            }
            dm.TraceService("GetLoadInData ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            return JSONString;
        }

        public string GetNotDispatchedItemBatch([FromForm] PostNotDispatchData inputParams)
        {
            dm.TraceService("GetNotDispatchedItemBatch STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string ordID = inputParams.OrdId == null ? "0" : inputParams.OrdId;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

            string[] arr = { userID.ToString() };
            DataSet dtItemBatch = dm.loadListDS("SelNotDispatchedBatchSerial", "sp_Dispatch", ordID.ToString(), arr);
            DataTable itemData = dtItemBatch.Tables[0];
            DataTable batchData = dtItemBatch.Tables[1];
            //Items In Picklist 

            //Batch/Serial Number in Picklist against each item

            try
            {
                if (itemData.Rows.Count > 0)
                {
                    List<GetNotDispatchItemHeader> listItems = new List<GetNotDispatchItemHeader>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<GetNotDispatchBatchSerial> listBatchSerial = new List<GetNotDispatchBatchSerial>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["prd_Code"].ToString() == drDetails["prd_Code"].ToString() && dr["odd_ID"].ToString() == drDetails["obs_odd_ID"].ToString())
                            {
                                listBatchSerial.Add(new GetNotDispatchBatchSerial
                                {
                                    OrdBatSerialId = drDetails["obs_ID"].ToString(),
                                    OrdDetailId = drDetails["obs_odd_ID"].ToString(),
                                    Number = drDetails["obs_Number"].ToString(),
                                    ExpiryDate = drDetails["obs_ExpiryDate"].ToString(),
                                    BaseUOM = drDetails["obs_BaseUOM"].ToString(),
                                    OrderedQty = drDetails["obs_Qty"].ToString(),
                                  
                                    ItemCode = drDetails["prd_Code"].ToString(),
                                    LineNo = drDetails["odd_LineNo"].ToString(),
                                });
                            }
                        }

                        listItems.Add(new GetNotDispatchItemHeader
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
                            SysHUOM = dr["odd_HigherUOM"].ToString(),
                            SysLUOM = dr["odd_LowerUOM"].ToString(),
                            SysHQty = dr["odd_HigherQty"].ToString(),
                            SysLQty = dr["odd_LowerQty"].ToString(),
                            PromoType = dr["odd_TransType"].ToString(),
                            WeighingItem = dr["prd_WeighingItem"].ToString(),
                            LineNo = dr["odd_LineNo"].ToString(),
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
            dm.TraceService("GetNotDispatchedItemBatch ENDED - " + DateTime.Now);
            dm.TraceService("==================");
            return JSONString;
        }
        public string SelectPicklocation([FromForm] PicklocationIn inputParams)
        {
            dm.TraceService("SelectPicklocation STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rot_ID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
               
                DataTable dtitem = dm.loadList("SelPickLocation", "sp_Dispatch", rot_ID.ToString());

                if (dtitem.Rows.Count > 0)
                {
                    List<PicklocationOut> listItems = new List<PicklocationOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new PicklocationOut
                        {
                            PickLocID = dr["plm_ID"].ToString(),
                            PickLocCode = dr["plm_Code"].ToString(),
                            PickLocName = dr["plm_Name"].ToString(),


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
                dm.TraceService(" SelectPicklocation Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectPicklocation ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectLoadInCompleted([FromForm] PostLoadInData inputParams)
        {
            dm.TraceService("SelectLoadInCompleted STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rot_ID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
             
                DataTable dtitem = dm.loadList("SelLoadInCompleted", "sp_Dispatch", rot_ID.ToString());

                if (dtitem.Rows.Count > 0)
                {
                    List<GetLoadInHeader> listItems = new List<GetLoadInHeader>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new GetLoadInHeader
                        {
                            dsp_DispatchID = dr["dsp_DispatchID"].ToString(),
                            OrderID = dr["OrderID"].ToString(),
                            dsp_ID = dr["dsp_ID"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            cus_ShortName = dr["cus_ShortName"].ToString(),
                            ArabicCusName = dr["cus_ArabicName"].ToString(),
                            ArabicDeliveryType = dr["dsp_ArabicDeliveryType"].ToString(),
                            Date = dr["Date"].ToString(),
                            Time = dr["Time"].ToString(),
                            Status = dr["Status"].ToString(),
                            NeedDownload = dr["NeedDownload"].ToString(),
                            dsp_DeliveryType = dr["dsp_DeliveryType"].ToString(),
                            DeliveyType = dr["DeliveyType"].ToString(),
                            ord_PayMode = dr["ord_PayMode"].ToString(),
                            dsp_LOStatus = dr["dsp_LOStatus"].ToString(),
                            TotalAmount = dr["TotalAmount"].ToString(),
                            PickLocation = dr["PickLocation"].ToString(),
                            ord_LPONumber = dr["ord_LPONumber"].ToString(),
                            IsPickupOrder = dr["IsPickupOrder"].ToString(),
                            ord_dep_ID = dr["ord_dep_ID"].ToString(),
                            ord_CashPaidStatus = dr["ord_CashPaidStatus"].ToString(),
                            Customer = dr["Customer"].ToString(),
                            Outlet = dr["Outlet"].ToString(),
                            OutletID = dr["OutletID"].ToString(),
                            PickLocationID = dr["PickLocationID"].ToString(),
                            VanToVan = dr["VanToVan"].ToString(),
                            dln_DeliveryNumber = dr["dln_DeliveryNumber"].ToString(),
                            dln_ID = dr["dln_ID"].ToString(),
                            dsp_IsPartialLoad = dr["dsp_IsPartialLoad"].ToString(),

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
                dm.TraceService(" SelectLoadInCompleted Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectLoadInCompleted ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string PrintLoadIn([FromForm] PrintIn inputParams)
        {
            dm.TraceService("PrintLoadIn STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PrintLoadIn> itemData = JsonConvert.DeserializeObject<List<PrintLoadIn>>(inputParams.JsonString);

                string rot_ID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
                string userID = inputParams.user_ID == null ? "0" : inputParams.user_ID;
                string udpID = inputParams.udp_ID == null ? "0" : inputParams.udp_ID;

                string InputXml = "";
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PrintLoadIn id in itemData)
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

                string[] ar = { InputXml.ToString(),userID.ToString(),udpID.ToString() };
                DataTable dtitem = dm.loadList("InsPrintLoadIn", "sp_Dispatch", rot_ID.ToString() ,ar);

                if (dtitem.Rows.Count > 0)
                {
                    List<PrintOut> listItems = new List<PrintOut>();
                    foreach (DataRow dr in dtitem.Rows)
                    {

                        listItems.Add(new PrintOut
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),
                            Descr = dr["Descr"].ToString(),

                        }) ;
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
                dm.TraceService("PrintLoadIn Exception - " + ex.Message.ToString());
            }
            dm.TraceService("PrintLoadIn ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

       
    }
}