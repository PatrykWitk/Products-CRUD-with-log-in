using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PS6.Models;
using Microsoft.Extensions.Logging;
using System.Data;

namespace PS6.Pages
{
    public class DeleteCategoryModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IConfiguration _configuration { get; }

        public DeleteCategoryModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            int idOfCategory = Int32.Parse(HttpContext.Request.Query["id"]);

            string myCompanyDB_connection_string = _configuration.GetConnectionString("myCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDB_connection_string);
            SqlCommand cmd = new SqlCommand("sp_categoryDelete", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter categoryID_SqlParam = new SqlParameter("@categoryID", SqlDbType.Int);
            categoryID_SqlParam.Value = idOfCategory;
            cmd.Parameters.Add(categoryID_SqlParam);

            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToPage("Index");
        }
    }
}

