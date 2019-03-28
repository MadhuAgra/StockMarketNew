using StockMarketWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockMarketWebAPI.Controllers
{
    [RoutePrefix("api/User")]
    public class ValuesController : ApiController
    {
        SQLConnectionClass obj = new SQLConnectionClass();
        [HttpGet]
        [System.Web.Http.Route("Login")]
        public User Get(string email, string password)
        {
            return obj.Login(email, password);
        }

        [HttpPost]
        [System.Web.Http.Route("RegisterUser")]
        public int Post([FromBody]User user)
        {
            return obj.RegisterUser(user);
        }
        [HttpGet]
        [System.Web.Http.Route("CheckExistingUser")]
        public User Get(string email)
        {
            return obj.CheckExistingUser(email);
        }
        [HttpGet]
        [System.Web.Http.Route("GetAllStockDetails")]
        public string Get()
        {
            return obj.GetAllStockDetails();
        }
        [HttpGet]
        [System.Web.Http.Route("GetUserStock")]
        public string Get(int userId)
        {
            return obj.GetUserStock(userId);
        }

        [HttpGet]
        [System.Web.Http.Route("GetSingleStockDetails")]
        public Stock GetSingleStockDetails(int stockId)
        {
            return obj.GetSingleStockDetails(stockId);
        }

        [HttpPost]
        [System.Web.Http.Route("InsertOrUpdateTransaction")]
        public int Post([FromBody]StockHoldings transaction)
        {
            return obj.InsertOrUpdateTransaction(transaction);
        }


        [HttpGet]
        [System.Web.Http.Route("UpdateUserDetails")]
        public int Get(int userId,decimal cashValue, decimal stockValue)
        {
            return obj.UpdateUserDetails(userId,cashValue,stockValue);
        }

        [HttpPut]
        [System.Web.Http.Route("DecreaseQuantityTransaction")]
        public int Put([FromBody]StockHoldings transaction)
        {
            return obj.DecreaseQuantityTransaction(transaction);
        }

    }
}
