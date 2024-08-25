using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowerAIScript : MonoBehaviour
{

    public GameObject followTarget;
    public bool needToOverrideFollowTarget = false;
    public GameObject overrideFollowTarget;
    private NavMeshAgent _agent;

    private CharacterScript _character;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _character = GetComponent<CharacterScript>();
        _agent = GetComponent<NavMeshAgent>();
        
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.stoppingDistance = 1.25f;
        _agent.acceleration = 25;
        _agent.radius = 0.2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_agent.enabled || !_agent.isOnNavMesh) return;
        _agent.speed = _agent.remainingDistance < 1.75 ? 2.3f : 6f;

        var target = needToOverrideFollowTarget ? overrideFollowTarget : followTarget;
        _agent.SetDestination(target.transform.position);

        _character.MoveByVector(_agent.velocity, _agent.speed, false);
    }

    public bool AIEnabled
    {
        get => GetComponent<NavMeshAgent>().enabled;
        set
        {
            GetComponent<NavMeshAgent>().enabled = value;
            GetComponent<BoxCollider2D>().isTrigger = value;
        }
    }
}
