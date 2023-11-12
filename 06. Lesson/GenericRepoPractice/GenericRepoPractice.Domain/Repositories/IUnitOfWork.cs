using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericRepoPractice.Domain.Repositories
{
    public interface IUnitOfWork
    {
        ITeacherRepository TeacherRepository { get; }
        IGroupRepository GroupRepository { get; }
        int Save();
    }
}
