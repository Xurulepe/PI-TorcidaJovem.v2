using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ManequimDrop : MonoBehaviour, IDropHandler
{
    public Image camisaCostas;
    public Image camisaFrente;

    public Image calcinha;
    public Image saia;

    public void OnDrop(PointerEventData eventData)
    {
        DragRoupa roupa =
            eventData.pointerDrag.GetComponent<DragRoupa>();

        if (roupa == null)
            return;

        switch (roupa.tipo)
        {
            case TipoRoupa.CamisaCostas:
                camisaCostas.sprite = roupa.GetSprite();
                break;

            case TipoRoupa.CamisaFrente:
                camisaFrente.sprite = roupa.GetSprite();
                break;

            case TipoRoupa.Calcinha:
                calcinha.sprite = roupa.GetSprite();
                break;

            case TipoRoupa.Saia:
                saia.sprite = roupa.GetSprite();
                break;
        }
    }
}