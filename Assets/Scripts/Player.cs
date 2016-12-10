using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public BaitComponent baitComponent;
    public Hololens_Network network;

    public bool readyToCast = true;

    Vector3 launchDir;

	// Use this for initialization
	void Start () {
	}

    public void Cast(float acceleration, float acceleromX, float acceleromY)
    {
        if (readyToCast)
        {
            // TODO: use accelerometer x and y to compute a direction for the toss
            // These are raw from the device.


            print(acceleration + " x " + acceleromX + " y " + acceleromY);

            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;
            gazeDirection.y = 0.0f;
            gazeDirection.Normalize();

            launchDir.x = Mathf.Abs(acceleromX);
            launchDir.y = Mathf.Abs(acceleromY);
            launchDir.z = 0.0f;
            launchDir.Normalize();

            // rotate launchDir so it points in the same direction as gazeDirection in the xz plane
            float angle = Mathf.Acos(gazeDirection.x); // angle between X dir and gazeDirection in x z plane
            launchDir = Quaternion.Euler(0, -Mathf.Rad2Deg * angle, 0) * launchDir;

            baitComponent.StartFlying(headPosition, launchDir * acceleration * 20.0f);
            readyToCast = false;
            network.SignalCastingReadiness(false);
        }
    }

    public void Reel()
    {
        bool nowReadyToCast = baitComponent.Retrieve(Camera.main.transform.position);
        if (nowReadyToCast != readyToCast)
        {
            network.SignalCastingReadiness(nowReadyToCast);
        }
        readyToCast = nowReadyToCast;
    }
}
