using UnityEngine;

public class Motor : MonoBehaviour
{
    public Rigidbody2D Rb;
    public float Speed;
    public AudioSource AudioSource;

    private void Update()
    {
        float xMovement = Input.GetAxis("Horizontal");
        Rb.velocity = new Vector2(xMovement * Speed, Rb.velocity.y);
        AudioSource.pitch = 0.8f + xMovement;
    }
}