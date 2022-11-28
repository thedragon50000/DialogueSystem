using Doublsb.Dialog;
using UnityEngine;
using Zenject;

public class TesterInstaller : MonoInstaller
{
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Without_MonoBehavier>().AsSingle();
        Container.Bind<Tester_sc>().FromNewComponentOnNewGameObject().AsSingle();
        
    }
    
}