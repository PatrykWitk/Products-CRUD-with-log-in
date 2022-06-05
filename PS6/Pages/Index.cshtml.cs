using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;
using PS6.Models;

namespace PS6.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Product> productList = new List<Product>();
        public IConfiguration _configuration { get; }

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT Product.Id, Product.name, Product.price, Category.longName  FROM Product INNER JOIN Category ON Product.categoryId=Category.Id";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                productList.Add(new Product
                {
                    id = Int32.Parse(reader["Id"].ToString()),
                    name = reader.GetString(1),
                    price = Decimal.Parse(String.Format("{0:0.00}", Decimal.Parse(reader["Price"].ToString()))),
                    desc = reader.GetString(3)
                });
            }
            reader.Close(); 
            con.Close();
        }

    }
}
