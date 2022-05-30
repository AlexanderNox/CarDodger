using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTest : MonoBehaviour
{
    [SerializeField] private Transform _popupPrefab;
    private float _timer = 1;
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            Transform scorePopupTransform = Instantiate(_popupPrefab, transform.position, Quaternion.identity);
            ScorePopup scorePopup = scorePopupTransform.GetComponent<ScorePopup>();
            scorePopup.Setup("+ test" );

            _timer = 1;
        }
    }
}
