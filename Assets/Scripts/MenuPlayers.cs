using UnityEngine;
using UnityEngine.UI;

public class MenuPlayers : MonoBehaviour
{
    public GameObject manager;
    private GlobalData GD;
    private SaveFile SF;
    private Languages LANG;
    [Header("Items")]
    public GameObject prefabButtonItem;
    public ItemList prefabItemList;
    [Header("AddPlayer")]
    public GameObject layoutAddPlayer;
    public Text textPlayerName;
    public InputField fieldAddPlayer;
    public Text textPlaceholder;
    public Text textPlayerNickname;
    public InputField fieldAddPlayerNickname;
    public Text textPlaceholderNickname;
    public GameObject textError;
    public Text textButtonAdd;
    [Space]
    public GameObject layoutRemovePlayer;
    public Text textRemovePlayerName;
    public InputField fieldRemovePlayer;
    public Text textPlaceholderRemove;
    public GameObject textErrorRemove;
    public Text textButtonRemove;
    public Transform listPlayers;
    public GameObject layoutEditPlayer;
    public Text textEditName;
    public Text textEditNickname;
    public Text textPlaceholderEditName;
    public Text textPlaceholderEditNickname;
    public InputField fieldEditPlayerName;
    public InputField fieldEditPlayerNickname;
    public GameObject textErrorEdit;
    public Text bConfirmEdit;
    private bool editingPlayer;

    [Header("Player Stats")]
    public GameObject layoutPersonStats;
    public Sprite tabUnselected;
    public Sprite tabSelected;
    public GameObject bTotal;
    public GameObject bSession;
    public Text textTotalRounds;
    public Text textTotalRoundsValue;
    public Text textWinRate;
    public Text textWinRateValue;
    public Text textModerated;
    public Text textModeratedValue;
    public GameObject bRounds;
    public GameObject bWins;
    public GameObject bRate;
    public Transform listRolesStats;
    private ItemList[] itemsRolesStats;
    private Person currentPerson;
    private bool total;  // True - Total   |    False - Session
    private enum Stats
    {
        ROUNDS,
        WINS,
        RATE
    }
    private Stats currentStat;

    private int lang;

    private void Awake()
    {
        GD = manager.GetComponent<GlobalData>();
        SF = manager.GetComponent<SaveFile>();
        LANG = manager.GetComponent<Languages>();

        itemsRolesStats = new ItemList[GD.GetNRoles()];
    }

    private void OnEnable()
    {
        lang = LANG.GetLanguage();

        FlushPlayers();
        UpdateLanguage();
    }

    private void Start()
    {
        BuildPlayerProfile();
    }

    private void OnDisable()
    {
        SF.SortPlayers();
        SF.SavePlayerData();
    }

    private void FlushPlayers()
    {
        for (int i = 0; i < listPlayers.childCount; i++)
            Destroy(listPlayers.GetChild(i).gameObject);

        for (int x = 0; x < SF.getTotalPersons(); x++)
        {
            GameObject go = Instantiate(prefabButtonItem);
            Item i = go.GetComponent<Item>();
            i.setPerson(SF.GetPerson(x));
            i.setText(SF.GetPerson(x).pName);
            i.setAsButtonUnselected();
            i.getButton().onClick.AddListener(() =>
            {
                ButtonOpenPlayer(go);
            });
            go.transform.SetParent(listPlayers);
        }
    }

    private void UpdateLanguage()
    {
        //Language
        textPlayerName.text = LANG.playerName[lang];
        textPlaceholder.text = LANG.tapToWrite[lang];
        textPlayerNickname.text = LANG.nickname[lang];
        textPlaceholderNickname.text = LANG.tapToWrite[lang];
        textButtonAdd.text = LANG.add[lang];
        textRemovePlayerName.text = LANG.playerName[lang];
        textPlaceholderRemove.text = LANG.tapToWrite[lang];
        textButtonRemove.text = LANG.remove[lang];
        textEditName.text = LANG.playerName[lang];
        textEditNickname.text = LANG.nickname[lang];
        bConfirmEdit.text = LANG.edit[lang];
        textPlaceholderEditName.text = LANG.tapToWrite[lang];
        textPlaceholderEditNickname.text = LANG.tapToWrite[lang];
    }

    private void BuildPlayerProfile()
    {
        for (int i = 0; i < itemsRolesStats.Length; i++)
            itemsRolesStats[i] = Instantiate(prefabItemList, listRolesStats).GetComponent<ItemList>();

    }

