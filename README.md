# Gestao-de-colaboradores-e-Unidades
Sistema de Gestão de colaboradores e Unidades em C# em Postgres

# Para rodar o projeto
- ```docker-compose up --build```
- ```dotnet run```
- Caso ao dar o ```dotnet run``` apareça algum erro de migration rode o seguinte comando: ```dotnet ef database update``` , assim as migrations que podem não ter sido colocas no seu banco pela primeira vez com o ```donet run```, é forçadamente aplicada pelo ```dotnet ef database update ``` 
- Obs: um em cada terminal

## Modelo:
- MVC
- Pattern de herança.
    - Classe Colaboradores;
    - Classe Usuarios;
    - Classe Unidades;

## Funcionalidades:
- [x] Cadastro de usuário: Os usuários dem ser criados com um código único, login, senha e status;
- [x] Atualização de Informações de Usuários;
- [x] Listagem de Usuários;
- [x] Cadastro de Colaboradores;
- [] Atualização de Informações de Colaboradores;
- [] Remoção de Colaboradores;
- [x] Listagem de Colaboradores;
- [x] Cadastro de Unidades;
- [x] Atualização de Informações de Unidades;
- [x] Listagem de Unidades;

## Lembretes:
- ao gerar o banco pela primeira vez, abra o banco e entre com o usuario host;
- ```dotnet ef migrations list``` - Listar migrations
- ```dotnet ef migrations remove``` - Remover migration (não aplicada)
- ```dotnet ef database update NomeDaMigrationAnterior``` - Reverter migration (aplicada)
- ```dotnet ef database update``` - Aplicar migration
