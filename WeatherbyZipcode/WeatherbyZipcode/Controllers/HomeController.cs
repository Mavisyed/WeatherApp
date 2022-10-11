using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeatherbyZipcode.Models;

namespace WeatherbyZipcode.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Weather()
        {

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public String WeatherDetail(int ZipCode)
        {

            //Assign API KEY which received from OPENWEATHERMAP.ORG  
            string appId = "8113fcc5a7494b0518bd91ef3acc074f";

            //API path with CITY parameter and other parameters.  
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?zip={0},us&appid={1}", ZipCode, appId);

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

               

                //Converting to OBJECT from JSON string.  
                RootObject weatherInfo = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

                //Special VIEWMODEL design to send only required fields not all fields which received from   
                //www.openweathermap.org api  
                ResultViewModel rslt = new ResultViewModel();

                rslt.Country = weatherInfo.sys.country;
                rslt.City = weatherInfo.name;
                rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                rslt.Description = weatherInfo.weather[0].description;
                rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                rslt.WeatherIcon = weatherInfo.weather[0].icon;

                //Converting OBJECT to JSON String   
                var jsonstring = new JavaScriptSerializer().Serialize(rslt);

                //Return JSON string.  
                return jsonstring;
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}