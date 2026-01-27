using UnityEngine;

namespace Bas.Pennings.DevTools
{
    /// <summary>
    /// Base class for creating safe singletons in Unity.  
    /// Handles scene reloads, duplicate prevention, and misuse detection.  
    /// Execution order is set to initialize early to avoid race conditions.
    /// </summary>
    [DefaultExecutionOrder(-100)]
    public abstract class AbstractSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// Whether to persist across scene reloads. False by default.
        /// </summary>
        [SerializeField, Tooltip("Don't Destroy On Load. Keeps the game object loaded across scenes.")]
        private bool _DDOL = false;

        private static T instance;
        private static bool applicationIsQuitting;

        /// <summary>
        /// Singleton instance. Returns null if accessed too early, after quit, or while disabled.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning($"[Singleton] Tried to access '{typeof(T)}.{nameof(Instance)}' after application quit.");
                    return null;
                }

                if (instance == null)
                {
                    Debug.LogError($"[Singleton] '{typeof(T)}' instance requested before initialization (Awake). Check script execution order.");
                    return null;
                }

                if (!instance.enabled)
                {
                    Debug.LogError($"[Singleton] '{typeof(T)}' instance exists but is disabled. This is not allowed, returning null.");
                    return null;
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance != null && instance != this)
            {
                Debug.LogWarning($"[Singleton] Duplicate '{typeof(T)}' found on '{gameObject.name}'. Disabling this component.");
                enabled = false;
                return;
            }

            instance = this as T;

            if (_DDOL) DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
                instance = null;
        }

        protected virtual void OnApplicationQuit()
            => applicationIsQuitting = true;
    }
}