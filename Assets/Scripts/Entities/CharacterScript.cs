using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterScript : Identifiable
{ 
    public enum Direction
    {
        Down, Left, Right, Up
    }

    public Vector2 direction;
    public CharacterScriptableObject characterModel;
    public bool playable;
    public List<Interactable> objectsToInteract = new();
    public bool forceRotating = false;
    
    private const int AnimationTick = 8;
    private int _animationCounter;
    private int _animationFrame;
    private Direction _lastDirection;
    private Vector2 _lastNonZeroDirection;
    
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody2D;

    public override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        PerformAnimation(0);
        transform.localScale = new Vector3(characterModel.size, characterModel.size, 1);
    }

    public void FixedUpdate()
    {
        if (!forceRotating) return;

        var directionVal = (int) (Time.time * 8) % 4;

        var direction = directionVal switch
        {
            0 => Direction.Left,
            1 => Direction.Up,
            2 => Direction.Right,
            3 => Direction.Down,
            _ => Direction.Down
        };
        
        SetDirection(direction);
    }

    public void MoveByVector(Vector2 movementDirection, float speed, bool changeObjectVelocity = true)
    {
        if (changeObjectVelocity)
            _rigidbody2D.velocity = movementDirection * speed;
        
        direction = movementDirection;
        if (direction != Vector2.zero)
            _lastNonZeroDirection = direction;
        
        // Do not animate if animation handles by Update rotation (Soloway)
        if (!forceRotating)
            PerformAnimation(speed);
    }

    public void SetDirection(Direction direction)
    {
        _lastDirection = direction;
        _spriteRenderer.sprite = FrameForDirection(direction, 0, true);
    }

    private void PerformAnimation(float speed)
    {
        var isIdle = false;

        if (Math.Abs(direction.x) > Math.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                _lastDirection = Direction.Right;
            }
            else if (direction.x < 0)
            {
                _lastDirection = Direction.Left;
            }
        }

        if (Math.Abs(direction.x) < Math.Abs(direction.y))
        {
            switch (direction.y)
            {
                case > 0:
                    _lastDirection = Direction.Up;
                    break;
                case < 0:
                    _lastDirection = Direction.Down;
                    break;
            }
        }
        
        if (direction == Vector2.zero)
        {
            _animationCounter = 0;
            _animationFrame = 0;
            isIdle = true;
        }

        isIdle |= speed == 0;
        
        _spriteRenderer.sprite = FrameForDirection(_lastDirection, _animationFrame, isIdle);

        _animationCounter += Math.Min((int)speed / 2, 2);
        if (_animationCounter >= AnimationTick)
        {
            _animationCounter = 0;
            _animationFrame++;
            _animationFrame %= 4;
        }
    }

    public Vector2 GetLastDirectionVector()
    {
        return _lastNonZeroDirection;
    }

    private Sprite FrameForDirection(Direction direction, int frame, bool isIdle)
    {
        var localFrame = !isIdle ? frame : 1;
        
        int[] animationSequence = { 2, 1, 0, 1 };

        return direction switch
        {
            Direction.Down => characterModel.tileset[animationSequence[localFrame]],
            Direction.Left => characterModel.tileset[3 + animationSequence[localFrame]],
            Direction.Right => characterModel.tileset[6 + animationSequence[localFrame]],
            Direction.Up => characterModel.tileset[9 + animationSequence[localFrame]],
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}