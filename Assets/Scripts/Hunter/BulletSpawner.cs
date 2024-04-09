using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Hunter
{
    public class BulletSpawner : MonoBehaviour
    {
        public GameObject bullet;
        public Transform spawn;
        
        void Update()
        {
            Quaternion bulletRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, -90f);

            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, spawn.position, bulletRotation.normalized);
            }
        }
    }
}