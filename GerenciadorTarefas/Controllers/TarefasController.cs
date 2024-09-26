// Controllers/TarefasController.cs
using GerenciadorTarefas.Data;
//using GerenciadorTarefas.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    private readonly TarefaRepository _repository;

    public TarefasController(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        _repository = new TarefaRepository(connectionString);
    }

    // Listar Tarefas
    [HttpGet]
    public ActionResult<IEnumerable<Tarefa>> GetTarefas()
    {
        return _repository.GetAll();
    }

    // Criar Tarefa
    [HttpPost]
    public IActionResult PostTarefa(Tarefa tarefa)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _repository.Add(tarefa);
        return CreatedAtAction(nameof(GetTarefas), new { id = tarefa.Id }, tarefa);
    }

    // Atualizar Status da Tarefa
    [HttpPut("{id}")]
    public IActionResult PutTarefaStatus(int id, bool concluida)
    {
        _repository.UpdateStatus(id, concluida);
        return NoContent();
    }

    // Deletar Tarefa
    [HttpDelete("{id}")]
    public IActionResult DeleteTarefa(int id)
    {
        _repository.Delete(id);
        return NoContent();
    }
}

