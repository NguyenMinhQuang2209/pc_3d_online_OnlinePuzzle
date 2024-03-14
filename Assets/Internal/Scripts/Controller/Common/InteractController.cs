using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    public static InteractController instance;

    public TextMeshProUGUI promptTxt;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        InteractText("");
    }
    public void InteractText(string txt)
    {
        promptTxt.text = txt;
    }
}
