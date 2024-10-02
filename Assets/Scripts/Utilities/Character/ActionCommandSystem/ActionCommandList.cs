using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCommandList
{
    private List<ActionCommandBase> _commandList;
    private int count;

    public void ExecuteCommands(List<ActionCommandBase> commandList, Action onCommandsEndeds)
    {
        Reset();
        for (int i = 0; i < commandList.Count - 1; i++)
        {
            commandList[i].OnExecutionEnded += ExecuteNext;
        }
        commandList[commandList.Count - 1].OnExecutionEnded += onCommandsEndeds;

        commandList[0].Execute();
    }
    public void ExecuteCommands(ActionCommandBase command, Action onCommandsEndeds)
        => ExecuteCommands(new List<ActionCommandBase> { command }, onCommandsEndeds);

    public void Reset()
    {
        _commandList.Clear();
        count = 0;
    }

    private void ExecuteNext()
    {
        count++;
        if (count >= _commandList.Count)
            throw new System.Exception("Execution list in not valid");

        _commandList[count].Execute();
    }
}
