using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog System/Dialog")]
public class Dialog : ScriptableObject
{
    public DialogEntry[] dialogEntries; // Array untuk menyimpan semua entri dialog

    [System.Serializable]
    public class DialogEntry
    {
        public string[] text_dialog; // Kalimat-kalimat dalam dialog ini
        public bool hasChoices; // Menandakan apakah entri ini memiliki pilihan atau tidak
        public DialogChoice[] choices; // Pilihan yang tersedia di akhir dialog, hanya digunakan jika hasChoices = true
    }

    [System.Serializable]
    public class DialogChoice
    {
        public string choiceText; // Teks yang ditampilkan pada tombol pilihan
        public Dialog nextDialog; // Dialog berikutnya (ScriptableObject) yang akan digunakan
    }
}
