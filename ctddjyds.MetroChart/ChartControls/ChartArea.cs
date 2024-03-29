﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
#if NETFX_CORE

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

#else

using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Media;

#endif
namespace ctddjyds.MetroChart
{
    public class ChartArea : ContentControl
    {
        static ChartArea()
        {
#if NETFX_CORE
            //do nothing
#else
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChartArea), new FrameworkPropertyMetadata(typeof(ChartArea)));
#endif
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PieChart"/> class.
        /// </summary>
        public ChartArea()
        {
#if NETFX_CORE
            this.DefaultStyleKey = typeof(ChartArea);
#endif
        }

        public static readonly DependencyProperty ParentChartProperty =
            DependencyProperty.Register("ParentChart",
            typeof(ChartBase),
            typeof(ChartArea),
            new PropertyMetadata(null));
        public static readonly DependencyProperty ChartLegendItemStyleProperty =
            DependencyProperty.Register("ChartLegendItemStyle",
            typeof(Style),
            typeof(ChartArea),
            new PropertyMetadata(null));

        public ChartBase ParentChart
        {
            get { return (ChartBase)GetValue(ParentChartProperty); }
            set { SetValue(ParentChartProperty, value); }
        }

        public ObservableCollection<string> GridLines
        {
            get
            {
                return ParentChart.GridLines;
            }
        }

        public ObservableCollection<DataPointGroup> DataPointGroups
        {
            get
            {
                return ParentChart.DataPointGroups;
            }
        }

        public ObservableCollection<ChartLegendItemViewModel> ChartLegendItems
        {
            get
            {
                return ParentChart.ChartLegendItems;
            }
        } 

        public Style ChartLegendItemStyle
        {
            get { return (Style)GetValue(ChartLegendItemStyleProperty); }
            set { SetValue(ChartLegendItemStyleProperty, value); }
        }
    }
}
