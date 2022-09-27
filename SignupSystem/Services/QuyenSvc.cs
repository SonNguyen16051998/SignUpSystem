using Microsoft.EntityFrameworkCore;
using SignupSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface IQuyen
    {
        Task<List<Quyen>> GetQuyensAsync();
        Task<Quyen> GetQuyenAsync(int id);
        Task<bool> DeleteQuyenAsync(int id);
        Task<int> AddQuyenAsync(Quyen quyen);
        Task<int> UpdateQuyenAsync(Quyen quyen);
    }
    public class QuyenSvc:IQuyen
    {
        private readonly DataContext _context;
        public QuyenSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Quyen>> GetQuyensAsync()
        {
            return await _context.Quyens.ToListAsync();
        }

        public async Task<Quyen> GetQuyenAsync(int id)
        {
            return await _context.Quyens.Where(x => x.Id_Quyen == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteQuyenAsync(int id)
        {
            bool ret = false;
            try
            {
                var role_Quyens = await _context.Role_Quyens.Where(x => x.Id_Quyen == id).ToListAsync();
                foreach (var item in role_Quyens)//xóa quyền ở bảng role_quyền
                {
                    _context.Role_Quyens.Remove(item);
                }
                Quyen quyen = new Quyen();
                quyen = await _context.Quyens.Where(x => x.Id_Quyen == id).FirstOrDefaultAsync();
                _context.Quyens.Remove(quyen);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }

        public async Task<int> AddQuyenAsync(Quyen quyen)
        {
            int ret = 0;
            try
            {
                await _context.Quyens.AddAsync(quyen);
                await _context.SaveChangesAsync();
                ret = quyen.Id_Quyen;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public async Task<int> UpdateQuyenAsync(Quyen quyen)
        {
            int ret = 0;
            try
            {
                _context.Quyens.Update(quyen);
                await _context.SaveChangesAsync();
                ret = quyen.Id_Quyen;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
    }
}
