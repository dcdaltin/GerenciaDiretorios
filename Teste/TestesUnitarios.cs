namespace Teste
{
    using Diretorio;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class TestesUnitarios
    {
        [TestMethod]
        public void verifica_separacao_de_diretorios()
        {
            string stringTeste = "Pasta 1/Outra filha da pasta 1/doc.ppt (10kb)";
            (IEnumerable<string>, int) verificaString = new DiretorioFactory(new List<string>()).SeparaStrings(stringTeste);

            Assert.IsInstanceOfType(verificaString.Item2, typeof(int), "Tamanho NÃO tem tipo inteiro");
            Assert.IsNotNull(verificaString.Item1, "Lista é nula");
            Assert.AreEqual(verificaString.Item1.FirstOrDefault(), "Pasta 1", "Primeiro item da lista está incorreto");
            Assert.AreEqual(verificaString.Item1.ElementAt(1), "Outra filha da pasta 1", "Segundo item da lista está incorreto");
            Assert.AreEqual(verificaString.Item2, 10, "O tamanho está incorreto");
        }

        [TestMethod]
        public void verifica_trata_linha()
        {
            string stringTeste = "Pasta 1/Outra filha da pasta 1/doc.ppt (10kb)";
            DiretorioFactory objeto = new DiretorioFactory(new List<string>());
            (IEnumerable<string>, int) verificaString = objeto.SeparaStrings(stringTeste);
            Diretorio verificaLinha = objeto.TrataLinha(verificaString);

            Assert.IsNotNull(verificaLinha, "Objeto Nulo");
            Assert.AreEqual(1, verificaLinha.SubDiretorios.Count, "Numero incorreto de objetos");
            Assert.AreEqual(10, verificaLinha.SubDiretorios.ElementAt(0).getTamanho(), "Tamanho incorreto do subdiretório");
            Assert.AreEqual(10, verificaLinha.getTamanho(), "Tamanho incorreto do diretório");
            Assert.AreEqual("Pasta 1", verificaLinha.Nome, "Numero incorreto de objetos");
        }

        [TestMethod]
        public void verifica_modelos()
        {
            List<string> listaEntrada = new List<string>
            {
                "Pasta 1/Outra filha da pasta 1/doc.ppt (10kb)",
                "Pasta 1/Filha da pasta 1/doc.docx (20kb)",
                "Pasta 2/Filha da pasta 2/Neta da pasta 2/script.sh (45 kb)",
                "Pasta 2/Filha da pasta 2/Outra neta da pasta 2/Bisneta da pasta 2/picture.png (5kb)",
                "Pasta 2/Outra filha da pasta 2/picture.png (25kb)"
            };

            DiretorioFactory objeto = new DiretorioFactory(listaEntrada);
            var listaModelos = objeto.getModelo();

            Assert.IsNotNull(listaModelos, "Objeto Nulo");
            Assert.IsInstanceOfType(listaModelos, typeof(ICollection<Diretorio>));
            Assert.IsTrue(listaModelos.Select(a=>a.Nome).Contains("Pasta 1"), "Numero incorreto de objetos");
            Assert.IsTrue(listaModelos.Select(a => a.Nome).Contains("Pasta 2"), "Numero incorreto de objetos");
            Assert.AreEqual(30, listaModelos.Where(a=>a.Nome == "Pasta 1").SingleOrDefault().getTamanho(), "Tamanho incorreto do subdiretório");
            Assert.AreEqual(3, listaModelos.Where(a => a.Nome == "Pasta 2").SingleOrDefault().SubDiretorios.Count, "Tamanho incorreto do subdiretório");
        }
    }
}
