using System.Collections;
using System.Windows;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Win32;

    using Prism.Commands;
    using Prism.Mvvm;

    using Repositories;

    internal class DocumentDetailViewModel : BindableBase
    {
        private readonly Action _navigateBack;

        private string _benutzer;

        private string _bezeichnung;

        private DateTime _erfassungsdatum;

        private string _filePath;

        private bool _isRemoveFileEnabled;

        private string _selectedTypItem;

        private string _stichwoerter;

        private List<string> _typItems;

        private DateTime? _valutaDatum;

        public DocumentDetailViewModel(string benutzer, Action navigateBack)
        {
            _navigateBack = navigateBack;
            Benutzer = benutzer;
            Erfassungsdatum = DateTime.Now;
            TypItems = ComboBoxItems.Typ;

            CmdDurchsuchen = new DelegateCommand(OnCmdDurchsuchen);
            CmdSpeichern = new DelegateCommand(OnCmdSpeichern);
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(Bezeichnung)
                || !ValutaDatum.HasValue
                || string.IsNullOrEmpty(SelectedTypItem))
                throw new ArgumentException("Es müssen alle Pflichtfelder ausgefüllt werden!");
        }

        public string Stichwoerter
        {
            get => _stichwoerter;
            set => SetProperty(ref _stichwoerter, value);
        }

        public string Bezeichnung
        {
            get => _bezeichnung;
            set => SetProperty(ref _bezeichnung, value);
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

        public DateTime Erfassungsdatum
        {
            get => _erfassungsdatum;
            set => SetProperty(ref _erfassungsdatum, value);
        }

        public string Benutzer
        {
            get => _benutzer;
            set => SetProperty(ref _benutzer, value);
        }

        public DelegateCommand CmdDurchsuchen { get; }

        public DelegateCommand CmdSpeichern { get; }

        public DateTime? ValutaDatum
        {
            get => _valutaDatum;
            set => SetProperty(ref _valutaDatum, value);
        }

        public bool IsRemoveFileEnabled
        {
            get => _isRemoveFileEnabled;
            set => SetProperty(ref _isRemoveFileEnabled, value);
        }

        private void OnCmdDurchsuchen()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result.GetValueOrDefault())
            {
                _filePath = openFileDialog.FileName;
            }
        }

        private void OnCmdSpeichern()
        {
            // TODO: Add your Code here
            try
            {
                Validate();
                var guid = new GuidProvider().NextGuid;
                var saveService = new SaveService();
                var document = new DocumentItem(ValutaYearAsString + "\\" + guid, _filePath, !_isRemoveFileEnabled);
                var metadata = new MetadataItem(ValutaYearAsString + "\\" + guid,  MetaDataAsMap);
                saveService.SaveDocument(document);
                saveService.SaveDocument(metadata);
                _navigateBack();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private string ValutaYearAsString => _valutaDatum.Value.Year.ToString();
        private IDictionary MetaDataAsMap => new Dictionary<string, object>()
        {
            {"Erfassungsdatum", _erfassungsdatum},
            {"Valutadatum", _valutaDatum},
            {"Bezeichnung", _bezeichnung},
            {"Stichwörter", _stichwoerter},
            {"Typ", _selectedTypItem},
            {"Benutzer", _benutzer}
        };
    }
}