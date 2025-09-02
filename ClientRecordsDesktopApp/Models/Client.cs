using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientRecordsDesktopApp.Models {
    public class Client {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Adress { get; set; }
    }
}
