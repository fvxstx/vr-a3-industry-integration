using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    public Transform vrPlayerTarget;
    public Transform mainCamera;

    public Transform destinationPoint;

    public void Teleport()
    {
        if (vrPlayerTarget != null && destinationPoint != null && mainCamera != null)
        {
            Vector3 trackingOffset = mainCamera.position - vrPlayerTarget.position;
            trackingOffset.y = 0;
            vrPlayerTarget.position = destinationPoint.position - trackingOffset;
            vrPlayerTarget.rotation = destinationPoint.rotation;

            Debug.Log("Player teleported to next game area.");
        }
        else if (vrPlayerTarget != null && destinationPoint != null)
        {
            vrPlayerTarget.position = destinationPoint.position;
            vrPlayerTarget.rotation = destinationPoint.rotation;
            Debug.Log("Player teleported to next game area (no tracking offset).");
        }
    }
}
