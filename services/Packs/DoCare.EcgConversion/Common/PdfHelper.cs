using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace DoCare.EcgConversion.Common
{
    public class PdfHelper
    {
        public static string PdfToImage(string strBase64)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(strBase64)) return result;
            try
            {
                PdfDocument doc = new PdfDocument();

                MemoryStream stream = new MemoryStream(Convert.FromBase64String(strBase64));
                doc.LoadFromStream(stream);

                if (doc == null) return result;

                //遍历PDF每一页
                for (int i = 0; i < doc.Pages.Count; i++)
                {
                    string currPage = string.Empty;

                    try
                    {
                        //将PDF页转换成Bitmap图形
                        Image img = doc.SaveAsImage(i);
                        currPage = GetBase64FromImage(img);
                    }
                    catch (Exception ex)
                    {
                        currPage = null;
                    }

                    if (!string.IsNullOrEmpty(currPage))
                    {
                        result += currPage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return $"data:image/png;base64,{result}";
        }

        private static string GetBase64FromImage(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            byte[] arr = null;
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();

            if (arr == null) return string.Empty;
            string pic = Convert.ToBase64String(arr);
            return pic;
        }
    }
}
