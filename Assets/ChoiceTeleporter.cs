using UnityEngine;

public class ChoiceTeleporter : MonoBehaviour
{
    public Transform vrPlayer;
    public Transform mainCamera;
    public Transform correctSpawnPoint;
    public Transform wrongSpawnPoint;

    public string correctTag = "CorrectEPI";
    public string wrongTag = "WrongEPI";

    public void CheckItemAndTeleport()
    {
        if (gameObject.CompareTag(correctTag))
        {
            Debug.Log("Correct item grabbed! Teleporting to Game 3.");
            TeleportPlayer(correctSpawnPoint);
        }
        else if (gameObject.CompareTag(wrongTag))
        {
            Debug.Log("Wrong item grabbed! Teleporting to Game 2.");
            TeleportPlayer(wrongSpawnPoint);
        }
    }

    private void TeleportPlayer(Transform destination)
    {
        if (vrPlayer != null && destination != null && mainCamera != null)
        {
            Vector3 trackingOffset = mainCamera.position - vrPlayer.position;
            trackingOffset.y = 0;
            vrPlayer.position = destination.position - trackingOffset;
            vrPlayer.rotation = destination.rotation;
        }
        else if (vrPlayer != null && destination != null)
        {
            vrPlayer.position = destination.position;
            vrPlayer.rotation = destination.rotation;
        }
    }
}
