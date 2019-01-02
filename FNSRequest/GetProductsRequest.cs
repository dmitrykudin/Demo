using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace FNSRequest
{
    public class GetProductsRequest
    {
        private readonly string _url = @"https://proverkacheka.nalog.ru:9999/v1/inns/*/kkts/*/fss/";
        private string _userName = @"+79811244324";
        private string _password = @"154030";
        private HttpWebRequest _request;
        private HttpWebResponse _response;
        private string _responseText;

        public GetProductsRequest(string fss, string tickets, string fiscalSign)
        {
            _url += fss + "/";
            _url += "tickets/" + tickets + "?";
            _url += "fiscalSign=" + fiscalSign + "&sendToEmail=no";
        }

        public JObject MakeRequest()
        {
            _request = (HttpWebRequest)WebRequest.Create(_url);
            string base64Credentials = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(_userName + ":" + _password));
            _request.Method = "GET";
            _request.ContentType = "application/json; charset=utf-8";
            _request.Headers.Add("Authorization", "Basic " + base64Credentials);
            _request.Headers.Add("Device-Id", "noneOrRealId");
            _request.Headers.Add("Device-OS", "Android 5.1");
            _request.Headers.Add("Version", "2");
            _request.Headers.Add("ClientVersion", "1.4.4.1");
            _request.Headers.Add("Accept-Encoding", "gzip");
            _request.Host = "proverkacheka.nalog.ru:9999";
            _request.UserAgent = "okhttp / 3.0.1";

            _response = (HttpWebResponse)_request.GetResponse();

            using (var sr = new StreamReader(_response.GetResponseStream()))
            {
                _responseText = sr.ReadToEnd();
            }

            JObject json = new JObject();
            try
            {
                json = JObject.Parse(_responseText);
                return json;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Thread.Sleep(3000);                
                return MakeRequest();
            }            
        }
    }
}
