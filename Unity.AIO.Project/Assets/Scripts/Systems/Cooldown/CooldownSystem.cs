/*--------------------------------------
Unity All-in-One Project
+---------------------------------------
Author: Quan Nguyen
Date:   11/7/22
--------------------------------------*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns.Singleton;

namespace Systems.Cooldown
{
    /// <summary>
    /// MonoBehaviour class to add to any object that wants to use the cooldown system
    /// </summary>
    public class CooldownSystem : LazySingleton<CooldownSystem>
    {
        #region Fields
        private List<CooldownData> _cooldowns = new List<CooldownData>();
        #endregion

        #region Unity Methods

        void Update() => TickCooldowns();

        #endregion

        public void PutOnCooldown(IHasCooldown cooldown)
        {
            _cooldowns.Add(new CooldownData(cooldown));
        }

        public bool IsOnCooldown(string id)
        {
            foreach (var cooldown in _cooldowns)
            {
                if (cooldown.Id == id) return true;
            }

            return false;
        }

        public float GetRemainingDuration(string id)
        {
            foreach (var cooldown in _cooldowns)
            {
                if (cooldown.Id != id) continue;

                return cooldown.RemainingTime;
            }

            return 0f; //returns 0 when there is no cooldown in the list meaning that the cooldown is 0
        }

        private void TickCooldowns()
        {
            float deltaTime = Time.deltaTime;
            
            for (int i = _cooldowns.Count - 1; i >= 0; i--)
            {
                if (!_cooldowns[i].TryTick(deltaTime))
                    _cooldowns.RemoveAt(i);
            }
        }
    }

    public class CooldownData
    {
        public CooldownData(IHasCooldown cooldown)
        {
            Id = cooldown.Id;
            RemainingTime = cooldown.CooldownDuration;
        }
        
        public string Id { get; }
        public float RemainingTime { get; private set; }

        /// <summary>
        /// Try to tick the remaining time of the cooldown.
        /// </summary>
        /// <param name="deltaTime">Last frametime.</param>
        /// <returns>True when the the cooldown can still be ticked (there is remaining time). False when the cooldown have reached 0.</returns>
        public bool TryTick(float deltaTime)
        {
            RemainingTime = Mathf.Max(RemainingTime - deltaTime, 0f); //clamps the remaining time at 0 if it ever goes below that.

            return RemainingTime != 0f;
        }
    }
}