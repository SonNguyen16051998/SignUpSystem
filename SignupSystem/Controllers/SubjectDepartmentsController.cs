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
    public class SubjectDepartmentsController : Controller
    {
        private readonly ISubjectDepartment _subjectDepartment;
        public SubjectDepartmentsController(ISubjectDepartment subjectDepartment)
        {
            _subjectDepartment = subjectDepartment;
        }
        /// <summary>
        /// lấy toàn bộ tổ bộ môn
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _subjectDepartment.GetSubjectDepartmentsAsync()
            });
        }
        /// <summary>
        /// lấy thông tin một tổ bộ môn
        /// </summary>
        /// <param name="id">mã bộ môn</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(new
            {
                retCode = 1,
                retText = "successfuly",
                data = await _subjectDepartment.GetSubjectDepartmentAsync(id)
            });
        }
        /// <summary>
        /// thêm tổ bộ môn
        /// </summary>
        /// <param name="subjectDepartment"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(SubjectDepartment subjectDepartment)
        {
            if (ModelState.IsValid)
            {
                if (await _subjectDepartment.AddSubjectDepartmentAsync(subjectDepartment))
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
        /// cập nhật tổ bộ môn
        /// </summary>
        /// <param name="subjectDepartment"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(SubjectDepartment subjectDepartment)
        {
            if (ModelState.IsValid)
            {
                if (await _subjectDepartment.UpdateSubjectDepartmentAsync(subjectDepartment))
                {
                    return Ok(new
                    {
                        retCode = 1,
                        retText = "successfuly",
                        data = await _subjectDepartment.GetSubjectDepartmentAsync(subjectDepartment.Id_SubjectDerpartment)
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
        /// xóa tổ bộ môn
        /// </summary>
        /// <param name="id">mã bộ môn</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _subjectDepartment.CheckDeleteDepartment(id))
            {
                if (await _subjectDepartment.DeleteSubjectDepartmentAsync(id))
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
