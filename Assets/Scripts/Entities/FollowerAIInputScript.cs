using UnityEngine;


[RequireComponent(typeof(CharacterScript))]
public class FollowerAIInputScript : MonoBehaviour
{
    private CharacterScript _characterScript;
    public GameObject followTarget;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _characterScript = GetComponent<CharacterScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var direction = followTarget.transform.position - transform.position;
        if (direction.magnitude < 1.25) direction = Vector2.zero;

        float speed;
        if (direction.magnitude < 1) speed = 1.5f;
        else if (direction.magnitude < 1.75) speed = 2.5f;
        else speed = 6;
        
        direction.Normalize();
        _characterScript.MoveByVector(direction, speed);
    }
}
