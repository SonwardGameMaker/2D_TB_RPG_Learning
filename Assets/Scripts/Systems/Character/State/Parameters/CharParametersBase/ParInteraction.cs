using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using UnityEngine;

[Serializable]
public class ParInteraction
{
    private List<CharParameterBase> _affectors;
    private List<CharParameterBase> _targets;

    public ModValueCalculateLogic CalculateLogic { get; set; }

    public List<CharParameterBase> Affectors {  get { return _affectors; } private set { } }
    public List<CharParameterBase> Targets { get { return _targets; } private set { } }

    public ParInteraction(List<CharParameterBase> affectors, List<CharParameterBase> targets, ModValueCalculateLogic CalculateLogic)
    {
        _affectors = affectors;
        _targets = targets;
        this.CalculateLogic = CalculateLogic;
        foreach (CharParameterBase affector in _affectors)
        {
            Type affectorType = affector.GetType();
            Type[] affectorInterfaces = affectorType.GetInterfaces();

            foreach (Type parameterInterface in affectorInterfaces)
            {
                EventInfo[] events = parameterInterface.GetEvents();
                foreach (EventInfo eventInfo in events)
                {
                    MethodInfo addMethod = eventInfo.GetAddMethod();
                    if (addMethod != null)
                    {
                        Delegate handler = Delegate.CreateDelegate(
                            eventInfo.EventHandlerType,
                            this,
                            GetType().GetMethod(nameof(Affect), BindingFlags.Public | BindingFlags.Instance)
                            );

                        addMethod.Invoke(affector, new object[] { handler });
                    }
                }
            }
        }
    }
    #region derived constructors and destructor
    public ParInteraction(List<CharParameterBase> affectors, List<CharParameterBase> targets) : this(affectors, targets, null) { }
    public ParInteraction(List<CharParameterBase> affectors, CharParameterBase target) : this(affectors, new List<CharParameterBase> { target }, null) { }
    public ParInteraction(CharParameterBase affector, List<CharParameterBase> targets) : this(new List<CharParameterBase> { affector }, targets, null) { }
    public ParInteraction(CharParameterBase affector, CharParameterBase target) : this(new List<CharParameterBase> { affector }, new List<CharParameterBase> { target }, null) { }
    public ParInteraction(List<CharParameterBase> affectors, CharParameterBase target, ModValueCalculateLogic CalculateLogic) : this(affectors, new List<CharParameterBase> { target }, CalculateLogic) { }
    public ParInteraction(CharParameterBase affector, List<CharParameterBase> targets, ModValueCalculateLogic CalculateLogic) : this(new List<CharParameterBase> { affector }, targets, CalculateLogic) { }
    public ParInteraction(CharParameterBase affector, CharParameterBase target, ModValueCalculateLogic CalculateLogic) : this(new List<CharParameterBase> { affector }, new List<CharParameterBase> { target }, CalculateLogic) { }
    ~ParInteraction() 
    {
        foreach(CharParameterBase affector in _affectors)
        {
            Type affectorType = affector.GetType();
            Type[] affectorInterfaces = affectorType.GetInterfaces();

            foreach (Type parameterInterface in affectorInterfaces)
            {
                EventInfo[] events = parameterInterface.GetEvents();
                foreach (EventInfo eventInfo in events)
                {
                    MethodInfo removeMethod = eventInfo.GetRemoveMethod();
                    if (removeMethod != null)
                    {
                        Delegate handler = Delegate.CreateDelegate(
                            eventInfo.EventHandlerType,
                            this,
                            this.GetType().GetMethod(nameof(Affect), BindingFlags.NonPublic | BindingFlags.Instance)
                            );

                        removeMethod.Invoke(affector, new object[] { handler });
                    }
                }
            }
        }
    }
    #endregion

    public void Affect()
    {
        if (CalculateLogic != null)
            CalculateLogic(ref _affectors, ref _targets);
        else Debug.LogErrorFormat("Affect logic is not set");
    }
}
