using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Data.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Tarefa), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada

            var tarefa = await _context.Tarefas.FindAsync(id);

            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }


        [HttpGet("ObterTodos")]
        [ProducesResponseType(typeof(List<Tarefa>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var tarefas = await _context.Tarefas.ToListAsync();

            return Ok(tarefas);
        }


        [HttpGet("ObterPorTitulo")]
        [ProducesResponseType(typeof(List<Tarefa>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefas = await _context.Tarefas.Where(p => p.Titulo.ToUpper().Contains(titulo.ToUpper())).ToListAsync();

            return Ok(tarefas);
        }

        [HttpGet("ObterPorData")]
        [ProducesResponseType(typeof(List<Tarefa>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPorData(DateTime data)
        {
            var tarefas = await _context.Tarefas.Where(x => x.Data.Date == data.Date).ToListAsync();

            return Ok(tarefas);
        }

        [HttpGet("ObterPorStatus")]
        [ProducesResponseType(typeof(List<Tarefa>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefas = await _context.Tarefas.Where(x => x.Status == status).ToListAsync();

            return Ok(tarefas);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Tarefa), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
                
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Tarefa), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = await _context.Tarefas.FindAsync(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            await _context.SaveChangesAsync();

            return Ok(tarefaBanco);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deletar(int id)
        {
            var tarefaBanco = await _context.Tarefas.FindAsync(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Tarefas.Remove(tarefaBanco);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
