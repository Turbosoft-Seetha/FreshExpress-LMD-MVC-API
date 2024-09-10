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
    public class CustomerController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;
        [HttpPost]
        public string SelectCustomers([FromForm] CustomerIN inputParams)
        {
            dm.TraceService("SelectCustomers STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string usrID = inputParams.usrID == null ? "0" : inputParams.usrID;
                string customer = inputParams.customer == null ? "0" : inputParams.customer;

                string[] arr = { usrID.ToString() };

                DataTable dtTrnsIn = dm.loadList("SelCustomers", "sp_CustomerConnect", customer.ToString() , arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<CustomerOut> listItems = new List<CustomerOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new CustomerOut
                        {
                           
                            cus_HeaderID = dr["csh_ID"].ToString(),
                            cus_HeaderCode = dr["csh_Code"].ToString(),
                            cus_HeaderName = dr["csh_Name"].ToString(),
                            
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
                dm.TraceService(" SelectCustomers Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectCustomers ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectCustomerActions([FromForm] CustomerActionIn inputParams)
        {
            dm.TraceService("SelectCustomerActions STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string CusID = inputParams.CusID == null ? "0" : inputParams.CusID;

                string[] arr = { ToDate.ToString(), CusID.ToString() };
                DataTable dtTrnsIn = dm.loadList("SelCustomersActionCounts", "sp_CustomerConnect", FromDate.ToString(),arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<CustomerActionOut> listItems = new List<CustomerActionOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new CustomerActionOut
                        {
                            Del_Count = dr["Del_Count"].ToString(),
                            PD_Count = dr["PD_Count"].ToString(),
                            FD_Count = dr["FD_Count"].ToString(),
                            Return_Count = dr["Return_Count"].ToString(),
                            AR_Count = dr["AR_Count"].ToString(),
                            PriceApproval_Count = dr["PriceApproval_Count"].ToString(),
                            Invoice_Count = dr["Invoice_Count"].ToString(),

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
                dm.TraceService(" SelectCustomerActions Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectCustomerActions ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectCustomerARCollection([FromForm] CustomerActionARIn inputParams)
        {
            dm.TraceService("SelectCustomerActions STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string CusID = inputParams.CusID == null ? "0" : inputParams.CusID;

                string[] arr = { ToDate.ToString(), CusID.ToString() };
                DataTable dtTrnsIn = dm.loadList("SelCustomersARCollection", "sp_CustomerConnect", FromDate.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<CustomerActionAROut> listItems = new List<CustomerActionAROut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new CustomerActionAROut
                        {
                            arh_ID = dr["arh_ID"].ToString(),                      
                            arh_ARNumber = dr["arh_ARNumber"].ToString(),
                            Date = dr["Date"].ToString(),
                            CollectedAmount = dr["arh_CollectedAmount"].ToString(),
                           
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
                dm.TraceService(" SelectCustomerActions Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectCustomerActions ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectCustomerDeliveries([FromForm] CustomerActionIn inputParams)
        {
            dm.TraceService("SelectCustomerDeliveries STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string CusID = inputParams.CusID == null ? "0" : inputParams.CusID;

                string[] arr = { ToDate.ToString() ,CusID.ToString() };
                DataTable dtTrnsIn = dm.loadList("SelCustomerDeliveries", "sp_CustomerConnect", FromDate.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<CustomerActionDelOut> listItems = new List<CustomerActionDelOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new CustomerActionDelOut
                        { 
                            dln_ID = dr["dln_ID"].ToString(),
                            dln_DeliveryNumber = dr["dln_DeliveryNumber"].ToString(),
                            Date = dr["Date"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            ord_LPONumber = dr["ord_LPONumber"].ToString(),
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
                dm.TraceService(" SelectCustomerDeliveries Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectCustomerDeliveries ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectCustomerReturns([FromForm] CustomerActionIn inputParams)
        {
            dm.TraceService("SelectCustomerReturns STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string CusID = inputParams.CusID == null ? "0" : inputParams.CusID;

                string[] arr = { ToDate.ToString(),CusID.ToString() };
                DataTable dtTrnsIn = dm.loadList("SelCustomerReturns", "sp_CustomerConnect", FromDate.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<CustomerActionReturnOut> listItems = new List<CustomerActionReturnOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new CustomerActionReturnOut
                        {
                            rtn_ID = dr["rtn_ID"].ToString(),
                            rtn_Number = dr["rtn_Number"].ToString(),
                            Date = dr["Date"].ToString(),
                           
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
                dm.TraceService(" SelectCustomerReturns Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectCustomerReturns ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelectCustomerInvoices([FromForm] CustomerActionIn inputParams)
        {
            dm.TraceService("SelectCustomerInvoices STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string CusID = inputParams.CusID == null ? "0" : inputParams.CusID;

                string[] arr = { ToDate.ToString(),CusID.ToString() };
                DataTable dtTrnsIn = dm.loadList("SelCustomerInvoices", "sp_CustomerConnect", FromDate.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<CustomerActionInvOut> listItems = new List<CustomerActionInvOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new CustomerActionInvOut
                        {
                            inv_ID = dr["inv_ID"].ToString(),
                            inv_InvoiceID = dr["inv_InvoiceID"].ToString(),
                            Date = dr["Date"].ToString(),
                            inv_TotalAmount = dr["inv_TotalAmount"].ToString(),
                           
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
                dm.TraceService(" SelectCustomerInvoices Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectCustomerInvoices ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelectCustomerPriceApproval([FromForm] CustomerActionIn inputParams)
        {
            dm.TraceService("SelectCustomerPriceApproval STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string CusID = inputParams.CusID == null ? "0" : inputParams.CusID;

                string[] arr = { ToDate.ToString(),CusID.ToString() };
                DataTable dtTrnsIn = dm.loadList("SelCustomerPriceApproval", "sp_CustomerConnect", FromDate.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<CustomerActionPriceApprovalOut> listItems = new List<CustomerActionPriceApprovalOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new CustomerActionPriceApprovalOut
                        {
                            pqh_ID = dr["pqh_ID"].ToString(),
                            dsp_DispatchID = dr["dsp_DispatchID"].ToString(),
                            Date = dr["Date"].ToString(),
                            ord_ERP_OrderNo = dr["ord_ERP_OrderNo"].ToString(),
                            ord_LPONumber = dr["ord_LPONumber"].ToString(),
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
                dm.TraceService(" SelectCustomerPriceApproval Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelectCustomerPriceApproval ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelDropdownForDeliveryInvoices([FromForm] DelInvIn inputParams)
        {
            dm.TraceService("SelDropdownForDeliveryInvoices STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string FromDate = inputParams.FromDate == null ? "0" : inputParams.FromDate;
                string ToDate = inputParams.ToDate == null ? "0" : inputParams.ToDate;
                string CusID = inputParams.CusID == null ? "0" : inputParams.CusID;

                string[] arr = { ToDate.ToString(), CusID.ToString() };
                DataTable dtTrnsIn = dm.loadList("SelDropdownDeliveryNote", "sp_CustomerConnect", FromDate.ToString(), arr);

                if (dtTrnsIn.Rows.Count > 0)
                {
                    List<DeliveryInvOut> listItems = new List<DeliveryInvOut>();
                    foreach (DataRow dr in dtTrnsIn.Rows)
                    {

                        listItems.Add(new DeliveryInvOut
                        {
                            inv_ID = dr["dln_InvoiceNumber"].ToString(),
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
                dm.TraceService(" SelDropdownForDeliveryInvoices Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelDropdownForDeliveryInvoices ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
    }
}