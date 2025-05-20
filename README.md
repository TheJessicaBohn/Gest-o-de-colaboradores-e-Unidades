# Gestao-de-colaboradores-e-Unidades
Sistema de Gestão de colaboradores e Unidades em C# em MySQL

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
- [] Cadastro de Colaboradores;
- [] Atualização de Informações de Colaboradores;
- [] Remoção de Colaboradores;
- [] Listagem de Colaboradores;
- [] Cadastro de Unidades;
- [] Atualização de Informações de Unidades;
- [] Listagem de Unidades;

## Lembretes:
- ao gerar o banco pela primeira vez, abra o banco e entre com o usuario host;
- ```dotnet ef migrations list``` - Listar migrations
- ```dotnet ef migrations remove``` - Remover migration (não aplicada)
- ```dotnet ef database update NomeDaMigrationAnterior``` - Reverter migration (aplicada)
- ```dotnet ef database update``` - Aplicar migration
