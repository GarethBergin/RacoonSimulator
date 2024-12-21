using UnityEngine;

public class MysticalForestMushroom : MonoBehaviour
{
    public float minScale = 0.8f; // Minimum scale of the mushroom cap
    public float maxScale = 1.2f; // Maximum scale of the mushroom cap
    public float scaleSpeed = 1f; // Speed of scaling
    public float minRotation = -15f; // Minimum rotation angle
    public float maxRotation = 15f; // Maximum rotation angle
    public float rotationSpeed = 30f; // Speed of rotation
    public float flashInterval = 0.5f; // Interval for flashing effect
    public Color flashColor = Color.white; // Color to flash the mushroom cap

    private float currentScale;
    private float currentRotation;
    private bool scalingUp = true;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool isFlashing = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found!");
        }

        // Initialize scale and rotation
        currentScale = Random.Range(minScale, maxScale);
        currentRotation = Random.Range(minRotation, maxRotation);
        transform.localScale = new Vector3(currentScale, currentScale, 1f);
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

        // Start flashing effect
        InvokeRepeating("FlashMushroom", flashInterval, flashInterval);
    }

    void Update()
    {
        // Scale pulsing effect
        float scaleChange = scaleSpeed * Time.deltaTime;
        if (scalingUp)
        {
            currentScale += scaleChange;
            if (currentScale >= maxScale)
                scalingUp = false;
        }
        else
        {
            currentScale -= scaleChange;
            if (currentScale <= minScale)
                scalingUp = true;
        }

        // Rotation effect
        currentRotation += rotationSpeed * Time.deltaTime;
        if (currentRotation > 360f)
            currentRotation -= 360f;

        // Apply scale and rotation
        transform.localScale = new Vector3(currentScale, currentScale, 1f);
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
    }

    void FlashMushroom()
    {
        if (spriteRenderer != null)
        {
            if (isFlashing)
            {
                spriteRenderer.color = originalColor;
            }
            else
            {
                spriteRenderer.color = flashColor;
            }
            isFlashing = !isFlashing;
        }
    }
}
