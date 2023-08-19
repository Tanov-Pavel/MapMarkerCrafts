using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IGeometricRepositories
    {
        Task AddPointAsync(double latitude, double longitude);
    }
}
