using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Models.ViewModel;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class User_RolesSvc : Controller
    {
        private readonly IUser_Role _nguoiDung_Role;
        public User_RolesSvc(IUser_Role nguoiDung_Role)
        {
            _nguoiDung_Role = nguoiDung_Role;
        }
        /// <summary>
        /// cập nhật hoặc thêm mới role cho người dùng
        /// </summary>
        /// <param name="nguoiDung_Roles">truyền về mã người dùng và mã role</param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> UpdateOrAddNguoiDung_RoleAsync(User_Role nguoiDung_Roles)
        {
            if (ModelState.IsValid)
            {
                if (await _nguoiDung_Role.AddOrUpdateNguoiDung_RoleAsync(nguoiDung_Roles))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "Cập nhật role cho người dùng thành công",
                        data = await _nguoiDung_Role.GetNguoiDung_RoleAsync(nguoiDung_Roles.Ma_NguoiDung)
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "Cập nhật role cho người dùng thất bại",
                data = ""
            });
        }
        /// <summary>
        /// trả về  role người dùng đang có
        /// </summary>
        /// <param name="id">id người dùng</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<User_Role> GetNguoiDung_RoleAsync(int id)//hiển thị role của người dùng có
        {
            return await _nguoiDung_Role.GetNguoiDung_RoleAsync(id);//id của người dùng
        }
    }
}
