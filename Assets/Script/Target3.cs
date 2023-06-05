using UnityEngine;

public class Target3 : MonoBehaviour
{
    public GameObject destroyedVersion;
    // Start is called before the first frame update
    public float health = 30f;



    public void TakeDamage(float amout)
    {
        health -= amout;
        FindObjectOfType<AudioManager>().Play("Pop");
        if (health <= 0f)
        { Die(); }
    }

    void Die()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
