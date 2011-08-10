using EF.CodeFirst.Common.Domain;

namespace EF.CodeFirst.Common.Repository
{
    public interface IReadOnlyRepository
    {
        IEntity GetById(int entityId);
    }
}