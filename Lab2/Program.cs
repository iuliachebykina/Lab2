using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using static System.Int32;
using static System.String;

namespace Lab2
{
    internal class Program
    {
        private static readonly Random Random = new Random();

        public static void Main(string[] args)
        {
            var repeat = 0;
            var startLength = 0;
            var maxLength = 0;
            var difference = 0;
            var denominator = 0.0;
            var minElement = 0;
            var maxElement = 0;
            var xDoc = new XmlDocument();
            try
            {
                xDoc.Load("C:/Users/cheby/RiderProjects/Lab2/Lab2/Experiment.xml");
            }
            catch
            {
                throw new ArgumentException("xml файл не найден");
            }

            var xRoot = xDoc.DocumentElement;
            if (xRoot == null) throw new ArgumentException("В xml файле не найдено элементов");
            var childArith = xRoot.ChildNodes[0].Name == "arithmeticProgression"
                ? xRoot.ChildNodes[0].ChildNodes
                : xRoot.ChildNodes[1].ChildNodes;
            var childGeom = xRoot.ChildNodes[0].Name == "arithmeticProgression"
                ? xRoot.ChildNodes[1].ChildNodes
                : xRoot.ChildNodes[0].ChildNodes;
            foreach (XmlNode childNode in childArith)
            {
                switch (childNode.Name)
                {
                    case "repeat":
                        repeat = Parse(childNode.InnerText);
                        break;
                    case "startLength":
                        startLength = Parse(childNode.InnerText);
                        break;
                    case "maxLength":
                        maxLength = Parse(childNode.InnerText);
                        break;
                    case "minElement":
                        minElement = 70 + Parse(childNode.InnerText);
                        break;
                    case "maxElement":
                        maxElement = 70 + Parse(childNode.InnerText);
                        break;
                    case "difference":
                        difference = Parse(childNode.InnerText);
                        break;
                }
            }

            var arithmeticProgression = new List<Tuple<int, int>>();
            while (startLength <= maxLength)
            {
                for (var i = 0; i < repeat; i++)
                {
                    var text = GetString(startLength, minElement, maxElement).ToCharArray();
                    var rbtSorting = new RbtSort<char>();

                    rbtSorting.Sort(text);
                    var count = rbtSorting.GetCount();
                    var tuple = Tuple.Create(text.Length, count);

                    arithmeticProgression.Add(tuple);
                }

                startLength += difference;
            }

            var sw = new StreamWriter("result.txt");
            foreach (var (item1, item2) in arithmeticProgression)
            {
                sw.WriteLine(item1 + " " + item2);
            }

            foreach (XmlNode childNode in childGeom)
            {
                switch (childNode.Name)
                {
                    case "repeat":
                        repeat = Parse(childNode.InnerText);
                        break;
                    case "startLength":
                        startLength = Parse(childNode.InnerText);
                        break;
                    case "maxLength":
                        maxLength = Parse(childNode.InnerText);
                        break;
                    case "minElement":
                        minElement = 70 + Parse(childNode.InnerText);
                        break;
                    case "maxElement":
                        maxElement = 70 + Parse(childNode.InnerText);
                        break;
                    case "denominator":
                        denominator = Convert.ToDouble(childNode.InnerText);
                        break;
                }
            }

            var geometricProgression = new List<Tuple<int, int>>();
            while (startLength <= maxLength)
            {
                for (var i = 0; i < repeat; i++)
                {
                    var text = GetString(startLength, minElement, maxElement).ToCharArray();
                    var rbtSorting = new RbtSort<char>();
                    rbtSorting.Sort(text);
                    var count = rbtSorting.GetCount();
                    var tuple = Tuple.Create(text.Length, count);
                    geometricProgression.Add(tuple);
                }

                startLength = (int) (startLength * denominator);
            }

            foreach (var (item1, item2) in geometricProgression)
            {
                sw.WriteLine(item1 + "\t" + item2);
            }
            sw.Close();
        }

        private static string GetString(int length, int minElement, int maxElement)
        {
            var letters = new List<char>();
            for (var j = 1; j <= length; j++)
            {
                var letterNum = Random.Next(minElement, maxElement);
                letters.Add((char) letterNum);
            }

            var str = Join("", letters);
            return str;
        }
    }
}