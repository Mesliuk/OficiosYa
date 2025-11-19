using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Utils
{
    public static class GeoDistanceCalculator
    {
        private const double EarthRadiusKm = 6371.0;

        /// <summary>
        /// Calcula la distancia entre dos puntos usando la fórmula Haversine.
        /// </summary>
        public static double CalcularDistancia(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = GradosARadianes(lat2 - lat1);
            double dLon = GradosARadianes(lon2 - lon1);

            lat1 = GradosARadianes(lat1);
            lat2 = GradosARadianes(lat2);

            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1) * Math.Cos(lat2) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c; // Devuelve la distancia en km
        }

        private static double GradosARadianes(double grados)
        {
            return grados * Math.PI / 180;
        }
    }
}

