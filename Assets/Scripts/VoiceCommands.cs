using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class VoiceCommands : MonoBehaviour {

    [Tooltip("Drag the Menu prefab asset.")]
    public GameObject MenuPrefab;
    private GameObject menuGameObject;

    public AudioSource BGMaudio;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowMenu()
    {
        Vector3 offset = new Vector3();
        offset.z = 0.5f;
        menuGameObject = (GameObject)Instantiate(MenuPrefab, offset + Camera.main.transform.position, MenuPrefab.transform.rotation);
        UnityEngine.Debug.Log("Menu???");
    }

    public void StopMusic()
    {
        if (!BGMaudio.mute)
            BGMaudio.mute = true;
    }

    public void StartMusic()
    {
        if (BGMaudio.mute)
            BGMaudio.mute = false;
    }
}
