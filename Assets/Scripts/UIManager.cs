using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private Image[] dialogueBackgrounds;

    private void Start()
    {
        instance = this;
    }
    public void SetDialogueBackground(Sprite newBackground)
    {
        foreach(Image i in dialogueBackgrounds)
        {
            i.sprite = newBackground;
        }
    }

}
