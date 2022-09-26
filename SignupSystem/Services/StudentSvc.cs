using SignupSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignupSystem.Models.ViewModel;

namespace SignupSystem.Services
{
    public interface IStudent
    {
        Task<List<Student>> GetStudentsAsync();//lấy toàn bộ học viên
        Task<int> AddStudentAsync(Student student);//thêm hoc viên
        Task<List<Student_Class>> GetClassesByStudentAsync(int id_Student);//lấy danh sách lớp học và thời khóa biểu bằng mã học viên
        Task<bool> DeleteClassesOfStudentAsync(Student_Class studentClass);//hủy đăng kí lớp học của học viên
        Task<bool> ClassRegistrationAsync(Student_Class studentClass);//đăng ký lớp học.
        Task<Student> GetStudentAsync(int id_Student);//lấy thông tin học viên bằng id
        Task<bool> UpdateStudentAsync (Student student);//cập nhật thông tin học viên   
        Task<bool> DeleteStudentAsync(int id_Student);//xóa học viên
        Task<bool> ThuHocPhiAsync(Fee fee);//tao phieu thu hoc phi
        Task<bool> CheckStudent(int id_Student);//kiểm tra học viên có được xóa hay k
        Task<Student> LoginAsync(ViewLogin login);//dang nhap
        Task<bool> isPass(string email, string pass);//kiem tra pass dung hay khong
        Task<bool> isEmail(string email);//kiem tra ton tai cua email
        Task<bool> CreateOrUpdateOTPAsync(OTP oTP);//tạo mã otp hoặc cập nhật mã otp mới cho email đã tồn tại
        Task<bool> ConfirmOTPAsync(string email, string otp);//xác nhận OTP sau khi nhập mã OTP
        Task<bool> QuenMatKhauAsync(ViewQuenMatKhau quenMatKhau);//đổi mật khẩu mới khi chọn chức năng quên mật khẩu
        Task<bool> ChangePassAsync(ViewDoiMatKhau changePass);
    }
    public class StudentSvc:IStudent
    {
        private readonly DataContext _context;
        public StudentSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Student>> GetStudentsAsync()
        {
            List<Student> students = new List<Student>();
            students=await _context.Students.ToListAsync();
            return students;
        }
        public async Task<int> AddStudentAsync(Student student)
        {
            int stt = 0;
            try
            {   student.PassWord=Helpers.MaHoaHelper.Mahoa(student.PassWord);
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                stt = student.Id_Student;
            }
            catch
            {
                stt = 0;
            }
            return stt;
        }
        public async Task<List<Student_Class>> GetClassesByStudentAsync(int id_Student)
        {
            List<Student_Class> classes = new List<Student_Class>();
            classes=await _context.Student_Classes.Where(x=>x.Id_Student==id_Student)
                    .Include(x=>x.Class)  
                    .Include(x=>x.TeacherSchedule)
                    .ToListAsync();
            return classes;
        }
        public async Task<bool> DeleteClassesOfStudentAsync(Student_Class studentClass)
        {
            bool ret = false;
            try
            {
                Student_Class student_Class= new Student_Class();
                student_Class=await _context.Student_Classes
                                .Where(x=>x.Id_Class==studentClass.Id_Class && x.Id_Student==studentClass.Id_Student)
                                .FirstOrDefaultAsync();
                if(student_Class!=null)
                {
                    _context.Student_Classes.Remove(student_Class);
                    Class lophoc= new Class();
                    lophoc=await _context.Classes.Where(x=>x.Id_Class==studentClass.Id_Class).FirstOrDefaultAsync();
                    lophoc.QtyStudent -= 1;
                    _context.Classes.Update(lophoc);
                }
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
        public async Task<bool> ClassRegistrationAsync(Student_Class studentClass)
        {
            bool ret = false;
            try
            {
                Class lophoc = new Class();
                lophoc = await _context.Classes.Where(x => x.Id_Class == studentClass.Id_Class).FirstOrDefaultAsync();
                if(lophoc.QtyStudent>lophoc.QtyStudentExist)
                {
                    TeacherSchedule teacherSchedule = new TeacherSchedule();
                    teacherSchedule = await _context.TeacherSchedules
                                     .Where(x => x.Id_Class == studentClass.Id_Class).FirstOrDefaultAsync();
                    studentClass.Id_ScheduleTeacher = teacherSchedule.Id_Schedule;
                    await _context.Student_Classes.AddAsync(studentClass);

                    lophoc.QtyStudent += 1;
                    lophoc.QtyStudentExist += 1;
                    _context.Classes.Update(lophoc);
                    await _context.SaveChangesAsync();
                    ret = true;
                }
                ret = false;
            }
            catch { }
            return ret;
        }
        public async Task<Student> GetStudentAsync(int id_Student)
        {
            Student student = new Student();
            student = await _context.Students.Where(x => x.Id_Student == id_Student)
                    .Include(x=>x.Student_Classes)
                    .FirstOrDefaultAsync();
            return student;
        }
        public async Task<bool> UpdateStudentAsync(Student student)
        {
            bool ret = false;
            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> DeleteStudentAsync(int id_Student)
        {
            bool ret = false;
            try
            {
                Student student = new Student();
                student = await _context.Students.Where(x => x.Id_Student == id_Student).FirstOrDefaultAsync();
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> ThuHocPhiAsync(Fee fee)
        {
            bool ret = true;
            try
            {
                await _context.Fees.AddAsync(fee);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> CheckStudent(int id_Student)
        {
            Student_Point student_Point = await _context.Student_Points
                                        .Where(x => x.Id_Student == id_Student).FirstOrDefaultAsync();
            Fee fee=await _context.Fees.Where(x=>x.Id_Student==id_Student).FirstOrDefaultAsync();
            Student_Class student_Class = await _context.Student_Classes
                                            .Where(x => x.Id_Student == id_Student).FirstOrDefaultAsync();
            if(student_Point == null && fee==null && student_Class==null)
            {
                return true;
            }
            return false;
        }
        public async Task<Student> LoginAsync(ViewLogin login)
        {
            Student student = await _context.Students.Where(x => x.Email == login.Email
                  && x.PassWord == Helpers.MaHoaHelper.Mahoa(login.PassWord)).FirstOrDefaultAsync();
            if (student != null)
            {
                return student;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> isPass(string email, string pass)
        {
            bool ret = false;
            try
            {
                Student nguoiDung = await _context.Students.Where(x => x.Email == email 
                && x.PassWord ==Helpers.MaHoaHelper.Mahoa(pass))
                    .FirstOrDefaultAsync();
                if (nguoiDung != null)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        public async Task<bool> isEmail(string email)
        {
            bool ret = false;
            try
            {
                Student nguoiDung = await _context.Students.Where(x => x.Email == email).FirstOrDefaultAsync();
                if (nguoiDung != null)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        public async Task<bool> CreateOrUpdateOTPAsync(OTP oTP)
        {
            bool result = false;
            try
            {
                OTP otp = new OTP();
                otp = await _context.OTPs.Where(x => x.email == oTP.email).FirstOrDefaultAsync();
                if (otp != null)
                {
                    otp.Code_OTP = oTP.Code_OTP;
                    otp.ExpiredAt = oTP.ExpiredAt;
                    otp.isUse = false;
                    _context.OTPs.Update(otp);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await _context.OTPs.AddAsync(oTP);
                    await _context.SaveChangesAsync();
                }
                Helpers.SendEmailHelper.SendEmail(oTP.email, oTP.Code_OTP);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public async Task<bool> ConfirmOTPAsync(string email, string OTP)
        {
            bool result = false;
            try
            {
                OTP otp = new OTP();
                otp = await _context.OTPs.Where(x => x.email == email && x.Code_OTP == OTP
                                && DateTime.Now < x.ExpiredAt).FirstOrDefaultAsync();
                if (otp != null)
                {
                    otp.isUse = true;
                    _context.OTPs.Update(otp);
                    await _context.SaveChangesAsync();
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch
            {
                result = false;
            }
            return result;
        }
        public async Task<bool> QuenMatKhauAsync(ViewQuenMatKhau quenMatKhau)
        {
            bool result = false;
            try
            {
                Student nguoiDung = await _context.Students.Where(x => x.Email == quenMatKhau.Email).FirstOrDefaultAsync();
                nguoiDung.PassWord =Helpers.MaHoaHelper.Mahoa(quenMatKhau.NewPass);
                _context.Students.Update(nguoiDung);
                await _context.SaveChangesAsync();
                result = true;
            }
            catch { }
            return result;
        }
        public async Task<bool> ChangePassAsync(ViewDoiMatKhau changePass)
        {
            bool result = false;
            try
            {
                Student nguoiDung = await _context.Students.Where(x => x.Email == changePass.email).FirstOrDefaultAsync();
                nguoiDung.PassWord =Helpers.MaHoaHelper.Mahoa(changePass.newPassword);
                _context.Students.Update(nguoiDung);
                await _context.SaveChangesAsync();
                result = true;
            }
            catch { }
            return result;
        }
    }
}
