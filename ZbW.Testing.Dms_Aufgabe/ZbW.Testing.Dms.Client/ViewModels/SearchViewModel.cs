using System.Configuration;
using System.Diagnostics;
using System.IO;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System.Collections.Generic;

    using Prism.Commands;
    using Prism.Mvvm;

    using Model;
    using Repositories;

    internal class SearchViewModel : BindableBase
    {
        private List<MetadataItem> _filteredMetadataItems;

        private MetadataItem _selectedMetadataItem;

        private string _selectedTypItem;

        private string _suchbegriff;

        private List<string> _typItems;

        public SearchViewModel()
        {
            TypItems = ComboBoxItems.Typ;

            CmdSuchen = new DelegateCommand(OnCmdSuchen);
            CmdReset = new DelegateCommand(OnCmdReset);
            CmdOeffnen = new DelegateCommand(OnCmdOeffnen, OnCanCmdOeffnen);
        }

        public DelegateCommand CmdOeffnen { get; }

        public DelegateCommand CmdSuchen { get; }

        public DelegateCommand CmdReset { get; }

        public string Suchbegriff
        {
            get => _suchbegriff;
            set => SetProperty(ref _suchbegriff, value);
        }

        public List<string> TypItems
        {
            get => _typItems;
            set => SetProperty(ref _typItems, value);
        }

        public string SelectedTypItem
        {
            get => _selectedTypItem;
            set => SetProperty(ref _selectedTypItem, value);
        }

        public List<MetadataItem> FilteredMetadataItems
        {
            get => _filteredMetadataItems;
            set => SetProperty(ref _filteredMetadataItems, value);
        }

        public MetadataItem SelectedMetadataItem
        {
            get => _selectedMetadataItem;
            set
            {
                if (SetProperty(ref _selectedMetadataItem, value))
                    CmdOeffnen.RaiseCanExecuteChanged();
            }
        }

        private bool OnCanCmdOeffnen()
        {
            return SelectedMetadataItem != null;
        }

        private void OnCmdOeffnen()
        {
            // TODO: Add your Code here
            var metadataFileName = SelectedMetadataItem.FileName;
            var guid = metadataFileName.Substring(metadataFileName.IndexOf('{'), 37);
            var searchService = new SearchService();
            var documentFile = searchService.FindDocumentFile(guid);
            Process.Start(documentFile.FullName);
        }

        private void OnCmdSuchen()
        {
            // TODO: Add your Code here
            var searchService = new SearchService();
            FilteredMetadataItems = searchService.FindMetadataItems(_suchbegriff, _selectedTypItem);
        }

        private void OnCmdReset()
        {
            // TODO: Add your Code here
            Suchbegriff = "";
            SelectedTypItem = "";
            FilteredMetadataItems = new List<MetadataItem>();
        }
    }
}