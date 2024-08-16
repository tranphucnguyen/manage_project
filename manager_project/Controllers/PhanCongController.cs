using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace manager_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanCongController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PhanCongController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetPhanCongs")]
        public JsonResult GetPhanCong(int MaGiaoVien, int? MaMonHoc = null, int? MaLopHoc = null)
        {
            // Xây dựng câu truy vấn SQL
            
            string query = @"
SELECT gv.MaGiaoVien, mh.TenMonHoc, lh.TenLopHoc, pc.MaMonHoc, pc.MaLopHoc
FROM PhanCong pc
JOIN GiaoVien gv ON pc.MaGiaoVien = gv.MaGiaoVien
JOIN MonHoc mh ON pc.MaMonHoc = mh.MaMonHoc
JOIN LopHoc lh ON pc.MaLopHoc = lh.MaLopHoc
WHERE pc.MaGiaoVien = @MaGiaoVien";


            // Thêm điều kiện vào câu truy vấn nếu MaMonHoc và MaLopHoc không null
            if (MaMonHoc.HasValue)
            {
                query += " AND pc.MaMonHoc = @MaMonHoc";
            }
            if (MaLopHoc.HasValue)
            {
                query += " AND pc.MaLopHoc = @MaLopHoc";
            }

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("manager-project");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MaGiaoVien", MaGiaoVien);
                    if (MaMonHoc.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@MaMonHoc", MaMonHoc.Value);
                    }
                    if (MaLopHoc.HasValue)
                    {
                        myCommand.Parameters.AddWithValue("@MaLopHoc", MaLopHoc.Value);
                    }

                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        table.Load(myReader);
                        myReader.Close();
                    }
                }
                myCon.Close();
            }
            return new JsonResult(table);
        }
    }
}
