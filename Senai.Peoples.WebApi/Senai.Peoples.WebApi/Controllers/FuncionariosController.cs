using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Peoples.WebApi.Domains;
using Senai.Peoples.WebApi.Interfaces;
using Senai.Peoples.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Controllers
{
    public class FuncionariosController : ControllerBase
    {
        //Define que o tipo de respsota da API sera em JSON
        [Produces("application/json")]

        //Define a rota da requisição da API exemplo: http://localhost:5000/api/funcionarios
        [Route("api/[controller]")]

        [ApiController]
        public class FuncionarioController : ControllerBase
        {
            private IFuncionariosRepository _funcionarios { get; set; }

            public FuncionarioController()
            {

                _funcionarios = new FuncionariosRepository();
            }

            #region Cadastra um novo usuario
            //Cadastra um novo usuário
            [HttpPost]
            public IActionResult Post(FuncionariosDomain novoFuncionario)
            {
                _funcionarios.Cadastrar(novoFuncionario);

                if (String.IsNullOrWhiteSpace(novoFuncionario.nome))
                {
                    return NotFound("Você tem que digitar o nome do usuário");
                }

                return Ok(201);
            }
            #endregion

            #region edição dos funcionarios

            //Atualiza um funcionário existente passando o seu id pela a URL
            [HttpPut("{id}")]
            public IActionResult PutIdUrl(int id, FuncionariosDomain funcionarioAtualizado)
            {
                FuncionariosDomain funcionarioBuscado = _funcionarios.BuscarPorId(id);

                if(funcionarioBuscado == null)
                {
                    return NotFound
                        (
                            new
                            {
                                mensagem = "Funcionário não encontrado!",
                                erro = true
                            }
                        );
                }
                try
                {
                    _funcionarios.AtualizarIdUrl(id, funcionarioAtualizado);

                    return Ok(201);
                }
                catch (Exception codErro)
                {
                    return BadRequest(codErro);
                }
            }


            [HttpPut]
            public IActionResult PutIdBody(FuncionariosDomain funcionarioAtualizado)
            {
                FuncionariosDomain funcionarioBuscado = _funcionarios.BuscarPorId(funcionarioAtualizado.idFuncionario);

                if(funcionarioBuscado != null)
                {
                    try
                    {
                        _funcionarios.AtualizarIdCorpo(funcionarioAtualizado);

                        return NoContent();
                    }
                    catch(Exception codErro)
                    {
                        return BadRequest(codErro);
                    }
                }
                return NotFound
                    (
                        new
                        {
                            mensagem = "Funcionário não encontrato!"
                        }
                        );
            }
            #endregion

            #region listar todos os funcionarios e buscar funcionaros por Id
            //Listar todos os Funcionários
            [HttpGet]
            public IActionResult Get()
            {
                List<FuncionariosDomain> listaFuncionarios = _funcionarios.ListarTodos();

                return Ok(listaFuncionarios);
            }

            [HttpGet("{id}")]
            public IActionResult GetById(int id)
            {
                FuncionariosDomain funcionarioBuscado = _funcionarios.BuscarPorId(id);

                if (funcionarioBuscado == null)
                {
                    return NotFound("Nenhum Funcionário encontrato!");

                }
                return Ok(funcionarioBuscado);
            }
            #endregion

            #region Deleta um Usuário 
            [HttpDelete("{id")]
            public IActionResult Delete(int id)
            {
                _funcionarios.Deletar(id);

                return StatusCode(204);
            }
            #endregion

        }
    }
}

