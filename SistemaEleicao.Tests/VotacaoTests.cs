using System;
using Xunit;
using SistemaEleicao.Services;

namespace SistemaEleicao.Tests;

public class VotacaoTests
{
    [Fact]
    public void Votar_EmCandidatoExistente_ContaVoto()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Ana", 10);
        eleicao.RegistrarEleitor("123");

        eleicao.Votar("123", 10);

        var resultado = eleicao.Apurar();
        Assert.Equal(1, Assert.Single(resultado.Candidatos).Votos);
        Assert.Equal(1, resultado.TotalVotos);
    }

    [Fact]
    public void Votar_DuasVezesComMesmoEleitor_DeveRejeitar()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Ana", 10);
        eleicao.RegistrarEleitor("123");
        eleicao.Votar("123", 10);

        Assert.Throws<InvalidOperationException>(
            () => eleicao.Votar("123", 10));

        Assert.Equal(1, eleicao.Apurar().TotalVotos);
    }

    [Fact]
    public void Votar_EmCandidatoInexistente_ContaNulo()
    {
        var eleicao = new EleicaoManager();
        eleicao.RegistrarEleitor("123");

        eleicao.Votar("123", 99);

        var resultado = eleicao.Apurar();
        Assert.Equal(1, resultado.VotosNulos);
        Assert.Equal(0, resultado.VotosValidos);
        Assert.Equal(1, resultado.TotalVotos);
    }

    [Fact]
    public void Votar_EmZero_ContaBranco()
    {
        var eleicao = new EleicaoManager();
        eleicao.RegistrarEleitor("123");

        eleicao.Votar("123", 0);

        var resultado = eleicao.Apurar();
        Assert.Equal(1, resultado.VotosBrancos);
        Assert.Equal(0, resultado.VotosNulos);
        Assert.Equal(1, resultado.TotalVotos);
    }
}
