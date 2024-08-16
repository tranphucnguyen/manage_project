using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace manager_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViphamController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ViphamController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllViPhams")]
        public JsonResult GetAllViPhams()
        {
            string query = @"
            SELECT MaViPham, MaGiaoVien, LoaiViPham, MoTa, NgayViPham, vipham_status
            FROM dbo.ViPham";

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

        [HttpGet("{id}")]
        public JsonResult GetViPhamById(int id)
        {
            string query = @"
            SELECT MaViPham, MaGiaoVien, LoaiViPham, MoTa, NgayViPham, vipham_status
            FROM dbo.ViPham
            WHERE MaViPham = @MaViPham";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("manager-project");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MaViPham", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPut("UpdateViPhamStatus")]
        public JsonResult UpdateViPhamStatus([FromForm] int MaViPham, [FromForm] int vipham_status)
        {
            string query = @"
            UPDATE dbo.ViPham
            SET vipham_status = @vipham_status
            WHERE MaViPham = @MaViPham";

            string sqlDataSource = _configuration.GetConnectionString("manager-project");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MaViPham", MaViPham);
                    myCommand.Parameters.AddWithValue("@vipham_status", vipham_status);

                    try
                    {
                        myCommand.ExecuteNonQuery();
                        return new JsonResult("Status Updated Successfully");
                    }
                    catch (Exception ex)
                    {
                        return new JsonResult("Update failed: " + ex.Message);
                    }
                }
            }
        }

        [HttpDelete("{id}")]
        public JsonResult DeleteViPham(int id)
        {
            string query = @"
            DELETE FROM dbo.ViPham
            WHERE MaViPham = @MaViPham";

            string sqlDataSource = _configuration.GetConnectionString("manager-project");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MaViPham", id);

                    try
                    {
                        myCommand.ExecuteNonQuery();
                        return new JsonResult("Record Deleted Successfully");
                    }
                    catch (Exception ex)
                    {
                        return new JsonResult("Delete failed: " + ex.Message);
                    }
                }
            }
        }
    }
}
