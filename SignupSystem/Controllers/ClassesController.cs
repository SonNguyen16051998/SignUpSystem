using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Models.ViewModel;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClassesController : Controller
    {
        private readonly IClass _class;
        private readonly IStudentClass _studentClass;
        public ClassesController(IClass entity, IStudentClass studentClass)
        {
            _studentClass = studentClass;
            _class = entity;
        }
        /// <summary>
        /// lấy toàn bộ danh sách lớp học
        /// </summary>
        /// <returns></returns>
        [HttpGet, ActionName("class")]
        public async Task<IActionResult> GetClassesAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _class.GetClassesAsync()
            });
        }
        /// <summary>
        /// lấy lớp học thông qua id lớp học
        /// </summary>
        /// <param name="id">id lớp học</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("class")]
        public async Task<IActionResult> GetClassAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _class.GetClassAsync(id)
            });
        }
        /// <summary>
        /// thêm lớp học
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost, ActionName("class")]
        public async Task<IActionResult> PostClassAsync(Class entity)
        {
            if (ModelState.IsValid)
            {
                if (await _class.AddClassAsync(entity))
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
        /// cập nhật lớp học
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut, ActionName("class")]
        public async Task<IActionResult> PutClassAsync(Class entity)
        {
            if (ModelState.IsValid)
            {
                if (await _class.UpdateClassAsync(entity))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _class.GetClassAsync(entity.Id_Class)
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
        /// thêm điểm cho một học viên
        /// </summary>
        /// <param name="student_Point"></param>
        /// <returns></returns>
        [HttpPost, ActionName("themdiem")]
        public async Task<IActionResult> AddPointAsync(Student_Point student_Point)
        {
            if (ModelState.IsValid)
            {
                if (await _class.AddPointAsync(student_Point))
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
        /// thêm điểm cho cả lớp học
        /// </summary>
        /// <param name="addPoint"></param>
        /// <returns></returns>
        [HttpPost, ActionName("themdiemcholop")]
        public async Task<IActionResult> AddListPointAsync(ViewAddPoint addPoint)
        {
            if (ModelState.IsValid)
            {
                if (await _class.AddPointsAsync(addPoint))
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
        /// lấy toàn bộ điểm của học viên ở môn học cần xem
        /// </summary>
        /// <param name="id">mã môn học</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("xemdiem")]
        public async Task<IActionResult> GetPointsAsync(string id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _class.GetStudent_PointsAsync(id)
            });
        }
        /// <summary>
        /// chốt điểm
        /// </summary>
        /// <param name="student_Points"></param>
        /// <returns></returns>
        [HttpPut, ActionName("chotdiem")]
        public async Task<IActionResult> BlockPointAsync(List<Student_Point> student_Points)
        {
            if (ModelState.IsValid)
            {
                if (await _class.BlockPointAsync(student_Points))
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
        /// danh sách học viên của lớp học
        /// </summary>
        /// <param name="id">mã lớp</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("danhsachhocvien")]
        public async Task<IActionResult> GetStudentByClassAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _class.GetStudent_ClassesAsync(id)
            });
        }
        /// <summary>
        /// toàn bộ điểm của học viên 
        /// </summary>
        /// <param name="id">mã học viên</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("diemhocvien")]
        public async Task<IActionResult> GetListDiemByStudentAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _class.GetListDiemByStudentAsync(id)
            });
        }
        /// <summary>
        /// xóa học viên khỏi lớp học
        /// </summary>
        /// <param name="id">mã học viên</param>
        /// <returns></returns>
        [HttpDelete("{id}"), ActionName("xoahocvien")]
        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            if (await _studentClass.DeleteStudentFromClass(id))
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
        /// <summary>
        /// lấy toàn bộ môn học của lớp học
        /// </summary>
        /// <param name="id">mã lớp học</param>
        /// <returns></returns>
        [HttpGet("{id}"), ActionName("danhsachmonhoc")]
        public async Task<IActionResult> GetAllSubjectInClassAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _class.GetTeacherSchedulesAsync(id)
            });
        }
        /// <summary>
        /// xóa môn học khỏi lớp học
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpDelete, ActionName("xoamonhoc")]
        public async Task<IActionResult> DeleteSubectInClassAsync(DeleteSubject subject)
        {
            if (await _class.DeleteSubjectInClassAsync(subject))
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
        /// <summary>
        /// xóa lớp học. lớp học chỉ được xóa khi chưa có sinh viên đăng kí
        /// </summary>
        /// <param name="id">mã lớp học</param>
        /// <returns></returns>
        [HttpDelete("{id}"),ActionName("xoalophoc")]
        public async Task<IActionResult> DeleteClassAsync(int id)
        {
            if(await _class.CheckDeleteClassAsync(id))
            {
                if(await _class.DeleteClassAsync(id))
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
                    retText = "class already exist table children"
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
