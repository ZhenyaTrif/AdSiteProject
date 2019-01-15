using BL.Interfaces;
using Common.Entities;
using DataAccess.Interfaces;
using DataAccess.Wrappers;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class Bridge : IBridge
    {
        public ICategoryWrapper CategoryDA { get; set; }
        public IAdWrapper AdDA { get; set; }

        public Bridge(ICategoryWrapper categoryWrapper, IAdWrapper adWrapper)
        {
            AdDA = adWrapper;
            CategoryDA = categoryWrapper;
        }

        public IEnumerable<Category> GetCategories()
        {
            return CategoryDA.GetAll();
        }

        public void CreateCategory(Category category)
        {
            CategoryDA.Insert(category);
        }

        public void UpdateCategory(Category category)
        {
            CategoryDA.Update(category);
        }

        public void DeleteCategory(int id)
        {
            CategoryDA.Delete(id);
        }

        public void CreateAd(Ad ad)
        {
            AdDA.Insert(ad);
        }

        public void DeleteAd(int id)
        {
            AdDA.Delete(id);
        }

        public void UpdateAd(Ad ad)
        {
            AdDA.Update(ad);
        }

        public IEnumerable<Ad> GetAds()
        {
            return AdDA.GetAll();
        }

        public Category GetCategoryById(int id)
        {
            return CategoryDA.GetAll().Where(t => t.Id == id).FirstOrDefault();
        }

        public Category GetCategoryByName(string name)
        {
            return CategoryDA.GetAll().Where(t => t.CategoryName.ToLower().Contains(name.ToLower())).FirstOrDefault();
        }

        public IEnumerable<Ad> GetAdsByName(string name)
        {
            return AdDA.GetAll().Where(x => x.AdName.ToLower().Contains(name.ToLower()));
        }

        public IEnumerable<Ad> GetAdsByUserId(string id)
        {
            return AdDA.GetAll().Where(x => x.UserId == id);
        }

        public IEnumerable<Ad> GetAdsByCategory(string name)
        {
            var category = GetCategoryByName(name);
            IEnumerable<Ad> ads;
            if (category != null)
            {
                ads = AdDA.GetAll().Where(x => x.CategoryId == category.Id);
            }
            else
            {
                ads = new List<Ad>();
            }
            return ads;
        }

        public IEnumerable<Ad> GetAdsByType(string type)
        {
            return AdDA.GetAll().Where(x => x.ProductType.ToLower().Contains(type.ToLower()));
        }

        public Ad GetAdById(int id)
        {
            return AdDA.GetAll().Where(t => t.Id == id).FirstOrDefault();
        }
    }
}
