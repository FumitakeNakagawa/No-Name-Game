using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomInput : BaseInputModule
{
    List<RaycastResult> resultList = new List<RaycastResult>();
    PointerEventData eventData;

    public override void Process()
    {
        resultList.Clear();
        
        eventSystem.RaycastAll(eventData, resultList);
    }

    protected override void Awake()
    {
        eventData = new PointerEventData(eventSystem);
    }
}
