// Bullet3
using UnityEngine;

public class Bullet3 : BulletParent
{
    [Tooltip("Transform de la bala desde el cual salen las sub-balas.")]
    public Transform shotPoint

    [Tooltip("Transform de la bala desde el cual salen las sub-balas.")]
    public Transform shotPoint2;

    [Tooltip("Prefab de la sub-bala.")]
    public GameObject bulletShot;

    private float timerShoot = 0.5f;

    private float timerShoot2 = 0.1f;

    private int shots;

    private float timerShootNow;

    private float scaleSpeed = 15f;

    protected override void Start()
    {
        base.Start();
        timerShootNow = timerShoot;
    }

    protected override void BulletMovement()
    {
        base.BulletMovement();
        base.transform.position += base.transform.forward * bulletSpeed * Time.deltaTime;
        if (base.transform.localScale.x > 8f)
        {
            timerShootNow += Time.deltaTime;
            if (timerShootNow >= timerShoot + timerShoot2 * 2f && shots == 2)
            {
                GameObject gameObject = Object.Instantiate(bulletShot, shotPoint.position, shotPoint.rotation);
                Object.Instantiate(bulletShot, shotPoint2.position, shotPoint2.rotation);
                shots = 0;
                timerShootNow = 0f;
                gameObject.GetComponent<AudioSource>().Play();
            }
            else if (timerShootNow >= timerShoot + timerShoot2 && shots == 1)
            {
                GameObject gameObject2 = Object.Instantiate(bulletShot, shotPoint.position, shotPoint.rotation);
                Object.Instantiate(bulletShot, shotPoint2.position, shotPoint2.rotation);
                shots++;
                gameObject2.GetComponent<AudioSource>().Play();
            }
            if (timerShootNow >= timerShoot && shots == 0)
            {
                GameObject gameObject3 = Object.Instantiate(bulletShot, shotPoint.position, shotPoint.rotation);
                Object.Instantiate(bulletShot, shotPoint2.position, shotPoint2.rotation);
                shots++;
                gameObject3.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            base.transform.localScale += new Vector3(1f, 1f, 1f) * scaleSpeed * Time.deltaTime;
        }
    }
}
