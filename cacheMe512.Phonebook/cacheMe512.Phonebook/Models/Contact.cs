using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cacheMe512.Phonebook.Models;

[Index(nameof(Name), IsUnique = true)]
internal class Contact
{
    [Key]
    public int ContactId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Email { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; }
}
