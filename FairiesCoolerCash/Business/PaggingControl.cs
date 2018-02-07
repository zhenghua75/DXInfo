﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Dynamic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FairiesCoolerCash.Business
{
    [TemplatePart(Name = "PART_FirstPageButton", Type = typeof(Button)),
    TemplatePart(Name = "PART_PreviousPageButton", Type = typeof(Button)),
    TemplatePart(Name = "PART_PageTextBox", Type = typeof(TextBox)),
    TemplatePart(Name = "PART_GoToPageButton", Type = typeof(Button)),
    TemplatePart(Name = "PART_NextPageButton", Type = typeof(Button)),
    TemplatePart(Name = "PART_LastPageButton", Type = typeof(Button)),
    TemplatePart(Name = "PART_PageSizesCombobox", Type = typeof(ComboBox))]
    public class PaggingControl : Control
    {
        #region CUSTOM CONTROL VARIABLES

        protected Button btnFirstPage, btnPreviousPage, btnGoToPage, btnNextPage, btnLastPage;
        protected TextBox txtPage;
        protected ComboBox cmbPageSizes;

        #endregion


        #region PROPERTIES

        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty PageProperty;
        public static readonly DependencyProperty TotalPagesProperty;
        public static readonly DependencyProperty TotalRecordsProperty;
        public static readonly DependencyProperty StartRecordProperty;
        public static readonly DependencyProperty EndRecordProperty;
        public static readonly DependencyProperty PageSizesProperty;
        public static readonly DependencyProperty MyQueryProperty;
        public static readonly DependencyProperty FilterTagProperty;

        public ObservableCollection<object> ItemsSource
        {
            get
            {
                return GetValue(ItemsSourceProperty) as ObservableCollection<object>;
            }
            protected set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public uint Page
        {
            get
            {
                return (uint)GetValue(PageProperty);
            }
            set
            {
                SetValue(PageProperty, value);
            }
        }
        public uint TotalPages
        {
            get
            {
                return (uint)GetValue(TotalPagesProperty);
            }
            protected set
            {
                SetValue(TotalPagesProperty, value);
            }
        }
        public uint TotalRecords
        {
            get
            {
                return (uint)GetValue(TotalRecordsProperty);
            }
            protected set
            {
                SetValue(TotalRecordsProperty, value);
            }
        }
        public uint StartRecord
        {
            get
            {
                return (uint)GetValue(StartRecordProperty);
            }
            protected set
            {
                SetValue(StartRecordProperty, value);
            }
        }
        public uint EndRecord
        {
            get
            {
                return (uint)GetValue(EndRecordProperty);
            }
            protected set
            {
                SetValue(EndRecordProperty, value);
            }
        }
        public ObservableCollection<uint> PageSizes
        {
            get
            {
                return GetValue(PageSizesProperty) as ObservableCollection<uint>;
            }
            protected set
            {
                SetValue(PageSizesProperty, value);
            }
        }

        public IQueryable MyQuery
        {
            get
            {
                return GetValue(MyQueryProperty) as IQueryable;
            }
            set
            {
                SetValue(MyQueryProperty, value);
                this.Page = 0;
                this.Navigate(PageChanges.First);
            }
        }

        public object FilterTag
        {
            get
            {
                return GetValue(FilterTagProperty);
            }
            set
            {
                SetValue(FilterTagProperty, value);
            }
        }

        #endregion

        #region EVENTS

        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs args);

        public static readonly RoutedEvent PreviewPageChangeEvent;
        public static readonly RoutedEvent PageChangedEvent;

        public event PageChangedEventHandler PreviewPageChange
        {
            add
            {
                AddHandler(PreviewPageChangeEvent, value);
            }
            remove
            {
                RemoveHandler(PreviewPageChangeEvent, value);
            }
        }

        public event PageChangedEventHandler PageChanged
        {
            add
            {
                AddHandler(PageChangedEvent, value);
            }
            remove
            {
                RemoveHandler(PageChangedEvent, value);
            }
        }

        #endregion

        #region CONTROL CONSTRUCTORS
        private static void OnMyQueryPropertyChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            PaggingControl pc = sender as PaggingControl;
            pc.Page = 0;
            pc.Navigate(PageChanges.First);
        }
        static PaggingControl()
        {
            ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<object>), typeof(PaggingControl), new PropertyMetadata(new ObservableCollection<object>()));
            PageProperty = DependencyProperty.Register("Page", typeof(uint), typeof(PaggingControl));
            TotalPagesProperty = DependencyProperty.Register("TotalPages", typeof(uint), typeof(PaggingControl));
            TotalRecordsProperty = DependencyProperty.Register("TotalRecords", typeof(uint), typeof(PaggingControl));
            StartRecordProperty = DependencyProperty.Register("StartRecord", typeof(uint), typeof(PaggingControl));
            EndRecordProperty = DependencyProperty.Register("EndRecord", typeof(uint), typeof(PaggingControl));
            PageSizesProperty = DependencyProperty.Register("PageSizes", typeof(ObservableCollection<uint>), typeof(PaggingControl), new PropertyMetadata(new ObservableCollection<uint>()));
            MyQueryProperty = DependencyProperty.Register("MyQuery", typeof(IQueryable), typeof(PaggingControl), new FrameworkPropertyMetadata(null, OnMyQueryPropertyChanged));
            FilterTagProperty = DependencyProperty.Register("FilterTag", typeof(object), typeof(PaggingControl));

            PreviewPageChangeEvent = EventManager.RegisterRoutedEvent("PreviewPageChange", RoutingStrategy.Bubble, typeof(PageChangedEventHandler), typeof(PaggingControl));
            PageChangedEvent = EventManager.RegisterRoutedEvent("PageChanged", RoutingStrategy.Bubble, typeof(PageChangedEventHandler), typeof(PaggingControl));

            //cmbPageSizes.SelectedIndex = 0;
        }

        public PaggingControl()
        {
            this.Loaded += new RoutedEventHandler(PaggingControl_Loaded);
            List<uint> lui = new List<uint>() { 100, 200, 300, 500, 1000 };
            this.PageSizes = new ObservableCollection<uint>(lui);
        }

        ~PaggingControl()
        {
            UnregisterEvents();
        }

        #endregion

        #region EVENTS

        void PaggingControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Template == null)
            {
                throw new Exception("Control template not assigned.");
            }

            //if (PageContract == null)
            //{
            //    throw new Exception("IPageControlContract not assigned.");
            //}

            RegisterEvents();
            SetDefaultValues();
            BindProperties();
        }

        void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.First);
        }

        void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.Previous);
        }
        void btnGoToPage_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.Current);
        }
        void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.Next);
        }

        void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.Last);
        }

        void txtPage_LostFocus(object sender, RoutedEventArgs e)
        {
            Navigate(PageChanges.Current);
        }

        void cmbPageSizes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigate(PageChanges.Current);
        }

        #endregion

        #region INTERNAL METHODS

        public override void OnApplyTemplate()
        {
            btnFirstPage = this.Template.FindName("PART_FirstPageButton", this) as Button;
            btnPreviousPage = this.Template.FindName("PART_PreviousPageButton", this) as Button;
            btnGoToPage = this.Template.FindName("PART_GoToPageButton", this) as Button;
            txtPage = this.Template.FindName("PART_PageTextBox", this) as TextBox;
            btnNextPage = this.Template.FindName("PART_NextPageButton", this) as Button;
            btnLastPage = this.Template.FindName("PART_LastPageButton", this) as Button;
            cmbPageSizes = this.Template.FindName("PART_PageSizesCombobox", this) as ComboBox;

            if (btnFirstPage == null ||
                btnPreviousPage == null ||
                txtPage == null ||
                btnNextPage == null ||
                btnLastPage == null ||
                cmbPageSizes == null)
            {
                throw new Exception("Invalid Control template.");
            }

            base.OnApplyTemplate();
        }

        private void RegisterEvents()
        {
            btnFirstPage.Click += new RoutedEventHandler(btnFirstPage_Click);
            btnPreviousPage.Click += new RoutedEventHandler(btnPreviousPage_Click);
            btnGoToPage.Click += new RoutedEventHandler(btnGoToPage_Click);
            btnNextPage.Click += new RoutedEventHandler(btnNextPage_Click);
            btnLastPage.Click += new RoutedEventHandler(btnLastPage_Click);

            txtPage.LostFocus += new RoutedEventHandler(txtPage_LostFocus);

            cmbPageSizes.SelectionChanged += new SelectionChangedEventHandler(cmbPageSizes_SelectionChanged);
        }

        private void UnregisterEvents()
        {
            btnFirstPage.Click -= btnFirstPage_Click;
            btnPreviousPage.Click -= btnPreviousPage_Click;
            btnNextPage.Click -= btnNextPage_Click;
            btnLastPage.Click -= btnLastPage_Click;

            txtPage.LostFocus -= txtPage_LostFocus;

            cmbPageSizes.SelectionChanged -= cmbPageSizes_SelectionChanged;
        }

        private void SetDefaultValues()
        {
            ItemsSource = new ObservableCollection<object>();

            cmbPageSizes.IsEditable = false;
            cmbPageSizes.SelectedIndex = 0;
        }

        private void BindProperties()
        {
            Binding propBinding;

            propBinding = new Binding("Page");
            propBinding.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
            propBinding.Mode = BindingMode.TwoWay;
            propBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            txtPage.SetBinding(TextBox.TextProperty, propBinding);

            propBinding = new Binding("PageSizes");
            propBinding.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
            propBinding.Mode = BindingMode.TwoWay;
            propBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            cmbPageSizes.SetBinding(ComboBox.ItemsSourceProperty, propBinding);
        }

        private void RaisePageChanged(uint OldPage, uint NewPage)
        {
            PageChangedEventArgs args = new PageChangedEventArgs(PageChangedEvent, OldPage, NewPage, TotalPages);
            RaiseEvent(args);
        }

        private void RaisePreviewPageChange(uint OldPage, uint NewPage)
        {
            PageChangedEventArgs args = new PageChangedEventArgs(PreviewPageChangeEvent, OldPage, NewPage, TotalPages);
            RaiseEvent(args);
        }

        public void Navigate(PageChanges change)
        {
            uint totalRecords;
            uint newPageSize;

            if (this.MyQuery == null || this.cmbPageSizes.ItemsSource == null)
            {
                return;
            }

            totalRecords = (uint)MyQuery.Count();
            newPageSize = (uint)cmbPageSizes.SelectedItem;
            if (totalRecords == 0)
            {
                ItemsSource.Clear();
                TotalPages = 1;
                Page = 1;
                StartRecord = 0;
                EndRecord = 0;
            }
            else
            {
                TotalPages = (totalRecords / newPageSize) + (uint)((totalRecords % newPageSize == 0) ? 0 : 1);
            }

            uint newPage = 1;

            switch (change)
            {
                case PageChanges.First:
                    if (Page == 1)
                    {
                        return;
                    }
                    break;
                case PageChanges.Previous:
                    newPage = (Page - 1 > TotalPages) ? TotalPages : (Page - 1 < 1) ? 1 : Page - 1;
                    break;
                case PageChanges.Current:
                    newPage = (Page > TotalPages) ? TotalPages : (Page < 1) ? 1 : Page;
                    break;
                case PageChanges.Next:
                    newPage = (Page + 1 > TotalPages) ? TotalPages : Page + 1;
                    //(Page + 1) < 1 ? 1 :
                    break;
                case PageChanges.Last:
                    if (Page == TotalPages)
                    {
                        return;
                    }
                    newPage = TotalPages;
                    break;
                default:
                    break;
            }

            uint StartingIndex = (newPage - 1) * newPageSize;
            StartRecord = StartingIndex + 1;

            uint oldPage = Page;
            RaisePreviewPageChange(Page, newPage);

            Page = newPage;
            TotalRecords = totalRecords;
            ItemsSource.Clear();

            IQueryable fetchData = MyQuery.Skip(Convert.ToInt32(StartingIndex)).Take(Convert.ToInt32(newPageSize));
            var enumerator = fetchData.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ItemsSource.Add(enumerator.Current);
            }
            EndRecord = StartingIndex + Convert.ToUInt32(ItemsSource.Count);
            RaisePageChanged(oldPage, Page);
        }

        #endregion
    }
    public enum PageChanges
    {
        First,
        Previous,
        Current,
        Next,
        Last
    }

    public class PageChangedEventArgs : RoutedEventArgs
    {
        #region PRIVATE VARIABLES

        private uint _OldPage, _NewPage, _TotalPages;

        #endregion

        #region PROPERTIES

        public uint OldPage
        {
            get
            {
                return _OldPage;
            }
        }

        public uint NewPage
        {
            get
            {
                return _NewPage;
            }
        }

        public uint TotalPages
        {
            get
            {
                return _TotalPages;
            }
        }

        #endregion

        #region CONSTRUCTOR

        public PageChangedEventArgs(RoutedEvent EventToRaise, uint OldPage, uint NewPage, uint TotalPages)
            : base(EventToRaise)
        {
            _OldPage = OldPage;
            _NewPage = NewPage;
            _TotalPages = TotalPages;
        }

        #endregion
    }
}
