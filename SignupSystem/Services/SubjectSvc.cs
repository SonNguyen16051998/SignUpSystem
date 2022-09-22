using SignupSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface ISubject
    {
        Task<List<Subject>> GetSubjectsAsync();//lay toan bo mon hoc
        Task<Subject> GetSubjectAsync(string code_subject);//lay mon hoc bang ma mon hoc
        Task<bool> AddSubjectAsync(Subject subject);//them mon hoc
        Task<bool> UpdateSubjectAsync(Subject subject);//cap nhat mon hoc
        Task<bool> DeleteSubjectAsync(string Code_Subject);//xoa mon hoc
        Task<bool> CheckDeleteSubjectAsync(string Code_Subject);//kiem tra mon hoc duoc xoa khong
    }
    public class SubjectSvc:ISubject
    {
        private readonly DataContext _context;
        public SubjectSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Subject>> GetSubjectsAsync()
        {
            List<Subject> subjects = new List<Subject>();
            subjects = await _context.Subjects
                        .Include(x=>x.khoa)
                        .Include(x=>x.SubjectDepartment)
                        .ToListAsync();
            return subjects;
        }
        public async Task<Subject> GetSubjectAsync(string code_subject)
        {
            return await _context.Subjects.Where(x=>x.Code_Subject == code_subject).FirstOrDefaultAsync();
        }
        public async Task<bool> AddSubjectAsync(Subject subject)
        {
            bool ret = false;
            try
            {
                await _context.Subjects.AddAsync(subject);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> UpdateSubjectAsync(Subject subject)
        {
            bool ret = false;
            try
            {
                _context.Subjects.Update(subject);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> DeleteSubjectAsync(string Code_Subject)
        {
            bool ret = false;
            try
            {
                Subject subject = new Subject();
                subject = await _context.Subjects.Where(x => x.Code_Subject == Code_Subject).FirstOrDefaultAsync();
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> CheckDeleteSubjectAsync(string Code_Subject)
        {
            var studentPoint=await _context.Student_Points
                .Where(x=>x.Code_Subject==Code_Subject).FirstOrDefaultAsync();
            var subjectPointType = await _context.Subject_Pointypes
                .Where(x => x.Code_Subject.Equals(Code_Subject)).FirstOrDefaultAsync();
            var teachershedule=await _context.TeacherSchedules
                .Where(x=>x.Code_Subject==Code_Subject).FirstOrDefaultAsync();
            var teacher=await _context.Teachers.Where(x=>x.Code_MainSubject==Code_Subject).FirstOrDefaultAsync();
            if(studentPoint==null && subjectPointType==null && teachershedule==null && teacher==null)
            {
                return true;
            }
            return false;
        }
    }
}
