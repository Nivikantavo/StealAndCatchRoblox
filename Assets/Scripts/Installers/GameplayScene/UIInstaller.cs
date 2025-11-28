using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [SerializeField] private MissionTrackerPanel _missionTrackerPanel;
    [SerializeField] private Canvas _canvas;

    public override void InstallBindings()
    {
        BindMissionTrackerPanel();
    }

    private void BindMissionTrackerPanel()
    {
        var spawnedTrackerPanel = Container.Bind<MissionTrackerPanel>().FromComponentInNewPrefab(_missionTrackerPanel).NonLazy();
        //var spawnedTrackerPanel = Container.InstantiatePrefabForComponent<MissionTrackerPanel>(_missionTrackerPanel, _canvas.transform);
    }
}
