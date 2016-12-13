using UnityEngine;
using System.Collections;

public class BaitComponent : MonoBehaviour {
    public bool attracting;

    public float hookDistance = 0.7f; // distance at which an attracted fish is "hooked"
    public float distOfDetection = 10.0f;
    public TapToPlaceParent tapToPlaceParent;
    public float pickupDistance = 1.0f;

    public GameObject hooked;
    public Player player;

    Rigidbody lureRigidbody;

    public bool flying;

    // Use this for initialization
    void Start () {
        attracting = false;
        hooked = null;
        // flight stuff
        lureRigidbody = this.GetComponent<Rigidbody>();
        lureRigidbody.useGravity = false;
        lureRigidbody.velocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        attracting = (tapToPlaceParent.floorDepth > this.transform.position.y);

        if (attracting && flying)
        {
            flying = false;
            lureRigidbody.useGravity = false;
            lureRigidbody.velocity = Vector3.zero;
        }
    }

    public void StartFlying(Vector3 startPosition, Vector3 velocity)
    {
        hooked = null;
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
        return attracting && hooked == null;
    }

    public void setAttracting(bool val)
    {
        attracting = val;
    }

    public bool isEssentiallyStill()
    {
        return lureRigidbody.velocity.magnitude < 0.001f;
    }

    public void reset()
    {
        player.ResetBait();
    }

    public Vector3 getDirection()
    {
        return lureRigidbody.velocity.normalized;
    }

    public void drag(Vector3 newPos)
    {
        Vector3 displace = this.transform.position - newPos;
        if (displace.magnitude > hookDistance)
        {
            this.transform.position = newPos + displace.normalized * hookDistance;
        }
    }
}
