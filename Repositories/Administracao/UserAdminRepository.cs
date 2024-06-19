using HefestusApi.Models.Administracao;
using HefestusApi.Models.Data;
using HefestusApi.Repositories.Administracao.Interfaces;
using Microsoft.EntityFrameworkCore;

public class UserAdminRepository : IUserAdminRepository
{
    private readonly DataContext _context;

    public UserAdminRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<UserAdmin> GetUserAdmin()
    {
        return await _context.UserAdmin.FirstOrDefaultAsync();
    }

    public async Task<bool> AddUserAdminAsync(UserAdmin userAdmin)
    {
        await _context.UserAdmin.AddAsync(userAdmin);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateUserAdminAsync(UserAdmin userAdmin)
    {
        _context.Entry(userAdmin).State = EntityState.Modified;
        return await _context.SaveChangesAsync() > 0;
    }
}
