using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Identification { get; set; }  

        
        public List<Test> Tests { get; set; } = new List<Test>();

        
        public List<Result> Results { get; set; } = new List<Result>();
    }
}

