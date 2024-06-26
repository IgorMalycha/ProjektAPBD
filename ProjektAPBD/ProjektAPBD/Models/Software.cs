using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPBD.Models;


public class Software
{
    //do dopracowania i wyjaśnienia
    [Key] 
    public int SoftwareId { get; set; }
    [MaxLength(30)]
    public string Name { get; set; }
    [MaxLength(300)]
    public string Description { get; set; }
    public string Version { get; set; }
    public string SoftwareCategoryId { get; set; }
    public bool IsSubscription { get; set; }
    public bool IsBoughtInOneTransaction { get; set; }

    [ForeignKey(nameof(SoftwareCategoryId))]
    public SoftwareCategory SoftwareCategory { get; set; }

    public ICollection<Agreement> Agreements { get; set; } = new HashSet<Agreement>();
    
}