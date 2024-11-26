using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombCooldownUI : MonoBehaviour
{
    public PlayerController playerController; // PlayerController ������
    public Image cooldownFill; // ��ȴ������䲿��

    private void Start()
    {
        // ��̬���� PlayerController
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
            if (playerController == null)
            {
                Debug.LogError("PlayerController reference is missing and could not be found dynamically.");
                return;
            }
        }

        // ��̬������ȴ��
        if (cooldownFill == null)
        {
            cooldownFill = transform.Find("CoolDownFill")?.GetComponent<Image>();
            if (cooldownFill == null)
            {
                Debug.LogError("CooldownFill reference is missing in BombCooldownUI.");
                return;
            }
        }
    }

    private void Update()
    {
        if (playerController == null || cooldownFill == null)
        {
            return; // �������δ���ã���ִֹͣ��
        }

        // ��ȡ PlayerController �е���ȴ״̬�ͼ�ʱ��
        bool isCooldown = GetPrivateField<bool>(playerController, "isBombCooldown");
        float cooldownTimer = GetPrivateField<float>(playerController, "bombCooldownTimer");
        float cooldownDuration = playerController.bombCooldownDuration;

        if (isCooldown)
        {
            // ������ȴ��
            cooldownFill.fillAmount = 1 - (cooldownTimer / cooldownDuration);
        }
        else
        {
            // ��ȴ��ɣ��������ȴ��
            cooldownFill.fillAmount = 1;
        }
    }

    // ͨ�������ȡ˽���ֶε�ֵ
    private T GetPrivateField<T>(object obj, string fieldName)
    {
        var fieldInfo = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (fieldInfo == null)
        {
            Debug.LogError($"Field '{fieldName}' not found in {obj.GetType()}.");
            return default;
        }
        return (T)fieldInfo.GetValue(obj);
    }
}
