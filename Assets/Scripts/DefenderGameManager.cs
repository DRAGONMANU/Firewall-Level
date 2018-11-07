using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DefenderGameManager : MonoBehaviour
{

    public GameObject totalCoins;
    public GameObject settings;
    public GameObject nmapScan;
    public GameObject portBlocker;
    public GameObject packetAnalyser;
    public GameObject wireShark;
    public GameObject hintBox;
    public GameObject feedbackBox;
    public GameObject IPSliderWindow;
    public GameObject GraphWindow;
    public GameObject scanBox;


    public List<GameObject> tutorialBoxes;
    public List<GameObject> objectives;


    public int level;
    public int coins;
    public List<int> ports;
    public List<int> SolutionPorts;

    private float time;
    private bool paused;
    private float negativeCoinChange; //Used to provide hints after failing too many times
    private int tutorialNumber;
    private int coinsTarget;
    public bool levelPassed;

    // Use this for initialization
    void Start()
    {
        settings.SetActive(false);
        //nmapScan.SetActive(false);
        //portBlocker.SetActive(false);
        //packetAnalyser.SetActive(false);
        //wireShark.SetActive(false);
        //hintBox.SetActive(false);
        feedbackBox.SetActive(false);
        IPSliderWindow.SetActive(false);
        GraphWindow.SetActive(false);

        ports = new List<int> { 80, 53, 443, 25, 100, 125, 20, 21, 22, 23 };
        SolutionPorts = new List<int> { 80, 443, 53, 25, 20, 21 };

        negativeCoinChange = 0;
        tutorialNumber = 0; //to be set 0 later
        level = 0; // to be set to 0 later
        coins = 100;
        coinsTarget = 100;

        levelPassed = false;

        for (int i = 0; i < objectives.Count; i++)
        {
            objectives[i].SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (tutorialNumber < 8)
        {
            tutorialBoxes[tutorialNumber].SetActive(true);
            HighlightObject.Highlight(tutorialNumber);
        }
        else
        {
            HighlightObject.StopIt();

            if (coins < coinsTarget)
                coins++;
            else if (coins > coinsTarget)
                coins--;


            totalCoins.GetComponentInChildren<Text>().text = coins.ToString();

            if (!paused)
            {
                negativeCoinChange += Time.deltaTime;
                time += Time.deltaTime;
                if (negativeCoinChange > 5.0f)
                {
                    HighlightObject.Highlight(1);
                }
            }

            if (level == 0)
            {
                var firstNotSecond = ports.Except(SolutionPorts).ToList();
                var secondNotFirst = SolutionPorts.Except(ports).ToList();
                if (!firstNotSecond.Any() && !secondNotFirst.Any())
                {
                    ShowNmapScan(0);
                    ShowPortBlocker(0);
                    feedbackBox.GetComponentInChildren<Text>().text = "Nice! You have completed this level";
                    feedbackBox.SetActive(true);
                }
            }
            if (level == 2)
            {
                if (!levelPassed)
                { 
                    IPSliderWindow.SetActive(true);
                }
                else
                {
                    IPSliderWindow.SetActive(false);
                    feedbackBox.GetComponentInChildren<Text>().text = "Nice! You have completed this level";
                    feedbackBox.SetActive(true);
                }
            }
            if (level == 1)
            {
                if (!levelPassed)
                {
                    GraphWindow.SetActive(true);
                }
                else
                {
                    GraphWindow.SetActive(false);
                    feedbackBox.GetComponentInChildren<Text>().text = "Nice! You have completed this level";
                    feedbackBox.SetActive(true);
                }
            }
        }
    }

    public void UpdateCoins(int change)
    {
        coinsTarget += change;
        if (change < 0)
            negativeCoinChange -= change;
        if (coinsTarget < 0)
            coinsTarget = 0;
    }

    public void TutorialNext()
    {
        tutorialBoxes[tutorialNumber].SetActive(false);
        tutorialNumber++;
    }


    public void NextLevel()
    {
        levelPassed = false;
        feedbackBox.SetActive(false);
        level++;
        ShowObjectives(1);
    }


    public void ShowObjectives(int state)
    {
        if (tutorialNumber < 8)
            return;

        if (state == 1)
        {
            objectives[level].SetActive(true);
        }
        else
        {
            for (int i = 0; i < objectives.Count; i++)
            {
                objectives[i].SetActive(false);
            }
        }
    }

    public void ShowHint(int state)
    {
        if (tutorialNumber < 8)
            return;
        if (state == 1)
        {
            UpdateCoins(-20);
            negativeCoinChange = 0;
            HighlightObject.StopIt();
            hintBox.SetActive(true);
            //hintBox.GetComponentInChildren<Text>().text = 
        }
        else
        {
            hintBox.SetActive(false);
        }
    }

    public void ShowSettings(int state)
    {
        if (tutorialNumber < 8)
            return;
        if (state == 1)
        {
            settings.SetActive(true);
            paused = true;
        }
        else if (state == 2)
        {
            Application.Quit();
        }
        else
        {
            settings.SetActive(false);
            paused = false;
        }
    }

    public void ShowNmapScan(int state)
    {
        if (tutorialNumber < 8)
            return;
        if (state == 1)
        {
            nmapScan.SetActive(true);
        }
        else
        {
            scanBox.GetComponent<Text>().text = "";
            transform.GetComponent<NmapManager>().printing = false;
            nmapScan.SetActive(false);
        }
    }

    public void ShowPortBlocker(int state)
    {
        if (tutorialNumber < 8)
            return;
        if (state == 1)
        {
            portBlocker.SetActive(true);
        }
        else
        {
            portBlocker.SetActive(false);
        }
    }

    public void ShowPacketAnalyser(int state)
    {
        if (state == 1)
        {
            packetAnalyser.SetActive(true);
        }
        else
        {
            packetAnalyser.SetActive(false);
        }
    }

    public void ShowWireShark(int state)
    {
        if (tutorialNumber < 8)
            return;
        if (state == 1)
        {
            wireShark.SetActive(true);
        }
        else
        {
            wireShark.SetActive(false);
        }
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
