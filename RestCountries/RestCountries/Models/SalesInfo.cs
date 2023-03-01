using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCountries.Models
{
    public class SalesInfo
    {
        public string PhoneNumber { get; set; }
        public int CallingCode { get; set; }
        public string CountryCode { get; set; }
        public User User { get; set; }
        public CountryDetails Details { get; set; }
    }
}
