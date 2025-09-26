// File: Models/Entities.cs
// SQLite öznitelikleriyle sade modeller
using System;
using System.Collections.Generic;
using SQLite;

namespace YKSTayfa.Models
{
    public enum RoleLevel : byte { User = 0, Admin25 = 25, Admin50 = 50, Admin75 = 75, Admin100 = 100 }
    public enum BanReason : byte { None = 0, Profanity = 1, Spam = 2, Abuse = 3, Other = 4 }
    public enum QuestionType : byte { SingleChoice = 0, MultiChoice = 1, Numeric = 2 }
    public enum LessonType : byte { TYT = 0, AYT = 1 }

    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement] public int RowId { get; set; }
        [Indexed(Unique = true)] public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public RoleLevel Role { get; set; }
        public bool IsBanned { get; set; }
        public BanReason BanReason { get; set; }
        public DateTime? BanUntilUtc { get; set; }
        public int Score { get; set; }
        public int Coins { get; set; }
        public string FrameName { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
            Username = string.Empty; Email = string.Empty; AvatarUrl = null;
            Role = RoleLevel.User; IsBanned = false; BanReason = BanReason.None; BanUntilUtc = null;
            Score = 0; Coins = 0; FrameName = null;
        }
    }

    [Table("ChatMessages")]
    public class ChatMessage
    {
        [PrimaryKey, AutoIncrement] public int RowId { get; set; }
        [Indexed] public Guid MessageId { get; set; }
        [Indexed] public Guid? ReplyToId { get; set; }
        [Indexed] public Guid AuthorId { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime SentUtc { get; set; }
        public bool IsDeleted { get; set; }

        public ChatMessage()
        {
            MessageId = Guid.NewGuid();
            ReplyToId = null; AuthorId = Guid.Empty;
            Text = string.Empty; Image = null;
            SentUtc = DateTime.UtcNow; IsDeleted = false;
        }
    }

    [Table("AdBanners")]
    public class AdBanner
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        [Indexed] public string Position { get; set; }
        public string Image { get; set; }
        public string TargetUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartUtc { get; set; }
        public DateTime? EndUtc { get; set; }

        public AdBanner()
        {
            Position = "home_top"; Image = "banner.png"; TargetUrl = null;
            IsActive = true; StartUtc = null; EndUtc = null;
        }
    }

    [Table("ExamPapers")]
    public class ExamPaper
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public string Title { get; set; }
        public LessonType Lesson { get; set; }
        public int TotalQuestions { get; set; }
        public int DurationMinutes { get; set; }
        public DateTime CreatedUtc { get; set; }

        public ExamPaper()
        {
            Title = "Deneme"; Lesson = LessonType.TYT;
            TotalQuestions = 0; DurationMinutes = 120; CreatedUtc = DateTime.UtcNow;
        }
    }

    [Table("HealthChecks")]
    public class HealthCheckResult
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public bool DatabaseOk { get; set; }
        public bool NetworkOk { get; set; }
        public string Notes { get; set; }
        public DateTime CheckedUtc { get; set; }

        public HealthCheckResult()
        {
            DatabaseOk = true; NetworkOk = true; Notes = null; CheckedUtc = DateTime.UtcNow;
        }
    }

    [Table("AppSettings")]
    public class AppSettings
    {
        [PrimaryKey] public int Id { get; set; }
        public string ApiBaseUrl { get; set; }
        public bool AdsEnabled { get; set; }
        public bool ProfanityFilter { get; set; }
        public int DefaultCountdownDays { get; set; }

        public AppSettings()
        {
            Id = 1;
            ApiBaseUrl = "https://localhost:7159";
            AdsEnabled = true; ProfanityFilter = true; DefaultCountdownDays = 250;
        }
    }
}
