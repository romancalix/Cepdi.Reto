using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cepdi.Domain.Entities
{
    public class Medicamentos
    {
        public int IDMEDICAMENTO { get; set; }
        public string? CONCENTRACION { get; set; }
        public int IDFORMAFARMACEUTICA { get; set; }
        public double PRECIO { get; set; }
        public string? PRESENTACION { get; set; }
        public bool HABILITADO { get; set; }
        public string? ACCIONES { get; set; }
    }
}
