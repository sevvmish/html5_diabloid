using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forShaders : MonoBehaviour
{
    private void Awake()
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        
        if (mr == null)
        {
            SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
            smr.SetPropertyBlock(mpb);
        }
        else
        {
            mr.SetPropertyBlock(mpb);
        }
        
    }
}
