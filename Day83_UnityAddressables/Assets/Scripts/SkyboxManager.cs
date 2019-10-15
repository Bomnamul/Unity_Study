//http://thegamedev.guru/unity-addressables/tutorial-learn-the-basics/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] private List<AssetReference> _skyboxMaterials;
    AsyncOperationHandle _currentHandle;

    public void SetSkybox(int skyboxIndex)
    {
        //RenderSettings.skybox = _skyboxMaterials[skyboxIndex];
        StartCoroutine(SetSkyboxInternal(skyboxIndex));
    }

    IEnumerator SetSkyboxInternal(int skyboxIndex)
    {
        if (_currentHandle.IsValid())
        {
            Addressables.Release(_currentHandle);
        }
        AssetReference skyboxMaterialReference = _skyboxMaterials[skyboxIndex];
        _currentHandle = skyboxMaterialReference.LoadAssetAsync<Material>();
        yield return _currentHandle;
        RenderSettings.skybox = (Material)_currentHandle.Result;
    }
}
