namespace Diretorio
{
    using System.Collections.Generic;
    using System.Linq;

    public class Diretorio
    {
        public int tamanho { get; set; }

        public string Nome { get; set; }

        public ICollection<Diretorio> SubDiretorios { get; set; }

        public Diretorio(string nome)
        {
            Nome = nome;
            SubDiretorios = new List<Diretorio>();
        }

        public Diretorio(string nome, ICollection<Diretorio> subDiretorios)
        {
            Nome = nome;
            SubDiretorios = subDiretorios;
        }

        public int getTamanho()
        {
            return SubDiretorios.Any() ? SubDiretorios.Sum(a => a.getTamanho()) : tamanho;
        }

        public void setTamanho(int tamanho)
        {
            this.tamanho += tamanho;
        }
    }
}
