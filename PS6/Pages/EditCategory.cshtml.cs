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
namespace PS6.Pages
{
    public class EditCategoryModel : PageModel
    {
        [BindProperty]
        public Category EditCategory { get; set; }
        public List<Category> categoryList = new List<Category>();
        public IConfiguration _configuration { get; }
        public EditCategoryModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnPost()
        {
            int idOfCategory = Int32.Parse(HttpContext.Request.Query["id"]);

            string myCompanyDB_connection_string = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);
            SqlCommand cmd = new SqlCommand("sp_categoryEdit", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter shortName_SqlParam = new SqlParameter("@shortName", SqlDbType.VarChar, 10);
            shortName_SqlParam.Value = EditCategory.shortName;
            cmd.Parameters.Add(shortName_SqlParam);

            SqlParameter longName_SqlParam = new SqlParameter("@longName", SqlDbType.VarChar, 50);
            longName_SqlParam.Value = EditCategory.longName;
            cmd.Parameters.Add(longName_SqlParam);

            SqlParameter categoryID_SqlParam = new SqlParameter("@categoryID", SqlDbType.Int);
            categoryID_SqlParam.Value = idOfCategory;
            cmd.Parameters.Add(categoryID_SqlParam);

            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            int categoryID = (int)cmd.Parameters["@categoryID"].Value;

            return RedirectToPage("Index");
        }
    }
}