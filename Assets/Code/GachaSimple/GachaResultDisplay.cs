using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GachaResultDisplay : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Animator animator;

    public void DisplayGachaResults(List<GachaItem> gachaResults)
    {
        StartCoroutine(ShowGachaResults(gachaResults));
    }

    private IEnumerator ShowGachaResults(List<GachaItem> gachaResults)
    {
        yield return new WaitForSeconds(8f); // Tunggu 8 detik sebelum menampilkan hasil pertama

        foreach (var item in gachaResults)
        {
            // Update UI dengan hasil gacha saat ini
            itemIcon.sprite = item.image;
            itemName.text = item.itemName;
            itemBackground.color = item.color;

            // Mulai animasi
            animator.SetTrigger("ShowResult");

            // Tunggu sampai pengguna menekan tombol 'space' untuk melanjutkan ke hasil berikutnya
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            // Tunggu sampai animasi selesai sebelum menampilkan item berikutnya
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
