using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject destroyedVersion;
    // Start is called before the first frame update
    public float health = 30f;



    public void TakeDamage (float amout)
    {  
        health -= amout;
        FindObjectOfType<AudioManager>().Play("PainAudio");
        if (health <= 0f)
        { Die(); }
    }

    void Die()
    {
        FindObjectOfType<AudioManager>().Play("DieEnemy");
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
