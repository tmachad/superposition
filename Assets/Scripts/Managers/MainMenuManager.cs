using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject m_MainMenuPanel;
    public GameObject m_LevelMenuPanel;

    [Header("Level Buttons")]
    public GameObject m_LevelButtonPrefab;
    public float m_ButtonSpacing = 10.0f;
    public int m_MaxButtonsPerRow = 5;

    private void Start()
    {
        // Initialize to default state
        MainMenu();

        int levelButtons = LevelLoader.Instance.m_LevelSceneNames.Length;
        int buttonsInThisRow = 0;
        int rowIndex = -1;
        float colIndex = 0;
        Vector2 prefabSize = m_LevelButtonPrefab.GetComponent<RectTransform>().rect.size;
        for (int i = 0; i < levelButtons; i++)
        {
            if (i % m_MaxButtonsPerRow == 0)
            {
                // At the start of a row, update row parameters
                buttonsInThisRow = Mathf.Min((levelButtons - i),  m_MaxButtonsPerRow);
                rowIndex++;
                colIndex = (buttonsInThisRow - 1.0f) / 2.0f * -1.0f;
            }

            GameObject buttonObj = Instantiate(m_LevelButtonPrefab, m_LevelMenuPanel.transform);
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            buttonText.text = i.ToString("D2");
            Button button = buttonObj.GetComponent<Button>();
            int levelIndex = i;
            button.onClick.AddListener(delegate {
                LevelLoader.Instance.LoadLevel(levelIndex);
            });

            RectTransform buttonTrans = buttonObj.GetComponent<RectTransform>();
            buttonTrans.localPosition = new Vector3(
                (prefabSize.x + m_ButtonSpacing) * colIndex,
                (prefabSize.y + m_ButtonSpacing) * rowIndex * -1,
                0
            );

            colIndex++;
        }
    }

    public void MainMenu()
    {
        // Show main menu panel
        m_MainMenuPanel.SetActive(true);
        m_LevelMenuPanel.SetActive(false);
    }

    public void LevelMenu()
    {
        // Show level menu panel
        m_MainMenuPanel.SetActive(false);
        m_LevelMenuPanel.SetActive(true);
    }

    public void Quit()
    {
        GameManager.Instance.Quit();
    }

    public void LoadLevel(int index)
    {
        LevelLoader.Instance.LoadLevel(index);
    }
}
