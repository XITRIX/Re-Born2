using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowerAIScript : MonoBehaviour
{

    public GameObject followTarget;
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
        _agent.radius = 0.01f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_agent.enabled) return;
        _agent.speed = _agent.remainingDistance < 1.75 ? 2.3f : 6f;
        
        _agent.SetDestination(followTarget.transform.position);
        _character.MoveByVector(_agent.velocity, _agent.speed, false);
    }
}
