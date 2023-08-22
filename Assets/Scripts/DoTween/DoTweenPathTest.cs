using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用DoTween的命名空间
using DG.Tweening;
using System.Linq;

public class DoTweenPathTest : MonoBehaviour
{
    //创建一个数组用来保存点的位置
    public Transform[] pointList;

    private void Start()
    {
        Vector3[] positions = pointList.Select(u => u.position).ToArray();

        #region 路径动画
        //.DOPath()
        //参数一 路径点数组
        //参数二 持续时间
        //参数三 绘制路径的方式，Linear为直线，CatmullRom为曲线
        //参数四 朝向模式，Full3D为任意方向，Ignore为忽略，TopDown2D为上下，Sidescroller2D为左右
        //参数五 曲线精度，高了会影响性能
        //参数六 路径颜色
        transform.DOPath(positions, 5, PathType.CatmullRom, PathMode.Full3D, 20, Color.blue);

        //.SetOptions()
        //参数一 路径是否闭合
        //参数二 锁定轴向，可以做路径在某条轴上的映射
        //参数三 锁定朝向
        transform.DOPath(positions, 5, PathType.CatmullRom).SetOptions(true, AxisConstraint.X, AxisConstraint.Y);

        //.SetLookAt()
        //看向某点
        transform.DOPath(positions, 5, PathType.CatmullRom).SetLookAt(Vector3.zero);
        //看向前方，受路径是否是闭环的影响
        //若路径闭合，当值为0时，看向路径前方；当值为0.5时，看向与路径方向垂直朝内；当值接近1时，看向路径后方；当值为1时会出毛病
        //若路径不闭合，在运动过程中会逐渐朝向路径方向
        //后两个参数表示方向向量，一般不做更改
        transform.DOPath(positions, 5, PathType.CatmullRom).SetLookAt(0);

        //Andy老师自己做了一套好用的插件，记得去要
        #endregion
    }

}
