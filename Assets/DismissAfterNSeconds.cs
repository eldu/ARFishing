using UnityEngine;
using System.Collections;

public class DismissAfterNSeconds : MonoBehaviour {

    private float numSecs;

	// Use this for initialization
	void Start () {
        if (numSecs == 0.0f)
        {
            numSecs = 5.0f;
        }	
	}
	
	// Update is called once per frame
	void Update () {
        if (numSecs > 0)
        {
            numSecs -= Time.deltaTime;
        }
        if (numSecs <= 0)
        {
            numSecs = 5.0f;
            gameObject.SetActive(false);
        }
    }
}
