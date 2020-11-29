using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouletShot : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 30;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Debug.Log("work");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
