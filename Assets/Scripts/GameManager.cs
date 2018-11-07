using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject welcomeText;
    public GameObject startButton;
    public GameObject finalScore;
    public List<GameObject> sceneObjects;

    public bool sceneStart;
    public int caseNumber;
    public int score;

    private int caseNumbersMax;
    private float welcomeTimer = 0;
    private List<int> casesFinished;
    private bool levelChange;

    void Start()
    {
        welcomeText.SetActive(true);
        startButton.SetActive(false);
        finalScore.SetActive(false);


        score = 0;

        caseNumbersMax = sceneObjects.Count;
        casesFinished = new List<int>();
        levelChange = false;

        for (int i = 0; i < caseNumbersMax; i++)
        {
            sceneObjects[i].SetActive(false);
        }


        welcomeText.GetComponent<Text>().fontSize = 15;
        finalScore.GetComponent<Text>().fontSize = 40;
    }

    void Update()
    {
        if (!sceneStart)
        {
            if (welcomeTimer < 5)
            {
                welcomeText.GetComponent<Text>().fontSize += 1;
                welcomeTimer += 5 * Time.deltaTime;
            }
            else
            {
                startButton.SetActive(true);
            }
        }
        else
        {
            if (levelChange)
            {
                if (casesFinished.Count < caseNumbersMax)
                {
                    do
                    {
                        UnityEngine.Random.seed = DateTime.Now.Millisecond;
                        caseNumber = UnityEngine.Random.Range(0, caseNumbersMax);
                    }
                    while (casesFinished.Contains(caseNumber));
                    casesFinished.Add(caseNumber);
                    ChangeLevel();
                    levelChange = false;
                }
                else if (casesFinished.Count >= caseNumbersMax)
                {
                    welcomeText.SetActive(true);
                    welcomeText.GetComponent<Text>().fontSize = 60;
                    welcomeText.GetComponent<Text>().text = "GAME OVER";
                    finalScore.SetActive(true);
                    welcomeText.GetComponent<Text>().text = "Score : " + score.ToString();
                    levelChange = false;
                }
            }
            else
            {

            }
        }

    }

    public void changeLevel()
    {
        levelChange = true;
    }

    public void ChangeScore(int amount)
    {
        score += amount;
    }

    public void ChangeLevel()
    {
        for (int i = 0; i < caseNumbersMax; i++)
        {
            if (caseNumber == i)
            {
                sceneObjects[i].SetActive(true);
            }
            else
            {
                sceneObjects[i].SetActive(false);
            }
        }
    }

    public void StartButtonClicked()
    {
        startButton.SetActive(false);
        welcomeText.SetActive(false);
        sceneStart = true;
        levelChange = true;
    }
}
