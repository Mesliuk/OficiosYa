using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Services
{
    public class GeoService : IGeoService
    {
        public double CalcularDistancia(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371; // Radio de la Tierra en kilómetros
            double dLat = (lat2 - lat1);
            double dLon = (lon2 - lon1);
            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1 * Math.PI / 180) *
                Math.Cos(lat2 * Math.PI / 180) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distancia = R * c;
            return distancia;
        }
    }
}
