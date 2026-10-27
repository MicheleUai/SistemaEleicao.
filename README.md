# Sistema de Eleição

Lista 31 - Garantia da Qualidade de Software.
Professor: Daniel Henrique Matos de Paiva.
Aluna: Michele Carvalho.

## Tecnologias

C#, .NET 10, xUnit e Git.
Histórico organizado com Conventional Commits.

## Funcionalidades

- Cadastro de candidatos com nome e número único.
- Registro de eleitores por título/CPF simulado.
- Votação com bloqueio de voto repetido.
- Contagem de votos válidos, brancos e nulos.
- Apuração por candidato, totais, vencedor e empate.

## Regras

- Candidatos devem ter nome e número positivo.
- Não é permitido cadastrar números de candidatos duplicados.
- O eleitor deve estar cadastrado e pode votar uma única vez.
- Número 0 representa voto branco.
- Número sem candidato cadastrado representa voto nulo.
- Sem votos válidos, não há vencedor.
- Em caso de empate, são apresentados os candidatos mais votados.
- Documentos são simulados, sem validação de CPF real.
- Os dados ficam em memória e são apagados ao encerrar a aplicação.

## Como executar

Instale o SDK .NET 10 e abra o terminal na pasta do projeto.

```sh
dotnet restore
dotnet build
dotnet run --project SistemaEleicao.Console/SistemaEleicao.Console.csproj
```

## Como executar os testes

```sh
dotnet test
```

São 10 testes das regras do sistema e 1 teste de exemplo do template.
Os testes verificam cadastro, duplicidade, votação, votos brancos e
nulos, totais, vencedor, empate e ausência de votos.

## Menu

- 1: Cadastrar candidato.
- 2: Registrar eleitor.
- 3: Votar.
- 4: Apurar votos.
- 0: Sair.

## Organização

- Models: candidatos, eleitores e resultados.
- Services: regras de cadastro, votação e apuração.
- UI/ApuracaoConsole.cs: apresentação dos resultados.
- Program.cs: menu e leitura das entradas.
- SistemaEleicao.Tests: testes unitários com xUnit.

## Versionamento

Commits separados por responsabilidade:
chore, test, feat, refactor e docs.

A refatoração extraiu a exibição da apuração do menu para
a classe ApuracaoConsole, mantendo o comportamento.
