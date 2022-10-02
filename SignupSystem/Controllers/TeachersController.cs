using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Policy = "User")]
    public class TeachersController : Controller
    {
        private readonly ITeacher _teacher;
        private readonly IStudent _student;
        public TeachersController(ITeacher teacher, IStudent student)
        {
            _student = student;
            _teacher = teacher;
        }
        /// <summary>
        /// lấy toàn bộ giáo viên
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("teacher")]
        public async Task<IActionResult> GetTeachersAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _teacher.GetTeachersAsync()
            });
        }
        /// <summary>
        /// thêm giáo viên
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost, ActionName("teacher")]
        public async Task<IActionResult> PostAsync(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                if (await _teacher.isEmail(teacher.Email))
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
                    if (await _teacher.AddTeacherAsync(teacher))
                    {
                        return Ok(new
                        {
                            retCode = 1,
                            retText = "successfuly",
                            data = await _teacher.GetTeacherByIdAsync(teacher.Id_Teacher)
                        });
                    }
                }

            }
            return Ok(new
            {
                retCode = 0,
                retText = "failure"
            });
        }
        /// <summary>
        /// lấy toàn bộ lịch dạy của một giáo viên
        /// </summary>
        /// <param name="id">mã giảng viên</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("lichdaygiangvien")]
        [Authorize(Policy = "Teacher")]
        public async Task<IActionResult> GetTeacherSchedules(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _teacher.GetTeacherSchedules(id)
            });
        }
        /// <summary>
        /// cập nhật thông tin giáo viên
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPut, ActionName("teacher")]
        [Authorize(Policy = "Teacher")]
        public async Task<IActionResult> UpdateTeacherAsync(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                if (await _teacher.UpdateTeacherAsync(teacher))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _teacher.GetTeacherByIdAsync(teacher.Id_Teacher)
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
        /// xóa giáo viên
        /// </summary>
        /// <param name="id">mã giáo viên</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ActionName("teacher")]
        public async Task<IActionResult> DeleteTeacherAsync(int id)
        {
            if (await _teacher.CheckDeleteTeacher(id))
            {
                if (await _teacher.DeleteTeacherAsync(id))
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
        /// lấy lịch dạy của toàn bộ giáo viên
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("lichday")]
        public async Task<IActionResult> GetTeacherSchedulesAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _teacher.GetTeacherSchedules()
            });
        }
        /// <summary>
        /// thêm lịch dạy cho giáo viên
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        [HttpPost, ActionName("lichday")]
        public async Task<IActionResult> AddTeacherScheduleAsync(TeacherSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                if (await _teacher.AddTeacherScheduleAsync(schedule))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _teacher.GetTeacherSchedules(schedule.Id_Teacher)
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
        /// cập nhật lịch dạy cho giáo viên
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        [HttpPut, ActionName("lichday")]
        public async Task<IActionResult> UpdateSchedule(TeacherSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                if (await _teacher.UpdateTeacherScheduleAsync(schedule))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _teacher.GetTeacherSchedules(schedule.Id_Teacher)
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
        /// xóa lịch dạy
        /// </summary>
        /// <param name="id">mã lịch dạy</param>
        /// <returns></returns>
        [HttpDelete("id"), ActionName("lichday")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            if (await _teacher.DeleteTeacherScheduleAsync(id))
            {
                return Ok(new
                {
                    retCode = 1,
                    retText = "successfuly"
                });
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
        [Authorize(Policy = "Teacher")]
        public async Task<IActionResult> DoiMatKhauAsync([FromBody] ViewDoiMatKhau changePass)
        {
            if (ModelState.IsValid)
            {
                if (await _teacher.isPass(changePass.email, changePass.password))
                {
                    if (await _teacher.ChangePassAsync(changePass))
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
        [Authorize(Policy = "Teacher")]
        public async Task<IActionResult> QuenMatKhauAsync([FromBody] ViewQuenMatKhau quenMatKhau)
        {
            //xác nhận mã OTP thành công cho qua trang cập nhật mật khẩu mới
            if (ModelState.IsValid)
            {
                if (await _teacher.QuenMatKhauAsync(quenMatKhau))
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
        [Authorize(Policy = "Teacher")]
        public async Task<IActionResult> CreateOrUpdateOTPAsync(OTP maOTP)//truyền về email,mặc định(otp=null,isuse=false)
        {
            maOTP.Code_OTP = Helpers.RandomOTPHelper.random();
            maOTP.isUse = false;
            maOTP.ExpiredAt = DateTime.Now.AddMinutes(2);
            if (ModelState.IsValid)
            {
                if (await _teacher.isEmail(maOTP.email))
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
        [Authorize(Policy = "Teacher")]
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

        [HttpPost,ActionName("salary")]
        public async Task<IActionResult> AddSalary(Salary salary)
        {
            if(await _teacher.AddSalaryAsync(salary) > 0)
            {
                return Ok(new
                {
                    retCode = 1,
                    retText = "Tạo phiếu lương thành công",
                    data = ""
                });
            }
            return Ok(new
            {
                retCode = 0,
                retText = "Thất bại",
                data = ""
            });
        }
    }
}
