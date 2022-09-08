using ClientTest;
using Grpc.Net.Client;
using System;
using System.Net.Http.Headers;

namespace ClientTest
{
    internal class Program
    {

        static void Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new ProductsService.ProductsServiceClient(channel);
            string select;
            Console.Write("-----------MENU------------");
            Console.WriteLine("1.   Create product");
            Console.WriteLine("2.   Delete product");
            Console.WriteLine("3.   Update product");
            Console.WriteLine("4.   Get product by ID");
            Console.WriteLine("5.   Display list products");
            Console.WriteLine("Enter select: ");
            select = Console.ReadLine();



            switch (select)
            {
                case "1":
                    var products = client.GetAll(new Empty());
                    Console.WriteLine(products.Items);
                    break;
                case "2":
                    var products = client.GetAll(new Empty());
                    var delpro = new ProductRowIdFilter();
                    delpro.ProductRowId = 1;
                    client.Delete(delpro);
                    Console.WriteLine("\n" + products.Items);
                    break;
            }
            ////Console.WriteLine("\n" + products.Items[0]);
            //Product newpro = new Product();
            //newpro.ProductId = "a";
            //newpro.CategoryName = "cate001";
            //newpro.Manufacturer = "manu12";
            //newpro.ProductName = "product 1";
            //newpro.Price = 120;
            //Console.WriteLine(products.Items);
            //var newproduct = client.Post(newpro);
            
            //var getbyId = client.GetById(delpro);
            //getbyId.ProductName = "ebafhgwuvbsdjhvb";
            //var updatepro=client.Put(getbyId);
            //var products = client.GetAll(new Empty());
            //Console.WriteLine(products.Items);
            Console.ReadLine();
        }
        public static void GetAll()
        {


        }
    }
}
