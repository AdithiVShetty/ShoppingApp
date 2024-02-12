using Products.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        private static List<Product> productList = new List<Product>
        {
            new Product { Id = 101, Name = "HP Laptop", Price = 70000 },
            new Product { Id = 102, Name = "Dell Laptop", Price = 65000 },
            new Product { Id = 103, Name = "Macbook", Price = 150000 },
            new Product { Id = 104, Name = "Iphone", Price = 120000 },
            new Product { Id = 105, Name = "OnePlus Mobile", Price = 60000 },
            new Product { Id = 106, Name = "Printer", Price = 50000},
        };
        private static List<CartItem> cart = new List<CartItem>();
        public ActionResult Index()
        {
            return View(productList);
        }
        public ActionResult AddNewProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewProduct(Product newProduct) 
        {
            productList.Add(newProduct);
            return RedirectToAction("Index");
        }
        public ActionResult AddToCart(int productId)
        {
            var product = productList.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                var existingCartItem = cart.FirstOrDefault(item => item.Product.Id == productId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity++;
                }
                else
                {
                    cart.Add(new CartItem { Product = product, Quantity = 1 });
                }
            }
            return RedirectToAction("Index");
        }
        public  ActionResult Cart()
        {
            return View(cart);
        }
        [HttpGet]
        public ActionResult Checkout()
        {
            var checkout = new Checkout
            {
                CartItems = cart,
                TotalAmount= cart.Sum(item => item.Product.Price * item.Quantity)
            };
            return View(checkout);
        }
        [HttpPost]
        public ActionResult Checkout(Checkout checkout) 
        {
            return Content($"Thank you for your order! ");
        }
        public ActionResult ConfirmOrder(string address)
        {
            ViewBag.Address = address;
            ViewBag.TotalPrice = cart.Sum(item => item.Product.Price * item.Quantity);

            cart.Clear();
            return View();
        }
    }
}