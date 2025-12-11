using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        //public Text scoreText; // Reference to the UI Text component
        private int score = 0;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void AddScore(int amount)
        {
            score += amount;
            //scoreText.text = "Score: " + score.ToString();
        }
    }