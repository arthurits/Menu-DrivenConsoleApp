using System.ComponentModel;

namespace MenuChoices;

public enum MenuChoices
{
    [Description("No value selected, default")]
    NoSelection = 0,

    [Description("Eat Candy")]
    EatCandy = 1,

    [Description("Go Fishing")]
    GoFishing = 2,

    [Description("Play Basketball")]
    PlayBasketball = 3,

    [Description("Exit")]
    Exit = 4
}
