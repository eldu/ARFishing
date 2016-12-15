using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FishCatalogue : MonoBehaviour {

    public int fishesCaughtCount;
    public HashSet<string> typesOfFishCaught = new HashSet<string>();

    public GameObject fishCatalogueItemPrefab;
    
    private Vector3 offset;

    // Use this for initialization
    void Start () {
        fishesCaughtCount = 0;
        offset = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddFishToCatalogue(FishInfo newFish)
    {
        typesOfFishCaught.Add(newFish.fishName);
        Object fcItemClone = Instantiate(fishCatalogueItemPrefab, transform);

        GameObject fcItemCloneGO = (GameObject)fcItemClone;

        offset.x += 0.2f;
        fcItemCloneGO.transform.position = offset + transform.position;
       // print("offset is " + offset);
       // print("position is " + fcItemCloneGO.transform.position);
        fcItemCloneGO.SetActive(true);
        FishCatalogueItem fcItemComponent = fcItemCloneGO.GetComponent<FishCatalogueItem>();
        fcItemComponent.updateInfo(newFish);
    }
}
