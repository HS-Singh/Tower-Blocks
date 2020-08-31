using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisioncut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        CubeCut.Cut_x(other.transform,transform.position );
    }
}
