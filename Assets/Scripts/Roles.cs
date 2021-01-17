[System.Serializable]
public struct Role
{
    public string text;
    public RoleEnum ID;
    public Faction faction;
    public int balance;
    public int cards;
    public bool good;
    public bool nightImmune;
}

public enum RoleEnum
{
    BODYGUARD,
    DEPUTY,
    DOCTOR,
    INVESTIGATOR,
    MAYOR,
    MEDIUM,
    PEACEFUL_TOWNIE,
    POLITICIAN,
    SHERIFF,
    SPITEFUL_TOWNIE,
    SURVIVOR,
    TOWNIE,
    VETERAN,
    VIGILANTE,
    BLACKMAILER,
    CONSIGLIERE,
    GODFATHER,
    JANITOR,
    MAFIOSO,
    AMNESIAC,
    EXECUTIONER,
    JESTER,
    SERIAL_KILLER,
    WEREWOLF,
    WITCH
}

public enum Faction
{
    TOWN,
    MAFIA,
    NEUTRAL
}