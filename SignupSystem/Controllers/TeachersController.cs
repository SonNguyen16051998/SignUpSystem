using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeachersController : Controller
    {
        private readonly ITeacher _teacher;
        public TeachersController(ITeacher teacher)
        {
            _teacher = teacher;
        }
        /// <summary>
        /// lấy toàn bộ giáo viên
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("teacher")]
        public async Task<IActionResult> GetTeachersAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _teacher.GetTeachersAsync()
            });
        }
        /// <summary>
        /// thêm giáo viên
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPost, ActionName("teacher")]
        public async Task<IActionResult> PostAsync(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                if (await _teacher.AddTeacherAsync(teacher))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _teacher.GetTeacherByIdAsync(teacher.Id_Teacher)
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
        /// lấy toàn bộ lịch dạy của một giáo viên
        /// </summary>
        /// <param name="id">mã giảng viên</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("lichdaygiangvien")]
        public async Task<IActionResult> GetTeacherSchedules(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _teacher.GetTeacherSchedules(id)
            });
        }
        /// <summary>
        /// cập nhật thông tin giáo viên
        /// </summary>
        /// <param name="teacher"></param>
        /// <returns></returns>
        [HttpPut, ActionName("teacher")]
        public async Task<IActionResult> UpdateTeacherAsync(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                if (await _teacher.UpdateTeacherAsync(teacher))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _teacher.GetTeacherByIdAsync(teacher.Id_Teacher)
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
        /// xóa giáo viên
        /// </summary>
        /// <param name="id">mã giáo viên</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ActionName("teacher")]
        public async Task<IActionResult> DeleteTeacherAsync(int id)
        {
            if (await _teacher.CheckDeleteTeacher(id))
            {
                if (await _teacher.DeleteTeacherAsync(id))
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
        /// lấy lịch dạy của toàn bộ giáo viên
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("lichday")]
        public async Task<IActionResult> GetTeacherSchedulesAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _teacher.GetTeacherSchedules()
            });
        }
        /// <summary>
        /// thêm lịch dạy cho giáo viên
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        [HttpPost, ActionName("lichday")]
        public async Task<IActionResult> AddTeacherScheduleAsync(TeacherSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                if (await _teacher.AddTeacherScheduleAsync(schedule))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _teacher.GetTeacherSchedules(schedule.Id_Teacher)
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
        /// cập nhật lịch dạy cho giáo viên
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        [HttpPut, ActionName("lichday")]
        public async Task<IActionResult> UpdateSchedule(TeacherSchedule schedule)
        {
            if (ModelState.IsValid)
            {
                if (await _teacher.UpdateTeacherScheduleAsync(schedule))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _teacher.GetTeacherSchedules(schedule.Id_Teacher)
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
        /// xóa lịch dạy
        /// </summary>
        /// <param name="id">mã lịch dạy</param>
        /// <returns></returns>
        [HttpDelete("id"), ActionName("lichday")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            if (await _teacher.DeleteTeacherScheduleAsync(id))
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
