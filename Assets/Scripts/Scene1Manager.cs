using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene1Manager : MonoBehaviour
{

    public GameObject welcomeText;
    public GameObject storyText;
    public GameObject portButtons;

    public int score;
    List<int> answers;
    List<string> questions;
    List<int> options;

    private float welcomeTimer;
    private bool playing;
    private int roundNumber;

    void Start()
    {
        welcomeText.SetActive(true);
        storyText.SetActive(false);
        portButtons.SetActive(false);

        welcomeText.GetComponent<Text>().color = new Color(1, 0, 0, 0);
        score = 0;
        answers.Add(0);
        answers.Add(2);
        answers.Add(1);
        questions.Add("Email ports are unfiltered by company. Use them to infiltrate inside\n\nHint: Company uses SMTP for handling mails");
        options.Add(25);
        options.Add(28);
        options.Add(80);
        options.Add(243);

        questions.Add("FTP ports are unfiltered by company. Use them to infiltrate inside");
        options.Add(25);
        options.Add(25);
        options.Add(25);
        options.Add(25);

        questions.Add("HTTP ports are unfiltered by company. Use them to infiltrate inside");

        welcomeTimer = 0;
        playing = false;
        roundNumber = 0;
    }

    void Update()
    {
        if (!playing)
        {
            if (welcomeTimer < 5)
            {
                welcomeTimer += Time.deltaTime;
                if (welcomeTimer > 2)
                {
                    welcomeText.GetComponent<Text>().color = new Color(1, 0, 0, (5 - welcomeTimer) / 3);
                }
                else
                {
                    welcomeText.GetComponent<Text>().color = new Color(1, 0, 0, welcomeTimer - 1);
                }
            }
            else if (welcomeTimer < 8)
            {
                welcomeTimer += Time.deltaTime;
                welcomeText.SetActive(false);
                storyText.SetActive(true);
            }
            else
            {
                welcomeText.SetActive(false);
                storyText.SetActive(true);
                portButtons.SetActive(true);
                playing = true;
            }
        }
        else // playing
        {

        }
    }

    void clickPort(int choice)
    {

    }
}
