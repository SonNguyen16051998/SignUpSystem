using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpGet("{date}")]
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
    }
}
