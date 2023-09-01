using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Basic properties
    public Vector2 velocity;
    public float speed;
    public float rotation;

    // Lifetime and damage
    public float lifetime = 5f;
    public int damage = 1;

    // Bullet behavior customization
    public bool isHoming = false;
    public Transform homingTarget;
    public float homingStrength = 1f;

    public bool hasAcceleration = false;
    public float accelerationAmount = 0.1f;

    public bool hasWaveMotion = false;
    public float waveFrequency = 1f;
    public float waveMagnitude = 1f;

    private Vector2 currentDirection;
    private SpriteRenderer spriteRenderer; // For color variation

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        currentDirection = transform.up;

        // Color variation based on behavior
        if (isHoming) spriteRenderer.color = Color.red;
        else if (hasWaveMotion) spriteRenderer.color = Color.blue;
        else if (hasAcceleration) spriteRenderer.color = Color.green;
    }

    void Update()
    {
        // Homing behavior
        if (isHoming && homingTarget != null)
        {
            Vector2 directionToTarget = (homingTarget.position - transform.position).normalized;
            currentDirection = Vector2.Lerp(currentDirection, directionToTarget, homingStrength * Time.deltaTime);
        }

        // Wave motion behavior
        if (hasWaveMotion)
        {
            currentDirection.x += Mathf.Sin(Time.time * waveFrequency) * waveMagnitude;
        }

        // Apply velocity and speed
        transform.Translate(currentDirection * speed * Time.deltaTime);

        // Acceleration behavior
        if (hasAcceleration)
        {
            speed += accelerationAmount * Time.deltaTime;
        }

        // Boundary check
        if (!Camera.main.orthographicSize.Equals(0) && !Camera.main.pixelWidth.Equals(0))
        {
            Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            if (Mathf.Abs(transform.position.x) > screenBounds.x || Mathf.Abs(transform.position.y) > screenBounds.y)
            {
                DeactivateBullet();
            }
        }
    }

    // Deactivate bullet and return to pool (assuming you have a pooling system)
    void DeactivateBullet()
    {
        gameObject.SetActive(false);
        // If using a pooling system, add the bullet back to the pool here
    }
}