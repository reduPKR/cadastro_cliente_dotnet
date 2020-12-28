using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_cliente.Models
{
    [Table("cidades")]
    public class Cidade
    {
        [Key()]
        public int id { get; set; }

        [Column("nome")]
        public string nome { get; set; }

        [ForeignKey("Estado")]
        [Column("id_estado")]
        public int estadoId { get; set; }
        public virtual Estado estado { get; set; }

        public virtual List<Endereco> enderecos { get; set; }
    }
}
