using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContentManager : MonoBehaviour {

    CanvasGroup cg;
    bool stop_content = true;

    private CanvasGroup[] slides;
    public GameObject[] slides_object = new GameObject[1];

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        slides = new CanvasGroup[slides_object.Length];
        int i = 0;
        foreach (GameObject obj in slides_object)
        {
            obj.SetActive(true);
            slides[i++] = obj.GetComponent<CanvasGroup>();
        }

        foreach (CanvasGroup slide in slides)
        {
            slide.alpha = 0;
            slide.interactable = false;
            slide.blocksRaycasts = false;
        }
    }

    private void Update()
    {
        if(cg.interactable == true && stop_content == true)
        {
            stop_content = false;
            StartContent();
        }
    }

    public void ContentInitializer()
    {
        foreach (CanvasGroup slide in slides)
        {
            slide.alpha = 0;
            slide.interactable = false;
            slide.blocksRaycasts = false;
        }
        stop_content = true;
    }

    private void StartContent()
    {
        slides[0].alpha = 1;
        slides[0].interactable = true;
        slides[0].blocksRaycasts = true;
        slides[0].GetComponent<SlideManager>().Active_Slide(true);
    }

    public void Move_Next(int current, int next)
    {
        StartCoroutine(DoFadeNext(slides[current], slides[next]));
    }

    IEnumerator DoFadeNext(CanvasGroup current, CanvasGroup next)
    {
        current.GetComponent<SlideManager>().Active_Slide(false);

        current.interactable = false;
        current.blocksRaycasts = false;

        while (current.alpha > 0)
        {
            current.alpha -= Time.deltaTime * 2;
            next.alpha += Time.deltaTime * 2;
            yield return null;
        }

        next.GetComponent<SlideManager>().Active_Slide(true);

        next.interactable = true;
        next.blocksRaycasts = true;

        yield return null;
    }

    public void Move_Prev(int current, int prev)
    {
        StartCoroutine(DoFadePrev(slides[current], slides[prev]));
    }

    IEnumerator DoFadePrev(CanvasGroup current, CanvasGroup prev)
    {
        current.GetComponent<SlideManager>().Active_Slide(false);

        prev.alpha = 1;
        current.interactable = false;
        current.blocksRaycasts = false;

        while (current.alpha > 0)
        {
            current.alpha -= Time.deltaTime * 2f;
            prev.alpha += Time.deltaTime * 2;

            yield return null;
        }

        prev.GetComponent<SlideManager>().Active_Slide(true);

        prev.interactable = true;
        prev.blocksRaycasts = true;

        yield return null;
    }
}
