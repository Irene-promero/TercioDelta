using UnityEngine;

public class Target3 : MonoBehaviour
{
    public GameObject destroyedVersion3;
    // Start is called before the first frame update
    public float health = 30f;



    public void TakeDamage3(float amout)
    {
        health -= amout;
        FindObjectOfType<AudioManager>().Play("Pop");
        if (health <= 0f)
        { Die3(); }
    }

    void Die3()
    {
        FindObjectOfType<AudioManager>().Play("MaderaBreaking");
        Instantiate(destroyedVersion3, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
