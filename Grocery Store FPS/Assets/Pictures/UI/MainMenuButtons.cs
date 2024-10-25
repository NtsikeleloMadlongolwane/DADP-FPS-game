using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public Vector3[] positions = new Vector3[3];
    public Image[] potions = new Image[3];
    public Button moveLeftButton;
    public Button moveRightButton;
    public Button[] potionButtons = new Button[5];

    private int[] indices = new int[3];

    void Start()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            indices[i] = i;
        }
        moveLeftButton.onClick.AddListener(MoveLeft);
        moveRightButton.onClick.AddListener(MoveRight);
        SetPotionPositions();
        LogCurrentPotion();

        // Set up button interactions
        for (int i = 0; i < potionButtons.Length; i++)
        {
            int index = i;
            potionButtons[i].onClick.AddListener(() => OnPotionButtonClick(index));
        }
    }

    void SetPotionPositions()
    {
        for (int i = 0; i < potions.Length; i++)
        {
            potions[indices[i]].rectTransform.localPosition = positions[i];
            potionButtons[indices[i]].GetComponent<RectTransform>().localPosition = positions[i]; // Correctly get RectTransform
            potions[indices[i]].rectTransform.localScale = (i == 1)
                ? new Vector3(1f, 1f, 1f)
                : new Vector3(.5f, .5f, 1f);

            // Enable or disable buttons based on their position
            potionButtons[indices[i]].gameObject.SetActive(i == 1);
        }
    }

    void MoveLeft()
    {
        int tempIndex = indices[0];
        for (int i = 0; i < indices.Length - 1; i++)
        {
            indices[i] = indices[i + 1];
        }
        indices[indices.Length - 1] = tempIndex;
        SetPotionPositions();
        LogCurrentPotion();
    }

    void MoveRight()
    {
        int tempIndex = indices[indices.Length - 1];
        for (int i = indices.Length - 1; i > 0; i--)
        {
            indices[i] = indices[i - 1];
        }
        indices[0] = tempIndex;
        SetPotionPositions();
        LogCurrentPotion();
    }

    void LogCurrentPotion()
    {
        Debug.Log("Image in third position: " + potions[indices[1]].name);
    }

    void OnPotionButtonClick(int index)
    {
        Debug.Log("Potion button clicked: " + potionButtons[index].name);
    }
}
