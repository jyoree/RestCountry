using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCountries.Models
{
    public class RestCountry
    {
        public string name { get; set; }
        public List<string> callingCodes { get; set; }
        public string region { get; set; }
        public List<RestLanguage> languages { get; set; }
        public Uri flag { get; set; }
    }
}
