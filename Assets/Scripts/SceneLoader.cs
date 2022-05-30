using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private int _sceneToLoad;
    
    public void LoadScene(int sceneIndex)
    {
        _sceneToLoad = sceneIndex;
        _animator.SetTrigger("FadeOut");
    }
    
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}
