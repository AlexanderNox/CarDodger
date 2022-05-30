using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class AudioSyncCameraColor : AudioSyncer
{
    
    [SerializeField] private Color[] _beatColors;
    private Color _restColor;

    private int _randomIndx;
    private Camera _camera;
    
    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _restColor = _camera.backgroundColor;
    }
    
    private IEnumerator MoveToColor(Color _target)
    {
        Color current = _camera.backgroundColor;
        Color initial = current;
        float timer = 0;
		
        while (current != _target)
        {
            current = Color.Lerp(initial, _target, timer / _timeToBeat);
            timer += Time.deltaTime;

            _camera.backgroundColor = current;

            yield return null;
        }

        _isBeat = false;
    }

    private Color RandomColor()
    {
        if (_beatColors == null || _beatColors.Length == 0) return Color.white;
        _randomIndx = Random.Range(0, _beatColors.Length);
        return _beatColors[_randomIndx];
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_isBeat) return;

        _camera.backgroundColor = Color.Lerp(_camera.backgroundColor, _restColor, _restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        Color _c = RandomColor();

        StopCoroutine("MoveToColor");
        StartCoroutine("MoveToColor", _c);
    }
    
}
