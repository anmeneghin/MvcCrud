using System.ComponentModel.DataAnnotations;

namespace MvcCrud.Models
{
    // Modelo que representa um produto na aplicação.
    // Cada instância dessa classe corresponde a uma linha na tabela "Products" (via EF Core).
    public class Product
    {
        // Chave primária (Id) — usada pelo EF Core para identificar registros.
        public int Id { get; set; }

        // Nome do produto — obrigatório e com tamanho máximo.
        [Required, StringLength(100)]
        public string? Name { get; set; }

        // Preço do produto.
        // DataType.Currency apenas ajuda na formatação em views; o tipo é decimal para precisão.
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        // Descrição opcional com limite de caracteres.
        [StringLength(500)]
        public string? Description { get; set; }
    }
}