using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace buildschool_chu.Controllers
{
    [RoutePrefix("parking")]
    public class ParkingController : Controller
    {
        [Route("areas")]
        public ActionResult Areas(string callback = null)
        {
            var client = new WebClient();
            var url = "http://data.tycg.gov.tw/opendata/datalist/datasetMeta/download?id=f4cc0b12-86ac-40f9-8745-885bddc18f79&rid=0daad6e6-0632-44f5-bd25-5e1de1e9146f";

            var buffer = client.DownloadData(url);
            var json = Encoding.UTF8.GetString(buffer);

            var parkingInfo = JsonConvert.DeserializeObject<parkingInfo>(json);
            var areas = parkingInfo.parkingLots.Select(x => x.areaName).Distinct().OrderBy(x => x);
            json = JsonConvert.SerializeObject(areas);

            if (string.IsNullOrEmpty(callback))
            {
                Response.ContentType = "application/json";
                return Content(json);
            }

            Response.ContentType = "application/javascript";
            return Content($"{callback}({json})");
        }

        [Route("index")]
        public ActionResult Index(string area = "", string callback = null)
        {
            var client = new WebClient();
            var url = "http://data.tycg.gov.tw/opendata/datalist/datasetMeta/download?id=f4cc0b12-86ac-40f9-8745-885bddc18f79&rid=0daad6e6-0632-44f5-bd25-5e1de1e9146f";

            var buffer = client.DownloadData(url);
            var json = Encoding.UTF8.GetString(buffer);

            var parkingInfo = JsonConvert.DeserializeObject<parkingInfo>(json);
            var areas = parkingInfo.parkingLots.Where(x => x.areaName == area);
            json = JsonConvert.SerializeObject(areas);


            if (string.IsNullOrEmpty(callback))
            {
                Response.ContentType = "application/json";
                return Content(json);
            }

            Response.ContentType = "application/javascript";
            return Content($"{callback}({json})");
        }

        public class parkingInfo
        {
            public IEnumerable<parkingLot> parkingLots { get; set; }

            public class parkingLot

            {
                public string areaId { get; set; }
                public string areaName { get; set; }
                public string parkName { get; set; }
                public int totalSpace { get; set; }
                public string surplusSpace { get; set; }
                public string payGuide { get; set; }
                public string introduction { get; set; }
                public string address { get; set; }
                public float wgsX { get; set; }
                public float wgsY { get; set; }
                public string parkId { get; set; }
            }
        }
    }
}