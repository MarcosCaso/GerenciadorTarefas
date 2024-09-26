//using GerenciadorTarefas.Models;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;


namespace GerenciadorTarefas.Data
{
    public class TarefaRepository
    {
        private readonly string _connectionString;

        public TarefaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Listar Tarefas
        public List<Tarefa> GetAll()
        {
            var tarefas = new List<Tarefa>();

            using (var con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Tarefas", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tarefas.Add(new Tarefa
                    {
                        Id = (int)reader["Id"],
                        Titulo = reader["Titulo"].ToString(),
                        Descricao = reader["Descricao"].ToString(),
                        DataVencimento = (DateTime)reader["DataVencimento"],
                        Concluida = (bool)reader["Concluida"]
                    });
                }
            }

            return tarefas;
        }

        // Criar Tarefa
        public void Add(Tarefa tarefa)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Tarefas (id, Titulo, Descricao, DataVencimento, Concluida) VALUES (@id, @Titulo, @Descricao, @DataVencimento, @Concluida)", con);

                cmd.Parameters.AddWithValue("@id", tarefa.Id);
                cmd.Parameters.AddWithValue("@Titulo", tarefa.Titulo);
                cmd.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
                cmd.Parameters.AddWithValue("@DataVencimento", tarefa.DataVencimento);
                cmd.Parameters.AddWithValue("@Concluida", tarefa.Concluida);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Atualizar Status
        public void UpdateStatus(int id, bool concluida)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Tarefas SET Concluida = @Concluida WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Concluida", concluida);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Deletar Tarefa
        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Tarefas WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
