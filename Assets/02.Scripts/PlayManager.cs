using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour {

    private CanvasGroup menu_canvas;
    public CanvasGroup[] contents = new CanvasGroup[1];
    public GameObject menu;

    private void Awake()
    {
        menu.SetActive(true);
        menu_canvas = GameObject.Find("MenuCanvas").GetComponent<CanvasGroup>();
        Initializer();
    }

    public void Initializer()
    {
        foreach (CanvasGroup content in contents)
        {
            content.alpha = 0;
            content.interactable = false;
        }
        menu_canvas.alpha = 1;
        menu_canvas.interactable = true;
    }

    public void Move_Content(int content_number)
    {
        if (contents[content_number - 1])
        {
            StartCoroutine(DoGroupFade(menu_canvas, contents[content_number - 1]));
        }
    }

    public void Move_Menu(int content_number)
    {
        string contentname = "Content" + content_number.ToString();
        StartCoroutine(DoGroupFade(contents[content_number - 1], menu_canvas));
        GameObject.Find(contentname).GetComponent<ContentManager>().ContentInitializer();
    }

    IEnumerator DoGroupFade(CanvasGroup prev, CanvasGroup next)
    {
        prev.interactable = false;
        next.interactable = true;

        while (prev.alpha > 0)
        {
            prev.alpha -= Time.deltaTime;
            next.alpha += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }
}
