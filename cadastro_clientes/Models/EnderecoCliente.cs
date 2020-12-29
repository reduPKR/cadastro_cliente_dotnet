using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_cliente.Models
{
    [Table("endereco_cliente")]
    public class EnderecoCliente
    {
        [Key()]
        public int id { get; set; }

        [ForeignKey("Endereco")]
        [Column("endereco_id")]
        public int enderecoId { get; set; }
        public virtual Endereco endereco { get; set; }

        [ForeignKey("Cliente")]
        [Column("cliente_id")]
        public int clienteId { get; set; }
        public virtual Cliente cliente { get; set; }
    }
}
