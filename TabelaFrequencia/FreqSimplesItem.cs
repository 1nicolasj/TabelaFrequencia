using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabelaFrequencia
{
    public class FreqSimplesItem
    {
        public int Valor { get; set; }
        public int Fa { get; set; }
        public int Fac { get; set; }
        public double Fr { get; set; }
        public double Fra { get; set; }

        public FreqSimplesItem(int valor, int fa, int fac, double fr, double fra)
        {
            Valor = valor;
            Fa = fa;
            Fac = fac;
            Fr = fr;
            Fra = fra;
        }
    }
}
