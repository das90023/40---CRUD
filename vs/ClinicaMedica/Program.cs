using ClinicaMedica.Views.Forms;
using System;
using System.Windows.Forms;

namespace ClinicaMedica
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Configurar manejo de excepciones no controladas
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
            {
                MessageBox.Show($"Error no controlado: {e.Exception.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            while (true)
            {
                using (var loginForm = new LoginForm())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK && loginForm.UsuarioLogueado != null)
                    {
                        using (var mainForm = new MainForm(loginForm.UsuarioLogueado))
                        {
                            Application.Run(mainForm);

                            // Si el mainForm se cierra, volver al login
                            if (mainForm.DialogResult == DialogResult.Cancel)
                                continue;
                            else
                                break;
                        }
                    }
                    else
                    {
                        break; // Salir de la aplicación
                    }
                }
            }
        }
    }
}