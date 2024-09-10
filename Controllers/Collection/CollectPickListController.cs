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
using MVC_API.Models.Collection;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using System.IO;
using System.Xml;

namespace MVC_API.Controllers.Collection
{
    public class CollectPickListController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;

        [HttpPost]
        public string GetPicklocation([FromForm] GetPicklocIn inputParams)
        {
            dm.TraceService("GetPicklocation STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string RotID = inputParams.rotID == null ? "0" : inputParams.rotID;
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string[] ar = { usrID.ToString() };
                DataTable picklocIn = dm.loadList("SelPicklocationforCollection", "sp_Collection", RotID.ToString(),ar);

                if (picklocIn.Rows.Count > 0)
                {
                    List<GetPicklocOut> listItems = new List<GetPicklocOut>();
                    foreach (DataRow dr in picklocIn.Rows)
                    {

                        listItems.Add(new GetPicklocOut
                        {
                            plm_ID = dr["plm_ID"].ToString(),
                            plm_Code = dr["plm_Code"].ToString(),
                            plm_Name = dr["plm_Name"].ToString(),
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
                dm.TraceService(" GetPicklocation Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetPicklocation ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string GetPickingHeaderByPickLocation([FromForm] GetPicklistIn inputParams)
        {
            dm.TraceService("GetPickingHeaderByPickLocation STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                string userID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string rotID = inputParams.rotID == null ? "0" : inputParams.rotID;
                string plmID = inputParams.plm_ID == null ? "0" : inputParams.plm_ID;

               

                string[] ar = { userID.ToString(),rotID };
                DataTable dtorder = dm.loadList("SelectPicklistsByLocation", "sp_Collection", plmID.ToString(), ar);

                if (dtorder.Rows.Count > 0)
                {
                    List<GetPicklistOut> listItems = new List<GetPicklistOut>();
                    foreach (DataRow dr in dtorder.Rows)
                    {

                        listItems.Add(new GetPicklistOut
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
                           
                            pih_Status = dr["pih_Status"].ToString(),
                            
                            PickerContactNo = dr["usr_ContactNo"].ToString(),
                            Remarks = dr["usr_ContactNo"].ToString(),
                            
                            PickingInstruction = dr["PickingInstruction"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            rot_ID = dr["rot_ID"].ToString(),
                            rot_Code = dr["rot_Code"].ToString(),
                            rot_Name = dr["rot_Name"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),
                            pickCount= dr["pickCount"].ToString(),
                            CollectedStatus= dr["pih_CollectedStatus"].ToString()


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
                dm.TraceService(" GetPickingHeaderByPickLocation Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetPickingHeaderByPickLocation ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string GetPickingHeaderNotCompleted([FromForm] GetPicklistIn inputParams)
        {
            dm.TraceService("GetPickingHeaderNotCompleted STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");

            try
            {
                string userID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string rotID = inputParams.rotID == null ? "0" : inputParams.rotID;
                string plmID = inputParams.plm_ID == null ? "0" : inputParams.plm_ID;



                string[] ar = { userID.ToString(), rotID };
                DataTable dtorder = dm.loadList("SelectNotCompletedPicklists", "sp_Collection", plmID.ToString(), ar);

                if (dtorder.Rows.Count > 0)
                {
                    List<GetPicklistOut> listItems = new List<GetPicklistOut>();
                    foreach (DataRow dr in dtorder.Rows)
                    {

                        listItems.Add(new GetPicklistOut
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

                            pih_Status = dr["pih_Status"].ToString(),

                            PickerContactNo = dr["usr_ContactNo"].ToString(),
                            Remarks = dr["usr_ContactNo"].ToString(),

                            PickingInstruction = dr["PickingInstruction"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            rot_ID = dr["rot_ID"].ToString(),
                            rot_Code = dr["rot_Code"].ToString(),
                            rot_Name = dr["rot_Name"].ToString(),
                            cus_ID = dr["cus_ID"].ToString(),
                            pickCount = dr["pickCount"].ToString(),
                            CollectedStatus = dr["pih_CollectedStatus"].ToString()

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
                dm.TraceService(" GetPickingHeaderNotCompleted Exception - " + ex.Message.ToString());
            }
            dm.TraceService("GetPickingHeaderNotCompleted ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string GetItemBatchForCollection([FromForm] pickingDetailsIn inputParams)
        {
            dm.TraceService("GetItemBatchForCollection STARTED ");
            dm.TraceService("====================");
            try
            {
                string pickingID = inputParams.PickingId == null ? "0" : inputParams.PickingId;
                string userID = inputParams.UserId == null ? "0" : inputParams.UserId;


                string[] arr = { userID.ToString() };
                DataSet dtItemBatch = dm.loadListDS("SelItemBatch", "sp_Collection", pickingID.ToString(), arr);
                DataTable itemData = dtItemBatch.Tables[0];
                DataTable batchData = dtItemBatch.Tables[1];
                if (itemData.Rows.Count > 0)
                {
                    List<GetPickingItemDetails> listItems = new List<GetPickingItemDetails>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<GetPickingBatchData> listBatchSerial = new List<GetPickingBatchData>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["prd_Code"].ToString() == drDetails["prd_Code"].ToString() && dr["pid_ID"].ToString() == drDetails["pbs_pid_ID"].ToString())
                            {
                                listBatchSerial.Add(new GetPickingBatchData
                                {
                                    DispBatSerialId = drDetails["pbs_ID"].ToString(),
                                    DisDetailId = drDetails["pbs_pid_ID"].ToString(),
                                    Number = drDetails["pbs_Number"].ToString(),
                                    ExpiryDate = drDetails["pbs_ExpiryDate"].ToString(),
                                    BaseUOM = drDetails["pbs_BaseUOM"].ToString(),
                                    OrderedQty = drDetails["pbs_OrderedQty"].ToString(),
                                   
                                    AdjustedQty = drDetails["pbs_AdjustedQty"].ToString(),
                                    LoadInQty = drDetails["pbs_PickedQty"].ToString(),
                                    ItemCode = drDetails["prd_Code"].ToString(),
                                    LineNo = drDetails["pid_LineNo"].ToString(),


                                });
                            }
                        }

                        listItems.Add(new GetPickingItemDetails
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
                            SysHUOM = dr["pid_HigherUOM"].ToString(),
                            SysLUOM = dr["pid_LowerUOM"].ToString(),
                            SysHQty = dr["pid_HigherQty"].ToString(),
                            SysLQty = dr["pid_LowerQty"].ToString(),
                            AdjHUOM = dr["pid_HigherUOM"].ToString(),
                            AdjLUOM = dr["pid_HigherUOM"].ToString(),
                            AdjHQty = dr["AdjHQty"].ToString(),
                            AdjLQty = dr["AdjLQty"].ToString(),
                            LiHUOM = dr["pid_HigherUOM"].ToString(),
                            LiLUOM = dr["pid_HigherUOM"].ToString(),
                            LiHQty = dr["pid_CollectedQty"].ToString(),
                            LiLQty = dr["LiLQty"].ToString(),
                            PromoType = dr["pid_TransType"].ToString(),
                            WeighingItem = dr["prd_WeighingItem"].ToString(),
                            LineNo = dr["pid_LineNo"].ToString(),
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

                            Reason = dr["pid_rsn_ID"].ToString(),



                           
                        }); ;
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

            dm.TraceService("GetItemBatchForCollection ENDED ");
            dm.TraceService("====================");

            return JSONString;
        }
        public string PickingCollection([FromForm] pickingCollectIn inputParams)
        {
            dm.TraceService("PickingCollection STARTED " + DateTime.Now.ToString());
            dm.TraceService("============================================");
            try
            {
                List<PostPickingCollectionItemData> itemData = JsonConvert.DeserializeObject<List<PostPickingCollectionItemData>>(inputParams.JSONString);
                List<PostPickingCollectionBatchSerial> batchData = JsonConvert.DeserializeObject<List<PostPickingCollectionBatchSerial>>(inputParams.BatchData);
                try
                {
                    string picklistID = inputParams.PicklistId == null ? "0" : inputParams.PicklistId;
                   
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
                            foreach (PostPickingCollectionItemData id in itemData)
                            {
                                string[] arr = { id.ItemId.ToString(), id.LineNumber ,id.CollectionQty};
                                string[] arrName = { "ItemId", "LineNumber","CollectionQty" };
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
                            foreach (PostPickingCollectionBatchSerial id in batchData)
                            {
                                string[] arr = { id.BatSerialId.ToString(), id.DetailId.ToString(),  id.CollectionQty.ToString()};
                                string[] arrName = { "BatSerialId", "DetailId", "CollectionQty" };
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
                        string[] arr = {  userID.ToString(), InputXml.ToString(), InputBatchSerialXML.ToString() };
                        DataTable dtPickColect = dm.loadList("PickingCollection","sp_Collection", picklistID.ToString(), arr);
                        if (dtPickColect.Rows.Count > 0)
                        {

                            List<GetCollectionStatus> listStatus = new List<GetCollectionStatus>();
                            foreach (DataRow dr in dtPickColect.Rows)
                            {
                                listStatus.Add(new GetCollectionStatus
                                {
                                    Mode = dr["Res"].ToString(),
                                    Status = dr["Status"].ToString()
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
                           
                                JSONString = "NoDataRes";
                                dm.TraceService("NoDataRes");
                          
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
            dm.TraceService("PickingCollection ENDED " + DateTime.Now.ToString());
            dm.TraceService("========================================");
            return JSONString;
        }


    }
}