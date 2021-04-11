using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using static System.String;

namespace Lab2
{
    internal class Program
    {
        private static readonly Random Random = new Random();

        public static void Main(string[] args)
        {
            Console.WriteLine("Введите адрес xml-файла с планом эксперимента: " );
            var expPlanPath = Console.ReadLine();
            var xDoc = new XmlDocument();
            try
            {
                xDoc.Load(expPlanPath ?? Empty);
            }
            catch
            {
                Console.WriteLine("xml файл не найден");
                Console.ReadLine();
                return;
            }
            var xRoot = xDoc.DocumentElement;
            Console.WriteLine("Введите адрес текстового файла для вывода результатов: ");
            var resPath = Console.ReadLine();
            if (xRoot != null)
            {
                var experiments = xRoot.SelectNodes("experiment")?.Item(0)?.ChildNodes;
                using (var streamWriter = new StreamWriter(resPath ?? Empty))
                {
                    if (experiments != null)
                        foreach (XmlNode experiment in experiments)
                        {
                            if (experiment.Attributes != null &&
                                experiment.Attributes["name"].Value == "arithmetic progression")
                            {
                                var repeat = Int32.Parse(experiment.Attributes["repeat"].Value);
                                var startLength = Int32.Parse(experiment.Attributes["startLength"].Value);
                                var maxLength = Int32.Parse(experiment.Attributes["maxLength"].Value);
                                var difference = Int32.Parse(experiment.Attributes["difference"].Value);
                                var minElement = Int32.Parse(experiment.Attributes["minElement"].Value);
                                var maxElement = Int32.Parse(experiment.Attributes["maxElement"].Value);
                                while (startLength <= maxLength)
                                {
                                    for (var i = 0; i < repeat; i++)
                                    {
                                        var text = GetString(startLength, minElement, maxElement).ToCharArray();
                                        var rbtSorting = new RbtSort<char>();
                                        rbtSorting.Sort(text);
                                        var count = rbtSorting.GetCount();
                                        streamWriter.WriteLine(text.Length + " " + count);
                                    }

                                    startLength += difference;
                                }
                            }
                            else if (experiment.Attributes != null &&
                                     experiment.Attributes["name"].Value == "geometric progression")
                            {
                                var repeat = Int32.Parse(experiment.Attributes["repeat"].Value);
                                var startLength = Int32.Parse(experiment.Attributes["startLength"].Value);
                                var maxLength = Int32.Parse(experiment.Attributes["maxLength"].Value);
                                var denominator = Double.Parse(experiment.Attributes["denominator"].Value);
                                var minElement = Int32.Parse(experiment.Attributes["minElement"].Value);
                                var maxElement = Int32.Parse(experiment.Attributes["maxElement"].Value);
                                while (startLength <= maxLength)
                                {
                                    for (var i = 0; i < repeat; i++)
                                    {
                                        var text = GetString(startLength, minElement, maxElement).ToCharArray();
                                        var rbtSorting = new RbtSort<char>();
                                        rbtSorting.Sort(text);
                                        var count = rbtSorting.GetCount();
                                        streamWriter.WriteLine(text.Length + " " + count);
                                    }

                                    startLength = (int) (startLength * denominator);
                                }
                            }
                        }
                }
            }

            Console.WriteLine("Эксперимент проведен успешно. Результаты в указанном вами файле");
            Console.ReadLine();
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