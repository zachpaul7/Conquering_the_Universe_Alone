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
        options.Clear(); // 옵션 리스트 초기화
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


    void AssignOptionsToButtons() // 버튼의 개수에 맞게 옵션을 무작위로 설정
    {
        List<string> randomizedOptions = new List<string>(options);
        Shuffle(randomizedOptions);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].onClick.RemoveAllListeners();

            if (i < randomizedOptions.Count) // 버튼의 개수가 옵션의 개수만큼 있을 때만 실행
            {
                string option = randomizedOptions[i];
                buttons[i].gameObject.SetActive(true);
                buttons[i].onClick.AddListener(() => Upgrade(option));

                Image buttonImage = buttons[i].GetComponent<Image>();  // 옵션의 이미지 받아오기
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
                // 옵션 A에 대한 기능 구현
                break;
            case "Option B":
                // 옵션 B에 대한 기능 구현
                break;
            case "Option C":
                // 옵션 C에 대한 기능 구현
                break;
            case "Option D":

                break;
            case "Option E":
                
                break;
            case "Option F":
                // 옵션 F에 대한 기능 구현
                break;
            case "Option A1":
                // 옵션 A1에 대한 기능 구현
                break;
            case "Option B1":
                // 옵션 B1에 대한 기능 구현
                break;
            case "Option C1":
                // 옵션 C1에 대한 기능 구현
                break;
            case "Option A2":
                // 옵션 A2에 대한 기능 구현
                break;
            case "Option B2":
                // 옵션 B2에 대한 기능 구현
                break;
            case "Option C2":
                // 옵션 C2에 대한 기능 구현
                break;
        }
        selectedOptions.Add(option);
        UpdateOptions();
       
        upgradePanel.SetActive(false); // 선택시 업그레이드 판넬 비활성화
    }

    void Shuffle(List<string> list) // 리스트 내의 항목을 랜덤으로 설정
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
