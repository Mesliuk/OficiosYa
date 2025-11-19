using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Domain.Entities
{
    public class PasswordResetToken
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Token { get; set; } = null!;
        public DateTime Expira { get; set; }
        public bool Usado { get; set; }


        public Usuario Usuario { get; set; } = null!;
    }
}
