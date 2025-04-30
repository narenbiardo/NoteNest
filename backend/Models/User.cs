using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace EnsolversChallenge.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Username { get; set; } = default!;

        [Required, StringLength(256)]
        public string PasswordHash { get; set; } = default!;

        //One to many relationship (one user can have multiple notes)
        public ICollection<Note> Notes { get; set; } = [];
        public ICollection<Category> Categories { get; set; } = [];

        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            var hashed = Convert.ToBase64String(
                Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        public static bool VerifyPassword(string hashWithSalt, string password)
        {
            var parts = hashWithSalt.Split('.');
            var salt = Convert.FromBase64String(parts[0]);
            var stored = parts[1];
            var hashed = Convert.ToBase64String(
                Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: Microsoft.AspNetCore.Cryptography.KeyDerivation.KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
            return hashed == stored;
        }
    }
}
