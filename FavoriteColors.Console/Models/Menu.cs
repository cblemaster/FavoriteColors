namespace FavoriteColors.Console.Models;

internal record Menu(
    string Title,
    IEnumerable<MenuOption> MenuOptions,
    IEnumerable<Menu> SubMenus,
    string Prompt,
    string ValidationError,
    ConsoleKey ExitKey,
    string ExitKeyString,
    ConsoleKey SelectedKey)
{
    internal ConsoleKey SelectedKey { get; set; } = SelectedKey;

    internal void GetKey() => SelectedKey = System.Console.ReadKey().Key;
    internal IEnumerable<ConsoleKey> ValidKeys()
    {
        List<ConsoleKey> valid = MenuOptions.Select(m => m.SelectKeys).SelectMany(m => m).ToList();
        valid.Add(ExitKey);
        return valid;
    }
    internal bool SelectedKeyIsValid() => ValidKeys().Contains(SelectedKey);
    internal bool SelectedKeyIsExit() => SelectedKeyIsValid() && SelectedKey.Equals(ExitKey);
    internal void Run()
    {
        System.Console.WriteLine('\n' + Title);

        SelectedKey = ConsoleKey.None;
        string validationError = string.Empty;

        while (!SelectedKeyIsValid() && !SelectedKeyIsExit())
        {
            if (!string.IsNullOrEmpty(validationError))
            {
                System.Console.WriteLine('\n' + validationError);
                break;
            }
            validationError = ValidationError;

            System.Console.WriteLine('\n' + Prompt);
            foreach (MenuOption option in MenuOptions.OrderBy(o => o.SortOrder))
            {
                System.Console.WriteLine(option.ToString());
            }
            System.Console.WriteLine($"{ExitKeyString}");

            GetKey();
        }
        if (SelectedKeyIsExit())
        {
            return;
        }

        MenuOptions.SingleOrDefault(o => o.SelectKeys.Contains(SelectedKey))?.MenuAction();
        Run();
    }
}
