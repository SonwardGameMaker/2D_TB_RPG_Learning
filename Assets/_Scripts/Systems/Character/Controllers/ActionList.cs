using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionList : MonoBehaviour
{
    [SerializeField] private GameObject _skillContainer;

    private List<BehaviourScriptBase> _baseActions;
    private List<BehaviourScriptBase> _actions;
    private List<BehaviourScriptBase> _skillActions;

    public void Setup(CharacterBlank characterBlank)
    {
        _baseActions = new List<BehaviourScriptBase>();
        _actions = new List<BehaviourScriptBase>();
        _skillActions = new List<BehaviourScriptBase>();

        CharacterBlank character = transform.parent.GetComponent<CharacterBlank>();
        Animator animator = transform.parent.GetComponentInChildren<Animator>();

        _baseActions.Add(GetComponent<Movable>());
        _baseActions.Add(GetComponent<Attackable>());

        _skillActions.AddRange(_skillContainer.GetComponents<BehaviourScriptBase>());
        foreach (var skill in _skillActions)
            skill.Setup(character);
    }

    #region properties
    public List<BehaviourScriptBase> BaseActions { get => _baseActions ; }
    public List<BehaviourScriptBase> Actions { get => _actions; }
    public List<BehaviourScriptBase> Skills {  get => _skillActions; }
    #endregion

    #region getters
    public T GetBaseAction<T>() where T : BehaviourScriptBase
        => _baseActions.Find(a => a is T) as T;

    public T GetAction<T>() where T : BehaviourScriptBase
        => _actions.Find(a => a is T) as T;

    public T GetSkillAction<T>() where T : BehaviourScriptBase
        => _skillActions.Find(a => a is T) as T;
    #endregion

    #region external interactions
    public List<PlayerBehaviourScriptContainer> BaseActionContainers() 
        => _baseActions.Select(a => a.GenerateScriptContainer()).ToList();

    public List<PlayerBehaviourScriptContainer> ActionContainers()
        => _actions.Select(a => a.GenerateScriptContainer()).ToList();

    public List<PlayerBehaviourScriptContainer> SkillActionContainers()
        => _skillActions.Select(a => a.GenerateScriptContainer()).ToList();

    public List<PlayerBehaviourScriptContainer> AllActionsContainer()
    {
        List<PlayerBehaviourScriptContainer> result = new List<PlayerBehaviourScriptContainer>();
        result.AddRange(BaseActionContainers());
        result.AddRange(ActionContainers());
        result.AddRange(SkillActionContainers());
        return result;
    }
    #endregion
}
