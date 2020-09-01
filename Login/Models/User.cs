using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Login.Model
{
    public class User
    {
        public User()
        {
            RegisterDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Auth_token { get; set; }

        public void GenerateAuthToken()
        {
            string toHash = Username + Password + Stopwatch.GetTimestamp().ToString();
            Auth_token = toHash.GetHashCode().ToString();
        }
    }
}
