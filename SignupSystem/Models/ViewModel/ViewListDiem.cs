using System.Collections.Generic;

namespace SignupSystem.Models.ViewModel
{
    public class ViewListDiem
    {
        public string code_Subject { get; set; }
        public int id_Student { get; set; }
        public List<DiemKiemTraMieng> DiemKiemTraMiengs { get; set; }
        public List<DiemKiemTraMotTiet> diemKiemTraMotTiets { get; set; }
        public List<KiemTra15Phut> kiemTra15Phuts { get; set; }
        public List<DiemThiCuoiKi> diemThiCuoiKis { get; set; }
    }
}
