using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class VoiceCommands : MonoBehaviour {

    public GameObject fishCatalogue;
    public GameObject instructions;

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
        fishCatalogue.SetActive(true);
        //UnityEngine.Debug.Log("Menu???");
    }

    public void HideMenu()
    {
        fishCatalogue.SetActive(false);
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

    public void ShowInstructions()
    {
        instructions.SetActive(true);
    }

    public void HideInstructions()
    {
        instructions.SetActive(false);
    }
}
