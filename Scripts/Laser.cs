using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    public GameObject destroyAnim;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
        
        if(transform.position.y > 6) Destroy(this.gameObject);
    }

    public void Death(){

        Instantiate(destroyAnim, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
