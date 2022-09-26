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
    public interface IPointType
    {
        Task<List<PointType>> GetPointTypesAsync();//lay toan bo loai diem 
        Task<PointType> GetPointTypeAsync(int id_PointType);//lay thong tin mot loai diem
        Task<bool> AddPointTypeAsync(PointType pointType);//them loai diem
        Task<bool> UpdatePointType(PointType pointType);//cap nhat loai diem 
        Task<bool> DeletePoint(int id_Pointtype);//xoa loai diem
        Task<bool> CheckDeletePointTypeAsync(int id_pointtype);//kiem tra loai diem co xoa dc khong
    }
    public class PointTypeSvc:IPointType
    {
        private readonly DataContext _context;
        public PointTypeSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<PointType>> GetPointTypesAsync()
        {
            return await _context.PointTypes.ToListAsync();
        }
        public async Task<PointType> GetPointTypeAsync(int id_PointType)
        {
            return await _context.PointTypes.Where(x=>x.Id_PointType==id_PointType).FirstOrDefaultAsync();
        }
        public async Task<bool> AddPointTypeAsync(PointType pointType)
        {
            bool ret = false;
            try
            {
                await _context.PointTypes.AddAsync(pointType);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
        public async Task<bool> UpdatePointType(PointType pointType)
        {
            bool ret = false;
            try
            {
                _context.PointTypes.Update(pointType);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
        public async Task<bool> DeletePoint(int id_Pointtype)
        {
            bool ret = false;
            try
            {
                var point = await _context.Subject_Pointypes
                    .Where(x => x.Id_PointType == id_Pointtype).FirstOrDefaultAsync();
                _context.Subject_Pointypes.Remove(point);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
        public async Task<bool> CheckDeletePointTypeAsync(int id_pointtype)
        {
            bool ret = false;
            try
            {
                var student = await _context.Student_Points
                    .Where(x => x.Id_PointType == id_pointtype).FirstOrDefaultAsync();
                var subject=await _context.Subject_Pointypes.Where(x=>x.Id_PointType==id_pointtype).FirstOrDefaultAsync();
                if(student==null && subject==null)
                {
                    ret = true;
                }
                ret = false;
            }
            catch { }
            return ret;
        }
    }
}
