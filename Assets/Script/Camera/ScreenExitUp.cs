using UnityEngine;

public class ScreenExitUp : MonoBehaviour
{
    public CameraTransition cameraTransition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        cameraTransition.MoveUp();
    }
}
