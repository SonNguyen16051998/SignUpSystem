using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = "User")]
    public class Role_QuyensController : Controller
    {
        private readonly IRole_Quyen _role_Quyen;
        public Role_QuyensController(IRole_Quyen role_Quyen)
        {
            _role_Quyen = role_Quyen;
        }
        /// <summary>
        ///thêm hoăc xóa quyền cho role
        /// </summary>
        /// <param name="Role_Quyens">truyền về danh sách object role_quyen</param>
        /// <returns></returns>
        [HttpPost(), ActionName("rolequyen")]
        public async Task<IActionResult> UpdateOrAddRole_QuyenAsync(List<Role_Quyen> Role_Quyens)
        {
            if (ModelState.IsValid)
            {
                if (await _role_Quyen.AddOrUpdateRole_QuyenAsync(Role_Quyens))
                {
                    if (await _role_Quyen.DeleteAlNothavelRole_QuyenAsync(Role_Quyens))
                    {
                        return Ok(new
                        {
                            retCode = 1,
                            retText = "Cập nhật quyền cho role thành công",
                            data = await _role_Quyen.GetRole_QuyensAsync(Role_Quyens[0].Id_Role)
                        });
                    }
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "Cập nhật quyền cho role thất bại",
                data = ""
            });
        }
        /// <summary>
        /// hiển thị toàn bộ quyền mà role có
        /// </summary>
        /// <param name="id">mã role</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("quyenin")]
        public async Task<List<Role_Quyen>> GetRole_QuyensAsync(int id)//hiển thị toàn bộ quyền của role có
        {
            return await _role_Quyen.GetRole_QuyensAsync(id);//id của role
        }
        /// <summary>
        /// hiển thị các quyền mà role chưa có
        /// </summary>
        /// <param name="id">mã role</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("quyennotin")]
        public async Task<List<Quyen>> GetQuyenRoleNotHaveAsync(int id)// hiển thị các quyen mà role chưa có
        {
            return await _role_Quyen.GetRoleQuyenNotHaveAsync(id);//id của role
        }
    }
}
