using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Agent Find Object")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Agent Find Object", message: "[Agend] found [Object] with [Tag]", category: "Events", id: "d3dfc4a796df311ce65d1e587925f17f")]
public sealed partial class AgentFindObject : EventChannel<GameObject, GameObject, string> { }

