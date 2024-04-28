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

	public static implicit operator Vector3(SerializableVector3 vector)
	{
		return new Vector3(vector.x, vector.y, vector.z);
	}

	public static implicit operator SerializableVector3(Vector3 vector)
	{
		return new SerializableVector3(vector);
	}
}

[System.Serializable]
public struct SerializableQuaternion
{
	public float x;
	public float y;
	public float z;
	public float w;

	public SerializableQuaternion(Quaternion quaternion)
	{
		x = quaternion.x;
		y = quaternion.y;
		z = quaternion.z;
		w = quaternion.w;
	}

	public override string ToString()
	{
		return $"[{x}, {y}, {z}, {w}]";
	}

	public static implicit operator Quaternion(SerializableQuaternion serializableQuaternion)
	{
		return new Quaternion(serializableQuaternion.x, serializableQuaternion.y, serializableQuaternion.z, serializableQuaternion.w);
	}

	public static implicit operator SerializableQuaternion(Quaternion quaternion)
	{
		return new SerializableQuaternion(quaternion);
	}
}