using BusinessLayer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class ProductContext : IDBContext<Product, string>
    {
        private Context context;
        public ProductContext(Context context)
        {
            this.context = context;
        }

        public void Create(Product item)
        {
            Brand brand = context.Brands.Find(item.Brand.ID);

            if (brand != null)
            {
                item.Brand = brand;
            }

            context.Products.Add(item);
            context.SaveChanges();
        }

        public Product Read(string key)
        {
            Product product = context.Products.Find(key);

            if (product == null)
            {
                throw new ArgumentException("There is no product with that key in the database!");
            }

            return product;
        }

        public ICollection<Product> ReadAll()
        {
            ICollection<Product> products = context.Products.ToList();

            if (products == null)
            {
                throw new ArgumentException("There are no products in the database!");
            }

            return products;
        }

        public void Update(Product item)
        {
            Product productFromDB = context.Products.Find(item.Barcode);

            if (productFromDB != null)
            {
                // Edit existing record via the foreign key
                if (productFromDB.Brand.ID != item.Brand.ID)
                {
                    context.Database.ExecuteSqlRaw(
                        "update [dbo].[Products] set BrandId = @brandId where Barcode = @barcode",
                        new SqlParameter("@brandId", item.Brand.ID),
                        new SqlParameter("@barcode", item.Barcode));
                }

                context.Entry(productFromDB).CurrentValues.SetValues(item);
                context.SaveChanges();
            }
            else
            {
                Create(item);
            }
        }

        public void Delete(string key)
        {
            Product product = new Product();
            product.Barcode = key;

            context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();

        }

    }
}
