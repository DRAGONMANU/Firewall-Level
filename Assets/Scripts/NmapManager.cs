using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NmapManager : MonoBehaviour
{
    public GameObject defender;
    public GameObject scanBox;
    public GameObject startInput;
    public GameObject endInput;

    public bool printing;

    private List<int> ports;
    private int startPort;
    private int endPort;
    private string scanResult;
    private int history;
    private float current;

    void Start()
    {
        printing = false;
    }

    void Update()
    {
        if (printing)
        {
            current += Time.deltaTime;
            if ((int)current == history)
                return;
            history = (int)current;
            if (history >= ports.Count)
            {
                printing = false;
                return;
            }
            if (ports[history] <= endPort && ports[history] >= startPort)
                    scanResult += ports[history].ToString() + " is Open\n";
            NmapScan();
            
        }
    }

    public void UpdateResult()
    {
        try
        {
            startPort = int.Parse(startInput.GetComponent<InputField>().text);
            endPort = int.Parse(endInput.GetComponent<InputField>().text);
        }
        catch (Exception e)
        {
            startPort = 0;
            endPort = 65535;
        }
        ports = defender.GetComponent<DefenderGameManager>().ports;
        if (ports.Count > 0)
        {
            ports.Sort();
            scanResult = "";
            printing = true;
            current = 0;
            history = -1;
        }
    }

    public void NmapScan()
    {
        scanBox.GetComponent<Text>().text = scanResult;
    }
}