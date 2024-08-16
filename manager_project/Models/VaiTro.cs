using System;
using System.Collections.Generic;

namespace manager_project.Models
{
    public partial class VaiTro
    {
        public VaiTro()
        {
            NguoiDungs = new HashSet<NguoiDung>();
        }

        public int IdVaiTro { get; set; }
        public string? TenVaiTro { get; set; }

        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
