using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RigidbodyDOTween
{    
    public static Tweener DOMove(this Rigidbody rigidbody, Vector3 endValue, float duration)
    {
        return DOTween.To(() => rigidbody.position, rigidbody.MovePosition, endValue, duration).SetId(rigidbody);
    }

    public static Tweener DORotate(this Rigidbody rigidbody, Vector3 endValue, float duration)
    {
        return DOTween.To(() => rigidbody.rotation, rigidbody.MoveRotation, endValue, duration).SetId(rigidbody);
    }    
}
