using SistemaEleicao.Models;

namespace SistemaEleicao.Services;

public sealed partial class EleicaoManager
{
    public ResultadoApuracao Apurar()
    {
        var resultados = _candidatos.Values
            .Select(candidato => new ResultadoCandidato(
                candidato,
                _votos.GetValueOrDefault(candidato.Numero)))
            .OrderByDescending(item => item.Votos)
            .ThenBy(item => item.Candidato.Numero)
            .ToList();

        var maiorVotacao = resultados.Count == 0
            ? 0
            : resultados[0].Votos;

        var vencedores = resultados
            .Where(item => maiorVotacao > 0
                && item.Votos == maiorVotacao)
            .Select(item => item.Candidato)
            .ToList();

        return new ResultadoApuracao(
            resultados.AsReadOnly(),
            _votosBrancos,
            _votosNulos,
            vencedores.AsReadOnly());
    }
}
