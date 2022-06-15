using System.ComponentModel.DataAnnotations;

namespace dotnet.Models;

public class Scheduler
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartOfTask { get; set; }

    [DataType(DataType.Date)]
    public DateTime EndOfTask { get; set; }
}