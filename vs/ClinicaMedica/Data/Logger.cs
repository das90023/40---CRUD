using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaMedica.Data
{
    internal class Logger
    {
        private static string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        static Logger()
        {
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
        }

        public static void LogActivity(string message)
        {
            try
            {
                string logFile = Path.Combine(logPath, $"activity_{DateTime.Today:yyyyMMdd}.log");
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";
                File.AppendAllText(logFile, logMessage);
            }
            catch (Exception ex)
            {
                // Si falla el logging, al menos mostrar en consola
                Console.WriteLine($"Error en logging: {ex.Message}");
            }
        }

        public static void LogError(Exception ex)
        {
            try
            {
                string logFile = Path.Combine(logPath, $"error_{DateTime.Today:yyyyMMdd}.log");
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Error: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
                File.AppendAllText(logFile, logMessage);
            }
            catch
            {
                Console.WriteLine($"Error crítico en logging: {ex.Message}");
            }
        }
    }
}
