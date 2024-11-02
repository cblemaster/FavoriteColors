using System.Drawing;

namespace FavoriteColors.Console.Models;

internal class Friend
{
    internal uint Id { get; set; }
    internal string FirstName { get; set; } = string.Empty;
    internal Color FavoriteColor { get; set; }
}
