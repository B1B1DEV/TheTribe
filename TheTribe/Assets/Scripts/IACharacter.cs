using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

abstract public class IACharacter : MonoBehaviour {

    protected Animator characterStateMachine;

	// Use this for initialization
	protected virtual void Start ()
    {
        characterStateMachine = GetComponent<Animator>();
    }
	
    public virtual Animator GetCharacterStateMachine() { return characterStateMachine; }

	// Update is called once per frame
	void Update () {
		
	}
}
