﻿using DC.DataAccess.Repository.IRepository;
using DC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.DataAccess.Repository
{
    public class UnitOfWork : IUnitofWork
    {
        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);

        }
        public ICategoryRepository Category { get; private set; }
         public ICoverTypeRepository CoverType { get; private set; }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}