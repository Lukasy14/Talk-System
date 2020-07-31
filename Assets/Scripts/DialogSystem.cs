using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [Header("UI组件")]
    public Text textLabel;
    public Image faceImage;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("头像")]
    public Sprite faceA, faceB;



    bool textFinished;
    bool cancelTyping;

    List<string> textList = new List<string>();

    void Awake()
    {
        GetTextFromFile(textFile);
    }

    private void OnEnable() 
    {
        // textLabel.text = textList[index];
        // index++;
        textFinished = true;
        StartCoroutine(SetTextUI());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && index == textList.Count && textFinished == true)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        // if(Input.GetKeyDown(KeyCode.R) && textFinished == true)
        // {
        //     // textLabel.text = textList[index];
        //     // index++;
        //     StartCoroutine(SetTextUI());
        // }
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(textFinished == true && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if(!textFinished)
            {
                cancelTyping = true;
            }
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var linDate = file.text.Split('\n');

        foreach (var line in linDate)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";
        switch(textList[index])
        {
            case "A":
                faceImage.sprite = faceA;
                index++;
                break;
            case "B":
                faceImage.sprite = faceB;
                index++;
                break;
        }

        // for(int i = 0; i < textList[index].Length; i++)
        // {
        //     textLabel.text += textList[index][i];
        //     yield return new WaitForSeconds(textSpeed);
        // }
        int letter = 0;
        while(!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }




}
