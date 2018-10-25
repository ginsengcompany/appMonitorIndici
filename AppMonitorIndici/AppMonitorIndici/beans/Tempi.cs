using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMonitorIndici.Bean
{
    public class Tempi
    {
        public String tempoMedio { get; set; }

        public String tempoMassimo { get; set; }

        public Tempi(String tempoMedio, String tempoMassimo)
        {
            this.tempoMedio = tempoMedio;
            this.tempoMassimo = tempoMassimo;
        }

        public String toString()
        {
            return "Tempi{" +
                    "tempoMedio='" + tempoMedio + '\'' +
                    ", tempoMassimo='" + tempoMassimo + '\'' +
                    '}';
        }
    }
}