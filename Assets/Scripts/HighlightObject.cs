using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighlightObject : MonoBehaviour {


    public List<GameObject> objects;

    static private int state;
    static private bool highlight;

    private float helper;
    /*
     0 - obj
     1 - hint
     2 - settings
     3 - nmap
     4 - packetanalyser
     5 - blocker
     6 - wireshark
     7 - coins
     */

    private float time;

    // Use this for initialization
    void Start () {
        highlight = false;
        helper = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
		if(highlight)
        {
            if(time>1)
            {
                time = 0;
            }
            objects[state].GetComponentInChildren<Image>().rectTransform.localScale = Vector2.Lerp(new Vector2(0.8f, 0.8f), new Vector2(1.2f, 1.2f), time);
        }
        else
        {
            for(int i = 0; i < objects.Count; i ++)
            {
                objects[i].GetComponentInChildren<Image>().rectTransform.localScale = new Vector2(1, 1);
            }
        }
	}


    public static void Highlight(int option)
    {
        StopIt();
        state = option;
        highlight = true ;
    }

    public static void StopIt()
    {
        highlight = false;
    }



}
