using Newtonsoft.Json.Linq;
using GLTF.Extensions;
using GLTF.Math;
using System.Collections.Generic;

namespace GLTF.Schema
{
	public class HT_node_colliderExtensionFactory : ExtensionFactory
	{
		public const string EXTENSION_NAME = "HT_node_collider";

		public const string TYPE = "type";
		public const string ISTRIGGER = "isTrigger";
		public const string CENTER = "center";
		public const string SIZE = "size";
		public const string RADIUS = "radius";
		public const string HEIGHT = "height";
		public const string DIRECTION = "direction";
		public const string COLLIDERS = "colliders";


		public HT_node_colliderExtensionFactory()
		{
			ExtensionName = EXTENSION_NAME;
		}

		public override IExtension Deserialize(GLTFRoot root, JProperty extensionToken)
		{
			List<HT_node_colliderExtension.Collider> Colliders = new List<HT_node_colliderExtension.Collider>();

			if (!extensionToken.HasValues)
			{
				return new HT_node_colliderExtension(Colliders);
			}

			foreach (JToken value in extensionToken.Values())
			{
				HT_node_colliderExtension.ColliderType type = HT_node_colliderExtension.COLLIDERTYPE_DEFAULT;
				bool isTrigger = HT_node_colliderExtension.ISTRIGGER_DEFAULT;
				Vector3 center = HT_node_colliderExtension.CENTER_DEFAULT;
				Vector3 size = HT_node_colliderExtension.SIZE_DEFAULT;
				float radius = HT_node_colliderExtension.RADIUS_DEFAULT;
				float height = HT_node_colliderExtension.HEIGHT_DEFAULT;
				HT_node_colliderExtension.CapsuleDirection direction = HT_node_colliderExtension.DIRECTION_DEFAULT;

				JToken typeToken = value[TYPE];
				type = typeToken != null ? (HT_node_colliderExtension.ColliderType)typeToken.DeserializeAsInt() : type;

				JToken isTriggerToken = value[ISTRIGGER];
				isTrigger = isTriggerToken != null ? isTriggerToken.DeserializeAsBool() : isTrigger;

				JToken centerToken = value[CENTER];
				center = centerToken != null ? centerToken.DeserializeAsVector3() : center;

				if (type == HT_node_colliderExtension.ColliderType.Box)
				{
					JToken sizeToken = value[SIZE];
					size = sizeToken != null ? sizeToken.DeserializeAsVector3() : size;

					Colliders.Add(new HT_node_colliderExtension.BoxCollider(isTrigger, center, size));
				}

				if (type == HT_node_colliderExtension.ColliderType.Sphere)
				{
					JToken radiusToken = value[RADIUS];
					radius = radiusToken != null ? radiusToken.DeserializeAsFloat() : radius;

					Colliders.Add(new HT_node_colliderExtension.SphereCollider(isTrigger, center, radius));
				}

				if (type == HT_node_colliderExtension.ColliderType.Capsule)
				{
					JToken radiusToken = value[RADIUS];
					radius = radiusToken != null ? radiusToken.DeserializeAsFloat() : radius;

					JToken heightToken = value[HEIGHT];
					height = heightToken != null ? heightToken.DeserializeAsFloat() : height;

					JToken directionToken = value[DIRECTION];
					direction = directionToken != null ? (HT_node_colliderExtension.CapsuleDirection)directionToken.DeserializeAsInt() : direction;

					Colliders.Add(new HT_node_colliderExtension.CapsuleCollider(isTrigger, center, radius, height, direction));
				}
			}

			return new HT_node_colliderExtension(Colliders);
		}
	}
}
