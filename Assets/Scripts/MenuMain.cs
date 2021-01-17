using UnityEngine;
using UnityEngine.UI;

public class MenuMain : MonoBehaviour
{
    public GameObject M;
    private SaveFile SF;
    private Languages LANG;

    public Text textSubtitle;
    public Text textPlay;
    public Text textPlayers;
    public Text textSettings;
    public Text textCredits;

    private void Awake()
    {
        SF = M.GetComponent<SaveFile>();
        LANG = M.GetComponent<Languages>();
        LANG.SetLanguage(SF.getLanguage());
    }

    public void OnEnable()
    {
        //Language
        textSubtitle.text = LANG.moderator[LANG.GetLanguage()];
        textPlay.text = LANG.play[LANG.GetLanguage()];
        textSettings.text = LANG.settings[LANG.GetLanguage()];
        textPlayers.text = LANG.players[LANG.GetLanguage()];
        textCredits.text = LANG.credits[LANG.GetLanguage()];

        textPlay.transform.parent.gameObject.SetActive(SF.getTotalPersons() > 0);
    }
}
