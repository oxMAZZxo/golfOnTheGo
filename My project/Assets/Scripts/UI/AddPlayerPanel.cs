using TMPro;
using UnityEngine;

public class AddPlayerPanel : MonoBehaviour
{
    [SerializeField]private PlayMenu playMenu;
    [SerializeField]private TMP_InputField inputField;

    void Start()
    {
        if(inputField == null)
        {
            Debug.LogError("The player name input field has not been assigned on the AddPlayerPanel");
        }
    }

    public void AddPlayer()
    {
        if(string.IsNullOrEmpty(inputField.text) || string.IsNullOrWhiteSpace(inputField.text)) {return;}
        playMenu.AddPlayer(inputField.text);
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        inputField.text = string.Empty;
    }
}
