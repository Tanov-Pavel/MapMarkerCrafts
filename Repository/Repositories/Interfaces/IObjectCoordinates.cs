using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IObjectCoordinates
    {
        void save(Coordinate[] coordinatesArray);
    }
}

