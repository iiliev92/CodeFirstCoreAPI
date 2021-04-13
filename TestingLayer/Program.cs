using BusinessLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestingLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    //CreateBrand();
                    //CreateProduct();
                    //CreateUser();
                }

                //UpdateBrandAndProduct();
                //UpdateUser();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        static void CreateBrand()
        {
            Brand brand = new Brand();
            brand.Name = Guid.NewGuid().ToString();
            
            BrandContext brandContext = new BrandContext(new Context());
            brandContext.Create(brand);

            Console.WriteLine("Brand added successfully!");
        }

        static void CreateProduct()
        {
            Product product = new Product();
            product.Barcode = Guid.NewGuid().ToString();
            product.Name = Guid.NewGuid().ToString();

            Random r = new Random();

            product.Price = r.Next();
            product.Quantity = r.Next();

            BrandContext brandContext = new BrandContext(new Context());
            List<Brand> brands = (List<Brand>)brandContext.ReadAll();

            product.Brand = brands[r.Next(0, brands.Count)];

            ProductContext productContext = new ProductContext(new Context());
            productContext.Create(product);

            Console.WriteLine("Product created successfully");
        }

        static void CreateUser()
        {
            User user = new User();
            user.Name = Guid.NewGuid().ToString();

            Random r = new Random();
            user.Age = r.Next();

            ProductContext productContext = new ProductContext(new Context());
            List<Product> products = (List<Product>)productContext.ReadAll();

            int productsCount = r.Next(1, products.Count);

            List<Product> productsForUser = new List<Product>(productsCount);

            for (int i = 0; i < productsCount; i++)
            {
                productsForUser.Add(products[r.Next(0, products.Count)]);
            }

            user.Products = productsForUser;

            UserContext userContext = new UserContext(new Context());
            userContext.Create(user);

            Console.WriteLine("User created successsfully!");
        }

        static void UpdateUser()
        {
            User user = new User();
            user.ID = 2;
            user.Name = "User 2";
            user.Age = 22;

            user.Products = new List<Product>();
            

            // Change existing user's product properties:
            Product p1 = new Product();
            p1.Barcode = "44fa3cd3-0a7f-42c1-ae03-5e0e40b44adc";

            p1.Name = "Product For Testing";
            p1.Price = 5000;
            p1.Quantity = 500;

            user.Products.Add(p1);

            // Add new product from the Database

            Product p2 = new Product();
            p2.Barcode = "cb5640cd-3e83-43ea-99ec-16844d30ad80";

            ProductContext productContext = new ProductContext(new Context());
            p2 = productContext.Read(p2.Barcode);
            p2.Name = "Updated product 2";

            user.Products.Add(p2);


            // Add new product that does not exist in the database

            Product p3 = new Product();
            p3.Barcode = Guid.NewGuid().ToString();
            p3.Name = Guid.NewGuid().ToString();

            Console.WriteLine(p3.Barcode);

            Random r = new Random();

            p3.Price = r.Next();
            p3.Quantity = r.Next();

            BrandContext brandContext = new BrandContext(new Context());

            p3.Brand = ((List<Brand>)brandContext.ReadAll())[0];

            user.Products.Add(p3);

            UserContext userContext = new UserContext(new Context());
            userContext.Update(user);

            Console.WriteLine("User updated successfully!");
        }

        static void UpdateBrandAndProduct()
        {
            Brand brand = new Brand();
            brand.ID = 1;
            brand.Name = "Updated brand's name 22";

            Context context = new Context();
            ProductContext productContext = new ProductContext(context);

            // Change existing Product:
            List<Product> productsForBrand = productContext.ReadAll().Where(p => p.Brand.ID == brand.ID).ToList();

            productsForBrand[0].Name = "Updated Productttt";
            productsForBrand[0].Quantity = 1000;

            brand.Products = productsForBrand;

            // Add new Product (existing in the DB)
            Product p1 = new Product();
            p1.Barcode = "867d2efd-7ab9-4e2e-834e-80367ef073c7";
            p1 = productContext.Read(p1.Barcode);
            p1.Quantity = 3000;
            p1.Brand = brand;

            brand.Products.Add(p1);

            // Add new Product (not existing in the DB)

            Product p2 = new Product();
            p2.Barcode = Guid.NewGuid().ToString();
            p2.Name = "New Product";
            p2.Quantity = 200;
            p2.Price = 2000;
            p2.Brand = brand;

            brand.Products.Add(p2);

            BrandContext brandContext = new BrandContext(new Context());
            brandContext.Update(brand);

            productContext = new ProductContext(new Context());

            foreach (Product product in brand.Products)
            {
                productContext.Update(product);
            }

            Console.WriteLine("Brand updated successfully!");
        }

    }
}
