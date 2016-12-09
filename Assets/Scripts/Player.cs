using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public BaitComponent baitComponent;

    protected bool readyToCast = true;

	// Use this for initialization
	void Start () {
	}

    public void Cast(float acceleration)
    {
        if (readyToCast)
        {
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            baitComponent.StartFlying(headPosition, gazeDirection * acceleration);
            readyToCast = false;
        }
    }

    public void Reel()
    {
        readyToCast = baitComponent.Retrieve(Camera.main.transform.position);
    }
}
