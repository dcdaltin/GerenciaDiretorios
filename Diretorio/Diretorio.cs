namespace Diretorio
{
    using System.Collections.Generic;
    using System.Linq;

    public class Diretorio
    {
        public int Tamanho { get; set; }

        public string Nome { get; set; }

        public virtual string Raiz { get; set; }

        public List<Diretorio> SubDiretorios { get; set; }

        public Diretorio(string nome)
        {
            Nome = nome;
            SubDiretorios = new List<Diretorio>();
        }

        public int GetTamanho()
        {
            return SubDiretorios.Any() ? SubDiretorios.Sum(a => a.GetTamanho()) : Tamanho;
        }

        public void SetTamanho(int tamanho)
        {
            this.Tamanho += tamanho;
        }

        public bool HasNext() { return this.SubDiretorios.Any(); }
    }
}
