using SignupSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface IThongKe
    {
        Task<List<Fee>> GetFeesAsync(int id_Class);//lay toan bo danh sach hoc vien da dong hoc phi theo lop

    }
    public class ThongKeSvc
    {
       /* private readonly DataContext _context;
        public ThongKeSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Fee>> GetFeesAsync(int id_Class)
        {

        }*/
    }
}
