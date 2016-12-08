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
        gameObject.SetActive(false);
        active = false;
        lureRigidbody = this.GetComponent<Rigidbody>();
        lureRigidbody.useGravity = false;
        lureRigidbody.velocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        active = tapToPlaceParent.floorDepth > this.transform.position.y;
        print(tapToPlaceParent.floorDepth + " " + this.transform.position.y);
        if (active)
        {
            flying = false;
            lureRigidbody.useGravity = false;
            lureRigidbody.velocity = Vector3.zero;
        }
    }

    public void StartFlying(Vector3 startPosition, Vector3 velocity)
    {
        gameObject.SetActive(true);
        active = false;
        flying = true;
        lureRigidbody.useGravity = true;
        lureRigidbody.velocity = velocity;
        this.transform.position = startPosition;
    }

    public void Retrieve()
    {
        active = false;
        gameObject.SetActive(false);
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
