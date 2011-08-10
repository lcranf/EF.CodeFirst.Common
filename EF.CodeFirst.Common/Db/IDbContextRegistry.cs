using System.Data.Entity;

namespace EF.CodeFirst.Common.Db
{
    public interface IDbContextRegistry
    {
        DbContext CurrentContext { get; }
    }
}