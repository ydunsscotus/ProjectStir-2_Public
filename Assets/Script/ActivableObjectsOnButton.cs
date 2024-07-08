using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivableObjectsOnButton : MonoBehaviour
{
    public GameObject[] activeGameObjects;
    public GameObject[] deactiveGameObjects;
    private Button thisButton;
    void OnEnable()
    {
        thisButton = GetComponent<Button>();
    }

    private void Start()
    {
            thisButton.onClick.AddListener(DeacvtivedGameObjects);
            thisButton.onClick.AddListener(ActivatedGameObjects);
    }
    void DeacvtivedGameObjects()
    {
        for (int i=0; i<deactiveGameObjects.Length; i++)
        {
            deactiveGameObjects[i].SetActive(false);
        }
    }

    void ActivatedGameObjects()
    {
        for (int i=0; i<activeGameObjects.Length; i++)
        {
            activeGameObjects[i].SetActive(true);
        }
    }
}
