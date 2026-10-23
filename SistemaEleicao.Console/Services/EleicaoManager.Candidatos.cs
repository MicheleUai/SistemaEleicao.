using SistemaEleicao.Models;

namespace SistemaEleicao.Services;

public sealed partial class EleicaoManager
{
    private readonly Dictionary<int, Candidato> _candidatos = new();

    public IReadOnlyList<Candidato> Candidatos =>
        _candidatos.Values.ToList().AsReadOnly();

    public void CadastrarCandidato(string nome, int numero)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Informe o nome do candidato.");

        if (numero <= 0)
            throw new ArgumentException("O numero deve ser maior que zero.");

        if (_candidatos.ContainsKey(numero))
            throw new InvalidOperationException(
                "Ja existe um candidato com esse numero.");

        _candidatos.Add(numero, new Candidato(nome.Trim(), numero));
    }
}
