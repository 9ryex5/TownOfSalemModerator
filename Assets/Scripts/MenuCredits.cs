using UnityEngine;
using UnityEngine.UI;

public class MenuCredits : MonoBehaviour
{
    public GameObject M;
    private Languages LANG;

    public Text textMadeBy;

    private void Awake()
    {
        LANG = M.GetComponent<Languages>();
    }

    private void OnEnable()
    {
        textMadeBy.text = LANG.madeBy[LANG.GetLanguage()];
    }
}
