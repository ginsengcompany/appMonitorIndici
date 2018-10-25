using AppMonitorIndici.Bean;
using AppMonitorIndici.bo;
using AppMonitorIndici.utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace AppMonitorIndici
{
    public class Indici : INotifyPropertyChanged
    {
        private List<RecordBean> lista = new List<RecordBean>();
        public ElencoPazientiBO elencoPazientiBO = new ElencoPazientiBO();
        public CallAPI callApi = new CallAPI();
        public bool _IsBusy = true;

        public string _tempiChirurgicoBianchiMedi = "--";
        public string _tempiChirurgicoVerdiMedi = "--";
        public string _tempiChirurgicoGialliMedi = "--";
        public string _tempiChirurgicoRossiMedi = "--";
        public string _tempiChirurgicoBianchiMassimi = "--";
        public string _tempiChirurgicoVerdiMassimi = "--";
        public string _tempiChirurgicoGialliMassimi = "--";           
        public string _tempiChirurgicoRossiMassimi = "--";            
        public string _tempiMedicoBianchiMedi = "--";                 
        public string _tempiMedicoVerdiMedi = "--";                   
        public string _tempiMedicoGialliMedi = "--";                  
        public string _tempiMedicoRossiMedi = "--";                   
        public string _tempiMedicoBianchiMassimi = "--";              
        public string _tempiMedicoVerdiMassimi = "--";                
        public string _tempiMedicoGialliMassimi = "--";               
        public string _tempiMedicoRossiMassimi = "--";                
        public string _tempiOrtopedicoBianchiMedi = "--";             
        public string _tempiOrtopedicoVerdiMedi = "--";               
        public string _tempiOrtopedicoGialliMedi = "--";              
        public string _tempiOrtopedicoRossiMedi = "--";               
        public string _tempiOrtopedicoBianchiMassimi = "--";          
        public string _tempiOrtopedicoVerdiMassimi = "--";            
        public string _tempiOrtopedicoGialliMassimi = "--";           
        public string _tempiOrtopedicoRossiMassimi = "--";            

        public string _valoreIndiceEdwin;
        public string _statoIndiceEdwin = "Edwin Index";
        public Color _coloreIndiceEdwin;
        
        public string _bianchiChirurgico = "0";       
        public string _verdiChirurgico = "0";         
        public string _gialliChirurgico = "0";        
        public string _rossiChirurgico = "0";                 
        public string _bianchiMedico = "0";           
        public string _verdiMedico = "0";             
        public string _gialliMedico = "0";            
        public string _rossiMedico = "0";                  
        public string _bianchiOrtopedico = "0";       
        public string _verdiOrtopedico = "0";         
        public string _gialliOrtopedico = "0";        
        public string _rossiOrtopedico = "0";                   
        public string _totaleBianco = "0";            
        public string _totaleVerde = "0";             
        public string _totaleGiallo = "0";            
        public string _totaleRosso = "0";             
        public string _totaleChirurgico = "0";        
        public string _totaleMedico = "0";            
        public string _totaleOrtopedico = "0";                  
        public string _totaleGenerale = "0";          

        public int _numeroPazientiDimessi = 0;
        public string _percentualeAbbandoni = "0";
        public string _totaleAccessi = "0";

        public Indici()
        {
            AggiornamentoDati();
        }

        public void AggiornamentoDati()
        {
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                if (lista.Count != 0)
                {
                    lista.Clear();
                    ConnessioneServizio();
                }
                else
                {
                    ConnessioneServizio();
                }
                return true;
            });
        }

        public async void ConnessioneServizio()
        {
            lista = await callApi.doInBackground();
            if (lista.Count == 0)
            {
                IsBusy = true;
            }
            else
            {
                aggiornamentoPagina();
            }
        }

        public void aggiornamentoPagina()
        {
            IsBusy = false;
            NumeroPazientiDimessi = elencoPazientiBO.getNumeroDimessi(lista);
            PercentualeAbbandoni = elencoPazientiBO.getAbbandoniPercentage(lista);
            StatoIndiceEdwin = elencoPazientiBO.setLabelIndiciEdwin(lista);
            ColoreIndiceEdwin = elencoPazientiBO.setColoreLabelIndiceEdwin(lista);
            TotaleAccessi = elencoPazientiBO.totaleAccessi(lista).ToString();

            TotaleBianco = elencoPazientiBO.TotalePazientiBianco(lista).ToString();
            TotaleGiallo = elencoPazientiBO.TotalePazientiGiallo(lista).ToString();
            TotaleVerde = elencoPazientiBO.TotalePazientiVerde(lista).ToString();
            TotaleRosso = elencoPazientiBO.TotalePazientiRosso(lista).ToString();
            TotaleMedico = elencoPazientiBO.TotalePazientiMedico(lista).ToString();
            TotaleChirurgico = elencoPazientiBO.TotalePazientiChirurgico(lista).ToString();
            TotaleOrtopedico = elencoPazientiBO.TotalePazientiOrtopedico(lista).ToString();
            TotaleGenerale = elencoPazientiBO.TotaleGenerale(lista).ToString();

            AggiornamentoTempiDiAttesaChirurgicoBianco = elencoPazientiBO.getTempoMedio("CHIRURGICO", "Bianco", lista).tempoMedio;
            AggiornamentoTempiDiAttesaChirurgicoVerdi = elencoPazientiBO.getTempoMedio("CHIRURGICO", "Verde", lista).tempoMedio;
            AggiornamentoTempiDiAttesaChirurgicoGialli = elencoPazientiBO.getTempoMedio("CHIRURGICO", "Giallo", lista).tempoMedio;
            AggiornamentoTempiDiAttesaChirurgicoRossi = elencoPazientiBO.getTempoMedio("CHIRURGICO", "Rosso", lista).tempoMedio;
            AggiornamentoTempiDiAttesaChirurgicoBiancoMassimi = elencoPazientiBO.getTempoMedio("CHIRURGICO", "Bianco", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaChirurgicoVerdiMassimi = elencoPazientiBO.getTempoMedio("CHIRURGICO", "Verde", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaChirurgicoGialliMassimi = elencoPazientiBO.getTempoMedio("CHIRURGICO", "Giallo", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaChirurgicoRossiMassimi = elencoPazientiBO.getTempoMedio("CHIRURGICO", "Rosso", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaMedicoBianco = elencoPazientiBO.getTempoMedio("MEDICO", "Bianco", lista).tempoMedio;
            AggiornamentoTempiDiAttesaMedicoVerdi = elencoPazientiBO.getTempoMedio("MEDICO", "Verde", lista).tempoMedio;
            AggiornamentoTempiDiAttesaMedicoGialli = elencoPazientiBO.getTempoMedio("MEDICO", "Giallo", lista).tempoMedio;
            AggiornamentoTempiDiAttesaMedicoRossi = elencoPazientiBO.getTempoMedio("MEDICO", "Rosso", lista).tempoMedio;
            AggiornamentoTempiDiAttesaMedicoBiancoMassimi = elencoPazientiBO.getTempoMedio("MEDICO", "Bianco", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaMedicoVerdiMassimi = elencoPazientiBO.getTempoMedio("MEDICO", "Verde", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaMedicoGialliMassimi = elencoPazientiBO.getTempoMedio("MEDICO", "Giallo", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaMedicoRossiMassimi = elencoPazientiBO.getTempoMedio("MEDICO", "Rosso", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaOrtopedicoBianco = elencoPazientiBO.getTempoMedio("ORTOPEDICO", "Bianco", lista).tempoMedio;
            AggiornamentoTempiDiAttesaOrtopedicoVerdi = elencoPazientiBO.getTempoMedio("ORTOPEDICO", "Verde", lista).tempoMedio;
            AggiornamentoTempiDiAttesaOrtopedicoGialli = elencoPazientiBO.getTempoMedio("ORTOPEDICO", "Giallo", lista).tempoMedio;
            AggiornamentoTempiDiAttesaOrtopedicoRossi = elencoPazientiBO.getTempoMedio("ORTOPEDICO", "Rosso", lista).tempoMedio;
            AggiornamentoTempiDiAttesaOrtopedicoBiancoMassimi = elencoPazientiBO.getTempoMedio("ORTOPEDICO", "Bianco", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaOrtopedicoVerdiMassimi = elencoPazientiBO.getTempoMedio("ORTOPEDICO", "Verde", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaOrtopedicoGialliMassimi = elencoPazientiBO.getTempoMedio("ORTOPEDICO", "Giallo", lista).tempoMassimo;
            AggiornamentoTempiDiAttesaOrtopedicoRossiMassimi = elencoPazientiBO.getTempoMedio("ORTOPEDICO", "Rosso", lista).tempoMassimo;
            AggiornamentoPazientiDimessiChirurgicoBianco = elencoPazientiBO.getPazientiDimessiPerColore("CHIRURGICO", "Bianco", lista).ToString();
            AggiornamentoPazientiDimessiChirurgicoVerde = elencoPazientiBO.getPazientiDimessiPerColore("CHIRURGICO", "Verde", lista).ToString();
            AggiornamentoPazientiDimessiChirurgicoGiallo = elencoPazientiBO.getPazientiDimessiPerColore("CHIRURGICO", "Giallo", lista).ToString();
            AggiornamentoPazientiDimessiChirurgicoRosso = elencoPazientiBO.getPazientiDimessiPerColore("CHIRURGICO", "Rosso", lista).ToString();
            AggiornamentoPazientiDimessiMedicoBianco = elencoPazientiBO.getPazientiDimessiPerColore("MEDICO", "Bianco", lista).ToString();
            AggiornamentoPazientiDimessiMedicoVerde = elencoPazientiBO.getPazientiDimessiPerColore("MEDICO", "Verde", lista).ToString();
            AggiornamentoPazientiDimessiMedicoGiallo = elencoPazientiBO.getPazientiDimessiPerColore("MEDICO", "Giallo", lista).ToString();
            AggiornamentoPazientiDimessiMedicoRosso = elencoPazientiBO.getPazientiDimessiPerColore("MEDICO", "Rosso", lista).ToString();
            AggiornamentoPazientiDimessiOrtopedicoBianco = elencoPazientiBO.getPazientiDimessiPerColore("ORTOPEDICO", "Bianco", lista).ToString();
            AggiornamentoPazientiDimessiOrtopedicoVerde = elencoPazientiBO.getPazientiDimessiPerColore("ORTOPEDICO", "Verde", lista).ToString();
            AggiornamentoPazientiDimessiOrtopedicoGiallo = elencoPazientiBO.getPazientiDimessiPerColore("ORTOPEDICO", "Giallo", lista).ToString();
            AggiornamentoPazientiDimessiOrtopedicoRosso = elencoPazientiBO.getPazientiDimessiPerColore("ORTOPEDICO", "Rosso", lista).ToString();
        }

        public bool IsBusy
        {
            set
            {
                _IsBusy = value;
                OnPropertychanged();
            }
            get
            {
                return _IsBusy;
            }
        }

        public string TotaleAccessi
        {
            set
            {
                _totaleAccessi = value;
                OnPropertychanged();
            }
            get
            {
                return _totaleAccessi;
            }
        }

        public string ValoreIndiceEdwin
        {
            set
            {
                _valoreIndiceEdwin = value;
                OnPropertychanged();
            }
            get
            {
                return _valoreIndiceEdwin;
            }
        }

        public string StatoIndiceEdwin
        {
            set
            {
                _statoIndiceEdwin = value;
                OnPropertychanged();
            }
            get
            {
                return _statoIndiceEdwin;
            }
        }

        public Color ColoreIndiceEdwin
        {
            set
            {
                _coloreIndiceEdwin = value;
                OnPropertychanged();
            }
            get
            {
                return _coloreIndiceEdwin;
            }
        }

        public int NumeroPazientiDimessi
        {
            set
            {
                _numeroPazientiDimessi = value;
                OnPropertychanged();
            }
            get
            {
                return _numeroPazientiDimessi;
            }
        }

        public string PercentualeAbbandoni
        {
            set
            {
                _percentualeAbbandoni = value;
                OnPropertychanged();
            }
            get
            {
                return _percentualeAbbandoni;
            }
        }

        public string AggiornamentoTempiDiAttesaChirurgicoBianco
        {
            set
            {
                _tempiChirurgicoBianchiMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiChirurgicoBianchiMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaChirurgicoVerdi
        {
            set
            {
                _tempiChirurgicoVerdiMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiChirurgicoVerdiMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaChirurgicoGialli
        {
            set
            {
                _tempiChirurgicoGialliMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiChirurgicoGialliMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaChirurgicoRossi
        {
            set
            {
                _tempiChirurgicoRossiMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiChirurgicoRossiMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaChirurgicoBiancoMassimi
        {
            set
            {
                _tempiChirurgicoBianchiMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiChirurgicoBianchiMassimi;
            }
        }

        public string AggiornamentoTempiDiAttesaChirurgicoVerdiMassimi
        {
            set
            {
                _tempiChirurgicoVerdiMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiChirurgicoVerdiMassimi;
            }
        }

        public string AggiornamentoTempiDiAttesaChirurgicoGialliMassimi
        {
            set
            {
                _tempiChirurgicoGialliMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiChirurgicoGialliMassimi;
            }
        }

        public string AggiornamentoTempiDiAttesaChirurgicoRossiMassimi
        {
            set
            {
                _tempiChirurgicoRossiMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiChirurgicoRossiMassimi;
            }
        }

        public string AggiornamentoTempiDiAttesaMedicoBianco
        {
            set
            {
                _tempiMedicoBianchiMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiMedicoBianchiMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaMedicoVerdi
        {
            set
            {
                _tempiMedicoVerdiMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiMedicoVerdiMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaMedicoGialli
        {
            set
            {
                _tempiMedicoGialliMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiMedicoGialliMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaMedicoRossi
        {
            set
            {
                _tempiMedicoRossiMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiMedicoRossiMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaMedicoBiancoMassimi
        {
            set
            {
                _tempiMedicoBianchiMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiMedicoBianchiMassimi;
            }
        }

        public string AggiornamentoTempiDiAttesaMedicoVerdiMassimi
        {
            set
            {
                _tempiMedicoVerdiMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiMedicoVerdiMassimi;
            }
        }

        public string AggiornamentoTempiDiAttesaMedicoGialliMassimi
        {
            set
            {
                _tempiMedicoGialliMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiMedicoGialliMassimi;
            }
        }

        public string AggiornamentoTempiDiAttesaMedicoRossiMassimi
        {
            set
            {
                _tempiMedicoRossiMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiMedicoRossiMassimi;
            }
        }



        public string AggiornamentoTempiDiAttesaOrtopedicoBianco
        {
            set
            {
                _tempiOrtopedicoBianchiMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiOrtopedicoBianchiMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaOrtopedicoVerdi
        {
            set
            {
                _tempiOrtopedicoVerdiMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiOrtopedicoVerdiMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaOrtopedicoGialli
        {
            set
            {
                _tempiOrtopedicoGialliMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiOrtopedicoGialliMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaOrtopedicoRossi
        {
            set
            {
                _tempiOrtopedicoRossiMedi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiOrtopedicoRossiMedi;
            }
        }

        public string AggiornamentoTempiDiAttesaOrtopedicoBiancoMassimi
        {
            set
            {
                _tempiOrtopedicoBianchiMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiOrtopedicoBianchiMassimi;
            }
        }

        public string AggiornamentoTempiDiAttesaOrtopedicoVerdiMassimi
        {
            set
            {
                _tempiOrtopedicoVerdiMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiOrtopedicoVerdiMassimi;
            }
        }

        public string AggiornamentoTempiDiAttesaOrtopedicoGialliMassimi
        {
            set
            {
                _tempiOrtopedicoGialliMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiOrtopedicoGialliMassimi;
            }
        }

        public string AggiornamentoTempiDiAttesaOrtopedicoRossiMassimi
        {
            set
            {
                _tempiOrtopedicoRossiMassimi = value;
                OnPropertychanged();
            }
            get
            {
                return _tempiOrtopedicoRossiMassimi;
            }
        }

        public string AggiornamentoPazientiDimessiChirurgicoBianco
        {
            set
            {
                _bianchiChirurgico = value;
                OnPropertychanged();
            }
            get
            {
                return _bianchiChirurgico;
            }
        }

        public string AggiornamentoPazientiDimessiChirurgicoVerde
        {
            set {
                _verdiChirurgico = value;
                OnPropertychanged();
            }
            get
            {
                return _verdiChirurgico;
            }
        }

        public string AggiornamentoPazientiDimessiChirurgicoGiallo
        {
            set
            {
                _gialliChirurgico = value;
                OnPropertychanged();
            }
            get
            {
                return _gialliChirurgico;
            }
        }

        public string AggiornamentoPazientiDimessiChirurgicoRosso
        {
            set
            {
                _rossiChirurgico = value;
                OnPropertychanged();
            }
            get {
                return _rossiChirurgico;
            }
        }

        public string AggiornamentoPazientiDimessiMedicoBianco
        {
            set {
                _bianchiMedico = value;
                OnPropertychanged();
            }
            get
            {
                return _bianchiMedico;
            }
        }
        public string AggiornamentoPazientiDimessiMedicoVerde
        {
            set {
                _verdiMedico = value;
                OnPropertychanged();
            }
            get
            {
                return _verdiMedico;
            }
        }

        public string AggiornamentoPazientiDimessiMedicoGiallo
        {
            set {
                _gialliMedico = value;
                OnPropertychanged();
            }
            get
            {
                return _gialliMedico;
            }
        }

        public string AggiornamentoPazientiDimessiMedicoRosso
        {
            set {
                _rossiMedico = value;
                OnPropertychanged();
            }
            get
            {
                return _rossiMedico;
            }
        }

        public string AggiornamentoPazientiDimessiOrtopedicoBianco
        {
            set
            {
                _bianchiOrtopedico = value;
                OnPropertychanged();
            }
            get
            {
                return _bianchiOrtopedico;
            }
        }

        public string AggiornamentoPazientiDimessiOrtopedicoVerde
        { set
            {
                _verdiOrtopedico = value;
                OnPropertychanged();
            }
            get
            {
                return _verdiOrtopedico;
            }
        }

        public string AggiornamentoPazientiDimessiOrtopedicoGiallo
        {
            set
            {
                _gialliOrtopedico = value;
                OnPropertychanged();
            }
            get
            {
                return _gialliOrtopedico;
            }
        }

        public string AggiornamentoPazientiDimessiOrtopedicoRosso
        {
            set
            {
                _rossiOrtopedico = value;
                OnPropertychanged();
            }
            get
            {
                return _rossiOrtopedico;
            }
        }

        public string TotaleBianco
        {
            set
            {
                _totaleBianco = value;
                OnPropertychanged();
            }
            get
            {
                return _totaleBianco;
            }
        }

        public string TotaleVerde
        {
            set
            {
                _totaleVerde = value;
                OnPropertychanged();
            }
            get
            {
                return _totaleVerde;
            }
        }

        public string TotaleGiallo
        {
            set
            {
                _totaleGiallo = value;
                OnPropertychanged();
            }
            get
            {
                return _totaleGiallo;
            }
        }

        public string TotaleRosso
        {
            set
            {
                _totaleRosso = value;
                OnPropertychanged();
            }
            get
            {
                return _totaleRosso;
            }
        }

        public string TotaleChirurgico
        {
            set
            {
                _totaleChirurgico = value;
                OnPropertychanged();
            }
            get
            {
                return _totaleChirurgico;
            }
        }
        public string TotaleMedico
        {
            set
            {
                _totaleMedico = value;
                OnPropertychanged();
            }
            get
            {
                return _totaleMedico;
            }
        }
        public string TotaleOrtopedico
        {
            set
            {
                _totaleOrtopedico = value;
                OnPropertychanged();
            }
            get
            {
                return _totaleOrtopedico;
            }
        }
        public string TotaleGenerale
        {
            set
            {
                _totaleGenerale = value;
                OnPropertychanged();
            }
            get
            {
                return _totaleGenerale;
            }
        }

        public List<RecordBean> Lista { get => lista; set => lista = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertychanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}