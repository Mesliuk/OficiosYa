using OficiosYa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Services
{
    public class ClienteService
    {
        private readonly IUsuarioRepository _repo;

        public ClienteService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Ejecutar(string nombre, string email, string password)
        {
            var existe = await _repo.GetByEmailAsync(email);
            if (existe != null) throw new Exception("Ya existe un usuario con ese email.");

            var usuario = new Usuario(nombre, email, password);
            await _repo.AddAsync(usuario);

            return usuario.Id;
        }
    }

}
