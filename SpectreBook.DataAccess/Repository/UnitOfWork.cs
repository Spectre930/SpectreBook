﻿using SpectreBook.DataAccess.Repository.IRepository;
using SpectreBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpectreBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDBContext _db;
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public UnitOfWork(AppDBContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
