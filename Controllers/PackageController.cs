using Microsoft.AspNetCore.Mvc;
using MVC_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
using System.Xml;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;


namespace MVC_API.Controllers
{
    public class PackageController : Controller
    {
        DataModel dm = new DataModel();
        string JSONString = string.Empty;

        [HttpPost]
        public string InsPackageReturn([FromForm] PackageReturnIn inputParams)
        {
            dm.TraceService("InsPackageReturn STARTED -" + DateTime.Now.ToString());
            dm.TraceService("====================");
            try
            {
                List<PackageItems> itemData = JsonConvert.DeserializeObject<List<PackageItems>>(inputParams.PackageItemsJson);

                dm.TraceService("itemData  - " + inputParams.PackageItemsJson);

                string udpID = inputParams.udpID == null ? "0" : inputParams.udpID;
                string rotID = inputParams.rotID == null ? "0" : inputParams.rotID;
                string cusID = inputParams.cusID == null ? "0" : inputParams.cusID;
                string UserID = inputParams.UserID == null ? "0" : inputParams.UserID;

                string DetailXml = "";

                using (var sw = new StringWriter())
                {
                    using (var writer = XmlWriter.Create(sw))
                    {

                        writer.WriteStartDocument(true);
                        writer.WriteStartElement("r");
                        int c = 0;
                        foreach (PackageItems id in itemData)
                        {
                            string[] arr = { id.itmID.ToString(), id.Qty.ToString() };
                            string[] arrName = { "itmID", "Qty" };
                            dm.createNode(arr, arrName, writer);
                        }

                        writer.WriteEndElement();
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    DetailXml = sw.ToString();
                }
               

                string[] ar = { rotID.ToString(), cusID.ToString(), UserID.ToString(), DetailXml.ToString()};
                DataTable dtReturn = dm.loadList("InsPackageReturn", "sp_PackageItems", udpID.ToString(), ar);

                dm.TraceService("dtReturn - " + dtReturn);

                if (dtReturn.Rows.Count > 0)
                {
                    List<PackageReturnOut> listReturnout = new List<PackageReturnOut>();
                    foreach (DataRow dr in dtReturn.Rows)
                    {
                        listReturnout.Add(new PackageReturnOut
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
                dm.TraceService("InsPackageReturn Exception");
                dm.TraceService(ex.Message.ToString());
            }
            dm.TraceService("InsPackageReturn ENDED");
            dm.TraceService("==================");
            return JSONString;
        }
    }
}