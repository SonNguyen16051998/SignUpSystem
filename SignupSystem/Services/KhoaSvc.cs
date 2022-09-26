using SignupSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface IKhoa
    {
        Task<List<Khoa>> GetKhoasAsync();//lay toan bo khoa-khoi
        Task<Khoa> GetKhoaAsync(int id_Khoa);
        Task<bool> AddKhoaAsync(Khoa khoa);//them khoa vd:khoa CNTT
        Task<bool> UpdateKhoaAsync(Khoa khoa);//cap nhat khoa
        Task<bool> DeleteKhoaAsync(int id_Khoa);
    }
    public class KhoaSvc:IKhoa
    {
        private readonly DataContext _context;
        public KhoaSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Khoa>> GetKhoasAsync()
        {
            List<Khoa> khoa = new List<Khoa>();
            khoa = await _context.Khoa.ToListAsync();
            return khoa;
        }
        public async Task<Khoa> GetKhoaAsync(int id_Khoa)
        {
            return await _context.Khoa.Where(x=>x.Id_Khoa==id_Khoa).FirstOrDefaultAsync();
        }
        public async Task<bool> AddKhoaAsync(Khoa khoa)
        {
            bool ret = false;
            try
            {
                await _context.Khoa.AddAsync(khoa);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> UpdateKhoaAsync(Khoa khoa)
        {
            bool ret = false;
            try
            {
                _context.Khoa.Update(khoa);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
            }
            return ret;
        }
        public async Task<bool> DeleteKhoaAsync(int id_Khoa)
        {
            
            bool ret = false;
            try
            {
                Khoa khoa = await _context.Khoa.Where(x => x.Id_Khoa == id_Khoa).FirstOrDefaultAsync();
                _context.Khoa.Remove(khoa);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
    }
}
