using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        
        string id = IDInputField.text;
        string pw = PasswordInputField.text;
        // 0. ���̵� �Ǵ� ��й�ȣ �Է�X -> "���̵�� ��й�ȣ�� ��Ȯ�ϰ� �Է����ּ���."
        if (id == string.Empty || pw == string.Empty)
        {
            NotifyTextUI.text = "���̵�� ��й�ȣ�� �Է��� �ּ���";
           
        }
        // 1. ���� ���̵�                -> "���̵� Ȯ�����ּ���."
        if (!PlayerPrefs.HasKey(id))
        {
            NotifyTextUI.text = "���̵�� ��й�ȣ�� Ȯ�����ּ���.";
        }
        // 2. Ʋ�� ��й�ȣ              -> "��й�ȣ�� Ȯ�����ּ���."
        if (PlayerPrefs.GetString(id) != pw)
        {
            NotifyTextUI.text = "���̵�� ��й�ȣ�� Ȯ�����ּ���.";
        }
        // 3. �α��μ���                 -> ���� ������ �̵�
        else
        {
            SceneManager.LoadScene("MainScene");

        }
    }
}
