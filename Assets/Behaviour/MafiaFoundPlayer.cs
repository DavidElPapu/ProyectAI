using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/MafiaFoundPlayer")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "MafiaFoundPlayer", message: "[Patroler] Found [Player]", category: "Events", id: "217e7dde92cd8bd31d1288c3fe2e4feb")]
public sealed partial class MafiaFoundPlayer : EventChannel<GameObject, GameObject> { }

