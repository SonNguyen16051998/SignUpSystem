using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : Controller
    {
        private readonly ISubject _subject;
        public SubjectsController(ISubject subject)
        {
            _subject = subject;
        }
        /// <summary>
        /// lấy toàn bộ môn học
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetSubjectsAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _subject.GetSubjectsAsync()
            });
        }
        /// <summary>
        /// lấy thông tin một môn học
        /// </summary>
        /// <param name="id">mã môn học</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectAsync(string id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _subject.GetSubjectAsync(id)
            });
        }
        /// <summary>
        /// thêm môn học
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostSubjectAsync(Subject subject)
        {
            if (ModelState.IsValid)
            {
                if (await _subject.AddSubjectAsync(subject))
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
        /// cập nhật môn học
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutSubjectAsync(Subject subject)
        {
            if (ModelState.IsValid)
            {
                if (await _subject.UpdateSubjectAsync(subject))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _subject.GetSubjectAsync(subject.Code_Subject)
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
        /// xóa môn học
        /// </summary>
        /// <param name="id">mã môn học</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjectAsync(string id)
        {
            if (await _subject.CheckDeleteSubjectAsync(id))
            {
                if (await _subject.DeleteSubjectAsync(id))
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
                retText = "đang tồn tại trong các bảng khác không được xóa"
            });
        }
    }
}
