using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordsMatching;

namespace EntityDatabase.Classification
{
    public class HowToClassify
    {
        string filePath = @"D:\My Docs\Visual Studio 2017\Projects\FNSRequest\log5.txt";

        public void Classify()
        {
            Dictionary<Product, ProductClass> setClass = new Dictionary<Product, ProductClass>();
            ProductClassRepository productClassRepository = new ProductClassRepository();
            ProductRepository productRepository = new ProductRepository();
            List<ProductClass> productClasses = productClassRepository.GetAllProductClasses();
            List<Product> products = productRepository.GetAllProducts();            

            foreach (var product in products)
            {
                float similarity = 0.0F;
                Dictionary<ProductClass, float> res = new Dictionary<ProductClass, float>();
                foreach (var cl in productClasses)
                {                    
                    string tagStr = "";
                    foreach (var tag in cl.ClassTags)
                    {
                        tagStr += tag.Tag.TagName + " ";
                    }
                    MatchsMaker match = new MatchsMaker(ReplaceBadStrings(product.Name), tagStr);
                    res.Add(cl, match.Score);                        
                    //if (similarity < match.Score)
                    //{
                    //    similarity = match.Score;
                    //    if (setClass.ContainsKey(product))
                    //    {
                    //        setClass.Remove(product);                            
                    //    }
                    //    setClass.Add(product, cl);
                    //    string output = "Product: " + product.Name + " is " + cl.Name + "(" + tagStr + ") with " + similarity * 100.0 + "% confidence.";
                    //    Console.WriteLine(output);
                    //    if (!File.Exists(filePath))
                    //    {
                    //        File.WriteAllText(filePath, output + Environment.NewLine);
                    //    }
                    //    else
                    //    {
                    //        File.AppendAllText(filePath, output + Environment.NewLine);
                    //    }
                    //}
                }
                var result = res.ToList();
                result.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
                Console.WriteLine("Лучшие 3 совпадения для " + product.Name + ":");
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "Лучшие 3 совпадения для " + product.Name + ":" + Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(filePath, "Лучшие 3 совпадения для " + product.Name + ":" + Environment.NewLine);
                }
                for (int i = 0; i < 3; i++)
                {
                    string output = "Product: " + product.Name + " is " + result.ElementAt(i).Key.Name + " with " + result.ElementAt(i).Value * 100.0 + "% confidence.";
                    Console.WriteLine(output);
                    File.AppendAllText(filePath, output + Environment.NewLine);
                }
            }
        }

        // HUINYA
        public void ClassifyV2()
        {
            TagRepository tagRepository = new TagRepository();
            List<Tag> tags = tagRepository.GetAllTags();
            ProductClassRepository productClassRepository = new ProductClassRepository();
            ProductRepository productRepository = new ProductRepository();
            List<Product> products = productRepository.GetAllProducts();
            List<ProductClass> productClasses = new List<ProductClass>();

            foreach (var product in products)
            {
                string[] terms = ReplaceBadStrings(product.Name).Split(' ');
                for (int i = 0; i < terms.Length; i++)
                {
                    Tag tagMatch = tags.Find(t => t.TagName == terms[i]);
                    if (tagMatch != null)
                    {
                        productClasses.AddRange(productClassRepository.GetProductClassesByTag(tagMatch));
                    }
                }

                Dictionary<ProductClass, float> res = new Dictionary<ProductClass, float>();
                foreach (var productClass in productClasses)
                {
                    string tagStr = "";
                    foreach (var tag in productClass.ClassTags)
                    {
                        tagStr += tag.Tag.TagName + " ";
                    }
                    MatchsMaker match = new MatchsMaker(ReplaceBadStrings(product.Name), tagStr);
                    res.Add(productClass, match.Score);
                }

                var result = res.ToList();
                result.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
                Console.WriteLine("Лучшие 3 совпадения для " + product.Name + ":");
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "Лучшие 5 совпадений для " + product.Name + ":" + Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(filePath, "Лучшие 5 совпадений для " + product.Name + ":" + Environment.NewLine);
                }
                if (result.Count < 5)
                {
                    foreach (var item in result)
                    {
                        string output = "Product: " + product.Name + " is " + item.Key.Name + " with " + item.Value * 100.0 + "% confidence.";
                        Console.WriteLine(output);
                        File.AppendAllText(filePath, output + Environment.NewLine);
                    }
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        string output = "Product: " + product.Name + " is " + result.ElementAt(i).Key.Name + " with " + result.ElementAt(i).Value * 100.0 + "% confidence.";
                        Console.WriteLine(output);
                        File.AppendAllText(filePath, output + Environment.NewLine);
                    }
                }                
            }
        }

        public void ClassifyV3()
        {
            Dictionary<Product, ProductClass> setClass = new Dictionary<Product, ProductClass>();
            ProductClassRepository productClassRepository = new ProductClassRepository();
            ProductRepository productRepository = new ProductRepository();
            List<ProductClass> productClasses = productClassRepository.GetAllProductClasses();
            List<Product> products = productRepository.GetAllProducts();

            foreach (var product in products)
            {
                float similarity = 0.0F;
                Dictionary<ProductClass, float> res = new Dictionary<ProductClass, float>();
                foreach (var cl in productClasses)
                {                    
                    MatchsMaker match = new MatchsMaker(ReplaceBadStrings(product.Name), ReplaceBadStrings(cl.Name));
                    res.Add(cl, match.Score);
                }
                var result = res.ToList();
                result.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
                Console.WriteLine("Лучшие 3 совпадения для " + product.Name + ":");
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "Лучшие 3 совпадения для " + product.Name + ":" + Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(filePath, "Лучшие 3 совпадения для " + product.Name + ":" + Environment.NewLine);
                }
                for (int i = 0; i < 3; i++)
                {
                    string output = "Product: " + product.Name + " is " + result.ElementAt(i).Key.Name + " with " + result.ElementAt(i).Value * 100.0 + "% confidence.";
                    Console.WriteLine(output);
                    File.AppendAllText(filePath, output + Environment.NewLine);
                }
            }
        }

        public string ReplaceBadStrings(string input)
        {
            return Regex.Replace(input, @"[\d-]", string.Empty);
        }
        
    }
}
