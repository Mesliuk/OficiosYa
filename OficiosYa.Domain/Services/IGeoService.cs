using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Services
{
    public interface IGeoService
    {
        double CalcularDistancia(double lat1, double lon1, double lat2, double lon2);
    }
}
