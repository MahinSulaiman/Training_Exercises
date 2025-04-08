using System.Globalization;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;



namespace ClaysysLearningPortal.Error
{
    public class ErrorLogger
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Inject IHttpContextAccessor via DI
        public ErrorLogger(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void WriteError(string errorMessage)
        {
            try
            {
                // Set the path to your log file directory
                string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Error");
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }

                // Set the path to your log file with today's date
                string path = Path.Combine(logDirectory, DateTime.Today.ToString("dd-MM-yy") + ".txt");

                // Create the file if it doesn't exist
                if (!File.Exists(path))
                {
                    using (File.Create(path)) { }
                }

                // Append error message to the file
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine("\r\nLog Entry : ");
                    writer.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));

                    // Get the URL of the current request
                    string requestUrl = _httpContextAccessor.HttpContext?.Request?.Path.ToString();
                    string errorEntry = $"Error in: {requestUrl}. Error Message: {errorMessage}";

                    writer.WriteLine(errorEntry);
                    writer.WriteLine("__________________________");
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                // In case of an error in logging itself, attempt to log that error as well
                WriteError(ex.Message);
            }
        }
    }
}
