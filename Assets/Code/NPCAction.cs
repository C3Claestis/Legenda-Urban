using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] GameObject text_action;
    [SerializeField] float range = 1f;
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] float timeInterval = 5f;
    void Start()
    {
        // Pastikan text_action dalam keadaan tidak aktif di awal
        text_action.SetActive(false);
        // Mulai mengacak teks setiap 5 detik
        InvokeRepeating("ShowRandomSaying", 0f, timeInterval);
    }

    void Update()
    {
        // Cek jarak antara NPC dan player
        float distance = Vector3.Distance(transform.position, player.position);

        // Jika jaraknya lebih kecil atau sama dengan range, aktifkan text_action
        if (distance <= range)
        {
            text_action.SetActive(true);
        }
        else
        {
            // Jika jaraknya lebih besar dari range, nonaktifkan text_action
            text_action.SetActive(false);
        }
    }

    // Metode untuk mengacak dan menampilkan teks
    void ShowRandomSaying()
    {
        int randomIndex = UnityEngine.Random.Range(0, saying.Length);
        textMeshProUGUI.text = saying[randomIndex];
    }
    // Menampilkan radius di scene view (hanya untuk visualisasi)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
