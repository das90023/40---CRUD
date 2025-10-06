using ClinicaMedica.Models.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaMedica.Views.Forms
{
    public partial class PacientesForm : Form
    {
        private List<Paciente> pacientes = new List<Paciente>();

        public PacientesForm()
        {
            InitializeComponent();
            CargarPacientesEjemplo();
        }

        private void CargarPacientesEjemplo()
        {
            // Datos de ejemplo
            pacientes.Add(new Paciente
            {
                Id = 1,
                Nombre = "María",
                Apellido = "García",
                FechaNacimiento = new DateTime(1985, 3, 15),
                Telefono = "555-1111",
                Email = "maria.garcia@email.com",
                Direccion = "Calle Principal 123",
                Activo = true
            });

            pacientes.Add(new Paciente
            {
                Id = 2,
                Nombre = "Juan",
                Apellido = "Martínez",
                FechaNacimiento = new DateTime(1990, 7, 22),
                Telefono = "555-2222",
                Email = "juan.martinez@email.com",
                Direccion = "Avenida Central 456",
                Activo = true
            });

            ActualizarGrid();
        }

        private void ActualizarGrid()
        {
            dataGridViewPacientes.DataSource = null;
            dataGridViewPacientes.DataSource = pacientes;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                GuardarPaciente();
                LimpiarFormulario();
                ActualizarGrid();
                MessageBox.Show("Paciente guardado correctamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtApellido.Text))
            {
                MessageBox.Show("Nombre y Apellido son obligatorios", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void GuardarPaciente()
        {
            var paciente = new Paciente
            {
                Id = pacientes.Count + 1,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                FechaNacimiento = dtpFechaNacimiento.Value,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                Direccion = txtDireccion.Text,
                Activo = true
            };

            pacientes.Add(paciente);
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            dtpFechaNacimiento.Value = DateTime.Now.AddYears(-30);
            txtTelefono.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
        }

        private void dataGridViewPacientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var paciente = pacientes[e.RowIndex];
                CargarDatosFormulario(paciente);
            }
        }

        private void CargarDatosFormulario(Paciente paciente)
        {
            txtNombre.Text = paciente.Nombre;
            txtApellido.Text = paciente.Apellido;
            dtpFechaNacimiento.Value = paciente.FechaNacimiento;
            txtTelefono.Text = paciente.Telefono;
            txtEmail.Text = paciente.Email;
            txtDireccion.Text = paciente.Direccion;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPacientes.CurrentRow != null)
            {
                int index = dataGridViewPacientes.CurrentRow.Index;
                pacientes.RemoveAt(index);
                ActualizarGrid();
                LimpiarFormulario();
                MessageBox.Show("Paciente eliminado", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
