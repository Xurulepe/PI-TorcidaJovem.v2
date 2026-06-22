using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button autoSelectButton;

    private void OnEnable()
    {
        if (autoSelectButton != null)
        {
            autoSelectButton.Select(); 
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetNewAutoSelectButton(Button newAutoSelectButton)
    {
        autoSelectButton = newAutoSelectButton;
    }
}
