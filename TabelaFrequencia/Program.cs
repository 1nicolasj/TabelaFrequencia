using System;
using System.Collections.Generic;
using System.Linq;
using TabelaFrequencia;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Calculadora de Tabela de Frequências ---");

        // a) Lê a entrada de 15 valores do usuário.
        List<int> vagas = Tabelas.LerEntradaDoUsuario(15);

        Console.WriteLine("\n------------------------------------------------------------");
        // b) Ordena e exibe os dados para verificação.
        vagas.Sort();
        Console.WriteLine("Dados Ordenados: " + string.Join(", ", vagas));
        Console.WriteLine("------------------------------------------------------------\n");

        // c) Gera e exibe a Tabela de Distribuição de Frequência Simples.
        Tabelas.GerarTabelaFrequenciaSimples(vagas);

        Console.WriteLine("\n------------------------------------------------------------\n");

        // d) Extra: Gera e exibe a Tabela de Distribuição de Frequência por Classes (Sturges).
        Tabelas.GerarTabelaFrequenciaClassesSturges(vagas);
    }
}