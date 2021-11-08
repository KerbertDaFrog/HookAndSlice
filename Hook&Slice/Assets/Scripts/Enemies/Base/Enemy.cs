using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    #region Fields
    public enum EnemyStates
	{
		idle,
		attacking,
		chasing,
		staggered,
		frenzy,
		dead,
		onHook,
		offHook
	}

	public EnemyStates currentState;
	private EnemyStates previousState;

	[Header("NavMesh")]
	public NavMeshAgent nav;

	[Header("Player")]
	[SerializeField]
	private Transform player;

	[Header("GameObjects")]
	[SerializeField]
	private GameObject damageBox;
	[SerializeField]
	private GameObject corpse;

	[Header("Movement Speed Variables")]
	[SerializeField]
	private float walkSpeed = 1f;
	[SerializeField]
	private float runSpeed = 4f;

	[Header("Attack Variables")]
	[SerializeField]
	private float timeBetweenAttacks;
	[SerializeField]
	private float setAttackDelay;
	[SerializeField]
	private float attackDelay;
	[SerializeField]
	private float attackRange;
	[SerializeField]
	private int damage;

	[Header("Booleans")]
	[SerializeField]
	protected bool playerInSightRange;
	[SerializeField]
	protected bool playerInAttackRange;
	[SerializeField]
	private bool hasAttacked;
	//public bool isDead = false;
	[SerializeField]
	protected bool chasing;
	[SerializeField]
	protected bool hasSeenPlayer;

	[Header("FOV Variables")]
	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	[Header("Layer Masks")]
	[SerializeField]
	private LayerMask targetMask;
	[SerializeField]
	private LayerMask obstacleMask;

	[Header("Lists")]
	//[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	[Header("FOV Mesh")]
	[SerializeField]
	private float meshResolution;
	[SerializeField]
	private int edgeResolveIterations;
	[SerializeField]
	private float edgeDstThreshold;
	[SerializeField]
	private float maskCutawayDst = .1f;

	[SerializeField]
	private MeshFilter viewMeshFilter;
	Mesh viewMesh;

	[Header("Animator")]
	[SerializeField]
	protected Animator anim;
    #endregion

    private void Awake()
	{
		player = GameObject.Find("Player").transform;
		nav = GetComponent<NavMeshAgent>();
	}

	protected virtual void Start()
	{
		if (viewMeshFilter != null)
		{
			viewMesh = new Mesh();
			viewMesh.name = "View Mesh";
			viewMeshFilter.mesh = viewMesh;
		}

		StartCoroutine("FindTargetsWithDelay", .2f);
	}

    #region Updates
    protected virtual void Update()
	{
		//If the enemy has already seen the player, this bool will be checked until death of said enemy.
		if (playerInSightRange && currentState != EnemyStates.dead)
			hasSeenPlayer = true;

		if (!hasAttacked)
			attackDelay = Mathf.Clamp(attackDelay -= Time.deltaTime, 0f, setAttackDelay);

		EnemyBehaviour();
	}

	private void FixedUpdate()
	{
		int groundMask = 1 << 8;

		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, groundMask))
        {

        }
	}

    private void LateUpdate()
	{
		DrawFieldOfView();
	}
	#endregion

	#region EnemyBehaviour
	protected virtual void EnemyBehaviour()
	{
		if (currentState != EnemyStates.dead)
		{
			if (currentState == EnemyStates.idle)
			{
				if (playerInSightRange && !playerInAttackRange)
				{
					SetState(EnemyStates.chasing);
				}
				else if (playerInSightRange && playerInAttackRange)
				{
					SetState(EnemyStates.attacking);
				}
			}
			else if(currentState == EnemyStates.chasing)
            {
				if (playerInSightRange && playerInAttackRange)
				{
					SetState(EnemyStates.attacking);
				}
			}
			else if(currentState == EnemyStates.attacking)
            {
				if (playerInSightRange && !playerInAttackRange)
				{
					SetState(EnemyStates.chasing);
				}
			}
            else
            {
				if (!playerInSightRange && !playerInAttackRange && !hasSeenPlayer)
				{
					SetState(EnemyStates.idle);
				}
			}
		}

		switch (currentState)
        {
            case EnemyStates.chasing:
				Chase();
                break;
        }
    }

	protected virtual void Idle() { }

	protected virtual void Chase()
	{
		nav.speed = runSpeed;

		//Rather than set the target to be position of the player (The enemy wouldn't stop until they overlap you) 
		// I set it to be just back from the player so they stop when they're in front of you

		// Get vector (direction and distance) to the player. Normalazing turns it into pure direction only
		Vector3 chasePos = (transform.position - player.position).normalized;

		// multiply chase pos by the attack range turns the direction into the target attackrange distance
		// Subtracting the new chase pos from the player position returns the target position for attacking
		chasePos = player.position - chasePos * -attackRange;

		//nav.SetDestination(chasePos);
		nav.SetDestination(player.position);
	}

	protected virtual void Die() { }

	protected virtual void Staggered() { }

	protected virtual void OnHook() { }

	protected virtual void OffHook() { }
	#endregion

	#region State Operations
	public virtual void SetState(EnemyStates state)
	{
		if (currentState != state)
		{
			Debug.Log("<color=orange>" + gameObject.name + " is going from " + currentState + " to " + state + "</color>");
			previousState = currentState;

			#region Enter State Operations
			switch (state)
			{
				case EnemyStates.idle:
					GoToIdleState();
					break;

				case EnemyStates.attacking:
					GoToAttackState();
					break;

				case EnemyStates.chasing:
					GoToChaseState();
					break;

				case EnemyStates.dead:
					GoToDeadState();
					break;

				case EnemyStates.staggered:
					GoToStaggeredState();
					break;

				case EnemyStates.frenzy:
					GoToFrenzyState();
					break;

				case EnemyStates.onHook:
					GoToOnHookState();
					SetSpeed(EnemyStates.onHook);
					break;

				case EnemyStates.offHook:
					GoToOffHookState();
					SetSpeed(EnemyStates.offHook);
					break;
			}
			#endregion

			#region Leave State Operations
			switch (previousState)
			{
				case EnemyStates.idle:
					LeaveIdleState();
					break;
				case EnemyStates.attacking:
					LeaveAttackState();
					break;
				case EnemyStates.chasing:
					LeaveChaseState();
					break;
				case EnemyStates.staggered:
					LeaveStaggeredState();
					break;
				case EnemyStates.frenzy:
					LeaveFrenzyState();
					break;
				case EnemyStates.dead:
					LeaveDeadState();
					break;
				case EnemyStates.onHook:
					LeaveOnHookState();
					break;
				case EnemyStates.offHook:
					LeaveOffHookState();
					break;
			}
			#endregion
		}
	}

	#region State Transitions
	protected virtual void GoToIdleState() 
	{
		currentState = EnemyStates.idle;
		anim.SetBool("caught", false);
		anim.SetBool("walk", false);
	}

	protected virtual void LeaveIdleState() { }

	protected virtual void GoToAttackState()
    {
		currentState = EnemyStates.attacking;
		anim.SetBool("attack", true);
    }

	protected virtual void LeaveAttackState()
    {
		anim.SetBool("attack", false);
    }

	protected virtual void GoToChaseState()
    {
		anim.SetBool("walk", true);
		currentState = EnemyStates.chasing;
	}
	
	protected virtual void LeaveChaseState()
    {
		//anim.SetBool("walk", false);
	}

	protected virtual void GoToDeadState()
	{
		currentState = EnemyStates.dead;
		this.GetComponent<BoxCollider>().isTrigger = true;
		anim.SetBool("caught", false);
		anim.SetBool("attack", false);
		anim.SetBool("walk", false);
		anim.SetBool("dead", true);
		StartCoroutine("Dead");
	}

	//destoying the gameobject after dead - Alex

	IEnumerator Dead()
    {
		yield return new WaitForSeconds(0.5f);
		Instantiate(corpse); //try raycast hit down on ground to instantiate on ground.
		Destroy(gameObject);
    }

	protected virtual void LeaveDeadState()
	{
		anim.SetBool("dead", false);
	}

	protected virtual void GoToStaggeredState()
	{
		currentState = EnemyStates.staggered;
		anim.SetBool("caught", true);
		anim.SetBool("walk", false);	
	}
	protected virtual void LeaveStaggeredState()
	{
        //anim.SetBool("caught", false);
    }

	protected virtual void GoToFrenzyState() 
	{ 
		currentState = EnemyStates.frenzy;
	}

	protected virtual void LeaveFrenzyState() { }

	protected virtual void GoToOnHookState() 
	{
		currentState = EnemyStates.onHook;
		this.GetComponent<BoxCollider>().isTrigger = true;
		anim.SetBool("caught", true);
	}

	protected virtual void LeaveOnHookState() 
	{
		if(currentState != EnemyStates.dead)
        {
			this.GetComponent<BoxCollider>().isTrigger = false;
		}
		anim.SetBool("caught", false);
	}

	protected virtual void GoToOffHookState() 
	{
		currentState = EnemyStates.offHook;
		anim.SetBool("caught", false);
	}
	protected virtual void LeaveOffHookState() { }
    #endregion

    public virtual void SetSpeed(EnemyStates speed)
	{
		switch (speed)
		{
			case EnemyStates.onHook:
				nav.speed = 0;
				break;
			case EnemyStates.offHook:
				nav.speed = runSpeed;
				break;
		}
	}
	#endregion

	#region FindTargets
	IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	private void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
		Collider[] targetsInAttackRadius = Physics.OverlapSphere(transform.position, attackRange, targetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					playerInSightRange = true;
					visibleTargets.Add(target);
				}
			}
		}

		for (int i = 0; i < targetsInAttackRadius.Length; i++)
		{
			Transform target = targetsInAttackRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					playerInAttackRange = true;
					visibleTargets.Add(target);
				}
			}
		}

		if (targetsInViewRadius.Length == 0 || currentState == EnemyStates.dead)
			playerInSightRange = false;


		if (targetsInAttackRadius.Length == 0 || currentState == EnemyStates.dead)
			playerInAttackRange = false;
	}
    #endregion

    #region FOVMeshDraw
    private void DrawFieldOfView()
	{
		int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
		float stepAngleSize = viewAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3>();
		ViewCastInfo oldViewCast = new ViewCastInfo();
		for (int i = 0; i <= stepCount; i++)
		{
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
			ViewCastInfo newViewCast = ViewCast(angle);

			if (i > 0)
			{
				bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.dst - newViewCast.dst) > edgeDstThreshold;
				if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded))
				{
					EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
					if (edge.pointA != Vector3.zero)
					{
						viewPoints.Add(edge.pointA);
					}
					if (edge.pointB != Vector3.zero)
					{
						viewPoints.Add(edge.pointB);
					}
				}

			}


			viewPoints.Add(newViewCast.point);
			oldViewCast = newViewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];

		vertices[0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++)
		{
			vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]) + Vector3.forward * maskCutawayDst;

			if (i < vertexCount - 2)
			{
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}
		}

		if (viewMeshFilter != null)
		{
			viewMesh.Clear();

			viewMesh.vertices = vertices;
			viewMesh.triangles = triangles;
			viewMesh.RecalculateNormals();
		}
	}


	EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
	{
		float minAngle = minViewCast.angle;
		float maxAngle = maxViewCast.angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		for (int i = 0; i < edgeResolveIterations; i++)
		{
			float angle = (minAngle + maxAngle) / 2;
			ViewCastInfo newViewCast = ViewCast(angle);

			bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDstThreshold;
			if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded)
			{
				minAngle = angle;
				minPoint = newViewCast.point;
			}
			else
			{
				maxAngle = angle;
				maxPoint = newViewCast.point;
			}
		}

		return new EdgeInfo(minPoint, maxPoint);
	}


	ViewCastInfo ViewCast(float globalAngle)
	{
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit hit;

		if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
		{
			return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
		}
		else
		{
			return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

    public struct ViewCastInfo
	{
		public bool hit;
		public Vector3 point;
		public float dst;
		public float angle;

		public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
		{
			hit = _hit;
			point = _point;
			dst = _dst;
			angle = _angle;
		}
	}

	private struct EdgeInfo
	{
		public Vector3 pointA;
		public Vector3 pointB;

		public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
		{
			pointA = _pointA;
			pointB = _pointB;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
		//Gizmos.color = Color.yellow;
		//Gizmos.DrawWireSphere(transform.position, sightRange);
	}
    #endregion

    #region Getters
    public int Damage()
    {
        return damage;
    }
    #endregion
}