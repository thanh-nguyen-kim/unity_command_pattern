using UnityEngine;
public class ActCommand : Command
{
    private IActable worker;
    public ActCommand(IActable worker)
    {
        name = "Work";
        this.worker = worker;
    }
    public override void Execute(GameObject actor)
    {
        worker.Act();
        actor.GetComponent<CharacterController>().PlayAnimation(CharacterController.IDLE);
    }
    public override bool IsCompleted()
    {
        return worker.IsActDone();
    }
}
