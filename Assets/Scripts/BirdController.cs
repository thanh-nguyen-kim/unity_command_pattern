using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour, IActable
{
    public const string FLY = "FlyAway";
    private bool isActDone = false;
    [SerializeField] private Transform target;
    public void Act()
    {
        if (isActDone) return;//bird only acts one time
        StartCoroutine(DoAct());
    }
    private IEnumerator DoAct()
    {
        isActDone = false;
        var anim = GetComponent<Animator>();
        anim.Play(FLY);
        yield return null;//wait for 1 frame for animator to change state
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//wait for work animation to complete
        isActDone = true;
    }
    public bool IsActDone()
    {
        return isActDone;
    }
    private void OnMouseDown()
    {
        var character = FindObjectOfType<CharacterController>();
        character.AddCommand(new MoveCommand(target.position));
        character.AddCommand(new ActCommand(this));
    }
}
