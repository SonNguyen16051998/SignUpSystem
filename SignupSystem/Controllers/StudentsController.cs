using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StudentsController : Controller
    {
        private readonly IStudent _student;
        public StudentsController(IStudent student)
        {
            _student = student;
        }
        /// <summary>
        /// lấy toàn bộ danh sách học viên
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("student")]
        public async Task<IActionResult> GetStudentsAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _student.GetStudentsAsync()
            });
        }
        /// <summary>
        /// thêm học viên
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost, ActionName("student")]
        public async Task<IActionResult> AddStudentAsync(Student student)
        {
            if (ModelState.IsValid)
            {
                if (await _student.AddStudentAsync(student) > 0)
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "add student successfuly"
                    });
                }
            }
            return Ok(new
            {
                retCode = 0,
                retText = "add student failure"
            });
        }
        /// <summary>
        /// lấy toàn bộ lớp học của học viên và thời khóa biểu của học viên
        /// </summary>
        /// <param name="id">mã học viên</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("lophoc")]
        public async Task<IActionResult> GetClassesByStudentAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _student.GetClassesByStudentAsync(id)
            });
        }
        /// <summary>
        /// hủy đăng ký lớp học
        /// </summary>
        /// <param name="student_Class"></param>
        /// <returns></returns>
        [HttpDelete(), ActionName("huydangky")]
        public async Task<IActionResult> DeleteClassesOfStudentAsync(Student_Class student_Class)
        {
            if (ModelState.IsValid)
            {
                if (await _student.DeleteClassesOfStudentAsync(student_Class))
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
        /// đăng ký lớp học
        /// </summary>
        /// <param name="studentClass"></param>
        /// <returns></returns>
        [HttpPost, ActionName("dangkylop")]
        public async Task<IActionResult> ClassRegistrationAsync(Student_Class studentClass)
        {
            studentClass.Id_ScheduleTeacher = 1;
            if (ModelState.IsValid)
            {
                if (await _student.ClassRegistrationAsync(studentClass))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "register class successfuly"
                    });
                }
                return Ok(new
                {
                    retCode = 0,
                    retText = "Qty already full"
                }); ;
            }
            return Ok(new
            {
                retCode = 0,
                retText = "failure"
            });
        }
        /// <summary>
        /// lấy thông tin một học viên
        /// </summary>
        /// <param name="id">mã học viên</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("student")]
        public async Task<IActionResult> GetStudentAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _student.GetStudentAsync(id)
            });
        }
        /// <summary>
        /// cập nhật thông tin học viên
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut, ActionName("student")]
        public async Task<IActionResult> UpdateStudentAsync(Student student)
        {
            if (ModelState.IsValid)
            {
                if (await _student.UpdateStudentAsync(student))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _student.GetStudentAsync(student.Id_Student)
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
        /// xóa học viên
        /// </summary>
        /// <param name="id">mã học viên</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ActionName("student")]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            if (await _student.CheckStudent(id))
            {
                if (await _student.DeleteStudentAsync(id))
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
        /// thu học phí
        /// </summary>
        /// <param name="fee"></param>
        /// <returns></returns>
        [HttpPost, ActionName("thuhocphi")]
        public async Task<IActionResult> ThuHocPhiAsync(Fee fee)
        {
            if (ModelState.IsValid)
            {
                if (await _student.ThuHocPhiAsync(fee))
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
