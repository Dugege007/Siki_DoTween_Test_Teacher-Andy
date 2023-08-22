using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用DoTween的命名空间
using DG.Tweening;

public class DoTweenMaterialTest : MonoBehaviour
{
    //创建一个渐变色管理器
    public Gradient _gradient;

    private void Start()
    {
        #region Material相关
        //material
        //color
        //获取材质球
        Material material = GetComponent<MeshRenderer>().material;
        //用material自带的方法改变颜色
        material.SetColor("_color", Color.red);
        //渐变到红色，默认改变的是shader中Main Color的值
        material.DOColor(Color.red, 2);
        //可通过增加color的名字，调整特定的color
        material.DOColor(Color.green, "MyColor", 2);
        //渐变到透明
        material.DOColor(Color.clear, 2);

        //alpha
        //渐变到alpha值为0
        material.DOFade(0, 2);
        //将shader中特定的color渐变到alpha值为0
        material.DOFade(0, "MyColor", 2);

        //gradient
        //渐变为目标gradient的颜色，不会应用gradient的alpha值，可以在Inspector窗口中设置
        material.DOGradientColor(_gradient, 2);

        //offset
        //贴图坐标渐变动画
        material.DOOffset(new Vector2(0.5f, 0), 2);

        //setvector
        //可以改变shader中color属性的值
        material.DOVector(Color.clear, "MyColor", 2);

        //如果同时执行两个DOColor，那么最下面的效果会将上面的覆盖掉，同transform
        //此时可以用blend
        material.DOBlendableColor(Color.red, 2);
        material.DOBlendableColor(Color.green, 2);
        #endregion
    }
}
