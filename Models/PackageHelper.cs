using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_API.Models
{
    public class PackageHelper
    {
    }
    public class PackageReturnIn
    {
        public string udpID { get; set; }
        public string rotID { get; set; }
        public string cusID { get; set; }
        public string UserID { get; set; }
        public string PackageItemsJson { get; set; }
    }
    public class PackageItems
    {
        public string itmID { get; set; }
        public string Qty { get; set; }
    }
    public class PackageReturnOut
    {
        public string Res { get; set; }
        public string Title { get; set; }
        public string Descr { get; set; }
    }
}