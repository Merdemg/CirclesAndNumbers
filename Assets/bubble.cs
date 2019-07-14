﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum position
{
    top, topRight, right, bottomRight, bottom, bottomLeft, left, topLeft
}


public class bubble : MonoBehaviour
{
    [SerializeField] bool isActive = false;
    [SerializeField] int value = 0;
    TextMeshProUGUI textBox;
    [SerializeField] position myPos;

    // Start is called before the first frame update
    void Start()
    {
        textBox = GetComponentInChildren<TextMeshProUGUI>();
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateUI()
    {
        if (isActive)
        {
            textBox.text = value.ToString();
        }
        else
        {
            textBox.text = "";
        }
        
    }

    public bool getIsActive()
    {
        return isActive;
    }

    public position getPosition()
    {
        return myPos;
    }

    public int getValue()
    {
        return value;
    }

    public void collide(int v)
    {
        value += v;

        if (value <= 0)
        {
            isActive = false;
        }

        updateUI();
    }
}
