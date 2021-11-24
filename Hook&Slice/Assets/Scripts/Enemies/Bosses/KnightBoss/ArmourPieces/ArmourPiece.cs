using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourPiece : ArmourPieces
{
    [SerializeField]
    private GameObject armourPiece;

    [SerializeField]
    private GameObject fallingArmour;

    [SerializeField]
    private GameObject ghostArmor;

    private Vector3 fallingArmourPos;

    private void Start()
    {
        fallingArmourPos = new Vector3(gameObject.transform.position.x, 0f, gameObject.transform.position.z);
    }

    private void Update()
    {
        if(hooked == true)
        {
            hooked = false;           
            StartCoroutine(FallOffKnightBoss());
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            Instantiate(fallingArmour, fallingArmourPos, armourPiece.transform.rotation);
        }
    }

    IEnumerator FallOffKnightBoss()
    {       
        yield return null;
        //play sound
        yield return null;
        Debug.Log("Instantiating Falling Armour");
        Instantiate(fallingArmour, fallingArmourPos, armourPiece.transform.rotation);
        Debug.Log("Turning Ghost Armour on");
        ghostArmor.SetActive(true);
        yield return null;       
        Destroy(armourPiece.gameObject);
        yield return new WaitForSeconds(0.5f);    
        Debug.Log("Knight Boss Going to Frenzy");
        kb.SetState(Enemy.EnemyStates.frenzy);
        yield return null;
        Destroy(gameObject);
        yield return null;
    }
}
