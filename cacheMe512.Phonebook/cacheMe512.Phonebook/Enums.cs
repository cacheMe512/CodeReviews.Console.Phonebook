namespace cacheMe512.Phonebook
{
    internal class Enums
    {
        internal enum MainMenuOptions
        {
            ManageCategories,
            ManageContacts,
            Quit
        }

        internal enum CategoryMenu
        {
            AddCategory,
            DeleteCategory,
            UpdateCategory,
            ViewAllCategories,
            ViewCategory,
            GoBack
        }

        internal enum ContactMenu
        {
            AddContact,
            DeleteContact,
            UpdateContact,
            ViewContact,
            ViewAllContacts,
            GoBack
        }

        internal enum ContactAttribute
        {
            Name,
            PhoneNumber,
            Email,
            Category
        }
    }
}
