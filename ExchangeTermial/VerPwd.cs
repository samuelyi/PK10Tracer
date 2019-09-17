﻿using System;
using System.Collections.Generic;
using System.IO;
using
System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace ExchangeTermial
{
    public class VerPwdClass
    {
        public static string getString(string url)
        {
            //AK/SK 
            var API_KEY = "FGPi0QpCbZxZxBaN6dvqt87X"; 
            var SECRET_KEY = "HunNq6XsLjF3a7aCAuirVaVQO7CKBuwW";
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 60000; // 修改超时时间 
            //var image = File.ReadAllBytes("E:\\Work Demo\\图片提取文字\\Recognition\\Recognition\\Image\\img.jpg");
            //var url = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1564654456007&di=7832dd6f515e654bdf5074e47b6803b1&imgtype=0&src=http%3A%2F%2Fpic.962.net%2Fup%2F2018-5%2F2018527102938219310.jpg"; // 调用通用文字识别, 图片参数为本地图片，可能会抛出网络等异常，请使用try/catch捕获 //用户向服务请求识别某张图中的所有文字 
            //var result = client.GeneralBasic(image);//本地图图片 
            var result =      client.GeneralBasicUrl(url);//网络图片 //
            //var result = client.Accurate(image); //本地图片：相对于通用文字识别该产品精度更高，但是识别耗时会稍长。//
            //var result = client.General(image); //本地图片：通用文字识别（含位置信息版）//
            //var result = client.GeneralUrl(url); //网络图片：通用文字识别（含位置信息版） 
                                                 //var result = client.GeneralEnhanced(image); //本地图片：调用通用文字识别（含生僻字版） 
                                                 //var result = client.GeneralEnhancedUrl(url);//网络图片：调用通用文字识别（含生僻字版） 
                                                 //var result = client.WebImage(image);//本地图片:用户向服务请求识别一些背景复杂，特殊字体的文字。 
                                                 //var result = client.WebImageUrl(url);//网络图片:用户向服务请求识别一些背景复杂，特殊字体的文字。 
            return result.ToString();
        }

        public static string getString(Image img)
        {
            var API_KEY = "FGPi0QpCbZxZxBaN6dvqt87X";
            var SECRET_KEY = "HunNq6XsLjF3a7aCAuirVaVQO7CKBuwW";
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 60000; // 修改超时时间 

            var result = client.Accurate(imageToByteArray(img));
            JToken words = null;
            if(result.TryGetValue("words_result",out words))
            {
                if(words.Count()>0)
                {
                    if(words[0].Contains("words"))
                    {
                        return words[0]["words"].ToString();
                    }
                }
                return null;
            }
            return result.ToString();
            //var result = client.General(File.ReadAllBytes("c:\\untitled.png"));
            return result["words_result"][0]["words"].ToString();
        }

        public static string getImgString(string path)
        {
            var API_KEY = "FGPi0QpCbZxZxBaN6dvqt87X";
            var SECRET_KEY = "HunNq6XsLjF3a7aCAuirVaVQO7CKBuwW";
            var client = new Baidu.Aip.Ocr.Ocr(API_KEY, SECRET_KEY);
            client.Timeout = 60000; // 修改超时时间 
            var img = File.ReadAllBytes(path);
            var result = client.GeneralBasic(img);
            JToken words = null;
            try
            {
                //if (result.TryGetValue("words_result", out words))
                //{

                //    if (words[0].Contains("words"))
                //    {
                //        return words[0]["words"].ToString();
                //    }

                //}
                string ret = result["words_result"][0]["words"].ToString();
                return ret;
            }
            catch(Exception ce)
            {
                return result.ToString();
            }
            return string.Format("<{0}>",result.ToString());
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}