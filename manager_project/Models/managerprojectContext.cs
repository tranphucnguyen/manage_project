using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace manager_project.Models
{
    public partial class managerprojectContext : DbContext
    {
        public managerprojectContext()
        {
        }

        public managerprojectContext(DbContextOptions<managerprojectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BaoCaoGiangDay> BaoCaoGiangDays { get; set; } = null!;
        public virtual DbSet<ChatLuongGiangDay> ChatLuongGiangDays { get; set; } = null!;
        public virtual DbSet<ChiTietBaoCao> ChiTietBaoCaos { get; set; } = null!;
        public virtual DbSet<GiaoVien> GiaoViens { get; set; } = null!;
        public virtual DbSet<KeHoachGiangDay> KeHoachGiangDays { get; set; } = null!;
        public virtual DbSet<LopHoc> LopHocs { get; set; } = null!;
        public virtual DbSet<MonHoc> MonHocs { get; set; } = null!;
        public virtual DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public virtual DbSet<PhanCong> PhanCongs { get; set; } = null!;
        public virtual DbSet<ThoiKhoaBieu> ThoiKhoaBieus { get; set; } = null!;
        public virtual DbSet<VaiTro> VaiTros { get; set; } = null!;
        public virtual DbSet<ViPham> ViPhams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=manager-project;User Id=sa;Password=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaoCaoGiangDay>(entity =>
            {
                entity.HasKey(e => e.MaBaoCao)
                    .HasName("PK__BaoCaoGi__25A9188C2913003C");

                entity.ToTable("BaoCaoGiangDay");

                entity.Property(e => e.NgayBaoCao).HasColumnType("date");

                entity.Property(e => e.TieuDe).HasMaxLength(100);

                entity.HasOne(d => d.MaGiaoVienNavigation)
                    .WithMany(p => p.BaoCaoGiangDays)
                    .HasForeignKey(d => d.MaGiaoVien)
                    .HasConstraintName("FK__BaoCaoGia__MaGia__59063A47");
            });

            modelBuilder.Entity<ChatLuongGiangDay>(entity =>
            {
                entity.HasKey(e => e.MaChatLuong)
                    .HasName("PK__ChatLuon__B2F4411E2F5EB7B4");

                entity.ToTable("ChatLuongGiangDay");

                entity.Property(e => e.DanhGia).HasMaxLength(255);

                entity.Property(e => e.NgayDanhGia).HasColumnType("date");

                entity.HasOne(d => d.MaGiaoVienNavigation)
                    .WithMany(p => p.ChatLuongGiangDays)
                    .HasForeignKey(d => d.MaGiaoVien)
                    .HasConstraintName("FK__ChatLuong__MaGia__534D60F1");
            });

            modelBuilder.Entity<ChiTietBaoCao>(entity =>
            {
                entity.HasKey(e => e.MaChiTiet)
                    .HasName("PK__ChiTietB__CDF0A1143CCE81D0");

                entity.ToTable("ChiTietBaoCao");

                entity.Property(e => e.GhiChu).HasMaxLength(255);

                entity.HasOne(d => d.MaBaoCaoNavigation)
                    .WithMany(p => p.ChiTietBaoCaos)
                    .HasForeignKey(d => d.MaBaoCao)
                    .HasConstraintName("FK__ChiTietBa__MaBao__5BE2A6F2");
            });

            modelBuilder.Entity<GiaoVien>(entity =>
            {
                entity.HasKey(e => e.MaGiaoVien)
                    .HasName("PK__GiaoVien__8D374F50281211FC");

                entity.ToTable("GiaoVien");

                entity.Property(e => e.DiaChi).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.GioiTinh).HasMaxLength(10);

                entity.Property(e => e.HoTen).HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.NgayVaoTruong).HasColumnType("date");

                entity.Property(e => e.SoDienThoai).HasMaxLength(15);
            });

            modelBuilder.Entity<KeHoachGiangDay>(entity =>
            {
                entity.HasKey(e => e.MaKeHoach)
                    .HasName("PK__KeHoachG__88C5741F1235B7D8");

                entity.ToTable("KeHoachGiangDay");

                entity.Property(e => e.HocKy).HasMaxLength(50);

                entity.Property(e => e.NamHoc).HasMaxLength(50);

                entity.Property(e => e.NgayBatDau).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");
            });

            modelBuilder.Entity<LopHoc>(entity =>
            {
                entity.HasKey(e => e.MaLopHoc)
                    .HasName("PK__LopHoc__FEE0578457EAE3E0");

                entity.ToTable("LopHoc");

                entity.Property(e => e.PhongHoc).HasMaxLength(50);

                entity.Property(e => e.TenLopHoc).HasMaxLength(100);
            });

            modelBuilder.Entity<MonHoc>(entity =>
            {
                entity.HasKey(e => e.MaMonHoc)
                    .HasName("PK__MonHoc__4127737FE34923E0");

                entity.ToTable("MonHoc");

                entity.Property(e => e.TenMonHoc).HasMaxLength(100);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.MaNguoiDung)
                    .HasName("PK__NguoiDun__C539D76221A7075E");

                entity.ToTable("NguoiDung");

                entity.HasIndex(e => e.TenDangNhap, "UQ__NguoiDun__55F68FC0824BE333")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.MatKhau).HasMaxLength(255);

                entity.Property(e => e.TenDangNhap).HasMaxLength(50);

                entity.HasOne(d => d.IdGiaoVienNavigation)
                    .WithMany(p => p.NguoiDungs)
                    .HasForeignKey(d => d.IdGiaoVien)
                    .HasConstraintName("FK__NguoiDung__IdGia__3F466844");

                entity.HasOne(d => d.IdVaiTroNavigation)
                    .WithMany(p => p.NguoiDungs)
                    .HasForeignKey(d => d.IdVaiTro)
                    .HasConstraintName("FK__NguoiDung__IdVai__3E52440B");
            });

            modelBuilder.Entity<PhanCong>(entity =>
            {
                entity.HasKey(e => e.MaPhanCong)
                    .HasName("PK__PhanCong__C279D916BF09E0B3");

                entity.ToTable("PhanCong");

                entity.Property(e => e.NgayPhanCong).HasColumnType("date");

                entity.HasOne(d => d.MaGiaoVienNavigation)
                    .WithMany(p => p.PhanCongs)
                    .HasForeignKey(d => d.MaGiaoVien)
                    .HasConstraintName("FK__PhanCong__MaGiao__47DBAE45");

                entity.HasOne(d => d.MaKeHoachNavigation)
                    .WithMany(p => p.PhanCongs)
                    .HasForeignKey(d => d.MaKeHoach)
                    .HasConstraintName("FK__PhanCong__MaKeHo__4AB81AF0");

                entity.HasOne(d => d.MaLopHocNavigation)
                    .WithMany(p => p.PhanCongs)
                    .HasForeignKey(d => d.MaLopHoc)
                    .HasConstraintName("FK__PhanCong__MaLopH__48CFD27E");

                entity.HasOne(d => d.MaMonHocNavigation)
                    .WithMany(p => p.PhanCongs)
                    .HasForeignKey(d => d.MaMonHoc)
                    .HasConstraintName("FK__PhanCong__MaMonH__49C3F6B7");
            });

            modelBuilder.Entity<ThoiKhoaBieu>(entity =>
            {
                entity.HasKey(e => e.MaThoiKhoaBieu)
                    .HasName("PK__ThoiKhoa__EDE8C869FF15663D");

                entity.ToTable("ThoiKhoaBieu");

                entity.Property(e => e.Ngay).HasColumnType("date");

                entity.HasOne(d => d.MaGiaoVienNavigation)
                    .WithMany(p => p.ThoiKhoaBieus)
                    .HasForeignKey(d => d.MaGiaoVien)
                    .HasConstraintName("FK__ThoiKhoaB__MaGia__4F7CD00D");

                entity.HasOne(d => d.MaKeHoachNavigation)
                    .WithMany(p => p.ThoiKhoaBieus)
                    .HasForeignKey(d => d.MaKeHoach)
                    .HasConstraintName("FK__ThoiKhoaB__MaKeH__5070F446");

                entity.HasOne(d => d.MaLopHocNavigation)
                    .WithMany(p => p.ThoiKhoaBieus)
                    .HasForeignKey(d => d.MaLopHoc)
                    .HasConstraintName("FK__ThoiKhoaB__MaLop__4D94879B");

                entity.HasOne(d => d.MaMonHocNavigation)
                    .WithMany(p => p.ThoiKhoaBieus)
                    .HasForeignKey(d => d.MaMonHoc)
                    .HasConstraintName("FK__ThoiKhoaB__MaMon__4E88ABD4");
            });

            modelBuilder.Entity<VaiTro>(entity =>
            {
                entity.HasKey(e => e.IdVaiTro)
                    .HasName("PK__VaiTro__491B115CA128F74C");

                entity.ToTable("VaiTro");

                entity.Property(e => e.TenVaiTro).HasMaxLength(50);
            });

            modelBuilder.Entity<ViPham>(entity =>
            {
                entity.HasKey(e => e.MaViPham)
                    .HasName("PK__ViPham__F1921D89198AAA1C");

                entity.ToTable("ViPham");

                entity.Property(e => e.LoaiViPham).HasMaxLength(100);

                entity.Property(e => e.MoTa).HasMaxLength(255);

                entity.Property(e => e.NgayViPham).HasColumnType("date");

                entity.HasOne(d => d.MaGiaoVienNavigation)
                    .WithMany(p => p.ViPhams)
                    .HasForeignKey(d => d.MaGiaoVien)
                    .HasConstraintName("FK__ViPham__MaGiaoVi__5629CD9C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
