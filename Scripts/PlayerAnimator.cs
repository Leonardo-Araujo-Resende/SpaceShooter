using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;

    public void PlayAnimation(string animationName){
        playerAnim.Play(animationName);
    }
}
