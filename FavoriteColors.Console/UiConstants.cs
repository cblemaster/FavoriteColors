using FavoriteColors.Console.Extensions;

namespace FavoriteColors.Console;

internal static class UiConstants
{
    internal const string INTRO_TEXT = "Welcome to Favorite Colors!!\nThis app makes it easy to keep track of the favorite colors of your friends!";
    internal const string DATA_LOADING_MESSAGE = "Loading data...";
    internal const string DATA_LOAD_FAILURE_ERROR = "Error: Unknown error loading data. The data was found but could not be loaded.\nThe program will now close.";
    internal const string MENU_TEXT = "*** MENU ***";
    internal const string MENU_OPTION_PROMPT = "Please select one of these menu options:\n1 = Add friend\n2 = See all friends\n3 = Search for friends\n{esc} = Exit program";
    internal const string MENU_OPTION_VALIDATION_ERROR = "Error: Invalid menu option.";
    internal const string ADD_FRIEND_HEADER = "*** ADD FRIEND ***";
    internal const string ADD_FRIEND_NAME_PROMPT = "Enter friend's first name, or {X} to return to menu:";
    internal const string ADD_FRIEND_TRIM_WHITESPACE_WARNING = "Warning: The name input has leading or trailing whitespace that will be trimmed off.";
    internal const string ADD_FRIEND_NAME_VALIDATION_ERROR = "Error: first name input is invalid.\nFirst name must be between one(1) and fifteen(15) characters in length.";
    internal const string ADD_FRIEND_FAV_COLOR_PROMPT = "What is your friend's favorite color? Choose from these colors:";
    internal const string ADD_FRIEND_FAV_COLOR_VALIDATION_ERROR = "Error: color input is invalid.";
    internal const string ADD_FRIEND_SUCCESS_CONFIRMATION = "Friend added sucessfully!!";
    internal const string ADD_FRIEND_FAILURE_ERROR = "Error: An unknown error occurred when attempting to add the friend.";
    internal const string SEE_ALL_FRIENDS_HEADER = "*** ALL FRIENDS ***";
    internal const string SEE_ALL_FRIENDS_NONE_FOUND_MESSAGE = "Uh oh! No friends found. Add some friends from the main menu!";
    internal const string SEARCH_FOR_FRIENDS_HEADER = "*** SEARCH FOR FRIENDS ***";
    internal const string SEARCH_FOR_FRIENDS_SEARCH_TEXT_PROMPT = "Enter search characters:";
    internal const string SEARCH_FOR_FRIENDS_SEARCH_TEXT_VALIDATION_ERROR = "Error: Invalid search characters. Search characters must be fifteen(15) characters or fewer in length.";
    internal const string SEARCH_RESULTS_HEADER = "SEARCH RESULTS";
    internal const string EXIT_CONFIRM_EXIT_PROMPT = "*** Are you sure you want to exit the program? Press Y(es) or N(o)";
    internal const string EXIT_CONFIRM_EXIT_VALIDATION_ERROR = "Error: Invalid selection.";
    internal const string EXIT_SAVING_DATA_MESSAGE = "Saving data...";
    internal const string EXIT_DATA_SAVE_SUCCESS_CONFIRMATION = "Data saved successfully!!";
    internal const string EXIT_DATA_SAVE_ERROR = "Error: Unknown error saving data.";
    internal const string APP_CLOSING_MESSAGE = "Exiting program...goodbye!";
    
    internal static string Stars => new('*', count: 80);
    internal static string SearchForFriendsNoneFoundMessage(string searchTerm) => $"Uh oh! No friends matching search term '{searchTerm}' were found";
}
