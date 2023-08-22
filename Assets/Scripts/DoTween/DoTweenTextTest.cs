using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//引用DoTween的命名空间
using DG.Tweening;

public class DoTweenTextTest : MonoBehaviour
{
    private void Start()
    {
        Text text = GetComponent<Text>();

        //Color
        //参数一 目标颜色
        //参数二 持续时间
        text.DOColor(Color.red, 2);

        //Fade
        //控制alpha值
        //参数一 目标alpha值
        //参数二 持续时间
        text.DOFade(0.5f, 2);

        //Blend
        text.DOBlendableColor(Color.red, 2);

        //打字机式显示Text
        //参数一 要显示的字符串
        //参数二 持续时间
        text.DOText("Hello World!", 2);
        // .SetEase(Ease.Linear) 匀速显示
        text.DOText("Hello World!", 2).SetEase(Ease.Linear);
    }
}