    public void ButtonAddPlayer()
    {
        fieldAddPlayer.text = string.Empty;
        fieldAddPlayerNickname.text = string.Empty;
    }

    public void ButtonConfirmAddPlayer()
    {
        string pName = fieldAddPlayer.text;
        string pNickname = fieldAddPlayerNickname.text;

        if (pName == string.Empty)
        {
            textError.GetComponent<Text>().text = LANG.emptyName[lang];
            textError.SetActive(true);
            return;
        }
        else if (pName == pNickname)
        {
            textError.GetComponent<Text>().text = LANG.nameSameNickname[lang];
            textError.SetActive(true);
            return;
        }

        string fullName;

        for (int i = 0; i < SF.getTotalPersons(); ++i)
        {
            fullName = SF.GetPerson(i).pName;

            if (pName == fullName)
            {
                textError.GetComponent<Text>().text = LANG.nameTaken[lang];
                textError.SetActive(true);
                return;
            }

            if (pNickname == fullName)
            {
                textError.GetComponent<Text>().text = LANG.nicknameTaken[lang];
                textError.SetActive(true);
                return;
            }
        }

        Person p = new Person(pName, pNickname);
        SF.addPerson(p);

        GameObject go = Instantiate(prefabButtonItem);
        Item it = go.GetComponent<Item>();
        it.setPerson(p);
        it.setText(pName);
        it.setAsButtonUnselected();
        it.getButton().onClick.AddListener(() =>
        {
            ButtonOpenPlayer(go);
        });
        go.transform.SetParent(listPlayers);
        layoutAddPlayer.SetActive(false);
    }

    public void ButtonEditPlayer()
    {
        editingPlayer = !editingPlayer;
        textErrorEdit.SetActive(false);
    }

    public void ButtonRemovePlayer()
    {
        fieldRemovePlayer.text = string.Empty;

        layoutRemovePlayer.SetActive(true);
    }

    /*public void ButtonConfirmRemovePlayer()
    {
        string pName = fieldRemovePlayer.text;

        if (pName == string.Empty)
        {
            textErrorRemove.GetComponent<Text>().text = LANG.emptyName[lang];
            textErrorRemove.SetActive(true);
            return;
        }

        Item i = null;

        for (int x = 0; x < SF.getTotalPersons(); ++x)
        {
            //Found player to remove
            if (pName == i.getText().text)
            {
                SF.removePerson(i.getPerson());
                UpdateStats();
                Destroy(i.gameObject);
                return;
            }
        }

        textErrorRemove.GetComponent<Text>().text = LANG.playerNotFound[lang];
        textErrorRemove.SetActive(true);
    }*/

    public void ButtonOpenPlayer(GameObject _button)
    {
        currentPerson = _button.GetComponent<Item>().GetPerson();

        total = false;              // So it updates total stats
        currentStat = Stats.RATE;   // So it updates rounds stats

        //Language
        bTotal.GetComponentInChildren<Text>().text = LANG.total[lang];
        bSession.GetComponentInChildren<Text>().text = LANG.session[lang];
        textTotalRounds.text = LANG.totalRounds[lang];
        textWinRate.text = LANG.winRate[lang];
        textModerated.text = LANG.moderated[lang];
        bRounds.GetComponentInChildren<Text>().text = LANG.rounds[lang];
        bWins.GetComponentInChildren<Text>().text = LANG.wins[lang];
        bRate.GetComponentInChildren<Text>().text = LANG.rate[lang];

        ButtonTotal();
        ButtonRounds();

        layoutPersonStats.SetActive(true);
    }

    private void OpenEditPlayer()
    {
        fieldEditPlayerName.text = currentPerson.pName;
        fieldEditPlayerNickname.text = currentPerson.pNameShort;

        layoutEditPlayer.SetActive(true);
    }

