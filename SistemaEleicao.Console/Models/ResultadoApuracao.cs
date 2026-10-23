namespace SistemaEleicao.Models;

public sealed record ResultadoCandidato(Candidato Candidato, int Votos);

public sealed record ResultadoApuracao(
    IReadOnlyList<ResultadoCandidato> Candidatos,
    int VotosBrancos,
    int VotosNulos,
    IReadOnlyList<Candidato> Vencedores)
{
    public int VotosValidos => Candidatos.Sum(item => item.Votos);
    public int TotalVotos => VotosValidos + VotosBrancos + VotosNulos;
    public bool HouveEmpate => Vencedores.Count > 1;
}
