using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralTextController : MonoBehaviour
{
    public static GeneralTextController generalTextControllerController;

    private void Awake()
    {
        generalTextControllerController = this;
    }

    public GeneralText text;
    public Transform textTransform;
    private readonly List<GeneralText> _tmpTexts = new();

    public void ShowText(string text, Vector3 location)
    {
        GeneralText newText = GetFromPool();
        newText.Setup(text);
        newText.gameObject.SetActive(true);

        newText.transform.position = location;
    }
    
    
    public GeneralText GetFromPool()
    {
        GeneralText pooledText = null;

        if (_tmpTexts.Count == 0)
        {
            pooledText = Instantiate(text, textTransform);
        }
        else
        {
            pooledText = _tmpTexts[0];
            _tmpTexts.RemoveAt(0);
        }
        return pooledText;
    }

    public void PlaceInPool(GeneralText textToAdd)
    {
        textToAdd.gameObject.SetActive(false);
        _tmpTexts.Add(textToAdd);
    }
}
