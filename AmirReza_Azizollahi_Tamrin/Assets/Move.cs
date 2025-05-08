using UnityEngine;

public class Move : MonoBehaviour
{
    public Rigidbody2D Rb;
    public Animator Anim;
    public SpriteRenderer Sr;
    public GameObject Ash;

    public float Force;
    public float Speed;
    private void Update()
    {
        float movementX = Input.GetAxisRaw("Horizontal");
        Rb.velocity = new Vector2(movementX * Speed, Rb.velocity.y);
        
        if (movementX != 0)
        {
            Anim.SetBool("isMoving", true);
        }
        else
        {
            Anim.SetBool("isMoving", false);
        }

        if (movementX < 0)
        {
            Sr.flipX = true;
        }
        else if (movementX > 0)
        {
            Sr.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject ash = Instantiate(Ash, transform.position, Quaternion.identity);
            Vector2 lookDirection = Sr.flipX ? Vector2.left : Vector2.right;
            ash.GetComponent<Rigidbody2D>().AddForce(lookDirection * Force);
            Destroy(ash, 3);
        }
    }
}