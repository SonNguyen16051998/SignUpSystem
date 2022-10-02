using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "User")]
    public class HolidaysController : Controller
    {
        private readonly IHoliday _holiday;
        public HolidaysController(IHoliday holiday)
        {
            _holiday = holiday;
        }
        /// <summary>
        /// lấy toàn bộ ngày nghỉ lễ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _holiday.GetHolidaySchedulesAsync()
            });
        }
        /// <summary>
        /// lấy thông tin một ngày nghỉ
        /// </summary>
        /// <param name="id">mã ngày nghỉ</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _holiday.GetHolidayScheduleAsync(id)
            });
        }
        /// <summary>
        /// thêm ngày nghỉ lễ
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(HolidaySchedule holiday)
        {
            if (ModelState.IsValid)
            {
                if (await _holiday.AddHolidayAsync(holiday))
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
        /// cập nhật ngày nghỉ
        /// </summary>
        /// <param name="holiday"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(HolidaySchedule holiday)
        {
            if (ModelState.IsValid)
            {
                if (await _holiday.UpdateHolidayAsync(holiday))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _holiday.GetHolidayScheduleAsync(holiday.Id_Holiday)
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
        /// xóa ngày nghỉ
        /// </summary>
        /// <param name="id">mã ngày nghỉ</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _holiday.DeleteHolidayAsync(id))
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
