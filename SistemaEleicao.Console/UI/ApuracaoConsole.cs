using SistemaEleicao.Models;
using Console = System.Console;

namespace SistemaEleicao.UI;

public static class ApuracaoConsole
{
    public static void Exibir(ResultadoApuracao resultado)
    {
        Console.WriteLine("\n=== APURACAO ===");

        foreach (var item in resultado.Candidatos)
            Console.WriteLine(
                $"{item.Candidato.Numero} - " +
                $"{item.Candidato.Nome}: {item.Votos} voto(s)");

        Console.WriteLine($"Brancos: {resultado.VotosBrancos}");
        Console.WriteLine($"Nulos: {resultado.VotosNulos}");
        Console.WriteLine($"Total: {resultado.TotalVotos}");

        if (resultado.Vencedores.Count == 0)
            Console.WriteLine("Sem vencedor: nenhum voto valido.");
        else if (resultado.HouveEmpate)
            Console.WriteLine(
                "Empate: " + string.Join(", ",
                    resultado.Vencedores.Select(c => c.Nome)));
        else
            Console.WriteLine(
                $"Vencedor: {resultado.Vencedores[0].Nome}");
    }
}
