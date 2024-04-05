using System;
using System.Threading.Tasks;

namespace library.Core
{
     public interface IUnitOfWork
     {
         Task CompleteAsync();
     }
    
}
