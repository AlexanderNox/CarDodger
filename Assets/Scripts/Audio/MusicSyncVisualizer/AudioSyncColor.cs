using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AudioSyncColor : AudioSyncer
{
    
    public Color[] _beatColors;
    public Color _restColor;

    private int _randomIndx;
    private Image _img;
    
    private IEnumerator MoveToColor(Color _target)
    {
        Color _curr = _img.color;
        Color _initial = _curr;
        float _timer = 0;
		
        while (_curr != _target)
        {
            _curr = Color.Lerp(_initial, _target, _timer / _timeToBeat);
            _timer += Time.deltaTime;

            _img.color = _curr;

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

        _img.color = Color.Lerp(_img.color, _restColor, _restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        Color _c = RandomColor();

        StopCoroutine("MoveToColor");
        StartCoroutine("MoveToColor", _c);
    }

    private void Start()
    {
        _img = GetComponent<Image>();
    }

}
