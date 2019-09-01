using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SpriteNeighbourMask))]
public class SpriteNeighbourMaskDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty spriteProp = property.FindPropertyRelative("sprite");
        SerializedProperty requiredMaskProp = property.FindPropertyRelative("requiredNeighboursMask");
        SerializedProperty optionalMaskProp = property.FindPropertyRelative("optionalNeighboursMask");

        EditorGUI.BeginProperty(position, label, property);

        Rect spriteFieldRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        Rect requiredMaskFieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
        Rect optionalMaskFieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight);

        SpriteNeighbour requiredMaskVal = (SpriteNeighbour)requiredMaskProp.intValue;
        SpriteNeighbour optionalMaskVal = (SpriteNeighbour)optionalMaskProp.intValue;

        EditorGUI.PropertyField(spriteFieldRect, spriteProp);
        requiredMaskVal = (SpriteNeighbour)EditorGUI.EnumFlagsField(requiredMaskFieldRect, "Required Neighbour Mask", requiredMaskVal);
        optionalMaskVal = (SpriteNeighbour)EditorGUI.EnumFlagsField(optionalMaskFieldRect, "Optional Neighbour Mask", optionalMaskVal);

        requiredMaskProp.intValue = (int)requiredMaskVal;
        optionalMaskProp.intValue = (int)optionalMaskVal;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 3;
    }
}
