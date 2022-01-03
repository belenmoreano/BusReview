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
    /// Lógica de interacción para BusesView.xaml
    /// </summary>
    public partial class BusesView : UserControl
    {
        HttpClient client = new HttpClient();
        public BusesView()
        {
            client.BaseAddress = new Uri("https://localhost:44380/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }
        private async void GetBuses()
        {
            var response = await client.GetStringAsync("buses");
            var buses = JsonConvert.DeserializeObject<List<Bus>>(response);
            dgBuses.DataContext = buses;
        }
        private async void SaveBus(Bus buses)
        {
            await client.PostAsJsonAsync("buses/", buses);
        }

        private async void UpdateBus(Bus buses)
        {
            await client.PutAsJsonAsync("buses/" + buses.Placa, buses);
        }
        private async void DeleteBus(int busId)
        {
            await client.DeleteAsync("buses/" + busId);
        }


        private void btnCargarBus_Click(object sender, RoutedEventArgs e)
        {
            this.GetBuses();
        }

        private void btnGuardarBus_Click(object sender, RoutedEventArgs e)
        {
            var buses = new Bus()
            {
                BusId = Convert.ToInt32(txtBusId.Text),
                Placa = txtPlaca.Text,
                Marca = txtMarca.Text,
                Nombres_Chofer = txtNombreC.Text,
                Cedula_Chofer = txtCedulaC.Text,
                Nombres_Asistente = txtNombreA.Text,
                Cedula_Asistente = txtCedulaA.Text,
                Cooperativa = txtCooperativa.Text,
                Sector = txtSector.Text,
                Wifi = cbWifi.IsEnabled.Equals(true),
                TV = cbTv.IsEnabled.Equals(true),
                Baño = cbBano.IsEnabled.Equals(true),
                Asientos_discapacitados = cbAsientos.IsEnabled.Equals(true)
            };

            if (buses.BusId == 0)
            {
                this.SaveBus(buses);
            }
            else
            {
                this.UpdateBus(buses);
            }
            txtBusId.Text = 0.ToString();
            txtPlaca.Text = "";
            txtMarca.Text = "";
            txtNombreC.Text = "";
            txtCedulaC.Text = "";
            txtNombreA.Text = "";
            txtCedulaA.Text = "";
            txtCooperativa.Text = "";
            txtSector.Text = "";
            cbWifi.IsChecked = default;
            cbTv.IsChecked = default;
            cbBano.IsChecked = default;
            cbAsientos.IsChecked = default;

        }

        void btnEditarBus(object sender, RoutedEventArgs e)
        {
            Bus buses = ((FrameworkElement)sender).DataContext as Bus;
            txtBusId.Text = buses.BusId.ToString();
            txtPlaca.Text = buses.Placa;
            txtMarca.Text = buses.Marca;
            txtNombreC.Text = buses.Nombres_Chofer;
            txtCedulaC.Text = buses.Cedula_Chofer;
            txtNombreA.Text = buses.Nombres_Asistente;
            txtCedulaA.Text = buses.Cedula_Asistente;
            txtCooperativa.Text = buses.Cooperativa;
            txtSector.Text = buses.Sector;
            cbWifi.IsChecked = buses.Wifi;
            cbTv.IsChecked = buses.TV;
            cbBano.IsChecked = buses.Baño;
            cbAsientos.IsChecked = buses.Asientos_discapacitados;
        }

        void btnEliminarBus(object sender, RoutedEventArgs e)
        {
            Bus buses = ((FrameworkElement)sender).DataContext as Bus;
            this.DeleteBus(buses.BusId);
        }
    }
}
