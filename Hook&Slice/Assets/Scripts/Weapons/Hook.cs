using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField]
    private float currentSpeed;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float dist;
    [SerializeField]
    private float distanceFromGun;
    [SerializeField]
    private float setTimer;
    [SerializeField]
    private float currentTimer;
    [SerializeField]
    private float totalDist;
    [SerializeField]
    private float maxDist = 1000f;

    [SerializeField]
    private bool done;
    [SerializeField]
    private bool retracting;
    public bool retracted;

    public int damage;
    public int maxEnemies;

    private int index = 0;

    [SerializeField]
    private List<Vector3> targets = new List<Vector3>();

    public List<Transform> enemies = new List<Transform>();

    private Harpoon harpoon;

    [SerializeField]
    private Vector3 origin;
    [SerializeField]
    private Vector3 target;

    [SerializeField]
    private PauseMenu pauseMenu;

    private void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        harpoon = FindObjectOfType<Harpoon>();
    }

    private void Start()
    {
        origin = transform.position;
        currentTimer = setTimer;
        //Take all of the elements from the hitPoints list in the Harpoon Component and add/copy them to the targets list in this script.
        targets.AddRange(harpoon.hitPoints);
        this.transform.SetParent(null);
    }

    private void Update()
    {
        //Pause Menu Stuff: If Paused, current speed is set to 0 until not paused, which will return currentSpeed to the player set speed.
        if (pauseMenu.paused != true)
            currentSpeed = speed;

        if (pauseMenu.paused != false)
        {
            currentSpeed = 0;
        }
        else if (pauseMenu.paused != true)
        {
            currentSpeed = speed;
        }

        //If the targets list is not null, then the Vector3 variable for target will be equal to the first element of targets index.
        if (targets != null && !done)
            target = targets[index];

        origin = transform.position;

        if(!done)
            print("my target = " + index);

        totalDist += dist;

        dist = Vector3.Distance(origin, target);
       
        distanceFromGun = Vector3.Distance(this.transform.position, harpoon.transform.position);

        origin = transform.position;

        //If the distance to the target is less 0.1 than change the index to one element up.
        if (dist < 0.1)
            index++;  

        //If the index int is equal to the amount of elements in the Vector3 targets list and the distance to the target is less than 0.1, do this.
        if (index == targets.Count && dist < 0.1)
            done = true;

        //If done, change the target to the harpoon gun's transform position.
        if(done)
        {
            target = harpoon.transform.position;
            retracting = true;
        }

        //If done and the hook's distance to the harpoon gun is less than 0.1 and the index count of enemies is 0, turn off done, turn off hasShot to false in Harpoon script, and destroy hook game object.
        if(done && distanceFromGun < 0.1 && enemies.Count == 0)
        {
            done = false;
            harpoon.hasShot = false;
            harpoon.staticHook.SetActive(true);
            harpoon.returned = true;
            Destroy(this.gameObject);
        }

        transform.LookAt(target);     
        
        transform.position = Vector3.MoveTowards(origin, target, currentSpeed);
        
        //If the number of enemies on the enemies list is more than 0, start the countdown timer.
        if(enemies.Count > 0 && retracted != false)
            currentTimer = Mathf.Clamp(currentTimer -= Time.deltaTime, 0f, setTimer);

        if(harpoon.hasShot != false && enemies.Count > 0 && distanceFromGun < 0.1)
        {
            retracting = false;
            retracted = true;
        }

        if(retracted != false && enemies.Count <= 0)
            retracted = false;

        //If index larger than maxEnemies than Remove all elements until maxEnemies limit reached.
        if(enemies.Count > maxEnemies)
            enemies.RemoveAt(maxEnemies);

        if(currentTimer <= 0)
            OnTimerEnd();

        if(targets.Count > 0)
            PullEnemyToPlayer();
        
        if(harpoon.hookCancelled)
            OnHookCancelled();

        if (totalDist >= maxDist)
            OnHookCancelled();

        if (retracted && enemies.Count > 0)
            CheckIfListElementsNull();

                  
    }

    private void OnDestroy()
    {
        Debug.Log("Total distance travelled:" + totalDist);
    }

    private void CheckIfListElementsNull()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                OnHookCancelled();
            }
        }
    }    

    private void PullEnemyToPlayer()
    {
        //For each enemy in the enemies transform list: Set as a child of this transform(the hook) and set their transform.position to the same position that the hook is at.
        foreach (Transform enemy in enemies)
        {
            if(enemy != null)
                enemy.GetComponent<Enemy>().nav.speed = 0;

            if (retracted && enemy != null)
            {
                enemy.SetParent(this.transform);
                enemy.transform.position = Vector3.MoveTowards(origin, target, currentSpeed);
            }
        }
    }

    private void OnTimerEnd()
    {
        //If current timer is less than or equal to 0 than run a for loop that iterates through the enemies list backwards starting at the last element of the index
        //and unparent each enemy from the list then remove all the elements from the list.
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if(enemies[i] != null)
            {
                enemies[i].transform.parent = null;
                enemies[i].GetComponent<Enemy>().nav.speed = 1;
                enemies.Remove(enemies[i]);
            }           
        }
    }

    private void OnHookCancelled()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if(enemies[i] != null)
            {
                enemies[i].transform.parent = null;
                enemies[i].GetComponent<Enemy>().SetSpeed(Enemy.EnemyStates.offHook);
                enemies.Remove(enemies[i]);
            }           
        }
        harpoon.staticHook.SetActive(true);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the trigger collided object has tag "Enemy", then add the Transform to the enemies list.
        if(other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.transform);
        }
    }
}
