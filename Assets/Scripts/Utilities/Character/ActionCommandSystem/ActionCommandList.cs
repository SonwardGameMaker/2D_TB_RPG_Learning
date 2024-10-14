using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

internal class ActionCommandList
{
    private List<ActionCommandBase> _commandList;
    private int _count;
    private bool _isBusy;

    #region events
    public event Action<bool, string> ExecutionEnded;
    #endregion

    #region init
    public ActionCommandList()
    {
        Reset();
    }
    #endregion

    #region external interctions
    public void ExecuteCommands(List<ActionCommandBase> commandList)
    {
        if (_isBusy)
        {
            OnExecutionEnded(false, "Character is busy");
            return;
        }
        _isBusy = true;

        Reset();
        _commandList = commandList;
        for (int i = 0; i < _commandList.Count - 1; i++)
        {
            _commandList[i].ExecutionEnded += ExecuteNext;
        }
        _commandList[_commandList.Count - 1].ExecutionEnded += OnExecutionEnded;

        _commandList[0].Execute();
    }
    public void ExecuteCommands(ActionCommandBase command)
        => ExecuteCommands(new List<ActionCommandBase> { command });
    #endregion

    #region internal operations
    private void ExecuteNext(bool status, string message)
    {
        if (!status)
        {
            OnExecutionEnded(status, message);
            return;
        }

        _count++;
        if (_count >= _commandList.Count)
            throw new Exception("Execution list in not valid");

        _commandList[_count].Execute();
    }

    private void Reset()
    {
        if (_commandList == null)
            _commandList = new List<ActionCommandBase>();
        else
            _commandList.Clear();

        _count = 0;
    }
    #endregion

    #region event triggers
    private void OnExecutionEnded(bool status, string message)
    {
        ExecutionEnded?.Invoke(status, message);
        _isBusy = false;

        for (int i = 0; i < _commandList.Count - 1; i++)
        {
            _commandList[i].ExecutionEnded -= ExecuteNext;
        }
        _commandList[_commandList.Count - 1].ExecutionEnded -= OnExecutionEnded;
    }
    #endregion
}
