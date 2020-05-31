using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text321 : MonoBehaviour
{
    private Text t;
    private void Start()
    {
    }
    public static IEnumerator set321(Text t)
    {
        yield return new WaitForSecondsRealtime(1);
        t.text = "3";
        yield return new WaitForSecondsRealtime(1);
        t.text = "2";
        yield return new WaitForSecondsRealtime(1);
        t.text = "1";
    }

    public static IEnumerator set321Start(Text t)
    {
        yield return new WaitForSecondsRealtime(1);
        t.text = "START";
        yield return new WaitForSecondsRealtime(1);
        t.text = "";
    }

    public static IEnumerator set321Resume(Text t)
    {
        yield return new WaitForSecondsRealtime(1);
        t.text = "RESUME";
        yield return new WaitForSecondsRealtime(1);
        t.text = "";
    }
}
