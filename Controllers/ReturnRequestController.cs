using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
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
    public class ReturnRequestController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        [HttpPost]

        public string GetReturnRequest([FromForm] ReturnRequestIn inputParams)
        {
            dm.TraceService("GetReturnRequest STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            string rotID = inputParams.rot_ID == null ? "0" : inputParams.rot_ID;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
            string[] arr = { userID.ToString() };
            DataSet dtreturn = dm.loadListDS("SelReturnRequestData", "sp_ReturnRequest", rotID.ToString(), arr);
            DataTable HeaderData = dtreturn.Tables[0];
            DataTable DetailData = dtreturn.Tables[1];
            DataTable batchData = dtreturn.Tables[2];


            try
            {
                if (HeaderData.Rows.Count > 0)
                {
                    List<GetRtnRequestHeader> listHeader = new List<GetRtnRequestHeader>();
                    foreach (DataRow dr in HeaderData.Rows)
                    {
                        List<GetRtnRequestDetail> listDetail = new List<GetRtnRequestDetail>();
                        foreach (DataRow drDetails in DetailData.Rows)
                        {
                            List<GetReturnBatchSerial> listBatchSerial = new List<GetReturnBatchSerial>();
                            foreach (DataRow drbatch in batchData.Rows)
                            {
                                string s = drbatch["prd_ID"].ToString();
                                string M= drDetails["rrd_prd_ID"].ToString();
                                if (dr["rrh_inv_ID"].ToString()==drbatch["ind_inv_ID"].ToString())
                                {
                                    if ( (drbatch["prd_ID"].ToString() == drDetails["rrd_prd_ID"].ToString()))
                                    {
                                        listBatchSerial.Add(new GetReturnBatchSerial
                                    {                                         
                                        ind_inv_ID = drbatch["inv_InvoiceID"].ToString(),                                       
                                        Number = drbatch["dns_Number"].ToString(),
                                        ExpiryDate = drbatch["ExpiryDate"].ToString(),
                                        BaseUOM = drbatch["BaseUOM"].ToString(),
                                        EligibleQty = drbatch["EligibleQty"].ToString(),
                                        prd_ID = drbatch["prd_ID"].ToString(),
                                        Price = "0",
                                        ID = drbatch["ind_inv_ID"].ToString(),
                                        BatchSerialId = drbatch["dns_ID"].ToString(),
                                            
                                        });
                                    }
                                }
                            }

                            if (drDetails["rrd_rrh_ID"].ToString() == dr["rrh_ID"].ToString())
                            {


                                listDetail.Add(new GetRtnRequestDetail
                                {

                                    prd_ID = drDetails["rrd_prd_ID"].ToString(),
                                    HUOM = drDetails["rrd_HUOM"].ToString(),
                                    HQty = drDetails["rrd_HQty"].ToString(),
                                    LUOM = drDetails["rrd_LUOM"].ToString(),
                                    LQty = drDetails["rrd_LQty"].ToString(),
                                    Weighing = drDetails["prd_WeighingItem"].ToString(),
                                    spec = drDetails["prd_Spec"].ToString(),
                                    batchserial = listBatchSerial,
                                    prd_Name = drDetails["prd_Name"].ToString(),
                                    prd_Desc = drDetails["prd_Desc"].ToString(),
                                    prd_LongDesc = drDetails["prd_LongDesc"].ToString(),
                                    prd_cat_id = drDetails["prd_cat_id"].ToString(),
                                    prd_sub_ID = drDetails["prd_sub_ID"].ToString(),
                                    prd_brd_ID = drDetails["prd_brd_ID"].ToString(),
                                    prd_EnableOrderHold = drDetails["EnableOrderHold"].ToString(),
                                    prd_EnableReturnHold = drDetails["EnableReturnHold"].ToString(),
                                    prd_EnableDeliveryHold = drDetails["EnableDeliveryHold"].ToString(),
                                    prd_NameArabic = drDetails["prd_NameArabic"].ToString(),
                                    prd_DescArabic = drDetails["prd_DescArabic"].ToString(),
                                    prd_Image = drDetails["prd_Image"].ToString(),                                                           
                                    prd_BaseUOM = drDetails["prd_BaseUOM"].ToString(),
                                    VATPercent = drDetails["odd_VATPercent"].ToString(),
                                    LineNo = drDetails["rrd_LineNo"].ToString(),
                                    rrh_inv_ID = drDetails["inv_InvoiceID"].ToString(),
                                    ID = drDetails["rrh_inv_ID"].ToString(),
                                    prd_code = drDetails["prd_Code"].ToString(),
                                }); 
                            }
                        }

                        listHeader.Add(new GetRtnRequestHeader
                        {
                           
                            inv_ID = dr["inv_InvoiceID"].ToString(),
                            RequestNumber = dr["rrh_RequestNumber"].ToString(),
                            date = dr["CreatedDate"].ToString(),
                           
                            RequestDetail = listDetail,

                            cus_ID = dr["rrh_cus_ID"].ToString(),
                            Request_ID = dr["rrh_ID"].ToString(),
                            ID = dr["rrh_inv_ID"].ToString(),
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
                    dm.TraceService("NoDataRes");
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                dm.TraceService(ex.Message.ToString());
                JSONString = "GetReturnRequest - " + ex.Message.ToString();
            }
            dm.TraceService("GetReturnRequest ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            return JSONString;
        }
        public string InsReturnRequest([FromForm] ReturnIn inputParams)
        {
            dm.TraceService("InsReturnRequest STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            try
            {
                List<ItemIDs> itemData = JsonConvert.DeserializeObject<List<ItemIDs>>(inputParams.ItemID);
                List<BatchSerial> batchData = JsonConvert.DeserializeObject<List<BatchSerial>>(inputParams.BatchData);

                dm.TraceService("itemData  - " + inputParams.ItemID);
                dm.TraceService("batchData  - " + inputParams.BatchData);

                string cseID = inputParams.cseID == null ? "0" : inputParams.cseID;
                string udpID = inputParams.udpID == null ? "0" : inputParams.udpID;
                string cusID = inputParams.cusID == null ? "0" : inputParams.cusID;
                string type = inputParams.type == null ? "0" : inputParams.type;
                //string number = inputParams.number == null ? "0" : inputParams.number;

                string date = inputParams.date == null ? "0" : inputParams.date;
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string Request_ID = inputParams.Request_ID == null ? "0" : inputParams.Request_ID;

                string DetailXml = "";
                
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (ItemIDs id in itemData)
                        {
                            string[] arr = { id.reason.ToString(),id.invoiceID.ToString(),id.prdID.ToString(), id.HigherUOM.ToString(), id.HigherQty.ToString(), id.LowerUOM.ToString(), id.LowerQty.ToString() };
                            string[] arrName = { "reason", "invoiceID", "prdID","HigherUOM","HigherQty", "LowerUOM", "LowerQty" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    DetailXml = sw.ToString();
                }
                string BatchXml = "";
                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("q");
                        int c = 0;
                        foreach (BatchSerial id in batchData)
                        {
                            string[] arr = { id.prdID.ToString(), id.ExpiryDate.ToString(), id.UOM.ToString(), id.ReturnQty.ToString(),id.Mode.ToString(),id.BatSerialNo };
                            string[] arrName = { "prdID", "ExpiryDate", "UOM", "ReturnQty","Mode","BatchSerialNo" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    BatchXml = sw.ToString();
                }

                dm.TraceService("InsReturnRequest Reuest_ID - " + inputParams.Request_ID.ToString());

                string[] ar = { cusID.ToString(), udpID.ToString(), usrID.ToString(), date.ToString(), type.ToString(), DetailXml.ToString() ,BatchXml.ToString(), Request_ID.ToString()};
                DataTable dtReturn = dm.loadList("InsReturnData", "sp_ReturnRequest", cseID.ToString(), ar);

                dm.TraceService("dtReturn - " + dtReturn);

                if (dtReturn.Rows.Count > 0)
                {
                    List<ReturnOut> listReturnout = new List<ReturnOut>();
                    foreach (DataRow dr in dtReturn.Rows)
                    {
                        listReturnout.Add(new ReturnOut
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),
                            Descr = dr["Descr"].ToString()


                        });
                    }
                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listReturnout
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
                dm.TraceService(" InsReturnRequest Exception - " + ex.Message.ToString());
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsReturnRequest ENDED - " + DateTime.Now.ToString());
            dm.TraceService("==================");
            return JSONString;
        }

        public string PostReturnRequestApproval([FromForm] PostReturnData inputParams)
        {
            dm.TraceService("PostReturnRequestApproval STARTED " + DateTime.Now.ToString());
            dm.TraceService("============================================");
            try
            {
                List<PostReturnItemData> itemData = JsonConvert.DeserializeObject<List<PostReturnItemData>>(inputParams.JSONString);
                try
                {
                    string ReturnID = inputParams.ReturnID == null ? "0" : inputParams.ReturnID;
                    string status = inputParams.Status == null ? "PA" : inputParams.Status;
                    string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
                    string udpID = inputParams.udpID == null ? "0" : inputParams.udpID;
                    string rotID = inputParams.rotID == null ? "0" : inputParams.rotID;

                    string InputXml = "";
                    using (var sw = new StringWriter())
                    {
                        using (var writer = XmlWriter.Create(sw))
                        {

                            writer.WriteStartDocument(true);
                            writer.WriteStartElement("r");
                            int c = 0;
                            foreach (PostReturnItemData id in itemData)
                            {
                                string[] arr = { id.ItemId.ToString(), id.HigherUOM.ToString(), id.HigherQty.ToString(), id.LowerUOM.ToString(), id.LowerQty.ToString(), id.ReasonId.ToString() };
                                string[] arrName = { "ItemId", "HigherUOM", "HigherQty", "LowerUOM", "LowerQty", "ReasonId" };
                                dm.createNode(arr, arrName, writer);
                            }

                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                            writer.Close();
                        }
                        InputXml = sw.ToString();
                    }

                    try
                    {
                        string[] arr = { userID.ToString(),  status.ToString(), InputXml.ToString() , udpID.ToString(), rotID.ToString() };
                        string Value = dm.SaveData("sp_ReturnRequest", "InsReturnForApproval", ReturnID.ToString(), arr);
                        int Output = Int32.Parse(Value);
                        List<GetReturnInsertStatus> listStatus = new List<GetReturnInsertStatus>();
                        if (Output > 0)
                        {

                            listStatus.Add(new GetReturnInsertStatus
                            {
                                Mode = "1",
                                Status = "Return Request for approval submitted successfully"
                            });
                            string JSONString = JsonConvert.SerializeObject(new
                            {
                                result = listStatus
                            });
                            return JSONString;
                        }
                        else
                        {
                            listStatus.Add(new GetReturnInsertStatus
                            {
                                Mode = "0",
                                Status = "Return Request for approval submission failed"
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
            dm.TraceService("PostReturnRequestApproval ENDED " + DateTime.Now.ToString());
            dm.TraceService("========================================");
            return JSONString;
        }
        public string GetRetrunApprovalStatus([FromForm] PostReturnApprovalStatusData inputParams)
        {
            dm.TraceService("GetRetrunApprovalStatus STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string ReturnID = inputParams.ReturnID == null ? "0" : inputParams.ReturnID;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;


            string[] arr = { userID.ToString() };
            DataTable dtReturnStatus = dm.loadList("SelStatusForReturnApproval", "sp_ReturnRequest", ReturnID.ToString(), arr);

            try
            {
                if (dtReturnStatus.Rows.Count > 0)
                {
                    List<GetReturnApprovalStatus> listHeader = new List<GetReturnApprovalStatus>();
                    foreach (DataRow dr in dtReturnStatus.Rows)
                    {
                        listHeader.Add(new GetReturnApprovalStatus
                        {
                            ApprovalStatus = dr["rad_ApprovalStatus"].ToString(),
                            ApprovalReason = dr["rad_ApprovalReason_ID"].ToString(),
                            Products = dr["rad_prd_ID"].ToString()

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
                    dm.TraceService("NoDataRes");
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                dm.TraceService(ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }

            dm.TraceService("GetRetrunApprovalStatus ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }

        public string GetReturnApprovalHeaderStatus([FromForm] PostReturnApprovalHeaderStatusData inputParams)
        {
            dm.TraceService("GetReturnApprovalHeaderStatus STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string ReturnID = inputParams.ReturnID == null ? "0" : inputParams.ReturnID;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

            string[] arr = { userID.ToString() };
            DataTable dtDeliveryStatus = dm.loadList("SelStatusForReturnApprovalHeader", "sp_ReturnRequest", ReturnID.ToString(), arr);

            try
            {
                if (dtDeliveryStatus.Rows.Count > 0)
                {
                    List<GetReturnApprovalHeaderStatus> listHeader = new List<GetReturnApprovalHeaderStatus>();
                    foreach (DataRow dr in dtDeliveryStatus.Rows)
                    {
                        listHeader.Add(new GetReturnApprovalHeaderStatus
                        {
                            ApprovalStatus = dr["rah_ApprovalStatus"].ToString()

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
                    dm.TraceService("NoDataRes");
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                dm.TraceService("GetReturnApprovalHeaderStatus  " + ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }

            dm.TraceService("GetReturnApprovalHeaderStatus ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }
    }
}