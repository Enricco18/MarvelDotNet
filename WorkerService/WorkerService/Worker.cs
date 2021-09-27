using MarvelDotNet.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                DateTimeOffset now = DateTimeOffset.Now;
                _logger.LogInformation("Worker running at: {time}", now);

                DatabaseManager manager = new DatabaseManager();

                string table = GetTableString(manager.findAll());
                Console.WriteLine(filepath);
                WriteToFile(table,filepath);
                await Task.Delay(10000, stoppingToken);
            }
        }

        protected String GetTableString(DataTable table)
        {
            IDictionary<string, int> maxLengths = new Dictionary<string, int>();

            //if the length of the column name is greater than the current max length
            //update the max length
            foreach (DataRow row in table.Rows)
            {
                foreach(DataColumn c in table.Columns)
                {
                    int value;
                    if (!maxLengths.TryGetValue(c.ColumnName, out value))
                    {
                        maxLengths.Add(c.ColumnName, row[c.ColumnName].ToString().Length+4);
                    }
                    else
                    {
                        maxLengths[c.ColumnName] = value < row[c.ColumnName].ToString().Length + 4 ? row[c.ColumnName].ToString().Length + 4 : value;
                    }
         
                }

            }

            var sb = new StringBuilder();

            //create the top row
            foreach (DataColumn c in table.Columns)
            {
                sb.AppendFormat("+{0}+", new String('-', maxLengths[c.ColumnName]));
            }
            sb.AppendLine();

            //create the column names
            foreach (DataColumn c in table.Columns)
            {
                sb.AppendFormat("|{0}|", c.ColumnName.PadLeft(maxLengths[c.ColumnName]));
            }
            sb.AppendLine();

            //create the bottom of the column headers same as the top
            foreach (DataColumn c in table.Columns)
            {
                sb.AppendFormat("+{0}+", new String('-', maxLengths[c.ColumnName]));
            }
            sb.AppendLine();

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn c in table.Columns)
                {
                    sb.AppendFormat("|{0}|", row[c.ColumnName].ToString().PadLeft(maxLengths[c.ColumnName]));
                }
                sb.AppendLine();
            }
            foreach (DataColumn c in table.Columns)
            {
                sb.AppendFormat("+{0}+", new String('-', maxLengths[c.ColumnName]));
            }
            sb.AppendLine();


            return sb.ToString();
        }

        protected void WriteToFile(string content, string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(content);
            }
        }
    }
}
