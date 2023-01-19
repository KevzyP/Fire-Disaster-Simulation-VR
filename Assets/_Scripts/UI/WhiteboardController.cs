using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WhiteboardController : MonoBehaviour
{
    [SerializeField] private GameObject imageSlot;
    [SerializeField] private TextMeshProUGUI indexText;
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private List<Sprite> imageListEvacSim;
    [SerializeField] private List<Sprite> imageListExtSim;
    private GameManager gameManager;

    public void PreviousImage()
    {
        if (currentIndex - 1 >= 0 )
        {
            currentIndex--;
            LoadImage(currentIndex);
        }
    }

    public void NextImage()
    {
        if (currentIndex + 1 <= imageListEvacSim.Count-1 || currentIndex + 1 <= imageListExtSim.Count-1)
        {
            currentIndex++;
            LoadImage(currentIndex);
        }
    }

    public void LoadImage(int index)
    {
        switch (gameManager.currentScenario)
        {
            case 1:
                imageSlot.GetComponent<Image>().sprite = imageListExtSim[index];
                indexText.text = index + 1 + " / " + imageListExtSim.Count;
                break;

            case 2:
                imageSlot.GetComponent<Image>().sprite = imageListEvacSim[index];
                indexText.text = index + 1 + " / " + imageListEvacSim.Count;
                break;

            default:
                break;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameManager.Instance;
        LoadImage(currentIndex);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
