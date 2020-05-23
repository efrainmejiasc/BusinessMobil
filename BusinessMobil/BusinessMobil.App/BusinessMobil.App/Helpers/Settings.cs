using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace BusinessMobil.App.Helpers
{
    public static class Settings
    {
        
        static ISettings Setting => CrossSettings.Current;

        public static string Token
        {
            get => Setting.GetValueOrDefault(nameof(Token),string.Empty);
            set => Setting.AddOrUpdateValue(nameof(Token),value);
        }

        public static bool Remember
        {
            get => Setting.GetValueOrDefault(nameof(Remember), false);
            set => Setting.AddOrUpdateValue(nameof(Remember), value);
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

        public static string Id
        {
            get => Setting.GetValueOrDefault(nameof(Id), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(Id), value);
        }

        public static string User
        {
            get => Setting.GetValueOrDefault(nameof(User), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(User), value);
        }

        public static string DNI
        {
            get => Setting.GetValueOrDefault(nameof(DNI), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(DNI), value);
        }

        public static string Password
        {
            get => Setting.GetValueOrDefault(nameof(Password), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(Password), value);
        }

        public static int IdCompany
        {
            get => Setting.GetValueOrDefault(nameof(IdCompany), 0);
            set => Setting.AddOrUpdateValue(nameof(IdCompany), value);
        }

        public static int IdTypeUser
        {
            get => Setting.GetValueOrDefault(nameof(IdTypeUser), 0);
            set => Setting.AddOrUpdateValue(nameof(IdTypeUser), value);
        }

        public static bool Status
        {
            get => Setting.GetValueOrDefault(nameof(Status), false);
            set => Setting.AddOrUpdateValue(nameof(Status), value);
        }

        public static string FotoUser
        {
            get => Setting.GetValueOrDefault(nameof(FotoUser),string.Empty);
            set => Setting.AddOrUpdateValue(nameof(FotoUser), value);
        }

        public static string Telefono
        {
            get => Setting.GetValueOrDefault(nameof(Telefono), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(Telefono), value);
        }

        public static string NombreCompleto
        {
            get => Setting.GetValueOrDefault(nameof(NombreCompleto), string.Empty);
            set => Setting.AddOrUpdateValue(nameof(NombreCompleto), value);
        }
    }
}