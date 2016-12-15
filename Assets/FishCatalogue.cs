using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FishCatalogue : MonoBehaviour {

    public int fishesCaughtCount;
    public HashSet<string> typesOfFishCaught;
    public List<GameObject> fishCatalogueItems;

    public GameObject fishCatalogueItemPrefab;
    
    private Vector3 offset;

    // Use this for initialization
    void Start () {
        typesOfFishCaught = new HashSet<string>();
        fishesCaughtCount = 0;
        offset = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddFishToCatalogue(FishInfo newFish)
    {
        Object fcItemClone = Instantiate(fishCatalogueItemPrefab, transform);

        GameObject fcItemCloneGO = (GameObject)fcItemClone;

        offset.x += 0.5f;
        fcItemCloneGO.transform.position = offset + transform.position;
        fcItemCloneGO.SetActive(true);
    }
}
