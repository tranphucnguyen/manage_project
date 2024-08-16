using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace manager_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaitroController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public VaitroController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/VaiTro
        [HttpGet]
        public IActionResult GetVaiTros()
        {
            string query = "SELECT * FROM dbo.VaiTro";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("manager-project");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    SqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return Ok(table);
        }

        // GET: api/VaiTro/5
        [HttpGet("{id}")]
        public IActionResult GetVaiTro(int id)
        {
            string query = "SELECT * FROM dbo.VaiTro WHERE IdVaiTro = @IdVaiTro";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("manager-project");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdVaiTro", id);
                    SqlDataReader myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            if (table.Rows.Count == 0)
            {
                return NotFound();
            }

            // Mapping IdVaiTro to role name
            string roleName;
            switch (id)
            {
                case 1:
                    roleName = "Quanlygiaoduc";
                    break;
                case 2:
                    roleName = "Giaovien";
                    break;
                default:
                    roleName = "Unknown";
                    break;
            }

            // Optionally, you can return the role name instead of the table row
            return Ok(new { RoleName = roleName });
        }
    }
}
