using System;

namespace Recipes.Services
{
    public class Users
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhotoPath { get; set; }
        public string Password { get; set; }
    }
}