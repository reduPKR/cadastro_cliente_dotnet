using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_cliente.Models
{
    [Table("sexo")]
    public class Sexo
    {
        [Key()]
        public int id { get; set; }

        [Column("genero")]
        public string genero { get; set; }

        public virtual List<Cliente> clientes { get; set; }
    }
}
