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
    public class AddCategoryModel : PageModel
    {
        [BindProperty]
        public Category NewCategory { get; set; }
        public List<Category> CategoryList = new List<Category>();
        public IConfiguration _configuration { get; }
        public AddCategoryModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult OnPost()
        {
            string myCompanyDB_connection_string = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);
            SqlCommand cmd = new SqlCommand("sp_categoryAdd", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter shortName_SqlParam = new SqlParameter("@shortName", SqlDbType.VarChar, 50);
            shortName_SqlParam.Value = NewCategory.shortName;
            cmd.Parameters.Add(shortName_SqlParam);

            SqlParameter longName_SqlParam = new SqlParameter("@longName", SqlDbType.VarChar, 50);
            longName_SqlParam.Value = NewCategory.longName;
            cmd.Parameters.Add(longName_SqlParam);

            SqlParameter categoryID_SqlParam = new SqlParameter("@categoryID", SqlDbType.Int);
            categoryID_SqlParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(categoryID_SqlParam);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            int categoryID = (int)cmd.Parameters["@categoryID"].Value;

            return RedirectToPage("Index");
        }
    }
}