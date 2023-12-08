using UnityEngine;

namespace Framework.Samples.SaveSystem
{
	/// <summary>
	/// An example class that needs to have data saved.
	/// This class, as well as other classes that needs saving, must implement the ISaveable interface.
	/// </summary>
	public class SaveSystemPhysicsDemo : MonoBehaviour, ISaveable
	{
		[SerializeField] private Rigidbody _rigidbody;

		public object CaptureState()
		{
			return new SaveData()
			{
				position = transform.position,
				rotation = transform.rotation,
				velocity = _rigidbody.velocity,
				angularVelocity = _rigidbody.angularVelocity
			};
		}

		public void RestoreState(object state)
		{
			var saveData = (SaveData) state;
			transform.position = saveData.position;
			transform.rotation = saveData.rotation;
			_rigidbody.velocity = saveData.velocity;
			_rigidbody.angularVelocity = saveData.angularVelocity;
		}

		/// <summary>
		/// This struct is used to define what to save
		/// </summary>
		[System.Serializable]
		private struct SaveData
		{
			public SerializableVector3 position;
			public SerializableQuaternion rotation;
			public SerializableVector3 velocity;
			public SerializableVector3 angularVelocity;
		}
	}
}