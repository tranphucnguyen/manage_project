using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class PhanCong
    {
        public int MaPhanCong { get; set; }
        public int? MaGiaoVien { get; set; }
        public int? MaLopHoc { get; set; }
        public int? MaMonHoc { get; set; }
        public int? SoTiet { get; set; }
        public DateTime? NgayPhanCong { get; set; }
        public int? MaKeHoach { get; set; }

        public virtual GiaoVien? MaGiaoVienNavigation { get; set; }
        public virtual KeHoachGiangDay? MaKeHoachNavigation { get; set; }
        public virtual LopHoc? MaLopHocNavigation { get; set; }
        public virtual MonHoc? MaMonHocNavigation { get; set; }
    }
}
