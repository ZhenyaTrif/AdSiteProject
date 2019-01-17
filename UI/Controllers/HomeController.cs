using BL.Interfaces;
using Common.Entities;
using Common.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using UI.Models.ViewModels;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        IBridge bridge;

        public HomeController(IBridge bridge)
        {
            this.bridge = bridge;
            Logger.InitLogger();
        }

        public ActionResult CategoryIndex()
        {
            return View(bridge.GetCategories());
        }

        [Authorize(Roles = "administrator, moderator")]
        public ActionResult AllCategories()
        {
            return View(bridge.GetCategories());
        }

        [HttpGet]
        [Authorize(Roles = "administrator, moderator")]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "administrator, moderator")]
        public ActionResult CreateCategory(Category category)
        {
            bridge.CreateCategory(category);
            return RedirectToAction("AllCategories");
        }

        [HttpGet]
        [Authorize(Roles = "administrator, moderator")]
        public ActionResult EditCategory(int id)
        {
            var category = bridge.GetCategoryById(id);
            return View(category);
        }

        [HttpPost]
        [Authorize(Roles = "administrator, moderator")]
        public ActionResult EditCategory(Category category)
        {
            bridge.UpdateCategory(category);
            return RedirectToAction("AllCategories");
        }

        [HttpGet]
        public ActionResult CreateAd()
        {
            List<string> category = new List<string>();
            foreach (var item in bridge.GetCategories())
            {
                category.Add(item.CategoryName);
            }
            ViewBag.Categories = category;
            List<string> type = new List<string>();
            type.Add("Товар");
            type.Add("Услуга");
            ViewBag.Type = type;
            List<string> state = new List<string>();
            state.Add("Новый");
            state.Add("БУ");
            ViewBag.State = state;
            return View();
        }
        [HttpPost]
        public ActionResult CreateAd(AdViewModel ad)
        {
            var cat = bridge.GetCategoryByName(ad.CategoryName);
            ad.UserId = HttpContext.Request.Cookies["id"].Value;
            Ad newAd = new Ad
            {
                AdName = ad.AdName,
                Info = ad.Info,
                PicPath = "-",
                CategoryId = cat.Id,
                CreateDate = DateTime.Now.Date,
                AuthorName = ad.AuthorName != "" && ad.AuthorName != null ? ad.AuthorName : "-",
                AuthorEmail = ad.AuthorEmail != "" && ad.AuthorEmail != null ? ad.AuthorEmail : "-",
                AuthorPhone = ad.AuthorPhone != "" && ad.AuthorPhone != null ? ad.AuthorPhone : "-",
                ProductPlacement = ad.ProductPlacement,
                ProductState = ad.ProductState,
                ProductType = ad.ProductType,
                UserId = HttpContext.Request.Cookies["id"].Value
            };

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("/Content/Pics/"), fileName);
                    newAd.PicPath = path;
                    file.SaveAs(path);
                }
            }
            else
            {
                newAd.PicPath = "/Content/Pics/Standart.png";
            }
            bridge.CreateAd(newAd);
            return RedirectToAction("CategoryIndex");
        }

        [HttpGet]
        public ActionResult EditAd(int adId)
        {
            List<int> category = new List<int>();
            foreach (var item in bridge.GetCategories())
            {
                category.Add(item.Id);
            }
            ViewBag.Categories = category;

            List<string> type = new List<string>();
            type.Add("Товар");
            type.Add("Услуга");
            ViewBag.Type = type;

            List<string> state = new List<string>();
            state.Add("Новый");
            state.Add("БУ");
            ViewBag.State = state;

            var ad = bridge.GetAdById(adId);
            return View(ad);
        }
        [HttpPost]
        public ActionResult EditAd(Ad ad)
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("/Content/Pics/"), fileName);
                    ad.PicPath = path;
                    file.SaveAs(path);
                }
            }
            bridge.UpdateAd(ad);
            return RedirectToAction("CategoryIndex");
        }

        public ActionResult DeleteAd(int adId)
        {
            bridge.DeleteAd(adId);
            Logger.Log.Info($"Произошло удаление объявления с индексом: {adId}");
            return RedirectToAction("CategoryIndex");
        }

        public ActionResult NavigateByCategory(int id)
        {
            var ads = bridge.GetAds().Where(x => x.CategoryId == id);
            return View(ads);
        }

        public ActionResult Details(int id)
        {
            return View(bridge.GetAdById(id));
        }

        [HttpGet]
        public ActionResult SearchByName()
        {
            var ads = bridge.GetAds();
            return View(ads);
        }
        [HttpPost]
        public ActionResult SearchByName(string Search)
        {
            var ads = bridge.GetAdsByName(Search);
            return View(ads);
        }

        [HttpGet]
        public ActionResult SearchByCategory()
        {
            var ads = bridge.GetAds();
            return View(ads);
        }
        [HttpPost]
        public ActionResult SearchByCategory(string Search)
        {
            var ads = bridge.GetAdsByCategory(Search);
            return View(ads);
        }

        [HttpGet]
        public ActionResult SearchByType()
        {
            var ads = bridge.GetAds();
            return View(ads);
        }
        [HttpPost]
        public ActionResult SearchByType(string Search)
        {
            var ads = bridge.GetAdsByType(Search);
            return View(ads);
        }
    }
}