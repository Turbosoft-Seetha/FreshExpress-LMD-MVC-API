using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using static MVC_API.Controllers.InventoryController;
using System.Xml;
//using iText.IO.Image;
//using iText.Kernel.Pdf;
//using iText.Layout.Element;
//using iText.Layout.Properties;
//using iText.Layout;
using static System.Web.Razor.Parser.SyntaxConstants;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using PdfWriter = iTextSharp.text.pdf.PdfWriter;
using Image = iTextSharp.text.Image;
using System.Diagnostics;
using MVC_API.FE_Nav_Service;

namespace MVC_API.Models
{
    public class DataModel
    {
        private static readonly object lockObject = new object();
        public DataTable loadList(string Mode, string sp, string Where)
        {
            SqlCommand cmd = null;
            SqlDataReader rdr = default(SqlDataReader);
            DataTable dt = new DataTable(Mode);
            SqlConnection cn = null;

            try
            {
                //
                cmd = new SqlCommand();
                //
                cn = FunMyCon(ref cn);

                {
                    cmd.Connection = cn;
                    cmd.CommandText = sp;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Mode", Mode);
                    cmd.Parameters.AddWithValue("@Para2", Where);
                    cmd.Parameters.Add(new SqlParameter("@Res", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@Res"].Direction = ParameterDirection.Output;

                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    dt.Load(rdr);

                    if (!rdr.IsClosed)
                        rdr.Close();

                    return dt;

                }
            }
            catch (SqlException ex)
            {
                String innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";
                //MessageBox.Show("Source: " & MyExp.Source & ControlChars.Cr & ControlChars.Cr & "State: " & MyExp.State.ToString() & ControlChars.Cr & "Class: " & MyExp.Class.ToString() & ControlChars.Cr & "Server: " & MyExp.Server & ControlChars.Cr & "Message: " & MyExp.Message.ToString() & ControlChars.Cr & "Line: " & MyExp.LineNumber.ToString())
                return null;
                //
            }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";
                //MessageBox.Show("Message : " & Exp.Message)
                return null;
                //
            }
            finally
            {
                //
                cmd.Dispose();
                if ((cn != null))
                {
                    cn.Close();
                }
                //
            }
        }

        public DataTable loadList(string Mode, string sp, string Para1, string[] Paras)
        {
            SqlCommand cmd = null;
            SqlDataReader rdr = default(SqlDataReader);
            DataTable dt = new DataTable("TT");
            SqlConnection cn = null;
            try
            {
                //
                cmd = new SqlCommand();
                //
                cn = FunMyCon(ref cn);
                {
                    cmd.Connection = cn;
                    cmd.CommandText = sp;
                    cmd.CommandTimeout = 3000;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Mode", Mode);
                    cmd.Parameters.AddWithValue("@Para1", Para1);
                    cmd.Parameters.Add(new SqlParameter("@Res", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@Res"].Direction = ParameterDirection.Output;

                    for (int i = 0; i < Paras.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@Para" + (i + 2).ToString(), Paras[i].ToString());
                    }

                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    dt.Load(rdr);

                    if (!rdr.IsClosed)
                        rdr.Close();

                    return dt;

                }
            }
            catch (SqlException ex)
            {
                String innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";
                return null;
                //
            }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";
                return null;
                //
            }
            finally
            {
                cmd.Dispose();
                if ((cn != null))
                {
                    cn.Close();
                }
            }
        }

        public DataTable loadList(string Mode, string sp)
        {

            SqlCommand cmd = null;
            SqlDataReader rdr = default(SqlDataReader);
            DataTable dt = new DataTable("TT");
            SqlConnection cn = null;

            try
            {
                //
                cmd = new SqlCommand();
                //
                cn = FunMyCon(ref cn);

                {
                    cmd.Connection = cn;
                    cmd.CommandText = sp;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Mode", Mode);
                    cmd.Parameters.Add(new SqlParameter("@Res", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@Res"].Direction = ParameterDirection.Output;

                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    dt.Load(rdr);

                    if (!rdr.IsClosed)
                        rdr.Close();

                    return dt;

                }
            }
            catch (SqlException ex)
            {
                String innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";

                return null;
                //
            }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";
                return null;
                //
            }
            finally
            {
                //
                cmd.Dispose();
                if ((cn != null))
                {
                    cn.Close();
                }
                //
            }
        }

        public string SaveData(string ProcedureName, string Mode, string Para1, string[] Paras)
        {
            //
            SqlConnection cn = null;
            SqlCommand cmd = null;

            try
            {
                //
                cmd = new SqlCommand();
                cn = FunMyCon(ref cn);

                {
                    cmd.Connection = cn;
                    cmd.CommandText = ProcedureName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Mode", Mode);
                    cmd.Parameters.AddWithValue("@Para1", Para1);
                    cmd.Parameters.Add(new SqlParameter("@Res", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@Res"].Direction = ParameterDirection.Output;

                    for (int i = 0; i < Paras.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@Para" + (i + 2).ToString(), Paras[i].ToString());

                    }



                    cmd.ExecuteNonQuery();

                    return cmd.Parameters["@Res"].Value.ToString();
                }

            }
            catch (SqlException ex)
            {
                String innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";
                return ex.Message.ToString();

            }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";
                return ex.Message.ToString();

            }
            finally
            {

                cn.Close();
                cmd.Dispose();

            }

        }

        public SqlConnection FunMyCon(ref SqlConnection _Conn)
        {
            string _ConStr = null;
            if (_Conn == null)
            {
                _ConStr = ConfigurationManager.AppSettings.Get("DIGITS-LMD");

                _Conn = new SqlConnection(_ConStr);
                _Conn.Open();
            }
            else
            {
                if (_Conn.State == ConnectionState.Closed)
                {
                    _ConStr = ConfigurationManager.AppSettings.Get("DIGITS-LMD");
                    _Conn = new SqlConnection(_ConStr);
                    _Conn.Open();
                }
            }
            return _Conn;
        }


        public string Execute(string fromEmail, string fromName, string Subject, string ToName, string ToEmail, string body, string base64stream, string fileName)
        {
            try
            {
                TraceService("=============Mial Sending Started for " + ToEmail + "==================");
                MailAddress fromAddress = new MailAddress(fromEmail, fromName);
                MailAddress toAddress = new MailAddress(ToEmail, ToName);

                MailMessage message = new MailMessage(fromAddress, toAddress);
                message.Subject = Subject;
                message.IsBodyHtml = true;
                message.Body = body;



                string smptClient = ConfigurationManager.AppSettings.Get("smtpClinet").ToString();
                string smtpUsername = ConfigurationManager.AppSettings.Get("smtpUsername").ToString();
                string smptPassword = ConfigurationManager.AppSettings.Get("smtpPassword").ToString();
                int smtpPort = Int32.Parse(ConfigurationManager.AppSettings.Get("smtpPort").ToString());
                bool ssl = bool.Parse(ConfigurationManager.AppSettings.Get("smtpSSL").ToString());

                SmtpClient client = new SmtpClient(smptClient, smtpPort);
                client.Credentials = new NetworkCredential(smtpUsername, smptPassword);
                client.EnableSsl = ssl;

                TraceService("Mail has been triggered");
                try
                {
                    client.Send(message);
                    TraceService("=============Mail sent to" + ToEmail + "==================");
                    return "Success";
                }
                catch (Exception ex)
                {
                    string innerException = ex.InnerException != null ? ex.InnerException.ToString() : " No Inner Exception";
                    TraceService("=============Mail sent Exception for " + ToEmail + "==================" + ex.Message.ToString() + "==========" + innerException);
                    return "Exception";
                }
            }
            catch (Exception ex)
            {
                string innerException = ex.InnerException != null ? ex.InnerException.ToString() : " No Inner Exception";
                TraceService("=============Mail sent Exception for " + ToEmail + "==================" + ex.Message.ToString() + "==========" + innerException);
                return "Exception";
            }
        }


        public void TraceService(string content)
        {
            try
            {
                StackTrace stackTrace = new StackTrace();
                string callingMethodName = stackTrace.GetFrame(1).GetMethod().Name;
                string callingControllerName = stackTrace.GetFrame(1).GetMethod().DeclaringType.Name;


                string LogPath = HttpRuntime.AppDomainAppPath;
                if (!Directory.Exists(LogPath + "/LogFile/" + callingControllerName))
                {
                    Directory.CreateDirectory(LogPath + "/LogFile/" + callingControllerName);
                }

                //FileStream fs = new FileStream(LogPath + "/LogFile/log_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt", FileMode.OpenOrCreate, FileAccess.Write);

                string logFileName = Path.Combine(LogPath + "/LogFile/" + callingControllerName + "/log_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt");

                // Use a lock to handle concurrent access
                lock (lockObject)
                {
                    using (StreamWriter sw = File.AppendText(logFileName))
                    {
                        sw.WriteLine(DateTimeOffset.Now.ToString("dd-MMM-yyyy HH:mm:ss") + "-" + content);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public int Pagination(int totalItems, int RowsOfPage)
        {

            int NoOfPages = totalItems / RowsOfPage;
            int Reminder = totalItems % RowsOfPage;
            NoOfPages = NoOfPages + (Reminder > 0 ? 1 : 0);
            return NoOfPages;
        }

        public string[] SplitString(string str)
        {
            string[] arr = str.Split(' ');

            string a = "";
            string b = "";
            int maxLoop = 0;
            if (str.Length < 25)
            {
                maxLoop = (arr.Length / 2);
            }
            else
            {
                maxLoop = (arr.Length / 2) + 1;
            }


            for (int i = 0; i < maxLoop; i++)
            {

                a += arr[i] + " ";
                if (a.Length > 25)
                {

                    a = a.Replace(arr[i] + " ", "");
                    a += arr[i];
                    if (a.Length > 25)
                    {
                        a = a.Replace(arr[i], "");
                        maxLoop = maxLoop - 1;
                    }
                    break;
                }

            }

            for (int i = maxLoop; i < arr.Length; i++)
            {
                b += arr[i] + " ";
            }
            string[] finalArr = { a, b };

            return finalArr;
        }


        public DataSet loadListDS(string Mode, string sp, string Where, string[] Paras)
        {

            SqlCommand cmd = null;
            SqlDataReader rdr = default(SqlDataReader);
            DataTable dt = new DataTable(Mode);
            SqlConnection cn = null;

            try
            {
                //
                cmd = new SqlCommand();
                //
                cn = FunMyCon(ref cn);
                {
                    cmd.Connection = cn;
                    cmd.CommandText = sp;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Mode", Mode);
                    cmd.Parameters.AddWithValue("@Para1", Where);
                    cmd.Parameters.Add(new SqlParameter("@Res", SqlDbType.NVarChar, 50));
                    cmd.Parameters["@Res"].Direction = ParameterDirection.Output;
                    for (int i = 0; i < Paras.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@Para" + (i + 2).ToString(), Paras[i].ToString());
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    return ds;
                }
            }
            catch (SqlException ex)
            {
                String innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";
                TraceService("innerMessage -   " + innerMessage);
                return null;
            }
            catch (Exception ex)
            {
                String innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";
                TraceService("innerMessage -   " + innerMessage);
                return null;
            }
            finally
            {
                cmd.Dispose();
                if ((cn != null))
                {
                    cn.Close();
                }
            }
        }

        public string WebServiceCall(string url, string paras)
        {
            try
            {
                TraceService("============WEB SERVICE STARTED=================");
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";

                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(paras);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                // Get the response.
                WebResponse response = request.GetResponse();
                // Display the status.
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    string responseFromServer = reader.ReadToEnd();
                    TraceService(responseFromServer);
                    TraceService("============WEB SERVICE ENDED=================");
                    response.Close();
                    return responseFromServer;
                }
            }
            catch (Exception ex)
            {
                var xptn = new
                {
                    responseCode = "NoData-501",
                    responseMessage = ex.Message.ToString(),
                };
                TraceService(ex.Message.ToString());
                TraceService("============WEB SERVICE Exception=================");
                return xptn.ToString();
            }
        }

        public DataSet bulkUpdate(DataSet das, string[] das_Params, string[] keys, string[] values, string sp)
        {
            SqlCommand cmd = null;
            SqlDataReader rdr = default(SqlDataReader);
            SqlConnection cn = null;
            try
            {
                cmd = new SqlCommand();
                cn = FunMyCon(ref cn);
                {
                    cmd.Connection = cn;
                    cmd.CommandText = sp;
                    cmd.CommandTimeout = 0;

                    cmd.CommandType = CommandType.StoredProcedure;
                    int i = 0;
                    int j = 0;
                    foreach (DataTable dt in das.Tables)
                    {
                        cmd.Parameters.AddWithValue(das_Params[i], dt);
                        i = i + 1;
                    }
                    foreach (string key in keys)
                    {
                        cmd.Parameters.AddWithValue(keys[j], values[j]);
                        j = j + 1;
                    }

                    cmd.Parameters.Add(new SqlParameter("@Res", SqlDbType.NVarChar, 1000));
                    cmd.Parameters["@Res"].Direction = ParameterDirection.Output;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);


                    return ds;

                }
            }
            catch (SqlException ex)
            {
                TraceService(ex.Message.ToString());
                return null;
                //
            }
            catch (Exception ex)
            {
                TraceService(ex.Message.ToString());
                return null;
                //
            }
            finally
            {
                //
                cmd.Dispose();
                //
                if ((cn != null))
                {
                    cn.Close();
                }
            }
        }
        public void createNode(string[] arr, string[] arrNames, XmlWriter writer)
        {
            writer.WriteStartElement("Values");
            for (int i = 0; i < arr.Length; i++)
            {
                writer.WriteStartElement(arrNames[i]);
                writer.WriteString(arr[i]);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

      
        public MasterMgmntDigits Creds()
        {
            MasterMgmntDigits gm = new MasterMgmntDigits();
			NetworkCredential ntc = new NetworkCredential();
            ntc.UserName = ConfigurationSettings.AppSettings["USERNAME"].ToString();
            ntc.Password = ConfigurationSettings.AppSettings["PASSWORD"].ToString();
			gm.Url = ConfigurationSettings.AppSettings["ERPURL"].ToString();

			gm.UseDefaultCredentials = false;
			gm.Credentials = ntc;

			return gm;
        }

        public int ERP_API_CALL()
        {
            int checkVal = 1;

            try
            {
                checkVal = Int32.Parse(ConfigurationSettings.AppSettings["Call_ERP_API"].ToString());
            }
            catch (Exception ex)
            {
                checkVal = 0;

            }
            return checkVal;
        }

        public string CombineImagesToPdf(string folderPath, string outputPath)
        {
            try
            {
                using (var document = new Document())
                {
                    using (var pdfStream = new FileStream(outputPath, FileMode.Create))
                    {
                        using (var pdfWriter = PdfWriter.GetInstance(document, pdfStream))
                        {
                            document.Open();

                            // Get all image files in the folder
                            string[] allFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);

                            foreach (var imagePath in allFiles)
                            {
                                var image = Image.GetInstance(imagePath);
                                float a4Width = 555; // Width of A4 in points
                                float a4Height = 842; // Height of A4 in points

                                image.ScaleToFit(a4Width, a4Height);
                                //image.ScaleToFit(document.PageSize.Width - 30, document.PageSize.Height);
                                document.Add(image);
                                document.NewPage();
                            }

                            document.Close();
                            document.Dispose();
                        }
                    }
                }
                return "1";
            }
            catch (Exception ex)
            {
                TraceService("CombineImagesToPdf => " + outputPath + " =>" + ex.Message.ToString());
                return "1";
            }
        }

    }
}