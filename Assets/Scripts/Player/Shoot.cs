using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bloodPrefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootRay();
    }

    void ShootRay()
    {
        //left click to fire
        //cast a ray from the center of the screen
        //debug the name of the object you hit
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);    //center of game view
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;
            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 0 | 1 << 7)) //layermask, can shoot Default and Enemy layers, ignore others
            {
                Debug.Log("Hit: " + hitInfo.collider.name);
                Health health = hitInfo.collider.GetComponent<Health>();
                if (health != null)
                {
                    Instantiate(_bloodPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    //instantiate blood splat effect
                    //at position of raycast hit
                    //rotate towards hit normal (surface normal)
                    health.Damage(10);
                }
            }
        }
    }

}
