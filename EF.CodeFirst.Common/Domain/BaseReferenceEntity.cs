namespace EF.CodeFirst.Common.Domain
{
    public abstract class BaseReferenceEntity : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
