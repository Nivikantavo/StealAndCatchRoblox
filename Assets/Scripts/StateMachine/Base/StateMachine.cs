using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : IStateSwitcher
{
    private List<IState> _states = new List<IState>();
    private IState _currentState;

    public StateMachine(List<IState> states)
    {
        _states = states;

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void AddState(IState state)
    {
        _states.Add(state);
    }

    public void SwitchState<T>() where T : IState
    {
        IState nextState = _states.FirstOrDefault(state => state is T);

        //проверка на null

        _currentState.Exit();
        _currentState = nextState;
        _currentState.Enter();
    }

    public void Update()
    {
        _currentState.Update();
    }

    public void InputAction(IInteractor interactor)
    {
        _currentState.InputAction(interactor);
    }
}
