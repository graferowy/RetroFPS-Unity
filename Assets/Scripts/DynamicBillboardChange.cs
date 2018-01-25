using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBillboardChange : MonoBehaviour {

    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    string[] animStates = new string[4] { "Forward", "Backward", "Left", "Right" };
    [SerializeField]
    bool isAnimated;

    Animator anim;
    SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GetAngle();
    }

    void GetAngle()
    {
        Vector3 playerDir = Camera.main.transform.forward;
        playerDir.y = 0;
        Vector3 enemyDir = transform.Find("Vision").forward;
        enemyDir.y = 0;

        float dotProduct = Vector3.Dot(playerDir, enemyDir);

        if (dotProduct < -0.5f && dotProduct >= -1.0f)
            ChangeSprite(0);
        else if (dotProduct > 0.5f && dotProduct <= 1.0f)
            ChangeSprite(1);
        else
        {
            Vector3 playerRight = Camera.main.transform.right;
            playerRight.y = 0;
            dotProduct = Vector3.Dot(playerRight, enemyDir);
            if (dotProduct >= 0)
                ChangeSprite(2);
            else
                ChangeSprite(3);
        }
    }

    void ChangeSprite(int index)
    {
        if (isAnimated)
            anim.Play(animStates[index]);
        else
            sr.sprite = sprites[index];
    }
}
