using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class ChiTietBaoCao
    {
        public int MaChiTiet { get; set; }
        public int? MaBaoCao { get; set; }
        public string? NoiDungChiTiet { get; set; }
        public string? GhiChu { get; set; }

        public virtual BaoCaoGiangDay? MaBaoCaoNavigation { get; set; }
    }
}
