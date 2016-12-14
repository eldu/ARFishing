using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class VoiceCommands : MonoBehaviour {

    [Tooltip("Drag the Menu prefab asset.")]
    public GameObject MenuPrefab;

    public AudioSource BGMaudio;

    public Material scanMeshMaterial;
    int debugShader = 0;

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
        MenuPrefab.SetActive(true);
        //UnityEngine.Debug.Log("Menu???");
    }

    public void HideMenu()
    {
        MenuPrefab.SetActive(false);
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

    public void DebugShader()
    {
        debugShader = debugShader == 1 ? 0 : 1;
        scanMeshMaterial.SetInt("_Debug", debugShader);
    }
}
