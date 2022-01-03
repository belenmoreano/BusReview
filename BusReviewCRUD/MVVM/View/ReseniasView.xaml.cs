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
    /// Lógica de interacción para ReseniasView.xaml
    /// </summary>
    public partial class ReseniasView : UserControl
    {
        HttpClient client = new HttpClient();
        public ReseniasView()
        {
            client.BaseAddress = new Uri("https://localhost:44380/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }
        private async void GetResenas()
        {
            var response = await client.GetStringAsync("resenas");
            var resenas = JsonConvert.DeserializeObject<List<Resena>>(response);
            dgResena.DataContext = resenas;
        }
        private async void DeleteResenas(int resenaId)
        {
            await client.DeleteAsync("resenas/" + resenaId);
        }
        private void btnCargarResena_Click(object sender, RoutedEventArgs e)
        {
            this.GetResenas();
        }
        private void btnEliminarResena_Click(object sender, RoutedEventArgs e)
        {
            Resena resenas = ((FrameworkElement)sender).DataContext as Resena;
            this.DeleteResenas(resenas.ResenaId);
        }

        void btnVerResena(object sender, RoutedEventArgs e)
        {
            Resena resenas = ((FrameworkElement)sender).DataContext as Resena;
            txtPlaca.Text = resenas.Placa;
            txtCalf.Text = resenas.Calificacion.ToString();
            txtUsuario.Text = resenas.Usuario;
            txtNota.Text = resenas.Nota;
        }
    }
}
