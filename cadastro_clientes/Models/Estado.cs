using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_cliente.Models
{
    [Table("estados")]
    public class Estado
    {
        [Key()]
        public int id { get; set; }

        [Column("nome")]
        public string nome { get; set; }

        [Column("uf")]
        public string uf { get; set; }

        public virtual List<Cidade> cidades { get; set; }
    }
}
