using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KhoasController : Controller
    {
        private readonly IKhoa _khoa;
        public KhoasController(IKhoa khoa)
        {
            _khoa = khoa;
        }
        /// <summary>
        /// lấy toàn bộ khoa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _khoa.GetKhoasAsync()
            });
        }
        /// <summary>
        /// lấy thông tin một khoa
        /// </summary>
        /// <param name="id">mã khoa</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _khoa.GetKhoaAsync(id)
            });
        }
        /// <summary>
        /// thêm khoa
        /// </summary>
        /// <param name="khoa"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                if (await _khoa.AddKhoaAsync(khoa))
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
        /// cập nhật khoa
        /// </summary>
        /// <param name="khoa"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Khoa khoa)
        {
            if (ModelState.IsValid)
            {
                if (await _khoa.UpdateKhoaAsync(khoa))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _khoa.GetKhoaAsync(khoa.Id_Khoa)
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
        /// xóa khoa
        /// </summary>
        /// <param name="id">mã khoa</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _khoa.DeleteKhoaAsync(id))
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
    }
}
