using RestCountries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestCountries.Interfaces
{
    public interface IUserService
    {
        public User GetUser(string lang);
        public User GetDefaultUser();
    }
}
