using UnityEngine;
using System.Collections.Generic;

public class TapToPlaceParent : MonoBehaviour
{
    public Transform swimmerPrefab;
    public int swimmerSpawnInterval;
    public Material scanMeshMaterial;

    // private List<GameObject> fishies;
    private int numFish;
    public int maxNumFish = 15;

    int swimmerSpawnTimer;
    bool readyToSpawnFish = false;

    bool placing = true;

    public float floorDepth;

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
            scanMeshMaterial.SetFloat("_DropDepth", floorDepth);
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
        Vector3 offset = new Vector3();
        offset.y = -0.5f;
        Transform swimmerClone = Instantiate(swimmerPrefab);
        swimmerClone.transform.position = offset + transform.position;
        numFish++;
    }
}