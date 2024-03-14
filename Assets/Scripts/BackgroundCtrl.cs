using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCtrl : Singleton<BackgroundCtrl>
{
    public Sprite[] sprites;
    public SpriteRenderer bgImg;
    // ko luu dữ liệu khi sang scenes mới
    public override void Awake()
    {
        MakeSingleton(false);
    }
    public override void Start()
    {
        ChangeSprite();
    }
    public void ChangeSprite()
    {
        if(bgImg != null && sprites != null && sprites.Length > 0)
        {
            int randomIdx = Random.Range(0, sprites.Length);
            if (sprites[randomIdx] != null)
            {
                bgImg.sprite = sprites[randomIdx];
            }
        }
    }



}
