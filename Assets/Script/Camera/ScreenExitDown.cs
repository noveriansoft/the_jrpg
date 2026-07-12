using UnityEngine;

public class ScreenExitDown : MonoBehaviour
{
    public CameraTransition cameraTransition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        cameraTransition.MoveDown();
    }
}
