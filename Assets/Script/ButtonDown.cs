using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonDown : MonoBehaviour, IPointerDownHandler
{
    
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.GetComponent<GameManager>().ButtonDo.Play();
    }
}
