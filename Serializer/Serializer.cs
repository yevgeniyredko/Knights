using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MyGame
{
    static class Serializer
    {
        public static void WriteToBinary<T>(string filePath, T objectToWrite)
        {
            using (var stream = File.Open(filePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, objectToWrite);
            }
        }

        public static T ReadFromBinary<T>(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
