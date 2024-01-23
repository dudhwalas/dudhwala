using Microsoft.FluentUI.AspNetCore.Components;
namespace Client.Application;
public static class Constants
{
    public static string APP_NAME = "x x x"; 
    public static string MODULE_CATALOG = "Catalog";
    public static string MODULE_DELIVERY_SQUAD = "Delivery Squad";
    
    public static Emoji EMOJI_CATALOG = new Emojis.Objects.Flat.Default.ShoppingBags();
    public static Emoji EMOJI_DELIVERY_SQUAD = new Emojis.PeopleBody.Flat.Default.ConstructionWorker();
}
