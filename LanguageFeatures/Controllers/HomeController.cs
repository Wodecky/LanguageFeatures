using LanguageFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Przejście do adresu URL pokazującego przykład";
        }

        public ViewResult AutoProperty()
        {
            Product myProduct = new Product();

            myProduct.Name = "Kajak";

            string productName = myProduct.Name;

            return View("Result", (object)String.Format("Nazwa produktu: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            Product myProduct = new Product()
            {
                ProductID = 100,
                Name = "Kajak",
                Description = "Łódka jednoosobowa",
                Price = 275M,
                Category = "Sporty wodne"
            };

            return View("Result", (object)String.Format($"Kategoria: {myProduct.Category}"));
        }

        public ViewResult CreateCollection()
        {
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product{Name="Kajak", Price=275M },
                    new Product{Name="Kamizelka ratunkowa", Price= 48.95M},
                    new Product{Name="Piłka nożna", Price= 19.50M},
                    new Product{Name="Flaga narożna", Price= 34.95M}
                }
            };
            decimal cartTotal = cart.TotalPrices();

            return View("Result", (object)String.Format($"Razem {cartTotal:c}"));
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product{Name="Kajak", Category="Sporty wodne", Price=275M },
                    new Product{Name="Kamizelka ratunkowa", Category="Sporty wodne", Price= 48.95M},
                    new Product{Name="Piłka nożna", Category="Piłka nożna", Price= 19.50M},
                    new Product{Name="Flaga narożna", Category="Piłka nożna", Price= 34.95M}
                }
            };

            decimal total = 0;
            foreach (Product prod in products.FilterByCategory("Piłka nożna"))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format($"Razem koszyk: {total}"));
        }

        public ViewResult UseFilterExtensionethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product{Name="Kajak", Category="Sporty wodne", Price=275M },
                    new Product{Name="Kamizelka ratunkowa", Category="Sporty wodne", Price= 48.95M},
                    new Product{Name="Piłka nożna", Category="Piłka nożna", Price= 19.50M},
                    new Product{Name="Flaga narożna", Category="Piłka nożna", Price= 34.95M}
                }
            };

            decimal total = 0;
            foreach (Product prod in products.Filter(prod => prod.Category == "Piłka nożna" || prod.Price>20))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format($"Razem koszyk: {total}"));
        }
    }
}