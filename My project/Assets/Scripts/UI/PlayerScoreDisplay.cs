using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text label;
    [SerializeField]private Image backgroundImage;
    public string Label
    {
        get
        {
            return label.text;
        }
        set
        {
            label.text = value;
        }
    }
    public Color LabelColour
    {
        get
        {
            return label.color;
        }
        set
        {
            label.color = value;
        }
    }
    public Image Background
    {
        get
        {
            return backgroundImage;
        }
        set
        {
            backgroundImage = value;
        }
    }
}
