using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    [SerializeField]
    private EnemyState _currentState = EnemyState.Chase;
    private Transform _player;
    private CharacterController _controller;
    [SerializeField]
    private float _speed;
    private float _gravity = 9.81f;
    private Vector3 _direction, _velocity;
    private Health _playerHealth;
    [SerializeField]
    private float _attackDelay = 1.5f;
    private float _nextAttack = -1;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<Health>();
        if (_player == null || _playerHealth == null)
        {
            Debug.Log("The player is null");
        }
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Chase:
                CalculateMovement();
                break;
            case EnemyState.Idle:

                break;
        }
    }

    void CalculateMovement()
    {
        if (_controller.isGrounded == true)
        {
            _direction = _player.position - transform.position;
            _direction.Normalize();
            _direction.y = 0;
            //rotate towards player
            transform.rotation = Quaternion.LookRotation(_direction);
            _velocity = _direction * _speed;
        }

        _velocity.y -= _gravity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    private void Attack()
    {
        if (Time.time > _nextAttack)
        {
            if (_playerHealth != null) _playerHealth.Damage(10);
            _nextAttack = Time.time + _attackDelay;
        }
    }

    public void StartAttack()
    {
        _currentState = EnemyState.Attack;
    }

    public void StopAttack()
    {
        _currentState = EnemyState.Chase;
    }
   
}
