using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeachersController:Controller
    {
        private readonly ITeacher _teacher;
        public TeachersController(ITeacher teacher)
        {
            _teacher = teacher;
        }

        [HttpGet,ActionName("teacher")]
        public async Task<IActionResult> GetTeachersAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _teacher.GetTeachersAsync()
            });
        }
        [HttpPost,ActionName("teacher")]
        public async Task<IActionResult> PostAsync(Teacher teacher)
        {
            if(ModelState.IsValid)
            {
                if(await _teacher.AddTeacherAsync(teacher))
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
        [HttpGet("{id}"),ActionName("lichdaygiangvien")]
        public async Task<IActionResult> GetTeacherSchedules(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _teacher.GetTeacherSchedules(id)
            });
        }
        [HttpPut,ActionName("teacher")]
        public async Task<IActionResult> UpdateTeacherAsync(Teacher teacher)
        {
            if(ModelState.IsValid)
            {
                if(await _teacher.UpdateTeacherAsync(teacher))
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
        [HttpDelete("{id}"),ActionName("teacher")]
        public async Task<IActionResult> DeleteTeacherAsync(int id)
        {
            if(await _teacher.CheckDeleteTeacher(id))
            {
                if(await _teacher.DeleteTeacherAsync(id))
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
        [HttpGet,ActionName("lichday")]
        public async Task<IActionResult> GetTeacherSchedulesAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data=await _teacher.GetTeacherSchedules()
            });
        }
        [HttpPost,ActionName("lichday")]
        public async Task<IActionResult> AddTeacherScheduleAsync(TeacherSchedule schedule)
        {
            if(ModelState.IsValid)
            {
                if (await _teacher.AddTeacherScheduleAsync(schedule))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data=await _teacher.GetTeacherSchedules(schedule.Id_Teacher)
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "failure"
            });
        }
        [HttpPut,ActionName("lichday")]
        public async Task<IActionResult> UpdateSchedule(TeacherSchedule schedule)
        {
            if(ModelState.IsValid)
            {
                if(await _teacher.UpdateTeacherScheduleAsync(schedule))
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
        [HttpDelete("id"),ActionName("lichday")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            if(await _teacher.DeleteTeacherScheduleAsync(id))
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
