using GenericRepoPractice.DataAccessLayer.Context;
using GenericRepoPractice.Domain.Entities;
using GenericRepoPractice.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepoPractice.DataAccessLayer.Implementation
{
    public class TeacherRepository:GenericRepository<Teacher>,ITeacherRepository
    {
        public TeacherRepository(AppDbContext context) : base(context)
        {
            
        }
    }
}
