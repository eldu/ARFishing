using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Wander : MonoBehaviour {
    // Components
    public GameObject bait;
    private BaitComponent bc;

    public float speed = 0.5f;
    public Vector3 range = new Vector3(10.0f, 2.0f, 10.0f); // Range of Motion in x y z directions

    private Vector3 wayPoint = new Vector3();
    private float time = 0.0f;

    Rigidbody rb;

    public Text fishCountText;
    public FishCatalogue fishCatalogue;
    
	// Use this for initialization
	void Start () {
       rb = GetComponent<Rigidbody>();


       GameObject[] bs = GameObject.FindGameObjectsWithTag("Lure");
       bait = bs[0];

       bc = bait.GetComponent<BaitComponent>();
       time = -1.0f;
    }
	
	// Update is called once per frame
	void Update () {
        // Go towards the wayPoint

        Vector3 wayPoint = getWayPoint();
        transform.LookAt(wayPoint);

        Vector3 dir = wayPoint - transform.position; // Changes the direction that the fish looks at
        rb.velocity = transform.forward * speed;

        if (bc.isActive() && dir.magnitude < bc.distOfDetection);
        {
            Vector3 force = (bc.strength * 1.0f / dir.magnitude) * dir.normalized;
            rb.AddForce(force);
        }

        time += Time.deltaTime;
    }

    private Vector3 getWayPoint()
    {
        if (time < 0.0f)
        {
        // Initial
           time = 0.0f;
           wayPoint = new Vector3(Random.Range(transform.position.x - range.x, transform.position.x + range.x),
                                    Random.Range(transform.position.y - range.y, transform.position.y + range.y),
                                    Random.Range(transform.position.z - range.z, transform.position.z + range.z));
        } else if (bc.isActive()) {
                wayPoint = bait.transform.position;
        }
        else if (time > 5.0f) {
            time = 0.0f;
            wayPoint = new Vector3(Random.Range(transform.position.x - range.x, transform.position.x + range.x),
                                    Random.Range(transform.position.y - range.y, transform.position.y + range.y),
                                    Random.Range(transform.position.z - range.z, transform.position.z + range.z));
        }

        return wayPoint;
    }

    void OnSelect()
    {
        fishCatalogue = GameObject.Find("FishCatalogue").GetComponent<FishCatalogue>();
        fishCountText = GameObject.Find("fishCount").GetComponent<Text>();
        fishCatalogue.fishCount++;
        fishCountText.text = fishCatalogue.fishCount.ToString();

        Debug.Log(fishCatalogue.name);
        Debug.Log(fishCountText.text);
        Destroy(gameObject);
    }
}
