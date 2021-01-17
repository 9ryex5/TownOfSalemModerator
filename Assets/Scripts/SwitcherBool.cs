using UnityEngine;
using UnityEngine.UI;

public class SwitcherBool : MonoBehaviour
{

    public Text myText;
    private string optionTrue;
    private string optionFalse;
    private bool state;

    public void buttonSwitch()
    {
        setState(!state);
    }

    public bool getState()
    {
        return state;
    }

    public void setState(bool _state)
    {
        state = _state;
        updateText();
    }

    public void setOptionTrue(string _option)
    {
        optionTrue = _option;
    }

    public void setOptionFalse(string _option)
    {
        optionFalse = _option;
    }

    private void updateText()
    {
        myText.text = state ? optionTrue : optionFalse;
    }

}
