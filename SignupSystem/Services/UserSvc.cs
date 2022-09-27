using Microsoft.EntityFrameworkCore;
using SignupSystem.Models;
using SignupSystem.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SignupSystem.Services
{
    public interface IUser
    {
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<User> LoginAsync(ViewLogin login);//ddawng nhap
        Task<bool> isPass(string email, string pass);//kiem tra pass dung hay khong
        Task<bool> isEmail(string email);//kiem tra ton tai cua email
        Task<bool> QuenMatKhauAsync(ViewQuenMatKhau quenMatKhau);//đổi mật khẩu mới khi chọn chức năng quên mật khẩu
        Task<bool> ChangePassAsync(ViewDoiMatKhau changePass);
    }
    public class UserSvc:IUser      
    {
        private readonly DataContext _context;
        public UserSvc(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users.Where(x => x.Id_User == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            bool ret = false;
            try
            {
                var user= await _context.Users.Where(x=>x.Id_User == id).FirstOrDefaultAsync();
                var user_role = await _context.User_Roles.Where(x => x.Ma_NguoiDung == id).FirstOrDefaultAsync();
                _context.User_Roles.Remove(user_role);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            bool ret = false;
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            bool ret = false;
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        public async Task<User> LoginAsync(ViewLogin login)
        {
            User user = await _context.Users.Where(x => x.Email == login.Email
                  && x.PassWord == Helpers.MaHoaHelper.Mahoa(login.PassWord)).FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> isPass(string email, string pass)
        {
            bool ret = false;
            try
            {
                var nguoiDung = await _context.Users.Where(x => x.Email == email
                && x.PassWord == Helpers.MaHoaHelper.Mahoa(pass))
                    .FirstOrDefaultAsync();
                if (nguoiDung != null)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
        public async Task<bool> isEmail(string email)
        {
            bool ret = false;
            try
            {
                var nguoiDung = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
                if (nguoiDung != null)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        public async Task<bool> QuenMatKhauAsync(ViewQuenMatKhau quenMatKhau)
        {
            bool result = false;
            try
            {
                var nguoiDung = await _context.Users.Where(x => x.Email == quenMatKhau.Email).FirstOrDefaultAsync();
                nguoiDung.PassWord = Helpers.MaHoaHelper.Mahoa(quenMatKhau.NewPass);
                _context.Users.Update(nguoiDung);
                await _context.SaveChangesAsync();
                result = true;
            }
            catch { }
            return result;
        }
        public async Task<bool> ChangePassAsync(ViewDoiMatKhau changePass)
        {
            bool result = false;
            try
            {
                var nguoiDung = await _context.Users.Where(x => x.Email == changePass.email).FirstOrDefaultAsync();
                nguoiDung.PassWord = Helpers.MaHoaHelper.Mahoa(changePass.newPassword);
                _context.Users.Update(nguoiDung);
                await _context.SaveChangesAsync();
                result = true;
            }
            catch { }
            return result;
        }
    }
}
