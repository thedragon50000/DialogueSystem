using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PuzzleLevel", menuName = "Installers/PuzzleLevel")]
public class PuzzleLevel : ScriptableObjectInstaller<PuzzleLevel>
{
    
    public int iLevel;
    public override void InstallBindings()
    {
        var s = E_ZenjectID.Level.ToString();
        Container.BindInstance(iLevel).WithId(s).IfNotBound();
    }
}
