using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Console;

namespace GB_Algoritmen_Lesson_6
{
    public static class FileOperation<T>
    {
        public static void SaveAsXmlFormat(T obj, string fileName)
        {
            // Сохранить объект класса Student в файле fileName в формате XML
            // typeof(Student) передает в XmlSerializer данные о классе.
            // Внутри метода Serialize происходит большая работа по постройке
            // графа зависимостей для последующего создания xml-файла.
            // Процесс получения данных о структуре объекта называется рефлексией.
            XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
            // Создаем файловый поток(проще говоря, создаем файл)
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            // В этот поток записываем сериализованные данные(записываем xml-файл)
            xmlFormat.Serialize(fStream, obj);
            fStream.Close();
        }
        public static T LoadFromXmlFormat(string fileName)
        {
            // Считать объект Student из файла fileName формата XML
            XmlSerializer xmlFormat = new XmlSerializer(typeof(T));
            Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            T obj = default(T);
            try
            {
                obj = (T)xmlFormat.Deserialize(fStream);
            }
            catch
            {

            }
            fStream.Close();
            return obj;
        }
        public static void SaveAsXmlCollectionFormat(List<T> obj, string fileName)
        {
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write); ;
            try
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<T>));
                xmlFormat.Serialize(fStream, obj);
                fStream.Close();
            }
            catch (Exception e)
            {
                WriteLine($"Ошибка при записе. {e.Message}");
                fStream.Close();
            }

        }
        public static List<T> LoadFromXmlCollectionFormat(string fileName)
        {
            List<T> obj = new List<T>();
            Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            try
            {
                XmlSerializer xmlFormat = new XmlSerializer(typeof(List<T>));
                obj = (xmlFormat.Deserialize(fStream) as List<T>);
                fStream.Close();
            }
            catch (Exception e)
            {
                WriteLine($"Ошибка при записе. {e.Message}");
                fStream.Close();
            }
            return obj;
        }

        public static void ConvertCSVtoXML(string fileName)
        {
            var lines = File.ReadAllLines(fileName);

            var xml = new XElement("TopElement",
               lines.Select(line => new XElement("Item",
                  line.Split(';')
                      .Select((column, index) => new XElement("Column" + index, column)))));

            xml.Save($"{fileName}.xml");
        }
    }
}