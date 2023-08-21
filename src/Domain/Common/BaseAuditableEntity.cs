namespace SiMinor7.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset? Updated { get; set; }

    public string? UpdatedBy { get; set; }
}