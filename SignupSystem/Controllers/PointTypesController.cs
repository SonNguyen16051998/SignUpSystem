using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PointTypesController : Controller
    {
        private readonly IPointType _pointType;
        public PointTypesController(IPointType pointType)
        {
            _pointType = pointType;
        }
        /// <summary>
        /// lấy toàn bộ loại điểm
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _pointType.GetPointTypesAsync()
            });
        }
        /// <summary>
        /// lấy thông tin một loại điểm
        /// </summary>
        /// <param name="id">mã loại điểm</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _pointType.GetPointTypeAsync(id)
            });
        }
        /// <summary>
        /// thêm loại điểm
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(PointType point)
        {
            if (ModelState.IsValid)
            {
                if (await _pointType.AddPointTypeAsync(point))
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
        /// cập nhật loại điểm
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(PointType point)
        {
            if (ModelState.IsValid)
            {
                if (await _pointType.UpdatePointType(point))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _pointType.GetPointTypeAsync(point.Id_PointType)
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
        /// xóa loại điểm
        /// </summary>
        /// <param name="id">mã loại điểm</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _pointType.CheckDeletePointTypeAsync(id))
            {
                if (await _pointType.DeletePoint(id))
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
    }
}
