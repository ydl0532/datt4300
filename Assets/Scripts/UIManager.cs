using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using TMPro;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    public Image imgBG;
    public Image imgCharacter;
    public Image imgCharacter2;
    public TMP_Text textName;
    public TMP_Text textTalkLine;
    public GameObject talkLineGo;
    public Transform[] characterPosTrans;
    public TMP_Text textEnergyValue;
    public TMP_Text textToxicValue;
    public GameObject empChoiceUIGo;
    public GameObject[] choiceUIGos;
    public TMP_Text[] textChoiceUIs;

    private void Awake()
    {
        Instance = this; 
    }

    /// <summary>
    /// set BG image
    /// </summary>
    /// <param name="spriteName"></param>
    public void SetBGImageSprite(string spriteName)
    { 
       imgBG.sprite=Resources.Load<Sprite>("Sprites/BG/"+ spriteName);
    }

    /// <summary>
    /// set Character image
    /// </summary>
    /// <param name="name"></param>
    public void ShowCharacter(string name,int characterID=0) 
    {
        ShowOrHideTalkLine();
        textName.text = name;
        if (characterID == 0)
        {
            imgCharacter.sprite = Resources.Load<Sprite>("Sprites/Characters/" + name);
            imgCharacter.SetNativeSize();
            imgCharacter.gameObject.SetActive(true);
        }
        else 
        {
            imgCharacter2.sprite = Resources.Load<Sprite>("Sprites/Characters/" + name);
            imgCharacter2.SetNativeSize();
            imgCharacter2.gameObject.SetActive(true);
        }
    }

    public void UpdateTalkLineText(string dialogueContent)
    {
        textTalkLine.text = dialogueContent;
    }

    public void SetCharacterPos(int posID, bool ifRotate = false,int characterID=0)
    {
        if (characterID == 0)
        {
            SetPos(posID, imgCharacter, ifRotate);
        }
        else
        {
            SetPos(posID, imgCharacter2, ifRotate);
        }

    }
    public void SetPos(int posID, Image imgTargeCharacter, bool ifRotate = false) 
    {
        imgTargeCharacter.transform.localPosition = characterPosTrans[posID - 1].localPosition;
        if (ifRotate)
        {
            imgTargeCharacter.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            imgTargeCharacter.transform.eulerAngles = Vector3.zero;
        }
    }

    public void UpdateEnergyValue(int value = 0)
    {
        textEnergyValue.text = value.ToString();
    }

    public void UpdateToxicValue(int value = 0, string name = null)
    {
       textToxicValue.text = value.ToString();  

    }

    public void ShowChoiceUI(int choiceNum, string[] choiceContent) 
    {
       empChoiceUIGo.SetActive(true);
       ShowOrHideTalkLine(false);
        for (int i = 0; i < choiceUIGos.Length; i++)
        {
            choiceUIGos[i].SetActive(false);
        }
        for (int i = 0; i < choiceNum; i++)
        {
            choiceUIGos[i].SetActive(true);
            textChoiceUIs[i].text = choiceContent[i];
        }
    }

   

    public void CloseChoiceUI(int choiceNum, string[] choiceContent) 
    {
        empChoiceUIGo.SetActive(false) ;
    }

    public void ShowOrHideTalkLine(bool show = true)
    { 
      talkLineGo.SetActive(show);
    }
}
