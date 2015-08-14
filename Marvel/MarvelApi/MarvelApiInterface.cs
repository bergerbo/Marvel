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

namespace Marvel.MarvelApi
{
    public class MarvelApiInterface
    {
        private static string publicKey;
        private static string privateKey;
        private static string endPoint = "http://gateway.marvel.com";

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

        public static DataWrapper<Creator> getCreators(string order, int limit, int offset)
        {
            string url = String.Format("{0}/v1/public/creators?orderBy={1}&limit={2}&offset={3}", endPoint, order, limit, offset);
            WebResponse response = apiCall(url, true);

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



        public static DataWrapper<Comic> getComics(string order, int limit, int offset)
        {
            
            string url = String.Format("{0}/v1/public/comics?orderBy={1}&limit={2}&offset={3}",endPoint, order, limit, offset);
            WebResponse response = apiCall(url, true);

            return parseAsDataWrapper<Comic>(response);
        }

        public static DataWrapper<Comic> getComics(object parameters)
        {
            Type t = parameters.GetType();
            PropertyInfo[] properties = t.GetProperties();
            bool any = false;

            StringBuilder sb = new StringBuilder(endPoint);
            sb.Append("/v1/public/comics");

            if (properties.Length > 0)
            {
                sb.Append("?");
                any = true;

                foreach (PropertyInfo property in properties)
                {
                    sb.Append(property.Name);
                    sb.Append("=");
                    sb.Append(property.GetValue(parameters).ToString());
                    sb.Append("&");
                }

                sb.Remove(sb.Length - 1, 1);
            }
            string url = sb.ToString();
            WebResponse response = apiCall(url, any);

            return parseAsDataWrapper<Comic>(response);
        }

        public static DataWrapper<Comic> getComicsForCreator(int creatorId, string order, int limit, int offset)
        {
            string url = String.Format("{0}/v1/public/creators/{1}/comics?orderBy={2}&limit={3}&offset={4}", endPoint, creatorId, order, limit, offset);
            WebResponse response = apiCall(url, true);

            return parseAsDataWrapper<Comic>(response);
        }

        public static DataWrapper<Comic> getComic(int id)
        {
            string url = String.Format("{0}/v1/public/comics/{1}", endPoint, id);
            WebResponse response = apiCall(url, false);

            return parseAsDataWrapper<Comic>(response);

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