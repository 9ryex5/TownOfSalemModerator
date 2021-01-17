using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Person myPerson;
    private Button myButton;
    private Image myImage;
    public Text myText;
    public GameObject mySeparator;
    public Text myText1;
    public Text myText2;

    public Sprite asList;
    public Sprite asButtonDeselected;
    public Sprite asButtonSelected;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        myImage = GetComponent<Image>();
    }

    public Person GetPerson()
    {
        return myPerson;
    }

    public void setPerson(Person _person)
    {
        myPerson = _person;
    }

    public Button getButton()
    {
        return myButton;
    }

    public Text getText()
    {
        return myText;
    }

    public void SetAsList()
    {
        myImage.sprite = asList;
        myText.color = Color.white;
        myText.fontStyle = FontStyle.Normal;
        myButton.interactable = false;
    }

    public void setAsButtonUnselected()
    {
        myImage.sprite = asButtonDeselected;
        myText.color = Color.white;
        myText.fontStyle = FontStyle.Normal;
        setInteractable(true);
    }

    public void setAsButtonUnselected(Color _color)
    {
        myImage.sprite = asButtonDeselected;
        myText.color = _color;
        myText.fontStyle = FontStyle.Normal;
        setInteractable(true);
    }

    public void setAsButtonSelected()
    {
        myImage.sprite = asButtonSelected;
        myText.color = Color.yellow;
        myText.fontStyle = FontStyle.Bold;
    }

    public void setInteractable(bool value)
    {
        myButton.interactable = value;

        if (value)
            myButton.GetComponent<Image>().color = Color.white;
        else
            myButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
    }

    public void setSingleText()
    {
        myText.gameObject.SetActive(true);
        myText1.gameObject.SetActive(false);
        mySeparator.SetActive(false);
        myText2.gameObject.SetActive(false);
    }

    public void setDoubleText()
    {
        myText.gameObject.SetActive(false);
        myText1.gameObject.SetActive(true);
        mySeparator.SetActive(true);
        myText2.gameObject.SetActive(true);
    }

    public void setText(string text)
    {
        myText.text = text;
    }

    public void setText1(string text)
    {
        myText1.text = text;
    }

    public void SetText2(string text)
    {
        myText2.text = text;
    }

    public void setTextColor(Color _color)
    {
        myText.color = _color;
        mySeparator.GetComponent<Text>().color = _color;
        myText1.color = _color;
        myText2.color = _color;
    }
}
