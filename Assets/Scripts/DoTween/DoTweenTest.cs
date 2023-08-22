using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用DoTween的命名空间
using DG.Tweening;

public class DoTweenTest : MonoBehaviour
{
    //创建一个unity自带的动画曲线
    public AnimationCurve _curve;
    //创建一个tweener
    public Tween _tweener;

    private void Start()
    {
        #region 动画队列常用方法
        //Sequence 创建Dotween动画队列
        Sequence quence = DOTween.Sequence();

        //Append 加入队列的Dotween将会按加入次序依次执行
        quence.Append(transform.DOMove(Vector3.one, 2));
        //等待时间
        quence.AppendInterval(3);
        quence.Append(transform.DOMove(-Vector3.one * 2, 2));

        //Insert 插入 Dotween 到队列的指定时间，并和持续时间内的已有 Dotween 同时执行，插入时间点可以超过队列时间长度
        //参数一 插入时间点
        //参数二 要插入的Dotween
        //类型相同的操作将会被“覆盖”掉
        quence.Insert(0, transform.DOMove(-Vector3.one, 2));
        //同过这种方式可以同时执行不同的动画
        quence.Insert(0, transform.DOScale(Vector3.one * 2, 2));
        //是否用了多线程？

        //Join
        quence.Append(transform.DOMove(Vector3.one, 2));
        quence.Join(transform.DOScale(Vector3.one * 2, 2)); //加入到上一个 Dotween 中，一起执行
        quence.AppendInterval(3);
        quence.Append(transform.DOMove(-Vector3.one * 2, 2));

        //Prepend 预添加，加入到 Append 之前，Prepend 执行顺序为从下到上，最后插入的先执行
        quence.PrependInterval(1);
        quence.Prepend(transform.DOMove(-Vector3.one, 2));

        //AppendCallback
        quence.Append(transform.DOMove(Vector3.one, 2));
        quence.AppendCallback(AppendCallBack);  //直接加一个回调函数
        quence.Append(transform.DOMove(-Vector3.one * 2, 2));

        //InsertCallback
        //参数一 插入的时间点
        //参数二 插入的回调函数
        quence.InsertCallback(5, InsertCallBack);

        //PrependCallback
        quence.PrependCallback(PrependCallBack);
        #endregion

        #region 常用设置 
        //给动画设置各种参数
        //方法一 链式编程，比较常用
        //transform.DOMove(Vector3.one, 2).Set.Set.Set
        //方法二 一行一行写
        //TweenParams para = new TweenParams();
        //para.SetLoops();
        //para.SetAutoKill();
        //transform.DOMove(Vector3.one, 2).SetAs(para);

        //.SetLoops()
        //参数一 正整数为循环次数，来回为两次，-1为无限循环
        //参数二 循环方式 Yoyo为来回循环，Restart为闪回，Incremental为增量
        transform.DOMove(Vector3.one, 2).SetLoops(-1, LoopType.Yoyo);

        //.SetAutoKill()
        //DoTween动画在生成后会进入缓存，SetAutoKill可以在执行完后自动杀掉这条动画
        transform.DOMove(Vector3.one, 2).SetLoops(4, LoopType.Yoyo).SetAutoKill(true);

        //.From()
        //不填参数表示从目标点运动回起始点，true 表示相对运动（增量）
        transform.DOMove(Vector3.one, 2).From(true);

        //.SetDelay()
        //延时执行
        transform.DOMove(Vector3.one, 2).SetDelay(3);

        //.SetSpeedBased()
        //改为以速度为基准（默认为时间），参数默认为 true
        //此时 DOMove() 参数二 代表的是速度，每秒运动的Unity的单位
        transform.DOMove(Vector3.one, 2).SetSpeedBased();

        //.SetId()
        //DoTween动画在生成后会进入缓存，SetId()可以为该条动画设置ID，方便复用
        transform.DOMove(Vector3.one, 2).SetId("ID");
        DOTween.Play("ID");

        //.SetRecyclable()
        //改条动画是否可回收，参数默认为 true
        transform.DOMove(Vector3.one, 2).SetRecyclable();

        //.SetRelative()
        //改为相对运动，参数默认为 true
        transform.DOMove(Vector3.one, 2).SetRelative();

        //.SetUpdate() 设置帧函数
        //参数一 Update的方式
        //Normal表示正常的Update，Fixed表示物理的Update，Late表示帧完成后的Update（同Unity自带的Update）
        //Manual为手动Update，需要再调用一个函数 DOTween.ManualUpdate(); 来执行（不常用）
        //参数二 是否不受Unity的TimeScale的影响（不常用）
        transform.DOMove(Vector3.one, 2).SetUpdate(UpdateType.Normal);
        #endregion

        #region 设置动画曲线
        //.SetEase()
        //设置运动速度曲线
        transform.DOMove(Vector3.one, 2).SetEase(Ease.Linear);  //匀速运动

        //有些枚举会有更多参数，例如 Flash
        //参数二 过冲或振幅
        //参数三 力度，取值 -1, 1 之间，负值时范围逐渐增大，正值时范围逐渐减小，零为完整均匀运动，详情查看官网
        transform.DOMove(Vector3.one, 2).SetEase(Ease.Flash, 3, 0.5f);

        //也可传入Unity自带的动画曲线，可以在Inspector窗口中设置该曲线
        transform.DOMove(Vector3.one, 2).SetEase(_curve);

        //手写曲线（不常用）
        transform.DOMove(Vector3.one, 2).SetEase(EaseFun);
        //没有最好用的，只有最合适的，或许在某种情况下就能用到那些不常用的奇奇怪怪的方法
        #endregion

        #region 常用回调函数
        //在动画完成时执行
        transform.DOMove(Vector3.one, 2).OnComplete(() => { Debug.Log("OnComplete"); });

        //在动画被杀死的时候执行
        transform.DOMove(Vector3.one, 2).OnKill(() => { Debug.Log("OnKill"); });

        //在动画播放的时候执行
        transform.DOMove(Vector3.one, 2).OnPlay(() => { Debug.Log("OnPlay"); });

        //在动画暂停时执行
        transform.DOMove(Vector3.one, 2).OnPause(() => { Debug.Log("OnPause"); });

        //在动画每个循环周期完成时调用一次
        transform.DOMove(Vector3.one, 2).OnStepComplete(() => { Debug.Log("OnStepComplete"); });

        //在DoTween的帧函数中执行
        transform.DOMove(Vector3.one, 2).OnUpdate(() => { Debug.Log("OnUpdate"); });

        //在动画重新播放的时候执行
        //restart
        //rewind
        //doflip
        //dobackwards
        transform.DOMove(Vector3.one, 2).OnRewind(() => { Debug.Log("OnRewind"); });
        #endregion

        #region 常用控制函数
        //暂停动画
        transform.DOPause();

        //播放动画
        transform.DOPlay();

        //动画重播
        transform.DORestart();

        //动画倒播，瞬间回到起始点    教程第31集控制函数（一）
        transform.DORewind();

        //平滑倒播
        transform.DOSmoothRewind();

        //杀死动画
        transform.DOKill();

        //翻转补间动画，起始点目标点位置互换
        transform.DOFlip();

        //改为反向播放，起始点变为目标点
        transform.DOPlayBackwards();

        //改为正向播放，通常在 DOPlayBackwards() 之后调用
        transform.DOPlayForward();

        //切换播放和暂停
        //如果现在是暂停状态就播放，如果是播放状态就暂停
        //在特定环境下调佣这个方法是非常不错的选择，不过播放暂停通常还是用 DOPlay() DOPause()
        transform.DOTogglePause();

        //Goto 直接跳转到时间点
        transform.DOGoto(1.5f);
        #endregion

        #region 获取动画数据 类方法
        //获取所有正在暂停的动画
        var pausingList = DOTween.PausedTweens();

        //获取所有正在播放的动画
        var playingList = DOTween.PlayingTweens();

        //收集所有名为 ID 为 "ID"的动画
        //参数二 是否正在播放
        var listById = DOTween.TweensById("ID",true);

        //收集目标对象的动画 比较实用
        //参数一 动画的对象
        //参数二 是否正在播放
        var listByTarget = DOTween.TweensByTarget(transform, true);

        //查找正在执行的动画ID
        var tweeningList = DOTween.IsTweening("ID");

        //获取当前所有正在播放的动画，包括延时状态中的动画
        var totalPlayingList=DOTween.TotalPlayingTweens();
        #endregion

        #region 获取动画数据 实例方法
        var tweener = transform.DOMove(Vector3.one, 2);

        //fullPosition
        //获取动画的时间点，可以设置当前时间
        //执行到1秒，fullPosition值为1，执行到2秒，fullPosition值为2
        var fullPosition = tweener.fullPosition;
        tweener.fullPosition = 0.5f;

        //.CompletedLoops()
        //获取完成的循环次数，若获取的值为0，则表示当前tweener已被杀死
        tweener.SetLoops(3);
        Debug.Log(tweener.CompletedLoops());

        //.Delay()
        //获取当前tweener的延迟时间
        Debug.Log(tweener.Delay());

        //.Duration()
        //获取当前tweener的持续时间，参数为true时表示包含循环的时间，若为无限循环，则返回无限
        Debug.Log(tweener.Duration());

        //.Elapsed()
        //获取当前tweener的已经播放了的时间，参数为true时表示包含循环的时间，若为无限循环，则返回无限
        Debug.Log(tweener.Elapsed());

        //.ElapsedDirectionalPercentage()
        //获取当前tweener的播放方向上的进度的百分比
        //正常的范围是0~1，获取的循环类型为Yoyo时，返回阶段的值为1~0
        Debug.Log(tweener.ElapsedDirectionalPercentage());

        //.ElapsedPercentage()
        //获取当前tweener的总播放进度的百分比，参数为true时表示包含循环的时间，若为无限循环，则返回无限
        Debug.Log(tweener.ElapsedPercentage());

        //.Loops()
        //获取当前tweener的循环次数
        Debug.Log(tweener.Loops());

        //.IsActive()
        //当前tweener是否在活动
        Debug.Log(tweener.IsActive());

        //.IsBackwards()
        //当前tweener是否是反向的
        Debug.Log(tweener.IsBackwards());

        //.IsComplete()
        //当前tweener是否已播完
        Debug.Log(tweener.IsComplete());

        //.IsInitialized()
        //当前tweener是否初始化
        Debug.Log(tweener.IsInitialized());

        //.IsPlaying()
        //当前tweener是否正在播放
        Debug.Log(tweener.IsPlaying());
        #endregion

        #region 和协程相关的方法
        StartCoroutine(Wait());
        //这种方法执行的效果，和回调函数差不多的
        #endregion
    }

