using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float minSize = 0.5f;
    float maxSize = 2f;
    Rigidbody2D rb;

    float minSpeed = 50f;
    float maxSpeed = 100f;

    float maxSpinSpeed = 10f;

    float maxVelocity = 5f;

    [SerializeField] private GameObject bounceWallPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float random = Random.Range(minSize, maxSize);
        Vector3 randomScale = new Vector3(random, random, 0);
        transform.localScale = randomScale;


        rb = GetComponent<Rigidbody2D>();

        float speed = Random.Range(minSpeed, maxSpeed) / random;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        rb.AddForce(randomDirection * speed);   

        float spinSpeed = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(spinSpeed);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxVelocity;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector2 contactPoint = collision.contacts[0].point;

        float colisionSpeed = collision.relativeVelocity.magnitude;

        float objectSize = transform.localScale.x;

        float impactIntensity = colisionSpeed * objectSize * 0.1f;


        GameObject bounceWall =  Instantiate(bounceWallPrefab, contactPoint, Quaternion.identity);

        BounceEffectController bounceEffectController = bounceWall.GetComponent<BounceEffectController>();
        if (bounceEffectController != null ) { 
            bounceEffectController.Initialize(impactIntensity);
        }

        Destroy(bounceWall, 0.5f);

    }
}
