# Projeto Interdisciplinar II - 4º Semestre do Curso de Análise e Desenvolvimento de Sistemas - Fatec Rio Preto

Disciplinas:
- Engenharia de Software III
- Programação Orientada a Objetos


# Star Coins

Integrantes:
- Deivid
- Pedro Arthur
- Priscilla
- Rafaela

Objetivos:
O objetivo da aplicação é oferecer um meio para compensar alunos por tarefas feitas. 
A aplicação deve permitir que um professor atribua uma nota à atividade e esta nota é convertida em certa quantidade de moedas. No sistema o aluno poderá trocar suas moedas por algum produto. 

Diagrama de Entidade e Relacionamento
![alt text](<StarCoins_ProjInterII-REAL DER Star Coin.jpg>)

Telas:
- Usuário Adm
- Usuário Prof
- Usuário Aluno


# Referências:

Bootstrap Icon

https://icons.getbootstrap.com/

Entity Framework

https://learn.microsoft.com/pt-br/ef/core/get-started/overview/first-app?tabs=netcore-cli

Configurar Entity Framework

https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/

Instalar o EF no computador:

`dotnet tool install --global dotnet-ef`

Para manipular dados em memória:

`dotnet add package Microsoft.EntityFrameworkCore.InMemory`

Para manipular banco de dados (Sqlite, SqlServer, Oracle, MySQL, Postgres etc.)
SQLite:

`dotnet add package Microsoft.EntityFrameworkCore.Sqlite`

SQL Server:

`dotnet add package Microsoft.EntityFrameworkCore.SqlServer`

E para realizar model first:

`dotnet add package Microsoft.EntityFrameworkCore.Design`

`dotnet ef migrations add InitialCreate`

Remove a última migração:

`dotnet ef migrations remove`

Visualizar o script gerado na migração:

`dotnet ef migrations script >> script.sql`

Atualiza o banco de dados com a migration:

`dotnet ef database update`

`dotnet ef database update --connection "Data Source=My.db"`

Banco de Dados:
- Pasta Data > StarCoinsDatabase.cs
- Program.cs
- Controllers:
    - public class <NomeModel>Controller : Controller
    {
        private readonly StarCoinsDatabase db;

        // Construtor que injeta a instância do banco de dados
        public <NomeModel>Controller(StarCoinsDatabase db)
        {
            this.db = db;
        }

    // outros métodos (HttpGet e HttpPost)
    }
