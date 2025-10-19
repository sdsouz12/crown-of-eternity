using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    //private float lastCameraPositionX;
    private Vector3 lastCameraPosition;
    private float cameraHalfWidth;

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        lastCameraPosition = mainCamera.transform.position;
        InitializeLayers();
    }

    //private void FixedUpdate()
    //{
    //    float currentCameraPositionX = mainCamera.transform.position.x;
    //    float distanceToMove = currentCameraPositionX - lastCameraPositionX;
    //    lastCameraPositionX = currentCameraPositionX;

    //    float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;
    //    float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;

    //    foreach (ParallaxLayer layer in backgroundLayers)
    //    {
    //        layer.Move(distanceToMove);
    //        layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
    //    }
    //}
    private void FixedUpdate()
    {
        Vector3 currentCameraPosition = mainCamera.transform.position;
        Vector3 deltaMovement = currentCameraPosition - lastCameraPosition;
        lastCameraPosition = currentCameraPosition;

        float cameraLeftEdge = currentCameraPosition.x - cameraHalfWidth;
        float cameraRightEdge = currentCameraPosition.x + cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(deltaMovement.x, deltaMovement.y);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    private void InitializeLayers()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
            layer.CalculateImageWidth();
    }
}
