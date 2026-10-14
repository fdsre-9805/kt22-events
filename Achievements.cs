using System;

namespace AchievementSystemApp
{
    interface IScoreSource
    {
        event EventHandler<(int OldScore, int NewScore)>? ScoreChanged;
        void UnlockAchievement(string achievementName);
    }

    class Player : IScoreSource
    {
        private int score = 0;

        public event EventHandler<(int OldScore, int NewScore)>? ScoreChanged;
        public event EventHandler<string>? AchievementUnlocked;

        public void AddScore(int amount)
        {
            int oldScore = score;
            score += amount;
            ScoreChanged?.Invoke(this, (oldScore, score));
        }

        public void UnlockAchievement(string achievementName)
        {
            AchievementUnlocked?.Invoke(this, achievementName);
        }
    }

    class AchievementTracker
    {
        private IScoreSource scoreSource;
        private int scoreThreshold;

        public AchievementTracker(IScoreSource scoreSource, int scoreThreshold)
        {
            this.scoreSource = scoreSource;
            this.scoreThreshold = scoreThreshold;
            scoreSource.ScoreChanged += OnScoreChanged;
            Console.WriteLine("AchievementTracker подписан на ScoreChanged");
        }

        private void OnScoreChanged(object? sender, (int OldScore, int NewScore) scoreChange)
        {
            if (scoreChange.OldScore <= scoreThreshold && scoreChange.NewScore > scoreThreshold)
                scoreSource.UnlockAchievement($"Больше {scoreThreshold} очков");
        }

        public void Unsubscribe()
        {
            scoreSource.ScoreChanged -= OnScoreChanged;
            Console.WriteLine("AchievementTracker отписан от ScoreChanged");
        }
    }
}
