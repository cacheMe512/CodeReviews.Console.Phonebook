using cacheMe512.Phonebook.Models;
using Microsoft.EntityFrameworkCore;

namespace cacheMe512.Phonebook.Controllers;

internal class ContactController
{
    internal static List<Contact> GetContacts()
    {
        using var db = new PhonebookContext();

        var contacts = db.Contacts
            .Include(x => x.Category)
            .ToList();

        return contacts;
    }

    internal static Contact GetContactById(int id)
    {
        using var db = new PhonebookContext();

        var contact = db.Contacts
            .Include(x => x.Category)
            .SingleOrDefault(x => x.ContactId == id);

        return contact;
    }

    internal static void AddContact(Contact contact)
    {
        using var db = new PhonebookContext();

        db.Add(contact);

        db.SaveChanges();
    }

    internal static void UpdateContact(Contact contact)
    {
        using var db = new PhonebookContext();

        db.Update(contact);

        db.SaveChanges();
    }

    internal static bool DeleteContact(Contact contact)
    {
        using var db = new PhonebookContext();

        db.Remove(contact);

        return db.SaveChanges() > 0;
    }
}
