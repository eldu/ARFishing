﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public BaitComponent baitComponent;
    public TrailRenderer baitComponentTrail;

    public Hololens_Network network;

    public bool readyToCast = true;

    public bool showHeldLure = false;

    Vector3 launchDir;

	// Use this for initialization
	void Start () {
        showHeldLure = false;
    }

    void Update()
    {
        if (readyToCast)
        {
            var headPosition = Camera.main.transform.position;
            var initialBaitPosition = headPosition + Camera.main.transform.forward * 0.7f;
            baitComponent.transform.position = initialBaitPosition;
            baitComponent.gameObject.SetActive(showHeldLure);
            baitComponentTrail.enabled = false;
        }
    }

    public void Cast(float acceleration, float acceleromX, float acceleromY)
    {
        if (readyToCast)
        {
            // TODO: use accelerometer x and y to compute a direction for the toss
            // These are raw from the device.
            baitComponent.gameObject.SetActive(true);
            baitComponentTrail.enabled = true;

            print(acceleration + " x " + acceleromX + " y " + acceleromY);

            var headPosition = Camera.main.transform.position;
            var initialBaitPosition = headPosition + Camera.main.transform.forward * 0.7f;
            var gazeDirection = Camera.main.transform.forward;
            gazeDirection.y = 0.0f;
            gazeDirection.Normalize();

            launchDir.x = Mathf.Abs(acceleromX);
            launchDir.y = Mathf.Abs(acceleromY);
            launchDir.z = 0.0f;
            launchDir.Normalize();

            // rotate launchDir so it points in the same direction as gazeDirection in the xz plane
            float angle = Mathf.Acos(gazeDirection.x); // angle between X dir and gazeDirection in x z plane
            if (gazeDirection.z < 0.0f)
            {
                angle = -angle;
            }
            launchDir = Quaternion.Euler(0, -Mathf.Rad2Deg * angle, 0) * launchDir;

            baitComponent.StartFlying(initialBaitPosition, launchDir * acceleration * 20.0f);
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

    public void ResetBait()
    {
        baitComponent.transform.position = Camera.main.transform.position;
        Reel();
    }

    public void SetDrawHeldLure(bool draw)
    {
        showHeldLure = draw;
    }
}
