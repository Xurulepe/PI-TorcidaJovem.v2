using UnityEngine;
using UnityEngine.UI;

public class PlayAudioOnClick : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    private Button button;


    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(PlaylClickSound);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveListener(PlaylClickSound);        
    }

    private void PlaylClickSound()
    {
        AudioManager.Instance.PlaySFX(clickSound);
    }
}
