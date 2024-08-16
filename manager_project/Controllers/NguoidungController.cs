using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace manager_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NguoidungController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public NguoidungController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetNguoidung")]
        public JsonResult GetNguoidung()
        {
            string query = "SELECT * FROM dbo.Nguoidung";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("manager-project");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string matKhau)
        {
            string query = @"
                SELECT * FROM dbo.Nguoidung 
                WHERE Email = @Email AND MatKhau = @MatKhau";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("manager-project");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                await myCon.OpenAsync();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Email", email);
                    myCommand.Parameters.AddWithValue("@MatKhau", matKhau);
                    myReader = await myCommand.ExecuteReaderAsync();
                    table.Load(myReader);
                    await myReader.CloseAsync();
                    await myCon.CloseAsync();
                }
            }

            if (table.Rows.Count > 0)
            {
                var user = table.Rows[0];
                int idGiaoVien = user.Field<int?>("IdGiaoVien") ?? 0;
                int idVaiTro = user.Field<int>("IdVaiTro");

                // Kiểm tra xem IdGiaoVien có tồn tại trong bảng GiaoVien không
                if (await CheckIfGiaoVienExists(idGiaoVien))
                {
                    return new JsonResult(new { message = "Login successful", user, IdVaiTro = idVaiTro });
                }
                else
                {
                    return new JsonResult(new { message = "Invalid GiaoVien" });
                }
            }

            return new JsonResult(new { message = "Invalid credentials" });
        }

        private async Task<bool> CheckIfGiaoVienExists(int idGiaoVien)
        {
            string query = "SELECT COUNT(1) FROM dbo.GiaoVien WHERE MaGiaoVien = @IdGiaoVien";
            string sqlDataSource = _configuration.GetConnectionString("manager-project");
            bool exists = false;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                await myCon.OpenAsync();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdGiaoVien", idGiaoVien);
                    exists = (int)await myCommand.ExecuteScalarAsync() > 0;
                }
                await myCon.CloseAsync();
            }

            return exists;
        }
    }
}
