using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Application.DTO
{
    public class UsuarioDTO
    {
        public int id { get; set; }
        public string nombre { get; set; } = string.Empty;
        public DateTime creacion { get; set; }
        public string usuario { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public bool status { get; set; }
        public string? acciones { get; set; }
    }
}
