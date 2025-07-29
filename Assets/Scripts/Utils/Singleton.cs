using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	private static bool _isInitialized;
	private static T m_Instance = null;
	
    public static T Instance
    {
        get
        {
	        if(m_Instance == null)
            {
                m_Instance = FindObjectOfType(typeof(T)) as T;

				if (!_isInitialized)
				{
					_isInitialized = true;
					m_Instance.Init();
				}
            }
            return m_Instance;
        }
    }
    
    private void Awake()
    {
        if (m_Instance == null) 
        {
			m_Instance = this as T;
		} 
        else if (m_Instance != this) 
        {
			DestroyImmediate (this);
			return;
		}
        
		if (!_isInitialized) 
		{
			DontDestroyOnLoad(gameObject);
			_isInitialized = true;
			m_Instance.Init ();
		}
    }

    protected virtual void Init()
    {
    }
 
	private void OnApplicationQuit()
    {
        m_Instance = null;
    }
}