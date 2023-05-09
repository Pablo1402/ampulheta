using Ampulheta.Domain.Entities;

namespace Ampulheta.Domain.Intefaces.Repositories
{
    public interface ITimeRepository
    {
        Task SaveAsync(Time time);
        Task UpdateAsync(Time time);
        Task<List<Time>> GetByProjetc(int projetcId);
        Task<List<Time>> GetByUser(int userId);
        Task<Time> GetById(int id);
    }
}
