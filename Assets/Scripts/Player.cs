using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float m_curFireRate;
    private bool m_isShooted;
    public GameObject viewFinder;
    private void Awake()
    {
        m_curFireRate = Const.FIRE_RATE;
    }
    void Update()
    {
        // shot bird
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !m_isShooted)
        {
            if (GameManager.Ins.m_isGameover == false)
            {
                Shot(mousePos);
            }
            
        }
        if (m_isShooted)
        {
            m_curFireRate -= Time.deltaTime;
            if(m_curFireRate <= 0)
            {
                m_isShooted = false;
                m_curFireRate = Const.FIRE_RATE;
            }
            GameGUIManager.Ins.UpdateFireRate(m_curFireRate / Const.FIRE_RATE);
        }
        // pos viewfinder
        viewFinder.transform.position = new Vector3(mousePos.x,mousePos.y,0f);
    }
    public void Shot(Vector3 mousePos) {
        m_isShooted = true;
        Vector3 shootDir = Camera.main.transform.position - mousePos;
        shootDir.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos,shootDir);
        if (hits.Length > 0 && hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider && Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos) <= 0.4)
                {
                    Bird bird = hit.collider.GetComponent<Bird>();
                    Debug.Log(hit.collider.name);
                    if (bird)
                    {
                        bird.Die();
                        GameManager.Ins.BirdKilled++;
                    }
                }
            }
        }
        AudioCtl.Ins.PlaySound(AudioCtl.Ins.shooting);
        CineController.Ins.ShakeTrigger();
    }

}
