using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjektAPBD.Models;


public class Klient
{
    [Key]
    public int Id { get; set; }
    public string Adres { get; set; }
    public string Email { get; set; }
    public int NumerTelefonu { get; set; }
    public int IdKlient { get; set; }
    public int IdOsobaFizyczna { get; set; }
    
    [ForeignKey(nameof(IdKlient))]
    public Firma Firma { get; set; }

    [ForeignKey(nameof(IdOsobaFizyczna))]
    public OsobaFizyczna OsobaFizyczna { get; set; }
    
    
    
}