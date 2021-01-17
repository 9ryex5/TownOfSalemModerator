using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Menu : MonoBehaviour
{
    private GlobalData GD;
    private DataTransfer DT;
    public SaveFile SF;
    public Languages LANG;

    private int nRoles;
    private int lang;
    public GameObject prefabButtonItem;

    // Play - Pick Players
    [Header("Play - Pick Players")]
    public GameObject layoutPickPlayers;
    public Transform listPlayAllPlayers;
    public Transform listPlayPickedPlayers;
    private List<Item> itemsPlayAllPlayers;
    private List<Item> itemsPlayPickedPlayers;
    public Text textAvailablePlayers;
    public Text textPickedPlayers;
    public Text textNPlayingPlayers;
    private int nPickedPlayers;
    public GameObject minPlayers;
    public GameObject bNext;
    private bool pickingModerator;
    public Text labelPickModerator;
    public GameObject bRandom;
    private Item moderator;
    private GameObject lastModerator;
    public GameObject bNextPickRoles;
    private List<Item> itemPickedPlayers;

    // Play - Pick Roles
    [Header("Play - Pick Roles")]
    public GameObject layoutRoles;
    private int pickedQ;
    private int balance;
    public Text textBalance;
    public Text textAvailableRoles;
    public Text textPickedRoles;
    public Transform listAllRoles;
    private Button[] bAllRoles;
    public Transform listPickedRoles;
    private Button[] bPickedRoles;
    private int[] rolesQ;
    private Role[] pickedRoles;
    public GameObject bStart;
    public GameObject textInvalidGame;
    public GameObject bottomLayout;


    private void Start()
    {
        GD = GlobalData.GD;
        DT = DataTransfer.DT;

        nRoles = GD.GetNRoles();

        lang = LANG.GetLanguage();

        itemsPlayAllPlayers = new List<Item>();
        itemsPlayPickedPlayers = new List<Item>();

        /*for (int x = 0; x < nRoles; x++)
        {
            GameObject go = Instantiate(prefabButtonItem);
            Item i = go.GetComponent<Item>();
            i.setAsList();
            i.setDoubleText();
            i.setText1(GD.getRole(x).text);
            i.setTextColor(GD.getRoleColor(x));
            itemsRolesStats[x] = i;
            go.transform.SetParent(listRolesStats);
        }*/
    }

    public void ButtonPlay()
    {
        List<Person> lastPlaying = new List<Person>(DT.getLastPlaying());

        nPickedPlayers = 0;
        itemPickedPlayers = new List<Item>();

        //Language
        textAvailablePlayers.text = LANG.available[lang];
        textPickedPlayers.text = LANG.picked[lang];
        minPlayers.GetComponent<Text>().text = LANG.min6players[lang];
        bNext.GetComponentInChildren<Text>().text = LANG.next[lang];
        labelPickModerator.text = LANG.pickModerator[lang];
        bRandom.GetComponentInChildren<Text>().text = LANG.random[lang];
        bNextPickRoles.GetComponentInChildren<Text>().text = LANG.next[lang];

        //Clear lists
        for (int x = 0; x < itemsPlayAllPlayers.Count; x++)
            Destroy(itemsPlayAllPlayers[x].gameObject);

        itemsPlayAllPlayers.Clear();
        itemsPlayPickedPlayers.Clear();

        Item i = null;
        Person p = null;
        Person other = null;
        bool useFullName;

        for (int x = 0; x < SF.getTotalPersons(); ++x)
        {
            GameObject go = Instantiate(prefabButtonItem);
            i = go.GetComponent<Item>();
            p = SF.GetPerson(x);
            i.setPerson(p);
            useFullName = p.pNameShort == string.Empty;
            if (!useFullName)
            {
                for (int y = 0; y < x; y++)
                {
                    other = itemsPlayAllPlayers[y].GetPerson();
                    if (p.pNameShort == other.pNameShort)
                    {
                        useFullName = true;
                        itemsPlayAllPlayers[y].setText(other.pName);
                        break;
                    }
                }
            }
            i.setText(useFullName ? p.pName : p.pNameShort);
            i.setAsButtonUnselected();
            i.getButton().onClick.AddListener(() =>
            {
                buttonPickPlayer(go);
            });
            go.transform.SetParent(listPlayAllPlayers);
            itemsPlayAllPlayers.Add(i);

            if (lastPlaying.Contains(p))
                buttonPickPlayer(go);
        }

        if (nPickedPlayers < 6)
        {
            if (nPickedPlayers == 0)
                textNPlayingPlayers.text = "0 " + LANG.playing[lang];

            bNext.SetActive(false);
        }

        layoutPickPlayers.SetActive(true);
    }

    public void buttonPickPlayer(GameObject button)
    {
        if (nPickedPlayers >= 39)
            return;

        ++nPickedPlayers;
        if (nPickedPlayers >= 6)
        {
            minPlayers.SetActive(false);
            bNext.SetActive(true);
        }

        Button b = button.GetComponent<Button>();
        Item i = button.GetComponent<Item>();

        b.onClick.RemoveAllListeners();

        b.onClick.AddListener(() =>
        {
            buttonUnpickPlayer(button);
        });

        button.transform.SetParent(listPlayPickedPlayers);
        itemsPlayPickedPlayers.Add(i);
        textNPlayingPlayers.text = nPickedPlayers + " " + LANG.playing[lang];
        itemPickedPlayers.Add(button.GetComponent<Item>());
    }

    public void buttonUnpickPlayer(GameObject button)
    {
        if (!pickingModerator) // Unpick Player
        {
            --nPickedPlayers;
            if (nPickedPlayers < 6)
            {
                bNext.SetActive(false);
                minPlayers.SetActive(true);
            }

            Button b = button.GetComponent<Button>();
            Item i = button.GetComponent<Item>();

            b.onClick.RemoveAllListeners();

            b.onClick.AddListener(() =>
            {
                buttonPickPlayer(button);
            });

            button.transform.SetParent(listPlayAllPlayers);
            itemsPlayPickedPlayers.Remove(i);
            textNPlayingPlayers.text = nPickedPlayers + " " + LANG.playing[lang];
            itemPickedPlayers.Remove(button.GetComponent<Item>());
        }
        else if (lastModerator == button) //Deselect Moderator
        {
            button.GetComponent<Item>().setAsButtonUnselected();
            lastModerator = null;
            bNextPickRoles.SetActive(false);
        }
        else //Select Moderator
        {
            if (lastModerator != null)
            {
                lastModerator.GetComponent<Item>().setAsButtonUnselected();
            }

            lastModerator = button;
            moderator = button.GetComponent<Item>();
            button.GetComponent<Item>().setAsButtonSelected();
            bNextPickRoles.SetActive(true);
        }
    }

    public void buttonNextPickModerator()
    {
        pickingModerator = true;
        textAvailablePlayers.gameObject.SetActive(false);
        textPickedPlayers.gameObject.SetActive(false);
        listPlayAllPlayers.gameObject.SetActive(false);
        bottomLayout.SetActive(false);
        labelPickModerator.gameObject.SetActive(true);
        bRandom.SetActive(true);
    }

    public void buttonPickRandom()
    {
        int random = 0;

        do
        {
            random = Random.Range(0, nPickedPlayers);
        } while (itemPickedPlayers[random].gameObject == lastModerator);

        buttonUnpickPlayer(itemPickedPlayers[random].gameObject);
    }

    public void buttonNextPickRoles()
    {
        List<Person> playing = new List<Person>();

        for (int i = 0; i < itemPickedPlayers.Count; i++)
            playing.Add(itemPickedPlayers[i].GetPerson());

        DT.setLastPlaying(playing);

        itemPickedPlayers.Remove(moderator);
        nPickedPlayers--;

        layoutPickPlayers.SetActive(false);

        //Language
        textAvailableRoles.text = LANG.available[lang];
        textPickedRoles.text = LANG.picked[lang];
        bStart.GetComponentInChildren<Text>().text = LANG.start[lang];

        bAllRoles = new Button[nRoles];
        bPickedRoles = new Button[nRoles];
        rolesQ = new int[nRoles];
        pickedRoles = new Role[nPickedPlayers];

        textBalance.text = "0";
        textBalance.color = Color.white;
        textInvalidGame.GetComponent<Text>().text = "0/" + nPickedPlayers + " roles";

        for (int i = 0; i < nRoles; i++)
        {
            GameObject go = Instantiate(prefabButtonItem);
            GameObject go2 = Instantiate(prefabButtonItem);
            bAllRoles[i] = go.GetComponent<Button>();
            bPickedRoles[i] = go2.GetComponent<Button>();
            Text t = go.GetComponentInChildren<Text>();
            Role r = GD.GetRole(i);
            t.text = r.text + " (" + (r.balance > 0 ? "+" : "") + r.balance + ")";
            t.color = GD.GetRoleColor(i);
            go2.GetComponentInChildren<Text>().color = GD.GetRoleColor(i);
            int aux = i;                                             //Has to be allocated inside loop, otherwise AddListener would always get the same value
            go.GetComponent<Button>().onClick.AddListener(() =>
            {
                buttonPickRole(aux);
            });
            go2.GetComponent<Button>().onClick.AddListener(() =>
            {
                buttonUnpickRole(aux);
            });
            go.transform.SetParent(listAllRoles);
            go2.transform.SetParent(listPickedRoles);
            go2.SetActive(false);
        }

        layoutRoles.SetActive(true);
    }

    public void buttonPickRole(int _roleIndex)
    {
        if (pickedQ == nPickedPlayers)
            return;

        Role r = GD.GetRole(_roleIndex);

        pickedQ++;
        rolesQ[_roleIndex]++;

        if (rolesQ[_roleIndex] >= r.cards)
            bAllRoles[_roleIndex].gameObject.SetActive(false);

        bPickedRoles[_roleIndex].GetComponentInChildren<Text>().text = rolesQ[_roleIndex] + " " + r.text;

        if (rolesQ[_roleIndex] == 1)
            bPickedRoles[_roleIndex].gameObject.SetActive(true);

        balance += r.balance;
        updateInterface();
    }

    public void buttonUnpickRole(int _roleIndex)
    {
        Role r = GD.GetRole(_roleIndex);

        pickedQ--;
        rolesQ[_roleIndex]--;

        if (rolesQ[_roleIndex] <= r.cards)
            bAllRoles[_roleIndex].gameObject.SetActive(true);

        bPickedRoles[_roleIndex].GetComponentInChildren<Text>().text = rolesQ[_roleIndex] + " " + r.text;

        if (rolesQ[_roleIndex] == 0)
            bPickedRoles[_roleIndex].gameObject.SetActive(false);

        balance -= r.balance;
        updateInterface();
    }

    private void updateInterface()
    {
        if (balance > 0)
        {
            textBalance.text = "+" + balance.ToString();
            textBalance.color = GD.townColor;
        }
        else if (balance < 0)
        {
            textBalance.text = balance.ToString();
            textBalance.color = GD.mafiaColor;
        }
        else
        {
            textBalance.text = balance.ToString();
            textBalance.color = Color.white;
        }

        if (pickedQ == nPickedPlayers)
        {
            bStart.SetActive(checkValidGame());
            textInvalidGame.SetActive(!bStart.activeSelf);
        }
        else
        {
            bStart.SetActive(false);
            textInvalidGame.GetComponent<Text>().text = pickedQ + "/" + nPickedPlayers + " roles";
            textInvalidGame.SetActive(true);
        }
    }

    public void buttonStart()
    {
        List<Person> p = new List<Person>();

        for (int i = 0; i < nPickedPlayers; i++)
            p.Add(itemPickedPlayers[i].GetPerson());

        DT.setPlayingPersons(p);

        int index = 0;

        for (int i = 0; i < nRoles; i++)
        {
            for (int j = 0; j < rolesQ[i]; j++)
            {
                pickedRoles[index] = GD.GetRole(i);
                index++;
            }
        }

        moderator.GetPerson().incrementGamesModerated();

        DT.setSplash(false);
        DT.setPlayingRoles(pickedRoles);
        SceneManager.LoadScene("Gameplay");
    }

    private bool checkValidGame()
    {
        int nTown = 0;
        int nMafia = 0;
        int nNeutral = 0;

        for (int i = 0; i < nRoles; i++)
        {
            if (GD.GetRole(i).faction == Faction.TOWN)
                nTown += rolesQ[i];
            else if (GD.GetRole(i).faction == Faction.MAFIA)
                nMafia += rolesQ[i];
            else
                nNeutral += rolesQ[i];
        }

        if (nTown == 0)
        {
            textInvalidGame.GetComponent<Text>().text = LANG.noTownRoles[lang];
            return false;
        }

        if (nMafia == 0 && nNeutral == 0)
        {
            textInvalidGame.GetComponent<Text>().text = LANG.noMafiaOrNeutral[lang];
            return false;
        }

        if (nMafia >= nTown + nNeutral)
        {
            textInvalidGame.GetComponent<Text>().text = LANG.tooMuchMafia[lang];
            return false;
        }

        if (rolesQ[(int)RoleEnum.DEPUTY] == 1 && rolesQ[(int)RoleEnum.SHERIFF] == 0)
        {
            textInvalidGame.GetComponent<Text>().text = LANG.deputyNeedsSheriff[lang];
            return false;
        }

        if (rolesQ[(int)RoleEnum.JESTER] == 2 && rolesQ[(int)RoleEnum.EXECUTIONER] == 1)
        {
            textInvalidGame.GetComponent<Text>().text = LANG.executionerCantPlayWith2Jesters[lang];
            return false;
        }

        return true;
    }

    /*private void CloseLayout()
    {
        // Not return when picking moderator
        if (pickingModerator)
            return;

        // No turning back when picking roles
        if (go == layoutRoles)
            return;

        if (go == popupEditPlayer)
        {
            editingPlayer = false;
            bEdit.GetComponent<Image>().sprite = tabUnselected;
        }

        go.SetActive(false);
        openedLayouts.RemoveAt(openedLayouts.Count - 1);

        if (openedLayouts.Count == 0)
        {
            Application.Quit();
            Debug.LogWarning("Application Quit");
        }
        else
            openedLayouts[openedLayouts.Count - 1].SetActive(true);
    }*/
}