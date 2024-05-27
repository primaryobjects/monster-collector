using System.ComponentModel.DataAnnotations;

public class AuditLog
{
    [Key]
    public Guid Id { get; set; }
    public string? TableName { get; set; }
    public string? ActionType { get; set; }
    public string KeyValues { get; set; }
    public Dictionary<string, object?> OldValues { get; set; } = new Dictionary<string, object?>();
    public Dictionary<string, object?> NewValues { get; set; } = new Dictionary<string, object?>();
    public List<string> ChangedColumns { get; set; } = new List<string>();
    public DateTime DateChanged { get; set; }

    public AuditLog()
    {
        Id = Guid.NewGuid();
        DateChanged = DateTime.Now;
    }
}