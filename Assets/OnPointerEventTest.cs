using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnPointerEventTest : MonoBehaviour,IPointerEnterHandler
{
    /* �θ� OnPointerEnter������������� �ڽ� UI���� �̺�Ʈ�� ȣ��Ǵ��� �׽�Ʈ��*/
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log(1);
    }

}
