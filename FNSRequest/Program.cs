using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using EntityDatabase.DataLayer.MSSQL;
using EntityDatabase.EntityModels;
using EntityDatabase.Classification;
using EntityDatabase.AprioriAlgorithm;

namespace FNSRequest
{
    public class Program
    {
        static void Main(string[] args)
        {
            //HowToClassify howTo = new HowToClassify();
            //howTo.ClassifyV3();
            ProductRepository productRepository = new ProductRepository();
            List<Product> products = productRepository.GetAllProducts();
            foreach (var item in products)
            {
                productRepository.CreateProduct(item.Name);
            }
        }


        public static void getProductClassesFromTemp()
        {
            List<string> pClasses = getProductClassesStrings();
            foreach (var item in pClasses)
            {
                // TODO не создавать одинаковые тэги
                //Console.WriteLine("Input: ");
                //string str = Console.ReadLine();
                //Console.WriteLine("ParentId: ");
                //int? parent = Convert.ToInt32(Console.ReadLine());
                string[] parts = item.Split(':');
                string name = parts[0].Split('"')[1];
                List<string> tags = parts[1].Split('"').Where((it, index) => index % 2 != 0).ToList();
                ProductClassRepository productClassRepository = new ProductClassRepository();
                ProductClass productClass = productClassRepository.CreateProductClass(name, null, null);
                TagRepository tagRepository = new TagRepository();
                ClassTagRepository classTagRepository = new ClassTagRepository();
                if (tags.Any())
                {
                    foreach (var tagName in tags)
                    {
                        Tag tag = tagRepository.CreateTag(tagName);
                        ClassTag classTag = classTagRepository.CreateClassTag(productClass.Id, tag.Id);
                    }
                }
            }
        }

        public static List<string> getProductClassesStrings()
        {
            List<string> pClasses = new List<string>();
            string[] parts;
            string name;
            List<string> tags = new List<string>();
            string[] file = File.ReadAllLines(@"d:\My Docs\Visual Studio 2017\Projects\FNSRequest\EntityDatabase\Classification\temp.json");
            foreach (var line in file)
            {
                try
                {
                    parts = line.Split(':');
                    name = parts[0].Split('"')[1];
                    tags = parts[1].Split('"').Where((item, index) => index % 2 != 0).ToList();
                    pClasses.Add(line);
                }
                catch(Exception ex)
                {
                    continue;
                }                
            }
            return pClasses;
        }

        public void oldMain()
        {
            //DataAccess da = new DataAccess();
            //da.CreateCustomer("myemail@gmail.com", "123456", 21);

            Guid customerId = new Guid("296BE888-B153-E811-BFD6-001583C810FA");

            while (true)
            {
                Console.WriteLine("Input fss: ");
                string fss = Console.ReadLine();

                Console.WriteLine("Input tickets: ");
                string tickets = Console.ReadLine();

                Console.WriteLine("Input fss: ");
                string fiscalSign = Console.ReadLine();

                //JObject json = JObject.Parse(File.ReadAllText(@"D:\leti\вкр\Моя работа\purchase.json"));
                GetProductsRequest request = new GetProductsRequest(fss, tickets, fiscalSign);
                JObject json = request.MakeRequest();

                if (json != null)
                {
                    PurchaseParser parser = new PurchaseParser(json);
                    parser.ParseJsonData(customerId);
                    Console.WriteLine(json.ToString(Formatting.Indented));
                }
                Console.ReadLine();
            }
        }
    }
}
