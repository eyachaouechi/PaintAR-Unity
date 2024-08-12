using UnityEngine;
using UnityEngine.Events;

namespace PaintIn3D
{
	/// <summary>This component invokes the <b>Action</b> event when this component is enabled.</summary>
	[HelpURL(P3dHelper.HelpUrlPrefix + "P3dActionOnEnable")]
	[AddComponentMenu(P3dHelper.ComponentMenuPrefix + "Examples/Action OnEnable")]
	public class P3dActionOnEnable : MonoBehaviour
	{
		/// <summary>The event that will be invoked.</summary>
		public UnityEvent Action { get { if (action == null) action = new UnityEvent(); return action; } } [SerializeField] public UnityEvent action;

		protected virtual void OnEnable()
		{
			if (action != null)
			{
				action.Invoke();
			}
		}
	}
}

#if UNITY_EDITOR
namespace PaintIn3D
{
	using UnityEditor;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(P3dActionOnEnable))]
	public class P3dActionOnEnable_Editor : P3dEditor<P3dActionOnEnable>
	{
		protected override void OnInspector()
		{
			Draw("action");
		}
	}
}
#endif