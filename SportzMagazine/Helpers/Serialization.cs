using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using SportzMagazine.Models;

//Zuhair's code
namespace SportzMagazine.Helpers
{
    public class Serialization
    {
        private string _xmlFileName;

        public Serialization(string xmlFileName)
        {
            _xmlFileName = xmlFileName;
        }
        //Saves ObservableCollction . this is invoked from eg. viewmodel save command method
        public void SaveSubscriptionsAsXmkAsync(ObservableCollection<Subscription> subscriptions)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(subscriptions.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, subscriptions);
            SerializeSubscriptionsFileAsync(textWriter.ToString(), _xmlFileName);
        }

        //this is invoked in eg. viewmodel save command method before saving the item (because item needs to be added to existing collection)
        public async Task<ObservableCollection<Subscription>> LoadSubscriptionsFromXmlAsync()
        {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            FileInfo fInfo = new FileInfo($"{storageFolder.Path}\\{_xmlFileName}");
            if (fInfo.Exists)
            {
                string subscriptionXmlString = await DeserializeSubscriptionFileAsync(_xmlFileName);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ObservableCollection<Subscription>));
                return
                    (ObservableCollection<Subscription>)xmlSerializer.Deserialize(new StringReader(subscriptionXmlString));
            }
            else
            {
                return new ObservableCollection<Subscription>();
            }


        }

        private async void SerializeSubscriptionsFileAsync(string subscriptionString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, subscriptionString);
        }

        private async Task<string> DeserializeSubscriptionFileAsync(string xmlFileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.TryGetItemAsync(xmlFileName) as StorageFile;
            return await FileIO.ReadTextAsync(localFile);
        }
    }
}
