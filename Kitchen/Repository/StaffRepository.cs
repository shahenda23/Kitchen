﻿using Kitchen.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.Repository
{
    public class StaffRepository: IStaffRepository
    {
        KitchenContext context;
        public StaffRepository(KitchenContext ctx) 
        {
            context=ctx;
        }

        public void Add(Staff obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            context.Staff.Remove(GetById(id));
        }

        public void Edit(Staff obj)
        {
            Staff staff=GetById(obj.Id);

            staff.Name = obj.Name;
            staff.Position = obj.Position;
            staff.Salary = obj.Salary;
            staff.AccountId = obj.AccountId;
        }

        public List<Staff> GetAll(string inclues = "")
        {
            if (inclues == "")
            {
                return context.Staff.ToList();
            }
            else
            {
                return context.Staff.Include(inclues).ToList();
            }
            
        }

        public List<Staff> GetAll()
        {
            throw new NotImplementedException();
        }

        public Staff GetById(int id)
        {
            return context.Staff.FirstOrDefault(s=> s.Id==id);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
