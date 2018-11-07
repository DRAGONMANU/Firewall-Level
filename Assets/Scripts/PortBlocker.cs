using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortBlocker : MonoBehaviour {
    public GameObject defender;
    public GameObject blockInput;
    public GameObject allowInput;

    private List<int> ports;


    public void BlockerTool(bool op)
    {
        int port;
        try
        {
            if (op) //block pressed
            {
                port = int.Parse(blockInput.GetComponent<InputField>().text);
                UpdatePorts(port, true);
            }
            else
            {
                port = int.Parse(allowInput.GetComponent<InputField>().text);
                UpdatePorts(port, false);
            }
        }
        catch (Exception e)
        { }
        blockInput.GetComponent<InputField>().text = "";
        allowInput.GetComponent<InputField>().text = "";
    }

    void UpdatePorts (int port, bool block) {
        ports = defender.GetComponent<DefenderGameManager>().ports;
        if (block)
        {
            if (ports.Contains(port))
            {
                ports.Remove(port);
            }
        }
        else
        {
            if (!ports.Contains(port))
            {
                ports.Add(port);
                ports.Sort();
            }
        }

        defender.GetComponent<DefenderGameManager>().ports = ports;
    }
}
