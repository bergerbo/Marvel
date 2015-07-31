using System;
using System.Net;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Marvel.Models;

namespace Marvel.MarvelApi
{
    public class MarvelApiInterface
    {
        private static string publicKey;
        private static string privateKey;
        private static string endPoint = "http://gateway.marvel.com/";
        private static MD5 mdhash;

        public static void Init()
        {
            publicKey = System.Configuration.ConfigurationManager.AppSettings["MarvelApiPublic"];
            privateKey = System.Configuration.ConfigurationManager.AppSettings["MarvelApiPrivate"];
            mdhash = MD5.Create();
        }

        public static KeyHash generateKeyHash()
        {
            KeyHash keyHash = new KeyHash();
            keyHash.apikey = publicKey;
            keyHash.ts = DateTime.Now.ToString("yyyyMMddHHmm");

            string concat = keyHash.ts + privateKey + publicKey;
            byte[] md5 = mdhash.ComputeHash(Encoding.UTF8.GetBytes(concat));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5.Length; i++)
            {
                sb.Append(md5[i].ToString("x2"));
            }

            keyHash.hash = sb.ToString();

            return keyHash;
        }

        public static ComicDataWrapper getComics(int limit, int offset, string order)
        {
            KeyHash keyHash = generateKeyHash();
            string parameters = String.Format("orderBy={0}&limit={1}&offset={2}&{3}", order, limit, offset, keyHash.asParams());
            string url = endPoint + "v1/public/comics?" + parameters;
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            WebResponse response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string result = reader.ReadToEnd();
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
                jsonSettings.MaxDepth = 10;
                jsonSettings.DateParseHandling = DateParseHandling.DateTime;
                ComicDataWrapper cdw = JsonConvert.DeserializeObject<ComicDataWrapper>(result,jsonSettings);
                return cdw;
            }
        }

        public class KeyHash
        {
            public string apikey;
            public string ts;
            public string hash;

            public string asParams()
            {
                return "apikey=" + apikey + "&ts=" + ts + "&hash=" + hash;
            }
        }
    }
}