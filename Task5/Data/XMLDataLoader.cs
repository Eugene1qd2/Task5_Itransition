using Bogus;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Task5.Data
{
    public class XMLDataLoader<T>
    {
        XmlSerializer xmlSerializer;

        public XMLDataLoader()
        {
            xmlSerializer = new XmlSerializer(typeof(T[]));
        }
        public void SerializeData(T[] objects,string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, objects);
            }
        }

        public T[] DeserializeData(string filename)
        {
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                T[]? dataObjects = xmlSerializer.Deserialize(fs) as T[];
                return dataObjects;
            }
        }
    }
}
