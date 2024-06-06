using System.ComponentModel.DataAnnotations;

public class AuditLog
{
    [Key]
    public Guid Id { get; set; }
    public string? TableName { get; set; }
    public string? ActionType { get; set; }
    public string KeyValues { get; set; } = "";
    public List<string> OldValues { get; set; } = [];
    public List<string> NewValues { get; set; } = [];
    public List<string> ChangedColumns { get; set; } = [];
    public DateTime DateChanged { get; set; }

    public AuditLog()
    {
        Id = Guid.NewGuid();
        DateChanged = DateTime.Now;
    }
}