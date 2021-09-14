using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsSender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // send a single custom event
        var result = Analytics.CustomEvent("Location", transform.position);
        Debug.Log(result);

        // prepare the data package
        var state = new Dictionary<string, object>();
        state["Health"] = 57;
        state["Speed"] = 1.23f;
        state["Level"] = "Intro";

        // send the data package as the player state
        result = Analytics.CustomEvent("PlayerState", state);
        Debug.Log(result);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
