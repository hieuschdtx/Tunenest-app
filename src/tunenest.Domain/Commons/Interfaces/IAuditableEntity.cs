namespace tunenest.Domain.Commons.Interfaces
{
    public interface IAuditableEntity
    {
        DateTime created_at { get; set; }
        DateTime? modified_at { get; set; }
    }
}
