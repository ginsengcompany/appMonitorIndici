using AppMonitorIndici.Bean;
using AppMonitorIndici.bo;
using AppMonitorIndici.utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppMonitorIndici
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            InstanzaPrincipale();
        }

        public void InstanzaPrincipale()
        {
            BindingContext = new Indici();
        }
    }
}
