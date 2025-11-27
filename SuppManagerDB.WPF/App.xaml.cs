using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.WPF.Configuration;
using SuppManagerDB.DTO;

namespace SuppManagerDB.WPF
{
    public partial class App : Application
    {
        // DI контейнер
        public static ServiceProvider Services { get; private set; }

        // 🔐 Поточний користувач після логіну
        public static User? CurrentUser { get; set; }

        public App()
        {
            Services = AppServices.Configure(); // завантажуємо залежності
        }
    }
}
