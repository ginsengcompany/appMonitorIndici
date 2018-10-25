using AppMonitorIndici.Bean;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AppMonitorIndici.bo
{
    public class ElencoPazientiBO
    {
        public int getNumeroPazientiDimessi(List<RecordBean> lista)
        {
            DateTime dataAttuale = DateTime.Now;
            DateTime c = new DateTime(dataAttuale.Year, dataAttuale.Month, dataAttuale.Day, dataAttuale.Hour - 2, dataAttuale.Minute, dataAttuale.Second);
            dataAttuale = c;

            int returnValue = 0;
            foreach (var i in lista)
            {
                if (i.Stato.Equals("Dimesso"))
                {
                    String strData = i.Datadimissione + " " + i.Oradimissione + ":00";
                    DateTime dataEvento = Convert.ToDateTime(strData);
                    if (dataAttuale.CompareTo(dataEvento) < 0)
                    {
                        if (i.Modalitadimissione.Equals("RICOVERO IN REPARTO DI DEGENZA"))
                        {
                            returnValue++;
                        }
                    }
                }
            }
            return returnValue;
        }

        public int getNumeroPazientiPresiInCarico(List<RecordBean> lista)
        {
            int returnValue = 0;
            foreach (var i in lista)
            {
                if (i.Stato.Equals("Accettato"))
                {
                    returnValue++;
                }
            }
            return returnValue;
        }

        public int getNumeroPazientiAttesaReferto(List<RecordBean> lista)
        {
            int returnValue = 0;
            foreach (var i in lista)
            {
                if (i.Stato.Equals("In Attesa Referto"))
                {
                    returnValue++;
                }
            }
            return returnValue;
        }

        public int getNumeroRespiratoriInUso(List<RecordBean> lista)
        {
            int returnValue = 0;
            foreach (var i in lista)
            {
                if (i.Colore.Equals("Rosso"))
                {
                    returnValue++;
                }
            }
            return returnValue;
        }

        public int getNumeroPazientiPerCodiceTriage(List<RecordBean> lista)
        {
            int returnValue = 0;
            int indiceBianco = 0;
            int indiceVerde = 0;
            int indiceGiallo = 0;
            int indiceRosso = 0;
            foreach (var i in lista)
            {
                if (i.Colore.Equals("Bianco") && !i.Stato.Equals("Dimesso") && !i.Stato.Equals("In Osservazione OBI"))
                {
                    indiceBianco++;
                }
                else if (i.Colore.Equals("Verde") && !i.Stato.Equals("Dimesso") && !i.Stato.Equals("In Osservazione OBI"))
                {
                    indiceVerde++;
                }
                else if (i.Colore.Equals("Giallo") && !i.Stato.Equals("Dimesso") && !i.Stato.Equals("In Osservazione OBI"))
                {
                    indiceGiallo++;
                }
                else if (i.Colore.Equals("Rosso") && !i.Stato.Equals("Dimesso") && !i.Stato.Equals("In Osservazione OBI"))
                {
                    indiceRosso++;
                }
            }
            returnValue = (indiceBianco * 1) + (indiceVerde * 2) + (indiceGiallo * 3) + (indiceRosso * 4);
            return returnValue;
        }

        public Double getPazientiDimessiPerColore(String sala, String colore, List<RecordBean> lista) 
        {
            DateTime dataAttuale = DateTime.Now;
            DateTime c = new DateTime(dataAttuale.Year, dataAttuale.Month, dataAttuale.Day, dataAttuale.Hour - 2, dataAttuale.Minute, dataAttuale.Second);
            dataAttuale = c;
            DateTime d = c;
            dataAttuale = d;
            double numeroPazienti = 0;
            foreach (var i in lista)
            {
                if (i.Stato.Equals("Dimesso") && i.Colore.Equals(colore) && i.Salaprimotriage.Equals(sala)) {
                    String strData = i.Datadimissione + " " + i.Oradimissione + ":00";
                    DateTime dataEvento = Convert.ToDateTime(strData);
                    if (dataAttuale.CompareTo(dataEvento) < 0) {
                        numeroPazienti++;
                    }
                }
            }
            return (numeroPazienti);
        }

        public int getAbbandoni(List<RecordBean> lista)
        {
            int numeroPaziente = 0;
            foreach (var i in lista)
            {
                if (i.Modalitadimissione.Contains("ABBANDONA"))
                {
                    numeroPaziente++;
                }
            }
            return numeroPaziente;
        }

        public int getNumeroDimessi(List<RecordBean> lista)
        {
            int numeroPaziente = 0;
            foreach (var i in lista)
            {
                if (!String.IsNullOrEmpty(i.Modalitadimissione))
                {
                    numeroPaziente++;
                }
            }
            return numeroPaziente;
        }

        public Tempi getTempoMedio(String sala, String colore, List<RecordBean> lista) 
        {
            DateTime dataAttuale = DateTime.Now;
            DateTime c = new DateTime(dataAttuale.Year, dataAttuale.Month, dataAttuale.Day, 0, 0, 0);
            dataAttuale = c;
            DateTime d = c;
            dataAttuale = d;

            Tempi returnValue = new Tempi("", "");
            int numeroPazienti = 0;
            int valoreMassimo = 0;
            int valorePresaincarico = 0;
            int somma = 0;
            foreach (var i in lista)
            {
                if (i.Colore.Equals(colore) && i.Salaprimotriage.Equals(sala) && i.Stato.Equals("Dimesso"))
                {
                    if (!i.Minutidimissione.Equals("") && !i.Modalitadimissione.Contains("ABBANDONA"))
                    {
                        String strData = i.Datadimissione + " " + i.Oradimissione + ":00";
                        DateTime dataEvento = Convert.ToDateTime(strData);
                        numeroPazienti++;
                        String mp = i.Minutipresaincarico.Equals("") ? "0" : i.Minutipresaincarico;
                        int minutidimissione = Convert.ToInt16(i.Minutidimissione);
                        int minutipresaincarico = Convert.ToInt16(mp);
                        int valore = minutipresaincarico + minutidimissione;
                        valorePresaincarico += minutidimissione;
                        somma += valore;
                        if (valore > valoreMassimo)
                        {
                            valoreMassimo = valore;
                        }
                    }
                }
            }
            if (numeroPazienti > 0) {
                returnValue.tempoMassimo = (valorePresaincarico / numeroPazienti).ToString();
                returnValue.tempoMedio = (somma / numeroPazienti).ToString();
            } else {
                returnValue.tempoMassimo = "---";
                returnValue.tempoMedio = "---";
            }
            return returnValue;
        }

        public string getAbbandoniPercentage(List<RecordBean> lista)
        {
            string percentualeAbbandoni;
            int result;
            if (getNumeroDimessi(lista) != 0)
            {
                result = getAbbandoni(lista) * 100 / getNumeroDimessi(lista);
            }
            else
                result = 0;
            percentualeAbbandoni = result.ToString() + "%";
            return percentualeAbbandoni;
        }

        public int totaleAccessi(List<RecordBean> lista)
        {
            if (lista.Count != 0)
            {
                return lista.Count;
            }
            else
                return 0;
        }

        public double TotalePazientiBianco(List<RecordBean> lista)
        {
            double a, b, c;
            a = getPazientiDimessiPerColore("CHIRURGICO", "Bianco", lista);
            b = getPazientiDimessiPerColore("MEDICO", "Bianco", lista);
            c = getPazientiDimessiPerColore("ORTOPEDICO", "Bianco", lista);
            return a + b + c;
        }

        public double TotalePazientiGiallo(List<RecordBean> lista)
        {
            double a, b, c;
            a = getPazientiDimessiPerColore("CHIRURGICO", "Giallo", lista);
            b = getPazientiDimessiPerColore("MEDICO", "Giallo", lista);
            c = getPazientiDimessiPerColore("ORTOPEDICO", "Giallo", lista);
            return a + b + c;
        }

        public double TotalePazientiVerde(List<RecordBean> lista)
        {
            double a, b, c;
            a = getPazientiDimessiPerColore("CHIRURGICO", "Verde", lista);
            b = getPazientiDimessiPerColore("MEDICO", "Verde", lista);
            c = getPazientiDimessiPerColore("ORTOPEDICO", "Verde", lista);
            return a + b + c;
        }

        public double TotalePazientiRosso(List<RecordBean> lista)
        {
            double a, b, c;
            a = getPazientiDimessiPerColore("CHIRURGICO", "Rosso", lista);
            b = getPazientiDimessiPerColore("MEDICO", "Rosso", lista);
            c = getPazientiDimessiPerColore("ORTOPEDICO", "Rosso", lista);
            return a + b + c;
        }

        public double TotalePazientiChirurgico(List<RecordBean> lista)
        {
            double a, b, c, d;
            a = getPazientiDimessiPerColore("CHIRURGICO", "Rosso", lista);
            b = getPazientiDimessiPerColore("CHIRURGICO", "Bianco", lista);
            c = getPazientiDimessiPerColore("CHIRURGICO", "Verde", lista);
            d = getPazientiDimessiPerColore("CHIRURGICO", "Giallo", lista);
            return a + b + c + d;
        }

        public double TotalePazientiMedico(List<RecordBean> lista)
        {
            double a, b, c, d;
            a = getPazientiDimessiPerColore("MEDICO", "Rosso", lista);
            b = getPazientiDimessiPerColore("MEDICO", "Bianco", lista);
            c = getPazientiDimessiPerColore("MEDICO", "Verde", lista);
            d = getPazientiDimessiPerColore("MEDICO", "Giallo", lista);
            return a + b + c + d;
        }

        public double TotalePazientiOrtopedico(List<RecordBean> lista)
        {
            double a, b, c, d;
            a = getPazientiDimessiPerColore("ORTOPEDICO", "Rosso", lista);
            b = getPazientiDimessiPerColore("ORTOPEDICO", "Bianco", lista);
            c = getPazientiDimessiPerColore("ORTOPEDICO", "Verde", lista);
            d = getPazientiDimessiPerColore("ORTOPEDICO", "Giallo", lista);
            return a + b + c + d;
        }

        public double TotaleGenerale(List<RecordBean> lista)
        {
            double a, b, c, d;
            a = TotalePazientiBianco(lista);
            b = TotalePazientiRosso(lista);
            c = TotalePazientiGiallo(lista);
            d = TotalePazientiVerde(lista);
            return a + b + c + d;
        }

        public double setIndiceEdwin(List<RecordBean> lista)
        {
            double a = getNumeroPazientiPresiInCarico(lista);
            double b = getNumeroPazientiAttesaReferto(lista);
            double c = getNumeroPazientiDimessi(lista);
            double d = 70;
            double e = getNumeroRespiratoriInUso(lista);
            double f = 4;
            double g = 2;
            double nedocs = -20 + 85.8 * (a / b) + 600 * (c / d) + 13.4 * (e / 3) + 0.93 * f + 5.64 * g;
            double numeroPazientiPerCodiceTriage = getNumeroPazientiPerCodiceTriage(lista);
            double numeroMediciInServizio = 7;
            double numeroPazientiInTrattamento = 15;
            double numeroPazientiRicoverati = c;
            double edwin = numeroPazientiPerCodiceTriage / (numeroMediciInServizio * (numeroPazientiInTrattamento - numeroPazientiRicoverati));
            return edwin;
        }

        public string setLabelIndiciEdwin(List<RecordBean> lista)
        {
            double indice = setIndiceEdwin(lista);
            string labelMessage = "";
            if (indice < 1.5)
            {
                labelMessage = "Normale" + indice.ToString("0.00");
            }
            else if (indice < 2.5)
            {
                labelMessage = "Affollato" + indice.ToString("0.00");
            }
            else
            {
                labelMessage = "Congestionato" + indice.ToString("0.00");
            }
            return labelMessage;
        }

        public Color setColoreLabelIndiceEdwin(List<RecordBean> lista)
        {
            string colore = "#000000";
            double indice = setIndiceEdwin(lista);
            if (indice < 1.5)
            {
                colore = "#146A22";
                //"Normale";
            }
            else if (indice < 2.5)
            {
                colore = "#ff9b00";
                //"Affollato";
            }
            else
            {
                colore = "#4d4d4d";
                // "Congestionato";
            }
            return Color.FromHex(colore);
        }

        public void setIndiceNedocs(int indicatore)
        {
            // Va settata una eventuale label
            //TextView lblPsMonitorIndiceNedocs = (TextView)findViewById(R.id.lblPsMonitorIndiceNedocs);
            Label lblIndiceNedocs = new Label();
            if (indicatore <= 50)
            {
                lblIndiceNedocs.Text = indicatore.ToString();
                lblIndiceNedocs.TextColor = Color.FromHex("#ff00FF00");
            }
            else if (indicatore <= 100)
            {
                lblIndiceNedocs.Text = indicatore.ToString();
                lblIndiceNedocs.TextColor = Color.FromHex("ffFFFF00");
            }
            else if (indicatore <= 140)
            {
                lblIndiceNedocs.Text = indicatore.ToString();
                lblIndiceNedocs.TextColor = Color.FromHex("ffff9b00");
            }
            else if (indicatore <= 180)
            {
                lblIndiceNedocs.Text = indicatore.ToString();
                lblIndiceNedocs.TextColor = Color.FromHex("ffFF0000");
            }
            else
            {
                lblIndiceNedocs.Text = indicatore.ToString();
                lblIndiceNedocs.TextColor = Color.FromHex("ff4d4d4d");
            }
        }
    }
}
