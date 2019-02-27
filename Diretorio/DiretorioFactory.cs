namespace Diretorio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DiretorioFactory
    {
        private List<Diretorio> _diretorios;
        private int profundidade = 0;

        public DiretorioFactory()
        {
            _diretorios = new List<Diretorio>();
        }

        public DiretorioFactory AdicionaDiretorios(ICollection<string> listaEntrada)
        {
            foreach (string item in listaEntrada)
            {
                _diretorios.AddRange(TrataLinha(SeparaStrings(item)));
            }

            return this;
        }

        public ICollection<Diretorio> AgregaDiretorios()
        {
            foreach (Diretorio item in _diretorios)
            {
                item.SubDiretorios.AddRange(_diretorios.Where(a => a.Raiz == item.Nome));
            }

            return _diretorios.Where(a => string.IsNullOrEmpty(a.Raiz)).ToList();
        }

        public ICollection<Diretorio> TrataLinha((IEnumerable<string>, int) stringSeparada)
        {
            List<Diretorio> listaResultado = new List<Diretorio>();
            for (int i = 0; i < stringSeparada.Item1.Count(); i++)
            {
                Diretorio modelo = new Diretorio(stringSeparada.Item1.ElementAt(i));
                if (i == stringSeparada.Item1.Count() - 1)
                {
                    modelo.SetTamanho(stringSeparada.Item2);
                }

                if (i > 0)
                {
                    modelo.Raiz = stringSeparada.Item1.ElementAtOrDefault(i - 1);
                }

                if (!_diretorios.Exists(a => a.Nome == modelo.Nome && a.Raiz == modelo.Raiz))
                {
                    listaResultado.Add(modelo);
                }
            }

            return listaResultado;
        }

        public (IEnumerable<string>, int) SeparaStrings(string Path)
        {
            string[] delimitadores = { "/", " (", "kb)" };
            string[] listaDiretorios = Path.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            string tamanhoArquivo = listaDiretorios[listaDiretorios.Length - 1];
            IEnumerable<string> diretorios = listaDiretorios.Take(listaDiretorios.Length - 2);
            return (diretorios, Convert.ToInt16(tamanhoArquivo));
        }

        public string EscreveDiretorio(Diretorio diretorio)
        {
            profundidade++;
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < profundidade; i++)
            {
                stringBuilder.AppendFormat("     ");
            }
            stringBuilder.AppendFormat("- {0} ({1} kb)", diretorio.Nome, diretorio.GetTamanho());
            stringBuilder.AppendLine();

            for (int i = 0; i < diretorio.SubDiretorios.Count; i++)
            {
                stringBuilder.AppendFormat(EscreveDiretorio(diretorio.SubDiretorios.ElementAt(i)));
                profundidade--;
            }

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (Diretorio item in AgregaDiretorios())
            {
                stringBuilder.AppendFormat(EscreveDiretorio(item));
                profundidade = 0;
            }

            return stringBuilder.ToString();
        }

    }
}
