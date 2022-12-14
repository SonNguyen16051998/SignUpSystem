
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SignupSystem.Models;
using SignupSystem.Models.ViewModel;
using SignupSystem.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class LoginsController : Controller
    {
        private readonly IStudent _student;
        private readonly ITeacher _teacher;
        private readonly IUser _user;
        private readonly IConfiguration _config;
        public LoginsController(IStudent student, ITeacher teacher, IConfiguration config, IUser user)
        {
            _student = student;
            _teacher = teacher;
            _config = config;
            _user = user;
        }
        /// <summary>
        /// đăng nhập bằng tài khoản học viên
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost, ActionName("studentlogin")]
        public async Task<IActionResult> StudentLoginAsync([FromBody] ViewLogin login)
        {
            if (ModelState.IsValid)
            {
                var stu = await _student.LoginAsync(login);
                if (stu != null)
                {
                    var Claims = new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim("Id",stu.Id_Student.ToString()),
                        new Claim("Name",stu.LastName + stu.FirstName),
                        new Claim("Email",stu.Email),
                        new Claim("Address",stu.Address),
                        new Claim("Role","Student")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                        _config["Jwt:Audience"], Claims, expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);
                    ViewToken<Student> viewToken = new ViewToken<Student>() { Token = new JwtSecurityTokenHandler().WriteToken(token), User = stu };
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "Đăng nhập thành công",
                        data = viewToken
                    });
                }
                else
                {
                    return Ok(new
                    {
                        retCode = 0,
                        retText = "Tài khoản hoặc mật khẩu không chính xác",
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
        /// đăng nhâp bằng tài khoản giảng viên
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost, ActionName("teacherlogin")]
        public async Task<IActionResult> TeacherLoginAsync([FromBody] ViewLogin login)
        {
            if (ModelState.IsValid)
            {
                var tea = await _teacher.LoginAsync(login);
                if (tea != null)
                {
                    var Claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim("Id",tea.Id_Teacher.ToString()),
                        new Claim("Name",tea.LastName + tea.FirstName),
                        new Claim("Email",tea.Email),
                        new Claim("Address",tea.Address),
                        new Claim("Role","Teacher")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                        _config["Jwt:Audience"], Claims, expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);
                    ViewToken<Teacher> viewToken = new ViewToken<Teacher>() { Token = new JwtSecurityTokenHandler().WriteToken(token), User = tea };
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "Đăng nhập thành công",
                        data = viewToken
                    });
                }
                else
                {
                    return Ok(new
                    {
                        retCode = 0,
                        retText = "Tài khoản hoặc mật khẩu không chính xác",
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
        /// đăng nhâp bằng tài khoản giảng viên
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost, ActionName("userlogin")]
        public async Task<IActionResult> UserLoginAsync([FromBody] ViewLogin login)
        {
            if (ModelState.IsValid)
            {
                var tea = await _user.LoginAsync(login);
                if (tea != null)
                {
                    var Claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_config["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim("Id",tea.Id_User.ToString()),
                        new Claim("Name",tea.UserName ),
                        new Claim("Email",tea.Email),
                        new Claim("Role","User")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                        _config["Jwt:Audience"], Claims, expires: DateTime.UtcNow.AddDays(1),
                        signingCredentials: signIn);
                    ViewToken<User> viewToken = new ViewToken<User>() { Token = new JwtSecurityTokenHandler().WriteToken(token), User = tea };
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "Đăng nhập thành công",
                        data = viewToken
                    });
                }
                else
                {
                    return Ok(new
                    {
                        retCode = 0,
                        retText = "Tài khoản hoặc mật khẩu không chính xác",
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
