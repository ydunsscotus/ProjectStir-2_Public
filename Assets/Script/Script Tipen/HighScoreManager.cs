using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

/*
 * next thing to do adalah buat listnya 10 doang dan kalo masuk list play animasi or something.
 */
public class HighScoreManager : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    private Transform yourHighScore;

    public WinHandler WinHandler;
    
    //ini buat ngatur gap antara score
    public float templateheight = 50f;
    public float time = 0f;
    //buat ngatur Total HighScore yang keluar
    public int JumlahHS = 5;
    public string nama;

    [Header("YoursHighScore")]
    public int yourpos;
    public float yourscore;
    public string yourname;
    private void Awake()
    {
        //Debug.Log("Hadir");
        
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");
        

        entryTemplate.gameObject.SetActive(false);



        //AddHighscoreEntry(30f, "Steven");
        ////masukin ke leaderboard
        time = WinHandler.totalwaktu;
        nama = WinHandler.nama;
        //AddHighscoreEntry(time, nama);
        if (WinHandler.menang == true)
        {
            AddHighscoreEntry(time, nama);
        }
        else
        {
            DisplayYourHighScore(time, nama);
        }

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        //Debug.Log(jsonString + "json");
        HighScore highscore = JsonUtility.FromJson<HighScore>(jsonString);


        //Sorting
        for (int i = 0; i < highscore.highscoreEntryList.Count; i++)
        {
            for (int j = 0; j < highscore.highscoreEntryList.Count; j++)
            {
                if (highscore.highscoreEntryList[j].score > highscore.highscoreEntryList[i].score)
                {
                    //Swap
                    HighScoreEntry temp = highscore.highscoreEntryList[i];
                    highscore.highscoreEntryList[i] = highscore.highscoreEntryList[j];
                    highscore.highscoreEntryList[j] = temp;
                }

            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighScoreEntry highScoreEntry in highscore.highscoreEntryList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highscoreEntryTransformList);
        }

        //Debug.Log(PlayerPrefs.GetString("highscoreTable"));


        ////ini buat pas awal doang (udah ga guna)
        //HighScore highscore = new HighScore { highscoreEntryList = highscoreEntryList };
        //string json = JsonUtility.ToJson(highscore);
        //PlayerPrefs.SetString("highscoreTable", json);
        //PlayerPrefs.Save();
        //Debug.Log(PlayerPrefs.GetString("highscoreTable"));

    }
    public void AddHighscoreEntry(float score, string name)
    {
        DisplayYourHighScore(score, name);

        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name};

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        HighScore highscore = JsonUtility.FromJson<HighScore>(jsonString);

        //Debug.Log(highScoreEntry);
        if (highscore != null)
        {
            highscore.highscoreEntryList.Add(highScoreEntry);
        }
        else
        {   
            
            highscoreEntryList = new List<HighScoreEntry>()
            {
                new HighScoreEntry{score = score, name = name}
            };

            highscore = new HighScore {highscoreEntryList = highscoreEntryList};
        }

        
        string json = JsonUtility.ToJson(highscore);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
    public void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform Container, List<Transform> transformList)
    {
        //ini buat bikin scorenya
        if(transformList.Count <= (JumlahHS-1))
        {
            Transform entryTransform = Instantiate(entryTemplate, Container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateheight * transformList.Count);

            entryTransform.gameObject.SetActive(true);

            int rank = transformList.Count + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "th"; break;

                case 1: rankString = "1st"; break;
                case 2: rankString = "2nd"; break;
                case 3: rankString = "3rd"; break;
            }

            entryTransform.Find("PosText").GetComponent<TMP_Text>().text = rankString;

            float score = highScoreEntry.score;
            entryTransform.Find("TimerText").GetComponent<TMP_Text>().text = FormatTime(score);

            string name = highScoreEntry.name;
            entryTransform.Find("NameText").GetComponent<TMP_Text>().text = name;

            transformList.Add(entryTransform);
        }
        
    }

    public class HighScore
    {
        public List<HighScoreEntry> highscoreEntryList;
    }
    

    [System.Serializable]
    public class HighScoreEntry
    {
        public float score;
        public string name;
    }

    public string FormatTime(float totalsecond)
    {
        int minute = Mathf.FloorToInt(totalsecond / 60);
        int second = Mathf.FloorToInt(totalsecond % 60);
        int milisecond = Mathf.FloorToInt((totalsecond * 100) % 100);

        return string.Format("{0:D2}:{1:D2}.{2:D2}", minute, second, milisecond);
    }

    public void DisplayYourHighScore(float score, string name)
    {
        yourHighScore = transform.Find("YourHighScore");

        yourHighScore.Find("YourTime").GetComponent<TMP_Text>().text = FormatTime(score);
    }
}
