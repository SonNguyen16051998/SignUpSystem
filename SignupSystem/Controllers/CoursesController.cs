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
        [HttpGet("{code:string}")]
        public async Task<IActionResult> GetCourseAsync(string code)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _coure.GetCourseAsync(code)
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddCourseAsync(Course course)
        {
            if(ModelState.IsValid)
            {
                if(await _coure.AddCourseAsync(course))
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
        [HttpPut]
        public async Task<IActionResult> UpdateCourseAsync(Course course)
        {
            if(ModelState.IsValid)
            {
                if(await _coure.UpdateCourseAsync(course))
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
        [HttpDelete("{code:string}")]
        public async Task<IActionResult> DeleteAsync(string code)
        {
            if(await _coure.CheckDeleteCourseAsync(code))
            {
                if (await _coure.DeleteCourseAsync(code))
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
