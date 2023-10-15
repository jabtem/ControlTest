using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerEventTest : MonoBehaviour,IPointerEnterHandler
{
    /* 부모에 OnPointerEnter가선언되있을때 자식 UI에도 이벤트가 호출되는지 테스트용*/
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(1);
    }

}
