using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour {

    Toggle toggle;

    public Image target_image;
    public Sprite default_image;
    public Sprite active_image;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = false;
        target_image.sprite = default_image;
        toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggle); });
    }

    public void ToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
            target_image.sprite = active_image;
        }
        else target_image.sprite = default_image;
    }
}
