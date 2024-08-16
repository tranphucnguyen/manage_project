using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class MonHoc
    {
        public MonHoc()
        {
            PhanCongs = new HashSet<PhanCong>();
            ThoiKhoaBieus = new HashSet<ThoiKhoaBieu>();
        }

        public int MaMonHoc { get; set; }
        public string? TenMonHoc { get; set; }

        public virtual ICollection<PhanCong> PhanCongs { get; set; }
        public virtual ICollection<ThoiKhoaBieu> ThoiKhoaBieus { get; set; }
    }
}
