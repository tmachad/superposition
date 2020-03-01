using UnityEditor;
using UnityEngine;
using System;

[CustomPropertyDrawer(typeof(AnimatedSpriteNeighbourMask), true)]
public class AnimatedSpriteNeighbourMaskDrawer : SpriteNeighbourMaskDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float minHeight = base.GetPropertyHeight(property, label);
        SerializedProperty sprites = property.FindPropertyRelative("animationSprites");

        return Mathf.Max(
            minHeight,
            EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing    // Height of first line
            + GetArrayHeight(sprites)
        );
    }

    /// <summary>
    /// Render the control to the inspector.
    /// </summary>
    /// <param name="position">The rect containing the control.</param>
    /// <param name="property">The serialized property to render the control for.</param>
    /// <param name="label">The label for this property.</param>
    protected override void Render(Rect position, SerializedProperty property, GUIContent label)
    {
        base.Render(position, property, label);
        SerializedProperty sprites = property.FindPropertyRelative("animationSprites");

        Rect spritesArrayBaseRect = new Rect(
            position.x + maskGridSize + EditorGUIUtility.singleLineHeight,
            position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
            position.width - maskGridSize * 3 - EditorGUIUtility.singleLineHeight * 3,
            GetArrayHeight(sprites)
        );

        Rect sizeFieldRect = new Rect(
            spritesArrayBaseRect.x,
            spritesArrayBaseRect.y + EditorGUIUtility.standardVerticalSpacing,
            spritesArrayBaseRect.width,
            EditorGUIUtility.singleLineHeight
        );
        sprites.arraySize = EditorGUI.IntField(sizeFieldRect, sprites.arraySize);
        
        for(int i = 0; i < sprites.arraySize; i++)
        {
            Rect itemFieldRect = new Rect(
                spritesArrayBaseRect.x,
                spritesArrayBaseRect.y + EditorGUIUtility.singleLineHeight * (i + 1) + EditorGUIUtility.standardVerticalSpacing * (i + 2),
                spritesArrayBaseRect.width,
                EditorGUIUtility.singleLineHeight
            );
            EditorGUI.PropertyField(itemFieldRect, sprites.GetArrayElementAtIndex(i), GUIContent.none);
        }
    }

    private float GetArrayHeight(SerializedProperty arrayProp)
    {
        float arrayHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;   // Height of size field
        if (arrayProp.isArray)
        {
            arrayHeight += EditorGUIUtility.singleLineHeight * arrayProp.arraySize + EditorGUIUtility.standardVerticalSpacing * arrayProp.arraySize;   // One line for each array entry
        }

        return arrayHeight;
    }
}
