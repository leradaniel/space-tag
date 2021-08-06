// Bullet1
using UnityEngine;

public class Bullet1 : BulletParent
{
    [Tooltip("No tocar.")]
    public bool emitSound

    protected override void BulletMovement()
    {
        base.BulletMovement();
        if (moveWorldSpace)
        {
            base.transform.position += new Vector3(bulletSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            base.transform.position += base.transform.forward * bulletSpeed * Time.deltaTime;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Enemy")
        {
            if ((bool)GetComponent<Renderer>())
            {
                GetComponent<Renderer>().enabled = false;
            }
            else
            {
                GetComponentInChildren<Renderer>().enabled = false;
            }
            GetComponent<Collider>().enabled = false;
            try
            {
                Object.Destroy(base.gameObject, GetComponent<AudioSource>().clip.length);
            }
            catch
            {
                Object.Destroy(base.gameObject);
            }
        }
    }
}
