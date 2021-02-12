using UnityEngine;
public abstract class Command
{
    public string name;
    public abstract void Execute(GameObject actor);
    public abstract bool IsCompleted();
}
