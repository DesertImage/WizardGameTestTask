using System;
using GameCOP.Spawning;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameCOPEditor.Spawning
{
    [CustomPropertyDrawer(typeof(SpawningNode))]
    public class SpawningNodeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var id = property.FindPropertyRelative(nameof(SpawningNode.Id));
            var instance = property.FindPropertyRelative(nameof(SpawningNode.Instance));
            var preRegisteredCount = property.FindPropertyRelative(nameof(SpawningNode.PreRegisterCount));

            var container = new VisualElement
            {
                style = { flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row) }
            };

            var idField = new PropertyField(id, string.Empty)
            {
                style =
                {
                    width = 100f,
                    maxWidth = 200f
                }
            };

            var enumIdField = new EnumField((ObjectId)id.intValue)
            {
                style =
                {
                    width = 100f,
                    maxWidth = 200f
                }
            };

            enumIdField.RegisterValueChangedCallback
            (
                evt =>
                {
                    if (evt.newValue == evt.previousValue) return;

                    id.intValue = (int)(ObjectId)evt.newValue;

                    instance.serializedObject.ApplyModifiedProperties();
                    instance.serializedObject.Update();
                }
            );

            var objectField = new ObjectField
            {
                objectType = typeof(SpawningNode).GetField(nameof(SpawningNode.Instance)).FieldType,
                allowSceneObjects = false,
                value = instance.objectReferenceValue,
                style = { flexGrow = 1f }
            };

            objectField.RegisterValueChangedCallback
            (
                evt =>
                {
                    if (evt.newValue == evt.previousValue) return;

                    instance.objectReferenceValue = evt.newValue;

                    instance.serializedObject.ApplyModifiedProperties();
                    instance.serializedObject.Update();
                }
            );

            container.Add(enumIdField);
            container.Add(new ToolbarSpacer());
            container.Add(objectField);
            container.Add(new ToolbarSpacer());
            container.Add(new PropertyField(preRegisteredCount, string.Empty));

            return container;
        }
    }
}