    /*public void ConfirmEditPlayer()
    {
        string pName = fieldEditPlayerName.text;
        string pNickname = fieldEditPlayerNickname.text;

        if (pName == string.Empty)
        {
            textErrorEdit.GetComponent<Text>().text = LANG.emptyName[lang];
            textErrorEdit.SetActive(true);
            return;
        }
        else if (pName == pNickname)
        {
            textErrorEdit.GetComponent<Text>().text = LANG.nameSameNickname[lang];
            textErrorEdit.SetActive(true);
            return;
        }

        string fullName;

        for (int x = 0; x < SF.getTotalPersons(); ++x)
        {
            if (currentPerson == itemsAllPlayers[x].getPerson())
                continue;

            fullName = itemsAllPlayers[x].getPerson().pName;

            if (pName != currentPerson.pName && pName == fullName)
            {
                textErrorEdit.GetComponent<Text>().text = LANG.nameTaken[lang];
                textErrorEdit.SetActive(true);
                return;
            }

            if (pNickname != currentPerson.pNameShort && pNickname == fullName)
            {
                textErrorEdit.GetComponent<Text>().text = LANG.nicknameTaken[lang];
                textErrorEdit.SetActive(true);
                return;
            }
        }

        currentPerson.pName = pName;
        currentPerson.pNameShort = pNickname;

        Item i = null;

        for (int x = 0; x < SF.getTotalPersons(); x++)
        {
            i = itemsAllPlayers[x];
            if (i.getPerson() == currentPerson)
            {
                i.setText(currentPerson.pName);
                break;
            }
        }
    }*/

    public void ButtonTotal()
    {
        if (total)
            return;

        total = true;

        bTotal.GetComponent<Image>().sprite = tabSelected;
        bSession.GetComponent<Image>().sprite = tabUnselected;

        textTotalRoundsValue.text = currentPerson.getTotalGames().ToString();
        textWinRateValue.text = (currentPerson.getTotalWinRate() * 100).ToString("F0") + "%";
        textModeratedValue.text = currentPerson.gamesModerated.ToString();

        UpdateStats();
    }

    public void ButtonSession()
    {
        if (!total)
            return;

        total = false;

        bTotal.GetComponent<Image>().sprite = tabUnselected;
        bSession.GetComponent<Image>().sprite = tabSelected;

        textTotalRoundsValue.text = currentPerson.getTotalGamesSession().ToString();
        textWinRateValue.text = (currentPerson.getTotalWinRateSession() * 100).ToString("F0") + "%";
        textModeratedValue.text = currentPerson.gamesModeratedSession.ToString();

        UpdateStats();
    }

    public void ButtonRounds()
    {
        if (currentStat == Stats.ROUNDS)
            return;

        bRounds.GetComponent<Image>().sprite = tabSelected;
        bWins.GetComponent<Image>().sprite = tabUnselected;
        bRate.GetComponent<Image>().sprite = tabUnselected;

        currentStat = Stats.ROUNDS;
        UpdateStats();
    }

    public void ButtonWins()
    {
        if (currentStat == Stats.WINS)
            return;

        bRounds.GetComponent<Image>().sprite = tabUnselected;
        bWins.GetComponent<Image>().sprite = tabSelected;
        bRate.GetComponent<Image>().sprite = tabUnselected;

        currentStat = Stats.WINS;
        UpdateStats();
    }

    public void ButtonRate()
    {
        if (currentStat == Stats.RATE)
            return;

        bRounds.GetComponent<Image>().sprite = tabUnselected;
        bWins.GetComponent<Image>().sprite = tabUnselected;
        bRate.GetComponent<Image>().sprite = tabSelected;

        currentStat = Stats.RATE;
        UpdateStats();
    }

    private void UpdateStats()
    {
        switch (currentStat)
        {
            case Stats.ROUNDS:
                for (int i = 0; i < GD.GetNRoles(); ++i)
                {
                    Role r = GD.GetRole(i);
                    itemsRolesStats[i].StartThis(r.text, (total ? currentPerson.GetGamesByRole(i) : currentPerson.GetGamesByRoleSession(i)).ToString(), GD.GetRoleColor(r));
                }
                break;
            case Stats.WINS:
                for (int i = 0; i < GD.GetNRoles(); ++i)
                {
                    Role r = GD.GetRole(i);
                    itemsRolesStats[i].StartThis(r.text, (total ? currentPerson.GetWinsByRole(i) : currentPerson.GetWinsByRoleSession(i)).ToString(), GD.GetRoleColor(r));
                }
                break;
            case Stats.RATE:
                for (int i = 0; i < GD.GetNRoles(); ++i)
                {
                    Role r = GD.GetRole(i);
                    itemsRolesStats[i].StartThis(r.text, ((total ? currentPerson.GetRateByRole(i) : currentPerson.GetRateByRoleSession(i)) * 100).ToString("F0") + "%", GD.GetRoleColor(r));
                }
                break;
        }
    }

    public void FieldPlayerNameValueChanged()
    {
        textError.SetActive(false);
    }

    public void FieldPlayerNameRemoveValueChanged()
    {
        textErrorRemove.SetActive(false);
    }
}
