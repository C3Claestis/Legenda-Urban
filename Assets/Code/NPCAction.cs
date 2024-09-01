using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class NPCAction : MonoBehaviour
{
    string[] saying = new string[] {
        "Kenapa hidupku begini-begini saja...",
        "Kadang aku merasa sendirian di tengah keramaian...",
        "Harapan tinggal harapan, kenyataan tak pernah seindah itu...",
        "Aku berusaha tersenyum, meski hati hancur berkeping-keping...",
        "Mimpi-mimpi itu hanya tinggal bayangan yang memudar...",
        "Semua orang pergi, aku ditinggalkan sendiri...",
        "Mengapa selalu aku yang harus mengalah?",
        "Cinta tak pernah berpihak padaku...",
        "Mungkin aku memang ditakdirkan sendiri...",
        "Seandainya waktu bisa diputar kembali..."
    };
    float timeActive = 5f;
    bool isCanActive = true;
    [SerializeField] Animator animator;
    [SerializeField] GameObject text_action;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    Coroutine randomShowCoroutine;

    void Start()
    {
        animator.SetInteger("arms", 5);
        animator.SetInteger("legs", 5);
        text_action.SetActive(false);
        randomShowCoroutine = StartCoroutine(RandomShowInterval());
    }

    void ShowRandomSaying()
    {
        int randomIndex = UnityEngine.Random.Range(0, saying.Length);
        textMeshProUGUI.text = saying[randomIndex];
        text_action.SetActive(true);
        StartCoroutine(HideTextActionAfterTime());
    }

    IEnumerator RandomShowInterval()
    {
        while (true)
        {
           float randomInterval = UnityEngine.Random.Range(1f, 10f);
           float elapsedTime = 0f;
            
            while (elapsedTime < randomInterval)
            {
                if (!isCanActive)
                {
                    yield break; // Menghentikan coroutine
                }
                elapsedTime += Time.deltaTime;
                yield return null; // Periksa kondisi setiap frame
            }

            ShowRandomSaying();
        }
    }

    IEnumerator HideTextActionAfterTime()
    {
        yield return new WaitForSeconds(timeActive);
        text_action.SetActive(false);
    }

    public void SetActive(bool active)
    {
        isCanActive = active;

        if (isCanActive)
        {
            // Memulai ulang coroutine
            randomShowCoroutine = StartCoroutine(RandomShowInterval());
        }
        else
        {
            // Menghentikan coroutine
            if (randomShowCoroutine != null)
            {
                StopCoroutine(randomShowCoroutine);
            }
        }
    }
}
