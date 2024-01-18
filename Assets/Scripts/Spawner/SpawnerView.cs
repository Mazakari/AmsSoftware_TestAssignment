using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _pointBlockedText;
    [SerializeField] private float _warningShowTimer = 3f;

    private void OnEnable()
    {
        HideWarning();
        SubscribeSpawnerCallbacks();
    }
   

    private void OnDisable() => 
        UnsubscribeSpawnerCallbacks();

    private void ShowWarningMessage()
    {
        StopAllCoroutines();
        StartCoroutine(ShowTextCoroutine());
    }
    private void ShowWarning() =>
      _pointBlockedText.gameObject.SetActive(true);

    private void HideWarning() =>
       _pointBlockedText.gameObject.SetActive(false);

    private void SubscribeSpawnerCallbacks() => 
        Spawner.OnSpawnPointBlockedEvent += ShowWarningMessage;

    private void UnsubscribeSpawnerCallbacks() => 
        Spawner.OnSpawnPointBlockedEvent -= ShowWarningMessage;

    private IEnumerator ShowTextCoroutine()
    {
        ShowWarning();
        yield return new WaitForSeconds(_warningShowTimer);
        HideWarning();
    }
}
