using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace manager_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThoiKhoaBieuController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ThoiKhoaBieuController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetChiTiet")]
        public JsonResult GetChiTiet(int MaGiaoVien, int MaMonHoc, int MaLopHoc)
        {
            string query = @"
            SELECT 
                tkb.MaThoiKhoaBieu,
                gv.HoTen,
                lh.TenLopHoc,
                mh.TenMonHoc,
                tkb.Ngay,
                tkb.ThoiGianBatDau,
                tkb.ThoiGianKetThuc,
                tkb.MaKeHoach,
                tkb.MaLopHoc,
                tkb.MaMonHoc,
                tkb.MaGiaoVien
            FROM ThoiKhoaBieu tkb
            JOIN GiaoVien gv ON tkb.MaGiaoVien = gv.MaGiaoVien
            JOIN LopHoc lh ON tkb.MaLopHoc = lh.MaLopHoc
            JOIN MonHoc mh ON tkb.MaMonHoc = mh.MaMonHoc
            WHERE tkb.MaGiaoVien = @MaGiaoVien 
              AND tkb.MaMonHoc = @MaMonHoc 
              AND tkb.MaLopHoc = @MaLopHoc";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("manager-project");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MaGiaoVien", MaGiaoVien);
                    myCommand.Parameters.AddWithValue("@MaMonHoc", MaMonHoc);
                    myCommand.Parameters.AddWithValue("@MaLopHoc", MaLopHoc);

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
