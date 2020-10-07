using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassRefraction : MonoBehaviour
{
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 initialBulletVelocity;
        Vector3 glassBulletVelocity;
        Vector3 contactPoint;
        Vector3 glassNormal;
        float thetaIn;
        float phiIn;
        float thetaOut;
        float phiOut;

        if (other.tag == "Bullet")
        {
            initialBulletVelocity = other.GetComponent<Rigidbody>().velocity;
            contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
            if (contactPoint.z < 0)
            {
                glassNormal = new Vector3(0, 0, -1);
            }
            else if (contactPoint.z > 0)
            {
                glassNormal = new Vector3(0, 0, 1);
            }
            else
            {
                glassNormal = Vector3.zero;
                Debug.LogWarning("Bullet Contact Point was not detected on one side or the other and thus the contact point normal could not be determined");
            }

            thetaIn = Mathf.Rad2Deg * Mathf.Acos((initialBulletVelocity.z * glassNormal.z) / (initialBulletVelocity.magnitude));
            Debug.Log(thetaIn);


            //Take Velocity
            //Calculate Theta
        }
    }

   
}
