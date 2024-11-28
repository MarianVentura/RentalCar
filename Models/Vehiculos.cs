using System.ComponentModel.DataAnnotations;

namespace RentalCar.Models;

public class Vehiculos
{
    [Key]
    public int VehiculosId { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Marca { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Modelo { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public decimal PrecioPorDia { get; set; }

    [Required(ErrorMessage = "Este campo es obligatorio.")]
    public string? Categoria { get; set; }
    public bool Disponible { get; set; } = true;
}
