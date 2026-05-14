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
}
