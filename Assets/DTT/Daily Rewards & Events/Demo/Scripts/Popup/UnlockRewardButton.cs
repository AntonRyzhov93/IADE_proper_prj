using System;
using UnityEngine;
using UnityEngine.UI;

namespace DTT.DailyRewards.Demo
{
    /// <summary>
    /// Handles the display for the claim button in the pop up demo.
    /// </summary>
    public class UnlockRewardButton : MonoBehaviour
    {
        /// <summary>
        /// Button component that will handle claiming rewards.
        /// </summary>
        [SerializeField]
        private Button _claimButton;

        /// <summary>
        /// Text component displaying the remaining time until the next reward.
        /// </summary>
        [SerializeField]
        private Text _timeRemaining;

        /// <summary>
        /// Display for when a reward can be claimed.
        /// </summary>
        [SerializeField]
        private GameObject _claimButtonVisual;

        /// <summary>
        /// Display for when a reward can't be claimed.
        /// </summary>
        [SerializeField]
        private GameObject _lockedButtonVisual;

        /// <summary>
        /// The reward being displayed by the block.
        /// </summary>
        private Reward _reward;

        /// <summary>
        /// Reference to the daily reward.
        /// </summary>
        private DailyRewardSO _dailyReward;

        /// <summary>
        /// Removes the onClick listener.
        /// </summary>
        private void OnDestroy() => _claimButton.onClick.RemoveListener(ClaimReward);

        /// <summary>
        /// Initializes the button.
        /// </summary>
        /// <param name="dailyReward">Daily reward to handle.</param>
        public void Initialize(DailyRewardSO dailyReward)
        {
            _dailyReward = dailyReward;
            _claimButton.onClick.AddListener(ClaimReward);
        }

        /// <summary>
        /// Updates the visuals for the claim button.
        /// </summary>
        /// <param name="reward">Current reward.</param>
        /// <param name="time">Time until claim.</param>
        public void UpdateReward(Reward reward, DateTime time)
        {
            if (reward == null)
                return;

            _timeRemaining.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                time.Hour,
                time.Minute,
                time.Second);

            //If it's the same reward, no need to update.
            if (_reward != null && reward != null && _reward.RewardCount == reward.RewardCount && _reward.RewardStatus == reward.RewardStatus)
                return;

            if (reward.RewardStatus == RewardStatus.AVAILABLE)
            {
                _lockedButtonVisual.SetActive(false);
                _claimButtonVisual.SetActive(true);
            }
            else
            {
                _claimButtonVisual.SetActive(false);
                _lockedButtonVisual.SetActive(true);
            }
        }

        /// <summary>
        /// Claims the available reward.
        /// </summary>
        private void ClaimReward()
        {
            _dailyReward.ClaimReward(result =>
            {
                switch (result.CurrentCallbackStatus)
                {
                    case RewardCallbackStatus.PENDING:
                        Debug.Log($"Reward {_dailyReward.name} is being claimed.");
                        break;
                    case RewardCallbackStatus.COMPLETE:
                        Debug.Log($"Reward {_dailyReward.name} successfully claimed.");
                        break;
                    case RewardCallbackStatus.NO_REWARD_AVAILABLE:
                        Debug.Log($"Reward {_dailyReward.name} is not available to be claimed.");
                        break;
                    case RewardCallbackStatus.ALL_REWARDS_EARNED:
                        Debug.Log($"Reward {_dailyReward.name} has had all rewards claimed.");
                        break;
                    case RewardCallbackStatus.COULD_NOT_VALIDATE_TIME:
                        Debug.Log($"Reward {_dailyReward.name} could not validate the time.");
                        break;
                    case RewardCallbackStatus.COULD_NOT_SAVE_REWARD:
                        Debug.Log($"Reward {_dailyReward.name} could not save the reward.");
                        break;
                    case RewardCallbackStatus.REWARDS_NOT_LOADED:
                        Debug.Log($"Reward {_dailyReward.name} has not yet loaded the rewards.");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }
    }
}
