using ClinicaMedica.Views.Forms;
using System.Windows.Forms;
using System;

private void menuCitas_Click(object sender, EventArgs e)
{
    AbrirFormularioCitas();
}

private void AbrirFormularioCitas()
{
    // Cerrar el panel de bienvenida si está visible
    panelMain.Visible = false;

    // Verificar si ya está abierto el formulario
    foreach (Form form in this.MdiChildren)
    {
        if (form is CitasForm)
        {
            form.BringToFront();
            form.WindowState = FormWindowState.Maximized;
            return;
        }
    }

    // Crear nuevo formulario
    CitasForm citasForm = new CitasForm();
    citasForm.MdiParent = this;
    citasForm.FormClosed += (s, args) => {
        // Al cerrar el formulario, mostrar panel de bienvenida
        panelMain.Visible = true;
    };

    citasForm.WindowState = FormWindowState.Maximized;
    citasForm.Show();
    citasForm.BringToFront();
}