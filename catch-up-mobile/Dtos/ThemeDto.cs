using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace catch_up_mobile.Dtos
{
    public class ThemeDto
    {
        [PrimaryKey, AutoIncrement]
        public int Id {  get; set; }
        public string Name { get; set; }
    }
}
