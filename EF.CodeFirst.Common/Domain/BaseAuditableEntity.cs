using System;
using System.ComponentModel.DataAnnotations;

namespace EF.CodeFirst.Common.Domain
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
