using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用DoTween的命名空间
using DG.Tweening;

public class DoTweenCameraTest : MonoBehaviour
{
    private void Start()
    {
        #region Camera相关
        Camera camera = GetComponent<Camera>();

        //Aspect 改变宽高比
        //参数一 宽高比
        //参数二 改变过程的时间
        camera.DOAspect(0.5f, 2);

        //Color 改变背景颜色
        camera.DOColor(Color.red, 2);

        //近切面、远切面渐变
        camera.DONearClipPlane(10, 2);
        camera.DOFarClipPlane(50, 2);

        //FieldOfView FOV 透视视野大小渐变
        camera.DOFieldOfView(10, 2);

        //OrthoSize 正交视野大小渐变
        camera.DOOrthoSize(10, 2);

        //多相机同屏显示
        //PixelRect 渐变在屏幕上显示多少像素，相当于将Game画面视口大小缩放
        //参数一 前两位和后两位分别表示屏幕坐标，左下是 (0, 0) 点
        camera.DOPixelRect(new Rect(0, 0, 500, 500), 2);

        //多相机同屏显示
        //Rect 对应Camera中的viewportRect，改变比例的方式，比 PixelRect 更常用
        camera.DORect(new Rect(0, 0, 0.5f, 0.5f), 2);

        //Shake
        //位置震动，参数直接填时间
        camera.DOShakePosition(2);
        //参数一 震动时间
        //参数二 振动力度
        //参数三 震动频率
        //参数四 随机角度
        //参数五 是否淡入淡出
        camera.DOShakePosition(2, 5, 20, 45, true);
        #endregion
    }
}
