using EF.CodeFirst.Common.Domain;

namespace EF.CodeFirst.Common.Repository
{
    public interface IAttachableRepository
    {
        IEntity Attach(IEntity entity);
    }
}