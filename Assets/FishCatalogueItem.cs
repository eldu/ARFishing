using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FishCatalogueItem : MonoBehaviour {

    public Text fishName;
    public Text fishDescription;
    public Text fishWeight;
    public RawImage fishImage;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void updateInfo(FishInfo fishInfo)
    {
        fishName.text = fishInfo.fishName;
        fishWeight.text = fishInfo.fishWeight;
        fishDescription.text = fishInfo.fishDescription;
        fishImage.texture = fishInfo.fishPhoto;
    }
}
