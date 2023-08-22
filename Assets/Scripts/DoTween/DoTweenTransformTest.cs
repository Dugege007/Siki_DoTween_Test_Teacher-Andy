using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用DoTween的命名空间
using DG.Tweening;

public class DoTweenTransformTest : MonoBehaviour
{
    private void Start()
    {
        #region Transform相关
        //position
        //第一个参数是目标位置，第二个参数是移动时间
        //移动到世界坐标
        transform.DOMove(Vector3.one, 2);
        //移动单轴坐标
        transform.DOMoveX(1, 2);
        //移动到相对坐标
        transform.DOLocalMove(Vector3.one, 2);

        //rotation
        //第一个参数是目标角度，第二个参数是旋转时间
        //以欧拉角为目标旋转
        //以世界坐标旋转
        transform.DORotate(new Vector3(45, 45, 45), 2);
        //以相对坐标旋转
        transform.DOLocalRotate(new Vector3(45, 45, 45), 2);
        //以四元数为目标旋转，较少使用
        transform.DORotateQuaternion(new Quaternion(0.1f, 0.1f, 0.1f, 0.1f), 2);
        //看向目标位置
        transform.DOLookAt(Vector3.one, 2);

        //scale
        //第一个参数是目标缩放比例，第二个参数是缩放时间
        transform.DOScale(Vector3.one * 2, 2);


        //punch
        //加一个带弹性的动画
        //第一个参数表示运动的方向和大小（相当于施加一个力）
        //第二个参数是持续时间
        //第三个参数是弹的频率（每秒n次）
        //第四个参数表示弹回来的比例，取值范围 0~1，0表示不弹
        transform.DOPunchPosition(new Vector3(0, 1, 0), 2, 2, 0.1f);
        transform.DOPunchRotation(new Vector3(45, 45, 45), 2, 2, 0.1f);
        transform.DOPunchScale(Vector3.one * 0.5f, 2, 2, 0.1f);

        //shake
        //相机震动，此方法很好用
        //参数一 持续时间
        //参数二 震动的力大小
        //参数三 震动频率
        //参数四 随机方向范围，设为0时只在一个轴向上震动，但是这个轴方向是随机的
        //参数五 是否淡入淡出
        transform.DOShakePosition(2, Vector3.one, 5, 90, false);

        //blend
        //position
        //运动混合模式，可以运动混合到一起
        transform.DOBlendableMoveBy(Vector3.one, 2);
        transform.DOBlendableMoveBy(-Vector3.one * 2, 2);
        //两次的运动结果相加为最后的目标位置

        //rotation
        //道理同上
        transform.DOBlendableRotateBy(new Vector3(45, 45, 45), 2);
        #endregion
    }
}
