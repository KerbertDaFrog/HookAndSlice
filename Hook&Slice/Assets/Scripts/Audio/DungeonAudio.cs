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
        FindObjectOfType<AudioManager>().StopPlaying(priorLevelMusic);
        FindObjectOfType<AudioManager>().Play(levelMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
