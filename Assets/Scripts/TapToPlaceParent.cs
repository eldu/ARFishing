using UnityEngine;

public class TapToPlaceParent : MonoBehaviour
{
    public Transform swimmerPrefab;
    public int swimmerSpawnInterval;
    int swimmerSpawnTimer;
    bool readyToSpawnFish = false;

    bool placing = false;


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
            SpatialMapping.Instance.DrawVisualMeshes = true;
        }
        // If the user is not in placing mode, hide the spatial mapping mesh.
        else
        {
            SpatialMapping.Instance.DrawVisualMeshes = false;
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
            SpawnFish();
        }
    }

    void SpawnFish()
    {
        Vector3 offset = new Vector3();
        offset.y = -0.5f;
        Transform swimmerClone = Instantiate(swimmerPrefab);
        swimmerClone.transform.position = offset + transform.position;
    }
}