using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class VirtualButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private Image handleImage;

    public bool IsDown { get; private set; }

    void Start()
    {
        handleImage = transform.Find("Handle").GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //handleImage.transform.position = transform.position;
        IsDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDown = false;

        //UpdateHandle(Vector3.zero);
    }

}