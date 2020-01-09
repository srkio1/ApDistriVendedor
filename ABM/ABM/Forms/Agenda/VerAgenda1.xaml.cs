using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ABM.Forms.Agenda
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VerAgenda1 : ContentPage
	{
        public VerAgenda1(string titulo, DateTime fecha, string hora, string descripcion, string estado)
        {
            InitializeComponent();
            txttitulo.Text = titulo;
            txtfecha.Text = fecha.ToString("dd/MM/yyy");
            txthora.Text = hora;
            txtdescripcion.Text = descripcion;
            txtestado.Text = estado;
        }
        private void MostrarAgenda()
        {
            // txttitulo.Text = titulo;
        }
    }
}