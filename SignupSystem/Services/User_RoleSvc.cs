using Microsoft.EntityFrameworkCore;
using SignupSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface IUser_Role
    {
        Task<bool> AddOrUpdateNguoiDung_RoleAsync(User_Role nguoiDung_Roles);
        Task<User_Role> GetNguoiDung_RoleAsync(int id_nguoiDung);//Hiển thị role đã chọn của người dùng
    }
    public class User_RoleSvc:IUser_Role
    {
        private readonly DataContext _context;
        public User_RoleSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOrUpdateNguoiDung_RoleAsync(User_Role nguoiDung_Role)
        {
            bool ret = false;
            try
            {
                
                User_Role user_role = new User_Role();
                user_role = await _context.User_Roles.Where(x => x.Ma_NguoiDung == nguoiDung_Role.Ma_NguoiDung).FirstOrDefaultAsync();//
                
                if (user_role != null)//kiểm tra người dùng đã có role chua
                {
                    if (nguoiDung_Role.Ma_Role == null)
                    {
                        user_role.Ma_Role = 1;
                        _context.User_Roles.Update(user_role);
                    }
                    else
                    {
                        user_role.Ma_Role = nguoiDung_Role.Ma_Role;
                        _context.User_Roles.Update(user_role);
                    }
                }
                else//chưa có role sẽ thêm role vào cho người dùng
                {
                    await _context.User_Roles.AddAsync(nguoiDung_Role);
                }
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        public async Task<User_Role> GetNguoiDung_RoleAsync(int id_nguoiDung)
        {
            return await _context.User_Roles.Where(x => x.Ma_NguoiDung == id_nguoiDung)
                            .Include(x => x.Role).FirstOrDefaultAsync(); ;
        }

    }
}
