using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabelaFrequencia
{
    public static class Tabelas
    {
        public static List<int> LerEntradaDoUsuario(int quantidade)
        {
            List<int> dados = new List<int>();
            Console.WriteLine($"Por favor, insira o número de vagas para {quantidade} empresas.");

            for (int i = 0; i < quantidade; i++)
            {
                int valorEntrada;
                bool entradaValida = false;

                do
                {
                    Console.Write($"Vagas da empresa {i + 1}: ");

                    if (int.TryParse(Console.ReadLine(), out valorEntrada))
                    {
                        dados.Add(valorEntrada);
                        entradaValida = true;
                    }
                    else
                    {
                        Console.WriteLine("Entrada inválida. Por favor, insira um número inteiro.");
                    }
                } while (!entradaValida);
            }
            return dados;
        }

        public static void GerarTabelaFrequenciaSimples(List<int> dados)
        {
            Console.WriteLine("Tabela de Distribuição de Frequência (Valores Únicos)");
            Console.WriteLine("=======================================================");
            Console.WriteLine("{0,-10} | {1,-5} | {2,-5} | {3,-10} | {4,-10}", "Valor", "Fa", "Fac", "FR (%)", "FRA (%)");
            Console.WriteLine("-------------------------------------------------------");

            int n = dados.Count;
            int fac = 0;

            var grupos = dados.GroupBy(x => x).OrderBy(g => g.Key);

            foreach (var grupo in grupos)
            {
                int valor = grupo.Key;
                int fa = grupo.Count();
                fac += fa;

                double fr = (double)fa / n;
                double fra = (double)fac / n;

                var item = new FreqSimplesItem(valor, fa, fac, fr, fra);

                Console.WriteLine("{0,-10} | {1,-5} | {2,-5} | {3,-10:P1} | {4,-10:P1}",
                    item.Valor, item.Fa, item.Fac, item.Fr, item.Fra);
            }
        }

        public static void GerarTabelaFrequenciaClassesSturges(List<int> dados)
        {
            Console.WriteLine("Tabela de Distribuição de Frequência (Classes - Sturges)");
            Console.WriteLine("=======================================================");

            int n = dados.Count;
            if (n == 0) return; // Evita erro se a lista estiver vazia.

            int k = (int)Math.Round(1 + 3.322 * Math.Log10(n));
            int min = dados.Min();
            int max = dados.Max();
            int at = max - min;

            // Evita divisão por zero se todos os valores forem iguais.
            if (at == 0)
            {
                Console.WriteLine("Todos os valores são iguais. Não é possível criar classes de frequência.");
                return;
            }

            int h = (int)Math.Ceiling((double)at / k);

            Console.WriteLine($"Análise de Sturges: n={n}, k={k}, AT={at}, h={h}\n");
            Console.WriteLine("{0,-15} | {1,-5} | {2,-5} | {3,-10} | {4,-10}", "Classe", "Fa", "Fac", "FR (%)", "FRA (%)");
            Console.WriteLine("----------------------------------------------------------------");

            int fac = 0;
            int limiteInferior = min;

            for (int i = 0; i < k; i++)
            {
                int limiteSuperior = limiteInferior + h;

                int fa = (i == k - 1)
                    ? dados.Count(d => d >= limiteInferior && d <= limiteSuperior)
                    : dados.Count(d => d >= limiteInferior && d < limiteSuperior);

                if (fa > 0)
                {
                    fac += fa;
                    double fr = (double)fa / n;
                    double fra = (double)fac / n;
                    string classeLabel = $"{limiteInferior} |- {limiteSuperior}";

                    var item = new FreqClasseItem(classeLabel, fa, fac, fr, fra);

                    Console.WriteLine("{0,-15} | {1,-5} | {2,-5} | {3,-10:P1} | {4,-10:P1}",
                    item.Classe, item.Fa, item.Fac, item.Fr, item.Fra);
                }

                limiteInferior = limiteSuperior;
            }
        }
    }
}
