using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDisplayManager : MonoBehaviour {

    public Sprite boneAspect;
    public Sprite woodAspect;
    public Sprite stoneAspect;
    public Sprite goldAspect;

    public void SwitchToWoodAspect() { SwitchMaterial(woodAspect); }
    public void SwitchToStoneAspect() { SwitchMaterial(stoneAspect); }
    public void SwitchToGoldAspect() { SwitchMaterial(goldAspect); }

    void SwitchMaterial(Sprite s)
    {
        foreach(Transform t in transform)
        {
            t.GetComponent<SpriteRenderer>().sprite = s;
        }
    }

    
}
