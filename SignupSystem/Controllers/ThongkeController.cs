using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = "User")]
    public class ThongkeController : Controller
    {
        private readonly IThongKe _thongKe;
        public ThongkeController(IThongKe thongke)
        {
            _thongKe = thongke;
        }
        /// <summary>
        /// lấy toàn bộ học viên đóng tiền ngày muốn xem
        /// </summary>
        /// <param name="date">ngày muốn xem</param>
        /// <returns></returns>
        [HttpGet("{date}"),ActionName("doanhthutheongay")]
        public async Task<IActionResult> GetFeeByDate(DateTime date)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _thongKe.GetFeesByDate(date)
            });
        }
        /// <summary>
        /// lấy toàn bộ học viên đã đóng tiền
        /// </summary>
        /// <returns></returns>
        [HttpGet,ActionName("getall")]
        public async Task<IActionResult> GetAllFees()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _thongKe.GetFeesAsync()
            });
        }
        /// <summary>
        /// lấy toàn bộ lương của giảng viên
        /// </summary>
        /// <param name="teacherId">mã giảng viên</param>
        /// <returns></returns>
        [HttpGet("{id}"),ActionName("luonggiangvien")]
        public async Task<IActionResult> GetSalatyByTeacher(int teacherId)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _thongKe.GetSalaryByTeacher(teacherId)
            });
        }
        /// <summary>
        /// lấy thông tin chi tiết của bảng lương
        /// </summary>
        /// <param name="salaryId">mã bảng lương</param>
        /// <returns></returns>
        [HttpGet("{id}"),ActionName("luongchitiet")]
        public async Task<IActionResult> GetDetailsSalary(int salaryId)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _thongKe.GetDetailsSalary(salaryId)
            });
        }
        /// <summary>
        /// lấy toàn bô lương theo tháng được chọn
        /// </summary>
        /// <param name="date">tháng và năm muốn xem</param>
        /// <returns></returns>
        [HttpGet("{date}"),ActionName("getbydate")]
        public async Task<IActionResult> GetSalaryByDate(DateTime date)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _thongKe.GetSlaryByDate(date)
            });
        }
    }
}
