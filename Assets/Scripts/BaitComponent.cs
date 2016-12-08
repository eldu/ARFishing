using UnityEngine;
using System.Collections;

public class BaitComponent : MonoBehaviour {
    private bool active;
    public float strength = 5.0f; // Strength of attraction
    public float distOfDetection = 10.0f;

	// Use this for initialization
	void Start () {
        active = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool isActive()
    {
        return active;
    }

    public void setActive(bool val)
    {
        active = val;
    }
}
