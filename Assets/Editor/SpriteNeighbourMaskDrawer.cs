using UnityEditor;
using UnityEngine;
using System;

[CustomPropertyDrawer(typeof(SpriteNeighbourMask))]
public class SpriteNeighbourMaskDrawer : PropertyDrawer
{
    private readonly SpriteNeighbour[] m_EnumValues = (SpriteNeighbour[])Enum.GetValues(typeof(SpriteNeighbour));

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty spriteProp = property.FindPropertyRelative("sprite");
        SerializedProperty requiredMaskProp = property.FindPropertyRelative("requiredNeighboursMask");
        SerializedProperty optionalMaskProp = property.FindPropertyRelative("optionalNeighboursMask");

        // Reset indentation to fix fields being offset from rects
        position = EditorGUI.IndentedRect(position);
        int indentLevel = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        EditorGUI.BeginProperty(position, label, property);

        float maskGridSize = EditorGUIUtility.singleLineHeight * 3 + EditorGUIUtility.standardVerticalSpacing * 2;

        // Place sprite field along the top above sprite preview and to the right of mask grids
        Rect spriteFieldRect = new Rect(
            position.x,
            position.y,
            position.width - maskGridSize * 2 - EditorGUIUtility.singleLineHeight * 2,
            EditorGUIUtility.singleLineHeight
        );
        // Place sprite preview along left edge
        Rect spritePreviewRect = new Rect(
            position.x,
            position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
            maskGridSize,
            maskGridSize
        );
        // Place required mask grid second from the right (singleLineHeight away from optional mask grid)
        Rect requiredMaskFieldRect = new Rect(
            position.xMax - maskGridSize * 2 - EditorGUIUtility.singleLineHeight, 
            position.y, 
            maskGridSize, 
            maskGridSize + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
        );
        // Place optional mask grid along right edge
        Rect optionalMaskFieldRect = new Rect(
            position.xMax - maskGridSize, 
            position.y, 
            maskGridSize, 
            maskGridSize + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing
        );

        SpriteNeighbour requiredMaskVal = (SpriteNeighbour)requiredMaskProp.intValue;
        SpriteNeighbour optionalMaskVal = (SpriteNeighbour)optionalMaskProp.intValue;

        bool val = EditorGUI.PropertyField(spriteFieldRect, spriteProp, GUIContent.none);

        // Render preview sprite
        Sprite sprite = spriteProp.objectReferenceValue as Sprite;
        if (sprite != null)
        {
            Rect spritesheetRect = new Rect(
                sprite.textureRect.x / sprite.texture.width,
                sprite.textureRect.y / sprite.texture.height,
                sprite.textureRect.width / sprite.texture.width,
                sprite.textureRect.height / sprite.texture.height
            );
            GUI.DrawTextureWithTexCoords(spritePreviewRect, sprite.texture, spritesheetRect);
        } else
        {
            EditorGUI.DrawRect(spritePreviewRect, Color.grey);
        }

        requiredMaskVal = ToggleGrid(requiredMaskFieldRect, requiredMaskVal, "Required");
        optionalMaskVal = ToggleGrid(optionalMaskFieldRect, optionalMaskVal, "Optional");

        requiredMaskProp.intValue = (int)requiredMaskVal;
        optionalMaskProp.intValue = (int)optionalMaskVal;

        EditorGUI.EndProperty();
        EditorGUI.indentLevel = indentLevel;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int lineCount = 4;
        return EditorGUIUtility.singleLineHeight * lineCount + EditorGUIUtility.standardVerticalSpacing * lineCount;
    }

    /// <summary>
    /// Generates a toggle grid for a sprite neighbour mask.
    /// </summary>
    /// <param name="offset">The position of the top-left corner of the grid</param>
    /// <param name="mask">The mask to generate a grid for</param>
    /// <returns>The mask value after being modified by user input</returns>
    private SpriteNeighbour ToggleGrid(Rect rect, SpriteNeighbour mask, string label = "")
    {
        if (label != "")
        {
            Rect labelRect = new Rect(rect.x,rect.y, rect.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(labelRect, label);

            // Shrink rect by area taken up by label so toggles are evenly distributed
            rect.y += EditorGUIUtility.singleLineHeight;
            rect.height -= EditorGUIUtility.singleLineHeight;
        }
        //EditorGUI.DrawRect(rect, new Color(1f, 0f, 0f));
        // Subtract space taken by final row/col of toggles so they can be placed at xMax/yMax
        rect.width -= EditorGUIUtility.singleLineHeight;
        rect.height -= EditorGUIUtility.singleLineHeight;

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (r != 1 || c != 1)
                {
                    // Conditional reduces the array index once it skips the middle square, because there are only 8 enums but the loop counts 0 to 8 (9 positions)
                    SpriteNeighbour positionMask = m_EnumValues[r * 3 + c - (r * 3 + c > 4 ? 1 : 0)];
                    bool value = (mask & positionMask) == positionMask;

                    Rect checkboxRect = new Rect(
                        Mathf.Lerp(rect.xMin, rect.xMax, c / 2f),
                        Mathf.Lerp(rect.yMin, rect.yMax, r / 2f),
                        EditorGUIUtility.singleLineHeight, // For some reason the checkbox is unclickable unless the width is doubled
                        EditorGUIUtility.singleLineHeight
                    );
                    value = EditorGUI.Toggle(checkboxRect, value);

                    if (EditorGUI.Toggle(checkboxRect, value))
                    {
                        // Toggle set to true, so set this bit to 1 with an OR mask
                        mask = mask | positionMask;
                    } else
                    {
                        // Toggle set to false, so set this bit to 0 with an inverted AND mask
                        mask = mask & ~positionMask;
                    }
                }
            }
        }

        return mask;
    }
}
