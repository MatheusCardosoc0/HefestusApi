using HefestusApi.Models.Administracao;
using HefestusApi.Models.Data;
using HefestusApi.Models.Pessoal;
using HefestusApi.Repositories.Administracao.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HefestusApi.Repositories.Administracao
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(x => x.Person)
                .ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(c => c.Person)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User?> GetUserByNameAsync(string name)
        {
            return await _context.Users
                .Include(c => c.Person)
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<IEnumerable<User>> SearchUserByNameAsync(string searchTerm)
        {
            return await _context.Users
                .Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%"))
                .ToListAsync();
        }

        public async Task<bool> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Person?> GetPersonAsync(int id)
        {
            return await _context.Person.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
