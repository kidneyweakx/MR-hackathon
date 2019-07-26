using UnityEngine;
using System.Collections.Generic;
using Vuforia;
using System.Collections;

public class ModeTrantitionManager : MonoBehaviour
{
    public static ModeTrantitionManager instance;
    public bool InAR = true;
    bool TansAR;
    MixedRealityController.Mode mCurrentMode = MixedRealityController.Mode.VIEWER_AR_DEVICETRACKER;

    public GameObject[] _ARContentObjects;
    public GameObject[] _VRContentObjects;

    public UnityEngine.UI.Image blackPanel;
    bool isTransition;
    bool isTransitionOver;
    Camera _MainCamera;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        VuforiaARController.Instance.RegisterVuforiaInitializedCallback(Init);
    }
    void OnDestroy()
    {
        VuforiaARController.Instance.UnregisterVuforiaInitializedCallback(Init);
    }
    void Start()
    {
        _MainCamera = Camera.main;
        VideoBackgroundManager.Instance.SetVideoBackgroundEnabled(true);
    }

    void Init()
    {
        MixedRealityController.Instance.SetMode(GetMixedRealityMode());
        ActiveContentObject();
    }
    public void SetMixedRealityModeAR(bool isModeAR)
    {
        if (!isTransition)
        {
            TansAR = isModeAR;
            SetBlackMask(true);
        }
    }
    void LateUpdate()
    {
        UpdateMixedRealityController();
        UpdateBlackMask();
    }

    void UpdateMixedRealityController()
    {
        MixedRealityController.Mode mixedRealityMode = GetMixedRealityMode();
        mCurrentMode = mixedRealityMode;
        Init();
    }

    void UpdateCamera()
    {
        if (InAR)
        {
            _MainCamera.clearFlags = CameraClearFlags.SolidColor;
        }
        else
        {
            _MainCamera.clearFlags = CameraClearFlags.Skybox;
        }
    }

    void UpdateBlackMask()
    {
        if (isTransition)
        {
            float alphaColor;
            if (!isTransitionOver)
            {
                alphaColor = blackPanel.color.a + Time.deltaTime * 1.5f;
                if (blackPanel.color.a >= 1.0f)
                {
                    isTransitionOver = true;
                    InAR = TansAR;
                    UpdateCamera();
                }
            }
            else
            {
                alphaColor = blackPanel.color.a - Time.deltaTime * 1.5f;
                if (blackPanel.color.a <= 0.0f)
                {
                    SetBlackMask(false);
                }
            }
            blackPanel.color = new Color(blackPanel.color.r, blackPanel.color.g, blackPanel.color.b, alphaColor);
        }
    }

    void ActiveContentObject()
    {
        foreach (GameObject element in _ARContentObjects)
        {
            element.SetActive(InAR);
        }
        foreach (GameObject element in _VRContentObjects)
        {
            element.SetActive(!InAR);
        }
    }
    MixedRealityController.Mode GetMixedRealityMode()
    {
        if (InAR)
        {
            return MixedRealityController.Mode.VIEWER_AR;
        }
        return MixedRealityController.Mode.VIEWER_AR_DEVICETRACKER;
    }

    void SetBlackMask(bool isActive)
    {
        blackPanel.enabled = isActive;
        isTransition = isActive;
        isTransitionOver = false;
    }

    public void ActivateDatasets(bool enableDataset)
    {
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        IEnumerable<DataSet> datasets = objectTracker.GetDataSets();

        foreach (DataSet dataset in datasets)
        {
            if (enableDataset)
            {
                objectTracker.ActivateDataSet(dataset);

            }
            else
            {
                objectTracker.DeactivateDataSet(dataset);
            }
        }
    }
}