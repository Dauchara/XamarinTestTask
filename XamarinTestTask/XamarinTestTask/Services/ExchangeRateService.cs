using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace XamarinTestTask.Services
{
    class ExchangeRateService
    {
        #region Send Request
        public string SendRequest(string prms)
        {
            string result = "";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"https://www.nbrb.by/api/exrates/rates?{prms}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }

            return result;

        }
        #endregion
    }
}
