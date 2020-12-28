using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_cliente.Models
{
    [Table("enderecos")]
    public class Endereco
    {
        [Key()]
        public int id { get; set; }

        [Column("cep")]
        public string cep { get; set; }

        [Column("bairro")]
        public string bairro { get; set; }

        [Column("rua")]
        public string rua { get; set; }

        [Column("numero")]
        public int numero { get; set; }

        [Column("complemento")]
        public string complemento { get; set; }

        [ForeignKey("Cidade")]
        [Column("cidade_id")]
        public int cidadeId { get; set; }
        public virtual Cidade cidade { get; set; }

        public virtual List<EnderecoCliente> clientes { get; set; }
    }
}
