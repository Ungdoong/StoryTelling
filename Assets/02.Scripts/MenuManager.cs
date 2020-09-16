using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour {

    public PlayManager pm;

    public void Enter_Content()
    {
        string content_name = EventSystem.current.currentSelectedGameObject.name;
        content_name = content_name.Replace("Content", "");
        content_name = content_name.Replace("Button", "");

        int content_number = int.Parse(content_name);

        pm.Move_Content(content_number);
    }
}
