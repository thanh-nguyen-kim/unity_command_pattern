using UnityEngine;
public class WorkCommand : Command
{
    private IWorkable worker;
    public WorkCommand(IWorkable worker)
    {
        name = "Work";
        this.worker = worker;
    }
    public override void Execute(GameObject actor)
    {
        worker.Work();
        actor.GetComponent<CharacterController>().PlayAnimation(CharacterController.IDLE);
    }
    public override bool IsCompleted()
    {
        return worker.IsWorkDone();
    }
}
