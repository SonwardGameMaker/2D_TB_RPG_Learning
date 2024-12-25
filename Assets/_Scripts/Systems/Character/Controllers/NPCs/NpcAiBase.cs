using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcAiBase : ControllerManagerBase
{
    private CharacterIngameController _characterController;

    public override void Setup(CharacterInfo character, CharacterIngameController controller)
    {
        
    }

    public override void NewTurn()
    {
        _characterController.NewTurn();
    }
}
