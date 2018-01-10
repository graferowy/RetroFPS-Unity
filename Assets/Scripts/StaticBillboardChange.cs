using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticBillboardChange : MonoBehaviour {

    [SerializeField]
    Sprite[] sprites;
    [SerializeField]
    AnimationClip[] anims;
    [SerializeField]
    bool isAnimated;

    Animator anim;
    SpriteRenderer sr;

    float angle;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        angle = GetAngle();

        if (angle >= 225.0f && angle <= 315.0f)
            ChangeSprite(0);
        else if (angle < 225.0f && angle > 135.0f)
            ChangeSprite(1);
        else if (angle <= 135.0f && angle >= 45.0f)
            ChangeSprite(2);
        else if ((angle < 45.0f && angle > 0.0f) || (angle > 315.0f && angle < 360.0f))
            ChangeSprite(3);
    }

    void ChangeSprite(int index)
    {
        if (isAnimated)
            anim.Play(anims[index].name);
        else
            sr.sprite = sprites[index];
    }

    float GetAngle()
    {
        Vector3 direction = Camera.main.transform.position - this.transform.position;
        float angleTemp = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;
        angleTemp += 180.0f;
        return angleTemp;
    }

}
