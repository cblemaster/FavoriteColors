
//using ConsoleMenus.UI;

//Menu mainMenu = MenuFactory.GetMainMenu();
//mainMenu.Run();

using FavoriteColors.Console.Extensions;
using FavoriteColors.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Consts = FavoriteColors.Console.UiConstants;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<IDataService, JsonDataService>();
using IHost host = builder.Build();
IDataService dataService = host.Services.GetService<IDataService>();

ShowIntro();
LoadData();
RunMenu();
SaveData();
ShowOutro();

void ShowIntro()
{
    Consts.Stars.WriteToTerminal(0, 2);
    Consts.INTRO_TEXT.WriteToTerminal(0, 2);
    Consts.Stars.WriteToTerminal(0, 2);
}
void ShowOutro() => Consts.APP_CLOSING_MESSAGE.WriteToTerminal(0, 2);
void LoadData()
{
    Consts.DATA_LOADING_MESSAGE.WriteToTerminal(0, 2);
    
    if (dataService is null || !dataService.TryLoadData())
    {
        Consts.DATA_LOAD_FAILURE_ERROR.WriteToTerminal(0, 2);
        return;
    }
}
void SaveData()
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
}
void RunMenu()
{
    ConsoleKey menuSelection = GetMenuSelection();
    GoToMenuSelection(menuSelection);

    ConsoleKey GetMenuSelection()
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
    void GoToMenuSelection(ConsoleKey menuSelection)
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
        void GoToAddFriend()
        {
            Consts.ADD_FRIEND_HEADER.WriteToTerminal(0, 2);
            string name = string.Empty;
            string invalidName = string.Empty;
            while (!name.IsValidName())
            {
                if (!string.IsNullOrWhiteSpace(invalidName))
                {
                    invalidName.WriteToTerminal(1, 2);
                }
                invalidName = Consts.ADD_FRIEND_NAME_VALIDATION_ERROR;
                Consts.ADD_FRIEND_NAME_PROMPT.WriteToTerminal(0, 1);
                name = Console.ReadLine().Trim();
            }

            if (name.Equals("x", StringComparison.CurrentCultureIgnoreCase))
            {
                RunMenu();
            }

            string favColor = string.Empty;
            string invalidFavColor = string.Empty;
            while (!favColor.IsValidFavColor())
            {
                if (!string.IsNullOrWhiteSpace(invalidFavColor))
                {
                    invalidFavColor.WriteToTerminal(1, 2);
                }
                invalidFavColor = Consts.ADD_FRIEND_FAV_COLOR_VALIDATION_ERROR;
                Consts.ADD_FRIEND_FAV_COLOR_PROMPT.WriteToTerminal(0, 1);
                favColor = Console.ReadLine().Trim();
            }

            dataService.AddFriend(name, favColor);
            Consts.ADD_FRIEND_SUCCESS_CONFIRMATION.WriteToTerminal(0, 2);
        }
        void GoToAllFriends() { }
        void GoToSearchForFriends() { }
        void GoToExitProgram()
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
