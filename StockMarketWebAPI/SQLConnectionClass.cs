using Newtonsoft.Json;
using StockMarketWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StockMarketWebAPI
{
    public class SQLConnectionClass
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private string _spLogin = ConfigurationManager.AppSettings["sp_Login"];
        private string _spInsertUser = ConfigurationManager.AppSettings["sp_InsertUser"];
        private string _spGetUser = ConfigurationManager.AppSettings["sp_GetUser"];
        private string _spGetStockIds = ConfigurationManager.AppSettings["sp_GetStockIds"];
        private string _spGetStockDetails = ConfigurationManager.AppSettings["sp_GetStockDetails"];
        private string _spGetAllStockDetails = ConfigurationManager.AppSettings["sp_GetAllStockDetails"];
        private string _spInsertOrUpdateTransaction = ConfigurationManager.AppSettings["sp_InsertOrUpdateTransaction"];
        private string _spUpdateUser = ConfigurationManager.AppSettings["sp_UpdateUser"];
        private string _spGetQuantity = ConfigurationManager.AppSettings["sp_GetQuantity"];
        private string _spDecreaseQuantity = ConfigurationManager.AppSettings["sp_DecreaseQuantity"];

        public User Login(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand(_spLogin, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    User user = new User();
                    while (rdr.Read())
                    {
                        user.userId = Convert.ToInt32(rdr["UserId"]);
                        user.userName= Convert.ToString(rdr["UserName"]);
                        user.email = Convert.ToString(rdr["Email"]);
                        user.password = Convert.ToString(rdr["Password"]);
                        user.cashValue = Convert.ToDecimal(rdr["CashValue"]);
                        user.stockValue = Convert.ToDecimal(rdr["StockValue"]);
                    }
                    return user;
                }
                return null;
            }

        }

        public int RegisterUser(User user)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(_spInsertUser, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", user.userName);
                cmd.Parameters.AddWithValue("@Email", user.email);
                cmd.Parameters.AddWithValue("@Password", user.password);
                int rdr = cmd.ExecuteNonQuery();
                return rdr;
            }
        }

        public User CheckExistingUser(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand(_spGetUser, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", email);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    User user = new User();
                    while (rdr.Read())
                    {
                        user.userId = Convert.ToInt32(rdr["UserId"]);
                        user.userName = Convert.ToString(rdr["UserName"]);
                        user.email = Convert.ToString(rdr["Email"]);
                        user.password = Convert.ToString(rdr["Password"]);
                        user.cashValue = Convert.ToDecimal(rdr["CashValue"]);
                        user.stockValue = Convert.ToDecimal(rdr["StockValue"]);
                    }
                    return user;
                }
                return null;
            }
        }

        public string GetAllStockDetails()
        {
            List<Stock> stockList = new List<Stock>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand(_spGetAllStockDetails, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Stock stock = new Stock();
                    stock.stockId = Convert.ToInt32(rdr["StockId"]);
                    stock.stockName = Convert.ToString(rdr["StockName"]);
                    stock.stockValue = Convert.ToDecimal(rdr["StockValue"]);
                    stock.stockTrend = Convert.ToDecimal(rdr["StockTrend"]);
                    stockList.Add(stock);
                }
            }
            return JsonConvert.SerializeObject(stockList);
        }

        public string GetUserStock(int userId)
        {
            List<StockHoldings> stockHoldingList = new List<StockHoldings>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand(_spGetStockIds, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    StockHoldings stock = new StockHoldings();
                    stock.stockId = Convert.ToInt32(rdr["StockId"]);
                    stock.userId = Convert.ToInt32(rdr["UserId"]);
                    stock.quantity = Convert.ToInt32(rdr["Quantity"]);
                    stockHoldingList.Add(stock);
                }
            }
            return JsonConvert.SerializeObject(stockHoldingList);
        }

        public Stock GetSingleStockDetails(int stockId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                Stock stock = new Stock();
                con.Open();
                SqlCommand cmd = new SqlCommand(_spGetStockDetails, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StockId", stockId);
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    stock.stockId = Convert.ToInt32(rdr["StockId"]);
                    stock.stockName = Convert.ToString(rdr["StockName"]);
                    stock.stockValue = Convert.ToDecimal(rdr["StockValue"]);
                    stock.stockTrend = Convert.ToDecimal(rdr["StockTrend"]);
                }
                return stock;
            }        
        }

        public int InsertOrUpdateTransaction(StockHoldings transaction)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(_spInsertOrUpdateTransaction, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", transaction.userId);
                cmd.Parameters.AddWithValue("@StockId", transaction.stockId);
                cmd.Parameters.AddWithValue("@Quantity", transaction.quantity);
                int rdr = cmd.ExecuteNonQuery();
                return rdr;
            }
        }

        public int UpdateUserDetails(int userId, decimal cashValue, decimal stockValue)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(_spUpdateUser, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@StockValue", stockValue);
                cmd.Parameters.AddWithValue("@CashValue", cashValue);
                int rdr = cmd.ExecuteNonQuery();
                return rdr;
            }
        }

        public int DecreaseQuantityTransaction(StockHoldings transaction)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(_spDecreaseQuantity, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", transaction.userId);
                cmd.Parameters.AddWithValue("@StockId", transaction.stockId);
                cmd.Parameters.AddWithValue("@Quantity", transaction.quantity);
                int rdr = cmd.ExecuteNonQuery();
                return rdr;
            }
        }
    }
}