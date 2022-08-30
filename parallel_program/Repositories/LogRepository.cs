using parallel_program.Interfaces;
using parallel_program.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parallel_program.Repositories
{
    internal class LogRepository : ILog
    {
        private const string path = "database/log.txt";
        private readonly FileStream _fileStream;

        public LogRepository(FileStream fileStream)
        {
            CreateFolderFile(path);
            this._fileStream = fileStream;
        }

        private static string PrepareLine(User user)
        {
            return $"O usuário {user.Name} - {user.JobTitle} acessou o banco de dados {DateTimeOffset.Now}.";
        }

        public static void CreateFolderFile(string path)
        {
            string folder = path.Split("/")[0];

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public void RegisterAccess(User user)
        {
            string line = PrepareLine(user);
            byte[] info = new UTF8Encoding(true).GetBytes(line);

            _fileStream.Write(info);
        }
    }
}
