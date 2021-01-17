using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Gameplay : MonoBehaviour
{
    private GlobalData GD;
    private DataTransfer DT;
    private SaveFile SF;
    private Languages LANG;
    private int lang;

    private static Role BODYGUARD;
    private static Role DEPUTY;
    private static Role DOCTOR;
    private static Role INVESTIGATOR;
    private static Role MAYOR;
    private static Role MEDIUM;
    private static Role PEACEFUL_TOWNIE;
    private static Role POLITICIAN;
    private static Role SHERIFF;
    private static Role SPITEFUL_TOWNIE;
    private static Role SURVIVOR;
    private static Role TOWNIE;
    private static Role VETERAN;
    private static Role VIGILANTE;
    private static Role BLACKMAILER;
    private static Role CONSIGLIERE;
    private static Role GODFATHER;
    private static Role JANITOR;
    private static Role MAFIOSO;
    private static Role AMNESIAC;
    private static Role EXECUTIONER;
    private static Role JESTER;
    private static Role SERIAL_KILLER;
    private static Role WEREWOLF;
    private static Role WITCH;

    public Music music;

    public GameObject buttonNext;
    public GameObject panelHelp;
    private bool helpOpened;
    public Text textHelp;
    public Text title;
    public Text message;
    public GameObject layoutAskAbility;
    public GameObject[] buttonsAbility;
    public Sprite usedAbility;
    public Sprite unusedAbility;
    public GameObject scrollerRoleList;
    public GameObject scrollerPlayerList;
    public GameObject speak;
    public GameObject show;
    public GameObject card;
    public GameObject check;
    public GameObject tap;
    public Transform playerList;
    public Transform roleList;
    public Transform prefabItem;
    public GameObject popupDayOptions;
    public GameObject bDayOptions;
    public Button bMayorReveal;
    public Text textBMayorReveal;
    public Text labelMayorRevealed;
    public Text textGuilty;
    public Text textInnocent;

    private List<Person> playingPersons;
    private Role[] playingRoles;
    private Role[] allRoles;
    private Item[] rolesItems;
    private List<int> selectedRolesIndexes;
    private Player[] players;
    private Item[] playersItems;
    private List<int> selectedPlayersIndexes;
    private Player roleless;

    private List<Player> toDie;
    private Player lynched;

    private int minPick;
    private int maxPick;

    private int nAllRoles;
    private int nPlayers;
    private int night;
    private int nAlivePlayers;
    private int nAliveMafia;
    private int nMafiaNoRole;

    private bool aUse;
    private Player blackmailed;
    private Player lastBlackmailed;
    private int maxConsigliere;
    private int aConsigliere;
    private int aJanitor;
    private string cleanedRolePlaying;
    private int maxVigilante;
    private int aVigilante;
    private int maxVeteran;
    private int aVeteran;
    private bool alert;
    private int maxInvestigator;
    private int aInvestigator;
    private Player bodyguarded;
    private int aDoctor;
    private Player healed;
    private bool seance;                //Anyone to seance?
    private List<Player> pickedPossibleKillers;
    private bool mayorRevealed;
    private int aMayor;
    private Player executionerTarget;
    private Player amnesiacPlayer;
    private bool existsAmnesiac;
    private bool amnesiacTurn;
    private Role amnesiacRole;
    private bool lynchedSurvivor; // Used in Survivor Variant to block town abilities

    private enum Panel
    {
        AMNESIAC_ROLES,
        EVERYONE_SLEEP,
        GODFATHER_WAKE,
        GODFATHER_WHO,
        BLACKMAILER_WAKE,
        BLACKMAILER_WHO,
        CONSIGLIERE_WAKE,
        CONSIGLIERE_WHO,
        JANITOR_WAKE,
        JANITOR_WHO,
        CLEANED_ROLE,
        MAFIOSO_WAKE,
        MAFIOSO_WHO,
        MAFIA_WAKE,
        BLACKMAILER,
        CONSIGLIERE_ASK,
        CONSIGLIERE,
        CONSIGLIERE_SHOW,
        MAFIA,
        MAFIA_SLEEP,
        SERIAL_KILLER_WAKE,
        SERIAL_KILLER_WHO,
        SERIAL_KILLER,
        SERIAL_KILLER_SLEEP,
        WITCH_WAKE,
        WITCH_WHO,
        WITCH,
        WITCH_SLEEP,
        WEREWOLF_WAKE,
        WEREWOLF,
        WEREWOLF_SLEEP,
        VIGILANTE_WAKE,
        VIGILANTE_WHO,
        VIGILANTE_ASK,
        VIGILANTE,
        VIGILANTE_SLEEP,
        VETERAN_WAKE,
        VETERAN_WHO,
        VETERAN_ASK,
        VETERAN_SLEEP,
        INVESTIGATOR_WAKE,
        INVESTIGATOR_WHO,
        INVESTIGATOR_ASK,
        INVESTIGATOR,
        INVESTIGATOR_SHOW,
        INVESTIGATOR_SLEEP,
        BODYGUARD_WAKE,
        BODYGUARD_WHO,
        BODYGUARD,
        BODYGUARD_SLEEP,
        DOCTOR_WAKE,
        DOCTOR_WHO,
        DOCTOR_ASK,
        DOCTOR,
        DOCTOR_SLEEP,
        POLITICIAN_WHO,
        SHERIFF_WAKE,
        SHERIFF_WHO,
        SHERIFF,
        SHERIFF_SHOW,
        SHERIFF_SLEEP,
        DEPUTY_WAKE,
        DEPUTY_WHO,
        DEPUTY,
        DEPUTY_SHOW,
        DEPUTY_SLEEP,
        MEDIUM_WAKE,
        MEDIUM,
        MEDIUM_SHOW_ROLE,
        MEDIUM_SHOW,
        MEDIUM_SLEEP,
        EXECUTIONER_WAKE,
        EXECUTIONER_WHO,
        EXECUTIONER,
        EXECUTIONER_SLEEP,
        AMNESIAC_WAKE,
        AMNESIAC,
        AMNESIAC_SLEEP,
        ROLELESS_PLAYERS,
        EXECUTIONER_JESTER,
        EVERYONE_WAKE,
        WHO_DIED,
        DISCUSSION,
        LYNCH,
        SHOW_LYNCHED,
        JESTER,
        JESTER_SHOW,
        TOWN_WIN,
        MAFIA_WIN,
        WITCH_WIN,
        SERIAL_KILLER_WIN,
        WEREWOLF_WIN,
        JESTER_WIN,
        EXECUTIONER_WIN,
        DRAW,
        WINNERS,
        END
    }

    private Panel panel;
    private Panel nextPanel;

    private void Start()
    {
        GD = GlobalData.GD;
        DT = DataTransfer.DT;
        SF = SaveFile.SF;
        LANG = Languages.LA;
        lang = LANG.GetLanguage();

        //Language
        speak.GetComponent<Text>().text = LANG.speak[lang];
        show.GetComponent<Text>().text = LANG.show[lang];
        card.GetComponent<Text>().text = LANG.card[lang];
        check.GetComponent<Text>().text = LANG.check[lang];
        tap.GetComponent<Text>().text = LANG.tap[lang];
        textBMayorReveal.text = LANG.revealMayor[lang];

        //Assign roles
        BODYGUARD = GD.getRole(RoleEnum.BODYGUARD);
        DEPUTY = GD.getRole(RoleEnum.DEPUTY);
        DOCTOR = GD.getRole(RoleEnum.DOCTOR);
        INVESTIGATOR = GD.getRole(RoleEnum.INVESTIGATOR);
        MAYOR = GD.getRole(RoleEnum.MAYOR);
        MEDIUM = GD.getRole(RoleEnum.MEDIUM);
        PEACEFUL_TOWNIE = GD.getRole(RoleEnum.PEACEFUL_TOWNIE);
        POLITICIAN = GD.getRole(RoleEnum.POLITICIAN);
        SHERIFF = GD.getRole(RoleEnum.SHERIFF);
        SPITEFUL_TOWNIE = GD.getRole(RoleEnum.SPITEFUL_TOWNIE);
        SURVIVOR = GD.getRole(RoleEnum.SURVIVOR);
        TOWNIE = GD.getRole(RoleEnum.TOWNIE);
        VETERAN = GD.getRole(RoleEnum.VETERAN);
        VIGILANTE = GD.getRole(RoleEnum.VIGILANTE);
        BLACKMAILER = GD.getRole(RoleEnum.BLACKMAILER);
        CONSIGLIERE = GD.getRole(RoleEnum.CONSIGLIERE);
        GODFATHER = GD.getRole(RoleEnum.GODFATHER);
        JANITOR = GD.getRole(RoleEnum.JANITOR);
        MAFIOSO = GD.getRole(RoleEnum.MAFIOSO);
        AMNESIAC = GD.getRole(RoleEnum.AMNESIAC);
        EXECUTIONER = GD.getRole(RoleEnum.EXECUTIONER);
        JESTER = GD.getRole(RoleEnum.JESTER);
        SERIAL_KILLER = GD.getRole(RoleEnum.SERIAL_KILLER);
        WEREWOLF = GD.getRole(RoleEnum.WEREWOLF);
        WITCH = GD.getRole(RoleEnum.WITCH);

        Transform t;
        Item it;
        playingPersons = DT.getPlayingPersons();

        nPlayers = playingPersons.Count;
        nAllRoles = GD.GetNRoles();
        nAlivePlayers = nPlayers;
        players = new Player[nPlayers];
        playingRoles = DT.getPlayingRoles();

        bool useFullName;

        for (int i = 0; i < nPlayers; ++i)
        {
            useFullName = playingPersons[i].pNameShort == string.Empty;
            if (!useFullName)
            {
                for (int x = 0; x < i; ++x)
                {
                    if (playingPersons[x].pNameShort == playingPersons[i].pNameShort)
                    {
                        useFullName = true;
                        players[x].setPName(playingPersons[x].pName);
                        break;
                    }
                }
            }
            players[i] = new Player(i, useFullName ? playingPersons[i].pName : playingPersons[i].pNameShort);
        }

        allRoles = new Role[nAllRoles];
        System.Array.Copy(GD.getAllRoles(), allRoles, nAllRoles);

        playersItems = new Item[nPlayers];
        rolesItems = new Item[GD.GetNRoles()];
        selectedPlayersIndexes = new List<int>();
        selectedRolesIndexes = new List<int>();
        toDie = new List<Player>();

        music.setMaxVolume(SF.getVolume());

        //Buttons for players
        for (int i = 0; i < nPlayers; i++)
        {
            t = Instantiate(prefabItem);
            it = t.gameObject.GetComponent<Item>();

            t.SetParent(playerList);
            it.setText(players[i].getPName());
            int aux = i;                                                            //Has to be allocated inside loop, otherwise AddListener would always get the same value
            it.getButton().onClick.AddListener(() =>
            {
                selectPlayer(aux);
            });
            playersItems[i] = it;

            if (playingRoles[i].faction == Faction.MAFIA)
                nAliveMafia++;
        }

        //Buttons for roles
        for (int i = 0; i < nAllRoles; i++)
        {
            t = Instantiate(prefabItem);
            it = t.gameObject.GetComponent<Item>();

            t.SetParent(roleList);
            it.setText(allRoles[i].text);
            it.setTextColor(GD.GetRoleColor(allRoles[i].ID));
            int aux = i;                                                            //Has to be allocated inside loop, otherwise AddListener would always get the same value
            it.getButton().onClick.AddListener(() =>
            {
                selectRole(aux);
            });
            rolesItems[i] = it;
        }

        nMafiaNoRole = nAliveMafia;

        labelMayorRevealed.color = GD.townColor;
        textInnocent.color = GD.townColor;
        textGuilty.color = GD.mafiaColor;

        //Initiate all abilities, in case Amnesiac needs any
        maxConsigliere = abilityValue();
        aConsigliere = maxConsigliere;
        aJanitor = abilityValue();
        maxVigilante = abilityValue() - 1;
        aVigilante = maxVigilante;
        maxVeteran = abilityValue();
        aVeteran = maxVeteran;
        maxInvestigator = abilityValue();
        aInvestigator = maxInvestigator;
        aDoctor = 1;
        existsAmnesiac = roleExists(AMNESIAC);
        amnesiacRole = AMNESIAC;
        if (roleExists(MAYOR))
            UpdateAMayor();

        clearScreen();
        panelAmnesiacRoles();   //First Panel
    }

    //Testing
    private void Update()
    {
        if (!Application.isEditor)
            return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            string s = string.Empty;
            for (int i = 0; i < nPlayers; i++)
            {
                Player p = players[i];
                s += p.getPName() + " " + p.getRole().text + " " + (p.getIsAlive() ? "Alive" : "Dead") + "\n";
            }
            Debug.Log(s);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            string s = string.Empty;
            for (int i = 0; i < nPlayers; i++)
            {
                Player p = players[i];
                if (p.getIsAlive())
                {
                    s += p.getRole().text + "\n";
                }
            }
            Debug.Log(s);
        }
    }

    public void buttonHelp()
    {
        helpOpened = !helpOpened;
        panelHelp.SetActive(helpOpened);
    }

    private void clearScreen()
    {
        title.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
        layoutAskAbility.SetActive(false);
        scrollerPlayerList.SetActive(false);

        clearProgressAbility();
        clearRoleList();
        clearPlayerList();

        speak.SetActive(false);
        show.SetActive(false);
        card.SetActive(false);
        check.SetActive(false);
        tap.SetActive(false);

        buttonNext.GetComponentInChildren<Text>().text = LANG.next[lang];
        textHelp.text = LANG.noHelp[lang];
        bDayOptions.gameObject.SetActive(false);
    }

    public void openNextPanel()
    {
        if (popupDayOptions.activeSelf)
            return;

        if (selectedRolesIndexes.Count > 0)
        {
            switch (panel)
            {
                case Panel.AMNESIAC_ROLES:
                    amnesiacRole = allRoles[selectedRolesIndexes[Random.Range(0, selectedRolesIndexes.Count)]];
                    break;
                case Panel.ROLELESS_PLAYERS:
                    Role r = GD.GetRole(selectedRolesIndexes[0]);
                    roleless.setStartingRole(r);
                    if (r.ID == AMNESIAC.ID)
                        amnesiacPlayer = roleless;
                    break;
            }
            selectedRolesIndexes.Clear();
        }

        if (selectedPlayersIndexes.Count > 0)
        {
            Player p = players[selectedPlayersIndexes[0]];
            Player p1 = null;
            bool goingToShow = false;

            if (selectedPlayersIndexes.Count > 1)
                p1 = players[selectedPlayersIndexes[1]];

            switch (panel)
            {
                case Panel.GODFATHER_WHO:
                    p.setStartingRole(GODFATHER);
                    --nMafiaNoRole;
                    break;
                case Panel.BLACKMAILER_WHO:
                    p.setStartingRole(BLACKMAILER);
                    --nMafiaNoRole;
                    break;
                case Panel.CONSIGLIERE_WHO:
                    p.setStartingRole(CONSIGLIERE);
                    --nMafiaNoRole;
                    break;
                case Panel.JANITOR_WHO:
                    p.setStartingRole(JANITOR);
                    --nMafiaNoRole;
                    break;
                case Panel.MAFIOSO_WHO:
                    foreach (int i in selectedPlayersIndexes)
                    {
                        players[i].setStartingRole(MAFIOSO);
                        --nMafiaNoRole;
                    }
                    break;
                case Panel.BLACKMAILER:
                    blackmailed = p;
                    p.addVisitor(findPlayer(BLACKMAILER.ID));
                    break;
                case Panel.CONSIGLIERE:
                    if (night == 1)
                    {
                        message.text = p.getPName();
                        message.color = Color.white;
                    }
                    else
                    {
                        message.text = p.getPName() + LANG.strIs[lang] + p.getRole().text;
                        message.color = p.getColor();
                    }
                    goingToShow = true;
                    p.addVisitor(findPlayer(CONSIGLIERE.ID));
                    break;
                case Panel.MAFIA:
                    addToDie(p);
                    Player rm = randomMafia();
                    p.addKiller(rm);
                    p.addVisitor(rm);
                    // Variant - godfatherAloneTwoKills
                    if (p1 != null)
                    {
                        addToDie(p1);
                        p1.addKiller(rm);
                        p1.addVisitor(rm);
                    }
                    break;
                case Panel.SERIAL_KILLER_WHO:
                    p.setStartingRole(SERIAL_KILLER);
                    break;
                case Panel.SERIAL_KILLER:
                    addToDie(p);
                    Player sk = findPlayer(SERIAL_KILLER.ID);
                    p.addKiller(sk);
                    p.addVisitor(sk);
                    break;
                case Panel.WITCH_WHO:
                    p.setStartingRole(WITCH);
                    break;
                case Panel.WITCH:
                    p.setIsCursed(true);
                    p.addVisitor(findPlayer(WITCH.ID));
                    break;
                case Panel.WEREWOLF:
                    Player ww = findPlayer(WEREWOLF.ID);
                    addToDie(p);
                    p.addKiller(ww);
                    p.addVisitor(ww);
                    if (p1 != null)
                    {
                        addToDie(p1);
                        p1.addKiller(ww);
                        p1.addVisitor(ww);
                    }
                    break;
                case Panel.VIGILANTE_WHO:
                    p.setStartingRole(VIGILANTE);
                    break;
                case Panel.VIGILANTE:
                    addToDie(p);
                    Player vig = findPlayer(VIGILANTE.ID);
                    p.addKiller(vig);
                    p.addVisitor(vig);
                    break;
                case Panel.VETERAN_WHO:
                    p.setStartingRole(VETERAN);
                    break;
                case Panel.INVESTIGATOR_WHO:
                    p.setStartingRole(INVESTIGATOR);
                    break;
                case Panel.INVESTIGATOR:
                    if (night == 1)
                    {
                        message.text = p.getPName();
                        message.color = Color.white;
                    }
                    else
                    {
                        message.text = p.getPName() + LANG.strIs[lang] + p.getRole().text;
                        message.color = p.getColor();
                    }
                    goingToShow = true;
                    p.addVisitor(findPlayer(INVESTIGATOR.ID));
                    break;
                case Panel.BODYGUARD_WHO:
                    p.setStartingRole(BODYGUARD);
                    break;
                case Panel.BODYGUARD:
                    bodyguarded = p;
                    p.addVisitor(findPlayer(BODYGUARD.ID));
                    break;
                case Panel.DOCTOR_WHO:
                    p.setStartingRole(DOCTOR);
                    break;
                case Panel.DOCTOR:
                    healed = p;
                    p.addVisitor(findPlayer(DOCTOR.ID));
                    break;
                case Panel.POLITICIAN_WHO:
                    foreach (int i in selectedPlayersIndexes)
                        players[i].setStartingRole(POLITICIAN);
                    break;
                case Panel.SHERIFF_WHO:
                    p.setStartingRole(SHERIFF);
                    break;
                case Panel.SHERIFF:
                case Panel.DEPUTY:
                    if (p.getRole().good)
                    {
                        message.text = LANG.good[lang];
                        message.color = GD.goodColor;
                    }
                    else
                    {
                        message.text = LANG.evil[lang];
                        message.color = GD.evilColor;
                    }
                    goingToShow = true;
                    p.addVisitor(roleCountAlive(SHERIFF) > 0 ? findPlayer(SHERIFF.ID) : findPlayer(DEPUTY.ID));
                    break;
                case Panel.DEPUTY_WHO:
                    p.setStartingRole(DEPUTY);
                    break;
                case Panel.MEDIUM:
                    List<Player> killers = new List<Player>(p.getKillers()); //Everyone that killed seanced player
                    List<Player> everyPossibleKiller = new List<Player>();   //Everyone that can be added as a possible killer
                    Player killer = killers[Random.Range(0, killers.Count)]; //Random killer to be considered
                    pickedPossibleKillers = new List<Player>();              //To be shown as suspect of killing
                    int nPossibleKillers = 0;

                    if (nAlivePlayers < 8)
                        nPossibleKillers = 2;
                    else if (nAlivePlayers < 13)
                        nPossibleKillers = 3;
                    else
                        nPossibleKillers = 4;

                    for (int i = 0; i < nPlayers; ++i)
                    {
                        Player pl = players[i];
                        if (pl.getIsAlive() && pl != killer && pl != findPlayer(MEDIUM.ID))
                            everyPossibleKiller.Add(pl);
                    }

                    pickedPossibleKillers.Add(killer);

                    int r1 = -1;
                    int r2 = -1;
                    int r3 = -1;
                    int nEveryPossibleKiller = everyPossibleKiller.Count;

                    r1 = Random.Range(0, nEveryPossibleKiller);
                    pickedPossibleKillers.Add(everyPossibleKiller[r1]);

                    if (nPossibleKillers >= 3)
                    {
                        do
                        {
                            r2 = Random.Range(0, nEveryPossibleKiller);
                        } while (r2 == r1);
                        pickedPossibleKillers.Add(everyPossibleKiller[r2]);

                        if (nPossibleKillers >= 4)
                        {
                            do
                            {
                                r3 = Random.Range(0, nEveryPossibleKiller);
                            } while (r3 == r1 || r3 == r2);
                            pickedPossibleKillers.Add(everyPossibleKiller[r3]);
                        }
                    }

                    goingToShow = true;
                    break;
                case Panel.MEDIUM_SHOW_ROLE:
                    goingToShow = true;
                    break;
                case Panel.EXECUTIONER_WHO:
                    p.setStartingRole(EXECUTIONER);
                    break;
                case Panel.EXECUTIONER:
                    executionerTarget = p;
                    break;
                case Panel.LYNCH:
                    lynched = p;
                    nextPanel = Panel.SHOW_LYNCHED;
                    break;
                case Panel.JESTER:
                    addToDie(p);
                    break;
            }
            if (!goingToShow)
                selectedPlayersIndexes.Clear();
        }
        else if (aUse)
        {
            switch (panel)
            {
                case Panel.CONSIGLIERE_ASK:
                    --aConsigliere;
                    nextPanel = Panel.CONSIGLIERE;
                    break;
                case Panel.VIGILANTE_ASK:
                    --aVigilante;
                    nextPanel = Panel.VIGILANTE;
                    break;
                case Panel.VETERAN_ASK:
                    --aVeteran;
                    alert = true;
                    break;
                case Panel.INVESTIGATOR_ASK:
                    --aInvestigator;
                    nextPanel = Panel.INVESTIGATOR;
                    break;
                case Panel.DOCTOR_ASK:
                    removeToDie(findPlayer(DOCTOR.ID));
                    --aDoctor;
                    nextPanel = Panel.DOCTOR_SLEEP;
                    break;
            }
        }

        if (nextPanel != Panel.END)
            clearScreen();

        panel = nextPanel;

        switch (panel)
        {
            case Panel.AMNESIAC_ROLES:
                panelAmnesiacRoles();
                break;
            case Panel.EVERYONE_SLEEP:
                panelEveryoneSleep();
                break;
            case Panel.GODFATHER_WAKE:
                panelGodfatherWake();
                break;
            case Panel.GODFATHER_WHO:
                panelGodfatherWho();
                break;
            case Panel.BLACKMAILER_WAKE:
                panelBlackmailerWake();
                break;
            case Panel.BLACKMAILER_WHO:
                panelBlackmailerWho();
                break;
            case Panel.CONSIGLIERE_WAKE:
                panelConsigliereWake();
                break;
            case Panel.CONSIGLIERE_WHO:
                panelConsigliereWho();
                break;
            case Panel.JANITOR_WAKE:
                panelJanitorWake();
                break;
            case Panel.JANITOR_WHO:
                panelJanitorWho();
                break;
            case Panel.CLEANED_ROLE:
                panelCleanedRole();
                break;
            case Panel.MAFIOSO_WAKE:
                panelMafiosoWake();
                break;
            case Panel.MAFIOSO_WHO:
                panelMafiosoWho();
                break;
            case Panel.MAFIA_WAKE:
                panelMafiaWake();
                break;
            case Panel.BLACKMAILER:
                panelBlackmailer();
                break;
            case Panel.CONSIGLIERE_ASK:
                panelConsigliereAsk();
                break;
            case Panel.CONSIGLIERE:
                panelConsigliere();
                break;
            case Panel.CONSIGLIERE_SHOW:
                panelConsigliereShow();
                break;
            case Panel.MAFIA:
                panelMafia();
                break;
            case Panel.MAFIA_SLEEP:
                panelMafiaSleep();
                break;
            case Panel.SERIAL_KILLER_WAKE:
                panelSerialKillerWake();
                break;
            case Panel.SERIAL_KILLER_WHO:
                panelSerialKillerWho();
                break;
            case Panel.SERIAL_KILLER:
                panelSerialKiller();
                break;
            case Panel.SERIAL_KILLER_SLEEP:
                panelSerialKillerSleep();
                break;
            case Panel.WITCH_WAKE:
                panelWitchWake();
                break;
            case Panel.WITCH_WHO:
                panelWitchWho();
                break;
            case Panel.WITCH:
                panelWitch();
                break;
            case Panel.WEREWOLF_WAKE:
                panelWerewolfWake();
                break;
            case Panel.WEREWOLF:
                panelWerewolf();
                break;
            case Panel.WEREWOLF_SLEEP:
                panelWerewolfSleep();
                break;
            case Panel.WITCH_SLEEP:
                panelWitchSleep();
                break;
            case Panel.VIGILANTE_WAKE:
                panelVigilanteWake();
                break;
            case Panel.VIGILANTE_WHO:
                panelVigilanteWho();
                break;
            case Panel.VIGILANTE_ASK:
                panelVigilanteAsk();
                break;
            case Panel.VIGILANTE:
                panelVigilante();
                break;
            case Panel.VIGILANTE_SLEEP:
                panelVigilanteSleep();
                break;
            case Panel.VETERAN_WAKE:
                panelVeteranWake();
                break;
            case Panel.VETERAN_WHO:
                panelVeteranWho();
                break;
            case Panel.VETERAN_ASK:
                panelVeteranAsk();
                break;
            case Panel.VETERAN_SLEEP:
                panelVeteranSleep();
                break;
            case Panel.INVESTIGATOR_WAKE:
                panelInvestigatorWake();
                break;
            case Panel.INVESTIGATOR_WHO:
                panelInvestigatorWho();
                break;
            case Panel.INVESTIGATOR_ASK:
                panelInvestigatorAsk();
                break;
            case Panel.INVESTIGATOR:
                panelInvestigator();
                break;
            case Panel.INVESTIGATOR_SHOW:
                panelInvestigatorShow();
                break;
            case Panel.INVESTIGATOR_SLEEP:
                panelInvestigatorSleep();
                break;
            case Panel.BODYGUARD_WAKE:
                panelBodyguardWake();
                break;
            case Panel.BODYGUARD_WHO:
                panelBodyguardWho();
                break;
            case Panel.BODYGUARD:
                panelBodyguard();
                break;
            case Panel.BODYGUARD_SLEEP:
                panelBodyguardSleep();
                break;
            case Panel.DOCTOR_WAKE:
                panelDoctorWake();
                break;
            case Panel.DOCTOR_WHO:
                panelDoctorWho();
                break;
            case Panel.DOCTOR_ASK:
                panelDoctorAsk();
                break;
            case Panel.DOCTOR:
                panelDoctor();
                break;
            case Panel.DOCTOR_SLEEP:
                panelDoctorSleep();
                break;
            case Panel.POLITICIAN_WHO:
                panelPoliticianWho();
                break;
            case Panel.SHERIFF_WAKE:
                panelSheriffWake();
                break;
            case Panel.SHERIFF_WHO:
                panelSheriffWho();
                break;
            case Panel.SHERIFF:
                panelSheriff();
                break;
            case Panel.SHERIFF_SHOW:
                panelSheriffShow();
                break;
            case Panel.SHERIFF_SLEEP:
                panelSheriffSleep();
                break;
            case Panel.DEPUTY_WAKE:
                panelDeputyWake();
                break;
            case Panel.DEPUTY_WHO:
                panelDeputyWho();
                break;
            case Panel.DEPUTY:
                panelDeputy();
                break;
            case Panel.DEPUTY_SHOW:
                panelDeputyShow();
                break;
            case Panel.DEPUTY_SLEEP:
                panelDeputySleep();
                break;
            case Panel.MEDIUM_WAKE:
                panelMediumWake();
                break;
            case Panel.MEDIUM:
                panelMedium();
                break;
            case Panel.MEDIUM_SHOW_ROLE:
                panelMediumShowRole();
                break;
            case Panel.MEDIUM_SHOW:
                panelMediumShow();
                break;
            case Panel.MEDIUM_SLEEP:
                panelMediumSleep();
                break;
            case Panel.EXECUTIONER_WAKE:
                panelExecutionerWake();
                break;
            case Panel.EXECUTIONER_WHO:
                panelExecutionerWho();
                break;
            case Panel.EXECUTIONER:
                panelExecutioner();
                break;
            case Panel.EXECUTIONER_SLEEP:
                panelExecutionerSleep();
                break;
            case Panel.AMNESIAC_WAKE:
                panelAmnesiacWake();
                break;
            case Panel.AMNESIAC:
                panelAmnesiac();
                break;
            case Panel.AMNESIAC_SLEEP:
                panelAmnesiacSleep();
                break;
            case Panel.ROLELESS_PLAYERS:
                panelRolelessPlayers();
                break;
            case Panel.EXECUTIONER_JESTER:
                panelExecutionerJester();
                break;
            case Panel.EVERYONE_WAKE:
                panelEveryoneWake();
                break;
            case Panel.WHO_DIED:
                panelWhoDied();
                break;
            case Panel.DISCUSSION:
                panelDiscussion();
                break;
            case Panel.LYNCH:
                panelLynch();
                break;
            case Panel.SHOW_LYNCHED:
                panelShowLynched();
                break;
            case Panel.JESTER:
                panelJester();
                break;
            case Panel.JESTER_SHOW:
                panelJesterShow();
                break;
            case Panel.TOWN_WIN:
                panelTownWin();
                break;
            case Panel.MAFIA_WIN:
                panelMafiaWin();
                break;
            case Panel.SERIAL_KILLER_WIN:
                panelSerialKillerWin();
                break;
            case Panel.WEREWOLF_WIN:
                panelWerewolfWin();
                break;
            case Panel.WITCH_WIN:
                panelWitchWin();
                break;
            case Panel.EXECUTIONER_WIN:
                panelExecutionerWin();
                break;
            case Panel.JESTER_WIN:
                panelJesterWin();
                break;
            case Panel.DRAW:
                panelDraw();
                break;
            case Panel.WINNERS:
                panelWinners();
                break;
            case Panel.END:
                panelEnd();
                break;
        }
    }

    private void panelAmnesiacRoles()
    {
        if (!roleExists(AMNESIAC))
        {
            jumpImmediate(Panel.EVERYONE_SLEEP);
            return;
        }

        useRoleListByAmnesiac();

        useTitle(LANG.possibleAmnesiacRoles[lang], GD.amnesiacColor);
        speak.SetActive(true);
        textHelp.text = LANG.helpPanelAnesiacRoles[lang];
        nextPanel = Panel.EVERYONE_SLEEP;
    }

    private void panelEveryoneSleep()
    {
        music.PlayNightTown();

        night++;

        foreach (Player p in players)
            p.clearVisitors();

        alert = false;
        healed = null;
        lastBlackmailed = blackmailed;
        blackmailed = null;

        if (roleCountAlive(WEREWOLF) > 0)
        {
            Player ww = findPlayer(WEREWOLF.ID);

            if (night % 2 == 0)
            {
                // Variant - werewolfImmuneFullMoon
                if (SF.werewolfImmuneFullMoon)
                    ww.setNightImmune(true);
                // Variant - sheriffFindWerewolfFullMoon
                if (SF.sheriffFindWerewolfFullMoon)
                    ww.setGood(false);
            }
            else
            {
                // Variant - werewolfImmuneFullMoon
                if (SF.werewolfImmuneFullMoon)
                    ww.setNightImmune(false);
                // Variant - sheriffFindWerewolfFullMoon
                if (SF.sheriffFindWerewolfFullMoon)
                    ww.setGood(true);
            }
        }

        speak.SetActive(true);
        useMessage(LANG.everyoneSleep[lang], Color.white);
        textHelp.text = LANG.tell[lang] + LANG.everyone[lang] + LANG.toSleep[lang];
        nextPanel = Panel.GODFATHER_WAKE;
    }

    private void panelGodfatherWake()
    {
        if (nAliveMafia == 0)
        {
            jumpImmediate(Panel.SERIAL_KILLER_WAKE);
            return;
        }

        music.PlayMafia();

        if (night > 1)
        {
            jumpImmediate(Panel.MAFIA_WAKE);
            return;
        }

        if (!roleExists(GODFATHER))
        {
            jumpImmediate(SF.blackmailerPlaysBeforeConsigliere ? Panel.BLACKMAILER_WAKE : Panel.CONSIGLIERE_WAKE); // Variant - blackmailerPlaysBeforeConsigliere
            return;
        }

        speak.SetActive(true);
        useMessage("Godfather " + LANG.wake[lang], GD.mafiaColor);
        textHelp.text = LANG.tell[lang] + "Godfather" + LANG.toWakeUp[lang];
        nextPanel = Panel.GODFATHER_WHO;
    }

    private void panelGodfatherWho()
    {
        useTitle(LANG.whoIs[lang] + " Godfather?", GD.mafiaColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Godfather";
        nextPanel = SF.blackmailerPlaysBeforeConsigliere ? Panel.BLACKMAILER_WAKE : Panel.CONSIGLIERE_WAKE; // Variant - blackmailerPlaysBeforeConsigliere
    }

    private void panelBlackmailerWake()
    {
        if (!roleExists(BLACKMAILER))
        {
            jumpImmediate(SF.blackmailerPlaysBeforeConsigliere ? Panel.CONSIGLIERE_WAKE : Panel.JANITOR_WAKE); // Variant - blackmailerPlaysBeforeConsigliere
            return;
        }

        speak.SetActive(true);
        useMessage("Blackmailer " + LANG.wake[lang], GD.mafiaColor);
        textHelp.text = LANG.tell[lang] + "Blackmailer" + LANG.toWakeUp[lang];
        nextPanel = Panel.BLACKMAILER_WHO;
    }

    private void panelBlackmailerWho()
    {
        useTitle(LANG.whoIs[lang] + " Blackmailer?", GD.mafiaColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Blackmailer";
        nextPanel = SF.blackmailerPlaysBeforeConsigliere ? Panel.CONSIGLIERE_WAKE : Panel.JANITOR_WAKE; // Variant - blackmailerPlaysBeforeConsigliere
    }

    private void panelConsigliereWake()
    {
        if (!roleExists(CONSIGLIERE))
        {
            jumpImmediate(SF.blackmailerPlaysBeforeConsigliere ? Panel.JANITOR_WAKE : Panel.BLACKMAILER_WAKE); // Variant - blackmailerPlaysBeforeConsigliere);
            return;
        }

        speak.SetActive(true);
        useMessage("Consigliere " + LANG.wake[lang], GD.mafiaColor);
        textHelp.text = LANG.tell[lang] + "Consigliere" + LANG.toWakeUp[lang];
        nextPanel = Panel.CONSIGLIERE_WHO;
    }

    private void panelConsigliereWho()
    {
        useTitle(LANG.whoIs[lang] + " Consigliere?", GD.mafiaColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Consigliere";
        nextPanel = SF.blackmailerPlaysBeforeConsigliere ? Panel.JANITOR_WAKE : Panel.BLACKMAILER_WAKE; // Variant - blackmailerPlaysBeforeConsigliere
    }

    private void panelJanitorWake()
    {
        if (!roleExists(JANITOR))
        {
            jumpImmediate(Panel.MAFIOSO_WAKE);
            return;
        }

        speak.SetActive(true);
        useMessage("Janitor " + LANG.wake[lang], GD.mafiaColor);
        textHelp.text = LANG.tell[lang] + "Janitor" + LANG.toWakeUp[lang];
        nextPanel = Panel.JANITOR_WHO;
    }

    private void panelJanitorWho()
    {
        useTitle(LANG.whoIs[lang] + " Janitor?", GD.mafiaColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Janitor";
        nextPanel = Panel.MAFIOSO_WAKE;
    }

    private void panelCleanedRole()
    {
        useTitle(cleanedRolePlaying, Color.white);
        useMessage(LANG.roleCleaned[lang], Color.white);
        textHelp.text = LANG.helpPanelCleanedRole[lang];
        speak.SetActive(true);
    }

    private void panelMafiosoWake()
    {
        if (nMafiaNoRole == 0)
        {
            jumpImmediate(SF.blackmailerPlaysBeforeConsigliere ? Panel.BLACKMAILER : Panel.CONSIGLIERE_ASK); // Variant - blackmailerPlaysBeforeConsigliere
            return;
        }

        //Plural
        if (nMafiaNoRole > 1)
        {
            useMessage("Mafiosos " + LANG.wake[lang], GD.mafiaColor);
            textHelp.text = LANG.tell[lang] + "Mafiosos" + LANG.toWakeUp[lang];
        }
        else
        {
            useMessage("Mafioso " + LANG.wake[lang], GD.mafiaColor);
            textHelp.text = LANG.tell[lang] + "Mafioso" + LANG.toWakeUp[lang];
        }

        speak.SetActive(true);
        nextPanel = Panel.MAFIOSO_WHO;
    }

    private void panelMafiosoWho()
    {
        //Plural
        if (nMafiaNoRole > 1)
        {
            useTitle(LANG.whoAre[lang] + " Mafiosos?", GD.mafiaColor);
            textHelp.text = LANG.pickWhoIs[lang] + "Mafiosos";
        }
        else
        {
            useTitle(LANG.whoIs[lang] + " Mafioso?", GD.mafiaColor);
            textHelp.text = LANG.pickWhoIs[lang] + "Mafioso";
        }
        usePlayerListSetRole(nMafiaNoRole);
        nextPanel = SF.blackmailerPlaysBeforeConsigliere ? Panel.BLACKMAILER : Panel.CONSIGLIERE_ASK; // Variant - blackmailerPlaysBeforeConsigliere
    }

    private void panelMafiaWake()
    {
        speak.SetActive(true);
        useMessage("Mafia " + LANG.wake[lang], GD.mafiaColor);
        textHelp.text = LANG.tell[lang] + "Mafia" + LANG.toWakeUp[lang];
        nextPanel = SF.blackmailerPlaysBeforeConsigliere ? Panel.BLACKMAILER : Panel.CONSIGLIERE_ASK; // Variant - blackmailerPlaysBeforeConsigliere
    }

    private void panelBlackmailer()
    {
        if (roleCountAlive(BLACKMAILER) == 0)
        {
            jumpImmediate(SF.blackmailerPlaysBeforeConsigliere ? Panel.CONSIGLIERE_ASK : Panel.MAFIA); // Variant - blackmailerPlaysBeforeConsigliere
            return;
        }

        useTitle(LANG.silence[lang], GD.mafiaColor);
        if (amnesiacRole.ID == BLACKMAILER.ID)
            textHelp.text = LANG.helpPanelBlackmailerAmne[lang];
        else
        {
            speak.SetActive(true);
            textHelp.text = LANG.helpPanelBlackmailer[lang];
        }
        usePlayerListEveryone();
        nextPanel = SF.blackmailerPlaysBeforeConsigliere ? Panel.CONSIGLIERE_ASK : Panel.MAFIA; // Variant - blackmailerPlaysBeforeConsigliere
    }

    private void panelConsigliereAsk()
    {
        if (roleCountAlive(CONSIGLIERE) == 0)
        {
            jumpImmediate(SF.blackmailerPlaysBeforeConsigliere ? Panel.MAFIA : Panel.BLACKMAILER); // Variant - blackmailerPlaysBeforeConsigliere
            return;
        }

        useTitle(LANG.investigate[lang] + "?", GD.mafiaColor);
        useAskAbility(aConsigliere, maxConsigliere);
        textHelp.text = amnesiacRole.ID == CONSIGLIERE.ID ? LANG.helpPanelAskAmne[lang] : LANG.helpPanelAsk[lang];
        nextPanel = SF.blackmailerPlaysBeforeConsigliere ? Panel.MAFIA : Panel.BLACKMAILER; // Variant - blackmailerPlaysBeforeConsigliere                 //If doesn't use ability
    }

    private void panelConsigliere()
    {
        useTitle(LANG.investigate[lang], GD.mafiaColor);
        usePlayerListByMafia();
        textHelp.text = LANG.helpPanelLimitedAbility[lang];
        nextPanel = Panel.CONSIGLIERE_SHOW;
    }

    private void panelConsigliereShow()
    {
        message.gameObject.SetActive(true);
        if (night == 1)
        {
            card.SetActive(true);
            textHelp.text = LANG.helpPanelConsigliereShowCard[lang];
        }
        else
        {
            show.SetActive(true);
            textHelp.text = LANG.helpPanelConsigliereShow[lang];
        }
        nextPanel = SF.blackmailerPlaysBeforeConsigliere ? Panel.MAFIA : Panel.BLACKMAILER; // Variant - blackmailerPlaysBeforeConsigliere
    }

    private void panelMafia()
    {
        if (night == 1)
        {
            jumpImmediate(Panel.MAFIA_SLEEP);
            return;
        }

        speak.SetActive(true);
        useTitle(LANG.kill[lang], GD.mafiaColor);
        usePlayerListByMafia();
        textHelp.text = LANG.helpPanelMafia[lang];
        nextPanel = Panel.MAFIA_SLEEP;
    }

    private void panelMafiaSleep()
    {
        speak.SetActive(true);
        useMessage("Mafia " + LANG.sleep[lang], GD.mafiaColor);
        textHelp.text = LANG.tell[lang] + "Mafia" + LANG.toSleep[lang];
        nextPanel = Panel.SERIAL_KILLER_WAKE;
    }

    private void panelSerialKillerWake()
    {
        if (amnesiacRole.ID == SERIAL_KILLER.ID)
        {
            jumpImmediate(Panel.WITCH_WAKE);
            return;
        }

        if (!roleExists(SERIAL_KILLER) || night > 1 && roleCountAlive(SERIAL_KILLER) == 0)
        {
            jumpImmediate(Panel.WITCH_WAKE);
            return;
        }

        music.PlaySerialKiller();
        speak.SetActive(true);
        useMessage("Serial Killer " + LANG.wake[lang], GD.serialKillerColor);
        textHelp.text = LANG.tell[lang] + "Serial Killer" + LANG.toWakeUp[lang];
        nextPanel = Panel.SERIAL_KILLER_WHO;
    }

    private void panelSerialKillerWho()
    {
        if (night > 1)
        {
            jumpImmediate(Panel.SERIAL_KILLER);
            return;
        }

        useTitle(LANG.whoIs[lang] + " Serial Killer?", GD.serialKillerColor);
        textHelp.text = LANG.pickWhoIs[lang] + "Serial Killer";
        usePlayerListSetRole(1);
        nextPanel = Panel.SERIAL_KILLER_SLEEP;
    }

    private void panelSerialKiller()
    {
        useTitle(LANG.kill[lang], GD.serialKillerColor);
        usePlayerListByPlayer(1, 1, SERIAL_KILLER);
        textHelp.text = LANG.helpPanelSerialKiller[lang];
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.SERIAL_KILLER_SLEEP;
    }

    private void panelSerialKillerSleep()
    {
        speak.SetActive(true);
        useMessage("Serial Killer " + LANG.sleep[lang], GD.serialKillerColor);
        textHelp.text = LANG.tell[lang] + "Serial Killer" + LANG.toSleep[lang];
        nextPanel = Panel.WITCH_WAKE;
    }

    private void panelWitchWake()
    {
        if (!roleExists(WITCH) || amnesiacRole.ID == WITCH.ID)
        {
            jumpImmediate(Panel.WEREWOLF_WAKE);
            return;
        }

        if (night > 1 && roleCountAlive(WITCH) == 0)
        {
            if (findPlayer(WITCH.ID).getIsCleaned())
            {
                cleanedRolePlaying = "Witch";
                jumpImmediate(Panel.CLEANED_ROLE);
                nextPanel = Panel.WEREWOLF_WAKE;
                return;
            }
            else
            {
                jumpImmediate(Panel.WEREWOLF_WAKE);
                return;
            }
        }

        music.PlayWitch();
        speak.SetActive(true);
        useMessage("Witch " + LANG.wake[lang], GD.witchColor);
        textHelp.text = LANG.tell[lang] + "Witch" + LANG.toWakeUp[lang];
        nextPanel = Panel.WITCH_WHO;
    }

    private void panelWitchWho()
    {
        if (night > 1)
        {
            jumpImmediate(Panel.WITCH);
            return;
        }

        useTitle(LANG.whoIs[lang] + " Witch?", GD.witchColor);
        textHelp.text = LANG.pickWhoIs[lang] + "Witch";
        usePlayerListSetRole(1);
        nextPanel = Panel.WITCH;
    }

    private void panelWitch()
    {
        Player p;

        useTitle(LANG.curse[lang], GD.witchColor);

        buttonNext.SetActive(false);

        speak.SetActive(true);
        scrollerPlayerList.SetActive(true);

        for (int i = 0; i < nPlayers; i++)
        {
            p = players[i];

            if (p.getIsAlive() && !p.getIsCursed() && p.getRole().ID != RoleEnum.WITCH)
                playersItems[i].gameObject.SetActive(true);
        }

        textHelp.text = LANG.helpPanelWitch[lang];
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.WITCH_SLEEP;
    }

    private void panelWitchSleep()
    {
        speak.SetActive(true);
        useMessage("Witch " + LANG.sleep[lang], GD.witchColor);
        textHelp.text = LANG.tell[lang] + "Witch" + LANG.toSleep[lang];
        nextPanel = Panel.WEREWOLF_WAKE;
    }

    private void panelWerewolfWake()
    {
        if (night % 2 != 0 || !roleExists(WEREWOLF) || amnesiacRole.ID == WEREWOLF.ID)
        {
            jumpImmediate(Panel.VIGILANTE_WAKE);
            return;
        }

        if (night > 1 && roleCountAlive(WEREWOLF) == 0)
        {
            if (findPlayer(WEREWOLF.ID).getIsCleaned())
            {
                cleanedRolePlaying = "Werewolf";
                jumpImmediate(Panel.CLEANED_ROLE);
                nextPanel = Panel.VIGILANTE_WAKE;
                return;
            }
            else
            {
                jumpImmediate(Panel.VIGILANTE_WAKE);
                return;
            }
        }

        music.PlayWerewolf();
        speak.SetActive(true);
        useMessage("Werewolf " + LANG.wake[lang], GD.werewolfColor);
        textHelp.text = LANG.tell[lang] + "Werewolf" + LANG.toWakeUp[lang];
        nextPanel = Panel.WEREWOLF;
    }

    private void panelWerewolf()
    {
        useTitle(LANG.kill[lang], GD.werewolfColor);
        usePlayerListByPlayer(nAlivePlayers > 2 ? 2 : 1, 2, WEREWOLF);
        textHelp.text = LANG.helpPanelWerewolf[lang];
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.WEREWOLF_SLEEP;
    }

    private void panelWerewolfSleep()
    {
        speak.SetActive(true);
        useMessage("Werewolf " + LANG.sleep[lang], GD.werewolfColor);
        textHelp.text = LANG.tell[lang] + "Werewolf" + LANG.toSleep[lang];
        nextPanel = Panel.VIGILANTE_WAKE;
    }

    ///////////////////////////////////////////////////////////////////////////////// TOWN ////////////////////////////////////////////////////////////////////////////////////////

    private void panelVigilanteWake()
    {
        music.PlayNightTown();

        if (lynchedSurvivor)
        {
            jumpImmediate(Panel.AMNESIAC_WAKE);
            return;
        }

        if (!roleExists(VIGILANTE) || amnesiacRole.ID == VIGILANTE.ID)
        {
            jumpImmediate(Panel.VETERAN_WAKE);
            return;
        }

        if (night > 1 && roleCountAlive(VIGILANTE) == 0)
        {
            if (findPlayer(VIGILANTE.ID).getIsCleaned())
            {
                cleanedRolePlaying = "Vigilante";
                jumpImmediate(Panel.CLEANED_ROLE);
                nextPanel = Panel.VETERAN_WAKE;
                return;
            }
            else
            {
                jumpImmediate(Panel.VETERAN_WAKE);
                return;
            }
        }

        speak.SetActive(true);
        useMessage("Vigilante " + LANG.wake[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Vigilante" + LANG.toWakeUp[lang];
        nextPanel = Panel.VIGILANTE_WHO;
    }

    private void panelVigilanteWho()
    {
        if (night > 1)
        {
            jumpImmediate(Panel.VIGILANTE_ASK);
            return;
        }

        useTitle(LANG.whoIs[lang] + " Vigilante?", GD.townColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Vigilante";
        nextPanel = Panel.VIGILANTE_ASK;
    }

    private void panelVigilanteAsk()
    {
        useTitle(LANG.shoot[lang] + "?", GD.townColor);
        useAskAbility(aVigilante, maxVigilante);
        textHelp.text = amnesiacRole.ID == VIGILANTE.ID ? LANG.helpPanelAskAmne[lang] : LANG.helpPanelAsk[lang];
        nextPanel = Panel.VIGILANTE_SLEEP;                   //If doesn't use ability
    }

    private void panelVigilante()
    {
        useTitle(LANG.shoot[lang], GD.townColor);
        usePlayerListByPlayer(1, 1, VIGILANTE);
        textHelp.text = LANG.helpPanelLimitedAbility[lang];
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.VIGILANTE_SLEEP;
    }

    private void panelVigilanteSleep()
    {
        speak.SetActive(true);
        useMessage("Vigilante " + LANG.sleep[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Vigilante" + LANG.toSleep[lang];
        nextPanel = Panel.VETERAN_WAKE;
    }

    private void panelVeteranWake()
    {
        if (!roleExists(VETERAN) || amnesiacRole.ID == VETERAN.ID)
        {
            jumpImmediate(Panel.INVESTIGATOR_WAKE);
            return;
        }

        if (night > 1 && roleCountAlive(VETERAN) == 0)
        {
            if (findPlayer(VETERAN.ID).getIsCleaned())
            {
                cleanedRolePlaying = "Veteran";
                jumpImmediate(Panel.CLEANED_ROLE);
                nextPanel = Panel.INVESTIGATOR_WAKE;
                return;
            }
            else
            {
                jumpImmediate(Panel.INVESTIGATOR_WAKE);
                return;
            }
        }

        speak.SetActive(true);
        useMessage("Veteran " + LANG.wake[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Veteran" + LANG.toWakeUp[lang];
        nextPanel = Panel.VETERAN_WHO;
    }

    private void panelVeteranWho()
    {
        if (night > 1)
        {
            jumpImmediate(Panel.VETERAN_ASK);
            return;
        }

        useTitle(LANG.whoIs[lang] + " Veteran?", GD.townColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Veteran";
        nextPanel = Panel.VETERAN_ASK;
    }

    private void panelVeteranAsk()
    {
        useTitle(LANG.alert[lang] + "?", GD.townColor);
        useAskAbility(aVeteran, maxVeteran);
        textHelp.text = amnesiacRole.ID == VETERAN.ID ? LANG.helpPanelAskAmne[lang] : LANG.helpPanelAsk[lang];
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.VETERAN_SLEEP;
    }

    private void panelVeteranSleep()
    {
        speak.SetActive(true);
        useMessage("Veteran " + LANG.sleep[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Veteran" + LANG.toSleep[lang];
        nextPanel = Panel.INVESTIGATOR_WAKE;
    }

    private void panelInvestigatorWake()
    {
        if (!roleExists(INVESTIGATOR) || amnesiacRole.ID == INVESTIGATOR.ID)
        {
            jumpImmediate(Panel.BODYGUARD_WAKE);
            return;
        }

        if (night > 1 && roleCountAlive(INVESTIGATOR) == 0)
        {
            if (findPlayer(INVESTIGATOR.ID).getIsCleaned())
            {
                cleanedRolePlaying = "Investigator";
                jumpImmediate(Panel.CLEANED_ROLE);
                nextPanel = Panel.BODYGUARD_WAKE;
                return;
            }
            else
            {
                jumpImmediate(Panel.BODYGUARD_WAKE);
                return;
            }
        }

        speak.SetActive(true);
        useMessage("Investigator " + LANG.wake[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Investigator" + LANG.toWakeUp[lang];
        nextPanel = Panel.INVESTIGATOR_WHO;
    }

    private void panelInvestigatorWho()
    {
        if (night > 1)
        {
            jumpImmediate(Panel.INVESTIGATOR_ASK);
            return;
        }

        useTitle(LANG.whoIs[lang] + " Investigator?", GD.townColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Investigator";
        nextPanel = Panel.INVESTIGATOR_ASK;
    }

    private void panelInvestigatorAsk()
    {
        useTitle(LANG.investigate[lang] + "?", GD.townColor);
        useAskAbility(aInvestigator, maxInvestigator);
        textHelp.text = amnesiacRole.ID == INVESTIGATOR.ID ? LANG.helpPanelAskAmne[lang] : LANG.helpPanelAsk[lang];
        nextPanel = Panel.INVESTIGATOR_SLEEP;                   //If doesn't use ability
    }

    private void panelInvestigator()
    {
        useTitle(LANG.investigate[lang], GD.townColor);
        usePlayerListByPlayer(1, 1, INVESTIGATOR);
        textHelp.text = LANG.helpPanelLimitedAbility[lang];
        nextPanel = Panel.INVESTIGATOR_SHOW;
    }

    private void panelInvestigatorShow()
    {
        message.gameObject.SetActive(true);
        if (night == 1)
        {
            card.SetActive(true);
            textHelp.text = LANG.helpPanelShowCard[lang];
        }
        else
        {
            show.SetActive(true);
            textHelp.text = LANG.helpPanelShow[lang];
        }
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.INVESTIGATOR_SLEEP;
    }

    private void panelInvestigatorSleep()
    {
        speak.SetActive(true);
        useMessage("Investigator " + LANG.sleep[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Investigator" + LANG.toSleep[lang];
        nextPanel = Panel.BODYGUARD_WAKE;
    }

    private void panelBodyguardWake()
    {
        if (!roleExists(BODYGUARD) || amnesiacRole.ID == BODYGUARD.ID)
        {
            jumpImmediate(Panel.DOCTOR_WAKE);
            return;
        }

        if (night > 1 && roleCountAlive(BODYGUARD) == 0)
        {
            if (findPlayer(BODYGUARD.ID).getIsCleaned())
            {
                cleanedRolePlaying = "Bodyguard";
                jumpImmediate(Panel.CLEANED_ROLE);
                nextPanel = Panel.DOCTOR_WAKE;
                return;
            }
            else
            {
                jumpImmediate(Panel.DOCTOR_WAKE);
                return;
            }
        }

        speak.SetActive(true);
        useMessage("Bodyguard " + LANG.wake[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Bodyguard" + LANG.toWakeUp[lang];
        nextPanel = Panel.BODYGUARD_WHO;
    }

    private void panelBodyguardWho()
    {
        if (night > 1)
        {
            jumpImmediate(Panel.BODYGUARD);
            return;
        }

        useTitle(LANG.whoIs[lang] + " Bodyguard?", GD.townColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Bodyguard";
        nextPanel = Panel.BODYGUARD;
    }

    private void panelBodyguard()
    {
        useTitle(LANG.guard[lang], GD.townColor);
        usePlayerListByPlayer(1, 1, BODYGUARD);
        textHelp.text = amnesiacRole.ID == BODYGUARD.ID ? LANG.helpPanelAmnesiacAbility[lang] : LANG.helpPanelBodyguard[lang];
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.BODYGUARD_SLEEP;
    }

    private void panelBodyguardSleep()
    {
        speak.SetActive(true);
        useMessage("Bodyguard " + LANG.sleep[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Bodyguard" + LANG.toSleep[lang];
        nextPanel = Panel.DOCTOR_WAKE;
    }

    private void panelDoctorWake()
    {
        if (!roleExists(DOCTOR) || amnesiacRole.ID == DOCTOR.ID)
        {
            jumpImmediate(Panel.POLITICIAN_WHO);
            return;
        }

        if (night > 1 && roleCountAlive(DOCTOR) == 0)
        {
            if (findPlayer(DOCTOR.ID).getIsCleaned())
            {
                cleanedRolePlaying = "Doctor";
                jumpImmediate(Panel.CLEANED_ROLE);
                nextPanel = Panel.POLITICIAN_WHO;
                return;
            }
            else
            {
                jumpImmediate(Panel.POLITICIAN_WHO);
                return;
            }
        }

        speak.SetActive(true);
        useMessage("Doctor " + LANG.wake[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Doctor" + LANG.toWakeUp[lang];
        nextPanel = Panel.DOCTOR_WHO;
    }

    private void panelDoctorWho()
    {
        if (night > 1)
        {
            jumpImmediate(SF.doctorCanSelfHeal ? Panel.DOCTOR_ASK : Panel.DOCTOR); // Variant - doctorCanSelfHeal
            return;
        }

        useTitle(LANG.whoIs[lang] + " Doctor?", GD.townColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Doctor";
        nextPanel = SF.doctorCanSelfHeal ? Panel.DOCTOR_ASK : Panel.DOCTOR; // Variant - doctorCanSelfHeal;
    }

    private void panelDoctorAsk()
    {
        useTitle(LANG.selfHeal[lang] + "?", GD.townColor);
        useAskAbility(aDoctor, 1);
        textHelp.text = amnesiacRole.ID == DOCTOR.ID ? LANG.helpPanelAskAmne[lang] : LANG.helpPanelAskDoctor[lang];
        nextPanel = Panel.DOCTOR;                   //If doesn't use ability
    }

    private void panelDoctor()
    {
        useTitle(LANG.heal[lang], GD.townColor);
        usePlayerListByPlayer(0, 1, DOCTOR);
        textHelp.text = amnesiacRole.ID == DOCTOR.ID ? LANG.helpPanelAmnesiacAbility[lang] : LANG.helpPanelOptionalAbility[lang];
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.DOCTOR_SLEEP;
    }

    private void panelDoctorSleep()
    {
        speak.SetActive(true);
        useMessage("Doctor " + LANG.sleep[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Doctor" + LANG.toSleep[lang];
        nextPanel = Panel.POLITICIAN_WHO;
    }

    private void panelPoliticianWho()
    {
        if (!roleExists(POLITICIAN) || night > 1)
        {
            jumpImmediate(Panel.SHERIFF_WAKE);
            return;
        }

        check.SetActive(true);
        useTitle(LANG.whoIs[lang] + " Politician?", GD.townColor);
        usePlayerListSetRole(countRole(POLITICIAN));
        textHelp.text = LANG.pickWhoIsPolitician[lang];
        nextPanel = Panel.SHERIFF_WAKE;
    }

    private void panelSheriffWake()
    {
        if (!roleExists(SHERIFF) || amnesiacRole.ID == SHERIFF.ID)
        {
            jumpImmediate(Panel.MEDIUM_WAKE);
            return;
        }

        if (night > 1 && roleCountAlive(SHERIFF) == 0)
        {
            if (findPlayer(SHERIFF.ID).getIsCleaned())
            {
                cleanedRolePlaying = "Sheriff";
                jumpImmediate(Panel.CLEANED_ROLE);
                nextPanel = Panel.DEPUTY_WAKE;
                return;
            }
            else
            {
                jumpImmediate(Panel.DEPUTY_WAKE);
                return;
            }
        }

        speak.SetActive(true);
        useMessage("Sheriff " + LANG.wake[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Sheriff" + LANG.toWakeUp[lang];
        nextPanel = Panel.SHERIFF_WHO;
    }

    private void panelSheriffWho()
    {
        if (night > 1)
        {
            jumpImmediate(Panel.SHERIFF);
            return;
        }

        useTitle(LANG.whoIs[lang] + " Sheriff?", GD.townColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Sheriff";
        nextPanel = Panel.SHERIFF;
    }

    private void panelSheriff()
    {
        useTitle(LANG.analyse[lang], GD.townColor);
        usePlayerListByPlayer(0, 1, SHERIFF);
        textHelp.text = amnesiacRole.ID == SHERIFF.ID ? LANG.helpPanelAmnesiacAbility[lang] : LANG.helpPanelOptionalAbility[lang];
        nextPanel = Panel.SHERIFF_SHOW;
    }

    private void panelSheriffShow()
    {
        if (selectedPlayersIndexes.Count == 0)
        {
            jumpImmediate(amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.SHERIFF_SLEEP);
            return;
        }
        message.gameObject.SetActive(true);
        show.SetActive(true);
        textHelp.text = LANG.helpPanelShow[lang];
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.SHERIFF_SLEEP;
    }

    private void panelSheriffSleep()
    {
        speak.SetActive(true);
        useMessage("Sheriff " + LANG.sleep[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Sheriff" + LANG.toSleep[lang];
        nextPanel = night == 1 && roleExists(DEPUTY) ? Panel.DEPUTY_WHO : Panel.MEDIUM_WAKE;
    }

    private void panelDeputyWake()
    {
        if (!roleExists(DEPUTY) || amnesiacRole.ID == DEPUTY.ID)
        {
            jumpImmediate(Panel.MEDIUM_WAKE);
            return;
        }

        if (night > 1 && roleCountAlive(DEPUTY) == 0)
        {
            if (findPlayer(DEPUTY.ID).getIsCleaned())
            {
                cleanedRolePlaying = "Deputy";
                jumpImmediate(Panel.CLEANED_ROLE);
                nextPanel = Panel.MEDIUM_WAKE;
                return;
            }
            else
            {
                jumpImmediate(Panel.MEDIUM_WAKE);
                return;
            }
        }

        speak.SetActive(true);
        useMessage("Deputy " + LANG.wake[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Deputy" + LANG.toWakeUp[lang];
        nextPanel = Panel.DEPUTY_WHO;
    }

    private void panelDeputyWho()
    {
        if (night > 1)
        {
            jumpImmediate(Panel.DEPUTY);
            return;
        }

        check.SetActive(true);
        useTitle(LANG.whoIs[lang] + " Deputy?", GD.townColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIsDeputy[lang];
        nextPanel = Panel.MEDIUM_WAKE;
    }

    private void panelDeputy()
    {
        useTitle(LANG.analyse[lang], GD.townColor);
        usePlayerListByPlayer(0, 1, DEPUTY);
        textHelp.text = amnesiacRole.ID == DEPUTY.ID ? LANG.helpPanelAmnesiacAbility[lang] : LANG.helpPanelOptionalAbility[lang];
        nextPanel = Panel.DEPUTY_SHOW;
    }

    private void panelDeputyShow()
    {
        if (selectedPlayersIndexes.Count == 0)
        {
            jumpImmediate(amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.DEPUTY_SLEEP);
            return;
        }
        message.gameObject.SetActive(true);
        show.SetActive(true);
        textHelp.text = LANG.helpPanelShow[lang];
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.DEPUTY_SLEEP;
    }

    private void panelDeputySleep()
    {
        speak.SetActive(true);
        useMessage("Deputy " + LANG.sleep[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Deputy" + LANG.toSleep[lang];
        nextPanel = Panel.MEDIUM_WAKE;
    }

    private void panelMediumWake()
    {
        if (!roleExists(MEDIUM) || night == 1 || !seance || amnesiacRole.ID == MEDIUM.ID)
        {
            jumpImmediate(Panel.EXECUTIONER_WAKE);
            return;
        }

        if (roleCountAlive(MEDIUM) == 0)
        {
            if (findPlayer(MEDIUM.ID).getIsCleaned())
            {
                cleanedRolePlaying = "Medium";
                jumpImmediate(Panel.CLEANED_ROLE);
                nextPanel = Panel.EXECUTIONER_WAKE;
                return;
            }
            else
            {
                jumpImmediate(Panel.EXECUTIONER_WAKE);
                return;
            }
        }

        speak.SetActive(true);
        useMessage("Medium " + LANG.wake[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Medium" + LANG.toWakeUp[lang];
        nextPanel = Panel.MEDIUM;
    }

    private void panelMedium()
    {
        useTitle(LANG.seance[lang], GD.townColor);
        usePlayerListByMedium();
        textHelp.text = amnesiacRole.ID == MEDIUM.ID ? LANG.helpPanelAmnesiacAbility[lang] : LANG.helpPanelOptionalAbility[lang];
        nextPanel = Panel.MEDIUM_SHOW_ROLE;
    }

    private void panelMediumShowRole()
    {
        if (selectedPlayersIndexes.Count == 0)
        {
            jumpImmediate(amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.MEDIUM_SLEEP);
            return;
        }

        Player p = players[selectedPlayersIndexes[0]];  //Seanced Player
        useTitle(p.getPName() + LANG.strIs[lang], Color.white);
        useMessage(p.getRole().text, p.getColor());
        show.SetActive(true);
        textHelp.text = LANG.helpPanelShow[lang];
        nextPanel = Panel.MEDIUM_SHOW;
    }

    private void panelMediumShow()
    {
        if (selectedPlayersIndexes.Count == 0)
        {
            jumpImmediate(amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.MEDIUM_SLEEP);
            return;
        }

        for (int i = 0; i < pickedPossibleKillers.Count; ++i)
        {
            Item it = playersItems[pickedPossibleKillers[i].getIndex()];
            it.SetAsList();
            it.setTextColor(Color.white);
            it.gameObject.SetActive(true);
        }

        useTitle(LANG.suspects[lang], GD.mafiaColor);
        scrollerPlayerList.SetActive(true);
        show.SetActive(true);
        textHelp.text = LANG.helpPanelShow[lang];
        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.MEDIUM_SLEEP;
    }

    private void panelMediumSleep()
    {
        speak.SetActive(true);
        useMessage("Medium " + LANG.sleep[lang], GD.townColor);
        textHelp.text = LANG.tell[lang] + "Medium" + LANG.toSleep[lang];
        nextPanel = Panel.EXECUTIONER_WAKE;
    }

    private void panelExecutionerWake()
    {
        if (!roleExists(EXECUTIONER) || night > 1)
        {
            jumpImmediate(Panel.AMNESIAC_WAKE);
            return;
        }

        speak.SetActive(true);
        useMessage("Executioner " + LANG.wake[lang], GD.executionerColor);
        textHelp.text = LANG.tell[lang] + "Executioner" + LANG.toWakeUp[lang];
        nextPanel = Panel.EXECUTIONER_WHO;
    }

    private void panelExecutionerWho()
    {
        useTitle(LANG.whoIs[lang] + " Executioner?", GD.executionerColor);
        usePlayerListSetRole(1);
        textHelp.text = LANG.pickWhoIs[lang] + "Executioner";
        nextPanel = Panel.EXECUTIONER;
    }

    private void panelExecutioner()
    {
        useTitle(LANG.target[lang], GD.executionerColor);

        // Variant - executionerPickTarget
        if (SF.executionerPickTarget)
        {
            usePlayerListByPlayer(1, 1, EXECUTIONER);
            textHelp.text = LANG.helpPanelExecutioner[lang];
        }
        else
        {
            do
            {
                executionerTarget = players[Random.Range(0, nPlayers)];
            } while (executionerTarget.getRole().ID == EXECUTIONER.ID);

            useMessage(executionerTarget.getPName(), Color.white);
            textHelp.text = LANG.helpPanelExecutionerVariant[lang];
        }

        nextPanel = amnesiacTurn ? Panel.AMNESIAC_SLEEP : Panel.EXECUTIONER_SLEEP;
    }

    private void panelExecutionerSleep()
    {
        speak.SetActive(true);
        useMessage("Executioner " + LANG.sleep[lang], GD.executionerColor);
        textHelp.text = LANG.tell[lang] + "Executioner" + LANG.toSleep[lang];
        nextPanel = Panel.AMNESIAC_WAKE;
    }

    private void panelAmnesiacWake()
    {
        if (!existsAmnesiac || night < 3)
        {
            jumpImmediate(Panel.ROLELESS_PLAYERS);
            return;
        }

        speak.SetActive(true);
        useMessage("Amnesiac " + LANG.wake[lang], GD.amnesiacColor);
        textHelp.text = LANG.tell[lang] + "Amnesiac" + LANG.toWakeUp[lang];
        nextPanel = Panel.AMNESIAC;
    }

    private void panelAmnesiac()
    {
        Color c = GD.GetRoleColor(amnesiacRole.ID);

        if (night == 3) // Getting Role
        {
            Player p = findPlayer(AMNESIAC.ID);

            p.setRole(amnesiacRole);
            if (amnesiacRole.faction == Faction.MAFIA)
                ++nAliveMafia;

            for (int i = 0; i < playingRoles.Length; ++i)
            {
                if (playingRoles[i].ID == AMNESIAC.ID)
                {
                    playingRoles[i] = amnesiacRole;
                    break;
                }
            }

            card.SetActive(true);
            useTitle(p.getPName() + " " + LANG.nowIs[lang], c);
            useMessage(amnesiacRole.text, c);
            textHelp.text = LANG.helpPanelAmnesiac[lang];

            if (amnesiacRole.ID == EXECUTIONER.ID)
            {
                amnesiacTurn = true;
                nextPanel = Panel.EXECUTIONER;
            }
            else
                nextPanel = Panel.AMNESIAC_SLEEP;
        }
        else
        {
            switch (amnesiacRole.ID)
            {
                case RoleEnum.MAYOR:
                case RoleEnum.BLACKMAILER:
                case RoleEnum.CONSIGLIERE:
                case RoleEnum.GODFATHER:
                case RoleEnum.JANITOR:
                case RoleEnum.MAFIOSO:
                case RoleEnum.EXECUTIONER:
                case RoleEnum.JESTER:
                    speak.SetActive(true);
                    useMessage(AMNESIAC.text, GD.GetRoleColor(AMNESIAC.ID));
                    textHelp.text = LANG.helpPanelAmnesiacSimpleRole[lang];
                    nextPanel = Panel.AMNESIAC_SLEEP;
                    break;
                case RoleEnum.BODYGUARD:
                    if (lynchedSurvivor)
                        nextPanel = Panel.AMNESIAC_SLEEP;
                    else
                    {
                        amnesiacTurn = true;
                        jumpImmediate(Panel.BODYGUARD);
                    }
                    break;
                case RoleEnum.DEPUTY:
                    if (lynchedSurvivor)
                        nextPanel = Panel.AMNESIAC_SLEEP;
                    else
                    {
                        amnesiacTurn = true;
                        jumpImmediate(Panel.DEPUTY);
                    }
                    break;
                case RoleEnum.DOCTOR:
                    if (lynchedSurvivor)
                        nextPanel = Panel.AMNESIAC_SLEEP;
                    else
                    {
                        amnesiacTurn = true;
                        jumpImmediate(Panel.DOCTOR);
                    }
                    break;
                case RoleEnum.INVESTIGATOR:
                    if (lynchedSurvivor)
                        nextPanel = Panel.AMNESIAC_SLEEP;
                    else
                    {
                        amnesiacTurn = true;
                        jumpImmediate(Panel.INVESTIGATOR_ASK);
                    }
                    break;
                case RoleEnum.MEDIUM:
                    if (lynchedSurvivor)
                        nextPanel = Panel.AMNESIAC_SLEEP;
                    else
                    {
                        amnesiacTurn = true;
                        jumpImmediate(Panel.MEDIUM);
                    }
                    break;
                case RoleEnum.SHERIFF:
                    if (lynchedSurvivor)
                        nextPanel = Panel.AMNESIAC_SLEEP;
                    else
                    {
                        amnesiacTurn = true;
                        jumpImmediate(Panel.SHERIFF);
                    }
                    break;
                case RoleEnum.VETERAN:
                    if (lynchedSurvivor)
                        nextPanel = Panel.AMNESIAC_SLEEP;
                    else
                    {
                        amnesiacTurn = true;
                        jumpImmediate(Panel.VETERAN_ASK);
                    }
                    break;
                case RoleEnum.VIGILANTE:
                    if (lynchedSurvivor)
                        nextPanel = Panel.AMNESIAC_SLEEP;
                    else
                    {
                        amnesiacTurn = true;
                        jumpImmediate(Panel.VIGILANTE_ASK);
                    }
                    break;
                case RoleEnum.SERIAL_KILLER:
                    amnesiacTurn = true;
                    jumpImmediate(Panel.SERIAL_KILLER);
                    break;
                case RoleEnum.WEREWOLF:
                    amnesiacTurn = true;
                    jumpImmediate(Panel.WEREWOLF);
                    break;
                case RoleEnum.WITCH:
                    amnesiacTurn = true;
                    jumpImmediate(Panel.WITCH);
                    break;
            }
        }
    }

    private void panelAmnesiacSleep()
    {
        amnesiacTurn = false;
        speak.SetActive(true);
        useMessage("Amnesiac " + LANG.sleep[lang], GD.amnesiacColor);
        textHelp.text = LANG.tell[lang] + "Amnesiac" + LANG.toSleep[lang];
        nextPanel = Panel.ROLELESS_PLAYERS;
    }

    private void panelRolelessPlayers()
    {
        if (night > 1)
        {
            jumpImmediate(Panel.EXECUTIONER_JESTER);
            return;
        }

        int nRolesNotSet = 0;
        Role notSet = new Role();

        //Check if there are only one role to set
        for (int i = 0; i < GD.GetNRoles(); ++i)
        {
            Role r = GD.GetRole(i);
            if (countRole(r) > roleCountAlive(r))
            {
                notSet = r;
                ++nRolesNotSet;
            }
        }

        if (nRolesNotSet == 1)
        {
            for (int i = 0; i < nPlayers; i++)
            {
                Player p = players[i];
                if (!p.getIsRoleSet())
                    p.setStartingRole(notSet);
            }
            jumpImmediate(Panel.EXECUTIONER_JESTER);
            return;
        }

        roleless = null;

        for (int i = 0; i < nPlayers; ++i)
        {
            if (!players[i].getIsRoleSet())
            {
                roleless = players[i];
                break;
            }
        }

        if (roleless != null)
        {
            check.SetActive(true);
            useTitle(LANG.whatIs[lang] + " " + roleless.getPName() + "?", Color.white);
            textHelp.text = LANG.helpPanelRoleless[lang];
            useRoleListRoleless();
            return;
        }

    }

    private void panelExecutionerJester()
    {
        if (!roleExists(EXECUTIONER) || !toDie.Contains(executionerTarget) || executionerTarget.getRole().nightImmune)
        {
            jumpImmediate(Panel.EVERYONE_WAKE);
            return;
        }

        card.SetActive(true);
        useTitle(findPlayer(RoleEnum.EXECUTIONER).getPName() + " " + LANG.nowIs[lang], GD.jesterColor);
        useMessage("Jester", GD.jesterColor);
        findPlayer(RoleEnum.EXECUTIONER).setRole(JESTER);
        textHelp.text = LANG.helpPanelExecutionerJester[lang];
        nextPanel = Panel.EVERYONE_WAKE;
    }

    private void panelEveryoneWake()
    {
        music.PlayDayTown();
        speak.SetActive(true);
        useMessage(LANG.everyoneWake[lang], Color.white);
        textHelp.text = LANG.tell[lang] + LANG.everyone[lang] + LANG.toWakeUp[lang];
        nextPanel = Panel.WHO_DIED;
    }

    private void panelWhoDied()
    {
        bool dead = false;
        bool cleaned = false;
        Player p;

        // Bodyguard
        if (bodyguarded != null)
        {
            p = findPlayer(BODYGUARD.ID);

            // Variant - DoctorCanSaveBodyguard
            if (!SF.doctorCanSaveBodyguard && p == healed)
                healed = null;

            if (toDie.Contains(bodyguarded))
            {
                List<Player> killers = new List<Player>(bodyguarded.getKillers());

                if (killers.Count == 1)
                {
                    removeToDie(bodyguarded);
                    addToDie(killers[0]);
                    killers[0].addKiller(p);
                }
                else
                {
                    Player guardedKiller = killers[Random.Range(0, killers.Count)];
                    bodyguarded.removeKiller(guardedKiller);
                    addToDie(guardedKiller);
                    guardedKiller.addKiller(p);
                }

                addToDie(p);
            }
        }

        // Veteran
        if (alert)
        {
            p = findPlayer(VETERAN.ID);

            removeToDie(p);
            p.clearKillers();
            List<Player> visitors = p.getVisitors();

            foreach (Player pl in visitors)
            {
                pl.addKiller(p);
                addToDie(pl);
            }
        }

        // Variant - witchDieCursedDie
        if (SF.witchDieCursedDie && roleCountAlive(WITCH) > 0)
        {
            Player witch = findPlayer(WITCH.ID);

            if (toDie.Contains(witch))
            {
                for (int i = 0; i < nPlayers; i++)
                {
                    p = players[i];
                    if (p.getIsCursed())
                    {
                        p.addKiller(witch);
                        toDie.Add(p);
                    }
                }
            }
        }

        // Killing
        for (int i = 0; i < nPlayers; i++)
        {
            p = players[i];

            if (toDie.Contains(p) && !p.getRole().nightImmune && p != healed)
            {
                Item it = playersItems[i];
                dead = true;
                killPlayer(p);

                // Janitor Clean
                if (roleCountAlive(JANITOR) > 0 && aJanitor > 0)
                {
                    cleaned = false;
                    foreach (Player killer in p.getKillers())
                    {
                        if (killer.getFaction() == Faction.MAFIA)
                        {
                            cleaned = true;
                            p.setIsCleaned(true);
                            break;
                        }
                    }
                }

                if (cleaned)
                {
                    it.setText1(LANG.cleaned[lang]);
                    it.setTextColor(Color.white);
                    --aJanitor;
                }
                else
                {
                    it.setText1(p.getRole().text);
                    it.setTextColor(p.getColor());
                }

                it.SetText2(p.getPName());
                it.SetAsList();
                it.setDoubleText();
                it.gameObject.SetActive(true);
            }
        }

        if (dead)
        {
            useTitle(LANG.deathList[lang], GD.mafiaColor);
            scrollerPlayerList.SetActive(true);
            textHelp.text = LANG.helpPanelWhoDied[lang];
        }
        else
        {
            useMessage(LANG.noOneDied[lang], GD.townColor);
            textHelp.text = LANG.helpPanelNoOneDied[lang];
        }

        toDie.Clear();

        speak.SetActive(true);
        nextPanel = Panel.DISCUSSION;
        checkWin();
    }

    private void panelDiscussion()
    {
        speak.gameObject.SetActive(true);
        useMessage(LANG.discussion[lang], Color.white);

        if (blackmailed != null)
            useTitle(blackmailed.getPName() + " " + (SF.blackmailedCanVote ? LANG.cannotTalk[lang] : LANG.cannotTalkVariant[lang]), GD.mafiaColor); // Variant - blackmailedCanVote

        textHelp.text = SF.blackmailedCanVote ? LANG.helpPanelDiscussion[lang] : LANG.helpPanelDiscussionVariant[lang]; // Variant - blackmailedCanVote
        nextPanel = Panel.LYNCH;
    }

    private void panelLynch()
    {
        lynchedSurvivor = false;
        useTitle(LANG.lynch[lang], Color.white);
        usePlayerListEveryone();
        if (roleCountAlive(MAYOR) == 1 || roleCountAlive(PEACEFUL_TOWNIE) == 1 || roleCountAlive(SPITEFUL_TOWNIE) == 1)
        {
            bDayOptions.gameObject.SetActive(true);
            updateOptions();
        }
        textHelp.text = LANG.helpPanelLynch[lang];
        nextPanel = Panel.EVERYONE_SLEEP; // In case no one is lynched
    }

    private void panelShowLynched()
    {
        killPlayer(lynched);
        useMessage(lynched.getPName() + LANG.was[lang] + lynched.getRole().text, lynched.getColor());
        speak.SetActive(true);
        textHelp.text = LANG.helpPanelShowPublicDeath[lang];
        nextPanel = Panel.EVERYONE_SLEEP;

        // Variant - witchDieCursedDie
        if (SF.witchDieCursedDie && lynched.getRole().ID == WITCH.ID)
        {
            Player p;
            Player witch = findPlayer(WITCH.ID);

            for (int i = 0; i < nPlayers; i++)
            {
                p = players[i];
                if (p.getIsCursed())
                {
                    p.addKiller(witch);
                    toDie.Add(p);
                }
            }
        }

        if (lynched == executionerTarget)
            findPlayer(EXECUTIONER.ID).setIsWinner(true);

        if (lynched.getRole().ID == JESTER.ID)
            lynched.setIsWinner(true);
        else if (lynched.getRole().ID == SURVIVOR.ID && SF.survivorLynchedDisableTownAbilities)
        {
            // Variant - survivorLynchedDisableTownAbilities
            lynchedSurvivor = true;
        }

        // Variant - executionerOnlyWinner
        if (roleCountAlive(EXECUTIONER) == 1 && findPlayer(EXECUTIONER.ID).getIsWinner() && SF.executionerOnlyWinner)
        {
            nextPanel = Panel.EXECUTIONER_WIN;
            return;
        }

        // Variant - jesterOnlyWinner
        if (lynched.getRole().ID == JESTER.ID)
        {
            nextPanel = SF.jesterOnlyWinner ? Panel.JESTER_WIN : Panel.JESTER;
            return;
        }

        checkWin();
    }

    private void panelJester()
    {
        useTitle(LANG.revenge[lang], GD.jesterColor);
        usePlayerListByPlayer(1, 1, JESTER);
        textHelp.text = LANG.helpPanelJester[lang];
        nextPanel = Panel.JESTER_SHOW;
    }

    private void panelJesterShow()
    {
        Player p = toDie[0];
        killPlayer(p);
        useMessage(p.getPName() + LANG.was[lang] + p.getRole().text, p.getColor());
        toDie.Clear();

        speak.SetActive(true);
        textHelp.text = LANG.helpPanelShowPublicDeath[lang];
        nextPanel = Panel.EVERYONE_SLEEP;
        checkWin();
    }

    private void panelTownWin()
    {
        music.PlayWinTown();

        for (int i = 0; i < nPlayers; i++)
        {
            if (players[i].getFaction() == Faction.TOWN)
                players[i].setIsWinner(true);
        }

        speak.SetActive(true);
        useMessage("Town " + LANG.winsHe[lang], GD.townColor);
        textHelp.text = LANG.helpPanelFactionWin[lang];
        nextPanel = Panel.WINNERS;
    }

    private void panelMafiaWin()
    {
        music.PlayMafia();

        for (int i = 0; i < nPlayers; i++)
        {
            if (players[i].getFaction() == Faction.MAFIA)
                players[i].setIsWinner(true);
        }

        speak.SetActive(true);
        useMessage("Mafia " + LANG.winsHe[lang], GD.mafiaColor);
        textHelp.text = LANG.helpPanelFactionWin[lang];
        nextPanel = Panel.WINNERS;
    }

    private void panelSerialKillerWin()
    {
        music.PlaySerialKiller();

        findPlayer(RoleEnum.SERIAL_KILLER).setIsWinner(true);

        speak.SetActive(true);
        useMessage("Serial Killer " + LANG.winsHe[lang], GD.serialKillerColor);
        textHelp.text = LANG.helpPanelFactionWin[lang];
        nextPanel = Panel.WINNERS;
    }

    private void panelWerewolfWin()
    {
        music.PlayWerewolf();

        findPlayer(RoleEnum.WEREWOLF).setIsWinner(true);

        speak.SetActive(true);
        useMessage("Werewolf " + LANG.winsHe[lang], GD.werewolfColor);
        textHelp.text = LANG.helpPanelFactionWin[lang];
        nextPanel = Panel.WINNERS;
    }

    private void panelWitchWin()
    {
        music.PlayWitch();

        findPlayer(RoleEnum.WITCH).setIsWinner(true);

        speak.SetActive(true);
        useMessage("Witch " + LANG.winsHe[lang], GD.witchColor);
        textHelp.text = LANG.helpPanelFactionWin[lang];
        nextPanel = Panel.WINNERS;
    }

    private void panelExecutionerWin()
    {
        findPlayer(RoleEnum.EXECUTIONER).setIsWinner(true);

        speak.SetActive(true);
        useMessage("Executioner " + LANG.winsHe[lang], GD.executionerColor);
        textHelp.text = LANG.helpPanelFactionWin[lang];
        nextPanel = Panel.WINNERS;
    }

    private void panelJesterWin()
    {
        lynched.setIsWinner(true);

        speak.SetActive(true);
        useMessage("Jester " + LANG.winsHe[lang], GD.jesterColor);
        textHelp.text = LANG.helpPanelFactionWin[lang];
        nextPanel = Panel.WINNERS;
    }

    private void panelDraw()
    {
        speak.SetActive(true);
        useMessage(LANG.draw[lang], Color.white);
        buttonNext.GetComponentInChildren<Text>().text = LANG.end[lang];
        textHelp.text = LANG.helpPanelDraw[lang];
        nextPanel = Panel.END;
    }

    private void panelWinners()
    {
        Item it;
        Player p;

        if (night < 3 && roleCountAlive(AMNESIAC) > 0)
            findPlayer(AMNESIAC.ID).setIsWinner(true);

        for (int i = 0; i < nPlayers; i++)
        {
            if (players[i].getIsWinner())
            {
                it = playersItems[i];
                p = players[i];
                it.setText1(p.getStartingRole().text);
                it.SetText2(p.getPName());
                it.setTextColor(p.getStartingColor());
                it.SetAsList();
                it.setDoubleText();
                it.gameObject.SetActive(true);
            }
        }

        useTitle(LANG.winners[lang], Color.white);
        scrollerPlayerList.SetActive(true);
        buttonNext.GetComponentInChildren<Text>().text = LANG.end[lang];
        textHelp.text = LANG.helpPanelWinners[lang];
        nextPanel = Panel.END;
    }

    private void panelEnd()
    {
        Player p = null;
        Person pe = null;
        RoleEnum r = RoleEnum.TOWNIE;
        int role;

        for (int i = 0; i < nPlayers; i++)
        {
            p = players[i];
            pe = playingPersons[i];
            r = p.getStartingRole().ID;
            role = (int)r;

            pe.incrementGameByRole(role);
            SF.incrementGamesByRole(role);

            if (p.getIsWinner())
                pe.incrementWinsByRole(role);
        }

        SF.SavePlayerData();
        SF.incrementTotalGames();
        SF.saveGameData();
        SceneManager.LoadScene("Menu");
    }

    private void updateOptions()
    {
        labelMayorRevealed.text = string.Empty;
        textGuilty.text = string.Empty;
        textInnocent.text = string.Empty;

        if (roleCountAlive(MAYOR) == 1)
        {
            if (mayorRevealed)
            {
                bMayorReveal.gameObject.SetActive(false);
                labelMayorRevealed.gameObject.SetActive(true);
                UpdateAMayor();
                labelMayorRevealed.text = GD.getRole(MAYOR.ID).text + " " + aMayor + " " + LANG.votes[lang];
            }
            else
            {
                bMayorReveal.gameObject.SetActive(true);
            }

        }

        if (roleCountAlive(SPITEFUL_TOWNIE) == 1)
            textGuilty.text = findPlayer(SPITEFUL_TOWNIE.ID).getPName() + LANG.votesGuilty[lang];
        if (roleCountAlive(PEACEFUL_TOWNIE) == 1)
            textInnocent.text = findPlayer(PEACEFUL_TOWNIE.ID).getPName() + LANG.votesInnocent[lang];
    }

    private void UpdateAMayor()
    {
        if (nAlivePlayers < 4)
            aMayor = 1;
        else if (nAlivePlayers < 10)
            aMayor = 2;
        else if (nAlivePlayers < 15)
            aMayor = 3;
        else
            aMayor = 4;
    }

    private void useTitle(string _text, Color _color)
    {
        title.gameObject.SetActive(true);
        title.text = _text;
        title.color = _color;
    }

    private void useMessage(string _text, Color _color)
    {
        message.gameObject.SetActive(true);
        message.text = _text;
        message.color = _color;
    }

    private void useAskAbility(int _available, int _max)
    {
        GameObject go;

        buttonNext.GetComponentInChildren<Text>().text = LANG.pass[lang];

        for (int i = 0; i < _max; i++)
        {
            go = buttonsAbility[i];
            go.GetComponent<Image>().sprite = _max - i > _available ? usedAbility : unusedAbility;
            go.GetComponent<Button>().interactable = i == _max - _available;
            go.SetActive(true);
        }

        show.SetActive(true);
        layoutAskAbility.SetActive(true);
    }

    public void buttonAbility(int _index)
    {
        aUse = !aUse;
        buttonsAbility[_index].GetComponent<Image>().sprite = aUse ? usedAbility : unusedAbility;
        buttonNext.GetComponentInChildren<Text>().text = aUse ? LANG.use[lang] : LANG.pass[lang];
    }

    private void useRoleListRoleless()
    {
        buttonNext.SetActive(false);

        minPick = 1;
        maxPick = 1;

        scrollerRoleList.SetActive(true);

        for (int i = 0; i < GD.GetNRoles(); ++i)
        {
            Role r = GD.GetRole(i);
            if (countRole(r) > roleCountAlive(r))
                rolesItems[i].gameObject.SetActive(true);
        }
    }

    private void useRoleListByAmnesiac()
    {
        buttonNext.SetActive(false);

        minPick = 1;
        maxPick = -1; //Infinite

        scrollerRoleList.SetActive(true);

        for (int i = 0; i < GD.GetNRoles(); ++i)
        {
            Role r = GD.GetRole(i);
            if (countRole(r) < r.cards)
                rolesItems[i].gameObject.SetActive(true);
        }
    }

    private void usePlayerListSetRole(int _maxPick)
    {
        buttonNext.SetActive(false);

        minPick = _maxPick;
        maxPick = _maxPick;

        scrollerPlayerList.SetActive(true);

        for (int i = 0; i < nPlayers; i++)
        {
            if (players[i].getIsAlive() && players[i].getRole().ID == TOWNIE.ID)
                playersItems[i].gameObject.SetActive(true);
        }
    }

    private void usePlayerListByPlayer(int _minPick, int _maxPick, Role _role)
    {
        RoleEnum r;

        buttonNext.SetActive(_minPick == 0);

        minPick = _minPick;
        maxPick = _maxPick;

        if (amnesiacRole.ID != _role.ID)
            speak.SetActive(true);
        scrollerPlayerList.SetActive(true);
        buttonNext.GetComponentInChildren<Text>().text = LANG.pass[lang];

        for (int i = 0; i < nPlayers; i++)
        {
            if (players[i].getIsAlive())
            {
                r = players[i].getRole().ID;

                if (r != _role.ID)
                {
                    // Doctor can't heal revealed Mayor
                    if (!(_role.ID == RoleEnum.DOCTOR && r == RoleEnum.MAYOR && mayorRevealed))
                        // Variant - bodyguardGuardTwice
                        if (!(!SF.bodyguardGuardTwice && _role.ID == RoleEnum.BODYGUARD && players[i] == bodyguarded))
                            playersItems[i].gameObject.SetActive(true);
                }
            }
        }
    }

    private void usePlayerListByMafia()
    {
        minPick = 0;
        maxPick = SF.godfatherAloneTwoKills && nAliveMafia == 1 && roleCountAlive(GODFATHER) > 0 ? 2 : 1; // Variant - godfatherAloneTwoKills

        scrollerPlayerList.SetActive(true);
        buttonNext.GetComponentInChildren<Text>().text = LANG.pass[lang];

        for (int i = 0; i < nPlayers; i++)
        {
            Player p = players[i];
            if (p.getIsAlive() && p.getFaction() != Faction.MAFIA)
                playersItems[i].gameObject.SetActive(true);
        }
    }

    private void usePlayerListByMedium()
    {
        minPick = 0;
        maxPick = 1;

        speak.SetActive(true);
        scrollerPlayerList.SetActive(true);
        buttonNext.GetComponentInChildren<Text>().text = LANG.pass[lang];

        Item i;
        Player p;

        for (int x = 0; x < nPlayers; x++)
        {
            i = playersItems[x];
            p = players[x];

            if (!p.getIsAlive() && p.getKillers().Count > 0)
            {
                i.setSingleText();
                i.setTextColor(Color.white);
                i.gameObject.SetActive(true);
            }
        }
    }

    private void usePlayerListEveryone()
    {
        minPick = 0;
        maxPick = 1;

        scrollerPlayerList.SetActive(true);

        for (int i = 0; i < nPlayers; i++)
        {
            Player p = players[i];
            if (p.getIsAlive() && !(panel == Panel.BLACKMAILER && p == lastBlackmailed))
                playersItems[i].gameObject.SetActive(true);
        }
    }

    private void jumpImmediate(Panel _panel)
    {
        nextPanel = _panel;
        openNextPanel();
    }

    public void selectRole(int _role)
    {
        if (selectedRolesIndexes.Contains(_role))
        {
            deselectRole(_role);
            return;
        }

        rolesItems[_role].GetComponent<Item>().setAsButtonSelected();
        selectedRolesIndexes.Add(_role);

        if (selectedRolesIndexes.Count >= minPick)
            buttonNext.SetActive(true);

        if (selectedRolesIndexes.Count == maxPick)
        {
            for (int i = 0; i < GD.GetNRoles(); i++)
            {
                if (!selectedRolesIndexes.Contains(i))
                    rolesItems[i].setInteractable(false);
            }
        }

        buttonNext.GetComponentInChildren<Text>().text = LANG.pick[lang];
    }

    public void deselectRole(int _role)
    {
        rolesItems[_role].GetComponent<Item>().setAsButtonUnselected(GD.GetRoleColor(_role));

        if (selectedRolesIndexes.Count == maxPick)
        {
            for (int i = 0; i < GD.GetNRoles(); i++)
                rolesItems[i].setInteractable(true);
        }

        selectedRolesIndexes.Remove(_role);

        if (selectedRolesIndexes.Count < minPick)
            buttonNext.SetActive(false);

        if (selectedRolesIndexes.Count == 0)
            buttonNext.GetComponentInChildren<Text>().text = LANG.pass[lang];
    }

    public void selectPlayer(int _player)
    {
        if (selectedPlayersIndexes.Contains(_player))
        {
            deselectPlayer(_player);
            return;
        }

        playersItems[_player].GetComponent<Item>().setAsButtonSelected();
        selectedPlayersIndexes.Add(_player);

        if (selectedPlayersIndexes.Count >= minPick)
            buttonNext.SetActive(true);

        if (selectedPlayersIndexes.Count == maxPick)
        {
            for (int i = 0; i < nPlayers; i++)
            {
                if (!selectedPlayersIndexes.Contains(i))
                    playersItems[i].setInteractable(false);
            }
        }

        buttonNext.GetComponentInChildren<Text>().text = LANG.pick[lang];
    }

    private void deselectPlayer(int _player)
    {
        playersItems[_player].GetComponent<Item>().setAsButtonUnselected();

        if (selectedPlayersIndexes.Count == maxPick)
        {
            for (int i = 0; i < nPlayers; i++)
                playersItems[i].setInteractable(true);
        }

        selectedPlayersIndexes.Remove(_player);

        if (selectedPlayersIndexes.Count < minPick)
            buttonNext.SetActive(false);

        if (selectedPlayersIndexes.Count == 0)
            buttonNext.GetComponentInChildren<Text>().text = LANG.pass[lang];
    }

    private int abilityValue()
    {
        if (nPlayers < 10)
            return 2;
        if (nPlayers < 15)
            return 3;

        return 4;
    }

    public void buttonDayOptions()
    {
        if (popupDayOptions.activeSelf)
        {
            title.gameObject.SetActive(true);
            playerList.gameObject.SetActive(true);
            popupDayOptions.SetActive(false);
        }
        else
        {
            title.gameObject.SetActive(false);
            playerList.gameObject.SetActive(false);
            popupDayOptions.SetActive(true);
        }
    }

    public void buttonMayorReveal()
    {
        mayorRevealed = true;
        updateOptions();
    }

    private void clearProgressAbility()
    {
        aUse = false;
        for (int i = 0; i < buttonsAbility.Length; i++)
            buttonsAbility[i].SetActive(false);
    }

    private void clearRoleList()
    {
        Item it;

        for (int i = 0; i < nAllRoles; i++)
        {
            it = rolesItems[i];
            it.setAsButtonUnselected(GD.GetRoleColor(i));
            it.gameObject.SetActive(false);
        }
    }

    private void clearPlayerList()
    {
        Item it;

        for (int i = 0; i < nPlayers; i++)
        {
            it = playersItems[i];
            it.setAsButtonUnselected();
            it.gameObject.SetActive(false);
        }
    }

    private void addToDie(Player _player)
    {
        if (!toDie.Contains(_player))
            toDie.Add(_player);
    }

    private void removeToDie(Player _player)
    {
        toDie.Remove(_player);
        _player.clearKillers();
    }

    private void killPlayer(Player _player)
    {
        _player.setIsAlive(false);
        --nAlivePlayers;

        if (_player.getRole().ID == BODYGUARD.ID)
            bodyguarded = null;

        if (_player.getFaction() == Faction.MAFIA)
            nAliveMafia--;

        if (_player.getKillers().Count > 0)
            seance = true;

        if (_player == amnesiacPlayer)
            existsAmnesiac = false;
    }

    private bool roleExists(Role _role)
    {
        for (int i = 0; i < nPlayers; i++)
        {
            if (playingRoles[i].ID == _role.ID)
                return true;
        }

        return false;
    }

    private int countRole(Role _role)
    {
        int count = 0;

        for (int i = 0; i < nPlayers; i++)
        {
            if (playingRoles[i].ID == _role.ID)
                ++count;
        }

        return count;
    }

    private int roleCountAlive(Role _role)
    {
        int c = 0;

        for (int i = 0; i < nPlayers; i++)
        {
            Player p = players[i];
            if (p.getIsAlive() && p.getIsRoleSet() && p.getRole().ID == _role.ID)
                ++c;
        }

        return c;
    }

    private int countRoleless()
    {
        int c = 0;

        for (int i = 0; i < nPlayers; i++)
        {
            if (!players[i].getIsRoleSet())
                ++c;
        }

        return c;
    }

    private Player findPlayer(RoleEnum _role)
    {
        for (int i = 0; i < nPlayers; ++i)
            if (players[i].getRole().ID == _role)
                return players[i];

        Debug.LogError("No player found.");
        return new Player(-1, "Invalid Player");
    }

    private Player randomMafia()
    {
        Player[] mafia = new Player[nAliveMafia];
        int index = 0;

        for (int i = 0; i < nPlayers; i++)
        {
            Player p = players[i];
            if (p.getIsAlive() && p.getFaction() == Faction.MAFIA)
            {
                mafia[index] = p;
                ++index;
            }
        }

        return mafia[Random.Range(0, nAliveMafia)];
    }

    private void checkWin()
    {
        int nAliveTown = 0;
        int nAliveNeutral = 0;

        // Witch
        if (roleCountAlive(WITCH) == 1)
        {
            bool witchWin = true;

            for (int i = 0; i < nPlayers; ++i)
            {
                Player p = players[i];

                if (p.getIsAlive())
                {
                    if (p.getRole().ID != WITCH.ID && p.getRole().ID != AMNESIAC.ID && !p.getIsCursed())
                    {
                        witchWin = false;
                        break;
                    }
                }
            }

            if (witchWin)
            {
                nextPanel = Panel.WITCH_WIN;
                return;
            }
        }

        // Counts town and neutral players
        for (int i = 0; i < nPlayers; ++i)
        {
            Player p = players[i];
            if (p.getIsAlive())
            {
                if (p.getFaction() == Faction.TOWN)
                    ++nAliveTown;
                else if (p.getFaction() == Faction.NEUTRAL)
                    ++nAliveNeutral;
            }
        }

        // Remove Amnesiac from win conditions, as he can win with any faction
        if (roleCountAlive(AMNESIAC) == 1)
            --nAliveNeutral;

        // TOWN
        if (nAliveTown > 0)
        {
            if (nAliveMafia == 0 && roleCountAlive(SERIAL_KILLER) == 0 && roleCountAlive(WITCH) == 0 && roleCountAlive(WEREWOLF) == 0)
            {
                nextPanel = Panel.TOWN_WIN;
                return;
            }
        }

        // MAFIA
        if (nAliveMafia > 0)
        {
            int townPower = nAliveTown;

            townPower += roleCountAlive(MAYOR) > 0 ? aMayor - 1 : 0;
            townPower += roleCountAlive(BODYGUARD) > 0 && roleCountAlive(DOCTOR) > 0 ? 1 : 0;
            townPower += roleCountAlive(VETERAN) > 0 && aVeteran > 0 ? aVeteran + 1 : 0;
            townPower += roleCountAlive(VIGILANTE) > 0 && aVigilante > 0 ? 1 : 0;

            if (roleCountAlive(SERIAL_KILLER) == 0 && roleCountAlive(WITCH) == 0 && roleCountAlive(WEREWOLF) == 0)
            {
                if (nAliveMafia >= townPower || nextPanel == Panel.DISCUSSION && nAliveTown == 1 /* In case there is 1v1 on votes, mafia wins*/)
                {
                    nextPanel = Panel.MAFIA_WIN;
                    return;
                }
            }
        }

        if (nAlivePlayers == 1)
        {
            // Serial Killer only
            if (roleCountAlive(SERIAL_KILLER) == 1)
                nextPanel = Panel.SERIAL_KILLER_WIN;
            // Werewolf only
            else if (roleCountAlive(WEREWOLF) == 1)
                nextPanel = Panel.WEREWOLF_WIN;
        }
        else if (nAlivePlayers == 2)
        {
            if (roleCountAlive(SURVIVOR) == 1)
            {
                if (roleCountAlive(SERIAL_KILLER) == 1)
                {
                    // 1 Survivor and 1 Serial Killer
                    nextPanel = Panel.SERIAL_KILLER_WIN;
                    return;
                }
                else if (roleCountAlive(WEREWOLF) == 1)
                {
                    // 1 Survivor and 1 Werewolf
                    nextPanel = Panel.WEREWOLF_WIN;
                    return;
                }
            }
            else if (roleCountAlive(JESTER) + roleCountAlive(EXECUTIONER) == 2)
            {
                // 2 Jesters or 1 Jester and 1 Executioner
                nextPanel = Panel.DRAW;
                return;
            }
        }
    }
}
