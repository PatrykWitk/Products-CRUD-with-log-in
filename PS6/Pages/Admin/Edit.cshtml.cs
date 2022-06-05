using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PS6.Models;
namespace PS6.Pages.Admin
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Product EditProduct { get; set; }
        public List<Product> productList = new List<Product>();
        public List<Category> categoryList = new List<Category>();
        public IConfiguration _configuration { get; }
        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            string myCompanyDBcs = _configuration.GetConnectionString("myCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Category";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
        
            while (reader.Read())
            {
                categoryList.Add(new Category
                {
                    id = Int32.Parse(reader["Id"].ToString()),
                    shortName = reader.GetString(1),
                    longName = reader.GetString(2)
                });
            }

            reader.Close();
            con.Close();

        }
        public IActionResult OnPost()
        {
            int idOfProduct = Int32.Parse(HttpContext.Request.Query["id"]);

            string myCompanyDB_connection_string = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);
            SqlCommand cmd = new SqlCommand("sp_productEdit", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter name_SqlParam = new SqlParameter("@name", SqlDbType.VarChar, 50);
            name_SqlParam.Value = EditProduct.name;
            cmd.Parameters.Add(name_SqlParam);

            SqlParameter price_SqlParam = new SqlParameter("@price", SqlDbType.Money);
            price_SqlParam.Value = EditProduct.price;
            cmd.Parameters.Add(price_SqlParam);

            SqlParameter productID_SqlParam = new SqlParameter("@productID", SqlDbType.Int);
            productID_SqlParam.Value = idOfProduct;
            cmd.Parameters.Add(productID_SqlParam);

            SqlParameter categoryId_SqlParam = new SqlParameter("@categoryId",
           SqlDbType.Int);
            categoryId_SqlParam.Value = EditProduct.categoryId;
            cmd.Parameters.Add(categoryId_SqlParam);

            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToPage("/Index");
        }
    }
}