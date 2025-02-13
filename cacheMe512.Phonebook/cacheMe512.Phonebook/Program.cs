using cacheMe512.Phonebook;

using (var context = new PhonebookContext())
{
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}


UserInterface.MainMenu();
