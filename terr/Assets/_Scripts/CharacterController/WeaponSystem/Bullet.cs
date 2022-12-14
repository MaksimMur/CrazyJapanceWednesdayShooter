using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float timeLive = 10;
    [SerializeField] private Effect bulletEffect;
    [SerializeField] private float damage = 10;
    private float _timeStart = 0;
    private void OnEnable()
    {
        _timeStart = Time.time;
    }

    private void Update()
    {
        if (_timeStart + timeLive < Time.time)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().GetDamge(damage);
        }
        Effect g = Instantiate<Effect>(bulletEffect);
        g.transform.position = this.transform.position;
        Destroy(this.gameObject);
    }

}
