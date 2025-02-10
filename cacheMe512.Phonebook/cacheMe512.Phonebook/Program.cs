using Spectre.Console;

var isAppRunning = true;
while (isAppRunning)
{
    var option = AnsiConsole.Prompt(
        new SelectionPrompt<MenuOptions>()
            .Title("What would you like to do?")
            .AddChoices(
                MenuOptions.ViewAllContacts,
                MenuOptions.ViewContact,
                MenuOptions.AddContact,
                MenuOptions.UpdateContact,
                MenuOptions.DeleteContact,
                MenuOptions.Quit));

    switch (option)
    {
        case MenuOptions.ViewAllContacts:
            PhonebookService.GetContacts();
            break;
        case MenuOptions.ViewContact:
            PhonebookService.GetContact();
            break;
        case MenuOptions.AddContact:
            PhonebookService.InsertContact();
            break;
        case MenuOptions.UpdateContact:
            PhonebookService.UpdateContact();
            break;
        case MenuOptions.DeleteContact:
            PhonebookService.DeleteContact();
            break;
        case MenuOptions.Quit:
            isAppRunning = false;
            break;
    }
}

enum MenuOptions
{
    ViewAllContacts,
    ViewContact,
    AddContact,
    UpdateContact,
    DeleteContact,
    Quit
}
