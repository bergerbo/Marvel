using System;
using System.Net;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using Marvel.Models;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Linq;

namespace Marvel.MarvelApi
{
    public class MarvelApiInterface
    {
        private static string publicKey;
        private static string privateKey;
        private static string endPoint = "http://gateway.marvel.com";

        public static void Init()
        {
            publicKey = System.Configuration.ConfigurationManager.AppSettings["MarvelApiPublic"];
            privateKey = System.Configuration.ConfigurationManager.AppSettings["MarvelApiPrivate"];
        }

        public static KeyHash generateKeyHash()
        {
            KeyHash keyHash = new KeyHash();
            keyHash.apikey = publicKey;
            keyHash.ts = DateTime.Now.ToString("yyyyMMddHHmm");

            string concat = keyHash.ts + privateKey + publicKey;

            MD5 mdhash = MD5.Create();
            byte[] md5 = mdhash.ComputeHash(Encoding.UTF8.GetBytes(concat));

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5.Length; i++)
            {
                sb.Append(md5[i].ToString("x2"));
            }

            keyHash.hash = sb.ToString();

            return keyHash;
        }

        internal static DataWrapper<Creator> getCreators(object parameters)
        {
            WebResponse response = apiCall(endPoint + "/v1/public/creators", parameters);

            return parseAsDataWrapper<Creator>(response);
        }

        public static DataWrapper<Creator> getCreator(int id)
        {
            string url = String.Format("{0}/v1/public/creators/{1}", endPoint, id);
            WebResponse response = apiCall(url, false);

            return parseAsDataWrapper<Creator>(response);
        }

        internal static DataWrapper<Creator> getCreator(string url)
        {
            WebResponse response = apiCall(url, false);

            return parseAsDataWrapper<Creator>(response);
        }

        public static DataWrapper<Comic> getComics(object parameters)
        {
            WebResponse response = apiCall(endPoint+"/v1/public/comics",parameters);

            return parseAsDataWrapper<Comic>(response);
        }

        public static DataWrapper<Comic> getComicsForCreator(int creatorId, object parameters)
        {
            string url = String.Format("{0}/v1/public/creators/{1}/comics", endPoint, creatorId);
            WebResponse response = apiCall(url, parameters);

            return parseAsDataWrapper<Comic>(response);
        }

        public static DataWrapper<Comic> getComic(int id)
        {
            string url = String.Format("{0}/v1/public/comics/{1}", endPoint, id);
            WebResponse response = apiCall(url, false);

            return parseAsDataWrapper<Comic>(response);

        }

        private static WebResponse apiCall(string url, object parameters)
        {
            Type t = parameters.GetType();
            PropertyInfo[] properties = t.GetProperties().ToArray();
            bool any = false;

            StringBuilder sb = new StringBuilder(url);

            if (properties.Length > 0)
            {
                sb.Append("?");
                any = true;

                foreach (PropertyInfo property in properties)
                {
                    var val = property.GetValue(parameters);
                    if (val != null && val.ToString() != "")
                    {
                        sb.Append(property.Name);
                        sb.Append("=");
                        sb.Append(val.ToString());
                        sb.Append("&");
                    }
                }

                sb.Remove(sb.Length - 1, 1);
            }

            return  apiCall(sb.ToString(), any);
        }

        private static WebResponse apiCall(string url, bool withParams)
        {
            KeyHash keyHash = generateKeyHash();
            char separator = withParams ? '&' : '?';
            string finalUrl = String.Format("{0}{1}{2}", url, separator, keyHash.asParams());
            WebRequest request = WebRequest.Create(finalUrl);
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            return response;
        }

        private static DataWrapper<Data> parseAsDataWrapper<Data>(WebResponse response)
        {
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                string result = reader.ReadToEnd();
                DataWrapper<Data> dataWrapper = new JavaScriptSerializer().Deserialize<DataWrapper<Data>>(result);
                return dataWrapper;
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