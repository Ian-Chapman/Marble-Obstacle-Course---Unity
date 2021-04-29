using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBehaviour : MonoBehaviour
{
    float timeValue = 120; //2 minute timer

    private void Start()
    {
        timeValue = 120;
    }

    void Update()
    {
        timeValue -= Time.deltaTime;
        displayTime();
    }

    void displayTime()
    {
        int min = Mathf.FloorToInt(timeValue / 60);
        int sec = Mathf.FloorToInt(timeValue % 60);
        if (sec >= 10)
        {
            gameObject.GetComponent<Text>().text = min.ToString() + ":" + sec.ToString();
        }
        else
        {
            gameObject.GetComponent<Text>().text = min.ToString() + ":0" + sec.ToString();
        }
    }
}

