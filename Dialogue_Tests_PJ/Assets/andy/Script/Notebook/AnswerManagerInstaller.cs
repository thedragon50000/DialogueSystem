using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AnswerManagerInstaller : MonoInstaller
{
    private Button[] btn = new Button[3];
    public StringManager_sc _stringManagerSc;

    public override void InstallBindings()
    {
        // Container.Bind<StringManager_sc>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindInstance(_stringManagerSc).AsSingle();
        {
            for (int i = 0; i < 3; i++)
            {
                int temp = i;
                btn[i].onClick.AddListener(() => { EnterRoom(i); });
            }
        }
    }

    void EnterRoom(int room){}
}