    private IEnumerator Wait()
    {
        //.WaitForCompletion()
        //等待这个动画完成之后，才会执行后面的逻辑
        yield return _tweener.WaitForCompletion();

        //.WaitForElapsedLoops()
        //等待这个动画执行完N次循环后，才会执行后面的逻辑
        //参数为循环次数
        yield return _tweener.WaitForElapsedLoops(2);

        //.WaitForKill()
        //等待这个动画被杀死后，才会执行后面的逻辑
        yield return _tweener.WaitForKill();

        //.WaitForPosition() 在DoTween中 Position 一般指的是动画的时间
        //等待这个动画执行了N秒之后，才会执行后面的逻辑
        yield return _tweener.WaitForPosition(2);

        //.WaitForRewind()
        //等待这个动画翻转后，才会执行后面的逻辑
        yield return _tweener.WaitForRewind();

        //.WaitForStart()
        //等待这个动画开始后，才会执行后面的逻辑
        //当这个动画执行的时候，会先调用Start，然后Play
        yield return _tweener.WaitForStart();
    }

    /// <summary>
    /// 手写运动曲线
    /// </summary>
    /// <param name="time">动画中当前时间点</param>
    /// <param name="duration">持续时间</param>
    /// <param name="overshootOrAmplitude">过冲或振幅</param>
    /// <param name="period">力度</param>
    /// <returns></returns>
    private float EaseFun(float time, float duration, float overshootOrAmplitude, float period)
    {
        //返回值范围正常是 0, 1，0表示起点，1表示终点
        //返回匀速运动
        return time / duration;
    }

    private void InsertCallBack()
    {
        Debug.Log("InsertCallBack");
    }

    private void AppendCallBack()
    {
        Debug.Log("AppendCallBack");
    }

    private void PrependCallBack()
    {
        Debug.Log("PrependCallBack");
    }
}