using SignupSystem.Models;
using SignupSystem.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface ISubjectPointType
    {
        Task<List<Subject_PointType>> GetSubject_PointTypesAsync();//lay toan bo loai diem cua tung mon hoc
        Task<Subject_PointType> GetSubject_PointTypeAsync(int id_PointType);//lay mot loai diem cua mon hoc
        Task<bool> AddSubject_Point(Subject_PointType subjectPointType);//them loai diem cho mon hoc
        Task<bool> UpdateSubjectPoint(Subject_PointType subjectPointType);//cap nhat loai diem cua mon hoc
        Task<bool> DeleteSubjectPoint(int id_subjectpoint);//xoa loai diem khoi mon hoc
    }
    public class Subject_PointTypeSvc:ISubjectPointType
    {
        private readonly DataContext _context;
        public Subject_PointTypeSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Subject_PointType>> GetSubject_PointTypesAsync()
        {
            return await _context.Subject_Pointypes
                .Include(x=>x.Course)
                .Include(x=>x.Subject)
                .Include(x=>x.PointType)
                .ToListAsync();
        }
        public async Task<Subject_PointType> GetSubject_PointTypeAsync(int id_PointType)
        {
            return await _context.Subject_Pointypes.Where(x => x.Id_PointType == id_PointType)
                .Include(x => x.Course)
                .Include(x => x.Subject)
                .Include(x => x.PointType)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> AddSubject_Point(Subject_PointType subjectPointType)
        {
            bool ret = false;
            try
            {
                await _context.Subject_Pointypes.AddAsync(subjectPointType);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
        public async Task<bool> UpdateSubjectPoint(Subject_PointType subjectPointType)
        {
            bool ret = false;
            try
            {
                _context.Subject_Pointypes.Update(subjectPointType);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
        public async Task<bool> DeleteSubjectPoint(int id_subjectpoint)
        {
            bool ret = false;
            try
            {
                var subjectpoint = await _context.Subject_Pointypes
                    .Where(x => x.Id_PointType == id_subjectpoint).FirstOrDefaultAsync();
                _context.Subject_Pointypes.Remove(subjectpoint);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
       
    }
}
