using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_DynamoDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<OrderController> _logger;
        private readonly IDynamoDBContext _DynamoDBContext;
        public OrderController(ILogger<OrderController> logger,IDynamoDBContext dynamoDBContext)
        {
            _logger = logger;
            _DynamoDBContext = dynamoDBContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> Get(string Id="1")
        {
            return await _DynamoDBContext
                .QueryAsync<Order>(Id)
                .GetRemainingAsync();

        }
        [HttpPost]
        public async Task post(string Id,string ItemName,int Qty)
        {
          var  OrderObj = new Order();
            OrderObj.Id = Id;
            OrderObj.ItemName = ItemName;
            OrderObj.Qty = Qty;
           await _DynamoDBContext.SaveAsync(OrderObj);
        }
     //   [HttpGet]
     //    public IEnumerable<Order> Get()
     //   {
     //var rng = new Random();
     //return Enumerable.Range(1, 5).Select(index => new Order
     //{
     //    Date = DateTime.Now.AddDays(index),
     //    TemperatureC = rng.Next(-20, 55),
     //    Summary = Summaries[rng.Next(Summaries.Length)]
     //})
     //.ToArray();
     // }
    }
}
