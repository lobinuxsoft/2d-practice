// Tomado de https://www.toptal.com/unity-unity3d/unity-ai-development-finite-state-machine-tutorial

using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState initialState;
    private Dictionary<Type, Component> cachedComponents;

    public BaseState CurrentState { get; set; }

    private void Awake() 
    {
        CurrentState = initialState;
        cachedComponents = new Dictionary<Type, Component>();
    }

    private void Update() => CurrentState.Execute(this);

    public new T GetComponent<T>() where T : Component
    {
        if (cachedComponents.ContainsKey(typeof(T)))
            return cachedComponents[typeof(T)] as T;

        var component = base.GetComponent<T>();

        if(component != null)
            cachedComponents.Add(typeof(T), component);

        return component;
    }
}