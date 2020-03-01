using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace BusinessMobil.App.Helpers
{
    public class Settings
    {
        public Settings()
        {
        }

        static ISettings Setting => CrossSettings.Current;

        public static string Token
        {
            get => Setting.GetValueOrDefault(nameof(Token),string.Empty);
            set => Setting.AddOrUpdateValue(nameof(Token),value);
        }

        public static string TypeToken
        {
            get => Setting.GetValueOrDefault(nameof(TypeToken), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(TypeToken), value);
        }

        public static string Email
        {
            get => Setting.GetValueOrDefault(nameof(Email), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(Email), value);
        }

        public static string Password
        {
            get => Setting.GetValueOrDefault(nameof(Password), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(Password), value);
        }

        public static string IdCompany
        {
            get => Setting.GetValueOrDefault(nameof(IdCompany), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(IdCompany), value);
        }

        public static string IdTypeUser
        {
            get => Setting.GetValueOrDefault(nameof(IdTypeUser), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(IdTypeUser), value);
        }

        public static string Status
        {
            get => Setting.GetValueOrDefault(nameof(Status), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(Status), value);
        }
    }
}