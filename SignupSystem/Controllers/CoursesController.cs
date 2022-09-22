using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : Controller
    {
        private readonly ICourse _coure;
        public CoursesController(ICourse course)
        {
            _coure = course;
        }
        /// <summary>
        /// lấy toàn bộ niên khóa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetCoursesAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _coure.GetCoursesAsync()
            });
        }
        /// <summary>
        /// lấy một niên khóa 
        /// </summary>
        /// <param name="id">mã niên khóa</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseAsync(string id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _coure.GetCourseAsync(id)
            });
        }
        /// <summary>
        /// thêm niên khóa
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCourseAsync(Course course)
        {
            if (ModelState.IsValid)
            {
                if (await _coure.AddCourseAsync(course))
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
        /// cập nhật niên khóa
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCourseAsync(Course course)
        {
            if (ModelState.IsValid)
            {
                if (await _coure.UpdateCourseAsync(course))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _coure.GetCourseAsync(course.Code_Course)
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
        /// xóa niên khóa
        /// </summary>
        /// <param name="id">mã niên khóa</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (await _coure.CheckDeleteCourseAsync(id))
            {
                if (await _coure.DeleteCourseAsync(id))
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
                retText = "exist class"
            });
        }
    }
}
