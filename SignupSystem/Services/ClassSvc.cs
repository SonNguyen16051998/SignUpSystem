using SignupSystem.Models;
using SignupSystem.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface IClass
    {
        Task<List<Class>> GetClassesAsync();//lay toan bo lop hoc
        Task<Class> GetClassAsync(int id_Class);//lay mot lop hoc
        Task<bool> AddClassAsync(Class entity);//them lop hoc
        Task<bool> UpdateClassAsync(Class entity);//cap nhat lop hoc
        Task<bool> AddPointAsync(Student_Point student_Point);//them diem cho mot hoc sinh
        Task<bool> AddPointsAsync(ViewAddPoint addListPoint); //them diem cho nhieu hoc sinh
        Task<List<ViewListDiem>> GetStudent_PointsAsync(string code_Subject);//lay toan bo diem cua hoc vien
        /*Task<bool> UpdateStudent_PointAsync(List<ViewListDiem> listDiem);//chinh sua diem*/
        Task<bool> BlockPointAsync(List<Student_Point> entity);//chot diem
        Task<List<Student_Class>> GetStudent_ClassesAsync(int id_Class);//lay toan bo  hoc vien cua lop hoc
        Task<List<ViewListDiem>> GetListDiemByStudentAsync(int studentId);//lay diem cua mot hoc vien
        Task<List<TeacherSchedule>> GetTeacherSchedulesAsync(int id_Class);//lay toan bo mon hoc cua lop hoc
        Task<bool> DeleteSubjectInClassAsync(DeleteSubject subject);//xoa mon hoc khoi lop hoc
        Task<bool> DeleteClassAsync(int id_Class);//xoas lop hoc
        Task<bool> CheckDeleteClassAsync(int id_Class);//kiem tra lop hoc co duoc xoa hay khong
    }
    public class ClassSvc:IClass
    {
        private readonly DataContext _context;
        public ClassSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Class>> GetClassesAsync()
        {
            List<Class> lophoc = new List<Class>();
            lophoc = await _context.Classes
                .Include(x=>x.Course)
                .Include(x=>x.khoa)
                .ToListAsync();
            return lophoc;
        }
        public async Task<Class> GetClassAsync(int id_Class)
        {
            return await _context.Classes.Where(x=>x.Id_Class==id_Class)
                .Include(x => x.Course)
                .Include(x => x.khoa)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> AddClassAsync(Class entity)
        {
            bool ret = false;
            try
            {
                await _context.Classes.AddAsync(entity);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> UpdateClassAsync(Class entity)
        {
            bool ret = false;
            try
            {
                _context.Classes.Update(entity);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> AddPointAsync(Student_Point student_Point)
        {
            bool ret = false;
            try
            {
                await _context.Student_Points.AddAsync(student_Point);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> AddPointsAsync(ViewAddPoint addListPoint)
        {
            bool ret = false;
            try
            {
                Student_Point student_Point = new Student_Point();
                foreach(var item in addListPoint.PointStudent)
                {
                    student_Point.Id_PointType = addListPoint.Id_PointType;
                    student_Point.Code_Subject = addListPoint.Code_Subject;
                    student_Point.Id_Student = item.Id_Student;
                    student_Point.Point = item.Point;
                    student_Point.IsBlock = false;
                    await _context.Student_Points.AddAsync(student_Point);
                }
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<List<ViewListDiem>> GetStudent_PointsAsync(string code_Subject)
        {
            List<Student_Point> student_Points = new List<Student_Point>();
            student_Points=await _context.Student_Points
                         .Where(x=>x.Code_Subject==code_Subject)  
                         .Include(x=>x.Student)
                         .ToListAsync();
            var GroupBy = student_Points.GroupBy(x=>x.Id_Student).Select(g=>new
            {
                id_Student=g.Key
            });
            var Diem15phut = student_Points.Where(x => x.Id_PointType == 1).ToList();
            var Diemmottiet=student_Points.Where(x => x.Id_PointType==2).ToList();
            var Diemcuoiky=student_Points.Where(x => x.Id_PointType==3).ToList();
            var Diemkiemtramieng=student_Points.Where(x => x.Id_PointType==4).ToList();
            List<DiemKiemTraMieng> diemmieng = new List<DiemKiemTraMieng>();
            List<DiemKiemTraMotTiet> diemmottiet = new List<DiemKiemTraMotTiet>();
            List<KiemTra15Phut> diem15phut = new List<KiemTra15Phut>();
            List<DiemThiCuoiKi> diemthicuoiki = new List<DiemThiCuoiKi>();
            List<ViewListDiem> viewListDiems = new List<ViewListDiem>();
            foreach (var item in GroupBy)
            {
                var diemmiengStudent= Diemkiemtramieng.Where(x=>x.Id_Student==item.id_Student).ToList();
                foreach(var student in diemmiengStudent)
                {
                    diemmieng.Add(new DiemKiemTraMieng
                    {
                        Id_point=student.Id_Point,
                        point=student.Point
                    });
                }
                var diem15Phut = Diem15phut.Where(x => x.Id_Student == item.id_Student).ToList();
                foreach (var student in diem15Phut)
                {
                    diem15phut.Add(new KiemTra15Phut
                    {
                        Id_point = student.Id_Point,
                        point = student.Point
                    });
                }
                var diemMotTiet = Diemmottiet.Where(x => x.Id_Student == item.id_Student).ToList();
                foreach (var student in diemMotTiet)
                {
                    diemmottiet.Add(new DiemKiemTraMotTiet
                    {
                        Id_point = student.Id_Point,
                        point = student.Point
                    });
                }
                var diemCuoiKi = Diemcuoiky.Where(x => x.Id_Student == item.id_Student).ToList();
                foreach (var student in diemCuoiKi)
                {
                    diemthicuoiki.Add(new DiemThiCuoiKi
                    {
                        Id_point = student.Id_Point,
                        point = student.Point
                    });
                }
                viewListDiems.Add(new ViewListDiem
                {
                    id_Student = item.id_Student,
                    code_Subject = code_Subject,
                    kiemTra15Phuts= diem15phut,
                    DiemKiemTraMiengs=diemmieng,
                    diemKiemTraMotTiets= diemmottiet,
                    diemThiCuoiKis= diemthicuoiki
                });
            }
            return viewListDiems;
        }
        /*public async Task<bool> UpdateStudent_PointAsync(List<ViewListDiem> listDiem)
        {
            bool ret = false;
            try
            {
                Student_Point student_Point = new Student_Point();
               foreach(var item in listDiem)
               {
                    
               }
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }*/
        public async Task<bool> BlockPointAsync(List<Student_Point> entity)
        {
            bool ret = false;
            try
            {
                foreach(var student in entity)
                {
                    student.IsBlock = true;
                    _context.Student_Points.Update(student);
                }
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<List<ViewListDiem>> GetListDiemByStudentAsync(int id_Student)
        {
            List<Student_Point> student_Points = new List<Student_Point>();
            student_Points = await _context.Student_Points
                         .Where(x => x.Id_Student == id_Student)
                         .Include(x => x.Student)
                         .ToListAsync();
            var Diem15phut = student_Points.Where(x => x.Id_PointType == 1).ToList();
            var Diemmottiet = student_Points.Where(x => x.Id_PointType == 2).ToList();
            var Diemcuoiky = student_Points.Where(x => x.Id_PointType == 3).ToList();
            var Diemkiemtramieng = student_Points.Where(x => x.Id_PointType == 4).ToList();
            List<DiemKiemTraMieng> diemmieng = new List<DiemKiemTraMieng>();
            List<DiemKiemTraMotTiet> diemmottiet = new List<DiemKiemTraMotTiet>();
            List<KiemTra15Phut> diem15phut = new List<KiemTra15Phut>();
            List<DiemThiCuoiKi> diemthicuoiki = new List<DiemThiCuoiKi>();
            List<ViewListDiem> viewListDiems = new List<ViewListDiem>();
            var diemmiengStudent = Diemkiemtramieng.Where(x => x.Id_Student == id_Student).ToList();
            foreach (var student in diemmiengStudent)
            {
                diemmieng.Add(new DiemKiemTraMieng
                {
                    Id_point = student.Id_Point,
                    point = student.Point
                });
            }
            var diem15Phut = Diem15phut.Where(x => x.Id_Student == id_Student).ToList();
            foreach (var student in diem15Phut)
            {
                diem15phut.Add(new KiemTra15Phut
                {
                    Id_point = student.Id_Point,
                    point = student.Point
                });
            }
            var diemMotTiet = Diemmottiet.Where(x => x.Id_Student == id_Student).ToList();
            foreach (var student in diemMotTiet)
            {
                diemmottiet.Add(new DiemKiemTraMotTiet
                {
                    Id_point = student.Id_Point,
                    point = student.Point
                });
            }
            var diemCuoiKi = Diemcuoiky.Where(x => x.Id_Student == id_Student).ToList();
            foreach (var student in diemCuoiKi)
            {
                diemthicuoiki.Add(new DiemThiCuoiKi
                {
                    Id_point = student.Id_Point,
                    point = student.Point
                });
            }
            foreach(var student in student_Points)
            {
                viewListDiems.Add(new ViewListDiem
                {
                    id_Student = id_Student,
                    code_Subject = student.Code_Subject,
                    kiemTra15Phuts = diem15phut,
                    DiemKiemTraMiengs = diemmieng,
                    diemKiemTraMotTiets = diemmottiet,
                    diemThiCuoiKis = diemthicuoiki
                });
            }
            return viewListDiems;
        }
        public async Task<List<Student_Class>> GetStudent_ClassesAsync(int id_Class)
        {
            return await _context.Student_Classes.Where(x=>x.Id_Class == id_Class)
                .Include(x=>x.Student)    
                .ToListAsync();
        }
        public async Task<List<TeacherSchedule>> GetTeacherSchedulesAsync(int id_Class)
        {
            return await _context.TeacherSchedules.Where(x=>x.Id_Class==id_Class)
                    .Include(x=>x.Code_Subject).ToListAsync();
        }
        public async Task<bool> DeleteSubjectInClassAsync(DeleteSubject subject)
        {
            bool ret = false;
            try
            {
                var QtySubjectInClass = await _context.TeacherSchedules
                .Where(x => x.Id_Class == subject.Id_Class).ToListAsync();
                var sub = await _context.TeacherSchedules.Where(x => x.Id_Class == subject.Id_Class
                             && x.Code_Subject == subject.Code_Subect).FirstOrDefaultAsync();
                if (QtySubjectInClass.Count <= 1)
                {
                    var student_class = await _context.Student_Classes.Where(x => x.Id_Class == subject.Id_Class).FirstOrDefaultAsync();
                    _context.Student_Classes.Remove(student_class);
                }
                _context.TeacherSchedules.Remove(sub);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
        public async Task<bool> DeleteClassAsync(int id_Class)
        {
            bool ret=false; 
            try
            {
                var entity=await _context.Classes.Where(x=>x.Id_Class==id_Class).FirstOrDefaultAsync(); 
                _context.Classes.Remove(entity);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
        public async Task<bool> CheckDeleteClassAsync(int id_Class)
        {
            bool ret = false;
            try
            {
                var fee=await _context.Fees.Where(x=>x.Id_Class== id_Class).FirstOrDefaultAsync();
                var Student = await _context.Student_Classes.Where(x => x.Id_Class == id_Class).FirstOrDefaultAsync();
                var schedule=await _context.TeacherSchedules.Where(x=>x.Id_Class.Equals(id_Class)).FirstOrDefaultAsync();
                if (schedule==null && fee==null && Student==null)
                {
                    ret = true;
                }
                ret = false;
            }
            catch { }
            return ret;
        }
    }
}
