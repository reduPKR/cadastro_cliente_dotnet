using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cadastro_cliente.Models;

namespace cadastro_clientes.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Context _context;

        public ClientesController(Context context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var context = _context.Clientes.Include(c => c.sexo);
            return View(await context.ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.sexo)
                .Include(c => c.enderecos)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            foreach(var endereco in cliente.enderecos)
            {
                endereco.endereco = _context.Enderecos.Where(item => item.id == endereco.enderecoId).FirstOrDefault();
                endereco.endereco.cidade = _context.Cidades.Where(item => item.id == endereco.endereco.cidadeId).FirstOrDefault();
                endereco.endereco.cidade.estado = _context.Estados.Where(item => item.id == endereco.endereco.cidade.estadoId).FirstOrDefault();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            ViewData["sexo"] = new SelectList(_context.Sexo, "id", "genero");

            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nome,data_nascimento, sexoId")] Cliente cliente, 
                                                [Bind("id,cep,bairro,rua,numero,complemento")] Endereco endereco, 
                                                [Bind("cidade")] String cidade)
        {
            if (ModelState.IsValid)
            {
                Cidade cidade_obj = _context.Cidades.Where(item => item.nome == cidade).FirstOrDefault();
                endereco.cidadeId = cidade_obj.id;                

                _context.Add(cliente);
                _context.Add(endereco);
                await _context.SaveChangesAsync();

                vincular_endereco_cliente(cliente, endereco);

                return RedirectToAction(nameof(Index));
            }

            ViewData["sexo"] = new SelectList(_context.Sexo, "id", "genero");
            return View(cliente);
        }

        private async void vincular_endereco_cliente(Cliente cliente, Endereco endereco)
        {
            EnderecoCliente enderecoCliente = new EnderecoCliente
            {
                clienteId = cliente.id,
                enderecoId = endereco.id
            };

            _context.Add(enderecoCliente);
            await _context.SaveChangesAsync();
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            ViewData["sexo"] = new SelectList(_context.Sexo, "id", "genero");

            EnderecoCliente endereco = _context.EnderecoCliente.Where(item => item.clienteId == cliente.id).FirstOrDefault();
            endereco.endereco = _context.Enderecos.Where(item => item.id == endereco.enderecoId).FirstOrDefault();
            endereco.endereco.cidade = _context.Cidades.Where(item => item.id == endereco.endereco.cidadeId).FirstOrDefault();
            endereco.endereco.cidade.estado = _context.Estados.Where(item => item.id == endereco.endereco.cidade.estadoId).FirstOrDefault();

            cliente.enderecos = new List<EnderecoCliente>();
            cliente.enderecos.Add(endereco);

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nome,data_nascimento,sexoId")] Cliente cliente,
                                                      [Bind("cep,bairro,rua,numero,complemento")] Endereco endereco,
                                                      [Bind("endereco_id")] int endereco_id,
                                                      [Bind("cidade")] String cidade)
        {
            if (id != cliente.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);

                    Cidade cidade_obj = _context.Cidades.Where(item => item.nome == cidade).FirstOrDefault();
                    endereco.cidadeId = cidade_obj.id;
                    endereco.id = endereco_id;
                    _context.Update(endereco);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["sexoId"] = new SelectList(_context.Sexo, "id", "id", cliente.sexo.id);
            ViewData["sexo"] = new SelectList(_context.Sexo, "genero", "genero", cliente.sexo);
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Clientes
                .Include(c => c.sexo)
                .Include(c => c.enderecos)
                .FirstOrDefaultAsync(m => m.id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            foreach (var endereco in cliente.enderecos)
            {
                endereco.endereco = _context.Enderecos.Where(item => item.id == endereco.enderecoId).FirstOrDefault();
                endereco.endereco.cidade = _context.Cidades.Where(item => item.id == endereco.endereco.cidadeId).FirstOrDefault();
                endereco.endereco.cidade.estado = _context.Estados.Where(item => item.id == endereco.endereco.cidade.estadoId).FirstOrDefault();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.id == id);
        }
    }
}
