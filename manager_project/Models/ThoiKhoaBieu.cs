using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class ThoiKhoaBieu
    {
        public int MaThoiKhoaBieu { get; set; }
        public int? MaLopHoc { get; set; }
        public int? MaMonHoc { get; set; }
        public int? MaGiaoVien { get; set; }
        public DateTime? Ngay { get; set; }
        public int? MaKeHoach { get; set; }
        public TimeSpan? ThoiGianBatDau { get; set; }
        public TimeSpan? ThoiGianKetThuc { get; set; }

        public virtual GiaoVien? MaGiaoVienNavigation { get; set; }
        public virtual KeHoachGiangDay? MaKeHoachNavigation { get; set; }
        public virtual LopHoc? MaLopHocNavigation { get; set; }
        public virtual MonHoc? MaMonHocNavigation { get; set; }
    }
}
