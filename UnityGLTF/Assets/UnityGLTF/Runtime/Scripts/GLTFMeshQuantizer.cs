using UnityEngine;

public class GLTFMeshQuantizer
{
	static public short[] EncodePositions(Vector3[] arr, Vector3 min, Vector3 max)
	{
		var dimensions = max - min;
		var scale = dimensions / 2.0f;	// scale into -1.0 ~ 1.0
		var halfDims = scale;
		var center = halfDims + min;
		var translation = center;
		var positions = new short[arr.Length * 3];

		for (int i = 0; i < arr.Length; ++i)
		{
			var pos = (arr[i] - translation);
			positions[i * 3] = Encode2Short(Mathf.Approximately(scale.x, 0.0f) ? 0.0f : pos.x / scale.x);
			positions[i * 3 + 1] = Encode2Short(Mathf.Approximately(scale.y, 0.0f) ? 0.0f : pos.y / scale.y);
			positions[i * 3 + 2] = Encode2Short(Mathf.Approximately(scale.z, 0.0f) ? 0.0f : pos.z / scale.z);
		}

		return positions;
	}

	static public short[] EncodeQuaternions(Quaternion[] arr)
	{
		var quaternions = new short[arr.Length * 4];

		for (int i = 0; i < arr.Length; ++i)
		{
			var quat = arr[i];
			quaternions[i * 3] = Encode2Short(quat.x);
			quaternions[i * 3 + 1] = Encode2Short(quat.y);
			quaternions[i * 3 + 2] = Encode2Short(quat.z);
			quaternions[i * 3 + 3] = Encode2Short(quat.w);
		}

		return quaternions;
	}

	static public sbyte Encode2SByte(float f)
	{
		return (sbyte)Mathf.RoundToInt(f * 127.0f);
	}

	static public byte Encode2Byte(float f)
	{
		return (byte)Mathf.RoundToInt(f * 255.0f);
	}

	static public short Encode2Short(float f)
	{
		return (short)Mathf.RoundToInt(f * 32767.0f);
	}

	static public ushort Encode2UShort(float f)
	{
		return (ushort)Mathf.RoundToInt(f * 65535.0f);
	}
}
