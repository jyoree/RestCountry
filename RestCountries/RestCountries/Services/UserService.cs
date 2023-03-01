using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestCountries.Interfaces;
using RestCountries.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RestCountries.Services
{
    public class UserService : IHostedService

    {

        private ILogger<UserService> _logger;
        private Dictionary<string, List<User>> _users = new Dictionary<string, List<User>>();
        private User _defaultUser = null;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {

            var users = LoadJson();
            if (users.Count > 0)
            {
                _defaultUser = users[0];
                foreach (var user in users)
                {
                    if (user.Languages.Count() > _defaultUser.Languages.Count())
                    {
                        _defaultUser = user;
                    }

                    foreach (var lang in user.Languages)
                    {
                        if (_users.TryGetValue(lang, out var value))
                        {
                            value.Add(user);
                        }
                        else
                        {
                            List<User> lstByLang = new List<User>();
                            lstByLang.Add(user);
                            _users.Add(lang, lstByLang);
                        }
                    }

                }
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public User GetUser(string language)
        {
            // JSON contains lower case string
            string key = language.ToLowerInvariant();

            if (_users.ContainsKey(key))
            {
                return _users[key].FirstOrDefault();
            }

            return _defaultUser;
        }

        public User GetDefaultUser()
        {
            return _defaultUser;
        }

        private List<User> LoadJson()
        {
            List<User> users = new List<User>();
            try
            {
                using (StreamReader r = new StreamReader("Users.json"))
                {
                    string json = r.ReadToEnd();
                    var userList = JsonSerializer.Deserialize<UserList>(json);
                    users = userList.Users;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError($"Error loading JSON: {ex.Message}");
            }
            return users;

        }
    }
}
