using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Transcript : MonoBehaviour
{
    public Image transcriptImage;
    public Button transcriptButton;
    public TextMeshProUGUI transcriptText;

    private bool isTranscriptShown;
    private Coroutine typingCoroutine;
    private string fullText;
    private float typingSpeed = 21f;

    private void Start()
    {
        transcriptButton.onClick.AddListener(ToggleTranscript);
        transcriptImage.gameObject.SetActive(false);
        isTranscriptShown = false;
        fullText = "Heyy, what's up?\n\n" +
                   "Oh my gosh, you won't believe what just happened to me today. So, I was walking down the street and this guy came up to me and was like, \"Hey, can I borrow your phone?\" And I was like, \"Uh, no way? Stranger danger!\"\n\n" +
                   "But then, get this, he showed me a picture of his adorable little puppy and I just couldn't resist. So, I let him borrow my phone and you know what? He ended up calling his mom right there on the spot! Can you believe it? Turns out his dog ran off with his phone and his mom had to track its location using Find My Friends.\n\n" +
                   "Anyway, what are you up to today? Oh wait, let me guess, you're trying to catch up on assignments? Take it one step at a time—that's what I always say! Taking breaks between difficult or time-consuming tasks makes them seem less daunting. Like cooking some food, or hey, even talking to me, your bff! Nothing beats a little fresh air! I mean, who wants to spend all day cooped up doing work, am I right?\n\n" +
                   "Oh, but speaking of schoolwork, did I tell you about the time I accidentally walked into the wrong class? Yeah, I was looking for my game design and development class and I ended up in a math class instead. And get this, the teacher was like, \"Alright class, let's find the area of this curve.\" And I was like, \"Umm, excuse me, I didn't sign up for calculus!\"\n\n" +
                   "Anyway, what else is new with you? Have you heard about that new restaurant downtown? I heard they have the best sushi in the city. We should totally go check it out sometime.\n\n" +
                   "Oh, and I almost forgot to tell you, I started taking this new dance class and it is so much fun. You should come with me next time, it'll be a blast.\n\n" +
                   "Anyway, I should probably let you go. I could talk for hours, but I know you have important student stuff to do. Love you! Bye!";

        // Split the full text into separate parts based on line breaks
        string[] textParts = fullText.Split(new[] { "\n\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        transcriptText.text = ""; // Clear the initial text

        // Start the coroutine to display the text parts sequentially
        typingCoroutine = StartCoroutine(TypeTextParts(textParts));
    }

    private IEnumerator TypeTextParts(string[] textParts)
    {
        // Display each part of the text sequentially
        for (int i = 0; i < textParts.Length; i++)
        {
            string currentPart = textParts[i];

            // Clear the transcript text and start typing the current part
            transcriptText.text = "";
            yield return StartCoroutine(TypeText(currentPart));

            // Wait for a short delay before displaying the next part
            yield return new WaitForSeconds(3f);
        }

        // After displaying all parts, hide the transcript
        HideTranscript();
    }

    private IEnumerator TypeText(string text)
    {
        // Type the text character by character
        for (int i = 0; i < text.Length; i++)
        {
            transcriptText.text += text[i];
            yield return new WaitForSeconds(1f / typingSpeed);
        }
    }

    public void ToggleTranscript()
    {
        if (isTranscriptShown)
        {
            HideTranscript();
        }
        else
        {
            ShowTranscript();
        }
    }

    public void ShowTranscript()
    {
        transcriptImage.gameObject.SetActive(true);
        isTranscriptShown = true;
    }

    public void HideTranscript()
    {
        transcriptImage.gameObject.SetActive(false);
        isTranscriptShown = false;
    }
}
