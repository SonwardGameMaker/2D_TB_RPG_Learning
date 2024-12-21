using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerAttackState : PlayerState
{
    private IAttackable _attackable;
    private int _attackRadius;

    #region init
    public PlayerAttackState(
        PlayerStateMachine stateMachine,
        PlayerIngameController playerController,
        IInputHandler inputHandler,
        CharacterInfo player,
        IAttackable attackable,
        int attackRadius) : base(stateMachine, playerController, inputHandler, player) 
            => Setup(attackable, attackRadius);

    public PlayerAttackState(
    PlayerStateMachine stateMachine,
    PlayerIngameController playerController,
    IInputHandler inputHandler,
    CharacterInfo player) : base(stateMachine, playerController, inputHandler, player) 
        => Setup();
    #endregion

    public IAttackable Attackable { get => _attackable; }
    public int AttackRadius { get => _attackRadius; }

    public override void EnterState()
    {
        _inputHandler.RMB_Pressed += ChangeToIdleState;
        _inputHandler.LMB_Pressed += Attack;

        _inputHandler.CancelButton_Pressed += ChangeToIdleState;

        _stateMachine.UiManager.SetAttackCursor();
    }

    public override void ExitState()
    {
        _inputHandler.RMB_Pressed -= ChangeToIdleState;
        _inputHandler.LMB_Pressed -= Attack;

        _inputHandler.CancelButton_Pressed -= ChangeToIdleState;

        _stateMachine.UiManager.SetBaseCursor();
    }

    public void Setup(IAttackable attackable, int attackRadius)
    {
        _attackable = attackable;
        _attackRadius = attackRadius;
    }
    public void Setup(IAttackable attackable) => Setup(attackable, 1);
    public void Setup(int attackRadius) => Setup(_player.GetComponentInChildren<Attackable>(), attackRadius);
    public void Setup() => Setup(_player.GetComponentInChildren<Attackable>(), 1);

    #region input event handlers
    private void Attack()
    {
        Vector3 mousePosition = GetMousePosition();
        TileNode targetNode = GetNodeByMousePosition(mousePosition);

        if (targetNode == null || !targetNode.IsWalkable) return;

        CharacterInfo characterOnTargetTile = targetNode.CharacterOnTile;

        if (characterOnTargetTile != null && characterOnTargetTile.tag == "Characters"
            && characterOnTargetTile.GetComponentInChildren<IDamagable>() != null)
        {
            _playerController.WalkAndAct(
                CalculatePath(mousePosition, _attackRadius),
                new AttackCommand(_attackable, characterOnTargetTile.GetComponentInChildren<IDamagable>()),
                mousePosition);

            ChangeToIdleState();
        }
    }

    private void ChangeToIdleState()
    {
        _stateMachine.ChangeState<PlayerIdleState>();
    }
    #endregion
}
