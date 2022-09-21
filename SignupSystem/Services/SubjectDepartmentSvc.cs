using SignupSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface ISubjectDepartment
    {
        Task<List<SubjectDepartment>> GetSubjectDepartmentsAsync();//lay toan bo SubjectDepartment-khoi
        Task<SubjectDepartment> GetSubjectDepartmentAsync(int id);//lay mot to bo mon
        Task<bool> AddSubjectDepartmentAsync(SubjectDepartment subjectDepartment);//them SubjectDepartment 
        Task<bool> UpdateSubjectDepartmentAsync(SubjectDepartment subjectDepartment);//cap nhat SubjectDepartment
        Task<bool> DeleteSubjectDepartmentAsync(int id_subjectDepartment);//xoa subjectdepartment
        Task<bool> CheckDeleteDepartment(int id_subjectDepartment);
    }
    public class SubjectDepartmentSvc:ISubjectDepartment
    {
        private readonly DataContext _context;
        public SubjectDepartmentSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<SubjectDepartment>> GetSubjectDepartmentsAsync()
        {
            List<SubjectDepartment> SubjectDepartment = new List<SubjectDepartment>();
            SubjectDepartment = await _context.SubjectDepartments.ToListAsync();
            return SubjectDepartment;
        }
        public async Task<SubjectDepartment> GetSubjectDepartmentAsync(int id)
        {
            return await _context.SubjectDepartments.Where(x => x.Id_SubjectDerpartment == id).FirstOrDefaultAsync();
        }
        public async Task<bool> AddSubjectDepartmentAsync(SubjectDepartment SubjectDepartment)
        {
            bool ret = false;
            try
            {
                await _context.SubjectDepartments.AddAsync(SubjectDepartment);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> UpdateSubjectDepartmentAsync(SubjectDepartment SubjectDepartment)
        {
            bool ret = false;
            try
            {
                _context.SubjectDepartments.Update(SubjectDepartment);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> DeleteSubjectDepartmentAsync(int id_subjectDepartment)
        {
            bool ret = false;
            try
            {
                SubjectDepartment subdepartment = new SubjectDepartment();
                subdepartment = await _context.SubjectDepartments.Where(x => x.Id_SubjectDerpartment == id_subjectDepartment).FirstOrDefaultAsync();
                _context.SubjectDepartments.Remove(subdepartment);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> CheckDeleteDepartment(int id_subjectDepartment)
        {
            Subject subject=await _context.Subjects
                .Where(x=>x.Id_SubjectDerpartment==id_subjectDepartment).FirstOrDefaultAsync();
            if(subject==null)
            {
                return true;
            }
            return false;
        }
    }
}
