using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassRefraction : MonoBehaviour
{
    [SerializeField]
    private float glassRefractionIndex = 1.52f;

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
        Vector2 initialXZ;
        Vector2 initialYZ;

        Vector3 glassBulletVelocity;

        Vector3 contactPoint;
        Vector3 glassNormal;

        float dotProd;
        float thetaIn;
        float phiIn;
        float thetaOut;
        float phiOut;

        if (other.tag == "Bullet")
        {
            initialBulletVelocity = other.GetComponent<Rigidbody>().velocity;
            initialXZ = new Vector2(initialBulletVelocity.x, initialBulletVelocity.z);
            initialYZ = new Vector2(initialBulletVelocity.y, initialBulletVelocity.z);

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
            dotProd = -initialBulletVelocity.z * glassNormal.z;
            thetaIn = Mathf.Acos((dotProd) / (initialXZ.magnitude));
            phiIn = Mathf.Acos((dotProd) / (initialYZ.magnitude));
            Debug.Log("Theta In: " + thetaIn * Mathf.Rad2Deg);
            Debug.Log("Phi In: " + phiIn * Mathf.Rad2Deg);

            thetaOut = Mathf.Acos(Mathf.Sin(thetaIn / glassRefractionIndex));
            phiOut = Mathf.Acos(Mathf.Sin(phiIn / glassRefractionIndex));




            //Take Velocity
            //Calculate Theta
        }
    }

   
}
