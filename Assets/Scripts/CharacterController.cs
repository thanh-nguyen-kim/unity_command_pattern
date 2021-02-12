using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterController : MonoBehaviour
{
    public const string IDLE = "Idle", WALK = "Walking";
    public int commandLength = 4;
    public Command[] commands;
    public Command activeCommand;
    [HideInInspector] public int firstIndex, lastIndex;
    public bool CanAddCommand()
    {
        int nextCommand = (lastIndex + 1) % commandLength;
        return commands[lastIndex] == null && commands[nextCommand] == null;
    }
    public void AddCommand(Command command)
    {
        if (commands[lastIndex] != null) return;
        commands[lastIndex] = command;
        lastIndex = (lastIndex + 1) % commandLength;
    }
    public bool CanPopCommand()
    {
        return commands[firstIndex] != null;
    }
    public Command PopCommand()
    {
        if (!CanPopCommand()) return null;
        Command result = commands[firstIndex];
        commands[firstIndex] = null;
        firstIndex = (firstIndex + 1) % commandLength;
        return result;
    }
    public void PlayAnimation(string animName)
    {
        GetComponent<Animator>().Play(animName);
    }
    protected IEnumerator PlayerLoop()//iterate through command list
    {
        bool isIdle = false;
        while (true)
        {
            if ((activeCommand == null || activeCommand.IsCompleted()))
            {
                if (CanPopCommand())
                {
                    isIdle = false;
                    activeCommand = PopCommand();
                    if (activeCommand != null)
                        activeCommand.Execute(gameObject);
                }
                else if (!isIdle)
                {
                    isIdle = true;
                    PlayAnimation(IDLE);
                }
            }
            yield return null;
        }
    }
    private void Start()
    {
        firstIndex = lastIndex = 0;
        commands = new Command[commandLength];
        StartCoroutine(PlayerLoop());
    }
}
