using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PuzzleLevel", menuName = "Installers/PuzzleLevel")]
public class PuzzleLevel : ScriptableObjectInstaller<PuzzleLevel>
{
    public int iLevel;
    public override void InstallBindings()
    {
        Container.BindInstance(iLevel).AsSingle().NonLazy().IfNotBound();
    }
}