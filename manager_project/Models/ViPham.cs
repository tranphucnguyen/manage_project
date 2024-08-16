using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class ViPham
    {
        public int MaViPham { get; set; }
        public int? MaGiaoVien { get; set; }
        public string? LoaiViPham { get; set; }
        public string? MoTa { get; set; }
        public DateTime? NgayViPham { get; set; }

        public virtual GiaoVien? MaGiaoVienNavigation { get; set; }
    }
}
