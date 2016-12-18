using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LoowooTech.Planner.Common.Utility
{
    public class Serializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static object LoadObjectFromFile(Type type,string fileName)
        {
            ApplicationAssert.CheckCondition(File.Exists(fileName) && Path.GetFileName(fileName) != string.Empty, string.Format("“{0}”文件不存在！", fileName), ApplicationAssert.LineNumber);
            TextReader textReader = null;
            object result;
            try
            {
                textReader = new StreamReader(fileName);
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                result = xmlSerializer.Deserialize(textReader);

            }catch(Exception ex)
            {
                ApplicationLog.WriteError(string.Format("从文件{0}钟反序列化对象{1}错误。错误详细信息：\n {2}", fileName, type.ToString(), ex.ToString()));
                throw;
            }
            finally
            {
                if (textReader != null)
                {
                    textReader.Close();
                }
            }
            return result;
        }
    }
}
