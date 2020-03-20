using System;
using GLTF.Math;
using GLTF.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace GLTF.Schema
{
	public class HT_node_colliderExtension : IExtension
	{
		public enum ColliderType
		{
			Box,
			Sphere,
			Capsule
		}

		public enum CapsuleDirection
		{
			X_Axis,
			Y_Axis,
			Z_Axis
		}

		public static readonly bool ISTRIGGER_DEFAULT = false;
		public static readonly Vector3 CENTER_DEFAULT = new Vector3(0.0f, 0.0f, 0.0f);
		public static readonly ColliderType COLLIDERTYPE_DEFAULT = ColliderType.Box;
		public static readonly Vector3 SIZE_DEFAULT = new Vector3(0.0f, 0.0f, 0.0f);
		public static readonly float RADIUS_DEFAULT = 0.0f;
		public static readonly float HEIGHT_DEFAULT = 0.0f;
		public static readonly CapsuleDirection DIRECTION_DEFAULT = CapsuleDirection.Y_Axis;

		public abstract class Collider
		{
			private JObject obj = new JObject();

			public ColliderType Type = COLLIDERTYPE_DEFAULT;
			public bool IsTrigger = ISTRIGGER_DEFAULT;
			public Vector3 Center = CENTER_DEFAULT;

			public Collider(ColliderType type, bool isTrigger, Vector3 center)
			{
				Type = type;
				IsTrigger = isTrigger;
				Center = center;
			}

			public virtual JObject Serialize()
			{
				obj.Add(new JProperty(
					HT_node_colliderExtensionFactory.TYPE,
					Type.ToString()
				));

				if (IsTrigger != ISTRIGGER_DEFAULT)
				{
					obj.Add(new JProperty(
						HT_node_colliderExtensionFactory.ISTRIGGER,
						IsTrigger
					));
				}

				if (Center != CENTER_DEFAULT)
				{
					obj.Add(new JProperty(
						HT_node_colliderExtensionFactory.CENTER,
						new JArray(Center.X, Center.Y, Center.Z)
					));
				}

				return obj;
			}
		}

		public class BoxCollider : Collider
		{
			public Vector3 Size = SIZE_DEFAULT;

			public BoxCollider(bool isTrigger, Vector3 center, Vector3 size)
				: base(ColliderType.Box, isTrigger, center)
			{
				Size = size;
			}

			public override JObject Serialize()
			{
				JObject box = base.Serialize();

				if (Size != SIZE_DEFAULT)
				{
					box.Add(new JProperty(
						HT_node_colliderExtensionFactory.SIZE,
						new JArray(Size.X, Size.Y, Size.Z)
					));
				}

				return box;
			}
		}

		public class SphereCollider : Collider
		{
			public float Radius = RADIUS_DEFAULT;

			public SphereCollider(bool isTrigger, Vector3 center, float radius)
				: base(ColliderType.Sphere, isTrigger, center)
			{
				Radius = radius;
			}

			public override JObject Serialize()
			{
				JObject sphere = base.Serialize();

				if (Radius != RADIUS_DEFAULT)
				{
					sphere.Add(new JProperty(
						HT_node_colliderExtensionFactory.RADIUS,
						Radius
					));
				}

				return sphere;
			}
		}

		public class CapsuleCollider : Collider
		{
			public float Radius = RADIUS_DEFAULT;
			public float Height = HEIGHT_DEFAULT;
			public CapsuleDirection Direction = DIRECTION_DEFAULT;

			public CapsuleCollider(bool isTrigger, Vector3 center, float radius, float height, CapsuleDirection direction)
				: base(ColliderType.Capsule, isTrigger, center)
			{
				Radius = radius;
				Height = height;
				Direction = direction;
			}

			public override JObject Serialize()
			{
				JObject capsule = base.Serialize();

				if (Radius != RADIUS_DEFAULT)
				{
					capsule.Add(new JProperty(
						HT_node_colliderExtensionFactory.RADIUS,
						Radius
					));
				}

				if (Height != HEIGHT_DEFAULT)
				{
					capsule.Add(new JProperty(
						HT_node_colliderExtensionFactory.HEIGHT,
						Height
					));
				}

				if (Direction != DIRECTION_DEFAULT)
				{
					capsule.Add(new JProperty(
						HT_node_colliderExtensionFactory.DIRECTION,
						Direction
					));
				}

				return capsule;
			}
		}

		List<Collider> Colliders = new List<Collider>();

		public HT_node_colliderExtension(List<Collider> colliders)
		{
			Colliders = new List<Collider>(colliders);
		}

		public IExtension Clone(GLTFRoot root)
		{
			return new HT_node_colliderExtension(Colliders);
		}

		public JProperty Serialize()
		{
			JArray ext = new JArray();

			foreach (Collider collider in Colliders)
			{
				ext.Add(JToken.FromObject(collider.Serialize()));
			}

			return new JProperty(HT_node_colliderExtensionFactory.EXTENSION_NAME, ext);
		}
	}
}
