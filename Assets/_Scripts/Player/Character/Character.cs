using System;
using UnityEngine;

public class Character : PooledMonoBehaviour
{
    public event Action<int> Collected;
    public event Action<int> Damaged;
    public event Action<int> Died;
    public event Action<int> Finished;
    public CharacterType CharacterType => _characterType;
    public override int InitialPoolSize => 1;

    [SerializeField] CharacterType _characterType = CharacterType.Standard;
    private CharacterAnimationController _animator;
    private CharacterMovementController _movement;
    private CharacterState _state;
    private CharacterInfo _info;

#region "MonoBehaviour Methods"
    private void Awake()
    {
        _animator = new CharacterAnimationController(GetComponentInChildren<Animator>());
        _movement = new CharacterMovementController(transform, GetComponent<Rigidbody>());

        SetState(CharacterState.Idle);
    }

    private void Update()
    {
        if (_movement.IsActive)
        {
            _movement.Move();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<ICollectable>(out var collectable))
        {
            _info.IncreaseCollected(collectable.Reward);
            collectable.Collect();
            Collected?.Invoke(_info.Collected);

            return;
        }

        if (collider.TryGetComponent<IObstacle>(out var obstacle))
        {
            _info.DecreaesHealth(obstacle.Damage);
            obstacle.Stumble();
            Damaged?.Invoke(_info.Health);

            if (_info.IsDead)
            {
                SetState(CharacterState.Dead);
                Died?.Invoke(_info.Collected);
            }

            return;
        }

        if (collider.TryGetComponent<IEndZone>(out var endZone))
        {
            ParticleCreator.Instance.PlayCelebrationParticle(transform.position);
            Finished?.Invoke(_info.Collected);
            SetState(CharacterState.Dancing);
            return;
        }
    }

    private void OnDestroy()
    {
        _movement?.Dispose();
    }
#endregion

    public void Reset(CharacterInfo info)
    {
        SetState(CharacterState.Idle);
        _movement.Reset();
        _info = info;
        
        // when restarted, xbot object position parameters stuck
        var xbot = transform.GetChild(0);
        xbot.rotation = Quaternion.identity;
        xbot.localPosition = Vector3.zero;
    }

    public void Run()
    {
        SetState(CharacterState.Running);
    }

    private void SetState(CharacterState state)
    {
        _state = state;
        switch (_state)
        {
            case CharacterState.Idle: _animator.Idle(); _movement.IsActive = false; break;
            case CharacterState.Running: _animator.Run(); _movement.IsActive = true; break;
            case CharacterState.Dancing: _animator.Dance(); _movement.IsActive = false; break;
            case CharacterState.Dead: _animator.Idle(); _movement.IsActive = false; break;
        }
    }
  
}
