using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

namespace Network
{
	public class ChatMessage
	{
        public MeaasgeType type;
        public string SenderName{ get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 对象转化数组
        /// </summary>
        /// <returns></returns>
        public byte[] ObjectToBytes()
        {
            //属性string/int/bool--->二进制写入器BinaryWrite--->内存流MemoryStream--->byte[]

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(stream);

                WriteStream(writer,type.ToString());
                WriteStream(writer, SenderName);
                WriteStream(writer, Content);
                //stream--->byte[]
                return stream.ToArray();
            }


        }

        private void WriteStream(BinaryWriter writer,string str)
        {
            //1编码
            byte[] typeBTS = Encoding.Unicode.GetBytes(str);
            //2写入长度
            writer.Write(typeBTS.Length);
            //3写入内容
            writer.Write(typeBTS);
        }
        /// <summary>
        /// 数组转化对象
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static ChatMessage ByteToObject(byte[] bytes)
        {
            ChatMessage obj = new ChatMessage();

            //byte[]--->内存流--->二进制读取器--->string/int/bool
            MemoryStream stream = new MemoryStream(bytes);
            using(BinaryReader reader = new BinaryReader(stream))
            { 
                string strType = ReadStream(reader);

                obj.type = (MeaasgeType)Enum.Parse(typeof(MeaasgeType), strType);
                obj.SenderName = ReadStream(reader);
                obj.Content = ReadStream(reader);
                return obj;
            }

        }

        private static string ReadStream(BinaryReader reader)
        {
            int typeLength = reader.ReadInt32();
            byte[] typeBTS = reader.ReadBytes(typeLength);

            string strType = Encoding.Unicode.GetString(typeBTS);
            return strType;
        }


	}
}
