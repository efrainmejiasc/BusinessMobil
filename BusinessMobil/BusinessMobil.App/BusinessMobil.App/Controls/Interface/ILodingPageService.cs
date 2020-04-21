﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BusinessMobil.App.Controls.Interface
{
    public interface ILodingPageService
    {
        void InitLoadingPage(ContentPage loadingIndicatorPage = null);

        void ShowLoadingPage();

        void HideLoadingPage();
    }
}
