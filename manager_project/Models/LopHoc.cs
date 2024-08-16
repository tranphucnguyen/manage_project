using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class LopHoc
    {
        public LopHoc()
        {
            PhanCongs = new HashSet<PhanCong>();
            ThoiKhoaBieus = new HashSet<ThoiKhoaBieu>();
        }

        public int MaLopHoc { get; set; }
        public string? TenLopHoc { get; set; }
        public int? SoLuongHocSinh { get; set; }
        public string? PhongHoc { get; set; }

        public virtual ICollection<PhanCong> PhanCongs { get; set; }
        public virtual ICollection<ThoiKhoaBieu> ThoiKhoaBieus { get; set; }
    }
}
