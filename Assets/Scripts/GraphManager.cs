using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphManager : MonoBehaviour {

    public GameObject analyseButton;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject graphImage;
    public GameObject sourceText;
    public GameObject prevText;
    public GameObject nodeInput;

    public List<Sprite> graphs;

    public class Node
    {
        public int prev { get; set; }
        public int src { get; set; }
    };

    List<List<Node>> nodeInfo;
    List<bool> answers;
    DefenderGameManager gm;

    int lvl;

    // Use this for initialization
    void Start ()
    {
        lvl = 0;

        nodeInfo = (new List<List<Node>>());
        answers = new List<bool>();
        nodeInfo.Add(new List<Node>());
        //nodeInfo[0] = (new List<Node>());
        nodeInfo[0].Add(new Node {prev = 1, src = 2});
        nodeInfo[0].Add(new Node {prev = 2, src = 2});
        nodeInfo[0].Add(new Node {prev = 1, src = 1});
        answers.Add(true);

        nodeInfo.Add(new List<Node>());
        //nodeInfo[1] = (new List<Node>());
        nodeInfo[1].Add(new Node { prev = -1, src = -1 });
        nodeInfo[1].Add(new Node { prev = 0, src = 0 });
        nodeInfo[1].Add(new Node { prev = 1, src = 1 });
        nodeInfo[1].Add(new Node { prev = 0, src = 0 });
        nodeInfo[1].Add(new Node { prev = 2, src = 3 });
        nodeInfo[1].Add(new Node { prev = 6, src = 3 });
        nodeInfo[1].Add(new Node { prev = 4, src = 3 });
        answers.Add(true);


        gm = GetComponent<DefenderGameManager>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (lvl < 2)
        {
            graphImage.GetComponent<Image>().sprite = graphs[lvl];
        }
        else
        {
            gm.levelPassed = true;
            lvl = 0;
        }
	}

    public void ClickAns(bool op)
    {
        if (answers[lvl] == op)
            gm.UpdateCoins(10);
        else
            gm.UpdateCoins(-10);
        lvl++;
    }

    public void ClickAnalyse()
    {
        int cur = int.Parse(nodeInput.GetComponent<InputField>().text);
        sourceText.GetComponent<Text>().text = nodeInfo[lvl][cur].src.ToString();
        prevText.GetComponent<Text>().text = nodeInfo[lvl][cur].prev.ToString();
    }


}
