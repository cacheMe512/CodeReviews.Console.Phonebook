using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace cacheMe512.Phonebook.Models;

[Index(nameof(Name), IsUnique = true)]
internal class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; }

    public List<Contact> Contacts { get; set; }
}
