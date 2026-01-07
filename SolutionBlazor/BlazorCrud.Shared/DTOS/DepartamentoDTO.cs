using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared.DTOS
{
    public class DepartamentoDTO
    {
        public int IdDepartamento { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
