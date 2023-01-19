using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class TutorialPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI indexCountText;
    [TextArea (10, 20)]
    [SerializeField] private List<string> textList;

    private GameManager gameManager;

    private GameObject locomotionSystem;

    private int textIndex = 0;


    private void OnEnable()
    {
        textIndex = 0;
        ShowText();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        locomotionSystem = FindObjectOfType<LocomotionSystem>().gameObject;
        locomotionSystem.gameObject.SetActive(false);
    }

    public void ShowText()
    {
        tutorialText.text = textList[textIndex];
        indexCountText.text = textIndex+1 + " / " + textList.Count;
    }

    public void PrevText()
    {
        if (textIndex-1 >= 0 )
        {
            textIndex--;
            ShowText();
        }
    }

    public void NextText()
    {
        if (textIndex + 1 < textList.Count)
        {
            textIndex++;
            ShowText();
        }
    }

    public void CloseTutorial()
    {
        gameManager.tutFinished = true;
        EnableLocomotion();
        DisableTutPanel();
    }

    void DisableTutPanel()
    {
        gameObject.SetActive(false);
    }

    void EnableLocomotion()
    {
        locomotionSystem.gameObject.SetActive(true);
    }
}
