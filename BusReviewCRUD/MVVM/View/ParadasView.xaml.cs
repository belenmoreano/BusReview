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
    /// Lógica de interacción para ParadasView.xaml
    /// </summary>
    public partial class ParadasView : UserControl
    {
        HttpClient client = new HttpClient();
        public ParadasView()
        {
            client.BaseAddress = new Uri("https://localhost:44380/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }
        private async void GetParadas()
        {
            var response = await client.GetStringAsync("paradas");
            var buses = JsonConvert.DeserializeObject<List<Parada>>(response);
            dgParada.DataContext = buses;
        }
        private async void SaveParada(Parada paradas)
        {
            await client.PostAsJsonAsync("paradas/", paradas);
        }

        private async void UpdateParada(Parada paradas)
        {
            await client.PutAsJsonAsync("paradas/" + paradas.ParadaId, paradas);
        }
        private async void DeleteParada(int paradaId)
        {
            await client.DeleteAsync("paradas/" + paradaId);
        }


        private void btnCargarParada_Click(object sender, RoutedEventArgs e)
        {
            this.GetParadas();
        }

        private void btnGuardarParada_Click(object sender, RoutedEventArgs e)
        {
            var paradas = new Parada()
            {
                ParadaId = Convert.ToInt32(txtIdParada.Text),
                Nombre = txtNombre.Text,
                Sector = txtSector.Text,
                Callep = txtCallep.Text,
                Calles =txtCalles.Text,
                Costo = Convert.ToDecimal(txtCosto.Text)
               
            };

            if (paradas.ParadaId == 0)
            {
                this.SaveParada(paradas);
            }
            else
            {
                this.UpdateParada(paradas);
            }
            txtIdParada.Text = 0.ToString();
            txtNombre.Text = "";
            txtSector.Text = "";
            txtCallep.Text = "";
            txtCalles.Text = "";
            txtCosto.Text = 0.ToString();

        }

        void btnEditarParada(object sender, RoutedEventArgs e)
        {
            Parada paradas = ((FrameworkElement)sender).DataContext as Parada;
            txtIdParada.Text = paradas.ParadaId.ToString();
            txtNombre.Text = paradas.Nombre;
            txtSector.Text = paradas.Sector;
            txtCallep.Text = paradas.Callep;
            txtCalles.Text = paradas.Calles;
            txtCosto.Text = paradas.Costo.ToString();
        }

        void btnEliminarParada(object sender, RoutedEventArgs e)
        {
            Parada paradas = ((FrameworkElement)sender).DataContext as Parada;
            this.DeleteParada(paradas.ParadaId);
        }
    }
}
