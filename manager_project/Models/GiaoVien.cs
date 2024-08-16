using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class GiaoVien
    {
        public GiaoVien()
        {
            BaoCaoGiangDays = new HashSet<BaoCaoGiangDay>();
            ChatLuongGiangDays = new HashSet<ChatLuongGiangDay>();
            NguoiDungs = new HashSet<NguoiDung>();
            PhanCongs = new HashSet<PhanCong>();
            ThoiKhoaBieus = new HashSet<ThoiKhoaBieu>();
            ViPhams = new HashSet<ViPham>();
        }

        public int MaGiaoVien { get; set; }
        public string? HoTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? GioiTinh { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public DateTime? NgayVaoTruong { get; set; }

        public virtual ICollection<BaoCaoGiangDay> BaoCaoGiangDays { get; set; }
        public virtual ICollection<ChatLuongGiangDay> ChatLuongGiangDays { get; set; }
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
        public virtual ICollection<PhanCong> PhanCongs { get; set; }
        public virtual ICollection<ThoiKhoaBieu> ThoiKhoaBieus { get; set; }
        public virtual ICollection<ViPham> ViPhams { get; set; }
    }
}
