namespace Bas.Pennings.UnityTools
{
    public class MissingComponentException<T> : System.Exception where T : UnityEngine.Component
    {
        public MissingComponentException(UnityEngine.GameObject gameObject)
            : base($"GameObject '{gameObject.name}' is missing a `{nameof(T)}` component!") { }

        public MissingComponentException(string message) : base(message) { }
    }
}