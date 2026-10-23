namespace SistemaEleicao.Models;

public sealed class Eleitor
{
    public string Documento { get; }
    public bool JaVotou { get; private set; }

    public Eleitor(string documento)
    {
        Documento = documento;
    }

    internal void MarcarComoVotou()
    {
        JaVotou = true;
    }
}
