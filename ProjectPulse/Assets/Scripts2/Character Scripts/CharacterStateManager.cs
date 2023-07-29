using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterStateManager : MonoBehaviour
{
    
    [SerializeField]
    internal CharacterMovement characterMovement;
    [SerializeField]
    internal CharacterStatus characterStatus;
    [SerializeField]
    internal CharacterWeapon weapon;

    internal string currentState;
    [SerializeField] internal Animator anim;

    internal void ChangeStateWithAnimation(string newState)
    {
        if (newState != currentState)
        {
            anim.Play(newState);
            
            currentState = newState;
            //Debug.Log(gameObject.name + " CHANGED STATE TO " + newState);
        }
    }
    internal void ChangeStateWithAnimationReset(string newState)
    {
            anim.Play(newState, -1, 0f);
            
            currentState = newState;
            //Debug.Log(gameObject.name + " CHANGED STATE TO " + newState);
        
    }

    internal void ChangeState(string newState)
    {
        if (newState != currentState)
        {
            currentState = newState;
        }
    }
    void OnDrawGizmos()
    {
        Handles.Label(transform.position, currentState);
    }
}
