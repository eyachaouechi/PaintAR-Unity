using UnityEngine;

namespace PaintIn3D.Examples
{
	/// <summary>This component allows you to rotate the current <b>Transform</b>.</summary>
	[HelpURL(P3dHelper.HelpUrlPrefix + "P3dRotate")]
	[AddComponentMenu(P3dHelper.ComponentMenuPrefix + "Examples/Rotate")]
	public class P3dRotate : MonoBehaviour
	{
		/// <summary>This allows you to set the coordinate space the movement will use.</summary>
		public Space Space { set { space = value; } get { return space; } } [SerializeField] private Space space = Space.Self;

		/// <summary>The position will be incremented by this each second.</summary>
		public Vector3 PerSecond { set { perSecond = value; } get { return perSecond; } } [SerializeField] private Vector3 perSecond;

		protected virtual void Update()
		{
			transform.Rotate(perSecond * Time.deltaTime, space);
		}
	}
}

#if UNITY_EDITOR
namespace PaintIn3D.Examples
{
	using UnityEditor;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(P3dRotate))]
	public class P3dRotate_Editor : P3dEditor<P3dRotate>
	{
		protected override void OnInspector()
		{
			Draw("space", "This allows you to set the coordinate space the movement will use.");
			Draw("perSecond", "The position will be incremented by this each second.");
		}
	}
}
#endif