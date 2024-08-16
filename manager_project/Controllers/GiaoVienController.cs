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
    public class GiaoVienController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GiaoVienController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetGiaoViens")]
        public JsonResult GetGiaoViens()
        {
            string query = @"
                SELECT gv.* 
                FROM dbo.GiaoVien gv
                JOIN dbo.NguoiDung nd ON gv.MaGiaoVien = nd.IdGiaoVien
                WHERE nd.IdVaiTro = 2";
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
        [Route("AddGiaoVien")]
        public JsonResult AddGiaoVien([FromForm] string HoTen, [FromForm] DateTime NgaySinh, [FromForm] string GioiTinh, [FromForm] string SoDienThoai, [FromForm] string Email, [FromForm] string DiaChi, [FromForm] DateTime NgayVaoTruong)
        {
            string queryGiaoVien = @"
        INSERT INTO dbo.GiaoVien (HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, NgayVaoTruong)
        VALUES (@HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @Email, @DiaChi, @NgayVaoTruong);
        SELECT CAST(scope_identity() AS int)";

            string queryNguoiDung = @"
        INSERT INTO dbo.NguoiDung (IdGiaoVien, TenDangNhap, MatKhau, IdVaiTro, Email)
        VALUES (@IdGiaoVien, @TenDangNhap, @MatKhau, @IdVaiTro, @Email);
        SELECT CAST(scope_identity() AS int)";

            string queryCheckVaiTro = @"
        SELECT IdVaiTro FROM dbo.VaiTro WHERE TenVaiTro = @TenVaiTro";

            string queryInsertVaiTro = @"
        INSERT INTO dbo.VaiTro (TenVaiTro) VALUES (@TenVaiTro);
        SELECT CAST(scope_identity() AS int)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("manager-project");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlTransaction transaction = myCon.BeginTransaction())
                {
                    try
                    {
                        int idVaiTro = 2; // Mặc định là 2 cho vai trò Giáo viên

                        // Kiểm tra xem vai trò Giáo Viên có tồn tại trong bảng VaiTro không
                        using (SqlCommand checkVaiTroCommand = new SqlCommand(queryCheckVaiTro, myCon, transaction))
                        {
                            checkVaiTroCommand.Parameters.AddWithValue("@TenVaiTro", "GiaoVien");
                            var result = checkVaiTroCommand.ExecuteScalar();

                            if (result != null) //
                            {
                                idVaiTro = (int)result; // Sử dụng giá trị đã tồn tại
                            }
                            else
                            {
                                // Nếu vai trò Giáo Viên không tồn tại, thêm nó vào bảng VaiTro
                                using (SqlCommand insertVaiTroCommand = new SqlCommand(queryInsertVaiTro, myCon, transaction))
                                {
                                    insertVaiTroCommand.Parameters.AddWithValue("@TenVaiTro", "GiaoVien");
                                    idVaiTro = (int)insertVaiTroCommand.ExecuteScalar();
                                }
                            }
                        }

                        int newGiaoVienId;
                        // Thêm thông tin Giáo viên vào bảng GiaoVien
                        using (SqlCommand myCommand = new SqlCommand(queryGiaoVien, myCon, transaction))
                        {
                            myCommand.Parameters.AddWithValue("@HoTen", HoTen);
                            myCommand.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                            myCommand.Parameters.AddWithValue("@GioiTinh", GioiTinh);
                            myCommand.Parameters.AddWithValue("@SoDienThoai", SoDienThoai);
                            myCommand.Parameters.AddWithValue("@Email", Email);
                            myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                            myCommand.Parameters.AddWithValue("@NgayVaoTruong", NgayVaoTruong);
                            newGiaoVienId = (int)myCommand.ExecuteScalar();
                        }

                        int newNguoiDungId;
                        // Thêm thông tin Người dùng vào bảng NguoiDung
                        using (SqlCommand myCommand = new SqlCommand(queryNguoiDung, myCon, transaction))
                        {
                            myCommand.Parameters.AddWithValue("@IdGiaoVien", newGiaoVienId);
                            myCommand.Parameters.AddWithValue("@TenDangNhap", HoTen); // Sử dụng Tên giáo viên làm Tên đăng nhập
                            myCommand.Parameters.AddWithValue("@MatKhau", newGiaoVienId); // Sử dụng Mã giáo viên làm mật khẩu
                            myCommand.Parameters.AddWithValue("@IdVaiTro", idVaiTro); // Sử dụng IdVaiTro từ bảng VaiTro
                            myCommand.Parameters.AddWithValue("@Email", Email); // Thêm Email vào bảng NguoiDung

                            newNguoiDungId = (int)myCommand.ExecuteScalar();
                        }

                        // Cập nhật lại IdVaiTro của Người dùng mới thêm vào
                        using (SqlCommand updateNguoiDungCommand = new SqlCommand("UPDATE dbo.NguoiDung SET IdVaiTro = @IdVaiTro WHERE MaNguoiDung = @MaNguoiDung", myCon, transaction))
                        {
                            updateNguoiDungCommand.Parameters.AddWithValue("@IdVaiTro", idVaiTro);
                            updateNguoiDungCommand.Parameters.AddWithValue("@MaNguoiDung", newNguoiDungId);
                            updateNguoiDungCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return new JsonResult("Added Successfully");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new JsonResult("Add failed: " + ex.Message);
                    }
                }
            }
        }


        [HttpDelete("{id}")]
        public JsonResult DeleteGiaoVien(int id)
        {
            string queryDeleteViPham = "DELETE FROM dbo.ViPham WHERE MaGiaoVien = @MaGiaoVien";
            string queryDeleteNguoiDung = "DELETE FROM dbo.NguoiDung WHERE IdGiaoVien = @IdGiaoVien";
            string queryDeleteGiaoVien = "DELETE FROM dbo.GiaoVien WHERE MaGiaoVien = @MaGiaoVien";
            string sqlDataSource = _configuration.GetConnectionString("manager-project");

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlTransaction transaction = myCon.BeginTransaction())
                {
                    try
                    {
                        // Xóa các bản ghi liên quan trong bảng ViPham
                        using (SqlCommand myCommand = new SqlCommand(queryDeleteViPham, myCon, transaction))
                        {
                            myCommand.Parameters.AddWithValue("@MaGiaoVien", id);
                            myCommand.ExecuteNonQuery();
                        }
                        using (SqlCommand myCommand = new SqlCommand(queryDeleteNguoiDung, myCon, transaction))
                        {
                            myCommand.Parameters.AddWithValue("@IdGiaoVien", id);
                            myCommand.ExecuteNonQuery();
                        }

                        using (SqlCommand myCommand = new SqlCommand(queryDeleteGiaoVien, myCon, transaction))
                        {
                            myCommand.Parameters.AddWithValue("@MaGiaoVien", id);
                            myCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return new JsonResult("Delete failed: " + ex.Message);
                    }
                }
                myCon.Close();
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        [Route("UpdateGiaoVien")]
        public JsonResult UpdateGiaoVien([FromForm] int MaGiaoVien, [FromForm] string HoTen, [FromForm] DateTime NgaySinh, [FromForm] string GioiTinh, [FromForm] string SoDienThoai, [FromForm] string Email, [FromForm] string DiaChi, [FromForm] DateTime NgayVaoTruong)
        {
            string query = @"
                UPDATE dbo.GiaoVien 
                SET HoTen = @HoTen, NgaySinh = @NgaySinh, GioiTinh = @GioiTinh, SoDienThoai = @SoDienThoai, 
                    Email = @Email, DiaChi = @DiaChi, NgayVaoTruong = @NgayVaoTruong 
                WHERE MaGiaoVien = @MaGiaoVien";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("manager-project");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MaGiaoVien", MaGiaoVien);
                    myCommand.Parameters.AddWithValue("@HoTen", HoTen);
                    myCommand.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                    myCommand.Parameters.AddWithValue("@GioiTinh", GioiTinh);
                    myCommand.Parameters.AddWithValue("@SoDienThoai", SoDienThoai);
                    myCommand.Parameters.AddWithValue("@Email", Email);
                    myCommand.Parameters.AddWithValue("@DiaChi", DiaChi);
                    myCommand.Parameters.AddWithValue("@NgayVaoTruong", NgayVaoTruong);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
    }
}
