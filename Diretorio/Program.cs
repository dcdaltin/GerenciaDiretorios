﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diretorio
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<string> listaEntrada = new List<string>
            {
                "Pasta 1/Outra filha da pasta 1/doc.ppt (10kb)",
                "Pasta 1/Filha da pasta 1/doc.docx (20kb)",
                "Pasta 2/Filha da pasta 2/Neta da pasta 2/script.sh (45 kb)",
                "Pasta 2/Filha da pasta 2/Outra neta da pasta 2/Bisneta da pasta 2/picture.png (5kb)",
                "Pasta 2/Outra filha da pasta 2/picture.png (25kb)"
            };

            DiretorioFactory objeto = new DiretorioFactory().AdicionaDiretorios(listaEntrada);
            Console.WriteLine(objeto.ToString());
            Console.ReadKey();
        }
    }
}
