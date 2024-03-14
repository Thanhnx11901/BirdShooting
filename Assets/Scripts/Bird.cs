using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float xSpeed;
    public float minYspeed;
    public float maxYspeed;
    public GameObject deathVFX;

    private bool m_moveLeftOnStart;
    private bool m_isDead;


    Rigidbody2D m_rb;
    // Start is called before the first frame update
    void Start()
    {
        GameGUIManager.Ins.UpdateKilledCounting(GameManager.Ins.BirdKilled);
        m_rb = GetComponent<Rigidbody2D>();
        RandomMovingDirection();
    }

    // Update is called once per frame
    void Update()
    {
        m_rb.velocity = m_moveLeftOnStart ?
            new Vector2(-xSpeed, Random.Range(minYspeed, maxYspeed)):
            new Vector2(xSpeed, Random.Range(minYspeed, maxYspeed));
        Flip();
    }
    public void RandomMovingDirection()
    {
        m_moveLeftOnStart = transform.position.x > 0 ? true : false;
    }
    public void Flip()
    {
        if (m_moveLeftOnStart)
        {
            if (transform.localScale.x < 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            if (transform.localScale.x > 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }
    public void Die()
    {
        m_isDead = true;
        Destroy(gameObject);
        if (deathVFX)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
        }
        GameGUIManager.Ins.UpdateKilledCounting(GameManager.Ins.BirdKilled);
    }
}
