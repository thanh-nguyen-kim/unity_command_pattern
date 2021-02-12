using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour, IWorkable
{
    public const string WORK = "MakeCoffee";
    [SerializeField] private Transform target;
    private bool isWorkDone = false;
    public void Work()
    {
        StartCoroutine(DoWork());
    }
    private IEnumerator DoWork()
    {
        isWorkDone = false;
        var anim = GetComponent<Animator>();
        anim.Play(WORK);
        yield return null;//wait for 1 frame for animator to change state
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//wait for work animation to complete
        isWorkDone = true;
    }
    public bool IsWorkDone()
    {
        return isWorkDone;
    }
    private void OnMouseDown()
    {
        var character = FindObjectOfType<CharacterController>();
        character.AddCommand(new MoveCommand(target.position));
        character.AddCommand(new WorkCommand(this));
    }
}
