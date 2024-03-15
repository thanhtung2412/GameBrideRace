using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private Vector3 destination;

    public bool IsDestination => Vector3.Distance(destination, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) > 0.1f;
    

    IState<Bot> currentState;
    //protected override void Start()
    //{
    //    base.Start();
    //    ChangeState(new PatrolState());
    //}
    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;      
        destination.y = 0;
        agent.SetDestination(position);
       
    }
    private void Update()
    {
        if(GameManager.Instance.IsState(GameState.GamePlay) && currentState != null)
        {
            currentState.OnExcute(this);           
            //check stair
            CanMove(TF.position);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeAnim(Constans.ANIM_RUN);

    }
    public void ChangeState(IState<Bot> state)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void MoveStop()
    {
        agent.enabled = false;
    }
}
