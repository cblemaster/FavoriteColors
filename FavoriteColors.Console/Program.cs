
using FavoriteColors.Console.Extensions;
using FavoriteColors.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Consts = FavoriteColors.Console.UiConstants;

#region Configure services
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IDataService, JsonDataService>();
using IHost host = builder.Build();
#endregion Configure services

IDataService dataService = host.Services.GetService<IDataService>();

ShowIntro();
LoadData(dataService);
RunMenu();
SaveData(dataService);
ShowOutro();

static void ShowIntro()
{
    Consts.Stars.WriteToTerminal(0, 2);
    Consts.INTRO_TEXT.WriteToTerminal(0, 2);
    Consts.Stars.WriteToTerminal(0, 2);
}
static void ShowOutro() => Consts.APP_CLOSING_MESSAGE.WriteToTerminal(0, 2);
static void LoadData(IDataService dataService)
{
    Consts.DATA_LOADING_MESSAGE.WriteToTerminal(0, 2);
    if (dataService is null || !dataService.TryLoadData())
    {
        Consts.DATA_LOAD_FAILURE_ERROR.WriteToTerminal(0, 2);
        return;
    }
}
static void SaveData(IDataService dataService)
{
    Consts.EXIT_SAVING_DATA_MESSAGE.WriteToTerminal(0, 2);
    if (dataService is null || !dataService.TrySaveData())
    {
        Consts.EXIT_DATA_SAVE_ERROR.WriteToTerminal(0, 2);
    }
    else
    {
        Consts.EXIT_DATA_SAVE_SUCCESS_CONFIRMATION.WriteToTerminal(0, 2);
    }
    return;
}
static void RunMenu()
{
    ConsoleKey menuSelection = GetMenuSelection();
    GoToMenuSelection(menuSelection);

    static ConsoleKey GetMenuSelection()
    {
        Consts.MENU_TEXT.WriteToTerminal(0, 2);
        ConsoleKey[] validKeys = [ConsoleKey.D1, ConsoleKey.NumPad1, ConsoleKey.D2, ConsoleKey.NumPad2, ConsoleKey.D3, ConsoleKey.NumPad3, ConsoleKey.Escape];
        ConsoleKey menuSelection = ConsoleKey.None;
        string invalidMenuSelection = string.Empty;
        while (!validKeys.Contains(menuSelection))
        {
            if (!string.IsNullOrWhiteSpace(invalidMenuSelection))
            {
                invalidMenuSelection.WriteToTerminal(1, 2);
            }
            invalidMenuSelection = Consts.MENU_OPTION_VALIDATION_ERROR;
            Consts.MENU_OPTION_PROMPT.WriteToTerminal(0, 1);
            menuSelection = Console.ReadKey().Key;
        }
        return menuSelection;
    }
    static void GoToMenuSelection(ConsoleKey menuSelection)
    {
        switch (menuSelection)
        {
            case ConsoleKey.Escape:
                GoToExitProgram();
                break;
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
                GoToAddFriend();
                break;
            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
                GoToAllFriends();
                break;
            case ConsoleKey.NumPad3:
            case ConsoleKey.D3:
                GoToSearchForFriends();
                break;
            default:
                break;
        }
        static void GoToAddFriend() { }
        static void GoToAllFriends() { }
        static void GoToSearchForFriends() { }
        static void GoToExitProgram()
        {
            ConsoleKey[] validKeys = [ConsoleKey.Y, ConsoleKey.N];
            ConsoleKey confirmExitKey = ConsoleKey.None;
            string invalidConfirmExit = string.Empty;
            while (!validKeys.Contains(confirmExitKey))
            {
                if (!string.IsNullOrEmpty(invalidConfirmExit))
                {
                    invalidConfirmExit.WriteToTerminal(1, 2);
                }
                invalidConfirmExit = Consts.EXIT_CONFIRM_EXIT_VALIDATION_ERROR;
                Consts.EXIT_CONFIRM_EXIT_PROMPT.WriteToTerminal(0, 1);
                confirmExitKey = Console.ReadKey().Key;
            }
            if (confirmExitKey.Equals(ConsoleKey.N))
            {
                RunMenu();
            }
            else
            {
                return;
            }
        }
    }
}
