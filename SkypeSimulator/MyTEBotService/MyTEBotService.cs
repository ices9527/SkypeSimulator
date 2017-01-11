using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkypeSimulator.MyTEBotService
{
    public class MyTEBotService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string GetMyTeBotResponse(string enterpriseId, string message)
        {
            var myTeBotUrl = 
                string.Format(Properties.Settings.Default.MyTEBot.Trim(), enterpriseId, message);
            try
            {
                using (var response = _CreateWebClient())
                {
                    byte[] postData = Encoding.UTF8.GetBytes(string.Empty);
                    //response.Headers.Add("Authorization", GetOAuthToken());
                    var responseData = response.DownloadString(myTeBotUrl);
 
                    //var responseTxt = Encoding.UTF8.GetString(responseData);
                    //var result = Newtonsoft.Json.JsonConvert.ToString(responseData);
                    //"[\r\n  \"The RRD number you entered is in wrong format, RRD number should be R## or r##\"\r\n]"
                    return responseData;
                }
            }
            catch (System.Exception e)
            {
            }
            return string.Empty;
        }

        protected virtual WebClient _CreateWebClient()
        {
            return new WebClient();
        }

    }
}
