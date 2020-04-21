﻿using BusinessMobil.App.Helpers;
using BusinessMobil.App.Model;
using BusinessMobil.App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BusinessMobil.App.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        public ICommand ScannerCommand { get; set; }
        public ICommand RegisterDeviceCommand { get; set; }


        public MenuViewModel()
        {
            LoadMenus();
            User = Settings.User;
            EmailUser = Settings.Email;
        }

        private string user;

        public string User
        {
            get { return user; }
            set { user = value; }
        }


        private string emailUser;
        public string EmailUser
        {
            get { return emailUser; }
            set { emailUser = value; }
        }



        ObservableCollection<MenuModel> listaMenu;
        public ObservableCollection<MenuModel> ListaMenu
        {
            get => listaMenu;
            set
            {
                listaMenu = value;
                OnPropertyChanged();
            }
        }

        void LoadMenus()
        {
            var menu = new List<MenuModel>
            {
                new MenuModel{ Title ="Perfil", Icon ="menuuser.png", PageName = "PerfilPage" },
                new MenuModel{ Title ="Inicio", Icon ="menulogin.png", PageName = "", MenuDetail ="Autentificación" },
                new MenuModel{ Title ="Asistencia Clase", Icon ="menulista.png", PageName = "GenerarListaAsistenciaPage", MenuDetail = "Listado" },
                //new MenuModel{ Title ="Actualizar Asistencia", Icon ="menuactualizarlista.png", PageName = "", MenuDetail ="Lector QR" },
                new MenuModel{ Title ="Registrar Dipositivo", Icon ="menudevice.png", PageName = "RegisterDevicePage", MenuDetail = "Tu Teléfono" },
                new MenuModel{ Title ="Escanear Carnet", Icon ="menuqr.png", PageName = "ScannerPage", MenuDetail ="Ver" },
                new MenuModel{ Title ="Salir", Icon ="menulogout.png", PageName = "", MenuDetail = "Cerrar Aplicación" },

                //new MenuModel { Icon = "", Title = "Escanear QR", PageName = "ScannerPage" },
                //new MenuModel { Icon = "", Title = "Registrar Dispositivo", PageName = "RegisterDevicePage" },
                //new MenuModel { Icon = "", Title = "Lista de Asistencia", PageName = "GenerarListaAsistenciaPage" },
                //new MenuModel { Icon = "", Title = "Escanear QR", PageName = "ScannerPage" }
            };

            listaMenu = new ObservableCollection<MenuModel>(menu.Select(m=> new MenuItemViewModel { Icon = m.Icon, Title = m.Title, PageName = m.PageName }).OrderBy(o=> o.Title).ToList());
        }

    }
}
