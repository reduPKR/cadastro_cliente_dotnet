using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cadastro_cliente.Models
{
    public class Context: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseMySQL("server=localhost;uid=root; pwd=admin;persistsecurityinfo=True;database=cadastro");
        }

        public DbSet<Estado> Estados { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Sexo> Sexo { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<EnderecoCliente> EnderecoCliente { get; set; }
    }
}
