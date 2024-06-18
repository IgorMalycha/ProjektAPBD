using System.ComponentModel.DataAnnotations;

namespace ProjektAPBD.Models;

public class OsobaFizyczna
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    [MaxLength(11)]
    public int Pesel { get; set; }
}