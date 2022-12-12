using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_Lab3_PIN_24_Khan_Anna
{
    public delegate KeyValuePair<TKey, TValue> GenerateElement<TKey, TValue>(int j);
    public class TestCollections<TKey, TValue>
    {


        List<TKey> tKeyList;
        List<string> strList;
        Dictionary<TKey, TValue> dictionary;
        Dictionary<string, TValue> strDictionary;
        GenerateElement<TKey, TValue> generateElement;


        public TestCollections(int countInCollection, GenerateElement<TKey, TValue> generator)
        {
            tKeyList = new List<TKey>();
            strList = new List<string>();
            dictionary = new Dictionary<TKey, TValue>();
            strDictionary = new Dictionary<string, TValue>();
            generateElement = generator;

            for (int i = 0; i < countInCollection; i++)
            {
                var temp = generateElement(i);
                dictionary.Add(temp.Key, temp.Value);
                strDictionary.Add(temp.Key.ToString(), temp.Value);
                tKeyList.Add(temp.Key);
                strList.Add(temp.Key.ToString());
            }
        }

        public void searchElementInTKeyList()
        {
            Console.WriteLine("\ntKeyList \nВремя поиска:\n");

            var firstElement = tKeyList[0];
            var middleElement = tKeyList[tKeyList.Count / 2];
            var lastElement = tKeyList[tKeyList.Count - 1];
            var noneElement = generateElement(tKeyList.Count + 1).Key;

            var watch = Stopwatch.StartNew();
            tKeyList.Contains(firstElement);
            watch.Stop();
            Console.WriteLine("Для первого элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            tKeyList.Contains(middleElement);
            watch.Stop();
            Console.WriteLine("Для среднего элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            tKeyList.Contains(lastElement);
            watch.Stop();
            Console.WriteLine("Для последнего элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            tKeyList.Contains(noneElement);
            watch.Stop();
            Console.WriteLine("Для элемента, не входящего в коллекции: " + watch.Elapsed.Ticks);
        }

        public void searchElementInStrList()
        {
            Console.WriteLine("\nStrList \nВремя поиска:\n");

            var firstElement = strList[0];
            var middleElement = strList[strList.Count / 2];
            var lastElement = strList[strList.Count - 1];
            var noneElement = generateElement(strList.Count + 1).Key.ToString();

            var watch = Stopwatch.StartNew();
            strList.Contains(firstElement);
            watch.Stop();
            Console.WriteLine("Для первого элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            strList.Contains(middleElement);
            watch.Stop();
            Console.WriteLine("Для среднего элемента:" + watch.Elapsed.Ticks);

            watch.Restart();
            strList.Contains(lastElement);
            watch.Stop();
            Console.WriteLine("Для последнего элемента:" + watch.Elapsed.Ticks);

            watch.Restart();
            strList.Contains(noneElement);
            watch.Stop();
            Console.WriteLine("Для элемента, не входящего в коллекции: " + watch.Elapsed.Ticks);
        }

        public void searchElementInTKeyDictionaryByKey()
        {
            Console.WriteLine("\nDictionary<TKey, TValue> по ключу TKey \nВремя поиска:\n");

            var firstElement = dictionary.ElementAt(0).Key;
            var middleElement = dictionary.ElementAt(dictionary.Count / 2).Key;
            var lastElement = dictionary.ElementAt(dictionary.Count - 1).Key;
            var noneElement = generateElement(dictionary.Count + 1).Key;

            var watch = Stopwatch.StartNew();
            dictionary.ContainsKey(firstElement);
            watch.Stop();
            Console.WriteLine("Для первого элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            dictionary.ContainsKey(middleElement);
            watch.Stop();
            Console.WriteLine("Для среднего элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            dictionary.ContainsKey(lastElement);
            watch.Stop();
            Console.WriteLine("Для последнего элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            dictionary.ContainsKey(noneElement);
            watch.Stop();
            Console.WriteLine("Для элемента, не входящего в коллекции: " + watch.Elapsed.Ticks);
        }

        public void searchElementInTKeyDictionaryByValue()
        {
            Console.WriteLine("\nDictionary<TKey, TValue> по значению TValue \nВремя поиска:\n");

            var firstElement = dictionary.ElementAt(0).Value;
            var middleElement = dictionary.ElementAt(dictionary.Count / 2).Value;
            var lastElement = dictionary.ElementAt(dictionary.Count - 1).Value;
            var noneElement = generateElement(dictionary.Count + 1).Value;

            var watch = Stopwatch.StartNew();
            dictionary.ContainsValue(firstElement);
            watch.Stop();
            Console.WriteLine("Для первого элемента: " + watch.ElapsedTicks);

            watch.Restart();
            dictionary.ContainsValue(middleElement);
            watch.Stop();
            Console.WriteLine("Для среднего элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            dictionary.ContainsValue(lastElement);
            watch.Stop();
            Console.WriteLine("Для последнего элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            dictionary.ContainsValue(noneElement);
            watch.Stop();
            Console.WriteLine("Для элемента, не входящего в коллекции: " + watch.Elapsed.Ticks);
        }

        public void searchElementInStrDictionaryByKey()
        {
            Console.WriteLine("\nDictionary<string, TValue> по ключу TKey \nВремя поиска:\n");

            var firstElement = strDictionary.ElementAt(0).Key;
            var middleElement = strDictionary.ElementAt(strDictionary.Count / 2).Key;
            var lastElement = strDictionary.ElementAt(strDictionary.Count - 1).Key;
            var noneElement = generateElement(strDictionary.Count + 1).Key.ToString();

            var watch = Stopwatch.StartNew();
            strDictionary.ContainsKey(firstElement);
            watch.Stop();
            Console.WriteLine("Для первого элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            strDictionary.ContainsKey(middleElement);
            watch.Stop();
            Console.WriteLine("Для среднего элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            strDictionary.ContainsKey(lastElement);
            watch.Stop();
            Console.WriteLine("Для последнего элемента: " + watch.Elapsed.Ticks);

            watch.Restart();
            strDictionary.ContainsKey(noneElement);
            watch.Stop();
            Console.WriteLine("Для элемента, не входящего в коллекции: " + watch.Elapsed.Ticks);
        }

        
    }
}
