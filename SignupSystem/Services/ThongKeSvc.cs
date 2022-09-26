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
        Task<List<Fee>> GetFeesAsync();//lay toan bo danh sach hoc vien da dong hoc phi theo lop
        Task<List<Fee>> GetFeesByDate(DateTime date);//lấy toàn bộ doanh thu trong ngày

    }
    public class ThongKeSvc:IThongKe
    {
        private readonly DataContext _context;
        public ThongKeSvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Fee>> GetFeesByDate(DateTime date)
        {

            return await _context.Fees.Where(x => x.PaymentDate.Year == date.Year && x.PaymentDate.Month == date.Month
                    && x.PaymentDate.Day == date.Day).ToListAsync();
        }

        public async Task<List<Fee>> GetFeesAsync()
        {
            return await _context.Fees.ToListAsync();
        }
    }
}
