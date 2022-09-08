using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using GrpcService.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using static GrpcService.ProductsService;
namespace GrpcService.Services
{
    public class ProductsAppService: ProductsServiceBase
    {
        public BlazorContext dbContext;
        public ProductsAppService(BlazorContext DBContext)
        {
            dbContext = DBContext;
        }
        #region GetAll
        public override Task<Products> GetAll(Empty request, ServerCallContext context)
        {
            Products response = new Products();
            var products = from prd in dbContext.Products
                           select new Product()
                           {
                               ProductRowId = prd.Productrowid,
                               ProductId = prd.Productid,
                               ProductName = prd.Productname,
                               CategoryName = prd.Categoryname,
                               Manufacturer = prd.Manufacturer,
                               Price = prd.Price
                           };
            response.Items.AddRange(products.ToArray());
            return Task.FromResult(response);
        }
        #endregion
        #region GetById
        public override Task<Product> GetById(ProductRowIdFilter request, ServerCallContext context)
        {
            var product = dbContext.Products.Find(request.ProductRowId);
            var searchedProduct = new Product()
            {
                ProductRowId = product.Productrowid,
                ProductId = product.Productid,
                ProductName = product.Productname,
                CategoryName = product.Categoryname,
                Manufacturer = product.Manufacturer,
                Price = product.Price
            };
            return Task.FromResult(searchedProduct);
        }
        #endregion
        #region PostInsert
        public override Task<Product> Post(Product request, ServerCallContext context)
        {
            var prdAdded = new Models.Product()
            {
                Productid = request.ProductId,
                Productname = request.ProductName,
                Categoryname = request.CategoryName,
                Manufacturer = request.Manufacturer,
                Price = request.Price
            };
            var res = dbContext.Products.Add(prdAdded);
            dbContext.SaveChanges();
            var response = new Product()
            {
                ProductRowId = res.Entity.Productrowid,
                ProductId = res.Entity.Productid,
                ProductName = res.Entity.Productname,
                CategoryName = res.Entity.Categoryname,
                Manufacturer = res.Entity.Manufacturer,
                Price = res.Entity.Price
            };
            return Task.FromResult<Product>(response);
        }
        #endregion
        #region PUTUPDATE
        public override Task<Product> Put(Product request, ServerCallContext context)
        {
            Models.Product prd = dbContext.Products.Find(request.ProductRowId);
            if (prd == null)
            {
                return Task.FromResult<Product>(null);
            }
            prd.Productrowid = request.ProductRowId;
            prd.Productid = request.ProductId;
            prd.Productname = request.ProductName;
            prd.Categoryname = request.CategoryName;
            prd.Manufacturer = request.Manufacturer;
            prd.Price = request.Price;
            dbContext.Products.Update(prd);
            dbContext.SaveChanges();
            return Task.FromResult<Product>(new Product()
            {
                ProductRowId = prd.Productrowid,
                ProductId = prd.Productid,
                ProductName = prd.Productname,
                CategoryName = prd.Categoryname,
                Manufacturer = prd.Manufacturer,
                Price = prd.Price
            });
        }
        #endregion
        #region DELETE
        public override Task<Empty> Delete(ProductRowIdFilter request, ServerCallContext context)
        {
            Models.Product prd = dbContext.Products.Find(request.ProductRowId);
            if (prd == null)
            {
                return Task.FromResult<Empty>(null);
            }
            dbContext.Products.Remove(prd);
            dbContext.SaveChanges();
            return Task.FromResult<Empty>(new Empty());
        }
        #endregion
    }
}

//using Grpc.Net.Client;
//using GrpcService;
//using System;
//using System.Net.Http.Headers;

//namespace Client
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            using var channel = GrpcChannel.ForAddress("http://localhost:5000/");
//            var client = new ProductsService.ProductsServiceClient(channel);
//            //string select;
//            ////Console.Write("-----------MENU------------");
//            ////Console.WriteLine("1.   Create product");
//            ////Console.WriteLine("2.   Delete product");
//            ////Console.WriteLine("3.   Update product");
//            ////Console.WriteLine("4.   Get product by ID");
//            ////Console.WriteLine("5.   Display list products");
//            ////Console.WriteLine("Enter select: ");
//            //select = Console.ReadLine();



//            //switch(select)
//            //{
//            //    case "1":
//            //}
//            ////Console.WriteLine("\n" + products.Items[0]);
//            //Product newpro = new Product();
//            //newpro.ProductId = "a";
//            //newpro.CategoryName = "cate001";
//            //newpro.Manufacturer = "manu12";
//            //newpro.ProductName = "product 1";
//            //newpro.Price = 120;
//            //Console.WriteLine(products.Items);
//            //var newproduct = client.Post(newpro);
//            //var delpro = new ProductRowIdFilter();
//            //delpro.ProductRowId = 1;
//            ////client.Delete(delpro);
//            ////Console.WriteLine("\n" + products.Items);
//            //var getbyId = client.GetById(delpro);
//            //getbyId.ProductName = "ebafhgwuvbsdjhvb";
//            //var updatepro=client.Put(getbyId);
//            var products = client.GetAll(new Empty());
//            Console.WriteLine(products.Items);
//            Console.ReadLine();
//        }
//    }
//}
