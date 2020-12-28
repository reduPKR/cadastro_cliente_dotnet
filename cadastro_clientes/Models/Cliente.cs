using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_cliente.Models
{
    [Table("clientes")]
    public class Cliente
    {
        [Key()]
        public int id { get; set; }

        [Column("nome")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Column("data_nascimento")]
        [Display(Name = "Data de nascimento")]
        public DateTime data_nascimento { get; set; }

        [ForeignKey("Sexo")]
        [Column("sexo_id")]
        [Display(Name = "Genero")]
        public int sexoId { get; set; }
        public virtual Sexo sexo { get; set; }

        public virtual List<EnderecoCliente> enderecos { get; set; }
    }
}
