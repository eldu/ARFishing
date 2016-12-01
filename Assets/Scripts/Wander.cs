using UnityEngine;
using System.Collections;

public class Wander : MonoBehaviour {
    public float speed = 0.5f;

    public float range = 10.0f;
    public Vector3 wayPoint = new Vector3();

    private float time = 0.0f;

    Rigidbody rb;
    
	// Use this for initialization
	void Start () {
       rb = GetComponent<Rigidbody>();
        generateWayPoint();
        Vector3 dir = transform.forward;
        rb.velocity = dir * speed;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        // Go towards the wayPoint

        if (time >= 5.0f) 
        {
            generateWayPoint();

            Vector3 dir = transform.forward;
            rb.velocity = dir * speed;
            time = 0.0f;
        }

	}

    // Generate waypoint and look at point
    private void generateWayPoint() {
        wayPoint = new Vector3(Random.Range(transform.position.x - range, transform.position.x + range),
        transform.position.y,
        Random.Range(transform.position.z - range, transform.position.z + range));

        transform.LookAt(wayPoint);
    }
}
