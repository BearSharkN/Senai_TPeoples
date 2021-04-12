CREATE DATABASE T_Peoples;
go 

USE T_Peoples;
go

CREATE TABLE Funcionarios
(
	idFuncionario int primary key identity
	,nomeDoFuncionario varchar (200) not null
	,sobrenomeDoFuncionario varchar (200) not null
);