using UnityEngine;

public class Singleton<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public bool global = true;
    private T _instance;
    public T Instance
    {
        get
        {
            if ( _instance == null )
            {
                _instance = FindObjectOfType<T>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (global)
        {
            DontDestroyOnLoad(gameObject);
        }
        OnStart();
    }
    private void OnStart()
    {

    }
}
