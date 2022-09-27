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
    public class QuyensController : Controller
    {
        private readonly IQuyen _quyen;
        public QuyensController(IQuyen quyen)
        {
            _quyen = quyen;
        }
        /// <summary>
        /// trả về toàn bộ quyền 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Quyen>> GetQuyensAsync()
        {
            return await _quyen.GetQuyensAsync();
        }
        /// <summary>
        /// trả về quyền được chọn
        /// </summary>
        /// <param name="id">mã quyền</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Quyen> GetQuyenAsync(int id)
        {
            return await _quyen.GetQuyenAsync(id);
        }
        /// <summary>
        /// thêm quyền
        /// </summary>
        /// <param name="quyen">truyền về object quyen trả về tên quyền </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostQuyenAsync([FromBody] Quyen quyen)
        {
            if (ModelState.IsValid)
            {
                int id_Quyen = await _quyen.AddQuyenAsync(quyen);
                if (id_Quyen > 0)
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "Thêm quyền thành công",
                        data = await _quyen.GetQuyenAsync(id_Quyen)
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
        /// cập nhật quyền
        /// </summary>
        /// <param name="Quyen">truyền đầy đủ dữ liệu tên</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutQuyenAsync(Quyen Quyen)
        {
            if (ModelState.IsValid)
            {
                int id_quyen = await _quyen.UpdateQuyenAsync(Quyen);
                if (id_quyen > 0)
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "Cập nhật quyền thành công",
                        data = await _quyen.GetQuyenAsync(id_quyen)
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
        /// xóa quyền và quyền sẽ được xóa khỏi role 
        /// </summary>
        /// <param name="id">mã quyền</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> DeleteQuyenAsync(int id)
        {
            if (await _quyen.DeleteQuyenAsync(id))
            {
                return Ok(new
                {
                    retCode = 1,
                    retText = "Xóa quyền thành công",
                    data = await _quyen.GetQuyenAsync(id)
                });
            }
            return Ok(new
            {
                retCode = 0,
                retText = "Xóa quyền thất bại",
                data = ""
            });
        }
    }
}
