using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienProjectileController : MonoBehaviour
{

    public float speed;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if(other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
