using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class VoiceCommands : MonoBehaviour {

    [Tooltip("Drag the Menu prefab asset.")]
    public GameObject MenuPrefab;
    private GameObject menuGameObject;

    public AudioSource audio;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowMenu()
    {
        menuGameObject = (GameObject)Instantiate(MenuPrefab);
        System.Diagnostics.Debug.WriteLine("Showing menu");

    }

    public void StopMusic()
    {
        if (!audio.mute)
            audio.mute = true;
    }

    public void StartMusic()
    {
        if (audio.mute)
            audio.mute = false;
    }
}
