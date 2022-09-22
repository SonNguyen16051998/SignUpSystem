using SignupSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignupSystem.Services
{
    public interface IHoliday
    {
        Task<List<HolidaySchedule>> GetHolidaySchedulesAsync();//lay toan bo lich nghi
        Task<HolidaySchedule> GetHolidayScheduleAsync(int id_holiday);//lay mot lich nghi
        Task<bool> AddHolidayAsync(HolidaySchedule holiday);//them lich nghi
        Task<bool> UpdateHolidayAsync(HolidaySchedule holiday);//cap nhat lich nghi
        Task<bool> DeleteHolidayAsync(int id_holiday);//xoa lich nghi
    }
    public class HolidaySvc:IHoliday
    {
        private readonly DataContext _context;
        public HolidaySvc(DataContext context)
        {
            _context = context;
        }
        public async Task<List<HolidaySchedule>> GetHolidaySchedulesAsync()
        {
            return await _context.HolidaySchedules.ToListAsync();
        }
        public async Task<HolidaySchedule> GetHolidayScheduleAsync(int id_holiday)
        {
            return await _context.HolidaySchedules.Where(x => x.Id_Holiday == id_holiday)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> AddHolidayAsync(HolidaySchedule holiday)
        {
            bool ret = false;
            try
            {
                await _context.HolidaySchedules.AddAsync(holiday);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
        public async Task<bool> UpdateHolidayAsync(HolidaySchedule holiday)
        {
            bool ret = false;
            try
            {
                _context.HolidaySchedules.Update(holiday);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
        public async Task<bool> DeleteHolidayAsync(int id_holiday)
        {
            bool ret = false;
            try
            {
                var Holiday = await _context.HolidaySchedules
                    .Where(x => x.Id_Holiday == id_holiday).FirstOrDefaultAsync();
                _context.HolidaySchedules.Remove(Holiday);
                await _context.SaveChangesAsync();
                ret = true;
            }
            catch { }
            return ret;
        }
    }
}
