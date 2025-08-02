using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

[RequireComponent(typeof(SpriteRenderer))]
public class Particle : MonoBehaviour
{
    [SerializeField] float initialVelocity = 5;
    [SerializeField] float spinMin = 2;
    [SerializeField] float spinMax = 10;
    [SerializeField] float timeToStop = 1;
    public Color color = Color.white;


    Rigidbody2D rb2d;

    /// <summary>
    /// sets the color of the particle
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(Color color)
    {
        this.color = color;

        GetComponent<SpriteRenderer>().color = color;
    }

    private void Start()
    {
        transform.rotation = Random.rotation;

        rb2d = GetComponent<Rigidbody2D>();

        Vector2 velocityDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        rb2d.linearVelocity = velocityDirection * (initialVelocity * Random.Range(.9f, 1.3f));

        // random spin force in random direction
        rb2d.angularVelocity = Random.Range(spinMin, spinMax) * Mathf.Sign(Random.Range(-1f, 1f));

        GetComponent<Animator>().Play("FadeOut");
    }

    private void FixedUpdate()
    {
        // decay velocity each second
        rb2d.linearVelocity -= rb2d.linearVelocity * (timeToStop * Time.deltaTime);
    }

    void DestroyParticle()
    {
        Destroy(this.gameObject);
    }
}
