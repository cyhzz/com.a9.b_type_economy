using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class B_TypeTestCanvas : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    GameObject panel;

    int this_time;
    Action this_end;

    public void BeginTest(int time, Action OnEnd)
    {
        this_time = time;
        this_end = OnEnd;
        panel.SetActive(true);
        StartCoroutine(TestRoutine_());
    }

    IEnumerator TestRoutine_()
    {
        for (int i = 0; i < this_time; i++)
        {
            text.text = (this_time - i).ToString();
            yield return new WaitForSecondsRealtime(1.0f);
        }
        panel.SetActive(false);
        this_end?.Invoke();
    }
}
