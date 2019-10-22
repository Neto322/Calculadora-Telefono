using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraficadorSeñales
{
    class SeñalResultante : Señal
    {
        public SeñalResultante()
        {
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
        }
        public override double evaluar(double tiempo)
        {
            throw new NotImplementedException();
        }
    }
}
