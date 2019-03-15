using System;
using System.Security.Cryptography;
using System.Text;

namespace TaskManager.AuthService.Data
{
    public class HashCreator : IHashCreator
    {
        public string Create(string username, string password, string passwordSalt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(String.Concat(username, password, passwordSalt)));

                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}