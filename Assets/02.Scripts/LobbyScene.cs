using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    // ����� ������ ���� �����ϰų�(ȸ������), ����� �����͸� �о�(�α���)
    // ����� �Է°� ��ġ�Ѵ��� �˻��Ѵ�.

    public TMP_InputField IDInputField;  // ���̵� �Է�â
    public TMP_InputField PasswordInputField;  // ��й�ȣ �Է�â
    public TextMeshProUGUI NotifyTextUI;  // �˸� �ؽ�Ʈ

    private void Start()
    {
        IDInputField.text       = string.Empty;
        PasswordInputField.text = string.Empty;
        NotifyTextUI.text       = string.Empty;
    }
    public void OnClickRegisterButton()
    {
        string id = IDInputField.text;
        string password = PasswordInputField.text;
        if (id == string.Empty || password == string.Empty)
        {
            NotifyTextUI.text = "���̵�� ��й�ȣ�� �Է��� �ּ���";
            return;
        }
        // 1. �̹� ���� �������� ȸ�������� �Ǿ� �ִ� ���
        if(PlayerPrefs.HasKey(id))
        {
            NotifyTextUI.text = "�̹� �����ϴ� �����Դϴ�";
        }
        // 2. ȸ�����Կ� �����ϴ� ���
        else
        {
            NotifyTextUI.text = "ȸ�������� �Ϸ��߽��ϴ�.";
            PlayerPrefs.SetString(id, password);
        }
        IDInputField.text = string.Empty;
        PasswordInputField.text = string.Empty;
    }

    public void OnClickLogInButton()
    {
        // 0. ���̵� �Ǵ� ��й�ȣ �Է�X -> "���̵�� ��й�ȣ�� ��Ȯ�ϰ� �Է����ּ���."
        // 1. ���� ���̵�                -> "���̵� Ȯ�����ּ���."
        // 2. Ʋ�� ��й�ȣ              -> "��й�ȣ�� Ȯ�����ּ���."
        // 3. �α��μ���                 -> ���� ������ �̵�
    }
}
