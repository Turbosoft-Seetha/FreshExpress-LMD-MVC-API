using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using MVC_API.Models.CustomerConnectHelpers;
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

namespace MVC_API.Controllers.CustomerConnect
{
    public class NotificationController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        [HttpPost]

        public string SelectUserNotifications([FromForm] NotificationIn inputParams)
        {
            dm.TraceService("SelectUserNotifications STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;

                DataTable dtNotification = dm.loadList("SelUserNotifications", "sp_CustomerConnect", UserID.ToString());

                if (dtNotification.Rows.Count > 0)
                {
                    List<NotificationOut> listItems = new List<NotificationOut>();
                    foreach (DataRow dr in dtNotification.Rows)
                    {

                        listItems.Add(new NotificationOut
                        {
                            rnt_ID = dr["rnt_ID"].ToString(),
                            rnt_Header = dr["rnt_Header"].ToString(),
                            rnt_Desc = dr["rnt_Desc"].ToString(),
                            rnt_ReadFlag = dr["rnt_ReadFlag"].ToString(),
                            rnt_ReplyMessage = dr["rnt_ReplyMessage"].ToString(),
                            rnt_ReplyUserID = dr["rnt_ReplyUserID"].ToString(),
                            rnt_ReplyTime = dr["rnt_ReplyTime"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            Status = dr["Status"].ToString(),
                            rnt_ArabicHeader = dr["rnt_ArabicHeader"].ToString(),
                            rnt_ArabicDesc = dr["rnt_ArabicDesc"].ToString(),
                            rnt_usr_ID = dr["rnt_usr_ID"].ToString(),
                            rnt_IsDetail = dr["rnt_IsDetail"].ToString(),
                            TotalItemCount = dr["TotalItemCount"].ToString(),
                            PartialPickedCount = dr["PartialPickedCount"].ToString(),
                            NotPickedCount = dr["NotPickedCount"].ToString(),
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
                dm.TraceService(" SelectUserNotifications Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectUserNotifications ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string InsNotificationReply([FromForm] InsNotificationIn inputParams)
        {
            dm.TraceService("InsNotificationReply STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rnt_ReplyMessage = inputParams.rnt_ReplyMessage == null ? "0" : inputParams.rnt_ReplyMessage; 
                string rnt_ReplyUserID = inputParams.rnt_ReplyUserID == null ? "0" : inputParams.rnt_ReplyUserID;
                string rnt_ID = inputParams.rnt_ID == null ? "0" : inputParams.rnt_ID;
               
                string []arr = { rnt_ReplyUserID.ToString(), rnt_ID.ToString() };
                DataTable dtNotification = dm.loadList("InsNotificationReply", "sp_CustomerConnect", rnt_ReplyMessage.ToString(),arr);

                if (dtNotification.Rows.Count > 0)
                {
                    List<InsNotificationOut> listItems = new List<InsNotificationOut>();
                    foreach (DataRow dr in dtNotification.Rows)
                    {

                        listItems.Add(new InsNotificationOut
                        {
                            Title = dr["Title"].ToString(),
                            Res = dr["Res"].ToString(),
                            Descr = dr["Descr"].ToString(),
                          
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
                dm.TraceService(" InsNotificationReply Exception - " + ex.Message.ToString());
            }
            dm.TraceService("InsNotificationReply ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        
        public string UpdateNotificationReadFlag([FromForm] UpdateNotificationIn inputParams)
        {
            dm.TraceService("UpdateNotificationReadFlag STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                
                string rnt_ID = inputParams.rnt_ID == null ? "0" : inputParams.rnt_ID;

                DataTable dtNotification = dm.loadList("UpdateNotificationReadFlag", "sp_CustomerConnect", rnt_ID.ToString());

                if (dtNotification.Rows.Count > 0)
                {
                    List<UpdateNotificationOut> listItems = new List<UpdateNotificationOut>();
                    foreach (DataRow dr in dtNotification.Rows)
                    {

                        listItems.Add(new UpdateNotificationOut
                        {
                            Title = dr["Title"].ToString(),
                            Res = dr["Res"].ToString(),
                            Descr = dr["Descr"].ToString(),

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
                dm.TraceService(" UpdateNotificationReadFlag Exception - " + ex.Message.ToString());
            }
            dm.TraceService("UpdateNotificationReadFlag ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectUserNotificationDetail([FromForm] NotiDetailIn inputParams)
        {
            dm.TraceService("SelectUserNotificationDetail STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string rnt_ID = inputParams.rnt_ID == null ? "0" : inputParams.rnt_ID;

                DataTable dtNotification = dm.loadList("SelUserNotificationDetail", "sp_CustomerConnect", rnt_ID.ToString());

                if (dtNotification.Rows.Count > 0)
                {
                    List<NotiDetailOut> listItems = new List<NotiDetailOut>();
                    foreach (DataRow dr in dtNotification.Rows)
                    {

                        listItems.Add(new NotiDetailOut
                        {
                            rnd_ID = dr["rnd_ID"].ToString(),
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),
                            rnd_ord_ID = dr["rnd_ord_ID"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            CreatedDate = dr["CreatedDate"].ToString(),
                            UOM = dr["rnd_UOM"].ToString(),
                            OrdQty = dr["rnd_OrdQty"].ToString(),
                            AdjQty = dr["rnd_AdjQty"].ToString(),
                            PickedQty = dr["rnd_PickedQty"].ToString(),
                            rsn_ID = dr["rnd_rsn_ID"].ToString(),
                            rsn_Name = dr["rsn_Name"].ToString(),
                           
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
                dm.TraceService(" SelectUserNotificationDetail Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectUserNotificationDetail ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectUserNotificationCount([FromForm] NotificationCountIn inputParams)
        {
            dm.TraceService("SelectUserNotificationCount STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;

                DataTable dtNotification = dm.loadList("SelUserNotificationCount", "sp_CustomerConnect", UserID.ToString());

                if (dtNotification.Rows.Count > 0)
                {
                    List<NotificationCountOut> listItems = new List<NotificationCountOut>();
                    foreach (DataRow dr in dtNotification.Rows)
                    {

                        listItems.Add(new NotificationCountOut
                        {
                            SentCount = dr["SentCount"].ToString(),
                            ReadCount = dr["ReadCount"].ToString(),
                           
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
                dm.TraceService("SelectUserNotificationCount Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectUserNotificationCount ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string UpdateUserToken([FromForm] UpdateUserTokenIn inputParams)
        {
            dm.TraceService("UpdateUserToken STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {

                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;
                string Token = inputParams.Token == null ? "0" : inputParams.Token;

                string[] arr = { Token };
                DataTable dt = dm.loadList("UpdateUserToken", "sp_CustomerConnect", UserID.ToString() , arr);

                if (dt.Rows.Count > 0)
                {
                    List<UpdateUserTokenOut> listItems = new List<UpdateUserTokenOut>();
                    foreach (DataRow dr in dt.Rows)
                    {

                        listItems.Add(new UpdateUserTokenOut
                        {
                            Res = dr["Res"].ToString(),
                            Title = dr["Title"].ToString(),                          
                            Descr = dr["Descr"].ToString(),

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
                dm.TraceService("UpdateUserToken Exception - " + ex.Message.ToString());
            }
            dm.TraceService("UpdateUserToken ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}