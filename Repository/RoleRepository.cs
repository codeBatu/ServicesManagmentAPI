using Model;
using Model.Results;
using Repository.DbContexts;
using Repository.RepositoryInterface;

namespace Repository;

public class RoleRepository : IRoleRepository
{
    private readonly SmartPulseServiceManagerContext? _context;

    public RoleRepository(SmartPulseServiceManagerContext? context)
    {
        _context = context;
    }

    public async Task<IResult> Create(Role entity)
    {
        _context.Roles.Add(entity);
        var result = await _context.SaveChangesAsync();

        if (result < 1)
        {
            return new ErrorResult("Rol oluşturulamadı!.");
        }
        return new SuccessResult("Rol başarıyla oluşturuldu.");
    }

    public IDataResult<Role> Get(int id)
    {
        throw new NotImplementedException();
    }

    public IDataResult<List<Role>> GetAll()
    {
        throw new NotImplementedException();
    }
}
