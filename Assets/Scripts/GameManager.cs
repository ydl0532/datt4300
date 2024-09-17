using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
public class ScriptData
{

    public int loadType;//1 background 2 character 3 event
    public string name;
    public string spriteName;
    public string dialogueContent;
    public int characterPos;//1. left 2. right 3.mid
    public bool ifRotate;
    public int toxicability;//changed value
    public int energyValue;//changed value
    public int characterID;//The ID of the three-person talk
    //1. options type 2. 
    public int eventID;
    //1. some option
    public int eventData;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private List<ScriptData> scriptDatas;
    private int scriptIndex;
    public int energyValue;//the charater 
    public Dictionary<string, int> toxicValueDic;//The salesperson's favorable impression of the player

    private void Awake()
    {
        Instance = this;
        scriptDatas = new List<ScriptData>()
        {
            new ScriptData()
            {
               loadType =1, spriteName= "Title",characterPos= 2
            },
            new ScriptData()
            {
               loadType =2, name= "Test", dialogueContent= "Hello, I just bought some items from your store",characterPos= 2,energyValue= 10,
            },
            new ScriptData()
            {
               loadType =2, name= "Test", dialogueContent= "and I'm extremely dissatisfied with their quality.",characterPos= 2
            },
            new ScriptData()
            {
               loadType =2, name= "Test", dialogueContent= "you are bad.",characterPos= 2,toxicability= 5,
            },
            new ScriptData()
            {
               loadType =2, name= "Test", dialogueContent= "Why would you sell me this?",characterPos= 1,ifRotate =true
            },
            new ScriptData()
            {
               loadType =2, name= "Test", dialogueContent= "you guys suck.",characterPos= 3,energyValue= -10,
            },
            new ScriptData()
            {
               loadType =2, name= "Debug", dialogueContent= "Excuse me? Karen",characterPos= 1,characterID=1,ifRotate=true,
            },
            new ScriptData()
            {
               loadType =2, name= "Debug", dialogueContent= "Excuse me?Excuse me? Karen",characterPos= 1,ifRotate=true,
            },
            new ScriptData()
            {
               loadType =2, name= "Debug", dialogueContent= "Excuse me?Excuse me?Excuse me? Karen",characterPos= 1,ifRotate=true,
            },

            new ScriptData()
            {
               loadType =2, name= "Debug", dialogueContent= "Karen,You only bought a bottle of water.",characterPos= 1,energyValue= -50,ifRotate=true,
            },
            new ScriptData()
            {
               loadType=3, eventID=1,eventData=3,
            },
            new ScriptData()
            {
               loadType =2, name= "Debug", dialogueContent= "ok.",characterPos= 1,ifRotate=true,
            },
            new ScriptData()
            {
               loadType =2, name= "Debug", dialogueContent= "no.",characterPos= 1,ifRotate=true,
            },
            new ScriptData()
            {
               loadType =2, name= "Debug", dialogueContent= "ooooooooookkkkkkkkkkk.",characterPos= 1,ifRotate=true,
            },

        };
        scriptIndex = 0;
        HandleData();
        energyValue = 100;
        ChangeEnergyValue(energyValue);

        toxicValueDic = new Dictionary<string, int>()
        {
            { "Player",0},
            { "Test",80},
            { "Debug",60},

        };
    }
    private void HandleData()
    {
        if (scriptIndex >= scriptDatas.Count)
        {
            Debug.Log("GAME END");
            return;
        }
        if (scriptDatas[scriptIndex].loadType == 1)
        {
            //background
            //setup backgrounds
            SetBGImageSprite(scriptDatas[scriptIndex].spriteName);
            //load next
            LoadNextScript();
        }
        else if (scriptDatas[scriptIndex].loadType == 2)
        {
            HandleCharacter();
        }
        else if (scriptDatas[scriptIndex].loadType == 3)//event
        {
            switch (scriptDatas[scriptIndex].eventID)
            {
                case 1:
                    ShowChoiceUI(scriptDatas[scriptIndex].eventData, GetChoiceContent(scriptDatas[scriptIndex].eventData));
                    break;
                default:
                    break;
            }
        }
        else
        {
            LoadNextScript();
        }

    }

    private void SetBGImageSprite(string spriteNmae)
    {
        UIManager.Instance.SetBGImageSprite(spriteNmae);
    }

    public void LoadNextScript()
    {
        Debug.Log("LOAD NEXT");
        scriptIndex++;
        HandleData();
    }

    private void ShowCharacter(string name, int characterID = 0)
    {
        UIManager.Instance.ShowCharacter(name, characterID);
    }

    //update text
    private void UpdateTalkLineText(string dialogueContent)
    {
        UIManager.Instance.UpdateTalkLineText(dialogueContent);
    }

    public void SetCharacterPos(int posID, bool ifRotate = false, int characterID = 0)
    {
        UIManager.Instance.SetCharacterPos(posID, ifRotate, characterID);

    }

    public void ChangeEnergyValue(int value = 0)
    {

        if (value == 0)
        {
            UpdateEnergyValue(energyValue);
            return;
        }
        if (value > 0)
        {

        }
        energyValue += value;
        if (energyValue >= 100)
        {
            energyValue = 100;
        }
        else if (energyValue <= 0)
        {
            energyValue = 0;
        }
        UpdateEnergyValue(energyValue);
    }

    //update UI
    public void UpdateEnergyValue(int value = 0)
    {
        UIManager.Instance.UpdateEnergyValue(value);

    }


    public void ChangeToxicValue(int value = 0, string name = null)
    {
        if (value == 0)
        {
            return;
        }
        if (value > 0)
        {

        }
        toxicValueDic[name] += value;
        if (toxicValueDic[name] >= 100)
        {
            toxicValueDic[name] = 100;
        }
        else if (toxicValueDic[name] <= 0)
        {
            toxicValueDic[name] = 0;
        }
        UpdateToxicValue(toxicValueDic[name], name);
    }

    public void UpdateToxicValue(int value = 0, string name = null)
    {
        UIManager.Instance.UpdateToxicValue(value, name);
    }

    public void HandleCharacter()
    {
        //character
        //show character
        ShowCharacter(scriptDatas[scriptIndex].name, scriptDatas[scriptIndex].characterID);
        //update text
        UpdateTalkLineText(scriptDatas[scriptIndex].dialogueContent);
        //set pos
        SetCharacterPos(scriptDatas[scriptIndex].characterPos, scriptDatas[scriptIndex].ifRotate, scriptDatas[scriptIndex].characterID);
        //update two value
        ChangeEnergyValue(scriptDatas[scriptIndex].energyValue);
        ChangeToxicValue(scriptDatas[scriptIndex].toxicability, scriptDatas[scriptIndex].name);
    }

    public void ShowChoiceUI(int choiceNum, string[] choiceContent)
    {
        UIManager.Instance.ShowChoiceUI(choiceNum, choiceContent);
    }

    public string[] GetChoiceContent(int num)
    {
        string[] choiceContent = new string[num];

        for (int i = 0; i < num; i++)
        {
            choiceContent[i] = scriptDatas[scriptIndex+1+i].dialogueContent;
        }
        return choiceContent;
    }
}

