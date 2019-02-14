namespace Diretorio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DiretorioFactory
    {
        private readonly ICollection<string> _listaEntrada;

        public DiretorioFactory(ICollection<string> listaEntrada)
        {
            _listaEntrada = listaEntrada;
        }

        public override string ToString()
        {
            // Falta implementar essa parte
            return _listaEntrada.ToString();
        }

        public ICollection<Diretorio> getModelo()
        {
            IEnumerable<Diretorio> listaModelos = _listaEntrada.Select(a => TrataLinha(SeparaStrings(a)));
            ICollection<Diretorio> _listaSaida = new List<Diretorio>();

            foreach (Diretorio item in listaModelos)
            {

                if (!_listaSaida.Where(a => a.Nome == item.Nome).Any())
                {
                    _listaSaida.Add(item);
                }
                else
                {
                    var aux = _listaSaida.Where(a => a.Nome == item.Nome).SingleOrDefault();
                    _listaSaida.Remove(aux);
                    aux.setTamanho(item.tamanho);
                    foreach (var subDiretorio in item.SubDiretorios)
                    {
                        if (!aux.SubDiretorios.Contains(subDiretorio)) aux.SubDiretorios.Add(subDiretorio);
                    }
                    _listaSaida.Add(aux);
                }
            }

            return _listaSaida;
        }

        public Diretorio TrataLinha((IEnumerable<string>, int) stringSeparada)
        {
            List<Diretorio> listaResultado = new List<Diretorio>();
            for (int i = 0; i < stringSeparada.Item1.Count(); i++)
            {
                Diretorio modelo = new Diretorio(stringSeparada.Item1.ElementAt(i));
                if (i == stringSeparada.Item1.Count() - 1)
                {
                    modelo.setTamanho(stringSeparada.Item2);
                }

                if (i > 0)
                {
                    listaResultado.ElementAt(i - 1).SubDiretorios.Add(modelo);
                }

                listaResultado.Add(modelo);
            }

            return listaResultado.ElementAt(0);
        }

        public (IEnumerable<string>, int) SeparaStrings(string Path)
        {
            string[] delimitadores = { "/", " (", "kb)" };
            string[] listaDiretorios = Path.Split(delimitadores, StringSplitOptions.RemoveEmptyEntries);
            string tamanhoArquivo = listaDiretorios[listaDiretorios.Length - 1];
            IEnumerable<string> diretorios = listaDiretorios.Take(listaDiretorios.Length - 2);
            return (diretorios, Convert.ToInt16(tamanhoArquivo));
        }


    }
}
