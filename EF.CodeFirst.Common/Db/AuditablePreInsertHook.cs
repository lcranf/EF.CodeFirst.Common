using System;
using System.Web;
using EF.CodeFirst.Common.Domain;
using EFHooks;
using Simple.Extensions;

namespace EF.CodeFirst.Common.Db
{
    public class AuditablePreInsertHook : PreInsertHook<BaseAuditableEntity>
    {
        /// <summary>
        /// The logic to perform per entity before the registered action gets performed.
        ///             This gets run once per entity that has been changed.
        /// </summary>
        /// <param name="entity">The entity that is processed by Entity Framework.</param><param name="metadata">Metadata about the entity in the context of this hook - such as state.</param>
        public override void Hook(BaseAuditableEntity entity, HookEntityMetadata metadata)
        {
            var userName = HttpContext.Current
                                      .IfNotNull(c => c.User)
                                      .IfNotNull(u => u.Identity)
                                      .IfNotNull(i => i.Name, "Anonymous User");

            entity.CreatedBy = userName;
            entity.CreatedOn = DateTime.Now;

        }
    }
}
