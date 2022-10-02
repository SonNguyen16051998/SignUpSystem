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
    public class UsersController : Controller
    {
        private readonly IUser _user;
        private readonly IStudent _student;
        public UsersController(IUser user, IStudent student)
        {
            _student = student;
            _user = user;
        }
        /// <summary>
        /// lấy toàn bộ người dùng
        /// </summary>
        /// <returns></returns>
        [HttpGet,ActionName("user")]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _user.GetUsersAsync()
            });
        }
        /// <summary>
        /// lấy một người dùng
        /// </summary>
        /// <param name="id">mã người dùng</param>
        /// <returns></returns>
        [HttpGet("{id}"),ActionName("user")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _user.GetUserAsync(id)
            });
        }
        /// <summary>
        /// thêm người dùng
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpPost,ActionName("user")]
        public async Task<IActionResult> AddUserAsync(User User)
        {
            if (ModelState.IsValid)
            {
                if (await _user.AddUserAsync(User))
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
        /// cập nhật người dùng
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpPut, ActionName("user")]
        public async Task<IActionResult> UpdateUserAsync(User User)
        {
            if (ModelState.IsValid)
            {
                if (await _user.UpdateUserAsync(User))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _user.GetUserAsync(User.Id_User)
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
        /// xóa người dùng
        /// </summary>
        /// <param name="id">mã người dùng</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ActionName("user")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _user.DeleteUserAsync(id))
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
                retText = "exist class"
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
                if (await _user.isPass(changePass.email, changePass.password))
                {
                    if (await _user.ChangePassAsync(changePass))
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
                if (await _user.QuenMatKhauAsync(quenMatKhau))
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
                if (await _user.isEmail(maOTP.email))
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
