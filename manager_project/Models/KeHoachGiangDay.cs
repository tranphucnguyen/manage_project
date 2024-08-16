using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class KeHoachGiangDay
    {
        public KeHoachGiangDay()
        {
            PhanCongs = new HashSet<PhanCong>();
            ThoiKhoaBieus = new HashSet<ThoiKhoaBieu>();
        }

        public int MaKeHoach { get; set; }
        public string? NamHoc { get; set; }
        public string? HocKy { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }

        public virtual ICollection<PhanCong> PhanCongs { get; set; }
        public virtual ICollection<ThoiKhoaBieu> ThoiKhoaBieus { get; set; }
    }
}
