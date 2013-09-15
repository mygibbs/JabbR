﻿using System;
using System.Windows.Input;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using JabbR.Client.UI.Core.Interfaces;
using System.Collections.Generic;
using Cirrious.CrossCore.Platform;
using Newtonsoft.Json;

namespace JabbR.Client.UI.Core.ViewModels
{
    public abstract class NavigationParametersBase
    {
        public const string Key = "Nav";
    }

    public class BaseViewModel : MvxViewModel
    {

        protected void ShowViewModel<TViewModel>(NavigationParametersBase parameters)
            where TViewModel : IMvxViewModel
        {
            var text = JsonConvert.SerializeObject(parameters);
            this.ShowViewModel<TViewModel>(new MvxBundle(new Dictionary<string, string>()
                    {
                        { NavigationParametersBase.Key, text }
                    }
            ));
        }

        private string _displayName = "JabbR";
        public string DisplayName
        {
            get { return _displayName; }
        }

        public string PageName { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        private bool _hasError;
        public bool HasError
        {
            get { return _hasError; }
            set
            {
                _hasError = value;
                RaisePropertyChanged(() => HasError);
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                RaisePropertyChanged(() => ErrorMessage);
                HasError = !String.IsNullOrEmpty(value);
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new MvxCommand(RequestClose);
            }
        }

        protected void RequestClose()
        {
            var closer = Mvx.Resolve<IViewModelCloser>();
            closer.RequestClose(this);
        }
    }
}