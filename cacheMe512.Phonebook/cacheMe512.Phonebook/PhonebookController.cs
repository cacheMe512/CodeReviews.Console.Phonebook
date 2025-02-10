﻿namespace cacheMe512.Phonebook;

internal class PhonebookController
{
    internal static List<Contact> GetContacts()
    {
        using var db = new PhonebookContext();

        var contacts = db.Contacts.ToList();

        return contacts;
    }

    internal static Contact GetContactById(int id)
    {
        using var db = new PhonebookContext();

        var contact = db.Contacts.SingleOrDefault(x => x.Id == id);

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

    internal static void DeleteContact(Contact contact)
    {
        using var db = new PhonebookContext();

        db.Remove(contact);

        db.SaveChanges();
    }
}
