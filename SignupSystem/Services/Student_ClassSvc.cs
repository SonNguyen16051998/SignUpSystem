using SignupSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface IStudentClass
    {
        Task<bool> DeleteStudentFromClass(int id_Student);//xoa hoc vien khoi lop hoc
    }
    public class Student_ClassSvc
    {
        private readonly DataContext _context;
        public Student_ClassSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteStudentFromClass(int id_Student)
        {
            bool ret = false;
            try
            {
                Student_Class student_Class = new Student_Class();
                student_Class = await _context.Student_Classes
                                .Where(x => x.Id_Student == id_Student)
                                .Include(x=>x.Class)
                                .FirstOrDefaultAsync();
                _context.Student_Classes.Remove(student_Class);
                Class entity = new Class();
                entity = await _context.Classes.Where(x => x.Id_Class == student_Class.Class.Id_Class).FirstOrDefaultAsync();
                entity.QtyStudent -= 1;
                _context.Classes.Update(entity);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
    }
}
