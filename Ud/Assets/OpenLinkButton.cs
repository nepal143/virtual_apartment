using UnityEngine;
using UnityEngine.UI;

public class OpenLinkButton : MonoBehaviour
{
    public string linkToOpen = "https://www.example.com";

    private Button button;

    void Start()
    {
        // Get the Button component attached to this GameObject
        button = GetComponent<Button>();

        // Add a listener to the button's onClick event
        button.onClick.AddListener(OpenLink);
    }

    void OpenLink()
    {
        // Open the link in a web browser
        Application.OpenURL(linkToOpen);
    }
}
