using UnityEngine;
using System.Collections.Generic;

public class TapToPlaceParent : MonoBehaviour
{
    public int swimmerSpawnInterval;
    public Material scanMeshMaterial;
    public BaitComponent bait;
    public GameObject fishHookedNotif;
    public GameObject fishEscapedNotif;

    // private List<GameObject> fishies;
    private int numFish;
    public int maxNumFish = 15;

    int swimmerSpawnTimer;
    bool readyToSpawnFish = false;

    bool placing = true;

    public float floorDepth;

    public List<GameObject> typesOfFish;
    private Transform swimmerPrefab;

    void Start()
    {
        floorDepth = this.transform.position.y;

        SpatialMapping.Instance.DrawVisualMeshes = true; // draw the visual mesh as-is
        scanMeshMaterial.SetInt("_Invisible", 0);
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // ALPHA: after first placing, start spawning fish
        if (placing) readyToSpawnFish = true;

        // On each Select gesture, toggle whether the user is in placing mode.
        placing = !placing;

        // If the user is in placing mode, display the spatial mapping mesh.
        if (placing)
        {
            SpatialMapping.Instance.DrawVisualMeshes = true; // draw the visual mesh as-is
            scanMeshMaterial.SetInt("_Invisible", 0);
        }
        // If the user is not in placing mode, hide the spatial mapping mesh, but continue to draw it for occlusion
        else
        {
            SpatialMapping.Instance.DrawVisualMeshes = true; // keep drawing the visual mesh for occlusion,
            scanMeshMaterial.SetInt("_Invisible", 1);        // but perform a vertex transformation and don't draw "lines"
            scanMeshMaterial.SetFloat("_DropDepth", floorDepth + 0.025f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the user is in placing mode,
        // update the placement to match the user's gaze.

        if (placing)
        {
            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                // Move this object's parent object to
                // where the raycast hit the Spatial Mapping mesh.
                this.transform.parent.position = hitInfo.point;
                floorDepth = hitInfo.point.y;

                // Rotate this object's parent object to face the user.
                Quaternion toQuat = Camera.main.transform.localRotation;
                toQuat.x = 0;
                toQuat.z = 0;
                this.transform.parent.rotation = toQuat;
            }
        }

        // debug
        if (Input.GetKey(KeyCode.P))
        {
            OnSelect();
        }
    }

    void FixedUpdate()
    {
        swimmerSpawnTimer--;
        if (swimmerSpawnTimer < 0 && readyToSpawnFish)
        {
            swimmerSpawnTimer = swimmerSpawnInterval;

            if (numFish < maxNumFish)
            {
                SpawnFish();
            }
        }
    }

    void SpawnFish()
    {
        chooseFishToSpawn();
        Vector3 offset = new Vector3();
        offset.y = -0.5f;
        Transform swimmerClone = Instantiate(swimmerPrefab);
        swimmerClone.transform.position = offset + transform.position;
        Wander wanderComponent = swimmerClone.GetComponent<Wander>();
        wanderComponent.worldInfo = this;
        wanderComponent.bait = bait;
        wanderComponent.fishEscapedNotif = fishEscapedNotif;
        wanderComponent.fishHookedNotif = fishHookedNotif;

        numFish++;
    }

    void chooseFishToSpawn()
    {
        //sets swimmerPrefab
        float randomNumber = Random.Range(0.0f, 1.0f);
        if (randomNumber >= 0.0f && randomNumber < 0.5f)
        {
            // koi fish
            swimmerPrefab = typesOfFish[0].transform;

        } else if (randomNumber >= 0.5f && randomNumber < 0.75f)
        {
            // small fish
            swimmerPrefab = typesOfFish[1].transform;

        } else if (randomNumber >= 0.75f && randomNumber < 0.85f)
        {
            // magikarp
            swimmerPrefab = typesOfFish[2].transform;

        } else if (randomNumber >= 0.85f && randomNumber < 0.95f)
        {
            // pocket whale
            swimmerPrefab = typesOfFish[3].transform;

        } else if (randomNumber >= 0.95f && randomNumber < 1.0f)
        {
            // credit card phish
            swimmerPrefab = typesOfFish[4].transform;

        }
    }
  
}