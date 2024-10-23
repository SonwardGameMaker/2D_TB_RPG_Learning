using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SkillList : MonoBehaviour
{
    private List<IngameActionBase> _actions;
    private List<IngameActionBase> _skillActions;

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    public void Setup()
    {
        _skillActions = new List<IngameActionBase>();
    }

    public List<IngameActionBase> Actions { get => _actions; }
    public List<IngameActionBase> SkillActions { get => _skillActions; }
}
