using ClinicaMedica.Data;
using ClinicaMedica.Models.Clases;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaMedica.Views.Forms
{
    public partial class LoginForm : Form
    {
        public Usuario UsuarioLogueado { get; private set; }
        private bool procesandoLogin = false;

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (procesandoLogin) return;

            string usuario = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, ingrese usuario y contraseña", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            procesandoLogin = true;
            btnLogin.Enabled = false;
            btnCancelar.Enabled = false;

            try
            {
                // Mostrar mensaje de carga
                statusLabel.Text = "Verificando credenciales...";

                // Ejecutar validación en segundo plano
                UsuarioLogueado = await Task.Run(() => ValidarCredenciales(usuario, password));

                if (UsuarioLogueado != null)
                {
                    Logger.LogActivity($"Inicio de sesión exitoso: {usuario}");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Logger.LogActivity($"Intento de inicio de sesión fallido: {usuario}");
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtUsuario.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                MessageBox.Show("Error al intentar iniciar sesión: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                procesandoLogin = false;
                btnLogin.Enabled = true;
                btnCancelar.Enabled = true;
                statusLabel.Text = "Listo";
            }
        }

        private Usuario ValidarCredenciales(string usuario, string password)
        {
            var dbHelper = new DatabaseHelper();
            return dbHelper.ValidarUsuario(usuario, password);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (procesandoLogin) return;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !procesandoLogin)
            {
                btnLogin.PerformClick();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            statusLabel.Text = "Listo";
        }
    }
}