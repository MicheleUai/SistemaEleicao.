using System;
using Xunit;
using SistemaEleicao.Services;

namespace SistemaEleicao.Tests;

public class CandidatoTests
{
    [Fact]
    public void CadastrarCandidato_ComDadosValidos_AdicionaCandidato()
    {
        var eleicao = new EleicaoManager();

        eleicao.CadastrarCandidato("Ana", 10);

        var candidato = Assert.Single(eleicao.Candidatos);
        Assert.Equal("Ana", candidato.Nome);
        Assert.Equal(10, candidato.Numero);
    }

    [Fact]
    public void CadastrarCandidato_ComNumeroDuplicado_DeveRejeitar()
    {
        var eleicao = new EleicaoManager();
        eleicao.CadastrarCandidato("Ana", 10);

        Assert.Throws<InvalidOperationException>(
            () => eleicao.CadastrarCandidato("Bruno", 10));

        Assert.Single(eleicao.Candidatos);
    }
}
