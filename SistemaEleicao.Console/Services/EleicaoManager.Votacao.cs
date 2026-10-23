using SistemaEleicao.Models;

namespace SistemaEleicao.Services;

public sealed partial class EleicaoManager
{
    private readonly Dictionary<string, Eleitor> _eleitores = new();
    private readonly Dictionary<int, int> _votos = new();
    private int _votosBrancos;
    private int _votosNulos;

    public void RegistrarEleitor(string documento)
    {
        documento = ValidarDocumento(documento);

        if (_eleitores.ContainsKey(documento))
            throw new InvalidOperationException("Eleitor ja cadastrado.");

        _eleitores.Add(documento, new Eleitor(documento));
    }

    public void Votar(string documento, int numeroCandidato)
    {
        documento = ValidarDocumento(documento);

        if (!_eleitores.TryGetValue(documento, out var eleitor))
            throw new InvalidOperationException(
                "Cadastre o eleitor antes de votar.");

        if (eleitor.JaVotou)
            throw new InvalidOperationException(
                "Este eleitor ja votou.");

        if (numeroCandidato == 0)
        {
            _votosBrancos++;
        }
        else if (_candidatos.ContainsKey(numeroCandidato))
        {
            _votos[numeroCandidato] =
                _votos.GetValueOrDefault(numeroCandidato) + 1;
        }
        else
        {
            _votosNulos++;
        }

        eleitor.MarcarComoVotou();
    }

    private static string ValidarDocumento(string documento)
    {
        if (string.IsNullOrWhiteSpace(documento))
            throw new ArgumentException(
                "Informe o titulo ou CPF simulado.");

        return documento.Trim();
    }
}
