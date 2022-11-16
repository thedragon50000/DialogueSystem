using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UniRx.Triggers;
using UnityEngine.PlayerLoop;

public class EventTest_sc : MonoBehaviour
{
    /*
    代码解析
        EventHandler是C#系统内置的一个事件处理类，指定一个参数类型；
    参数类型由系统的EventArgs来派生；
    FromEvent需要指定两个包装类型，第一个是代理，第二是参数；
    一般的写法都是按照EventHandler和EventArgs的方式；
    EventMethod，东EventHandler过来的数据经过什么来派生，
    由于EventHandler的构造方式指定是object和EventArgs，
    返回的是一个指针函数，在函数里面将需要的数据返回到流那边去处理；
    AddHandler和RemoveHandler分别是对原始的回调进行订阅和取消订阅，通过流的Subscribe和Disposable方法来进行订阅和移除
    */

    /*EventHandler 就是一個delegate，可以省下宣告的步驟直接實例化
    public delegate void EventHandler<IntEventArgs>();*/
    //↑如果是自定義的delegate，就少不了宣告這步↑
    public event EventHandler<IntEventArgs> mEventArgCallback;

    #region 自己宣告delegate的情況下

    public delegate void Sample<T1, T2>(); //1.宣告這個委派要接收幾個args

    // public Sample<int> delegateSampleX;  //2.實例化要有相應數量的args
    public Sample<int, string> delegateSample;
    /*
    
     */

    #endregion

    public class IntEventArgs : EventArgs
    {
        public int Property { get; set; }
    }

    private void Start()
    {
        void AddHandler(EventHandler<IntEventArgs> handler)
        {
            mEventArgCallback += handler;
            print("AddHandler");
        }

        void RemoveAction(EventHandler<IntEventArgs> handler)
        {
            mEventArgCallback -= handler;
            print("RemoveHandler");
        }

        mEventArgCallback += B;

        #region 流程

        // var fromEvent =
        // Observable.FromEvent<EventHandler<IntEventArgs>, IntEventArgs>(EventMethod, AddHandler, RemoveAction);
        var fromEvent =
            Observable.FromEvent<EventHandler<IntEventArgs>, IntEventArgs>(EventMethod, AddHandler, RemoveAction);
        var disposable = fromEvent.Subscribe(Next, Error, Compelete);
        disposable.AddTo(gameObject);
        
        #endregion
        mEventArgCallback += A;

        IntEventArgs args = new IntEventArgs();
        args.Property = 100;
        // mEventArgCallback(this, args);

        Observable.EveryUpdate().Subscribe(_ => mEventArgCallback(this, args));
    }

    private void A(object sender, IntEventArgs e)
    {
        print("A");
    }

    private void B(object sender, IntEventArgs e)
    {
        print("B");
    }

    public EventHandler<IntEventArgs> EventMethod(Action<IntEventArgs> action) //Action<IntEventArgs>(IntEventArgs e)
    {
        void Handler(object sender, IntEventArgs e)
        {
            action(e);
        }

        return Handler;
    }

    void A(IntEventArgs args)
    {
        print("A:" + args);
    }

    #region Voids

    void Next(IntEventArgs args)
    {
        print("回調事件觸發:" + args);
    }

    void Error(Exception ex)
    {
        Debug.LogException(ex);
    }

    void Compelete()
    {
        print("Compelete");
    }

    #endregion
}
/*
      sender参数用于传递指向事件源对象的引用。简单来讲就是当前的对象。
      例如button的点击事件，那么这个sender就代表这个button自己。
      private void btn1_Click(object sender, EventArgs e)
      {
       //获取当前点击的Button
       Button thisClickedButton = sender as Button;
       }
       EventArgs e：表示事件数据的类的基类
       e参数是是EventArgs类型。简单来理解就是记录事件传递过来的额外信息。*/