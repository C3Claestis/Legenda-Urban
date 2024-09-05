using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class DialogManager : MonoBehaviour
{
    [Header("COMPONENT DIALOG")]
    [SerializeField] private NPCTalkManager nPCTalkManager;
    [SerializeField] private Text dialogText;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private Button[] choiceButtons;

    [Header("ISI DARI SCRIPTABLEOBJECT DIALOG")]
    [SerializeField] private Dialog initialDialogData; // ScriptableObject dialog awal
    private Dialog dialogData; // ScriptableObject dialog saat ini

    private Queue<string> sentences;
    private Dialog.DialogEntry currentDialogEntry;
    private int currentDialogIndex;

    void Start()
    {
        sentences = new Queue<string>();
        dialogData = initialDialogData; // Setel dialogData awal
        nextButton.onClick.AddListener(DisplayNextSentence);
        choicesPanel.SetActive(false);
        StartDialog(dialogData, 0); // Selalu mulai dari indeks 0
    }

    public void StartDialog(Dialog dialog, int entryIndex)
    {
        Debug.Log("Starting dialog");
        dialogData = dialog;
        currentDialogIndex = entryIndex;
        currentDialogEntry = dialogData.dialogEntries[currentDialogIndex];
        sentences.Clear();

        foreach (string sentence in currentDialogEntry.text_dialog)
        {
            sentences.Enqueue(sentence);
        }

        // Tampilkan kalimat pertama dari dialog
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (currentDialogEntry.hasChoices && currentDialogEntry.choices.Length > 0)
            {
                ShowChoices(currentDialogEntry.choices);
            }
            else
            {
                EndDialog();
            }
            return;
        }

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;
    }

    private void ShowChoices(Dialog.DialogChoice[] choices)
    {
        choicesPanel.SetActive(true);
        nextButton.gameObject.SetActive(false);

        // Reset tombol pilihan dan listener
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Length)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = choices[i].choiceText;
                int index = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(choices[index]));
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnChoiceSelected(Dialog.DialogChoice choice)
    {
        choicesPanel.SetActive(false);
        nextButton.gameObject.SetActive(true);

        // Mulai dialog baru berdasarkan pilihan yang dipilih
        StartDialog(choice.nextDialog, 0);

        // Reset tombol pilihan
        foreach (var button in choiceButtons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
    }

    private void EndDialog()
    {
        dialogText.text = "";
        choicesPanel.SetActive(false);

        // Reset ke ScriptableObject dialog awal dan index awal
        dialogData = initialDialogData;
        currentDialogIndex = 0;

        // Mulai ulang dialog dari awal
        StartDialog(dialogData, currentDialogIndex);

        nPCTalkManager.RandomAgain();
        Debug.Log("End of dialog");
    }
}
