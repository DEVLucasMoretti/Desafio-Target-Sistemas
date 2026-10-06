using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Logger
    {
        private readonly string logPath;

        public Logger()
        {
            
        }

        public Logger(string logPath)
        {
            this.logPath = logPath;
        }

        public async Task Log(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Data: {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}\n");
            sb.Append($"Erro: {ex.Message}\n");
            sb.Append($"StackTrace: {ex.StackTrace}");
            sb.Append("\n____________________________________________________________________________________________________________________________________________________________________________________________________________________________________");
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                await sw.WriteLineAsync(sb.ToString());
            }
        }

    }
}
