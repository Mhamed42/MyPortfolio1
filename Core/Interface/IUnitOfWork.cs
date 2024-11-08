using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface
{
  public interface IUnitOfWork<T> where T : class
    {
        IGenericRepository<T> Repository { get; }
        void Save();

    }
}
