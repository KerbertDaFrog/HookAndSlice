using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonAudio : MonoBehaviour
{
    [SerializeField]
    private string levelMusic;

    [SerializeField]
    private string priorLevelMusic;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.StopPlaying(priorLevelMusic);
        AudioManager.instance.Play(levelMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
