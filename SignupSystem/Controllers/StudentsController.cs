using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Models.ViewModel;
using SignupSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StudentsController : Controller
    {
        private readonly IStudent _student;
        public StudentsController(IStudent student)
        {
            _student = student;
        }
        /// <summary>
        /// lấy toàn bộ danh sách học viên
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("student")]
        public async Task<IActionResult> GetStudentsAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _student.GetStudentsAsync()
            });
        }
        /// <summary>
        /// thêm học viên
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost, ActionName("student")]
        public async Task<IActionResult> AddStudentAsync(Student student)
        {
            if (ModelState.IsValid)
            {
                if (await _student.isEmail(student.Email))
                {
                    return Ok(new
                    {
                        retCode = 0,
                        retText = "Email đã tồn tại",
                        data = ""
                    });
                }
                else
                {
                    if (await _student.AddStudentAsync(student) > 0)
                    {
                        return Ok(new
                        {
                            retCode = 1,
                            retText = "add student successfuly"
                        });
                    }
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "add student failure"
            });
        }
        /// <summary>
        /// lấy toàn bộ lớp học của học viên và thời khóa biểu của học viên
        /// </summary>
        /// <param name="id">mã học viên</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("lophoc")]
        public async Task<IActionResult> GetClassesByStudentAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _student.GetClassesByStudentAsync(id)
            });
        }
        /// <summary>
        /// hủy đăng ký lớp học
        /// </summary>
        /// <param name="student_Class"></param>
        /// <returns></returns>
        [HttpDelete(), ActionName("huydangky")]
        public async Task<IActionResult> DeleteClassesOfStudentAsync(Student_Class student_Class)
        {
            if (ModelState.IsValid)
            {
                if (await _student.DeleteClassesOfStudentAsync(student_Class))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly"
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "failure"
            });
        }
        /// <summary>
        /// đăng ký lớp học
        /// </summary>
        /// <param name="studentClass"></param>
        /// <returns></returns>
        [HttpPost, ActionName("dangkylop")]
        public async Task<IActionResult> ClassRegistrationAsync(Student_Class studentClass)
        {
            studentClass.Id_ScheduleTeacher = 1;
            if (ModelState.IsValid)
            {
                if (await _student.ClassRegistrationAsync(studentClass))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "register class successfuly"
                    });
                }
                return Ok(new
                {
                    retCode = 0,
                    retText = "Qty already full"
                }); ;
            }
            return Ok(new
            {
                retCode = 0,
                retText = "failure"
            });
        }
        /// <summary>
        /// lấy thông tin một học viên
        /// </summary>
        /// <param name="id">mã học viên</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("student")]
        public async Task<IActionResult> GetStudentAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _student.GetStudentAsync(id)
            });
        }
        /// <summary>
        /// cập nhật thông tin học viên
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut, ActionName("student")]
        public async Task<IActionResult> UpdateStudentAsync(Student student)
        {
            if (ModelState.IsValid)
            {
                if (await _student.UpdateStudentAsync(student))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _student.GetStudentAsync(student.Id_Student)
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "failure"
            });
        }
        /// <summary>
        /// xóa học viên
        /// </summary>
        /// <param name="id">mã học viên</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ActionName("student")]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            if (await _student.CheckStudent(id))
            {
                if (await _student.DeleteStudentAsync(id))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly"
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "failure"
            });
        }
        /// <summary>
        /// thu học phí
        /// </summary>
        /// <param name="fee"></param>
        /// <returns></returns>
        [HttpPost, ActionName("thuhocphi")]
        public async Task<IActionResult> ThuHocPhiAsync(Fee fee)
        {
            fee.PaymentDate = System.DateTime.Now;
            if (ModelState.IsValid)
            {
                if (await _student.ThuHocPhiAsync(fee))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly"
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "failure"
            });
        }
        /// <summary>
        /// đổi mật khẩu
        /// </summary>
        /// <param name="changePass">mật khẩu từ 6 đến 30 kí tự</param>
        /// <returns></returns>
        [HttpPut, ActionName("doimatkhau")]
        public async Task<IActionResult> DoiMatKhauAsync([FromBody] ViewDoiMatKhau changePass)
        {
            if (ModelState.IsValid)
            {
                if (await _student.isPass(changePass.email, changePass.password))
                {
                    if (await _student.ChangePassAsync(changePass))
                        return Ok(new
                        {
                            retCode = 1,
                            retText = "Đổi mật khẩu thành công",
                            data = new
                            {
                                Email = changePass.email,
                                Password = changePass.newPassword
                            }
                        });
                }
                else
                {
                    return Ok(new
                    {
                        retCode = 0,
                        retText = "Mật khẩu củ không chính xác",
                        data = ""
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "Đổi mật khẩu thất bại",
                data = ""
            });
        }
        /// <summary>
        /// chức năng cập nhật mật khẩu khi đã xác nhận mã otp thành công
        /// </summary>
        /// <param name="quenMatKhau"></param>
        /// <returns></returns>
        [HttpPut, ActionName("quenmatkhau")]
        public async Task<IActionResult> QuenMatKhauAsync([FromBody] ViewQuenMatKhau quenMatKhau)
        {
            //xác nhận mã OTP thành công cho qua trang cập nhật mật khẩu mới
            if (ModelState.IsValid)
            {
                if (await _student.QuenMatKhauAsync(quenMatKhau))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "Cập nhật mật khẩu thành công",
                        data = new
                        {
                            Email = quenMatKhau.Email,
                            NewPass = quenMatKhau.NewPass
                        }
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "Cập nhật mật khẩu thất bại",
                data = ""
            });
        }
        /// <summary>
        /// chức năng nhận mã OTP khi xác nhận xong capcha và nhập email chính xác. cần nhập email đã có tài khoản
        /// </summary>
        /// <param name="maOTP">trả về obect OTP chỉ cần trả về email. còn lại trả về null</param>
        /// <returns></returns>
        [HttpPut, ActionName("maotp")]
        public async Task<IActionResult> CreateOrUpdateOTPAsync(OTP maOTP)//truyền về email,mặc định(otp=null,isuse=false)
        {
            maOTP.Code_OTP = Helpers.RandomOTPHelper.random();
            maOTP.isUse = false;
            maOTP.ExpiredAt = DateTime.Now.AddMinutes(2);
            if (ModelState.IsValid)
            {
                if (await _student.isEmail(maOTP.email))
                {
                    if (await _student.CreateOrUpdateOTPAsync(maOTP))
                    {
                        return Ok(new
                        {
                            retCode = 1,
                            retText = "Mã otp đã được gửi đến email",
                            data = new
                            {
                                Email = maOTP.email,
                                OTP = maOTP.Code_OTP
                            }
                        });
                    }
                }
                else
                {
                    return Ok(new
                    {
                        retCode = 0,
                        retText = "Email không tồn tại",
                        data = ""
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "Dữ liệu không hợp lệ",
                data = ""
            });
        }
        /// <summary>
        /// chức năng xác nhận mã OTP
        /// </summary>
        /// <param name="maOTP">truyền về object OTP gồm email, mã otp, còn lại có thể để null</param>
        /// <returns></returns>
        [HttpPut, ActionName("xacnhanotp")]
        public async Task<IActionResult> XacNhanOTP(OTP maOTP)//truyền về email và mã otp ,mặc định isuse=false
        {
            if (ModelState.IsValid)
            {
                if (await _student.ConfirmOTPAsync(maOTP.email, maOTP.Code_OTP))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "Mã OTP chính xác",
                        data = ""
                    });
                }
                else
                {
                    return Ok(new
                    {
                        retCode = 0,
                        retText = "Mã OTP đã hết hạn hoặc đã sử dụng",
                        data = ""
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "Dữ liệu không hợp lệ",
                data = ""
            });
        }
    }
}
