using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private MissionTrackerPanel _missionTrackerPanel;
    [SerializeField] private GamePlayCanvas _canvas;

    public override void InstallBindings()
    {
        BindMissionTrackerPanel();
    }

    private void BindMissionTrackerPanel()
    {
        var spawnedTrackerPanel = Container.InstantiatePrefabForComponent<MissionTrackerPanel>(_missionTrackerPanel, _canvas.transform);
        Container.Bind<MissionTrackerPanel>().FromInstance(spawnedTrackerPanel).NonLazy();
    }

    
}
