using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class ChatLuongGiangDay
    {
        public int MaChatLuong { get; set; }
        public int? MaGiaoVien { get; set; }
        public string? DanhGia { get; set; }
        public DateTime? NgayDanhGia { get; set; }

        public virtual GiaoVien? MaGiaoVienNavigation { get; set; }
    }
}
