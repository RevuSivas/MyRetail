using MyRetail.Rest.DAL;
using MyRetail.Rest.Models;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace MyRetail.Rest.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        [Route("api/products/{id:long}")]
        public IHttpActionResult Get(long id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid ProductId.");
            }
            var url = ConfigurationManager.AppSettings["DataUrl"];

            HttpClient client = new HttpClient();

            var response = client.GetAsync(url).Result;
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Product not found.");
            };

            var jsonString = response.Content.ReadAsStringAsync().Result;
            JObject jsonResponse = JObject.Parse(jsonString);

            var productName = jsonResponse["product"]["item"]["product_description"]["title"].ToString();

            var dl = new RetailDl();
            var data = dl.getData();

            if(!data.Any())
            {
                return BadRequest("Price not found.");
            }

            var price = data.FirstOrDefault(p => p.ProductId == id);

            if (productName == null || price == null)
            {
                return BadRequest("Product not found.");
            }

            var product = new Product
            {
                Id = id,
                Name = productName,
                CurrentPrice = new Price
                {
                    Value = price.Value,
                    CurrencyCode = price.CurrencyCode
                }
            };
            
            return Ok(product);
        }

        [HttpPut]
        [Route("api/products/{id:long}")]
        public IHttpActionResult Put(long id, [FromBody]decimal price)
        {
            if (id == 0)
            {
                return BadRequest("Invalid ProductId");
            }
            var dl = new RetailDl();
            dl.updateData(id, price);

            return Ok();
        }


        // Using Mock data for update
         
        //[HttpPut]
        //[Route("api/products/{id:long}")]
        //public IHttpActionResult Put(long id, [FromBody]decimal price)
        //{
        //    var productPrice = GetTestPrice().FirstOrDefault(p => p.ProductId == id);

        //    if(productPrice == null)
        //    {
        //        return BadRequest("Product not found.");
        //    }

        //    productPrice.Value = price;

        //    return Ok();
        //}

        //private List<Price> GetTestPrice()
        //{
        //    var testPrices = new List<Price>()
        //    {
        //        new Price { ProductId = 13860428, Value = 13.79M, CurrencyCode = "USD" } ,
        //        new Price { ProductId = 15117729, Value = 3.75M, CurrencyCode = "USD" } ,
        //        new Price { ProductId = 16483589, Value = 16.99M, CurrencyCode = "USD" } ,
        //        new Price { ProductId = 16696652, Value = 11.00M, CurrencyCode = "USD" }
        //    };

        //    return testPrices;
        //}
    }
}
