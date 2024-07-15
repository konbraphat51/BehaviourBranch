using System.Collections.Generic;
using UnityEngine;

namespace BehaviourBranch
{
    /// <summary>
    /// Stop the time when needed
    /// </summary>
    public class TimeManager
    {
        /// <summary>
        /// Singleton instance
        /// </summary>
        public static TimeManager instance = new TimeManager();

        /// <summary>
        /// Is currently stopping
        /// </summary>
        public bool isStopping => stoppers.Count > 0;

        /// <summary>
        /// IDs that now stopping the time
        /// </summary>
        protected List<int> stoppers = new List<int>();

        /// <summary>
        /// Stops the time for the given stopper
        ///
        /// The time will be stopped until the stopper is removed
        /// </summary>
        /// <returns>stopper's id</returns>
        public int AddStopper()
        {
            int id = GenerateId();

            stoppers.Add(id);

            ControlTimeScale();

            return id;
        }

        /// <summary>
        /// Remove the stopper from the list
        /// </summary>
        /// <param name="id">id got from AddStopper()</param>
        /// <returns>if succeeded or not</returns>
        public bool RemoveStopper(int id)
        {
            bool result = stoppers.Remove(id);

            ControlTimeScale();

            return result;
        }

        /// <summary>
        /// Force the time moving by remove all stoppers
        /// </summary>
        public void Reset()
        {
            stoppers.Clear();

            ControlTimeScale();
        }

        private int GenerateId()
        {
            while (true)
            {
                int id = Random.Range(0, int.MaxValue);
                if (!stoppers.Contains(id))
                {
                    return id;
                }
            }
        }

        protected virtual void ControlTimeScale()
        {
            // if there is stopper...
            if ((stoppers.Count > 0) && (Time.timeScale != 0))
            {
                //...stop
                Time.timeScale = 0;
            }
            else if ((stoppers.Count == 0) && (Time.timeScale == 0))
            {
                Time.timeScale = 1;
            }
        }
    }
}
