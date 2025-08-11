using UnityEditor;
using UnityEngine;
using Zenject;

public class OnSceneInjector : MonoInstaller
{
    [SerializeField] private HoleHandler _hole;

    public override void InstallBindings()
    {
        HoleHandler initHole = Instantiate(_hole);
        Container.Bind<Level>().FromInstance(initHole.GetComponentInParent<Level>()).AsSingle();
    }
}
