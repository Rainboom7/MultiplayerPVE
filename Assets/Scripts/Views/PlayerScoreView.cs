using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class PlayerScoreView : MonoBehaviour
    {
        public string Name;
        public int Score;
        [SerializeField]
        private Text _nameText;
        [SerializeField]
        private Text _scoreText;
        public void UpdateView() {
            _nameText.text = Name;
            if (Score == -1)
                _scoreText.text = "";
            else
                _scoreText.text = Score.ToString();

        }
    }
}
