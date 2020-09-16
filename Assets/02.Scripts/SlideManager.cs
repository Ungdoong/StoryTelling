using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour {
    public Toggle play_toggle;
    public Button replay_button;
    public Button next_button;
    public Button prev_button;
    public Button menu_button;
    public AudioClip[] audio_clip;

    private AudioSource audio_source;
    private int current_audio = 0;

    private void Awake()
    {
        audio_source = GetComponent<AudioSource>();
        play_toggle.onValueChanged.AddListener(delegate { AudioPlay(play_toggle); });
        replay_button.onClick.AddListener(delegate { AudioReplay(); });
        if (next_button != null)
        {
            next_button.onClick.AddListener(delegate { Next_Button(next_button); });
        }
        if(prev_button != null)
        {
            prev_button.onClick.AddListener(delegate { Prev_Button(prev_button); });
        }
        if (menu_button != null)
        {
            menu_button.onClick.AddListener(delegate { Menu_Button(); });
        }

        audio_source.clip = audio_clip[current_audio++];
	}

    IEnumerator Playlist()
    {
        while (current_audio != 0)
        {
            yield return new WaitForSeconds(1.0f);
            if (!audio_source.isPlaying && current_audio < audio_clip.Length)
            {
                audio_source.clip = audio_clip[current_audio++];
                audio_source.Play();
            }
            else if (!audio_source.isPlaying && current_audio == audio_clip.Length)
            {
                play_toggle.isOn = true;
                play_toggle.GetComponent<ToggleController>().ToggleValueChanged(play_toggle);
                current_audio = 0;
            }
        }
    }

    public void Active_Slide(bool active)
    {
        if (active)
        {
            audio_source.Play();
            StartCoroutine("Playlist");
        }
        else
        {
            StopCoroutine("Playlist");
            audio_source.Stop();
            current_audio = 0;
        }
    }

    private void AudioPlay(Toggle pause)
    {
        if (pause.isOn)
        {
            audio_source.Pause();
        }
        else audio_source.Play();
    }

    private void AudioReplay()
    {
        audio_source.Stop();
        audio_source.clip = audio_clip[0];
        audio_source.Play();
        current_audio = 1;
    }

    private void Next_Button(Button bt)
    {
        string[] parentname = (bt.transform.parent.name).Split('-');
        int number = int.Parse(parentname[1]);
        string contentname = "Content" + parentname[0];

        GameObject.Find(contentname).GetComponent<ContentManager>().Move_Next(number - 1, number);
    }

    private void Prev_Button(Button bt)
    {
        string[] parentname = (bt.transform.parent.name).Split('-');
        int number = int.Parse(parentname[1]);
        string contentname = "Content" + parentname[0];

        GameObject.Find(contentname).GetComponent<ContentManager>().Move_Prev(number - 1, number - 2);
    }

    private void Menu_Button()
    {
        int content_number = int.Parse(this.gameObject.name.Split('-')[0]);

        Active_Slide(false);
        GameObject.Find("CanvasManager").GetComponent<PlayManager>().Move_Menu(content_number);
    }
}
