## Desafio 2

### Entendendo o desafio!

O objetivo desse desafio era construir uma aplicação em linha de comando para realizar o CRUD de Clientes no Banco de Dados, utilizando o Entity Framework.

Dessa especificação, podemos perceber que será necessário adicionarmos um modelo para representar a nossa Entidade dentro do nosso código, conforme exige o modelo de ORM. 
Também será necessário criarmos um contexto, que seria uma representação do Banco de Dados e de suas tabelas.

Nossa interface também deverá ser criada, e iremos usar o Paradigma de Programação Orientada a Objetos para fazer uma separação de funcionalidade, sendo elas o Menu e as Operações.


## Passo a passo
### Setup

Primeiro iremos fazer o setup do nosso projeto! Comece criando uma aplicação Console pelo Visual Studio ou também pela CLI do dotnet com:

    dotnet new console

Depois disso iremos até a Solution do nosso projeto e clicaremos com o botão esquerdo, iremos em "Manage Nuggets Package", vamos baixar o Entity Framework para SQLite e o Entity Framework Tools. 

O Entity Framework Core SQLite é o ORM(Object Relational Mapping) e irá fazer o mapeamento das Entidades do Banco para as classes do nosso código, junto com o Driver de conexão para SQLite.

O Tools irá nos ajudar a gerir as Migrations, as Migrations são como diversos Snapshots ou versões do nosso banco de dados, vc pode voltar o banco pra uma versão anterior e auditar suas alterações.

### Código

Iremos criar uma classe chamada de "Client" dentro do package "Models", ela será nosso modelo e representará uma entidade do Banco de Dados, ai iremos definir nossos atributos conforme os que tem na tabela, ou terão. É de suma importância criar um atributo para a Primary Key, anotando-a com"Key" ou usando a nomenclatura conhecida, como "Id". Também iremos criar um constructor para deixar explicito as propriedade obrigatórias(o id será auto-gerado).

Depois disso iremos criar uma classe chamada SQLiteContext e vamos herdar de DbContext. Iremos criar um atributo de tipo DbSet com o nosso modelo, isso irá permitir que possamos fazer operações nessa tabela, como criação, leitura e etc. E vamos configurar no método sobreescrito "OnConfiguring" o caminho para o banco.

Agora iremos acessar o Packet Manager Console. Na barra superior do Visual Studio -> Tools -> Nuget Package Manager -> Packet Manager Console.

E aí usaremos os seguintes comandos:

    Add-Migration Initial
    Database-Update

Essas funções vieram do Entity Tools e irá gerar uma Migration conforme nossos modelos, e executá-la no segundo. É possível alterar a migration também,

Agora vamos criar nossa classe de Menu. Sua única função é fazer o display das operações e perguntas ao usuário.

E o Enum de Operation vai executar a operação escolhida no Menu.

Para adicionar é necessário instanciar um Client, adiciona-lo ao DbSet do Context e por fim commitar as alterações pra alinha-lá ao banco.
Para ler é só pegar o DbSet do model e utilizar o Where, caso precise filtrar o conteúdo.
Para modificar é necessário pegar uma entidade gerenciada, altera-la e salvar as alterações.
Para deletar iremos usar o método Remove passando uma entidade gerenciada e salvando.

Nosso programa irá fazer um loop para realizar operações e mostrar o menu até o usuário desejar sair.

## Links utéis

https://www.alura.com.br/artigos/poo-programacao-orientada-a-objetos

https://www.treinaweb.com.br/blog/o-que-e-orm

https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

https://docs.microsoft.com/en-us/ef/ef6/saving/change-tracking/entity-state