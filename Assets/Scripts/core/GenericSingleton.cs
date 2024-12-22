using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // The static instance of the class
    private static T _instance;

    // Property to access the instance
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // Try to find the Singleton instance in the scene
                _instance = FindObjectOfType<T>();

                // If an instance does not exist in the scene, create one
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    // Ensure that only one instance exists by checking on Awake
    protected virtual void Awake()
    {
        // If another instance already exists, destroy the current one
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject); // Keep the instance across scenes
        }
    }
}
