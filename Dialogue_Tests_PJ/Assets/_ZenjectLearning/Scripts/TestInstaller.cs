using System;
using UniRx;
using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<UserJoinedSignal>();
        Container.Bind<Greeter>().AsSingle();
        Container.BindSignal<UserJoinedSignal>().ToMethod<Greeter>(x => x.SayHello)
            .FromResolve();
        Container.BindInterfacesAndSelfTo<GameInitailizer>().AsSingle();
    }
}

public class UserJoinedSignal
{
    public string _strUserName;
}

public class GameInitailizer : IInitializable
{
    readonly SignalBus _signalBus;

    public GameInitailizer(SignalBus s)
    {
        _signalBus = s;
    }

    public void Initialize()
    {
            _signalBus.Fire(new UserJoinedSignal {_strUserName = "Fork"});
    }
}

public class Greeter
{
    public void SayHello(UserJoinedSignal u)
    {
        Debug.Log("_strUserName = " + u._strUserName);
    }
}