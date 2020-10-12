using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour
    where T : class
{
    /// <summary>
    /// SingletoneBase instance back field
    /// </summary>
    private static T instance = null;
    private static readonly Object syncRoot = new Object();
    /// <summary>
    /// SingletoneBase instance
    /// </summary>
    ///
    public T localInstance;
    public static T TestShit;
    public static string StringTestShit;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = GameObject.FindObjectOfType(typeof(T)) as T;
                        if (instance == null)
                            Debug.LogError("SingletoneBase<T>: Could not found GameObject of type " + typeof(T).Name);
                    }
                }
            }
            return instance;
        }
        set { }
    }

}