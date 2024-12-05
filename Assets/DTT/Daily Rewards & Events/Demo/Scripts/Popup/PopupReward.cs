using UnityEngine;
using UnityEngine.UI;

namespace DTT.DailyRewards.Demo
{
    /// <summary>
    /// Handles the display for a reward in the pop up demo.
    /// </summary>
    public class PopupReward : MonoBehaviour
    {
        /// <summary>
        /// Image when the block displays a red gem.
        /// </summary>
        [SerializeField]
        private Sprite _redGem;

        /// <summary>
        /// Color when the block displays a red gem.
        /// </summary>
        [SerializeField]
        private Color _redGemColor;

        /// <summary>
        /// Image component of the block.
        /// </summary>
        [SerializeField]
        private Image _gemImage;

        /// <summary>
        /// Text component displaying the score.
        /// </summary>
        [SerializeField]
        private Text _scoreText;

        /// <summary>
        /// Text component displaying the day.
        /// </summary>
        [SerializeField]
        private Text _dayText;

        /// <summary>
        /// Canvas group handling the fade effect.
        /// </summary>
        [SerializeField]
        private CanvasGroup _fade;

        /// <summary>
        /// Objects active when the reward is unavailable.
        /// </summary>
        [SerializeField]
        private GameObject[] _activeOnUnavailable;

        /// <summary>
        /// Objects active when the reward is available.
        /// </summary>
        [SerializeField]
        private GameObject[] _activeOnAvailable;

        /// <summary>
        /// Objects active when the reward is claimed.
        /// </summary>
        [SerializeField]
        private GameObject[] _activeOnClaimed;

        /// <summary>
        /// The reward being displayed by the block.
        /// </summary>
        private Reward _reward;

        /// <summary>
        /// Initializes the reward block.
        /// </summary>
        /// <param name="day">Day of the reward.</param>
        /// <param name="points">Points of the reward.</param>
        /// <param name="isRed">Whether it should be red.</param>
        public void Initialize(int day, int points, bool isRed)
        {
            if (isRed)
            {
                _scoreText.color = _redGemColor;
                _gemImage.sprite = _redGem;
            }

            _dayText.text = $"Day {day}";
            _scoreText.text = points.ToString();
        }

        /// <summary>
        /// Display a given reward.
        /// </summary>
        /// <param name="newReward">The reward to display.</param>
        public void UpdateReward(Reward newReward)
        {
            //If it's the same reward, no need to update.
            if (_reward != null && newReward != null && _reward.RewardCount == newReward.RewardCount && _reward.RewardStatus == newReward.RewardStatus)
                return;
            _reward = newReward;
            UpdateDisplay();
        }

        /// <summary>
        /// Update the display of the reward
        /// </summary>
        private void UpdateDisplay()
        {
            if (_reward == null)
            {
                _fade.alpha = .5f;
                SetCollectionActive(_activeOnAvailable, false);
                SetCollectionActive(_activeOnClaimed, false);
                SetCollectionActive(_activeOnUnavailable, true);
                return;
            }

            switch (_reward.RewardStatus)
            {
                case RewardStatus.UNAVAILABLE:
                    _fade.alpha = .5f;
                    SetCollectionActive(_activeOnAvailable, false);
                    SetCollectionActive(_activeOnClaimed, false);
                    SetCollectionActive(_activeOnUnavailable, true);
                    break;
                case RewardStatus.AVAILABLE:
                    _fade.alpha = 1;
                    SetCollectionActive(_activeOnUnavailable, false);
                    SetCollectionActive(_activeOnClaimed, false);
                    SetCollectionActive(_activeOnAvailable, true);
                    break;
                case RewardStatus.CLAIMED:
                    _fade.alpha = 1;
                    SetCollectionActive(_activeOnUnavailable, false);
                    SetCollectionActive(_activeOnAvailable, false);
                    SetCollectionActive(_activeOnClaimed, true);
                    break;
            }
        }

        /// <summary>
        /// Sets a given collection of objects active.
        /// </summary>
        /// <param name="gameObjects">Game object collection.</param>
        /// <param name="active">Whether the objects should be set active or not.</param>
        private void SetCollectionActive(GameObject[] gameObjects, bool active)
        {
            foreach (GameObject gameObject in gameObjects)
                gameObject.SetActive(active);
        }
    }
}
