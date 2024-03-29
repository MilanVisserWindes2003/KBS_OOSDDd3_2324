﻿using Skepta.Business;
using Skepta.Presentation.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Skepta.Presentation.ViewModel
{
    //ID's for every page for ease of use and to prevent wrong pageID's being used in the rest of the code
    public enum PageId
    {
        Register,
        None,
        Login,
        TextDifficultySelect,
        TextLength,
        ExerciseTypeSelect,
        TextShuffle,
        TextExcersize,
        TextToSpeech,
        TypeWindow,
        Resultaat,
        Exercise,
        Geschiedenis,
        MenuScreen,
        Settings,
        Toetsenbord,
        ChangePassword
    }

    public class MainWindowViewModel : ViewModelBase
    {
        private SkeptaModel model;
        private ViewModelBase currentPage;
        private Dictionary<PageId, ViewModelBase> pages = new Dictionary<PageId, ViewModelBase>();

        private PageId currentPageId;

        public MainWindowViewModel()
        {
            // adding all pages to the dictionary so they are all reachable
            model = new SkeptaModel();
            pages.Add(PageId.Register, new RegistreerViewModel(model));
            pages.Add(PageId.Login, new LoginViewModel(model));
            pages.Add(PageId.TextDifficultySelect, new TextDifficultySelectViewModel(model));
            pages.Add(PageId.TextLength, new TextLenghtSelectViewModel(model));
            pages.Add(PageId.ExerciseTypeSelect, new ExerciseTypeSelectViewModel(model));
            pages.Add(PageId.TextShuffle, new TextShuffleViewModel(model));
            pages.Add(PageId.TextExcersize, new TextExcersizeViewModel(model));
            pages.Add(PageId.TextToSpeech, new TextToSpeechViewModel(model));
            pages.Add(PageId.Resultaat, new ResultaatViewModel(model));
            pages.Add(PageId.Exercise, new ExercisePageViewModel(model));
            pages.Add(PageId.Geschiedenis, new GeschiedenisViewModel(model));
            pages.Add(PageId.MenuScreen, new MenuScreenViewModel(model));
            pages.Add(PageId.Settings, new SettingsViewModel(model));
            pages.Add(PageId.Toetsenbord, new ToetsenbordViewModel(model));
            pages.Add(PageId.ChangePassword, new ChangePasswordViewModel(model));
            SelectPage(PageId.Login);
        }
        // if a key is pressed this is communicated to the application
        public void HandleKey(Key key)
        {
            CurrentPage?.KeyPressed(key);
        }

        private void CurrentPage_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            // navigates to different page depending on what property is changed
            if (e.PropertyName == nameof(ViewModelBase.RequestNext))
            {
                SelectNextPage();
            }
            else if (e.PropertyName == nameof(ViewModelBase.RequestPrev))
            {
                SelectPrevPage();
            }
            else if (e.PropertyName == nameof(ViewModelBase.RequestPage))
            {
                if (currentPage != null)
                {
                    SelectPage(currentPage.RequestPage);
                }
            }
        }

        // this method figures out what page is selected
        private void SelectPage(PageId pageId)
        {
            if (!pages.ContainsKey(pageId))
            {
                return;
            }

            if (currentPageId != pageId)
            {
                if (currentPage != null)
                {
                    currentPage.PropertyChanged -= CurrentPage_PropertyChanged;
                }

                currentPageId = pageId;
                pages[currentPageId].PropertyChanged += CurrentPage_PropertyChanged;
                CurrentPage = pages[currentPageId];
                CurrentPage.OpenPage();
            }
        }


        private void SelectNextPage() => SelectPage(currentPageId + 1);
        private void SelectPrevPage() => SelectPage(currentPageId - 1);

        public ViewModelBase CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                NotifyPropertyChanged(nameof(CurrentPage));
            }
        }
    }
}
