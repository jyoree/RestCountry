using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCountries.Models
{
    public class User
    {
        public int Id { get; set; }

        public List<string> Languages { get; set; }
    }
}
