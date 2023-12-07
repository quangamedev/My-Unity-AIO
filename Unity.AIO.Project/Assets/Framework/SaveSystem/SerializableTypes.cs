using UnityEngine;
using System.Collections;

[System.Serializable]
public struct SerializableVector3
{
	public float x;
	public float y;
	public float z;

	public SerializableVector3(Vector3 vector)
	{
		x = vector.x;
		y = vector.y;
		z = vector.z;
	}

	public override string ToString()
	{
		return $"[{x}, {y}, {z}]";
	}

	/// <summary>
	/// Automatic converts from SerializableVector3 to Vector3
	/// </summary>
	/// <param name="vector"></param>
	/// <returns></returns>
	public static implicit operator Vector3(SerializableVector3 vector)
	{
		return new Vector3(vector.x, vector.y, vector.z);
	}

	/// <summary>
	/// Automatic converts from Vector3 to SerializableVector3
	/// </summary>
	/// <param name="vector"></param>
	/// <returns></returns>
	public static implicit operator SerializableVector3(Vector3 vector)
	{
		return new SerializableVector3(vector);
	}
}