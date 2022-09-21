using SignupSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface ICourse
    {
        Task<List<Course>> GetCoursesAsync();//lay toan bo khoa hoc
        Task<Course> GetCourseAsync(string code_course);//lay thong tin nien khoa bang id
        Task<bool> AddCourseAsync(Course course);//them khoa hoc
        Task<bool> UpdateCourseAsync(Course course);//cap nhat khoa hoc
        Task<bool> DeleteCourseAsync(string code_Course);//xoa khoa hoc
        Task<bool> CheckDeleteCourseAsync(string code_course);
    }
    public class CourseSvc:ICourse
    {
        private readonly DataContext _context;
        public CourseSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Course>> GetCoursesAsync()
        {
            List<Course> courses = new List<Course>();
            courses = await _context.Courses.ToListAsync();
            return courses;
        }
        public async Task<Course> GetCourseAsync(string code_course)
        {
            return await _context.Courses.Where(x => x.Code_Course == code_course).FirstOrDefaultAsync();
        }
        public async Task<bool> AddCourseAsync(Course course)
        {
            bool ret = false;
            try
            {
                await _context.Courses.AddAsync(course);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> UpdateCourseAsync(Course course)
        {
            bool ret = false;
            try
            {
                _context.Courses.Update(course);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> DeleteCourseAsync(string code_Course)
        {
            bool ret = false;
            try
            {
                Course course = new Course();
                course = await _context.Courses.Where(x => x.Code_Course == code_Course).FirstOrDefaultAsync();
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> CheckDeleteCourseAsync(string code_course)
        {
            Class entity=await _context.Classes.Where(x=>x.Code_Course==code_course).FirstOrDefaultAsync();
            if (entity!=null)
            {
                return false;
            }
            return true;
        }
    }
}
