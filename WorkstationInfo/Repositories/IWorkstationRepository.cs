using System;
using System.Threading.Tasks;
using Infrastructure.Database;
using Shared.Entities;

namespace WorkstationInfo.Repositories;

public interface IWorkstationRepository : IGenericRepository<Workstation>
{
    Task<Workstation?> GetByIdWithDetailsAsync(int id);
}