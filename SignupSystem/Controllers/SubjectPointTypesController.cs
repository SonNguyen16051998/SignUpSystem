using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectPointTypesController : Controller
    {
        private readonly ISubjectPointType _subjectPoint;
        public SubjectPointTypesController(ISubjectPointType subjectPoint)
        {
            _subjectPoint = subjectPoint;
        }
        /// <summary>
        /// lấy toàn bộ loại điểm của môn học
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _subjectPoint.GetSubject_PointTypesAsync()
            });
        }
        /// <summary>
        /// lấy thông tin loại điểm của một môn học
        /// </summary>
        /// <param name="id">mã môn</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _subjectPoint.GetSubject_PointTypeAsync(id)
            });
        }
        /// <summary>
        /// thêm loại điểm cho môn học
        /// </summary>
        /// <param name="subject_PointType"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(Subject_PointType subject_PointType)
        {
            if (ModelState.IsValid)
            {
                if (await _subjectPoint.AddSubject_Point(subject_PointType))
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
        /// cập nhật loại điểm cho môn học
        /// </summary>
        /// <param name="subject_PointType"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Subject_PointType subject_PointType)
        {
            if (ModelState.IsValid)
            {
                if (await _subjectPoint.UpdateSubjectPoint(subject_PointType))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _subjectPoint.GetSubject_PointTypeAsync(subject_PointType.Id_PointType)
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
        /// xóa loại điểm của môn học
        /// </summary>
        /// <param name="id">mã loại điểm</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _subjectPoint.DeleteSubjectPoint(id))
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
