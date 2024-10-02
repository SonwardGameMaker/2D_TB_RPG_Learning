using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActionCommandList
{
    private List<ActionCommandBase> _commandList;
    private int _count;

    public ActionCommandList()
    {
        Reset();
    }

    public void ExecuteCommands(List<ActionCommandBase> commandList, Action onCommandsEndeds)
    {
        Reset();
        _commandList = commandList;
        for (int i = 0; i < _commandList.Count - 1; i++)
        {
            _commandList[i].OnExecutionEnded += ExecuteNext;
        }
        _commandList[_commandList.Count - 1].OnExecutionEnded += onCommandsEndeds;

        _commandList[0].Execute();
    }
    public void ExecuteCommands(ActionCommandBase command, Action onCommandsEndeds)
        => ExecuteCommands(new List<ActionCommandBase> { command }, onCommandsEndeds);

    public void Reset()
    {
        if (_commandList == null)
            _commandList = new List<ActionCommandBase>();
        else
            _commandList.Clear();

        _count = 0;
    }

    private void ExecuteNext()
    {
        _count++;
        if (_count >= _commandList.Count)
            throw new Exception("Execution list in not valid");

        _commandList[_count].Execute();
    }
}
