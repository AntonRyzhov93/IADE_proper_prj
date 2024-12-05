using System;
using UnityEngine;

namespace DTT.DailyRewards.Demo
{
    /// <summary>
    /// Handles the display for the daily reward pop up demo.
    /// </summary>
    public class PopupRewardHandler : MonoBehaviour
    {
        /// <summary>
        /// Reference to the daily reward.
        /// </summary>
        [SerializeField]
        private DailyReward _dailyReward;

        /// <summary>
        /// Handles claiming available rewards.
        /// </summary>
        [SerializeField]
        private UnlockRewardButton _claimButton;

        /// <summary>
        /// Blocks displaying each daily reward in order.
        /// </summary>
        [SerializeField]
        private PopupReward[] _rewardBlocks;

        /// <summary>
        /// Daily score increment.
        /// </summary>
        private const int DAILY_SCORE = 50;

        /// <summary>
        /// Intervals at which red gems should appear.
        /// </summary>
        private const int RED_INTERVALS = 5;

        /// <summary>
        /// A lock on updating the display, to stop interference during
        /// it's asynchronous callback.
        /// </summary>
        private bool _updatingDisplayLock;

        // Start is called before the first frame update
        void Awake()
        {
            for (int i = 0; i < _rewardBlocks.Length; i++)
                _rewardBlocks[i].Initialize(i + 1, DAILY_SCORE + i * DAILY_SCORE, (i + 1) % RED_INTERVALS == 0);

            _claimButton.Initialize(_dailyReward.RewardInstance);
        }

        
        /// <summary>
        /// Handles updating the block visuals.
        /// </summary>
        private void Update() => UpdateBlocks();

        /// <summary>
        /// Updates the reward blocks.
        /// </summary>
        private void UpdateBlocks()
        {
            if (_updatingDisplayLock)
                return;

            _updatingDisplayLock = true;

            _dailyReward.RewardInstance.CheckAvailableReward(rewardCallback =>
            {
                if (!_dailyReward || rewardCallback.CurrentCallbackStatus == RewardCallbackStatus.PENDING)
                    return;

                _updatingDisplayLock = false;

                // Gets the display time.
                DateTime dateTime = new DateTime();
                if (rewardCallback.NextAvailableReward > rewardCallback.CurrentUnixTime)
                {
                    dateTime = dateTime.AddSeconds(rewardCallback.NextAvailableReward - rewardCallback.CurrentUnixTime).ToLocalTime();
                    dateTime = dateTime.AddHours(-1);
                }

                _claimButton.UpdateReward(rewardCallback.Reward, dateTime);

                Reward[] rewards = _dailyReward.RewardInstance.EarnedRewards(10000);

                int baseAmount = Mathf.FloorToInt(rewards.Length / (float)_rewardBlocks.Length);

                // Updates all blocks.
                for (int i = 0; i < _rewardBlocks.Length; i++)
                {
                    int rewardIndex = baseAmount * _rewardBlocks.Length + i;

                    Reward reward = null;
                    if (rewardIndex == rewards.Length)
                        reward = rewardCallback.Reward;
                    else if (rewardIndex < rewards.Length)
                        reward = rewards[rewardIndex];

                    _rewardBlocks[i].UpdateReward(reward);
                }
            });
        }
    }
}
