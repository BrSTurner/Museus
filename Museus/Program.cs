using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Museus.Museu;

namespace Museus{
    class Program{
        private static List<Paises> listaPaises;
        private static List<Cidades> listaCidades;
        private static List<Museu.Museu> listaMuseus;
        static void Main(string[] args){
            listaMuseus = new List<Museu.Museu>();
            new Thread(carregarListas).Start();
            Menu();
        }

        private static void Menu(){
            uint escolhaAtual;
            do {
                Console.Clear();
                Console.WriteLine("1-Adicionar novo museu\n" +
                                  "2-Mostrar todos os museus\n" +
                                  "3-Mostrar museu com classificação acima de 3 Estrelas\n" +
                                  "4-Mostrar museu(s) do Brasil\n" +
                                  "5-Sair");
                escolhaAtual = 0;
                try { escolhaAtual = UInt32.Parse(Console.ReadLine()); } catch { Console.WriteLine("Somente números permitidos!"); escolhaAtual = 6; Console.ReadKey(); }
                processarEscolha(escolhaAtual);
            } while (true && escolhaAtual != 5);
        }

        private static void processarEscolha(uint escolhaAtual){
            Console.Clear();
            switch (escolhaAtual){
                case 1:
                    adicionarMuseu();
                    break;
                case 2:
                    mostrarTodos(1);
                    break;
                case 3:
                    mostrarTodos(2);
                    break;
                case 4:
                    mostrarTodos(3);
                    break;
                default:
                    break;
            }
        }

        private static void mostrarTodos(uint tipo) {
            Console.Clear();
            if (listaMuseus.Count <= 0) {
                Console.WriteLine("Ainda não há museus cadastrados!");
                Console.ReadKey();
                return;
            }

            switch (tipo) {
                case 1:
                    foreach (Museu.Museu M in listaMuseus)
                        Console.WriteLine($"Nome: {M.Nome}\nPaís: {listaPaises[(int)listaCidades[(int)M.Cidade].PaisID].Nome}\nCidade: {listaCidades[(int)M.Cidade].Nome}\nValor:{M.Valor}\nNota: {M.Nota}\n");
                    break;
                case 2:
                    foreach (Museu.Museu M in listaMuseus){
                        if(M.Nota > 3)
                            Console.WriteLine($"Nome: {M.Nome}\nPaís: {listaPaises[(int)listaCidades[(int)M.Cidade].PaisID].Nome}\nCidade: {listaCidades[(int)M.Cidade].Nome}\nValor:{M.Valor}\nNota: {M.Nota}\n");
                    }
                    break;
                case 3:
                    foreach (Museu.Museu M in listaMuseus){
                        if (listaPaises[(int)listaCidades[(int)M.Cidade].PaisID].Nome.Equals("Brasil"))
                            Console.WriteLine($"Nome: {M.Nome}\nPaís: {listaPaises[(int)listaCidades[(int)M.Cidade].PaisID].Nome}\nCidade: {listaCidades[(int)M.Cidade].Nome}\nValor:{M.Valor}\nNota: {M.Nota}\n");
                    }
                    break;
            }

            Console.ReadKey();
        }
        private static void adicionarMuseu(){
            string Nome;
            double Valor;
            int Nota;
            uint Pais;
            uint Cidade;

            //Obriga o usuário a inserir um pais válido
            do {
                Console.Clear();
                Console.WriteLine("Selecione um país:\n");

                foreach (Paises p in listaPaises){
                    if(listaCidades.Select(x => x).Where(x => x.PaisID == p.ID).Count() > 0)
                        Console.WriteLine($"{p.ID}-{p.Nome}");
                }

                try { Pais = UInt32.Parse(Console.ReadLine()); } catch { Console.WriteLine("Somente números permitidos!"); Pais = listaPaises.Max(x => x.ID) + 1; Console.ReadKey(); }
                
            } while (listaPaises.Min(x => x.ID) > Pais || listaPaises.Max(x => x.ID) < Pais);

            List<Cidades> cidadesDoPais = new List<Cidades>();
            cidadesDoPais.AddRange(listaCidades.Select(x => x).Where(x => x.PaisID == Pais));

            do{
                Console.Clear();
                Console.WriteLine("Selecione uma cidade:\n");

                foreach (Cidades c in cidadesDoPais)
                    Console.WriteLine($"{c.ID}-{c.Nome}");
                

                try { Cidade = UInt32.Parse(Console.ReadLine()); } catch { Console.WriteLine("Somente números permitidos!"); Cidade = listaPaises.Max(x => x.ID) + 1; Console.ReadKey(); }
            } while (cidadesDoPais.Min(x => x.ID) > Cidade || cidadesDoPais.Max(x => x.ID) < Cidade);

            do{
                Console.Clear();
                Console.WriteLine("Digite o nome do museu: ");
                Nome = Console.ReadLine();
            } while (Nome.Length <= 0);

            do {
                Console.Clear();
                Console.WriteLine("Digite o valor da entrada do museu:");
                try { Valor = Double.Parse(Console.ReadLine()); } catch { Console.WriteLine("Somente números permitidos!"); Valor = -1; Console.ReadKey(); }
            } while (Valor < 0);

            do {
                Console.Clear();
                Console.WriteLine("Digite a nota do museu:");
                try { Nota = Int32.Parse(Console.ReadLine()); } catch { Console.WriteLine("Somente números permitidos!"); Nota = -1; Console.ReadKey(); }
            } while (Nota < 0 || Nota > 5);

            listaMuseus.Add(new Museu.Museu(Nome, Valor, Cidade, (byte)Nota));
            Console.Clear();
            Console.WriteLine("Museu inserido com sucesso!");
            Console.ReadKey();
        }

        private static void carregarListas(){
            listaPaises = new List<Paises>(){
                new Paises() { ID = 0, Nome = "Brasil"},
                new Paises() { ID = 1, Nome = "Estados Unidos"},
                new Paises() { ID = 2, Nome = "México"},
            };

            listaCidades = new List<Cidades>(){
                new Cidades() {ID = 0, PaisID = 0, Nome = "Campinas"},
                new Cidades() {ID = 1, PaisID = 0, Nome = "São Paulo"},
                new Cidades() {ID = 2, PaisID = 1, Nome = "New York"},
                new Cidades() {ID = 3, PaisID = 1, Nome = "Washington DC"},
                new Cidades() {ID = 4, PaisID = 2, Nome = "Cancún"},
                new Cidades() {ID = 5, PaisID = 2, Nome = "Acapulco"}
            };
        }
    }
}
