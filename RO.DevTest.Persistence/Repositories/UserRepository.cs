using BE.Application.Contracts.Persistance.Repositories;
using BE.Domain.Entities;

namespace RO.DevTest.Persistence.Repositories;

public class UserRepository(DefaultContext context): BaseRepository<User>(context), IUserRepository { }
