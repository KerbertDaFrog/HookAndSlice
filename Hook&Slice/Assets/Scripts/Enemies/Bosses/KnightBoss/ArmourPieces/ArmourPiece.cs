using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPiece : ArmourPieces
{
    private KnightBoss kb;

    [SerializeField]
    private bool chestPlate;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "ChestPlate")
        {
            chestPlate = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hooked == true)
        {
            StartCoroutine(FallOffKnightBoss());
        }
    }

    IEnumerator FallOffKnightBoss()
    {
        yield return null;
        //play sound
        yield return null;
        Destroy(gameObject);
        yield return null;
        //instantiate falling armour
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Knight Boss Going to Frenzy");
        kb.SetState(Enemy.EnemyStates.frenzy);
        //tell knight to go to frenzy state
    }
}
