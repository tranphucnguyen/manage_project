using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class NguoiDung
    {
        public int MaNguoiDung { get; set; }
        public string? TenDangNhap { get; set; }
        public string? MatKhau { get; set; }
        public int? IdVaiTro { get; set; }
        public int? IdGiaoVien { get; set; }
        public string? Email { get; set; }

        public virtual GiaoVien? IdGiaoVienNavigation { get; set; }
        public virtual VaiTro? IdVaiTroNavigation { get; set; }
    }
}
