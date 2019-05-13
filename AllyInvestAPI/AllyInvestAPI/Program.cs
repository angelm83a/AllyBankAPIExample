using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Xml;
using System.Diagnostics;
using Nito.AsyncEx;

/*Developer (modified/adapted): Angelo A. Munoz, BSc. Information Systems Engineering (I am still learning! ☺)
 * Ally Bank API uses similar functionality as found in Twitter API. So, I found it very helpful to use a Twitter's application and implement it for Ally Bank API. 
 * Documentation:
 *          https://www.ally.com/api/invest/documentation/accounts-get/
 *          https://www.ally.com/api/invest/documentation/accounts-id-get/
 *          https://www.ally.com/api/invest/documentation/streaming-market-quotes-get-post/
 * According to Ally: "The keys you received when you registered your application are all that is required." 
 *          See: https://www.ally.com/api/invest/documentation/oauth/
 *          Thus, the variables below need to be filled with your personal keys provided by the bank
 *              var oauth_consumer_key = "5LaqR_YOUR_OWN_KEY_HERE_46";
 *              var oauth_consumer_secret = "Ji_YOUR_OWN_KEY_HERE_328U5";
 *              var oauth_token = "FKz_YOUR_OWN_KEY_HERE_hQ7";
 *              var oauth_token_secret = "YDG_YOUR_OWN_KEY_HERE_Mo80";
 * Helping code by Aydin (question/answer): https://stackoverflow.com/a/27108442/7536542
 * Helping project by Mike Carlisle: https://www.codeproject.com/Articles/247336/Twitter-OAuth-authentication-using-Net
 * I adapted their examples to be used with Ally Invest API
 */

namespace AllyBankTrade
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncContext.Run(() => MainAsync(args));
        }

        static async void MainAsync(string[] args)
        {
            // oauth application keys
			var oauth_consumer_key = "5LaqR_YOUR_OWN_KEY_HERE_46";
			var oauth_consumer_secret = "Ji_YOUR_OWN_KEY_HERE_328U5";
			var oauth_token = "FKz_YOUR_OWN_KEY_HERE_hQ7";
			var oauth_token_secret = "YDG_YOUR_OWN_KEY_HERE_Mo80";


            // oauth implementation details
            var oauth_version = "1.0";
            var oauth_signature_method = "HMAC-SHA1";

            // unique request details
            var oauth_nonce = Convert.ToBase64String(new ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();
            string symbols = "AAPL,QQQ,MSFT,VXX,GOOG";
            string httpHeader = "%20HTTP%2F1.1";
            //string getQuery1 = "/v1/market/quotes.xml?symbols=" + symbols + " HTTP/1.0 ";
            string getQuery1 = "/v1/market/accounts.xml";

            // message api details
            var status = "Updating status via REST API if this works"; //?symbols=AAPL,QQQ,MSFT
            //var resource_url = "https://stream.tradeking.com/v1/market/quotes.xml?symbols=UPS";

            /*It works individually - user needs to have the account number
             * https://api.tradeking.com/v1/accounts/ACCOUNT_NUMBER_HERE.xml
             * https://api.tradeking.com/v1/accounts/ACCOUNT_NUMBER_HERE.xml
             * All accounts: https://api.tradeking.com/v1/accounts.xml
             */

            var resource_url = "https://api.tradeking.com/v1/accounts.xml";
            //var resource_url = "https://api.tradeking.com" + getQuery1;
            // create oauth signature
            var baseFormat = "OAuth oauth_consumer_key={0}&oauth_token={1}&oauth_signature_method={2}" +
                             "&oauth_timestamp={3}&oauth_nonce={4}&oauth_version={5}";
            //"&oauth_timestamp={3}&oauth_nonce={4}&oauth_version={5}&status={6}";

            var baseString = string.Format(baseFormat,
                                        oauth_consumer_key,
                                        oauth_token,                                        
                                        oauth_signature_method,
                                        oauth_timestamp,
                                        oauth_nonce,
                                        oauth_version
                                        //oauth_version,
                                        //Uri.EscapeDataString(status)
                                        );

            baseString = string.Concat("POST&", Uri.EscapeDataString(resource_url), "&", Uri.EscapeDataString(baseString));

            var compositeKey = string.Concat(Uri.EscapeDataString(oauth_consumer_secret), "&", Uri.EscapeDataString(oauth_token_secret));

            string oauth_signature;

            using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(compositeKey)))
            {
                oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes(baseString)));
            }

            // create the request header
            var headerFormat = "OAuth oauth_consumer_key={0}," +
                               "oauth_token={1}," +
                               "oauth_signature_method={2}," +
                               "oauth_signature={3}," +
                               "oauth_timestamp={4}," +
                               "oauth_nonce={5}," +
                               "oauth_version={6}";
            /*
            var headerFormat = "OAuth oauth_consumer_key=\"{0}\"," +
                               "oauth_token=\"{1}\"," +
                               "oauth_signature_method=\"{2}\"," +
                               "oauth_signature=\"{3}\"," +
                               "oauth_timestamp=\"{4}\"," +
                               "oauth_nonce=\"{5}\"," +
                               "oauth_version=\"{6}\""; */

            var authHeader = string.Format(headerFormat,
                                    Uri.EscapeDataString(oauth_consumer_key),
                                    Uri.EscapeDataString(oauth_token),                                  
                                    Uri.EscapeDataString(oauth_signature_method),
                                    Uri.EscapeDataString(oauth_signature),
                                    Uri.EscapeDataString(oauth_timestamp),
                                    Uri.EscapeDataString(oauth_nonce),
                                    Uri.EscapeDataString(oauth_version)
                            );

            // make the request
            //var postBody = "status=" + Uri.EscapeDataString(status);
            var postBody = "status = " + status;
            Console.WriteLine(postBody);

            ServicePointManager.Expect100Continue = false;

            //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(resource_url);

            //byte[] bytes;
            //bytes = System.Text.Encoding.ASCII.GetBytes("<?xml version='1.0' encoding='UTF - 8'?>");

            //bytes = System.Text.Encoding.UTF8.GetBytes("<?xml version='1.0' encoding='UTF - 8'?>");
            //webRequest.ContentLength = bytes.Length;
            //webRequest.Headers.Add("Authorization", authHeader);
            //webRequest.ContentType = "text/xml; encoding='utf-8'";
            //webRequest.Accept = "application/xml";
            //webRequest.Method = "POST";


            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(resource_url);
                request.Headers.Add("Authorization", authHeader);
                
                //request.Host = "stream.tradeking.com";
                request.ContentType = "text/xml; encoding='utf-8'";
                request.Accept = "application/xml";
                //request.Method = "GET";
                request.KeepAlive = false;
                request.ProtocolVersion = HttpVersion.Version10;
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                using (Stream stream = response.GetResponseStream())
                    
                using (StreamReader reader = new StreamReader(stream))
                {
                    Console.WriteLine("Waiting response from server..."/*response*/);
                    string responseStr = await reader.ReadToEndAsync();
                    Console.WriteLine(responseStr);
                    Console.ReadKey();
                }
            }
            catch (WebException webExc)
            {
                Console.WriteLine(webExc);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }
}
