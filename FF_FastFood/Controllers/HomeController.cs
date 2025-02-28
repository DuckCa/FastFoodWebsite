﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;
using System.Web.UI.WebControls;
using FF_Fastfood.Models;
using System.Data.Entity;

namespace FF_Fastfood.Controllers
{
    public class HomeController : Controller
    {
        FF_FastFoodEntities1 db = new FF_FastFoodEntities1();
        public ActionResult Index()
        {
            
            List<Banner> bannerList = db.Banners.ToList();
            List<Category> categoryList = db.Categories.ToList();
            var popularFoodsQuery = db.Order_Items
                                    .GroupBy(oi => new {
                                        oi.Food.food_id,
                                        oi.Food.name,
                                        oi.Food.description,
                                        oi.Food.price,
                                        oi.Food.category_id,
                                        oi.Food.image_url,
                                        oi.Food.seo_title,
                                        oi.Food.seo_description,
                                        oi.Food.seo_keywords,
                                        oi.Food.slug,
                                        oi.Food.created_at,
                                        oi.Food.updated_at
                                    })
                                    .Select(group => new {
                                        group.Key.food_id,
                                        group.Key.name,
                                        group.Key.description,
                                        group.Key.price,
                                        group.Key.category_id,
                                        group.Key.image_url,
                                        group.Key.seo_title,
                                        group.Key.seo_description,
                                        group.Key.seo_keywords,
                                        group.Key.slug,
                                        group.Key.created_at,
                                        group.Key.updated_at,
                                        TotalQuantity = group.Sum(oi => oi.quantity)
                                    })
                                    .OrderByDescending(x => x.TotalQuantity)
                                    .Take(6)
                                    .ToList();


            // Khởi tạo đối tượng Food từ dữ liệu truy vấn
            var popularFoods = popularFoodsQuery
                                .Select(group => new Food
                                {
                                    food_id = group.food_id,
                                    name = group.name,
                                    description = group.description,
                                    price = group.price,
                                    category_id = group.category_id,
                                    image_url = group.image_url,
                                    seo_title = group.seo_title,
                                    seo_description = group.seo_description,
                                    seo_keywords = group.seo_keywords,
                                    slug = group.slug,
                                    created_at = group.created_at,
                                    updated_at = group.updated_at
                                })
                                .ToList();
            var viewmodel = new HomeViewModel
            {
                banners = bannerList,
                categories = categoryList,
                foods = popularFoods
            };
            return View(viewmodel);
        }
    }
}