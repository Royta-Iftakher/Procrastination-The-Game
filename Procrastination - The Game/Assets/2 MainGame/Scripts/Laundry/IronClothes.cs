using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IronClothes : MonoBehaviour
{
    public Slider ironingSlider; // Assign the Slider component in the Inspector
    public TextMeshProUGUI instructionText; // Assign the TextMeshProUGUI component in the Inspector
    public WashingMachine washingMachine; // Assign the WashingMachine component in the Inspector
    public float taskDoneDisplayTime = 3f; // The time (in seconds) to display the "Task Done" message
    public Image ironTableImage; // Assign the Image component of the iron table in the Inspector
    public Sprite[] ironTableImages; // Assign the array of images in the Inspector

    private bool isTaskComplete = false; // Initially, the task is not complete
    private bool hasImageChanged = false; // Initially, the image has not been changed

    void Start()
    {
        instructionText.text = ""; // Hide the instruction text at the start
        ironTableImage.sprite = ironTableImages[0]; // Display the initial image
    }

    void Update()
    {
        if (washingMachine.isLaundryDone && !isTaskComplete)
        {
            instructionText.text = "Hold Space to Iron";

            // Change the iron table image once
            if (!hasImageChanged)
            {
                ironTableImage.sprite = ironTableImages[1]; // Display the second image
                hasImageChanged = true; // The image has been changed
            }

            if (Input.GetKey(KeyCode.Space))
            {
                ironingSlider.value += Time.deltaTime; // Increase the slider value while holding Space
            }

            if (ironingSlider.value >= ironingSlider.maxValue)
            {
                // laundry task complete
                isTaskComplete = true;
                instructionText.text = "Task Done";
                StartCoroutine(HideTaskDoneTextAfterDelay(taskDoneDisplayTime));

                PauseMenu.Instance.laundryTask(true);
            }
        }
    }

    IEnumerator HideTaskDoneTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        instructionText.text = "";
    }
}
