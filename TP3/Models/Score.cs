﻿namespace TP3.Models
{
    public class Score
    {
        public int Id { get; set; }

        public string Pseudo { get; set; }

        public string Date { get; set; }

        public string TimeInSeconds { get; set; }

        public int ScoreValue { get; set; }

        public bool IsPublic { get; set; }

        public virtual User? User { get; set; }

    }
}
