using UnityEngine;
using System.Collections;

public class BaitComponent : MonoBehaviour {
    private bool active;
    public bool flying;
    public float strength = 5.0f; // Strength of attraction
    public float distOfDetection = 10.0f;
    public TapToPlaceParent tapToPlaceParent;
    Rigidbody lureRigidbody;

    // Use this for initialization
    void Start () {
        active = false;
        lureRigidbody = this.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        active = tapToPlaceParent.floorDepth > this.transform.position.y;
        if (active)
        {
            flying = false;
            lureRigidbody.useGravity = false;
            lureRigidbody.velocity = Vector3.zero;
        }
    }

    public void StartFlying(Vector3 startPosition, Vector3 velocity)
    {
        this.active = true;
        active = false;
        flying = true;
        lureRigidbody.useGravity = true;
        lureRigidbody.velocity = velocity;
    }

    public void Retrieve()
    {
        active = false;
        this.active = false;
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
