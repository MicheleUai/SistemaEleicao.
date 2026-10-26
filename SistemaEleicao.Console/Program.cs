using SistemaEleicao.Services;
using Console = System.Console;

var eleicao = new EleicaoManager();

while (true)
{
    Console.WriteLine("\n=== SISTEMA DE ELEICAO ===");
    Console.WriteLine("1 - Cadastrar candidato");
    Console.WriteLine("2 - Registrar eleitor");
    Console.WriteLine("3 - Votar");
    Console.WriteLine("4 - Apurar votos");
    Console.WriteLine("0 - Sair");
    Console.Write("Opcao: ");

    var opcao = Console.ReadLine();

    if (opcao is null or "0")
        break;

    try
    {
        switch (opcao)
        {
            case "1":
            {
                var nome = LerTexto("Nome do candidato: ");
                var numero = LerInteiro("Numero do candidato: ");

                eleicao.CadastrarCandidato(nome, numero);
                Console.WriteLine("Candidato cadastrado.");
                break;
            }

            case "2":
            {
                var documento = LerTexto("Titulo/CPF simulado: ");

                eleicao.RegistrarEleitor(documento);
                Console.WriteLine("Eleitor cadastrado.");
                break;
            }

            case "3":
            {
                foreach (var candidato in eleicao.Candidatos)
                    Console.WriteLine(
                        $"{candidato.Numero} - {candidato.Nome}");

                Console.WriteLine(
                    "0 = branco | -1 ou numero inexistente = nulo");

                var documento = LerTexto("Titulo/CPF simulado: ");
                var numero = LerInteiro("Numero escolhido: ");

                eleicao.Votar(documento, numero);
                Console.WriteLine("Voto registrado.");
                break;
            }

            case "4":
            {
                var resultado = eleicao.Apurar();

                Console.WriteLine("\n=== APURACAO ===");

                foreach (var item in resultado.Candidatos)
                    Console.WriteLine(
                        $"{item.Candidato.Numero} - " +
                        $"{item.Candidato.Nome}: {item.Votos} voto(s)");

                Console.WriteLine(
                    $"Brancos: {resultado.VotosBrancos}");
                Console.WriteLine(
                    $"Nulos: {resultado.VotosNulos}");
                Console.WriteLine(
                    $"Total: {resultado.TotalVotos}");

                if (resultado.Vencedores.Count == 0)
                    Console.WriteLine(
                        "Sem vencedor: nenhum voto valido.");
                else if (resultado.HouveEmpate)
                    Console.WriteLine(
                        "Empate: " + string.Join(", ",
                            resultado.Vencedores.Select(c => c.Nome)));
                else
                    Console.WriteLine(
                        $"Vencedor: {resultado.Vencedores[0].Nome}");

                break;
            }

            default:
                Console.WriteLine("Opcao invalida.");
                break;
        }
    }
    catch (Exception erro) when (
        erro is ArgumentException or InvalidOperationException)
    {
        Console.WriteLine($"Atencao: {erro.Message}");
    }
}

static string LerTexto(string mensagem)
{
    Console.Write(mensagem);

    return Console.ReadLine()
        ?? throw new InvalidOperationException("Entrada encerrada.");
}

static int LerInteiro(string mensagem)
{
    if (!int.TryParse(LerTexto(mensagem), out var numero))
        throw new ArgumentException("Digite um numero inteiro valido.");

    return numero;
}
