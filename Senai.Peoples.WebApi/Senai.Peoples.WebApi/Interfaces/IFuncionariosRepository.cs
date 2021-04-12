using System;
using Senai.Peoples.WebApi.Domains;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IFuncionariosRepository
    {
        // Aqui eu estou criando a Função para listar todos os Funcionários
        List<FuncionariosDomain> ListarTodos();

        //Aqui eu estou criando uma função para buscar um funcionário especifico por seu ID
        FuncionariosDomain BuscarPorId(int id);

        //Aqui eu estou cadastrando um novo Funcionário
        void Cadastrar(FuncionariosDomain novoFuncionario);

        //Aqui eu estou atualizando as Informações existente pelo o ID 
        void AtualizarIdCorpo(FuncionariosDomain funcionario);

        //Aqui estou atualizando o funcionário passando a id pela url da requisição
        void AtualizarIdUrl(int id, FuncionariosDomain funcionarios);

        //Aqui eu deleto um funionário pelo o ID
        void Deletar(int id);
    }
}
