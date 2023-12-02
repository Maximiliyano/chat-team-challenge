namespace ChatTeamChallenge.Contracts.Enums;

[Flags]
public enum CreativeRoles
{
    User = 0,
    Artist = 1 << 0,
    Creator = 1 << 1,
    Designer = 1 << 2,
    Dancer = 1 << 3,
    Videographer = 1 << 4,
    Photographer = 1 << 5,
    Painter = 1 << 6,
    Musician = 1 << 7,
    Writer = 1 << 8,
    Moderator = 1 << 9
}
