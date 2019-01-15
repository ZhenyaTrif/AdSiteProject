using Common.Entities;
using DataAccess.Interfaces;
using System.Collections.Generic;

namespace BL.Interfaces
{
    public interface IBridge
    {
        ICategoryWrapper CategoryDA { get; set; }
        IAdWrapper AdDA { get; set; }

        IEnumerable<Category> GetCategories();

        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);

        void CreateCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);

        IEnumerable<Ad> GetAds();
        IEnumerable<Ad> GetAdsByUserId(string id);
        IEnumerable<Ad> GetAdsByName(string name);
        IEnumerable<Ad> GetAdsByCategory(string name);
        IEnumerable<Ad> GetAdsByType(string type);

        Ad GetAdById(int id);

        void CreateAd(Ad ad);
        void UpdateAd(Ad ad);
        void DeleteAd(int id);
        


        


        


        
        

        

    }
}
