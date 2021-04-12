using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using System;

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Repositories
{
    public class FuncionariosRepository : IFuncionariosRepository
    {
        #region String de Conexão do Banco de Dados
        //String de Conexão com o Banco de Dados
        private string StringConexao = "Data Source=DESKTOP-9573JRK; initial catalog=T_Peoples; user=sa; pwd=senai@132;";
        #endregion

        #region AtualizarIdCorpo - Atualizar Funcionário passando o seu id pelo o corpo
        public void AtualizarIdCorpo(FuncionariosDomain funcionarios)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryUpdateIdBody = "UPDATE Funcionarios SET nomeDoFuncionario = @nome where idFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdBody, con))
                {
                    cmd.Parameters.AddWithValue("@ID", funcionarios.idFuncionario);

                    cmd.Parameters.AddWithValue("@nome", funcionarios.nome);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #endregion

        #region AtualizarIdUrl - Atualiza o Funciopnário passando o seu id pela a URL
        public void AtualizarIdUrl(int id, FuncionariosDomain funcionarios)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryUpdateIdUrl = "UPDATE Funcionarios SET @Nome WHERE idFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryUpdateIdUrl, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Nome", funcionarios.nome);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region BuscarPorId - Busca o Funcionário pelo o Id
        public FuncionariosDomain BuscarPorId(int id)
        {
            using(SqlConnection con = new SqlConnection(StringConexao))
            {
                string querySelectById = "select idFuncionario, nomeDoFuncionario, sobrenomeDoFuncionario from Funcionarios where idFuncionario = @ID";

                con.Open();

                SqlDataReader rdr;

                using(SqlCommand cmd = new SqlCommand(querySelectById, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        FuncionariosDomain FuncionariosBuscado = new FuncionariosDomain
                        {
                            idFuncionario = Convert.ToInt32(rdr["idFuncionario"]),
                            nome = rdr["nomeDoFuncionario"].ToString(),
                            sobrenome = rdr["sobrenomeDoFuncionario"].ToString()
                        };

                        return FuncionariosBuscado;
                    }

                    return null;
                }
            }
        }
        #endregion

        #region Cadasta um novo Funcionário
        public void Cadastrar(FuncionariosDomain novoFuncionario)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryInsert = "insert into Funcionarios values(@nomeDoFuncionario, @sobrenomeDoFuncionario)";

                using(SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@NomeDoFuncionario", novoFuncionario.nome);

                    cmd.Parameters.AddWithValue("@sobrenomeDoFuncionario", novoFuncionario.sobrenome);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        #region Deleta um Funcionário pelo o ID
        public void Deletar(int id)
        {
           using(SqlConnection con = new SqlConnection(StringConexao))
            {
                string queryDelete = "DELETE FROM Funcionarios WHERE idFuncionario = @ID";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        public List<FuncionariosDomain> ListarTodos()
        {
            List<FuncionariosDomain> listaFuncionarios = new List<FuncionariosDomain>();

            using(SqlConnection con = new SqlConnection(StringConexao))
            {
                string QuerySelectAll = "SELECT idFuncionarios, NomeDoFuncionario, sobrenomeDoFuncionario from Funcionarios";

                con.Open();
                SqlDataReader rdr;

                using(SqlCommand cmd = new SqlCommand(QuerySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FuncionariosDomain funcionarios = new FuncionariosDomain()
                        {
                            idFuncionario = Convert.ToInt32(rdr[0]),
                            nome = rdr[1].ToString(),
                            sobrenome = rdr[2].ToString(),
                        };
                        listaFuncionarios.Add(funcionarios);
                    }
                }
            }
            return listaFuncionarios;
        }
    }
}
