using Microsoft.EntityFrameworkCore;
using SignupSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface IRole
    {
        Task<List<Role>> GetRolesAsync();
        Task<Role> GetRoleAsync(int id);
        Task<bool> DeleteRoleAsync(int id);
        Task<int> AddRoleAsync(Role role);
        Task<int> UpdateRoleAsync(Role role);
    }
    public class RoleSvc:IRole
    {
        private readonly DataContext _context;
        public RoleSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRoleAsync(int id)
        {
            return await _context.Roles.Where(x => x.Id_Role == id).FirstOrDefaultAsync(); ;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            bool ret = false;
            try
            {
                List<User> nguoiDungs = new List<User>();
                nguoiDungs = await _context.Users.Where(x => x.Id_Role == id).ToListAsync();
                foreach (var item in nguoiDungs)//chuyen role cua nguoi dung về mặc định
                {
                    item.Id_Role = 1;
                    _context.Users.Update(item);

                }
                var role_Quyens = await _context.Role_Quyens.Where(x => x.Id_Role == id).ToListAsync();
                foreach (var item in role_Quyens)//xóa các quyền của role
                {
                    _context.Role_Quyens.Remove(item);
                }

                var role = await _context.Roles.Where(x => x.Id_Role == id).FirstOrDefaultAsync();
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
                ret = true;

            }
            catch { }
            return ret;
        }

        public async Task<int> AddRoleAsync(Role role)
        {
            int ret = 0;
            try
            {
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
                ret = role.Id_Role;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }

        public async Task<int> UpdateRoleAsync(Role role)
        {
            int ret = 0;
            try
            {
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
                ret = role.Id_Role;
            }
            catch
            {
                ret = 0;
            }
            return ret;
        }
    }
}
