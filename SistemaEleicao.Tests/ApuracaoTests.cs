using System.Linq;
using Xunit;
using SistemaEleicao.Services;

namespace SistemaEleicao.Tests;

public class ApuracaoTests
{
    [Fact]
    public void Apurar_ComVotosMistos_RetornaTotaisEVencedor()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Ana", 10);
        eleicao.CadastrarCandidato("Bruno", 20);

        var votos = new[] { 10, 10, 20, 0, 99 };

        for (var i = 0; i < votos.Length; i++)
        {
            var documento = $"E{i}";
            eleicao.RegistrarEleitor(documento);
            eleicao.Votar(documento, votos[i]);
        }

        var resultado = eleicao.Apurar();

        Assert.Equal(2, resultado.Candidatos
            .Single(c => c.Candidato.Numero == 10).Votos);
        Assert.Equal(1, resultado.Candidatos
            .Single(c => c.Candidato.Numero == 20).Votos);
        Assert.Equal(1, resultado.VotosBrancos);
        Assert.Equal(1, resultado.VotosNulos);
        Assert.Equal(3, resultado.VotosValidos);
        Assert.Equal(5, resultado.TotalVotos);
        Assert.Equal(10, Assert.Single(resultado.Vencedores).Numero);
        Assert.False(resultado.HouveEmpate);
    }

    [Fact]
    public void Apurar_ComEmpate_RetornaOsDoisCandidatos()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Ana", 10);
        eleicao.CadastrarCandidato("Bruno", 20);
        eleicao.RegistrarEleitor("E1");
        eleicao.RegistrarEleitor("E2");

        eleicao.Votar("E1", 10);
        eleicao.Votar("E2", 20);

        var resultado = eleicao.Apurar();

        Assert.True(resultado.HouveEmpate);
        Assert.Equal(
            new[] { 10, 20 },
            resultado.Vencedores.Select(c => c.Numero).ToArray());
        Assert.Equal(2, resultado.TotalVotos);
    }

    [Fact]
    public void Apurar_ComCandidatoSemVotos_NaoDeclaraVencedor()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Ana", 10);

        var resultado = eleicao.Apurar();

        Assert.Empty(resultado.Vencedores);
        Assert.Equal(0, resultado.TotalVotos);
        Assert.False(resultado.HouveEmpate);
        Assert.Equal(0, Assert.Single(resultado.Candidatos).Votos);
    }

    [Fact]
    public void Apurar_SemCandidatos_RetornaResultadoVazio()
    {
        var eleicao = new EleicaoManager();

        var resultado = eleicao.Apurar();

        Assert.Empty(resultado.Candidatos);
        Assert.Empty(resultado.Vencedores);
        Assert.Equal(0, resultado.TotalVotos);
    }
}
