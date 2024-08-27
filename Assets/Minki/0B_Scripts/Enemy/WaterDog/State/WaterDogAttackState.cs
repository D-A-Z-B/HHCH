using UnityEngine;

public class WaterDogAttackState : AttackState
{
    public WaterDogAttackState(Enemy enemy, EnemyStateMachine<EnemyStateEnum> stateMachine, string animationBoolName) : base(enemy, stateMachine, animationBoolName) { }

    private float _moveX, _moveY;
    private float _timer = 0f, _jumpTime;
    private float _yRotation;
    private Vector3 _startPos;

    public override void Enter() {
        base.Enter();

        _enemy.StopImmediately(false);

        _moveX = Mathf.Abs(PlayerManager.Instance.Head.transform.position.x - _enemy.transform.position.x);
        _moveY = Mathf.Abs(PlayerManager.Instance.Head.transform.position.y - _enemy.transform.position.y);

        _timer = 0.05f;
        _jumpTime = (_enemy as WaterDog).attackTime;
        _yRotation = _enemy.FacingDirection * 90f - 90;

        _startPos = _enemy.transform.position;
        _startPos.x += _moveX * _enemy.FacingDirection;
    }

    public override void UpdateState() {
        _timer += Time.deltaTime;

        float percent = _timer / _jumpTime;
        percent = EaseInOutCubic(percent);

        float percentMulPI = percent * Mathf.PI;
        Vector3 direction = new Vector3(Mathf.Cos(percentMulPI) * _moveX * -_enemy.FacingDirection, Mathf.Sin(percentMulPI) * _moveY);

        _enemy.transform.position = _startPos + direction;
        _enemy.transform.rotation = Quaternion.Euler(0, _yRotation, (percent * 2 - 1) * -90f);
        
        if(percent >= 0.85f) {
            _stateMachine.ChangeState(EnemyStateEnum.Chase);
            _enemy.transform.rotation = Quaternion.Euler(0, _yRotation, 0);
        }
    }

    private float EaseInOutCubic(float x) {
        return x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2 : (2 - Mathf.Pow(2, -20 * x + 10)) / 2;
    }
}
