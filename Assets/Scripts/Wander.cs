using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Wander : MonoBehaviour {
    // Components
    public TapToPlaceParent worldInfo;
    public BaitComponent bait;
    public GameObject fishHookedNotif;
    public GameObject fishEscapedNotif;

    public float speed = 0.5f;
    public float turnSpeed = 60.0f; // turnSpeed in max degrees that can be rotated in one second
    public float maxWaypointTime = 5.0f;
    public float minWaypointTime = 1.0f;
    public float hookTime = 10.0f;

    public Vector3 range = new Vector3(10.0f, 2.0f, 10.0f); // Range of Motion in x y z directions

    public Vector3 wayPoint = new Vector3();
    private float time = 0.0f;
    float curHookTime = 0.0f;
    private bool beingAttracted = false;
    private bool hooked = false;

    Rigidbody rb;

    private Text fishCountText;
    private FishCatalogue fishCatalogue;
    
	// Use this for initialization
	void Start () {
       rb = GetComponent<Rigidbody>();
       time = -1.0f;
    }
	
	// Update is called once per frame
	void Update () {
        // Go towards the wayPoint
        float deltaTime = Time.deltaTime;
        float currSpeed = speed;

        // check if this is hooked
        if (beingAttracted && !hooked)
        {
            Vector3 dirToBait = bait.transform.position - transform.position;
            dirToBait.y = 0.0f;
            if (dirToBait.magnitude < bait.hookDistance)
            {
                bait.hooked = gameObject;
                hooked = true;
                curHookTime = hookTime;
                print("hooked!");
                fishHookedNotif.SetActive(true);

                // signal the phone to vibrate
                byte evCode = 4;
                bool reliable = true;
                PhotonNetwork.RaiseEvent(evCode, null, reliable, null);
            }
        }

        // if this is hooked, then move with the lure's velocity if any.
        // if the lure has virtually no velocity, pull the lure.
        // Decrement the lure time.
        if (hooked)
        {
            curHookTime -= deltaTime;

            // check if done being lured
            if (curHookTime < 0.0f)
            {
                bait.hooked = null;
                bait.reset();
                hooked = false;
                print("fish got away!");

                // signal the phone to vibrate
                byte evCode = 4;
                bool reliable = true;
                PhotonNetwork.RaiseEvent(evCode, null, reliable, null);

                fishHookedNotif.SetActive(false);
                fishEscapedNotif.SetActive(true);
            }

            if (bait.isEssentiallyStill())
            {
                currSpeed *= 0.5f; // slow the fish down b/c dragging a lure

                // update lure position
                bait.drag(this.transform.position);
            }
            else
            {
                Vector3 displace = this.transform.position - bait.transform.position;
                if (displace.magnitude > bait.hookDistance)
                {
                    this.transform.position = bait.transform.position + displace.normalized * bait.hookDistance;
                }

                // turn to go with the lure
                displace.y = 0.0f;
                displace.Normalize();
                transform.forward = -displace;

                return; // being towed, skip standard waypoint handling
            }
        }

        Vector3 wayPoint = getWayPoint();

        // turn to look more at waypoint
        Vector3 dirToWayPoint = (wayPoint - transform.position).normalized;
        float dot = Vector3.Dot(dirToWayPoint, transform.forward);
        Vector3 axis = Vector3.Cross(transform.forward, dirToWayPoint);
        if (axis.y < 0.0f)
            transform.Rotate(-Vector3.up, turnSpeed * deltaTime * (1.0f - dot));
        else
            transform.Rotate(Vector3.up, turnSpeed * deltaTime * (1.0f - dot));

        // if already pointing roughly at the waypoint, just move directly towards it
        if (dot > 0.8f)
            rb.velocity = dirToWayPoint * currSpeed;
        else
            rb.velocity = transform.forward * currSpeed;

        time -= deltaTime;

        // enforce some rules: fish can't leave the surface
        if (transform.position.y > worldInfo.floorDepth - 0.5f)
        {
            Vector3 correctedPosition = transform.position;
            correctedPosition.y = worldInfo.floorDepth - 0.5f;
            transform.position = correctedPosition;
        }
    }

    private Vector3 getWayPoint()
    {
        if (time < 0.0f && !beingAttracted)
        {
           time = Random.Range(minWaypointTime, maxWaypointTime);
           wayPoint = new Vector3(Random.Range(transform.position.x - range.x, transform.position.x + range.x),
                                    worldInfo.floorDepth - Random.Range(0.0f, transform.position.y + range.y),
                                    Random.Range(transform.position.z - range.z, transform.position.z + range.z));
        }

        // check if the bait is visible. if so, go at it.
        if (bait.isAttracting())
        {
            if (beingAttracted) wayPoint = bait.transform.position;
            else
            {
                Vector3 dirToBait = bait.transform.position - transform.position;
                float dot = Vector3.Dot(dirToBait.normalized, transform.forward);
                if (dirToBait.magnitude < bait.pickupDistance && dot > 0.0f)
                {
                    wayPoint = bait.transform.position;
                    beingAttracted = true;
                }
            }
        }
        else
        {
            beingAttracted = false;
        }

        return wayPoint;
    }

    void OnSelect()
    {
        /*fishCatalogue = GameObject.Find("FishCatalogue").GetComponent<FishCatalogue>();
        fishCountText = GameObject.Find("fishesCaughtCount").GetComponent<Text>();
        fishCatalogue.fishesCaughtCount++;
        fishCountText.text = fishCatalogue.fishesCaughtCount.ToString();

        if (!fishCatalogue.typesOfFishCaught.Contains(gameObject.GetComponent<FishInfo>()))
        {
            fishCatalogue.typesOfFishCaught.Add(gameObject.GetComponent<FishInfo>());
            fishCatalogue.AddFishToCatalogue(gameObject.GetComponent<FishInfo>());
        }

        //Debug.Log(fishCatalogue.name);
       // Debug.Log(fishCountText.text);
        Destroy(gameObject);*/
    }
}
