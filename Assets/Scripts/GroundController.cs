using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
public class GroundController : MonoBehaviour
{
    public void OnGroundClick(BaseEventData data)
    {
        Vector3 clickPos = Vector3.zero;
        // This function needs information about a click so cast the BaseEventData to a PointerEventData.
        PointerEventData pData = (PointerEventData)data;
        // Try and find a point on the nav mesh nearest to the world position of the click and set the destination to that.
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pData.pointerCurrentRaycast.worldPosition, out hit, 4, NavMesh.AllAreas))
            clickPos = hit.position;
        else
            // In the event that the nearest position cannot be found, set the position as the world position of the click.
            clickPos = pData.pointerCurrentRaycast.worldPosition;
        var character = FindObjectOfType<CharacterController>();
        character.AddCommand(new MoveCommand(clickPos));
    }
}
