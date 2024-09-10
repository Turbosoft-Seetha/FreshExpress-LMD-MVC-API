using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using MVC_API.FE_Nav_Service;
using MVC_API.Models;
using MVC_API.Models.CustomerConnectHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Web.DynamicData;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers
{
    public class InventoryController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;

        //Items and Batch based on one picklist - 2 Dataset and send as single JSON - INPUT - PicklistID, UserID
        [HttpPost]
        public string GetItemBatch([FromForm] PostPickingData inputParams)
        {
            dm.TraceService("GetItemBatch STARTED ");
            dm.TraceService("====================");
            try
            {
                string pickingID = inputParams.PickingId == null ? "0" : inputParams.PickingId;
                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;


                string[] arr = { userID.ToString() };
                DataSet dtItemBatch = dm.loadListDS("SelItemBatch", "sp_Inventory", pickingID.ToString(), arr);
                DataTable itemData = dtItemBatch.Tables[0];
                DataTable batchData = dtItemBatch.Tables[1];
                if (itemData.Rows.Count > 0)
                {
                    List<GetPickingItemHeader> listItems = new List<GetPickingItemHeader>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<GetPickingBatchSerial> listBatchSerial = new List<GetPickingBatchSerial>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["prd_Code"].ToString() == drDetails["prd_Code"].ToString() && dr["pid_ID"].ToString() == drDetails["pbs_pid_ID"].ToString())
                            {
                                listBatchSerial.Add(new GetPickingBatchSerial
                                {
                                    Number = drDetails["pbs_Number"].ToString(),
                                    ExpiryDate = drDetails["pbs_ExpiryDate"].ToString(),
                                    BaseUOM = drDetails["pbs_BaseUOM"].ToString(),
                                    OrderedQty = drDetails["pbs_OrderedQty"].ToString(),
                                    PickedQty = drDetails["pbs_PickedQty"].ToString(),
                                    AdjustedQty = drDetails["pbs_AdjustedQty"].ToString(),
                                    ItemCode = drDetails["prd_Code"].ToString(),
                                    ReasonId = drDetails["pbs_rsn_ID"].ToString(),
                                    UserId = drDetails["ModifiedBy"].ToString(),
                                    salesman = drDetails["bat_SalesPerson"].ToString(),
                                    EligibleQty = drDetails["bat_AvailbleQty"].ToString(),
                                    BatchID = drDetails["pbs_ID"].ToString(),
                                    pid_ID = drDetails["pbs_pid_ID"].ToString(),
                                    ReservationNo = drDetails["pbs_ReservationNumber"].ToString()
                                });
                            }
                        }

                        listItems.Add(new GetPickingItemHeader
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
                            OHUOM = dr["pid_OrdHuom"].ToString(),
                            OLUOM = dr["pid_OrdLuom"].ToString(),
                            OHQty = dr["pid_OrdHqty"].ToString(),
                            OLQty = dr["pid_OrdLqty"].ToString(),
                            HigherUOM = dr["pid_HigherUOM"].ToString(),
                            LowerUOM = dr["pid_LowerUOM"].ToString(),
                            HigherQty = dr["pid_HigherQty"].ToString(),
                            LowerQty = dr["pid_LowerQty"].ToString(),
                            PromoType = dr["pid_TransType"].ToString(),
                            ReasonId = dr["pid_rsn_ID"].ToString(),
                            EnableExcess = dr["prd_EnableExcess"].ToString(),
                            WeighingItem = dr["prd_WeighingItem"].ToString(),
                            Desc = dr["prd_Desc"].ToString(),
                            LineNo = dr["pid_LineNo"].ToString(),
                            pid_ID = dr["pid_ID"].ToString(),
                            BatchSerial = listBatchSerial,
                            odd_PromoType = dr["odd_PromoType"].ToString(),
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
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService("NoDataSQL - " + ex.Message.ToString());
            }

            dm.TraceService("GetItemBatch ENDED ");
            dm.TraceService("====================");

            return JSONString;
        }

        // New Batch Selection INPUT - orderID, itmID, userID, picklistID and OUTPUT - Batch as single JSON - Need to call FE API
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
                        listNewBatch.Add(new GetNewBatch
                        {
                            Id = 0,
                            Number = bd_info.BatchCode,
                            ExpiryDate = bd_info.ExpirationDate,
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

        //public string GetNewBatch([FromForm] PostOrderData inputParams)
        //{
        //    dm.TraceService("GetNewBatch STARTED ");
        //    dm.TraceService("====================");
        //    string itemID = inputParams.ItemId == null ? "0" : inputParams.ItemId;
        //    string orderID = inputParams.OrderId == null ? "0" : inputParams.OrderId;
        //    string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

        //    string[] arr = { orderID.ToString(), userID.ToString() };
        //    DataTable dtNewBatch = dm.loadList("SelectBatch", "sp_Inventory", itemID.ToString(), arr);
        //    try
        //    {
        //        if (dtNewBatch.Rows.Count > 0)
        //        {
        //            List<GetNewBatch> listNewBatch = new List<GetNewBatch>();
        //            foreach (DataRow dr in dtNewBatch.Rows)
        //            {
        //                listNewBatch.Add(new GetNewBatch
        //                {
        //                    Id = Int32.Parse(dr["bat_ID"].ToString()),
        //                    Number = dr["bat_Number"].ToString(),
        //                    ExpiryDate = dr["bat_ExpiryDate"].ToString(),
        //                    AvailableQty = dr["bat_AvailbleQty"].ToString(),
        //                    SalesPerson = dr["bat_SalesPerson"].ToString()

        //                });
        //            }
        //            JSONString = JsonConvert.SerializeObject(new
        //            {
        //                result = listNewBatch
        //            });

        //            return JSONString;
        //        }
        //        else
        //        {
        //            JSONString = "NoDataRes";
        //            dm.TraceService("NoDataRes");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        JSONString = "NoDataSQL - " + ex.Message.ToString();
        //        dm.TraceService(ex.Message.ToString());
        //    }
        //    dm.TraceService("GetNewBatch ENDED ");
        //    dm.TraceService("==================");
        //    return JSONString;
        //}


        //ParkPicking - Status Changing
        public string GetPickParkAndParkRelease([FromForm] PostParkAndParkRelease inputParams)
        {
            dm.TraceService("GetPickParkAndParkRelease STARTED ");
            dm.TraceService("==================================");
            try
            {
                List<PostPickingItemData> itemData = JsonConvert.DeserializeObject<List<PostPickingItemData>>(inputParams.ItemData);
                List<PostPickingBatchData> batchData = JsonConvert.DeserializeObject<List<PostPickingBatchData>>(inputParams.BatchData);
                try
                {
                    string pickingID = inputParams.PickingId == null ? "0" : inputParams.PickingId;
                    string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
                    string status = inputParams.Status == null ? "0" : inputParams.Status;

                    DataSet dsItemBatch = new DataSet();

                    DataTable dtItem = new DataTable();
                    dtItem.Columns.Add("ProductId", typeof(int));
                    dtItem.Columns.Add("ProductHUOM", typeof(string));
                    dtItem.Columns.Add("ProductLUOM", typeof(string));
                    dtItem.Columns.Add("ProductHQty", typeof(string));
                    dtItem.Columns.Add("ProductLQty", typeof(string));
                    dtItem.Columns.Add("ReasonId", typeof(string));
                    dtItem.Columns.Add("UserId", typeof(string));
                    dtItem.Columns.Add("ModifiedOn", typeof(string));
                    dtItem.Columns.Add("LineNumber", typeof(string));


                    DataTable dtBatch = new DataTable();
                    dtBatch.Columns.Add("ProductId", typeof(int));
                    dtBatch.Columns.Add("Number", typeof(string));
                    dtBatch.Columns.Add("PickedQty", typeof(string));
                    dtBatch.Columns.Add("AdjustedQty", typeof(string));
                    dtBatch.Columns.Add("ReasonId", typeof(string));
                    dtBatch.Columns.Add("BatchMode", typeof(string));
                    dtBatch.Columns.Add("ExpiryDate", typeof(string));
                    dtBatch.Columns.Add("UserId", typeof(string));
                    dtBatch.Columns.Add("ModifiedOn", typeof(string));
                    dtBatch.Columns.Add("BatchID", typeof(string));
                    dtBatch.Columns.Add("pid_ID", typeof(string));
                    dtBatch.Columns.Add("SalesPerson", typeof(string));
                    dtBatch.Columns.Add("EligibleQty", typeof(string));
                    //dtBatch.Columns.Add("BaseUOM", typeof(string));
                    foreach (var item in itemData)
                    {
                        dtItem.Rows.Add(item.ProductId, item.ProductHUOM, item.ProductLUOM, item.ProductHQty, item.ProductLQty, item.ReasonId, item.UserId, item.ModifiedOn, item.LineNumber);
                    }

                    foreach (var batch in batchData)
                    {
                        dtBatch.Rows.Add(batch.ProductId, batch.Number, batch.PickedQty, batch.AdjustedQty, batch.ReasonId, batch.BatchMode, batch.ExpiryDate, batch.UserId, batch.ModifiedOn, batch.BatchID, batch.pid_ID, batch.SalesPerson, batch.EligibleQty);
                    }

                    dsItemBatch.Tables.Add(dtItem);
                    dsItemBatch.Tables.Add(dtBatch);

                    try
                    {
                        string[] keys = { "@PickingId", "@UserId", "@Status" };
                        string[] values = { pickingID.ToString(), userID.ToString(), status.ToString() };
                        string[] arr = { "@ItemData", "@BatchData" };
                        DataSet Value = dm.bulkUpdate(dsItemBatch, arr, keys, values, "sp_CompletePicking");
                        foreach (DataTable table in Value.Tables)
                        {
                            string Mode = table.Rows[0]["res"].ToString();
                            string Status = table.Rows[0]["status"].ToString();
                            dm.TraceService("Response from Sql Procedure : Mode=" + Mode + " and Status=" + Status);
                            List<GetInsertStatus> listStatus = new List<GetInsertStatus>();
                            listStatus.Add(new GetInsertStatus
                            {
                                Mode = Mode,
                                Status = Status
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

            dm.TraceService("GetPickParkAndParkRelease ENDED ");
            dm.TraceService("================================");

            return JSONString;
        }

        //CompletePick - Need to call FE API - INPUT - JSON, if new batch then call FE API else 
        public string GetPickingComplete([FromForm] PostCompletePicking inputParams)
        {
            dm.TraceService("GetPickingComplete STARTED ");
            dm.TraceService("===========================");
            try
            {
                dm.TraceService("GetPickingComplete inparas ERP_OrderNo - " + inputParams.ERP_OrderNo);
                dm.TraceService("GetPickingComplete inparas ItemData - " + inputParams.ItemData + DateTime.Now.ToString());
                dm.TraceService("GetPickingComplete inparas BatchData - " + inputParams.BatchData + DateTime.Now.ToString());
                dm.TraceService("GetPickingComplete inparas IntegrationMode - " + inputParams.IntegrationMode );
              
                
                List<PostPickingItemData> itemData = JsonConvert.DeserializeObject<List<PostPickingItemData>>(inputParams.ItemData);
                List<PostPickingBatchData> batchData = JsonConvert.DeserializeObject<List<PostPickingBatchData>>(inputParams.BatchData);

                try
                {
                    List<ErrorBatches> Errorbatches = new List<ErrorBatches>();
                    string mode = "0";
                    try
                    {
                        mode = inputParams.IntegrationMode.ToString();
                    }
                    catch(Exception ex)
                    {
                        mode = "0";
                    }

                    if (dm.ERP_API_CALL() > 0 && mode.Equals("1"))
                    {
						MasterMgmntDigits gm = dm.Creds();
						OrderStatus1 itemsInPickList = new OrderStatus1();
                        List<BatchAvailability> batchAvailabilities = new List<BatchAvailability>();

                        int apiCall = 0;

                        foreach (PostPickingBatchData pd in batchData)
                        {
                            if (decimal.Parse(pd.PickedQty) > 0)
                            {
                                apiCall = 1;


                                decimal pkQty = Math.Round(decimal.Parse(pd.PickedQty),3);
                                decimal exQty = Math.Round(decimal.Parse(pd.ExtraQty),3);
                                string finQty = "";

                                //try
                                //{
                                //    finQty = (pkQty - exQty).ToString();
                                //}
                                //catch (Exception ex)
                                //{
                                //    finQty = pkQty.ToString();

                                //}

                                finQty = pkQty.ToString();

                                BatchAvailability batchAvailability = new BatchAvailability();
                                batchAvailability.QtyPicked = finQty;
                                batchAvailability.DocumentNo = inputParams.ERP_OrderNo;
                                batchAvailability.LineNo = pd.itemLineNo;
                                batchAvailability.DeviceUser = inputParams.UserId.ToString();
                                batchAvailability.BatchNo = pd.Number;
                                batchAvailability.batchLineNo = pd.BatchID;
                                batchAvailability.AvailQty = "0";
                                batchAvailability.Status = "Y";
                                batchAvailability.Description = "";
                                batchAvailability.ExtraQty = exQty.ToString();
                                batchAvailability.ItemNo = pd.itemCode;
                                batchAvailability.ReservationEntryNo = pd.ReservationLineNo;
                                batchAvailability.TransType = "1";
                                batchAvailability.SerialNo = 0;
                                batchAvailabilities.Add(batchAvailability);

                                dm.TraceService("GetPickingComplete Item Added to the list => " + pd.itemCode + " LineNumber => " + pd.itemLineNo + " PickedQty =>" + pd.PickedQty + " ExtraQty =>" + exQty.ToString());
                            }
                            else
                            {
                                dm.TraceService("GetPickingComplete Item picked Quantity is 0 for this item => " + pd.itemCode + " LineNumber => " + pd.itemLineNo);
                            }
                        }

                        itemsInPickList.BatchAvailability = batchAvailabilities.ToArray();

                        if (apiCall > 0)
                        {
                            dm.TraceService("GetPickingComplete FE API Call started At - " + DateTime.Now.ToString());
                            gm.ConfirmPicking(ref itemsInPickList, inputParams.UserId);
                            dm.TraceService("GetPickingComplete FE API Call Completed At - " + DateTime.Now.ToString());

                            //ERP CALL COMPLETED
                            batchAvailabilities = itemsInPickList.BatchAvailability.ToList();

                            foreach (BatchAvailability availability in batchAvailabilities)
                            {
                                decimal x = 0;

                                try
                                {
                                    x = Decimal.Parse(availability.QtyPicked.ToString());
                                    dm.TraceService("Item Sent to ERP-> " + availability.ItemNo + " and item qty " + availability.QtyPicked + "and eligible Qty " + availability.AvailQty
                                        + " Batch Number " + availability.BatchNo + " and Status " + availability.Status);
                                }
                                catch (Exception ex)
                                {
                                    dm.TraceService("Item Sent to ERP Exception-> " + availability.ItemNo + " and item qty " + availability.QtyPicked + "and eligible Qty " + availability.AvailQty
                                       + " Batch Number " + availability.BatchNo + " and Status " + availability.Status);
                                }

                                if (!availability.Status.Equals("Y") && x > 0)
                                {
                                    ErrorBatches pb = new ErrorBatches();
                                    pb.Status = "N";
                                    pb.EligibleQty = availability.AvailQty;
                                    pb.Number = availability.BatchNo;
                                    pb.BatchID = availability.batchLineNo;
                                    pb.itemLineNo = availability.LineNo;
                                    pb.Desc = availability.Description;
                                    Errorbatches.Add(pb);
                                }
                            }
                        }

                    }


                    if (Errorbatches != null && Errorbatches.Count > 0)
                    {
                        List<GetInsertStatus> listStatus = new List<GetInsertStatus>();
                        listStatus.Add(new GetInsertStatus
                        {
                            Mode = "0",
                            Status = "Faiure"

                        });
                        string JSONString = JsonConvert.SerializeObject(new
                        {
                            result = listStatus,
                            ErrorBatchs = Errorbatches
                        });

                        dm.TraceService("GetPickingComplete Error bactches Response " + JSONString);
                        return JSONString;
                    }
                    else
                    {
                        try
                        {
                            string pickingID = inputParams.PickingId == null ? "0" : inputParams.PickingId;
                            string userID = inputParams.UserId == null ? "0" : inputParams.UserId.ToString();
                            string status = inputParams.Status == null ? "0" : inputParams.Status;

                            DataSet dsItemBatch = new DataSet();

                            DataTable dtItem = new DataTable();
                            dtItem.Columns.Add("ProductId", typeof(int));
                            dtItem.Columns.Add("ProductHUOM", typeof(string));
                            dtItem.Columns.Add("ProductLUOM", typeof(string));
                            dtItem.Columns.Add("ProductHQty", typeof(string));
                            dtItem.Columns.Add("ProductLQty", typeof(string));
                            dtItem.Columns.Add("ReasonId", typeof(string));
                            dtItem.Columns.Add("UserId", typeof(string));
                            dtItem.Columns.Add("ModifiedOn", typeof(string));
                            dtItem.Columns.Add("LineNumber", typeof(string));

                            DataTable dtBatch = new DataTable();
                            dtBatch.Columns.Add("ProductId", typeof(int));
                            dtBatch.Columns.Add("Number", typeof(string));
                            dtBatch.Columns.Add("PickedQty", typeof(string));
                            dtBatch.Columns.Add("AdjustedQty", typeof(string));
                            dtBatch.Columns.Add("ReasonId", typeof(string));
                            dtBatch.Columns.Add("BatchMode", typeof(string));
                            dtBatch.Columns.Add("ExpiryDate", typeof(string));
                            dtBatch.Columns.Add("UserId", typeof(string));
                            dtBatch.Columns.Add("ModifiedOn", typeof(string));
                            dtBatch.Columns.Add("BatchID", typeof(string));
                            dtBatch.Columns.Add("pid_ID", typeof(string));
                            dtBatch.Columns.Add("SalesPerson", typeof(string));
                            dtBatch.Columns.Add("EligibleQty", typeof(string));
                            // dtBatch.Columns.Add("BaseUOM", typeof(string));
                            foreach (var item in itemData)
                            {
                                dtItem.Rows.Add(item.ProductId, item.ProductHUOM, item.ProductLUOM, item.ProductHQty, item.ProductLQty, item.ReasonId, item.UserId, item.ModifiedOn, item.LineNumber);
                            }

                            foreach (var batch in batchData)
                            {
                                dtBatch.Rows.Add(batch.ProductId, batch.Number, batch.PickedQty, batch.AdjustedQty, batch.ReasonId, batch.BatchMode, batch.ExpiryDate, batch.UserId, batch.ModifiedOn, batch.BatchID, batch.pid_ID, batch.SalesPerson, batch.EligibleQty);
                            }

                            dsItemBatch.Tables.Add(dtItem);
                            dsItemBatch.Tables.Add(dtBatch);

                            try
                            {
                                string[] keys = { "@PickingId", "@UserId", "@Status" };
                                string[] values = { pickingID.ToString(), userID.ToString(), status.ToString() };
                                string[] arr = { "@ItemData", "@BatchData" };
                                DataSet Value = dm.bulkUpdate(dsItemBatch, arr, keys, values, "sp_CompletePicking");
                                foreach (DataTable table in Value.Tables)
                                {
                                    string Mode = table.Rows[0]["res"].ToString();
                                    string Status = table.Rows[0]["status"].ToString();
                                    dm.TraceService("Response from Sql Procedure : Mode=" + Mode + " and Status=" + Status);
                                    List<GetInsertStatus> listStatus = new List<GetInsertStatus>();
                                    listStatus.Add(new GetInsertStatus
                                    {
                                        Mode = Mode,
                                        Status = Status
                                    });
                                    string JSONString = JsonConvert.SerializeObject(new
                                    {
                                        result = listStatus
                                    });
                                    dm.TraceService(JSONString);
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
                }
                catch (Exception ex)
                {
                    dm.TraceService("NoDataERP - " + ex.Message.ToString());
                    JSONString = "NoDataERP - " + ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                dm.TraceService("NoDataSQL - " + ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }

            dm.TraceService("GetPickingComplete ENDED ");
            dm.TraceService("=========================");

            return JSONString;
        }

        public string GetPickingItemBatch([FromForm] PostPickingData inputParams)
        {
            dm.TraceService("GetPickingItemBatch STARTED " + DateTime.Now.ToString());
            dm.TraceService("============================");
            try
            {
                string pickingID = inputParams.PickingId == null ? "0" : inputParams.PickingId;
                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

                string[] arr = { userID.ToString() };
                DataSet dtItemBatch = dm.loadListDS("SelPickingItemBatch", "sp_Inventory", pickingID.ToString(), arr);
                DataTable pickData = dtItemBatch.Tables[0];
                DataTable itemData = dtItemBatch.Tables[1];
                DataTable batchData = dtItemBatch.Tables[2];
                if (pickData.Rows.Count > 0)
                {
                    List<PickListInfo> listPicking = new List<PickListInfo>();
                    foreach (DataRow drp in pickData.Rows)
                    {
                        List<GetPickingItemHeader> listItems = new List<GetPickingItemHeader>();
                        foreach (DataRow dr in itemData.Rows)
                        {
                            List<GetPickingBatchSerial> listBatchSerial = new List<GetPickingBatchSerial>();
                            foreach (DataRow drDetails in batchData.Rows)
                            {
                                if (dr["prd_Code"].ToString() == drDetails["prd_Code"].ToString())
                                {
                                    listBatchSerial.Add(new GetPickingBatchSerial
                                    {
                                        Number = drDetails["pbs_Number"].ToString(),
                                        ExpiryDate = drDetails["pbs_ExpiryDate"].ToString(),
                                        BaseUOM = drDetails["pbs_BaseUOM"].ToString(),
                                        OrderedQty = drDetails["pbs_OrderedQty"].ToString(),
                                        PickedQty = drDetails["pbs_PickedQty"].ToString(),
                                        AdjustedQty = drDetails["pbs_AdjustedQty"].ToString(),
                                        ItemCode = drDetails["prd_Code"].ToString(),
                                        ReasonId = drDetails["pbs_rsn_ID"].ToString(),
                                        UserId = drDetails["ModifiedBy"].ToString()

                                    });
                                }
                            }
                            if (drp["pih_ID"].ToString() == dr["pih_ID"].ToString())
                            {
                                listItems.Add(new GetPickingItemHeader
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
                                    OHUOM = dr["pid_OrdHuom"].ToString(),
                                    OLUOM = dr["pid_OrdLuom"].ToString(),
                                    OHQty = dr["pid_OrdHqty"].ToString(),
                                    OLQty = dr["pid_OrdLqty"].ToString(),
                                    HigherUOM = dr["pid_HigherUOM"].ToString(),
                                    LowerUOM = dr["pid_LowerUOM"].ToString(),
                                    HigherQty = dr["pid_HigherQty"].ToString(),
                                    LowerQty = dr["pid_LowerQty"].ToString(),
                                    PromoType = dr["pid_TransType"].ToString(),
                                    ReasonId = dr["pid_rsn_ID"].ToString(),
                                    EnableExcess = dr["prd_EnableExcess"].ToString(),
                                    WeighingItem = dr["prd_WeighingItem"].ToString(),
                                    BatchSerial = listBatchSerial,
                                });
                            }
                        }
                        listPicking.Add(new PickListInfo
                        {
                            PickingId = Int32.Parse(drp["pih_ID"].ToString()),
                            OrderId = drp["pih_ord_ID"].ToString(),
                            PickingLocation = drp["plm_Name"].ToString(),
                            CusHeaderCode = drp["csh_Code"].ToString(),
                            CusHeaderName = drp["csh_Name"].ToString(),
                            CustomerCode = drp["cus_Code"].ToString(),
                            CustomerName = drp["cus_Name"].ToString(),
                            ExpectedDeliveryDate = drp["ord_ExpectedDelDate"].ToString(),
                            PickListNumber = drp["pih_Number"].ToString(),
                            Picker = drp["pih_User"].ToString(),
                            PickListStatus = drp["pih_Status"].ToString(),
                            Mode = drp["Mode"].ToString(),
                            PickerContactNumber = drp["usr_ContactNo"].ToString(),
                            Remarks = drp["pih_Remarks"].ToString(),
                            PickingItems = listItems,
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
                    dm.TraceService("NoDataRes - ");
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService("NoDataSQL - " + ex.Message.ToString());

            }

            dm.TraceService("GetPickingItemBatch ENDED " + DateTime.Now.ToString());
            dm.TraceService("==========================");

            return JSONString;
        }
        public string SelOrderPickingHeader([FromForm] OrderPickingHeaderIn inputParams)
        {
            dm.TraceService("SelOrderPickingHeader STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                List<OrderPickingFilters> filterData = JsonConvert.DeserializeObject<List<OrderPickingFilters>>(inputParams.JsonString);
                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
                //string MainCondition = "";
                string InputXml = "";


                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (OrderPickingFilters id in filterData)
                        {
                            string[] arr = { id.cus_ID.ToString(), id.rot_ID.ToString(), id.ordNo.ToString(), id.time.ToString() };
                            string[] arrName = { "cus_ID", "rot_ID", "ordNo", "time" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw.ToString();
                }





                string[] ar = { userID.ToString() };
                DataTable dtorder = dm.loadList("SelectOrderPicking", "sp_Inventory", InputXml.ToString(), ar);

                if (dtorder.Rows.Count > 0)
                {
                    List<OrderPickingHeaderOut> listItems = new List<OrderPickingHeaderOut>();
                    foreach (DataRow dr in dtorder.Rows)
                    {

                        listItems.Add(new OrderPickingHeaderOut
                        {
                            PickListID = dr["pih_ID"].ToString(),
                            ord_ID = dr["pih_ord_ID"].ToString(),
                            PickLocation = dr["plm_Name"].ToString(),
                            CusHeaderCode = dr["csh_Code"].ToString(),
                            CusHeaderName = dr["csh_Name"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            ExpectedDelDate = dr["ord_ExpectedDelDate"].ToString(),
                            PickListNumber = dr["pih_Number"].ToString(),
                            Picker = dr["usr_Name"].ToString(),
                            pih_Status = dr["pih_Status"].ToString(),
                            Mode = dr["Mode"].ToString(),
                            PickerContactNo = dr["usr_ContactNo"].ToString(),
                            Remarks = dr["usr_ContactNo"].ToString(),
                            TimeRange = dr["TimeRange"].ToString(),
                            PickingInstruction = dr["PickingInstruction"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            rot_ID = dr["rot_ID"].ToString(),
                            rot_Code = dr["rot_Code"].ToString(),
                            rot_Name = dr["rot_Name"].ToString(),
                            cus_ID = dr["cus_ID"].ToString()



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
                dm.TraceService(" SelOrderPickingHeader Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelOrderPickingHeader ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelCustomer([FromForm] selCusIn inputParams)
        {
            dm.TraceService("SelCustomer STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;
                string rotID= inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
                string[] ar = { rotID.ToString() };

                DataTable dtcus = dm.loadList("SelCusForOrderPicking", "sp_Inventory", UserID.ToString(),ar);

                if (dtcus.Rows.Count > 0)
                {
                    List<selCusOut> listItems = new List<selCusOut>();
                    foreach (DataRow dr in dtcus.Rows)
                    {

                        listItems.Add(new selCusOut
                        {
                            cus_ID = dr["cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            cus_HeaderID = dr["csh_ID"].ToString(),
                            cus_HeaderCode = dr["csh_Code"].ToString(),
                            cus_HeaderName = dr["csh_Name"].ToString()


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
                dm.TraceService(" SelCustomer Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelCustomer ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelRot([FromForm] selRotIn inputParams)
        {
            dm.TraceService("SelRot STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;

                DataTable dtrot = dm.loadList("SelRotForOrderPicking", "sp_Inventory", UserID.ToString());

                if (dtrot.Rows.Count > 0)
                {
                    List<selRotOut> listItems = new List<selRotOut>();
                    foreach (DataRow dr in dtrot.Rows)
                    {

                        listItems.Add(new selRotOut
                        {
                            rot_ID = dr["rot_ID"].ToString(),
                            rot_Code = dr["rot_Code"].ToString(),
                            rot_Name = dr["rot_Name"].ToString()



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
                dm.TraceService(" SelRot Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelRot ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string ScheduleSelfPicking([FromForm] SelfPickingIn inputParams)
        {
            dm.TraceService("ScheduleSelfPicking STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                List<SelfpickingIds> PickingData = JsonConvert.DeserializeObject<List<SelfpickingIds>>(inputParams.JsonString);
                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
                //string MainCondition = "";
                string InputXml = "";


                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (SelfpickingIds id in PickingData)
                        {
                            string[] arr = { id.pih_ID.ToString() };
                            string[] arrName = { "pih_ID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw.ToString();
                }





                string[] ar = { userID.ToString() };
                DataTable dtorder = dm.loadList("ScheduleSelfPicking", "sp_Inventory", InputXml.ToString(), ar);

                if (dtorder.Rows.Count > 0)
                {
                    List<SelfPickingOut> listItems = new List<SelfPickingOut>();
                    foreach (DataRow dr in dtorder.Rows)
                    {

                        listItems.Add(new SelfPickingOut
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
                dm.TraceService(" ScheduleSelfPicking Exception - " + ex.Message.ToString());
            }
            dm.TraceService("ScheduleSelfPicking ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectUndoPickListItems([FromForm] UndoIn inputParams)
        {
            dm.TraceService("SelectUndoPickListItems STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string pickingID = inputParams.PickingId == null ? "0" : inputParams.PickingId;

                DataTable dtcus = dm.loadList("SelUndoPickListItems", "sp_Inventory", pickingID.ToString());

                if (dtcus.Rows.Count > 0)
                {
                    List<UndoOut> listItems = new List<UndoOut>();
                    foreach (DataRow dr in dtcus.Rows)
                    {

                        listItems.Add(new UndoOut
                        {
                            prd_ID = dr["pid_prd_ID"].ToString(),
                            LineNo = dr["pid_LineNo"].ToString(),

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
                dm.TraceService(" SelectUndoPickListItems Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectUndoPickListItems ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string GetWeight([FromForm] WeighInfo value)
        {

            //value.IP = "192.168.151.13";
            //value.UserID = "45";
            //value.PortNo = 3002;

            string Weight = "0.25";
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    string ipAddress = value.IP; // IP address of the remote server
                    int port = value.PortNo; // Port number of the remote server

                    // Create a socket and connect to the remote server

                    socket.Connect(ipAddress, port);
                    byte[] buffer = new byte[1024];
                    int bytesRead = socket.Receive(buffer);
                    Weight = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    string[] values = Weight.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    Weight = values[0].Trim();

                    try
                    {
                        Decimal x = Decimal.Parse(Weight);
                        Weight = x.ToString();
                    }
                    catch (Exception ex)
                    {
                        Weight = "0";
                    }
                }
                catch (Exception ex)
                {
                    Weight = "NoData - " + ex.Message.ToString();
                }
                finally
                {
                    socket.Close();
                }
            }
            //Weight = "0.50";
            return Weight;
        }

        public string SelItemwiseSummaryOrders([FromForm] ItemwiseSummaryOrdersIn inputParams)
        {
            dm.TraceService("SelItemwiseSummaryOrders STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PickingIds> PickingData = JsonConvert.DeserializeObject<List<PickingIds>>(inputParams.JsonString);
                string prdID = inputParams.prdID == null ? "0" : inputParams.prdID;
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;

                string InputXml = "";

                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PickingIds id in PickingData)
                        {
                            string[] arr = { id.pih_ID.ToString() };
                            string[] arrName = { "pih_ID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw.ToString();
                }

                string[] ar = { UserID.ToString() , InputXml.ToString() };

                DataTable dtOrders = dm.loadList("SelItemwiseSummaryOrders", "sp_Inventory", prdID.ToString(), ar);

                if (dtOrders.Rows.Count > 0)
                {
                    List<ItemwiseSummaryOrdersOut> listItems = new List<ItemwiseSummaryOrdersOut>();
                    foreach (DataRow dr in dtOrders.Rows)
                    {

                        listItems.Add(new ItemwiseSummaryOrdersOut
                        {
                            pih_ID = dr["pih_ID"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            pih_Number = dr["pih_Number"].ToString(),
                            prd_ID = dr["prd_ID"].ToString(),
                            ord_Huom = dr["ord_Huom"].ToString(),
                            ord_Hqty = dr["ord_Hqty"].ToString(),
                            pid_HigherQty = dr["pid_HigherQty"].ToString(),
                            pid_HigherUOM = dr["pid_HigherUOM"].ToString(),
                            ord_ExpectedDelDate = dr["ord_ExpectedDelDate"].ToString(),
                            prd_WeighingItem = dr["prd_WeighingItem"].ToString(),
                            ord_ID = dr["ord_ID"].ToString(),
                            cus_Name = dr["cus_Name"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            csh_Name = dr["csh_Name"].ToString(),
                            csh_Code = dr["csh_Code"].ToString(),
                            pih_Remarks = dr["pih_Remarks"].ToString(),
                            usr_Name = dr["usr_Name"].ToString(),
                            usr_ContactNo = dr["usr_ContactNo"].ToString(),
                            ModifiedDate = dr["ModifiedDate"].ToString(),
                            pih_Status = dr["pih_Status"].ToString(),
                            pid_LineNo = dr["pid_LineNo"].ToString(),
                            plm_Name = dr["plm_Name"].ToString(),
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
                dm.TraceService(" SelItemwiseSummaryOrders Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelItemwiseSummaryOrders ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelItemwiseSummary([FromForm] ItemwiseSummarysIn inputParams)
        {
            dm.TraceService("SelItemwiseSummary STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                List<PickingId> PickingData = JsonConvert.DeserializeObject<List<PickingId>>(inputParams.JsonString);
               
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;

                string InputXml = "";

                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PickingId id in PickingData)
                        {
                            string[] arr = { id.pih_ID.ToString() };
                            string[] arrName = { "pih_ID" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    InputXml = sw.ToString();
                }

                string[] ar = { UserID.ToString() };

                DataTable dtOrders = dm.loadList("SelItemwiseSummary", "sp_Inventory", InputXml.ToString(), ar);

                if (dtOrders.Rows.Count > 0)
                {
                    List<ItemwiseSummarysOut> listItems = new List<ItemwiseSummarysOut>();
                    foreach (DataRow dr in dtOrders.Rows)
                    {

                        listItems.Add(new ItemwiseSummarysOut
                        { 
                           
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            PickingInstruction = dr["PickingInstruction"].ToString(),
                            BaseUOM = dr["BaseUOM"].ToString(),
                            Qty = dr["Qty"].ToString(),
                          
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
                dm.TraceService(" SelItemwiseSummaryOrders Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelItemwiseSummaryOrders ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectRotInfo([FromForm] selRotIn inputParams)
        {
            dm.TraceService("SelectRotInfo STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;

                DataTable dtrot = dm.loadList("SelRotInfo", "sp_Inventory", UserID.ToString());

                if (dtrot.Rows.Count > 0)
                {
                    List<selRotInfoOut> listItems = new List<selRotInfoOut>();
                    foreach (DataRow dr in dtrot.Rows)
                    {

                        listItems.Add(new selRotInfoOut
                        {
                            rot_ID = dr["rot_ID"].ToString(),
                            rot_Code = dr["rot_Code"].ToString(),
                            rot_Name = dr["rot_Name"].ToString(),
                            User_Name = dr["usr_Name"].ToString(),
                            StartTime = dr["rds_FromTime"].ToString(),
                            EndTime = dr["rds_ToTime"].ToString(),
                            PicklistCount = dr["NoofOrders"].ToString()


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
                dm.TraceService(" SelectRotInfo Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectRotInfo ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string UpdateUserAppVersion([FromForm] VersionIn inputParams)
        {
            dm.TraceService("UpdateUserAppVersion STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;
                string Version = inputParams.Version == null ? "0" : inputParams.Version;


                string[] ar = { Version.ToString() };
                DataTable dt = dm.loadList("UpdateUserAppVersion", "sp_Inventory", UserID.ToString(), ar);

                if (dt.Rows.Count > 0)
                {
                    List<VersionOut> listItems = new List<VersionOut>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        listItems.Add(new VersionOut
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
                dm.TraceService(" UpdateUserAppVersion Exception - " + ex.Message.ToString());
            }
            dm.TraceService("UpdateUserAppVersion ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}