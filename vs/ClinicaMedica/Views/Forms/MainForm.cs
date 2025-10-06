using ClinicaMedica.Models.Clases;
using System;
using System.Windows.Forms;

namespace ClinicaMedica.Views.Forms
{
    public partial class MainForm : Form
    {
        private Usuario usuarioLogueado;

        public MainForm(Usuario usuario)
        {
            InitializeComponent();
            usuarioLogueado = usuario;
            ConfigurarInterfazSegunRol();
        }

        private void ConfigurarInterfazSegunRol()
        {
            this.Text = $"Sistema de Gestión de Clínica - Usuario: {usuarioLogueado.NombreUsuario} ({usuarioLogueado.RolNombre})";

            switch (usuarioLogueado.RolId)
            {
                case 1:
                    break;

                case 2:
                    menuPacientes.Enabled = false;
                    menuMedicos.Enabled = false;
                    menuEspecialidades.Enabled = false;
                    menuUsuarios.Enabled = false;
                    break;

                case 3:
                    menuMedicos.Enabled = false;
                    menuEspecialidades.Enabled = false;
                    menuUsuarios.Enabled = false;
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            statusLabel.Text = $"Usuario: {usuarioLogueado.NombreUsuario} | Rol: {usuarioLogueado.RolNombre} | Fecha: {DateTime.Now:dd/MM/yyyy}";
        }

        private void menuPacientes_Click(object sender, EventArgs e)
        {
            AbrirFormularioPacientes();
        }

        private void AbrirFormularioPacientes()
        {
            // Cerrar el panel de bienvenida si está visible
            panelMain.Visible = false;

            // Verificar si ya está abierto el formulario
            foreach (Form form in this.MdiChildren)
            {
                if (form is PacientesForm)
                {
                    form.BringToFront();
                    form.WindowState = FormWindowState.Maximized;
                    return;
                }
            }

            // Crear nuevo formulario
            PacientesForm pacientesForm = new PacientesForm();
            pacientesForm.MdiParent = this;
            pacientesForm.FormClosed += (s, args) => {
                // Al cerrar el formulario, mostrar panel de bienvenida
                panelMain.Visible = true;
            };

            pacientesForm.WindowState = FormWindowState.Maximized;
            pacientesForm.Show();
            pacientesForm.BringToFront();
        }

        private void menuMedicos_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aquí se abrirá el formulario de gestión de médicos", "Médicos",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuCitas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aquí se abrirá el formulario de gestión de citas", "Citas",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuEspecialidades_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aquí se abrirá el formulario de gestión de especialidades", "Especialidades",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aquí se abrirá el formulario de gestión de usuarios", "Usuarios",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void menuCambiarUsuario_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.None)
            {
                this.DialogResult = DialogResult.Abort;
            }
        }
    }
}