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
    [RoutePrefix("youbike")]
    public class YoubikeController : Controller
    {
        [Route("district")]
        public ActionResult District(string callback = null)
        {
            var client = new WebClient();
            var url = "http://data.taipei/opendata/datalist/apiAccess?scope=resourceAquire&rid=ddb80380-f1b3-4f8e-8016-7ed9cba571d5";

            var buffer = client.DownloadData(url);
            var json = Encoding.UTF8.GetString(buffer);

            var youbikeData = JsonConvert.DeserializeObject<TaipeiOpenData<YoubikeData>>(json);
            var areas = youbikeData.result.results.Select(x => x.sarea).Distinct().OrderBy(x => x);
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
        public ActionResult Index(string district, string callback = null)
        {
            var client = new WebClient();
            var url = "http://data.taipei/opendata/datalist/apiAccess?scope=resourceAquire&rid=ddb80380-f1b3-4f8e-8016-7ed9cba571d5";

            var buffer = client.DownloadData(url);
            var json = Encoding.UTF8.GetString(buffer);

            var youbikeData = JsonConvert.DeserializeObject<TaipeiOpenData<YoubikeData>>(json);
            var query = youbikeData.result.results;

            if (!string.IsNullOrEmpty(district))
            {
                query = query.Where(x => x.sarea == district);
            }

            var sites = query.ToList();
            json = JsonConvert.SerializeObject(sites);


            if (string.IsNullOrEmpty(callback))
            {
                Response.ContentType = "application/json";
                return Content(json);
            }

            Response.ContentType = "application/javascript";
            return Content($"{callback}({json})");
        }

        public class TaipeiOpenData<T>
        {
            public T result { get; set; }
        }

        public class YoubikeData
        {
            public int offset { get; set; }

            public int limit { get; set; }

            public int count { get; set; }

            public string sort { get; set; }

            public IEnumerable<YoubikeSite> results { get; set; }
        }

        public class YoubikeSite
        {
            public string _id { get; set; }
            public string iid { get; set; }
            public string sv { get; set; }
            public string sd { get; set; }
            public string vtyp { get; set; }
            public string sno { get; set; }
            public string sna { get; set; }
            public string sip { get; set; }
            public string tot { get; set; }
            public string sbi { get; set; }
            public string sarea { get; set; }
            public string mday { get; set; }
            public string lat { get; set; }
            public string lng { get; set; }
            public string ar { get; set; }
            public string sareaen { get; set; }
            public string snaen { get; set; }
            public string aren { get; set; }
            public string nbcnt { get; set; }
            public string bemp { get; set; }
            public string act { get; set; }
        }

    }
}