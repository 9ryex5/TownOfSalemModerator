using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    public Text myText;
    public GameObject mySeparator;
    public Text myText1;
    public Text myText2;

    public void StartThis(string _text1, string _text2, Color _textColor)
    {
        if (_text2 == string.Empty)
        {
            myText.gameObject.SetActive(true);
            myText1.gameObject.SetActive(false);
            mySeparator.SetActive(false);
            myText2.gameObject.SetActive(false);
            myText.text = _text1;
        }
        else
        {
            myText.gameObject.SetActive(false);
            myText1.gameObject.SetActive(true);
            mySeparator.SetActive(true);
            myText2.gameObject.SetActive(true);
            myText1.text = _text1;
            myText2.text = _text2;
        }

        myText.color = _textColor;
        mySeparator.GetComponent<Text>().color = _textColor;
        myText1.color = _textColor;
        myText2.color = _textColor;
    }
}

