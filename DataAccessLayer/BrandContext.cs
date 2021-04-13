using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class BrandContext : IDBContext<Brand, int>
    {
        private Context context;

        public BrandContext(Context context)
        {
            this.context = context;
        }

        public void Create(Brand item)
        {
            context.Brands.Add(item);
            context.SaveChanges();
        }

        public Brand Read(int key)
        {
            Brand brand = context.Brands.Find(key);

            if (brand == null)
            {
                throw new ArgumentException("There is no brand with that key in the database!");
            }

            return brand;
        }

        public ICollection<Brand> ReadAll()
        {
            ICollection<Brand> brands = context.Brands.ToList();

            if (brands == null)
            {
                throw new ArgumentException("There are no brands in the database!");
            }

            return brands;
        }

        /// <summary>
        /// If you want to change Products' properties, use ProductContext.Update()
        /// </summary>
        /// <param name="item"></param>
        public void Update(Brand item)
        {
            Brand brandFromDB = context.Brands.Find(item.ID);

            if (brandFromDB != null)
            {
                context.Entry(brandFromDB).CurrentValues.SetValues(item);
                context.SaveChanges();
            }
            else
            {
                Create(item);
            }  
        }

        public void Delete(int key)
        {
            Brand brand = new Brand();
            brand.ID = key;

            context.Entry(brand).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();
        }

    }
}
