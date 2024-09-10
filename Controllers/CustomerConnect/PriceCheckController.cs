using Microsoft.AspNetCore.Mvc;
using MVC_API.FE_Nav_Service;
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

    public class PriceCheckController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;

        [HttpPost]


        public string SelCustomerPriceCheck([FromForm] selPriceCheckCustomerIn inputParams)
        {
            dm.TraceService("SelCustomerPriceCheck STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string salePersonID = inputParams.SalesPersonID == null ? "0" : inputParams.SalesPersonID;

                DataTable dtcus = dm.loadList("SelCustomerPriceCheck", "sp_CustomerConnect", salePersonID.ToString());

                if (dtcus.Rows.Count > 0)
                {
                    List<selPriceCheckCustomerOut> listItems = new List<selPriceCheckCustomerOut>();
                    foreach (DataRow dr in dtcus.Rows)
                    {

                        listItems.Add(new selPriceCheckCustomerOut
                        {
                            cus_ID = dr["cus_ID"].ToString(),
                            cus_Code = dr["cus_Code"].ToString(),
                            cus_Name = dr["cus_Name"].ToString()



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
                dm.TraceService(" SelCustomerPriceCheck Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelCustomerPriceCheck ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }
        public string SelPriceCheck([FromForm] selPriceCheckIn inputParams)
        {
            dm.TraceService("SelPriceCheck STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string ProductCode = inputParams.ProductCode == null ? "0" : inputParams.ProductCode;
                string CustomerCode = inputParams.CustomerCode == null ? "0" : inputParams.CustomerCode;

                string price = "" ;
                MasterMgmntDigits gm = dm.Creds();
                gm.GetPrice(ProductCode, CustomerCode, ref price);

                string JSONString = JsonConvert.SerializeObject(new
                {
                        UOM = "",
                      Price = price
                }) ;

                    return JSONString;
               
            }
            catch (Exception ex)
            {
                JSONString = "NoDataSQL - " + ex.Message.ToString();
                dm.TraceService(" SelPriceCheck Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelPriceCheck ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

        public string SelProductsPriceCheck([FromForm] PriceCheckProductsIn inputParams)
        {
            dm.TraceService("SelProductsPriceCheck STARTED -" + DateTime.Now);
            dm.TraceService("====================");

            try
            {
                string Product = inputParams.Product == null ? "0" : inputParams.Product;

                DataTable dtcus = dm.loadList("SelProductsPriceCheck", "sp_CustomerConnect", Product.ToString());

                if (dtcus.Rows.Count > 0)
                {
                    List<PriceCheckProductsOut> listItems = new List<PriceCheckProductsOut>();
                    foreach (DataRow dr in dtcus.Rows)
                    {

                        listItems.Add(new PriceCheckProductsOut
                        {
                            prd_ID = dr["prd_ID"].ToString(),
                            prd_Code = dr["prd_Code"].ToString(),
                            prd_Name = dr["prd_Name"].ToString(),
                            prd_Desc = dr["prd_Desc"].ToString(),


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
                dm.TraceService(" SelProductsPriceCheck Exception - " + ex.Message.ToString());
            }
            dm.TraceService("SelProductsPriceCheck ENDED - " + DateTime.Now);
            dm.TraceService("==================");


            return JSONString;
        }

    }
}