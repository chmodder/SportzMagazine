using SportzMagazine.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private string _fileName;

        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }

        public ReadWriteToFile(string fileName)
        {
            this.FileName = fileName;
        }

        #region XML
        public async void WriteToXmlFile(object obj)
        {

            //Serializing the data option 1 - WORKS ok - writes the obj data to xml file
            using (Stream stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                          this._fileName,
                          CreationCollisionOption.ReplaceExisting))
            {
                DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
                serializer.WriteObject(stream, obj);
            }
        }

        public async Task<ObservableCollection<Subscription>> ReadXMLAsync()
        {
            string content = string.Empty;

            ObservableCollection<Subscription> myCollection = new ObservableCollection<Subscription>();
            var xmlSerializer = new DataContractSerializer(typeof(ObservableCollection<Subscription>));

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(this._fileName);

            myCollection = (ObservableCollection<Subscription>)xmlSerializer.ReadObject(myStream);

            //using (StreamReader reader = new StreamReader(myStream))
            //{
            //    content = await reader.ReadToEndAsync();
                
            //}
            return myCollection;

        }
        #endregion
        /*
        string content = String.Empty;

            List<Car> myCars;
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<Car>));

            var myStream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(JSONFILENAME);

            myCars = (List<Car>)jsonSerializer.ReadObject(myStream);
        */
    }
}
