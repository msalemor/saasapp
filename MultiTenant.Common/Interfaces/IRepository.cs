using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenant.Common.Interfaces
{
    public interface IRepository<T>
    {
        string Connection { get; set; }
        string DbUser { get; set; }
        string DbPassword { get; set; }
        Task<List<T>> GetAllAsync();
    }
}
