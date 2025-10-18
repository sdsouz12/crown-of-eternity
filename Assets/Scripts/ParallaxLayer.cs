using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier = 1f;
    [SerializeField] private float imageWidthOffset = 10f;
    [SerializeField] private bool moveYWithCamera = false;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMoveX, float distanceToMoveY)
    {

        Vector3 movement = Vector3.right * (distanceToMoveX * parallaxMultiplier);

        if (moveYWithCamera)
            movement += Vector3.up * (distanceToMoveY * parallaxMultiplier);

        background.position += movement;
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageRightEdge = (background.position.x + imageHalfWidth) - imageWidthOffset;
        float imageLeftEdge = (background.position.x - imageHalfWidth) + imageWidthOffset;

        if (imageRightEdge < cameraLeftEdge)
            background.position += Vector3.right * imageFullWidth;
        else if (imageLeftEdge > cameraRightEdge)
            background.position += Vector3.left * imageFullWidth;
    }
}
