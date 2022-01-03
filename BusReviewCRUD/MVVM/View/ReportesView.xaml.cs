using BusReviewCRUD.MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BusReviewCRUD.MVVM.View
{
    /// <summary>
    /// Lógica de interacción para ReportesView.xaml
    /// </summary>
    public partial class ReportesView : UserControl
    {
        HttpClient client = new HttpClient();
        public ReportesView()
        {
            client.BaseAddress = new Uri("https://localhost:44380/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }
        private async void GetReportes()
        {
            var response = await client.GetStringAsync("reportes");
            var reportes = JsonConvert.DeserializeObject<List<Reporte>>(response);
            dgReporte.DataContext = reportes;
        }
        private async void DeleteReportes(int reporteId)
        {
            await client.DeleteAsync("reportes/" + reporteId);
        }
        private void btnCargarReporte_Click(object sender, RoutedEventArgs e)
        {
            this.GetReportes();
        }
        private void btnEliminarReporte_Click(object sender, RoutedEventArgs e)
        {
            Reporte reportes = ((FrameworkElement)sender).DataContext as Reporte;
            this.DeleteReportes(reportes.ReporteId);
        }

        void btnVerReporte(object sender, RoutedEventArgs e)
        {
            Reporte reportes = ((FrameworkElement)sender).DataContext as Reporte;
            txtPlaca.Text = reportes.Placa;
            txtUsuario.Text = reportes.Usuario;
            cbAcoso.IsChecked = reportes.Acoso;
            cbMalaC.IsChecked = reportes.Mala_Conduccion;
            txtNota.Text = reportes.Nota;
        }
    }
}
