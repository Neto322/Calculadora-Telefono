using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace GraficadorSeñales
{
    abstract class Señal
    {
        public List<Muestra> Muestras { get; set; }
        public double TiempoInicial { get; set; }
        public double TiempoFinal { get; set; }
        public double FrecuenciaMuestreo { get; set; }
        public double AmplitudMaxima { get; set; }
        public abstract double evaluar(double tiempo);
        public void construirSeñal()
        {
            double periodoMuestreo = 1 / FrecuenciaMuestreo;
            Muestras.Clear();

            for (double a = TiempoInicial; a <= TiempoFinal; a += periodoMuestreo)
            {
                double muestra = evaluar(a);

                Muestras.Add(new Muestra(a, muestra));

                if(Math.Abs(muestra)> AmplitudMaxima)
                {
                    AmplitudMaxima = Math.Abs(muestra);
                }
            }

        }
        public static Señal multiplicarseñales(Señal señal1, Señal señal2 )
        {
            SeñalResultante resultado = new SeñalResultante();
            resultado.TiempoInicial = señal1.TiempoInicial;
            resultado.TiempoFinal = señal1.TiempoFinal;
            resultado.FrecuenciaMuestreo = señal1.FrecuenciaMuestreo;
            int indice = 0;
            foreach (var muestra in señal1.Muestras)
            {
                double nuevoValor = muestra.Y * señal2.Muestras[indice].Y;
                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));
                if (Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
                indice++;
            }
            return resultado;
            
        }
        public static Señal escalarAmplitud(Señal señalOriginal,double factorEscala)
        {
            SeñalResultante resultado = new SeñalResultante();

            resultado.TiempoInicial = señalOriginal.TiempoInicial;
            resultado.TiempoFinal = señalOriginal.TiempoFinal;
            resultado.FrecuenciaMuestreo = señalOriginal.FrecuenciaMuestreo;

            foreach(var muestra in señalOriginal.Muestras)
            {
                double nuevoValor = muestra.Y * factorEscala;
                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));
                if(Math.Abs((nuevoValor)) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }
            return resultado;
        }
        public static Señal desplazarAmplitud(Señal señalOriginal,double factorDesplaze)
        {
            SeñalResultante resultado = new SeñalResultante();

            resultado.TiempoInicial = señalOriginal.TiempoInicial;
            resultado.TiempoFinal = señalOriginal.TiempoFinal;
            resultado.FrecuenciaMuestreo = señalOriginal.FrecuenciaMuestreo;

            foreach(var muestra in señalOriginal.Muestras)
            {
                double NuevoValor = muestra.Y + factorDesplaze;
                resultado.Muestras.Add(new Muestra(muestra.X, NuevoValor));
                if(Math.Abs((NuevoValor)) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(NuevoValor);
                }
;           }
            return resultado;
        }
        public static Señal escalarExponencial(Señal señaloriginal,double exponente)
        {
            SeñalResultante resultado = new SeñalResultante();

            resultado.TiempoInicial = señaloriginal.TiempoInicial;
            resultado.TiempoFinal = señaloriginal.TiempoFinal;
            resultado.FrecuenciaMuestreo = señaloriginal.FrecuenciaMuestreo;

            foreach(var muestra in señaloriginal.Muestras)
            {
                double nuevoValor = Math.Pow(muestra.Y, exponente);
                resultado.Muestras.Add(new Muestra(muestra.X, nuevoValor));
                if(Math.Abs(nuevoValor) > resultado.AmplitudMaxima)
                {
                    resultado.AmplitudMaxima = Math.Abs(nuevoValor);
                }
            }

            return resultado;
            
        }
        public static Señal transformadaFourirer(Señal señal)
        {
            SeñalResultante resultado = new SeñalResultante();

            resultado.TiempoInicial = señal.TiempoInicial;
            resultado.TiempoFinal = señal.TiempoFinal;
            resultado.FrecuenciaMuestreo = señal.FrecuenciaMuestreo;

            for (int k = 0; k < señal.Muestras.Count; k++)
            {
                Complex muestra = 0;
                for (int n = 0; n < señal.Muestras.Count; n++)
                {
                    muestra += señal.Muestras[n].Y * Complex.Exp((-2 * Math.PI * Complex.ImaginaryOne * k) / señal.Muestras.Count);
                }
                resultado.Muestras.Add(new Muestra(señal.Muestras[k].X,muestra.Magnitude));
                if(Math.Abs(muestra.Magnitude) > señal.AmplitudMaxima)
                {
                    señal.AmplitudMaxima = Math.Abs(muestra.Magnitude);
                }
            }
            return resultado;
        }
    }
}
