using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject upgradePanel;
    public List<string> options;
    public List<Button> buttons;
    private HashSet<string> selectedOptions;



    void Start()
    {
        selectedOptions = new HashSet<string>();
        UpdateOptions();
    }

    void UpdateOptions()
    {
        options.Clear(); // �ɼ� ����Ʈ �ʱ�ȭ
        options.AddRange(new[] { "Option D", "Option E", "Option F" });

        // Option A
        if (!selectedOptions.Contains("Option A"))
        {
            options.Add("Option A");
        }
        else if (!selectedOptions.Contains("Option A1"))
        {
            options.Add("Option A1");
        }
        else
        {
            options.Add("Option A2");
        }

        // Option B
        if (!selectedOptions.Contains("Option B"))
        {
            options.Add("Option B");
        }
        else if (!selectedOptions.Contains("Option B1"))
        {
            options.Add("Option B1");
        }
        else
        {
            options.Add("Option B2");
        }

        // Option C
        if (!selectedOptions.Contains("Option C"))
        {
            options.Add("Option C");
        }
        else if (!selectedOptions.Contains("Option C1"))
        {
            options.Add("Option C1");
        }
        else
        {
            options.Add("Option C2");
        }

        AssignOptionsToButtons();
    }


    void AssignOptionsToButtons() // ��ư�� ������ �°� �ɼ��� �������� ����
    {
        List<string> randomizedOptions = new List<string>(options);
        Shuffle(randomizedOptions);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].onClick.RemoveAllListeners();

            if (i < randomizedOptions.Count) // ��ư�� ������ �ɼ��� ������ŭ ���� ���� ����
            {
                string option = randomizedOptions[i];
                buttons[i].gameObject.SetActive(true);
                buttons[i].onClick.AddListener(() => Upgrade(option));

                Image buttonImage = buttons[i].GetComponent<Image>();  // �ɼ��� �̹��� �޾ƿ���
                Texture2D imageAsset = Resources.Load<Texture2D>($"Images/{option}");
                buttonImage.sprite = Sprite.Create(imageAsset, new Rect(0, 0, imageAsset.width, imageAsset.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                buttons[i].gameObject.SetActive(false);
            }
        }
    }

    void Upgrade(string option)
    {
        switch (option)
        {
            case "Option A":
                // �ɼ� A�� ���� ��� ����
                break;
            case "Option B":
                // �ɼ� B�� ���� ��� ����
                break;
            case "Option C":
                // �ɼ� C�� ���� ��� ����
                break;
            case "Option D":

                break;
            case "Option E":
                
                break;
            case "Option F":
                // �ɼ� F�� ���� ��� ����
                break;
            case "Option A1":
                // �ɼ� A1�� ���� ��� ����
                break;
            case "Option B1":
                // �ɼ� B1�� ���� ��� ����
                break;
            case "Option C1":
                // �ɼ� C1�� ���� ��� ����
                break;
            case "Option A2":
                // �ɼ� A2�� ���� ��� ����
                break;
            case "Option B2":
                // �ɼ� B2�� ���� ��� ����
                break;
            case "Option C2":
                // �ɼ� C2�� ���� ��� ����
                break;
        }
        selectedOptions.Add(option);
        UpdateOptions();
       
        upgradePanel.SetActive(false); // ���ý� ���׷��̵� �ǳ� ��Ȱ��ȭ
    }

    void Shuffle(List<string> list) // ����Ʈ ���� �׸��� �������� ����
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
