using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

abstract public class IACharacter : MonoBehaviour {

    protected Animator characterStateMachine;
    protected List<Sprite> toolSprites = new List<Sprite>();
    protected SpriteRenderer sr;

    // Use this for initialization
    protected virtual void Start ()
    {
        characterStateMachine = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
	
    public virtual Animator GetCharacterStateMachine() { return characterStateMachine; }

	// Update is called once per frame
	void Update () {
		
	}
}
