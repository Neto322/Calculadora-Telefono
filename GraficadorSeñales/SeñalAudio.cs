using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
namespace GraficadorSeñales
{
    class SeñalAudio : Señal
    {
        public string RutaArchivo { get; set; }

        public SeñalAudio(string rutaArchivo)
        {
            RutaArchivo = rutaArchivo;
            Muestras = new List<Muestra>();
            AmplitudMaxima = 0.0;
            AudioFileReader reader = new AudioFileReader(rutaArchivo);
            TiempoInicial = 0.0;
            TiempoInicial = reader.TotalTime.TotalSeconds;
            FrecuenciaMuestreo = reader.WaveFormat.SampleRate;

            var bufferLectura = new float[reader.WaveFormat.Channels];

            double instanteActual = 0.0;

            double periodoMuestreo = 1.0 / FrecuenciaMuestreo;
            int muestrasLeidas = 0;
           do {
                muestrasLeidas = reader.Read(bufferLectura,0,reader.WaveFormat.Channels);
                if(muestrasLeidas > 0)
                {
                    double max = bufferLectura.Take(muestrasLeidas).Max();
                    Muestras.Add(new Muestra(instanteActual, max));
                    if(Math.Abs(max) > AmplitudMaxima)
                    {
                        AmplitudMaxima = Math.Abs(max);
                    }
                }
                instanteActual += periodoMuestreo;
            } while (muestrasLeidas > 0);
        }

        public override double evaluar(double tiempo)
        {
            throw new NotImplementedException();
        }
    }
}
