using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.CSharp.RuntimeBinder;
using Binder = Microsoft.CSharp.RuntimeBinder.Binder;

namespace HospitalInsurance.WebApi.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlHelper
    {
        /// <summary>
        /// 文件序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlPath"></param>
        /// <returns></returns>
        public static T XmlDeserializeFromFile<T>(string xmlPath)
        {
            T result;
            try
            {
                result = XmlDeserializeFromFile<T>(xmlPath, Encoding.UTF8);
            }
            catch (Exception)
            {
                result = default(T);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="xmlPath"></param>
        public static void XmlSerializeToFile(dynamic model, string xmlPath)
        {
            if (SiteContainer.Site == null)
            {
                SiteContainer.Site = CallSite<Action<CallSite, Type, object, string, Encoding>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "XmlSerializeToFile", null, typeof(XmlHelper), new CSharpArgumentInfo[]
                {
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
                    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
                }));
            }
            SiteContainer.Site.Target(SiteContainer.Site, typeof(XmlHelper), model, xmlPath, Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="o"></param>
        /// <param name="encoding"></param>
        /// <exception cref="ArgumentNullException"></exception>
        private static void XmlSerializeInternal(Stream stream, object o, Encoding encoding)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
            XmlSerializer xmlSerializer = new XmlSerializer(o.GetType());
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                NewLineChars = "\r\n",
                Encoding = encoding,
                IndentChars = "    "
            };
            using (XmlWriter xmlWriter = XmlWriter.Create(stream, settings))
            {
                xmlSerializer.Serialize(xmlWriter, o);
                xmlWriter.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        public static void XmlSerializeToFile(object o, string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                XmlSerializeInternal(fileStream, o, encoding);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T XmlDeserialize<T>(string s, Encoding encoding)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentNullException("s");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result;
            using (MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(s)))
            {
                using (StreamReader streamReader = new StreamReader(memoryStream, encoding))
                {
                    result = (T)xmlSerializer.Deserialize(streamReader);
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T XmlDeserializeFromFile<T>(string path, Encoding encoding)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            if (encoding == null)
            {
                throw new ArgumentNullException("encoding");
            }
            string s = File.ReadAllText(path, encoding);
            return XmlDeserialize<T>(s, encoding);
        }

        /// <summary>
        /// 
        /// </summary>
        [CompilerGenerated]
        private static class SiteContainer
        {
            // Token: 0x0400014B RID: 331
            public static CallSite<Action<CallSite, Type, object, string, Encoding>> Site;
        }
    }
}