using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : UiController
{
    public Image img;

    [SerializeField]public List<Sprite> puzzleSprites = new List<Sprite>();
    protected override void Awake()
    {
        img = transform.Find("Slot1").GetComponentInChildren<Image>();
        //puzzleSprites.AddRange(Resources.LoadAll("Sprites", typeof(Sprite))
            //.Cast<Sprite>().ToArray());
        
    }

    protected override void Start()
    {
        GameEvents.onItemPicked.AddListener(UpdatePlaceHolder);
        GameEvents.onInventoryClear.AddListener(CleanPlaceHolder);
    }

    void UpdatePlaceHolder(GameObject _go)
    {
        //Fun��o que mudar� colocar� o item no placeholder
        switch(_go.gameObject.name)
        {
            case "FirstPuzzle":
                img.sprite = puzzleSprites[0];
                img.color = Color.red;
                img.enabled = true;
                Debug.Log("Trocando sprite");
                break;
            case "SecondPuzzle":
                img.sprite = puzzleSprites[1];
                img.color = Color.blue;
                img.enabled = true;
                break;
            case "ThirdPuzzle":
                img.sprite = puzzleSprites[2];
                img.color = Color.cyan;
                img.enabled = true;
                break;
            default:
                break;
        }
    }
    void CleanPlaceHolder()
    {
        img.sprite = null;
        img.enabled = false;
    }
}
