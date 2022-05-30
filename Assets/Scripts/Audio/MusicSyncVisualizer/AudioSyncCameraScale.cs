using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AudioSyncCameraScale : AudioSyncer
{
    
    [SerializeField] private float _beatScale;
    
    private float _restScale;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _restScale = _camera.orthographicSize;
    }

    private IEnumerator MoveToScale(float _target)
    {
        float current  = _camera.orthographicSize;
        float initial = current ;
        float timer = 0;

        while (current  != _target)
        {
            current  = Mathf.Lerp(initial, _target, timer / _timeToBeat);
            timer += Time.deltaTime;

            _camera.orthographicSize = current ;

            yield return null;
        }

        _isBeat = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_isBeat) return;

        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _restScale, _restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", _beatScale);
    }
}
