using System;
using OficiosYa.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Interfaces
{
    public interface IOficioRepository
    {
        Task<IEnumerable<Oficio>> GetAllAsync();
        Task CreateAsync(Oficio oficio);
    }
}
