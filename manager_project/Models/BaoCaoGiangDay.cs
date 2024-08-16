using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class BaoCaoGiangDay
    {
        public BaoCaoGiangDay()
        {
            ChiTietBaoCaos = new HashSet<ChiTietBaoCao>();
        }

        public int MaBaoCao { get; set; }
        public int? MaGiaoVien { get; set; }
        public string? TieuDe { get; set; }
        public string? NoiDung { get; set; }
        public DateTime? NgayBaoCao { get; set; }

        public virtual GiaoVien? MaGiaoVienNavigation { get; set; }
        public virtual ICollection<ChiTietBaoCao> ChiTietBaoCaos { get; set; }
    }
}
