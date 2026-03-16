using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

namespace DoCare.Core.Common
{
    public static class WebServiceHelper
    {
        /// <summary>
        /// 调用webservice
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        public static async Task<string> HttpPostWebService(string url, Dictionary<string, string> parm)
        {
            string result = string.Empty;
            string param = string.Empty;
            byte[] bytes = null;
            Stream writer = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            foreach (string dic in parm.Keys)
            {
                param += "&" + HttpUtility.UrlEncode(dic) + "=" + HttpUtility.UrlEncode(parm[dic]);
            }
            param = param.Substring(1);
            bytes = Encoding.UTF8.GetBytes(param);

            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;

            try
            {
                writer = request.GetRequestStream();        //获取用于写入请求数据的Stream对象
            }
            catch (Exception ex)
            {
                return "";
            }

            writer.Write(bytes, 0, bytes.Length);       //把参数数据写入请求数据流
            writer.Close();

            try
            {
                response = (HttpWebResponse)request.GetResponse();      //获得响应
            }
            catch (WebException ex)
            {
                return "";
            }

            #region 这种方式读取到的是一个返回的结果字符串
            Stream stream = response.GetResponseStream();        //获取响应流
            XmlTextReader Reader = new XmlTextReader(stream);
            Reader.MoveToContent();
            result = Reader.ReadInnerXml();
            #endregion

            #region 这种方式读取到的是一个Xml格式的字符串
            //StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            //result = reader.ReadToEnd();
            #endregion 

            response.Dispose();
            response.Close();
            Reader.Dispose();
            Reader.Close();
            stream.Dispose();
            stream.Close();
            return result;
        }

        /// <summary>
        /// Http Get
        /// </summary>
        /// <param name="uri">资源地址</param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string HttpGet(string uri, string body)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static string GetRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "get";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string result = sr.ReadToEnd();
                return result;
            }
        }
    }
}
