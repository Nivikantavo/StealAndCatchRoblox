using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GamePlayCanvas : MonoBehaviour
{
    [SerializeField] private Button _missionTrackerPanelButton;

    private MissionTrackerPanel _missionTrackerPanel;

    [Inject]
    private void Construct(MissionTrackerPanel missionTrackerPanel)
    {
        _missionTrackerPanel = missionTrackerPanel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnMissionTracerButtonClick();
        }
    }

    private void OnEnable()
    {
        _missionTrackerPanelButton.onClick.AddListener(OnMissionTracerButtonClick);
    }

    private void OnDisable()
    {
        _missionTrackerPanelButton.onClick.RemoveListener(OnMissionTracerButtonClick);
    }

    private void OnMissionTracerButtonClick()
    {
        _missionTrackerPanel.gameObject.SetActive(!_missionTrackerPanel.gameObject.activeInHierarchy);
    }
}
