using System.ComponentModel.DataAnnotations;

namespace ProjektAPBD.Models;

public class SoftwareCategory
{
    [Key]
    public int SoftwareCategoryId { get; set; }
    [MaxLength(20)]
    public string Type { get; set; }
    //public int CostForYear { get; set; }

    public ICollection<Software> Softwares { get; set; }
}