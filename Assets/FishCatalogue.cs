using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FishCatalogue : MonoBehaviour {

    public int fishesCaughtCount;
    public HashSet<FishInfo> typesOfFishCaught;

    // Use this for initialization
    void Start () {
        fishesCaughtCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddFishToCatalogue(FishInfo newFish)
    {
        Text newFishInfo = Instantiate(gameObject.GetComponentInChildren<Text>()); 
    }
}
