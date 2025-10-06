using ClinicaMedica.Models.Clases;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClinicaMedica.Views.Forms
{
    public partial class EspecialidadesForm : Form
    {
        private List<Especialidad> especialidades = new List<Especialidad>();

        public EspecialidadesForm()
        {
            InitializeComponent();
            CargarDatosEjemplo();
        }

        private void CargarDatosEjemplo()
        {
            // Especialidades de ejemplo
            especialidades.Add(new Especialidad { Id = 1, Nombre = "Cardiología", Descripcion = "Especialidad en corazón y sistema cardiovascular" });
            especialidades.Add(new Especialidad { Id = 2, Nombre = "Pediatría", Descripcion = "Medicina para niños y adolescentes" });
            especialidades.Add(new Especialidad { Id = 3, Nombre = "Dermatología", Descripcion = "Especialidad en piel, cabello y uñas" });
            especialidades.Add(new Especialidad { Id = 4, Nombre = "Neurología", Descripcion = "Especialidad en sistema nervioso" });

            ActualizarGrid();
        }

        private void ActualizarGrid()
        {
            dataGridViewEspecialidades.DataSource = null;
            dataGridViewEspecialidades.DataSource = especialidades;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                GuardarEspecialidad();
                LimpiarFormulario();
                ActualizarGrid();
                MessageBox.Show("Especialidad guardada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void GuardarEspecialidad()
        {
            var especialidad = new Especialidad
            {
                Id = especialidades.Count + 1,
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text
            };

            especialidades.Add(especialidad);
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
        }

        private void dataGridViewEspecialidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var especialidad = especialidades[e.RowIndex];
                CargarDatosFormulario(especialidad);
            }
        }

        private void CargarDatosFormulario(Especialidad especialidad)
        {
            txtNombre.Text = especialidad.Nombre;
            txtDescripcion.Text = especialidad.Descripcion;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewEspecialidades.CurrentRow != null && dataGridViewEspecialidades.CurrentRow.Index >= 0)
            {
                int index = dataGridViewEspecialidades.CurrentRow.Index;
                especialidades.RemoveAt(index);
                ActualizarGrid();
                LimpiarFormulario();
                MessageBox.Show("Especialidad eliminada", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}