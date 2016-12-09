using UnityEngine;
using System.Collections;

public class BaitComponent : MonoBehaviour {
    private bool attracting;

    public float strength = 5.0f; // Strength of attraction
    public float distOfDetection = 10.0f;
    public TapToPlaceParent tapToPlaceParent;
    public float pickupDistance = 1.0f;

    Rigidbody lureRigidbody;

    public bool flying;

    // Use this for initialization
    void Start () {
        gameObject.SetActive(false);
        attracting = false;

        // flight stuff
        lureRigidbody = this.GetComponent<Rigidbody>();
        lureRigidbody.useGravity = false;
        lureRigidbody.velocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        attracting = (tapToPlaceParent.floorDepth > this.transform.position.y) && gameObject.GetActive();

        if (attracting && flying)
        {
            flying = false;
            lureRigidbody.useGravity = false;
            lureRigidbody.velocity = Vector3.zero;
        }
    }

    public void StartFlying(Vector3 startPosition, Vector3 velocity)
    {
        gameObject.SetActive(true);
        attracting = false;
        flying = true;
        lureRigidbody.useGravity = true;
        lureRigidbody.velocity = velocity;
        this.transform.position = startPosition;
    }

    // returns whether the thing has been retrieved or not
    public bool Retrieve(Vector3 playerPosition)
    {
        Vector3 displacement = playerPosition - this.transform.position;
        displacement.y = 0.0f;
        float distance = displacement.magnitude;

        // if within some threshold of player position, pick up
        if (distance < pickupDistance)
        {
            attracting = false;
            flying = false;
            gameObject.SetActive(false);
            return true;
        }
        // otherwise, get a little closer
        else
        {
            lureRigidbody.velocity = displacement.normalized;
            return false;
        }
    }

    public bool isAttracting()
    {
        return attracting;
    }

    public void setAttracting(bool val)
    {
        attracting = val;
    }
}
