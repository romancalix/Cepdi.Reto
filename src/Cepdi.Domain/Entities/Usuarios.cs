using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Domain.Entities
{
    public class Usuarios
    {
        public int IDUSUARIO { get; set; }
        public string NOMBRE { get; set; }
        public DateTime FECHACREACION { get; set; }
        public string USUARIO { get; set; }
        public string PASSWORD { get; set; }
        public bool ESTATUS { get; set; }
        public string ACCIONES { get; set; }

    }
}
