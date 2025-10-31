using Microsoft.EntityFrameworkCore;
using ProjectManager.API.Data;
using ProjectManager.API.Models;
using System.Security.Cryptography;
using System.Text;

namespace ProjectManager.API.Services {
  public class AuthService {
    private readonly AppDbContext _db;
    public AuthService(AppDbContext db) => _db = db;

    public async Task<User?> Register(string username, string password) {
      if (await _db.Users.AnyAsync(u => u.Username == username)) return null;
      var user = new User { Username = username, PasswordHash = Hash(password) };
      _db.Users.Add(user);
      await _db.SaveChangesAsync();
      return user;
    }

    public async Task<User?> Validate(string username, string password) {
      var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
      if (user == null) return null;
      return Verify(password, user.PasswordHash) ? user : null;
    }

    private static string Hash(string input) {
      using var sha = SHA256.Create();
      var b = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
      return Convert.ToBase64String(b);
    }
    private static bool Verify(string input, string hash) => Hash(input) == hash;
  }
}
