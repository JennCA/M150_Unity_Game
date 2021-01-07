using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
     {
         text.color = Color.yellow;
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
         text.color = Color.white;
     }
}
