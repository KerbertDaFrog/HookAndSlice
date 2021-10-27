using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
           Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            //damage
            other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
        }

    }

}
