using Microsoft.AspNetCore.Mvc;
using SignupSystem.Models;
using SignupSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignupSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectDepartmentsController : Controller
    {
        private readonly ISubjectDepartment _subjectDepartment;
        public SubjectDepartmentsController(ISubjectDepartment subjectDepartment)
        {
            _subjectDepartment = subjectDepartment;
        }
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
        [HttpPost]
        public async Task<IActionResult> PostAsync(SubjectDepartment subjectDepartment)
        {
            if(ModelState.IsValid)
            {
                if(await _subjectDepartment.AddSubjectDepartmentAsync(subjectDepartment))
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
        public async Task<IActionResult> UpdateAsync(SubjectDepartment subjectDepartment)
        {
            if(ModelState.IsValid)
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
        [HttpDelete("{id")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if(await _subjectDepartment.CheckDeleteDepartment(id))
            {
                if(await _subjectDepartment.DeleteSubjectDepartmentAsync(id))
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
