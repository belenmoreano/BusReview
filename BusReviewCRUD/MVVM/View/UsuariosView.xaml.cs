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
    /// Lógica de interacción para UsuariosView.xaml
    /// </summary>
    public partial class UsuariosView : UserControl
    {
        HttpClient client = new HttpClient();
        public UsuariosView()
        {
            client.BaseAddress = new Uri("https://localhost:44380/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            InitializeComponent();
        }

        private void btnCargarUsuario_Click(object sender, RoutedEventArgs e)
        {
            this.GetUsuarios();
        }

        private async void GetUsuarios()
        {
            var response = await client.GetStringAsync("usuarios");
            var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(response);
            dgUsuarios.DataContext = usuarios;
        }

        private async void SaveUsuarios(Usuario usuario)
        {
            await client.PostAsJsonAsync("usuarios", usuario);
        }

        private async void UpdateUsuarios(Usuario usuario)
        {
            await client.PutAsJsonAsync("usuarios/"+ usuario.UsuarioId, usuario);
        }
        private async void DeleteUsuarios(int id)
        {
            await client.DeleteAsync("usuarios/"+ id);
        }

        private void btnGuardarUsuario_Click(object sender, RoutedEventArgs e)
        {

            var usuario = new Usuario()
            {
                UsuarioId = Convert.ToInt32(txtId.Text),
                Nombre = txtName.Text,
                Apellido = txtApellido.Text,
                Correo = txtCorreo.Text,
                Contrasena = txtContrasenia.Text,
                Fecha_Nacimiento = Convert.ToDateTime(dpFecha.SelectedDate),
                Administrador = cbAdmin.IsEnabled.Equals(true)
                
            };

            if (usuario.UsuarioId == 0)
            {
                this.SaveUsuarios(usuario);
            }
            else
            {
                this.UpdateUsuarios(usuario);
            }
            txtId.Text = 0.ToString();
            txtName.Text = "";
            txtApellido.Text = "";
            txtCorreo.Text = "";
            dpFecha.SelectedDate = DateTime.Today;
            txtContrasenia.Text = "";
            cbAdmin.IsChecked = default;
        }

        void btnEditarUsuario(object sender, RoutedEventArgs e)
        {
            Usuario usuario = ((FrameworkElement)sender).DataContext as Usuario;
            txtId.Text = usuario.UsuarioId.ToString();
            txtName.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            txtCorreo.Text = usuario.Correo;
            dpFecha.SelectedDate = usuario.Fecha_Nacimiento;
            txtContrasenia.Text = usuario.Contrasena;
            cbAdmin.IsChecked = usuario.Administrador;
        }

        void btnEliminarUsuario(object sender, RoutedEventArgs e)
        {
            Usuario usuario = ((FrameworkElement)sender).DataContext as Usuario;
            this.DeleteUsuarios(usuario.UsuarioId);
        }
    }
}
