using UnityEngine;

//TODO
/*
 * Editar/Remover jogadores no profile deles
 * BUG: Morto pelo Vigilante apareceu limpo (janitor morreu nessa ronda)
 * BUG: Executioner ganhou morto (alvo morreu enforcado depois do executioner morrer)
 * General roles statistics
 * Adicionar Wake a roles tipo Survivor (se houver apenas 1), para reduzir tempo de espera a checar cartas
*/

public class GlobalData : MonoBehaviour
{
    public static GlobalData GD; // Singleton

    private const int N_ROLES = 25;

    [Header("Config")]
    public Color townColor;
    public Color mafiaColor;
    public Color amnesiacColor;
    public Color executionerColor;
    public Color jesterColor;
    public Color serialKillerColor;
    public Color werewolfColor;
    public Color witchColor;
    public Color goodColor;
    public Color evilColor;

    private Role[] allRoles;

    private void Awake()
    {
        if (GD == null)
        {
            GD = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        allRoles = new Role[N_ROLES];
        loadRoles();
    }

    public int GetNRoles()
    {
        return N_ROLES;
    }

    public Role[] getAllRoles()
    {
        return allRoles;
    }

    public Role getRole(RoleEnum role)
    {
        return GetRole((int)role);
    }

    public Role GetRole(int role)
    {
        return allRoles[role];
    }

    public Color GetRoleColor(Role r)
    {
        return GetRoleColor(r.ID);
    }

    public Color GetRoleColor(RoleEnum r)
    {
        return GetRoleColor((int)r);
    }

    public Color GetRoleColor(int role)
    {
        switch (allRoles[role].faction)
        {
            case Faction.TOWN:
                return townColor;
            case Faction.MAFIA:
                return mafiaColor;
            case Faction.NEUTRAL:
                switch (allRoles[role].ID)
                {
                    case RoleEnum.AMNESIAC:
                        return amnesiacColor;
                    case RoleEnum.EXECUTIONER:
                        return executionerColor;
                    case RoleEnum.JESTER:
                        return jesterColor;
                    case RoleEnum.SERIAL_KILLER:
                        return serialKillerColor;
                    case RoleEnum.WEREWOLF:
                        return werewolfColor;
                    case RoleEnum.WITCH:
                        return witchColor;
                    default:
                        Debug.LogError("Invalid Role");
                        return Color.clear;
                }
            default:
                Debug.LogError("Invalid Role");
                return Color.clear;
        }
    }

    private void loadRoles()
    {
        Role r;

        r.text = "Bodyguard";
        r.ID = RoleEnum.BODYGUARD;
        r.faction = Faction.TOWN;
        r.balance = 4;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[0] = r;

        r.text = "Deputy";
        r.ID = RoleEnum.DEPUTY;
        r.faction = Faction.TOWN;
        r.balance = 4;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[1] = r;

        r.text = "Doctor";
        r.ID = RoleEnum.DOCTOR;
        r.faction = Faction.TOWN;
        r.balance = 4;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[2] = r;

        r.text = "Investigator";
        r.ID = RoleEnum.INVESTIGATOR;
        r.faction = Faction.TOWN;
        r.balance = 6;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[3] = r;

        r.text = "Mayor";
        r.ID = RoleEnum.MAYOR;
        r.faction = Faction.TOWN;
        r.balance = 8;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[4] = r;

        r.text = "Medium";
        r.ID = RoleEnum.MEDIUM;
        r.faction = Faction.TOWN;
        r.balance = 3;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[5] = r;

        r.text = "Peaceful Townie";
        r.ID = RoleEnum.PEACEFUL_TOWNIE;
        r.faction = Faction.TOWN;
        r.balance = -1;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[6] = r;

        r.text = "Politician";
        r.ID = RoleEnum.POLITICIAN;
        r.faction = Faction.TOWN;
        r.balance = -1;
        r.cards = 2;
        r.good = false;
        r.nightImmune = false;
        allRoles[7] = r;

        r.text = "Sheriff";
        r.ID = RoleEnum.SHERIFF;
        r.faction = Faction.TOWN;
        r.balance = 7;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[8] = r;

        r.text = "Spiteful Townie";
        r.ID = RoleEnum.SPITEFUL_TOWNIE;
        r.faction = Faction.TOWN;
        r.balance = -1;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[9] = r;

        r.text = "Survivor";
        r.ID = RoleEnum.SURVIVOR;
        r.faction = Faction.TOWN;
        r.balance = 4;
        r.cards = 2;
        r.good = true;
        r.nightImmune = true;
        allRoles[10] = r;

        r.text = "Townie";
        r.ID = RoleEnum.TOWNIE;
        r.faction = Faction.TOWN;
        r.balance = 1;
        r.cards = 8;
        r.good = true;
        r.nightImmune = false;
        allRoles[11] = r;

        r.text = "Veteran";
        r.ID = RoleEnum.VETERAN;
        r.faction = Faction.TOWN;
        r.balance = 3;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[12] = r;

        r.text = "Vigilante";
        r.ID = RoleEnum.VIGILANTE;
        r.faction = Faction.TOWN;
        r.balance = 5;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[13] = r;

        r.text = "Blackmailer";
        r.ID = RoleEnum.BLACKMAILER;
        r.faction = Faction.MAFIA;
        r.balance = -9;
        r.cards = 1;
        r.good = false;
        r.nightImmune = false;
        allRoles[14] = r;

        r.text = "Consigliere";
        r.ID = RoleEnum.CONSIGLIERE;
        r.faction = Faction.MAFIA;
        r.balance = -10;
        r.cards = 1;
        r.good = false;
        r.nightImmune = false;
        allRoles[15] = r;

        r.text = "Godfather";
        r.ID = RoleEnum.GODFATHER;
        r.faction = Faction.MAFIA;
        r.balance = -8;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[16] = r;

        r.text = "Janitor";
        r.ID = RoleEnum.JANITOR;
        r.faction = Faction.MAFIA;
        r.balance = -8;
        r.cards = 1;
        r.good = false;
        r.nightImmune = false;
        allRoles[17] = r;

        r.text = "Mafioso";
        r.ID = RoleEnum.MAFIOSO;
        r.faction = Faction.MAFIA;
        r.balance = -6;
        r.cards = 5;
        r.good = false;
        r.nightImmune = false;
        allRoles[18] = r;

        r.text = "Amnesiac";
        r.ID = RoleEnum.AMNESIAC;
        r.faction = Faction.NEUTRAL;
        r.balance = 0;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[19] = r;

        r.text = "Executioner";
        r.ID = RoleEnum.EXECUTIONER;
        r.faction = Faction.NEUTRAL;
        r.balance = -4;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[20] = r;

        r.text = "Jester";
        r.ID = RoleEnum.JESTER;
        r.faction = Faction.NEUTRAL;
        r.balance = -1;
        r.cards = 2;
        r.good = true;
        r.nightImmune = false;
        allRoles[21] = r;

        r.text = "Serial Killer";
        r.ID = RoleEnum.SERIAL_KILLER;
        r.faction = Faction.NEUTRAL;
        r.balance = -8;
        r.cards = 1;
        r.good = false;
        r.nightImmune = true;
        allRoles[22] = r;

        r.text = "Werewolf";
        r.ID = RoleEnum.WEREWOLF;
        r.faction = Faction.NEUTRAL;
        r.balance = -9;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[23] = r;

        r.text = "Witch";
        r.ID = RoleEnum.WITCH;
        r.faction = Faction.NEUTRAL;
        r.balance = -5;
        r.cards = 1;
        r.good = true;
        r.nightImmune = false;
        allRoles[24] = r;
    }
}
