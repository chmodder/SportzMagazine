using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Storage;

namespace SportzMagazine.Helpers
{
    public class ReadWriteToFile
    {
        #region XML
        public static async void WriteToXmlFile(object obj)
        {

            string fileName = "xml.txt";

            //Serializing the data option 1 - WORKS ok - writes the obj data to xml file
            using (Stream stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                          fileName,
                          CreationCollisionOption.ReplaceExisting))
            {
                DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(stream, obj);
            }
        }

        public static async Task<object> ReadXMLAsync(string fileName)
        {
            string content = String.Empty;

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(fileName);
            using (StreamReader reader = new StreamReader(myStream))
            {
                content = await reader.ReadToEndAsync();
            }
            return content;
        }
        #endregion

        #region JSON
        public static async Task WriteJsonAsyncFile(object obj)
        {
            // Notice that the write is ALMOST identical ... except for the serializer.

            string fileName = "json.txt";

            using (Stream stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                          fileName,
                          CreationCollisionOption.ReplaceExisting))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                serializer.WriteObject(stream, obj);
            }            
        }
        #endregion
    }
}
