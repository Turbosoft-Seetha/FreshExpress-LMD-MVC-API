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
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Xml;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace MVC_API.Controllers
{
    public class DeliveryController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        string JSONStr= string.Empty;
        [HttpPost]
        public string PostPartialDelivery([FromForm] PostDeliveryData inputParams)
        {
            dm.TraceService("PostPartialDelivery STARTED " + DateTime.Now.ToString());
            dm.TraceService("============================================");
            try
            {
                List<PostDeliveryItemData> itemData = JsonConvert.DeserializeObject<List<PostDeliveryItemData>>(inputParams.JSONString);
                try
                {
                    string dispatchID = inputParams.DispatchId == null ? "0" : inputParams.DispatchId;
                    string status = inputParams.Status == null ? "PA" : inputParams.Status;
                    string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
                    string finalAmount = inputParams.FinalAmount == null ? "0" : inputParams.FinalAmount;
                    string InputXml = "";
                    using (var sw = new StringWriter())
                    {
                        using (var writer = XmlWriter.Create(sw))
                        {

                            writer.WriteStartDocument(true);
                            writer.WriteStartElement("r");
                            int c = 0;
                            foreach (PostDeliveryItemData id in itemData)
                            {
                                string[] arr = { id.ItemId.ToString(), id.HigherUOM.ToString(), id.HigherQty.ToString(), id.LowerUOM.ToString(), id.LowerQty.ToString(), id.ReasonId.ToString(),id.LineNo };
                                string[] arrName = { "ItemId", "HigherUOM", "HigherQty", "LowerUOM", "LowerQty", "ReasonId","LineNo" };
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
                        string[] arr = { userID.ToString(), finalAmount.ToString(),status.ToString(), InputXml.ToString() };
                        string Value = dm.SaveData("sp_PartialDeliveryForApproval", "InsPartialDeliveryForApproval", dispatchID.ToString(), arr);
                        int Output = Int32.Parse(Value);
                        List<GetDeliveryInsertStatus> listStatus = new List<GetDeliveryInsertStatus>();
                        if (Output > 0)
                        {
                            string url = ConfigurationManager.AppSettings.Get("DelIntURL");
                            dm.TraceService(" Partial Delivery Initial  starts- " + DateTime.Now.ToString());
                            string Json = "";
                            WebServiceCal(url, Json);
                            dm.TraceService("Partial Delivery Initial End - " + DateTime.Now.ToString());

                            listStatus.Add(new GetDeliveryInsertStatus
                            {
                                Mode = "1",
                                Status = "Delivery for approval submitted successfully"
                            });
                            string JSONString = JsonConvert.SerializeObject(new
                            {
                                result = listStatus
                            });
                            return JSONString;

                            
                        }
                        else
                        {
                            listStatus.Add(new GetDeliveryInsertStatus
                            {
                                Mode = "0",
                                Status = "Delivery for approval submission failed"
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
            dm.TraceService("PostPartialDelivery ENDED " + DateTime.Now.ToString());
            dm.TraceService("========================================");
            return JSONString;
        }
        public string WebServiceCal(string URL, string jsonData)
        {

            try
            {

                
                    // Create a request using a URL that can receive a post.
                    WebRequest request = WebRequest.Create(URL);
                    // Set the Method property of the request to POST.
                    request.Method = "POST";
                    request.ContentType = "application/json";

                    byte[] postData = Encoding.UTF8.GetBytes(jsonData);

                    // Set the ContentLength property of the request to the length of the data
                    request.ContentLength = postData.Length;

                    // Get the request stream and write the data to it
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(postData, 0, postData.Length);
                    }

                    WebResponse response = request.GetResponse();
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                    // Get the stream containing content returned by the server.
                    // The using block ensures the stream is automatically closed.
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);
                        // Read the content.
                        string responseFromServer = reader.ReadToEnd();
                        // Display the content.
                        dm.TraceService("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] @ " + " DataLake_Service WebServiceCall Success => " + responseFromServer);
                        response.Close();
                        return responseFromServer;
                    }
               


            }
            catch (Exception ex)
            {
                dm.TraceService("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] @ " + " DataLake_Service WebServiceCall Exception" + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }

        public string GetDeliveryApproval([FromForm] PostDeliveryApprovalData inputParams)
        {
            dm.TraceService("GetDeliveryApproval STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

            string[] arr = { "" };
            DataSet dtItemBatch = dm.loadListDS("SelectDeliveryApproval", "sp_PartialDeliveryForApproval", userID.ToString(), arr);
            DataTable headerData = dtItemBatch.Tables[0];
            DataTable detailData = dtItemBatch.Tables[1];
            //Items In Picklist 

            //Batch/Serial Number in Picklist against each item

            try
            {
                if (headerData.Rows.Count > 0)
                {
                    List<GetDeliveryApprovalHeader> listHeader = new List<GetDeliveryApprovalHeader>();
                    foreach (DataRow dr in headerData.Rows)
                    {
                        List<GetDeliveryApprovalDetail> listDetail = new List<GetDeliveryApprovalDetail>();
                        foreach (DataRow drDetails in detailData.Rows)
                        {
                            if (dr["dah_ID"].ToString() == drDetails["dad_dah_ID"].ToString())
                            {
                                listDetail.Add(new GetDeliveryApprovalDetail
                                {
                                    ItemId = drDetails["prd_ID"].ToString(),
                                    ItemName = drDetails["prd_Name"].ToString(),
                                    ItemCode = drDetails["prd_Code"].ToString(),
                                    ItemSpec = drDetails["prd_Spec"].ToString(),
                                    ItemPrice = drDetails["pru_Price"].ToString(),
                                    HigherUOM = drDetails["dad_HigherUOM"].ToString(),
                                    HigherQty = drDetails["dad_HigherQty"].ToString(),
                                    LowerUOM = drDetails["dad_LowerUOM"].ToString(),
                                    LowerQty = drDetails["dad_LowerQty"].ToString(), 
                                    UserReasonId = drDetails["dad_UserReason_ID"].ToString(), 
                                    ApproverReasonId = drDetails["dad_ApprovalReason_ID"].ToString(),
                                    LineNo = drDetails["dad_LineNo"].ToString(),

                                });
                            }
                        }

                        listHeader.Add(new GetDeliveryApprovalHeader
                        {
                            Id = dr["dah_dsp_ID"].ToString(),
                            Number = dr["dsp_DispatchID"].ToString(),
                            DepartmentId = dr["ord_dep_ID"].ToString(),
                            LPONumber = dr["ord_LPONumber"].ToString(),
                            Status = dr["Status"].ToString(),
                            ApprovalStatus = dr["dah_ApprovalStatus"].ToString(),
                            Date = dr["Date"].ToString(),
                            Time = dr["Time"].ToString(),
                            FinalAmount = dr["dah_FinalAmount"].ToString(),
                            ApprovalDetail = listDetail
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

            dm.TraceService("GetDeliveryApproval ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }

        public string GetDeliveryApprovalStatus([FromForm] PostApprovalStatusData inputParams)
        {
            dm.TraceService("GetDeliveryApprovalStatus STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string dispatchID = inputParams.DispatchId == null ? "0" : inputParams.DispatchId;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;
           

            string[] arr = { userID.ToString()};
            DataTable dtDeliveryStatus = dm.loadList("SelStatusForDeliveryApproval", "sp_PartialDeliveryForApproval", dispatchID.ToString(), arr);
            
            try
            {
                if (dtDeliveryStatus.Rows.Count > 0)
                {
                    List<GetDeliveryApprovalStatus> listHeader = new List<GetDeliveryApprovalStatus>();
                    foreach (DataRow dr in dtDeliveryStatus.Rows)
                    {
                        listHeader.Add(new GetDeliveryApprovalStatus
                        {
                            ApprovalStatus = dr["dad_ApprovalStatus"].ToString(),
                            ApprovalReason = dr["dad_ApprovalReason_ID"].ToString(),
                            Products = dr["dad_prd_ID"].ToString(),

                            IsLossInTransit = dr["rsn_IsLossInTransit"].ToString(),
                            ReasonCode = dr["rsn_Code"].ToString(),
                            LineNo= dr["dad_LineNo"].ToString()

                        }) ;
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

            dm.TraceService("GetDeliveryApprovalStatus ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }

        public string InsDeliveryVerification([FromForm] DeliveryVerificationIn inputParams)
        {
            dm.TraceService("InsDeliveryVerification STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string dispatchID = inputParams.DispatchId == null ? "0" : inputParams.DispatchId;
            string PickerName = inputParams.PickerName == null ? "0" : inputParams.PickerName;
            string PickerMobileNumber = inputParams.PickerMobileNumber == null ? "0" : inputParams.PickerMobileNumber;
            string CreatedBy = inputParams.UserId == null ? "0" : inputParams.UserId;
            string CreatedDate = inputParams.CreatedDate == null ? "0" : inputParams.CreatedDate;
            string InvPrintName = inputParams.InvPrintName == null ? "0" : inputParams.InvPrintName;

            string[] arr = { PickerName.ToString(), PickerMobileNumber.ToString(), CreatedBy.ToString(), CreatedDate.ToString() , InvPrintName.ToString() };
            DataTable dtDelivery = dm.loadList("InsDeliveryVerification", "sp_PartialDeliveryForApproval", dispatchID.ToString(), arr);

           

            try
            {
                if (dtDelivery.Rows.Count > 0)
                {
                    List<DeliveryVerificationOut> listHeader = new List<DeliveryVerificationOut>();
                    foreach (DataRow dr in dtDelivery.Rows)
                    {
                        listHeader.Add(new DeliveryVerificationOut
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),
                            Desr = dr["Desr"].ToString(),
                           
                        });
                    }

                    if (dtDelivery.Rows[0]["Res"].ToString() == "1")
                    {


                        DeliveryOTP listOTP = new DeliveryOTP();
                        foreach (DataRow dr in dtDelivery.Rows)
                        {
                            listOTP = new DeliveryOTP
                            {
                                MobNumber = dr["dvr_PickerMobileNumber"].ToString(),
                                Message = dr["message"].ToString(),

                            };
                        }


                        JSONStr = JsonConvert.SerializeObject(listOTP);


                      
                        string url = ConfigurationManager.AppSettings.Get("SMS_URL");
                        dm.TraceService(" Delivery Verification OTP SMS  starts- " + DateTime.Now.ToString());
                        string res=WebServiceCall(url, JSONStr);
                        dm.TraceService(" Delivery Verification OTP SMS End - " + DateTime.Now.ToString());


                    }
                    JSONString = JsonConvert.SerializeObject(new
                    {
                        result = listHeader
                    });

                   // return JSONString;
                }
                else
                {
                    dm.TraceService("NoDataRes");
                    JSONString = "NoDataRes";
                }
            }
            catch (Exception ex)
            {
                dm.TraceService(" InsDeliveryVerification Exception - " + ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }

            dm.TraceService("InsDeliveryVerification ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }
        public string WebServiceCall(string URL, string jsonData)
        {

            try
            {

                if (jsonData != null)
                {
                    // Create a request using a URL that can receive a post.
                    WebRequest request = WebRequest.Create(URL);
                    // Set the Method property of the request to POST.
                    request.Method = "POST";
                    request.ContentType = "application/json";

                    byte[] postData = Encoding.UTF8.GetBytes(jsonData);

                    // Set the ContentLength property of the request to the length of the data
                    request.ContentLength = postData.Length;

                    // Get the request stream and write the data to it
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(postData, 0, postData.Length);
                    }

                    WebResponse response = request.GetResponse();
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                    // Get the stream containing content returned by the server.
                    // The using block ensures the stream is automatically closed.
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);
                        // Read the content.
                        string responseFromServer = reader.ReadToEnd();
                        // Display the content.
                        dm.TraceService("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] @ " + " DataLake_Service WebServiceCall Success => " + responseFromServer);
                        response.Close();
                        return responseFromServer;
                    }
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                dm.TraceService("[" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] @ " + " DataLake_Service WebServiceCall Exception" + ex.Message.ToString());
                return ex.Message.ToString();
            }
        }
        public string DeliveryVerification([FromForm] DeliveryVerifyOTPIn inputParams)
        {

            dm.TraceService("DeliveryVerification STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string dispatchID = inputParams.DispatchId == null ? "0" : inputParams.DispatchId;
            string PickerMobileNumber = inputParams.PickerMobileNumber == null ? "0" : inputParams.PickerMobileNumber;
            string OTP = inputParams.OTP == null ? "0" : inputParams.OTP;
           
          
            string[] arr = { dispatchID.ToString(), PickerMobileNumber.ToString() };
            DataTable dtDelivery = dm.loadList("DeliveryVerification", "sp_PartialDeliveryForApproval", OTP.ToString(), arr);

            try
            {
                if (dtDelivery.Rows.Count > 0)
                {
                    List<DeliveryVerifyOTPOut> listHeader = new List<DeliveryVerifyOTPOut>();
                    foreach (DataRow dr in dtDelivery.Rows)
                    {
                        listHeader.Add(new DeliveryVerifyOTPOut
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),
                            Desr = dr["Desr"].ToString(),

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
                dm.TraceService(" DeliveryVerification Exception - " + ex.Message.ToString());
                JSONString = "NoDataSQL - " + ex.Message.ToString();
            }

            dm.TraceService("DeliveryVerification ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }
        public string GetDeliveryApprovalHeaderStatus([FromForm] PostApprovalHeaderStatusData inputParams)
        {
            dm.TraceService("GetDeliveryApprovalHeaderStatus STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string dispatchID = inputParams.DispatchId == null ? "0" : inputParams.DispatchId;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

            string[] arr = { userID.ToString() };
            DataTable dtDeliveryStatus = dm.loadList("SelStatusForDeliveryApprovalHeader", "sp_PartialDeliveryForApproval", dispatchID.ToString(), arr);

            try
            {
                if (dtDeliveryStatus.Rows.Count > 0)
                {
                    List<GetDeliveryApprovalHeaderStatus> listHeader = new List<GetDeliveryApprovalHeaderStatus>();
                    foreach (DataRow dr in dtDeliveryStatus.Rows)
                    {
                        listHeader.Add(new GetDeliveryApprovalHeaderStatus
                        {
                            ApprovalStatus = dr["dah_ApprovalStatus"].ToString()
                           
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

            dm.TraceService("GetDeliveryApprovalHeaderStatus ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }

        public string GetDeliveryPaymentStatus([FromForm] DeliveryPaymentStatusIn inputParams)
        {
            dm.TraceService("GetDeliveryPaymentStatus STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            string ordNumber = inputParams.ordNumber == null ? "0" : inputParams.ordNumber;
          

          
            DataTable dtDeliveryStatus = dm.loadList("SelPaymentStatusForDelivery", "sp_PartialDeliveryForApproval", ordNumber.ToString());

            try
            {
                if (dtDeliveryStatus.Rows.Count > 0)
                {
                    List<DeliveryPaymentStatusOut> listHeader = new List<DeliveryPaymentStatusOut>();
                    foreach (DataRow dr in dtDeliveryStatus.Rows)
                    {
                        listHeader.Add(new DeliveryPaymentStatusOut
                        {
                            PaymentStatus = dr["ord_CashPaidStatus"].ToString()

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

            dm.TraceService("GetDeliveryPaymentStatus ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }
        public string DeliveryItemCount([FromForm] DeliveryItemCountIn inputParams)
        {
            dm.TraceService("DeliveryItemCount STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
           
            string dln_ID = inputParams.dln_ID == null ? "0" : inputParams.dln_ID;

            DataTable dt = dm.loadList("SelDeliveryItemCount", "sp_PartialDeliveryForApproval", dln_ID.ToString());

            try
            {
                if (dt.Rows.Count > 0)
                {
                    List<DeliveryItemCountOut> listHeader = new List<DeliveryItemCountOut>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        listHeader.Add(new DeliveryItemCountOut
                        {
                            DeliveryItemCount = dr["DeliveryItemCount"].ToString(),
                            IsPickupOrder = dr["IsPickupOrder"].ToString()

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

            dm.TraceService("DeliveryItemCount ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }

        public string DeliveryItems([FromForm] DeliveryItemsIn inputParams)
        {
            dm.TraceService("DeliveryItems STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");
            
            string dln_ID = inputParams.dln_ID == null ? "0" : inputParams.dln_ID;
            string userID = inputParams.UserId == null ? "0" : inputParams.UserId;

            string[] arr = { userID.ToString() };
            DataSet dtItemBatch = dm.loadListDS("SelDeliveryItems", "sp_PartialDeliveryForApproval", dln_ID.ToString(), arr);
            DataTable itemData = dtItemBatch.Tables[0];
            DataTable batchData = dtItemBatch.Tables[1];
            //Items In Picklist 

            //Batch/Serial Number in Picklist against each item

            try
            {
                if (itemData.Rows.Count > 0)
                {
                    List<DeliveryItemsOut> listItems = new List<DeliveryItemsOut>();
                    foreach (DataRow dr in itemData.Rows)
                    {
                        List<DeliveryItemBatchSerial> listBatchSerial = new List<DeliveryItemBatchSerial>();
                        foreach (DataRow drDetails in batchData.Rows)
                        {
                            if (dr["prd_Code"].ToString() == drDetails["prd_Code"].ToString() && dr["dld_ID"].ToString() == drDetails["dns_dld_ID"].ToString())
                            {
                                listBatchSerial.Add(new DeliveryItemBatchSerial
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
                                    LineNo = drDetails["dln_LineNo"].ToString(),
                                });
                            }
                        }

                        listItems.Add(new DeliveryItemsOut
                        {
                            prd_ID = Int32.Parse(dr["prd_ID"].ToString()),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Spec = dr["prd_Spec"].ToString(),
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
                            Discount = dr["odd_Discount"].ToString(),
                            DiscountPercentage = dr["odd_DiscountPercentage"].ToString(),
                            Reason = dr["dld_Appr_ReasonCode"].ToString()
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

            dm.TraceService("DeliveryItems ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }

        public string DeliverySalesPerson([FromForm] DeliverySPIn inputParams)
        {
            dm.TraceService("DeliverySalesPerson STARTED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            string dln_ID = inputParams.dln_ID == null ? "0" : inputParams.dln_ID;

            DataTable dt = dm.loadList("SelDeliverySalesPerson", "sp_PartialDeliveryForApproval", dln_ID.ToString());

            try
            {
                if (dt.Rows.Count > 0)
                {
                    List<DeliverySPOut> listHeader = new List<DeliverySPOut>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        listHeader.Add(new DeliverySPOut
                        {
                            SalesPersonCode = dr["spm_Code"].ToString(),
                            SalesPersonName = dr["spm_Name"].ToString(),
                            SalesPersonPhone = dr["spm_Phone"].ToString(),
                            OrderReleasedBy = dr["ord_ReleasedBy"].ToString(),
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

            dm.TraceService("DeliverySalesPerson ENDED " + DateTime.Now.ToString());
            dm.TraceService("======================================");

            return JSONString;
        }

    }
}