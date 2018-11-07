using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IpAddress : MonoBehaviour
{

    public GameObject IPSlider11;
    public GameObject IPSlider12;
    public GameObject IPSlider21;
    public GameObject IPSlider22;
    public GameObject IPSlider31;
    public GameObject IPSlider32;
    public GameObject IPSlider41;
    public GameObject IPSlider42;

    public GameObject IPvalue1;
    public GameObject IPvalue2;
    public GameObject IPvalue3;
    public GameObject IPvalue4;

    public GameObject insideIP;
    public GameObject outsideIP;


    private int ipValMin1;
    private int ipValMax1;
    private int ipValMin2;
    private int ipValMax2;
    private int ipValMin3;
    private int ipValMax3;
    private int ipValMin4;
    private int ipValMax4;

    private int insideCount;
    private int outsideCount;
    private int totalInsideCount;
    private int totalOutsideCount;

    private float insidePercent;
    private float outsidePercent;
    private DefenderGameManager gm;


    private IDictionary<int, int> includeDict1;
    private IDictionary<int, int> includeDict2;
    private IDictionary<int, int> includeDict3;
    private IDictionary<int, int> includeDict4;
    private IDictionary<int, int> excludeDict1;
    private IDictionary<int, int> excludeDict2;
    private IDictionary<int, int> excludeDict3;
    private IDictionary<int, int> excludeDict4;

    void Awake()
    {
        ipValMin1 = 0;
        ipValMin2 = 0;
        ipValMin3 = 0;
        ipValMin4 = 0;
        ipValMax1 = 255;
        ipValMax2 = 255;
        ipValMax3 = 255;
        ipValMax4 = 255;

        insidePercent = 0;
        outsidePercent = 100;

        gm = this.GetComponent<DefenderGameManager>();

        includeDict1 = new Dictionary<int, int>();
        includeDict2 = new Dictionary<int, int>();
        includeDict3 = new Dictionary<int, int>();
        includeDict4 = new Dictionary<int, int>();
        excludeDict1 = new Dictionary<int, int>();
        excludeDict2 = new Dictionary<int, int>();
        excludeDict3 = new Dictionary<int, int>();
        excludeDict4 = new Dictionary<int, int>();


        includeDict1.Add(120, 250);
        includeDict1.Add(100, 200);
        includeDict1.Add(123, 180);
        includeDict1.Add(104, 170);
        includeDict1.Add(105, 150);
        includeDict1.Add(125, 50);

        includeDict2.Add(168, 220);
        includeDict2.Add(194, 180);
        includeDict2.Add(184, 150);
        includeDict2.Add(182, 100);
        includeDict2.Add(192, 80);
        includeDict2.Add(108, 70);
        includeDict2.Add(136, 50);
        includeDict2.Add(16, 50);
        includeDict2.Add(18, 50);
        includeDict2.Add(44, 50);

        includeDict3.Add(1, 220);
        includeDict3.Add(128, 180);
        includeDict3.Add(4, 150);
        includeDict3.Add(104, 100);
        includeDict3.Add(116, 80);
        includeDict3.Add(140, 70);
        includeDict3.Add(43, 50);
        includeDict3.Add(223, 50);
        includeDict3.Add(227, 50);
        includeDict3.Add(93, 50);
        includeDict3.Add(16, 50);

        includeDict4.Add(2, 50);
        includeDict4.Add(13, 50);
        includeDict4.Add(121, 50);
        includeDict4.Add(6, 50);
        includeDict4.Add(25, 50);
        includeDict4.Add(78, 50);
        includeDict4.Add(242, 50);
        includeDict4.Add(144, 50);
        includeDict4.Add(213, 50);
        includeDict4.Add(163, 50);
        includeDict4.Add(254, 50);
        includeDict4.Add(165, 50);
        includeDict4.Add(218, 50);
        includeDict4.Add(190, 50);
        includeDict4.Add(122, 50);
        includeDict4.Add(99, 50);
        includeDict4.Add(4, 50);
        includeDict4.Add(43, 50);
        includeDict4.Add(51, 50);
        includeDict4.Add(204, 50);

        excludeDict1.Add(192, 200);


    }

    void Update()
    {
        ipValMin1 = (int)IPSlider11.GetComponent<Slider>().value;
        ipValMax1 = 255 - (int)IPSlider12.GetComponent<Slider>().value;
        ipValMin2 = (int)IPSlider21.GetComponent<Slider>().value;
        ipValMax2 = 255 - (int)IPSlider22.GetComponent<Slider>().value;
        ipValMin3 = (int)IPSlider31.GetComponent<Slider>().value;
        ipValMax3 = 255 - (int)IPSlider32.GetComponent<Slider>().value;
        ipValMin4 = (int)IPSlider41.GetComponent<Slider>().value;
        ipValMax4 = 255 - (int)IPSlider42.GetComponent<Slider>().value;

        IPvalue1.GetComponent<Text>().text = ipValMin1.ToString() + " : " + ipValMax1.ToString();
        IPvalue2.GetComponent<Text>().text = ipValMin2.ToString() + " : " + ipValMax2.ToString();
        IPvalue3.GetComponent<Text>().text = ipValMin3.ToString() + " : " + ipValMax3.ToString();
        IPvalue4.GetComponent<Text>().text = ipValMin4.ToString() + " : " + ipValMax4.ToString();

        insideIP.GetComponent<Text>().text = "User nodes covered : " + ((int)insidePercent).ToString() + "%";
        outsideIP.GetComponent<Text>().text = "Suspicious nodes covered : " + ((int)outsidePercent).ToString() + "%";
    }

    public void CheckStats()
    {

        insideCount = 0;
        outsideCount = 0;
        totalInsideCount = 4000;
        totalOutsideCount = 4000;
        Count(includeDict1, ipValMin1, ipValMax1, 1);
        Count(includeDict2, ipValMin2, ipValMax2, 1);
        Count(includeDict3, ipValMin3, ipValMax3, 1);
        Count(includeDict4, ipValMin4, ipValMax4, 1);
        Count(excludeDict1, ipValMin1, ipValMax1, 0);
        Count(excludeDict2, ipValMin2, ipValMax2, 0);
        Count(excludeDict3, ipValMin3, ipValMax3, 0);
        Count(excludeDict4, ipValMin4, ipValMax4, 0);

        insidePercent = 100 * (insideCount*1.0f / totalInsideCount);
        outsidePercent = 100 * (outsideCount*1.0f / totalOutsideCount);

        //Last
        if (insidePercent > 80)
        {
            insideIP.GetComponent<Text>().color = new Color(0, 1, 0);
        }
        else
        {
            insideIP.GetComponent<Text>().color = new Color(1, 0, 0);
        }

        if (outsidePercent < 50)
        {
            outsideIP.GetComponent<Text>().color = new Color(0, 1, 0);
        }
        else
        {
            outsideIP.GetComponent<Text>().color = new Color(1, 0, 0);
        }
    }

    public void Submit()
    {
        if(insidePercent > 80 && outsidePercent < 50)
        {
            gm.levelPassed = true;
        }
    }

    private void Count(IDictionary<int, int> dict, int startIP, int endIP, int flag)
    {
        foreach (KeyValuePair<int, int> i in dict)
        {
            if (i.Key >= startIP && i.Key < endIP)
            {
                if (flag == 1)
                {
                    insideCount += i.Value;
                }
                else
                {
                    outsideCount += i.Value;
                }
            }
        }
    }
}
